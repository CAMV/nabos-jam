using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[Serializable]
[PostProcess(typeof(BlurRenderer), PostProcessEvent.AfterStack, "Custom/Blur")]
public sealed class BlurEffect : PostProcessEffectSettings
{
    [Range(0f, 1f), Tooltip("Blurr Intensity.")]
    public FloatParameter radius = new FloatParameter { value = 0.5f };

    [Range(0f, 1f), Tooltip("Blurr Intensity.")]
    public FloatParameter sigma = new FloatParameter { value = 0.5f };
}
 
public sealed class BlurRenderer : PostProcessEffectRenderer<BlurEffect>
{
    public override void Render(PostProcessRenderContext context)
    {
        var sheet = context.propertySheets.Get(Shader.Find("Hidden/Custom/Blur"));
        sheet.properties.SetFloat("_Radius", settings.radius);
        sheet.properties.SetFloat("_Sigma", settings.sigma);
        context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
    }
}
