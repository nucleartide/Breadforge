using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class CardPresenter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    /// <summary>
    /// Passed in dynamically.
    /// </summary>
    [field: SerializeField]
    public Card Model
    {
        get;
        set;
    }

    public readonly string Identifier = System.Guid.NewGuid().ToString();

    [SerializeField]
    [NotNull]
    TMP_Text attackText;

    [SerializeField]
    [NotNull]
    TMP_Text healthText;

    [SerializeField]
    [NotNull]
    TMP_Text summonCostText;

    [SerializeField]
    [NotNull]
    TMP_Text nameText;

    [SerializeField]
    [Tooltip("When enabled, this object makes it seem like the card is glowing.")]
    [NotNull]
    GameObject glow;

    [field: SerializeField]
    public GameViewPresenter GameViewPresenter
    {
        get;
        set;
    }

    [field: SerializeField]
    public CurrentlySelectedCard CurrentlySelectedCard
    {
        get;
        set;
    }

    [field: SerializeField]
    public HandPresenter HandPresenter
    {
        get;
        set;
    }

    void Update()
    {
        Render();
    }

    void Render()
    {
        attackText.text = "ATK: " + Model.Attack.ToString();
        healthText.text = "HP: " + Model.CurrentHealth.ToString();
        summonCostText.text = "COST: " + Model.SummonCost.ToString();
        nameText.text = Model.Name;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Highlight the card only if it's in the player's hand.
        if (!HandPresenter.Contains(this))
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
        GameViewPresenter.PoseState = GameViewPresenter.PoseState.ObservePlayingField;

        // Maintain the currently selected card.
        CurrentlySelectedCard.Card = this;
        Debug.Log($"Set currently selected card to {CurrentlySelectedCard.Name}");
    }
}
