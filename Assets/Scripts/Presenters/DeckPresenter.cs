using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeckPresenter : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]
    [NotNull]
    Deck deck;

    [SerializeField]
    [NotNull]
    CardPresenter cardPrefab;

    [SerializeField]
    [NotNull]
    HandPresenter handPresenter;

    [SerializeField]
    [NotNull]
    CardFactory cardFactory;

    readonly List<CardPresenter> cards = new List<CardPresenter>();

    public void OnPointerDown(PointerEventData eventData)
    {
        // Pop the top card from the deck.
        var lastCard = cards[cards.Count - 1];
        cards.RemoveAt(cards.Count - 1);

        // Add the top card to the hand.
        handPresenter.Add(lastCard);
    }

    void InitializeCards(Deck deck, CardPresenter cardPrefab)
    {
        for (var i = 0; i < deck.Cards.Count; i++)
        {
            var currentCard = deck.Cards[i];
            for (var j = 0; j < currentCard.Count; j++)
            {
                var card = cardFactory.Build(currentCard.Card);
                cards.Add(card);
            }
        }

        Shuffle(cards);
    }

    void Start()
    {
        InitializeCards(deck, cardPrefab);
        PositionCards();
    }

    void PositionCards()
    {
        for (var i = 0; i < cards.Count; i++)
        {
            // Position and orient the cards so that they appear as a deck of cards.
            cards[i].transform.position = transform.position + new Vector3(0, .01f * i, 0);
            cards[i].transform.rotation = Orientation.FACE_DOWN;
        }
    }

    /// <summary>
    /// Implements the Fisher-Yates shuffle algorithm.
    /// </summary>
    public static void Shuffle<T>(List<T> cards)
    {
        for (var i = cards.Count - 1; i >= 1; i--)
        {
            // Compute index to swap places with.
            var j = Random.Range(0, i + 1);

            // Swap places.
            var temp = cards[j];
            cards[j] = cards[i];
            cards[i] = temp;
        }
    }
}
