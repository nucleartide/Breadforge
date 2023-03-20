using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostprocessRenderPass : ScriptableRenderPass
{
    public RenderTargetIdentifier? cameraColorTarget;

    private Material material;

    private RenderTargetHandle tempRenderTargetHandle;

    public PostprocessRenderPass(Material material)
    {
        this.material = material;
        tempRenderTargetHandle.Init("_TemporaryColorTexture");
    }

    public void Initialize(RenderTargetIdentifier cameraColorTarget)
    {
        this.cameraColorTarget = cameraColorTarget;
    }

    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
    {
        // Sanity check.
        if (!cameraColorTarget.HasValue)
            throw new System.Exception("Whoops, did you forget to Initialize() the PostprocessRenderPass?");

        // Get command buffer.
        var commandBuffer = CommandBufferPool.Get("CustomBlitRenderPass");

        // Get temporary render texture that shares the same properties as the camera.
        commandBuffer.GetTemporaryRT(tempRenderTargetHandle.id, renderingData.cameraData.cameraTargetDescriptor);

        // Copy data from CameraColorTarget to temporary render texture, but use the provided material.
        Blit(commandBuffer, cameraColorTarget.Value, tempRenderTargetHandle.Identifier(), material);

        // Copy data from temporary render texture back to CameraColorTarget.
        Blit(commandBuffer, tempRenderTargetHandle.Identifier(), cameraColorTarget.Value);

        // Execute commands queued in our command buffer.
        context.ExecuteCommandBuffer(commandBuffer);

        // Release command buffer.
        CommandBufferPool.Release(commandBuffer);
    }
}
