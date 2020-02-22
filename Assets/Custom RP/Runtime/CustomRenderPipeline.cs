using UnityEngine;
using UnityEngine.Rendering;

public class CustomRenderPipeline : RenderPipeline
{
    protected override void Render(
        ScriptableRenderContext context, Camera[] cameras
    )
    { }

    protected override RenderPipeline CreatePipeline()
    {
        return new CustomRenderPipeline();
    }
}