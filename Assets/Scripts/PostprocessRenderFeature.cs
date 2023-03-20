using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEngine;

public class PostprocessRenderFeature : ScriptableRendererFeature
{
    [System.Serializable]
    public class Settings
    {
        [NotNull]
        public Material Material;
    }

    [SerializeField]
    Settings settings = new Settings();

    PostprocessRenderPass postprocessRenderPass;

    /// <inheritdoc/>
    public override void Create()
    {
        postprocessRenderPass = new PostprocessRenderPass(settings.Material);
        postprocessRenderPass.renderPassEvent = RenderPassEvent.BeforeRenderingPostProcessing;
        postprocessRenderPass.ConfigureClear(ClearFlag.All, Color.black);
    }

    /// Here you can inject one or multiple render passes in the renderer.
    /// This method is called when setting up the renderer once per-camera.
    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        postprocessRenderPass.Initialize(renderer.cameraColorTarget);
        renderer.EnqueuePass(postprocessRenderPass);
    }
}
