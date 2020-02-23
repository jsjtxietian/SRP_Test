using UnityEngine;
using UnityEngine.Rendering;

public class CustomRenderPipeline : RenderPipeline
{
    CameraRenderer renderer = new CameraRenderer();

    protected override void Render(
        ScriptableRenderContext context, Camera[] cameras)
    {
        foreach (Camera camera in cameras)
        {
            renderer.Render(context, camera);
        }
    }

    public CustomRenderPipeline () {
        //all their data gets cached on the GPU and each draw call only has to 
        //contain an offset to the correct memory location
		GraphicsSettings.useScriptableRenderPipelineBatching = true;
	}
}