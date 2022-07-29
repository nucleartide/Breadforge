using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if false

* [ ] Deck
    * [ ] Initial set of cards should be 5 charmanders (char chars) to start
    * [ ] Collider that you can click on, containing a bunch of card objects
    * [ ] When clicked on, an action occurs that transfers the top-most card object to your hand
* [ ] Hand
    * [ ] The physical representation is a GameObject that is a child of the game camera
    * [ ] Adding a card to the hand amounts to:
        * [ ] Parenting the card to this Hand GameObject
        * [ ] Order should be handled by a CardPresenter

#endif

[CreateAssetMenu]
public class Deck : ScriptableObject
{
    [System.Serializable]
    public class CardInDeck
    {
        public CardStatistics card;

        /// <summary>
        /// Number of this card in the deck.
        /// </summary>
        public int count;
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
