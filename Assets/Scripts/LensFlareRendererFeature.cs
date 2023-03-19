using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class LensFlareRendererFeature : ScriptableRendererFeature
{
    public class LensFlarePass : ScriptableRenderPass
    {
        private Material material;
        private Mesh mesh;

        public LensFlarePass(Material material, Mesh mesh)
        {
            this.material = material;
            this.mesh = mesh;
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            Debug.Log(message: "The Execute() method runs.");
            var command = CommandBufferPool.Get(name: "LensFlarePass");

            // Get the Camera data from the renderingData argument.
            Camera camera = renderingData.cameraData.camera;
            // Set the projection matrix so that Unity draws the quad in screen space
            command.SetViewProjectionMatrices(Matrix4x4.identity, Matrix4x4.identity);
            // Add the scale variable, use the Camera aspect ratio for the y coordinate
            Vector3 scale = new Vector3(1, camera.aspect, 1);
            // Draw a quad for each Light, at the screen space position of the Light.
            foreach (VisibleLight visibleLight in renderingData.lightData.visibleLights)
            {
                Light light = visibleLight.light;
                // Convert the position of each Light from world to viewport point.
                Vector3 position =
                    camera.WorldToViewportPoint(light.transform.position) * 2 - Vector3.one;
                // Set the z coordinate of the quads to 0 so that Uniy draws them on the same plane.
                position.z = 0;
                // Change the Matrix4x4 argument in the cmd.DrawMesh method to use the position and
                // the scale variables.
                command.DrawMesh(mesh, Matrix4x4.TRS(position, Quaternion.identity, scale), material, 0, 0);
            }

            context.ExecuteCommandBuffer(command);
            CommandBufferPool.Release(command);
        }
    }

    private LensFlarePass lensFlarePass;

    [SerializeField]
    private Material material;

    [SerializeField]
    private Mesh mesh;

    /// Create: Unity calls this method on the following events:
    /// When the Renderer Feature loads the first time.
    /// When you enable or disable the Renderer Feature.
    /// When you change a property in the inspector of the Renderer Feature.
    public override void Create()
    {
        if (material != null && mesh != null)
            lensFlarePass = new LensFlarePass(material, mesh);

        lensFlarePass.renderPassEvent = RenderPassEvent.AfterRenderingSkybox;
    }

    /// AddRenderPasses: Unity calls this method every frame, once for each
    /// Camera. This method lets you inject ScriptableRenderPass instances into
    /// the scriptable Renderer.
    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        if (lensFlarePass != null)
            renderer.EnqueuePass(lensFlarePass);
    }
}
