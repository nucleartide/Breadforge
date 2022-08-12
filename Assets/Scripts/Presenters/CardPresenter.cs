using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class CardPresenter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [field: SerializeField]
    public Card Card
    {
        private get;
        set;
    }

    public string Identifier = System.Guid.NewGuid().ToString();

    [SerializeField]
    TMP_Text attackText;

    [SerializeField]
    TMP_Text healthText;

    [SerializeField]
    TMP_Text summonCostText;

    [SerializeField]
    TMP_Text nameText;

    [SerializeField]
    [Tooltip("When enabled, this object makes it seem like the card is glowing.")]
    [NotNull]
    GameObject glow;

    public GameViewPresenter GameViewPresenter;

    [field: SerializeField]
    public CurrentlySelectedCard CurrentlySelectedCard
    {
        private get;
        set;
    }

    public PlayingFieldPresenter PlayingFieldPresenter
    {
        get;
        set;
    }

    void Update()
    {
        attackText.text = "ATK: " + Card.Attack.ToString();
        healthText.text = "HP: " + Card.CurrentHealth.ToString();
        summonCostText.text = "COST: " + Card.SummonCost.ToString();
        nameText.text = Card.Name;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!PlayingFieldPresenter.IsPlayingCardOnField(this))
			glow.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        glow.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        PlayCard();
    }

    void PlayCard()
    {
        // Zoom into the playing field.
        GameViewPresenter.CurrentPose = GameViewPresenter.PoseState.ObservePlayingField;

        // Maintain the currently selected card.
        CurrentlySelectedCard.Card = this;
        Debug.Log($"Set currently selected card to {CurrentlySelectedCard.Card.Card.Name}");
    }
}
