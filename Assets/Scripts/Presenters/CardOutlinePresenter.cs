using UnityEngine;
using UnityEngine.EventSystems;

public class CardOutlinePresenter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField]
    [NotNull]
    [Tooltip("When enabled, makes it appear as if the card outline has a glow.")]
    GameObject glow;

    [field: SerializeField]
    public CurrentlySelectedCard SelectedCard
    {
        get;
        set;
    }

    public enum Side
    {
        Player,
        Enemy,
    }

    public Side PlayerSide
    {
        get;
        set;
    }

    public HandPresenter HandPresenter
    {
        get;
        set;
    }

    public PlayingFieldPresenter PlayingFieldPresenter
    {
        get;
        set;
    }

    public GameViewPresenter GameViewPresenter
    {
        get;
        set;
    }

    /// <summary>
    /// Provide some feedback when hovering over the card.
    /// </summary>
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (SelectedCard.IsPresent && PlayerSide == Side.Player)
            glow.SetActive(true);
    }

    /// <summary>
    /// Provide some feedback when hovering over the card.
    /// </summary>
    public void OnPointerExit(PointerEventData eventData)
    {
        if (SelectedCard.IsPresent && PlayerSide == Side.Player)
            glow.SetActive(false);
    }

    /// <summary>
    /// Handle clicks on a card outline on the playing field.
    /// </summary>
    public void OnPointerClick(PointerEventData eventData)
    {
        // Grab a reference to the card.
        var card = SelectedCard.Card;
        if (card == null)
        {
            Debug.Log("There is no selected card to place. Returning...");
            return;
        }

        // Clear the selected card.
        SelectedCard.Clear();

        // Remove the card from the player's hand.
        var wasRemoved = HandPresenter.Remove(card);
        if (!wasRemoved)
            Debug.Log("Card wasn't removed, something is wrong.");
        else
            Debug.Log("Card was removed.");

        // Set the card on the playing field.
        PlayingFieldPresenter.SetCard(card, this);

        // Unset the glow.
        glow.SetActive(false);

        // Finally, switch back to the neutral game view.
        GameViewPresenter.CurrentPose = GameViewPresenter.PoseState.Neutral;
    }
}
