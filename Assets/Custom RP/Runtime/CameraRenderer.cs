using UnityEngine;
using UnityEngine.Rendering;

public partial class CameraRenderer
{
    static ShaderTagId
		unlitShaderTagId = new ShaderTagId("SRPDefaultUnlit"),
		litShaderTagId = new ShaderTagId("CustomLit");

    ScriptableRenderContext context;
    const string bufferName = "Render Camera";
    CullingResults cullingResults;

    CommandBuffer buffer = new CommandBuffer
    {
        name = bufferName
    };

    Camera camera;

    public void Render(ScriptableRenderContext context, Camera camera,
                        bool useDynamicBatching, bool useGPUInstancing)
    {
        this.context = context;
        this.camera = camera;
        PrepareBuffer();
        PrepareForSceneWindow();
        if (!Cull())
        {
            return;
        }
        Setup();
        DrawVisibleGeometry(useDynamicBatching,useGPUInstancing);
        DrawUnsupportedShaders();
        DrawGizmos();
        Submit();
    }

    //culling those that fall outside of the view frustum of the camera.
    bool Cull()
    {
        if (camera.TryGetCullingParameters(out ScriptableCullingParameters p))
        {
            cullingResults = context.Cull(ref p);
            return true;
        }
        return false;
    }


    void DrawVisibleGeometry(bool useDynamicBatching, bool useGPUInstancing)
    {
        //determine whether orthographic or distance-based sorting applies.
        var sortingSettings = new SortingSettings(camera)
        {
            criteria = SortingCriteria.CommonOpaque
        };
        var drawingSettings = new DrawingSettings(
            unlitShaderTagId, sortingSettings
        )
        {
            enableDynamicBatching = useDynamicBatching,
			enableInstancing = useGPUInstancing
        };
        drawingSettings.SetShaderPassName(1, litShaderTagId);

        var filteringSettings = new FilteringSettings(RenderQueueRange.opaque);

        context.DrawRenderers(
            cullingResults, ref drawingSettings, ref filteringSettings
        );


        context.DrawSkybox(camera);

        //transparent
        sortingSettings.criteria = SortingCriteria.CommonTransparent;
        drawingSettings.sortingSettings = sortingSettings;
        filteringSettings.renderQueueRange = RenderQueueRange.transparent;

        context.DrawRenderers(
            cullingResults, ref drawingSettings, ref filteringSettings
        );
    }

    void Submit()
    {
        buffer.EndSample(SampleName);
        ExecuteBuffer();
        context.Submit();
    }

    void Setup()
    {
        context.SetupCameraProperties(camera);
        CameraClearFlags flags = camera.clearFlags;
        //ClearRenderTarget requires at least three arguments. 
        //The first two indicate whether the depth and color data should be cleared
        // The third argument is the color used to clearing
        buffer.ClearRenderTarget(
            flags <= CameraClearFlags.Depth,
            flags == CameraClearFlags.Color,
            flags == CameraClearFlags.Color ?
                camera.backgroundColor.linear : Color.clear);
        buffer.BeginSample(SampleName);//inject profiler samples, which will show up both in the profiler and the frame debugger
        ExecuteBuffer();
    }

    void ExecuteBuffer()
    {
        context.ExecuteCommandBuffer(buffer);
        buffer.Clear();
    }


}