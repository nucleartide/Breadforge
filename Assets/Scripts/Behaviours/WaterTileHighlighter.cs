using UnityEngine;

public class WaterTileHighlighter : MonoBehaviour
{
    [NotNull]
    [SerializeField]
    private MeshRenderer meshRenderer;

    private Color32 waterTransparentColor = new Color32(1, 1, 1, 0);

    [SerializeField]
    private Color32 waterHighlightColor;

    private Material[] waterMaterials;

    private void Awake()
    {
        waterMaterials = meshRenderer.materials;
    }

    private void OnDestroy()
    {
        foreach (var material in waterMaterials)
            Destroy(material);
    }

    public void Highlight()
    {
        foreach (var material in waterMaterials)
            material.color = waterHighlightColor;
    }

    public void Unhighlight()
    {
        foreach (var material in waterMaterials)
            material.color = waterTransparentColor;
    }
}
