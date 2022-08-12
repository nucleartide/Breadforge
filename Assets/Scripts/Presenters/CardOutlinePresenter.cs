using UnityEngine;
using UnityEngine.EventSystems;

public class CardOutlinePresenter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
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

    public enum PlayerSide
    { 
        Player,
        Enemy,
    }

    public PlayerSide PlayingSide
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (CurrentlySelectedCard.Card != null && PlayingSide == PlayerSide.Player)
			glow.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (CurrentlySelectedCard.Card != null && PlayingSide == PlayerSide.Player)
			glow.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("on pointer click");

	    // [ ] clicking an empty spot will clear the currently selected card,
        var card = CurrentlySelectedCard.Card;
        if (card == null)
        {
            Debug.Log("no card is selected. returning");
		}
        CurrentlySelectedCard.Card = null;

        // [x] pass in hand dependency

        // remove from hand,
        var wasRemoved = HandPresenter.Cards.Remove(card);
        if (!wasRemoved)
            Debug.Log("card wasn't removed, something is wrong");
        else
            Debug.Log("card was removed");

        // pass in playing field dependency
        if (PlayingFieldPresenter == null)
            throw new System.Exception("playing field is null");

        // and set on the playing field
        PlayingFieldPresenter.SetCard(this, card);

        // the playing field should update the positions
        // ...

        // also, unset the glow
        glow.SetActive(false);

        // finally, switch back the game view
        GameViewPresenter.CurrentPose = GameViewPresenter.PoseState.Neutral;
    }
}
