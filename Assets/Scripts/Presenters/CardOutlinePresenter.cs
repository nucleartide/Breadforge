using UnityEngine;
using UnityEngine.EventSystems;

public class CardOutlinePresenter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    [NotNull]
    [Tooltip("When enabled, makes it appear as if the card outline has a glow.")]
    GameObject glow;

    public void OnPointerEnter(PointerEventData eventData)
    {
        glow.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        glow.SetActive(false);
    }
}
