using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Assertions;

public class TileUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IPointerDownHandler
{
    TileQuantity tileQuantity;
    TileEventManager tileEventManager;

    [SerializeField]
    [NotNull]
    RawImage backgroundImage;

    [SerializeField]
    [NotNull]
    TMP_Text label;

    public GameObject Initialize(TileQuantity newTileQuantity, Vector3 position, TileEventManager tileEventManager)
    {
        tileQuantity = newTileQuantity;
        transform.position = position;
        this.tileEventManager = tileEventManager;
        return gameObject;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        tileEventManager.ClickTile(tileQuantity);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (tileQuantity.Tile != null)
        {
            backgroundImage.color = tileQuantity.Tile.Hover;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (tileQuantity.Tile != null)
        {
            backgroundImage.color = tileQuantity.Tile.Idle;
            tileEventManager.MouseLeaveTile(tileQuantity);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        tileEventManager.ReleaseTile(tileQuantity);
    }

    void Start()
    {
        var pos = transform.position;
        if (tileQuantity.Tile != null)
        {
            label.text = $"{tileQuantity.Tile.Label}:\n({pos.x}, {pos.z})";
            backgroundImage.color = tileQuantity.Tile.Idle;
        }
    }

}
