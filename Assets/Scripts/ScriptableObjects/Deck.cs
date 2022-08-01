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
    /// </summary>
    [field: SerializeField]
    public List<CardInDeck> Cards
    {
        get;
        set;
    }
}
