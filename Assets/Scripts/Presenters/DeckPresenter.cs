using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeckPresenter : MonoBehaviour, IPointerDownHandler
{
    public Deck Deck;
    public CardPresenter CardPrefab;
    public HandPresenter HandPresenter;

    List<CardPresenter> cardList;
    readonly Quaternion FACE_DOWN = Quaternion.AngleAxis(90, Vector3.right);

    public void OnPointerDown(PointerEventData eventData)
    {
        // [ ] when the deck is clicked,
        Debug.Log("deck was clicked: " + eventData.pointerCurrentRaycast.gameObject.name);

        // [ ] pop the top card from the deck, and
        //

        // [ ] add the top card to the hand
        //
    }

    // TODO: be able to click on the deck collider, and trigger a not-implemented action.
    void Start()
    {
        // [x] iterate over the deck
        cardList = new List<CardPresenter>();
        for (var i = 0; i < Deck.Cards.Count; i++)
        {
            var currentCard = Deck.Cards[i];
            for (var j = 0; j < currentCard.count; j++)
            { 
			    var card = Instantiate(
				    CardPrefab,
				    transform.position + new Vector3(0, .1f * cardList.Count, 0),
                    FACE_DOWN
			    );
                card.cardStatistics = currentCard.card;
                card.id = $"{i}{j}";
			    cardList.Add(card);
		    }
		}

        // [ ] then shuffle the list
        Shuffle(cardList);

        for (var i = 0; i < cardList.Count; i++)
        {
            Debug.Log(cardList[i].id);
		}

        for (var i = 0; i < cardList.Count; i++)
        { 
			// HandPresenter.cards.Add(cardList[i]);
		}
    }

    public static void Shuffle<T>(List<T> cards)
    {
        // Fisher-Yates shuffle:
        /*
                -- To shuffle an array a of n elements (indices 0..n-1):
                for i from n−1 down to 1 do
                     j ← random integer such that 0 ≤ j ≤ i
                     exchange a[j] and a[i]
                */

        for (var i = cards.Count - 1; i >= 1; i--)
        {
            // Compute index to swap places with.
            var j = UnityEngine.Random.Range(0, i + 1);

            // Swap places.
            var temp = cards[j];
            cards[j] = cards[i];
            cards[i] = temp;
        }
    }

    /*
    public CardStatistics Draw()
    {
        var lastCard = InPlayCards[InPlayCards.Count - 1];
        InPlayCards.RemoveAt(InPlayCards.Count - 1);
        return lastCard;
    }

    public void Shuffle()
    {
        throw new System.NotImplementedException();
    }
    */
}
