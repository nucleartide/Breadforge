using UnityEngine;
using UnityEngine.EventSystems;

public class CardOutlinePresenter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    [NotNull]
    [Tooltip("When enabled, makes it appear as if the card outline has a glow.")]
    GameObject glow;

    [field: SerializeField]
    public CurrentlySelectedCard CurrentlySelectedCard
    {
        get;
        set;
	}

    public enum CardOutlineType
    { 
        Player,
        Enemy,
    }

    public CardOutlineType cardOutlineType
    {
        get;
        set;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (CurrentlySelectedCard.Card != null && cardOutlineType == CardOutlineType.Player)
			glow.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (CurrentlySelectedCard.Card != null && cardOutlineType == CardOutlineType.Player)
			glow.SetActive(false);
    }
}
