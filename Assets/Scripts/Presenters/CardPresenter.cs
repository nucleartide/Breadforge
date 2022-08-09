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

    void Update()
    {
        attackText.text = "ATK: " + Card.Attack.ToString();
        healthText.text = "HP: " + Card.CurrentHealth.ToString();
        summonCostText.text = "COST: " + Card.SummonCost.ToString();
        nameText.text = Card.Name;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
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
        if (GameViewPresenter == null)
        { 
            throw new System.Exception("it's not working");
		}
        GameViewPresenter.CurrentPose = GameViewPresenter.PoseState.ObservePlayingField;
        // [ ] maintain that this card was clicked (store in a piece of state)
    }
}
