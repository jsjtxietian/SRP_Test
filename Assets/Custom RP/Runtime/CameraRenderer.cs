using UnityEngine;
using UnityEngine.Rendering;

public class CameraRenderer
{
    ScriptableRenderContext context;
    const string bufferName = "Render Camera";

    CommandBuffer buffer = new CommandBuffer
    {
        name = bufferName
    };

    Camera camera;

    public void Render(ScriptableRenderContext context, Camera camera)
    {
        this.context = context;
        this.camera = camera;
        Setup();
        DrawVisibleGeometry();
        Submit();
    }

    void DrawVisibleGeometry()
    {
        context.DrawSkybox(camera);
    }

    void Submit()
    {
        buffer.EndSample(bufferName);
        ExecuteBuffer();
        context.Submit();
    }

    void Setup()
    {
        buffer.BeginSample(bufferName);//inject profiler samples, which will show up both in the profiler and the frame debugger
        ExecuteBuffer();
        context.SetupCameraProperties(camera);
    }

    void ExecuteBuffer () {
		context.ExecuteCommandBuffer(buffer);
		buffer.Clear();
	}
}