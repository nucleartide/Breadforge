using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Deck : ScriptableObject
{
    [System.Serializable]
    public class CardInDeck
    {
        public Card Card;

        /// <summary>
        /// Number of this card in the deck.
        /// </summary>
        public int Count;
    }

    /// <summary>
    /// Cards that are in the deck.
    ///
    /// Does not hold the actual CardPresenters, rather holds the counts of CardPresenters
    /// prior to starting the game.
    /// </summary>
    [field: SerializeField]
    public List<CardInDeck> Cards
    {
        get;
        set;
    }
}
