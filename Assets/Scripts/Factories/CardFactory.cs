using UnityEngine;

/// <summary>
/// Creates cards from a Card prefab.
/// </summary>
public class CardFactory : MonoBehaviour
{
    [SerializeField]
    CardPresenter cardPrefab;

    public CardPresenter Build(Card card)
    {
        var cardPresenter = Instantiate(cardPrefab);
        cardPresenter.Card = card;
        return cardPresenter;
    }

    public CardPresenter Build(Card card, Vector3 position, Quaternion rotation)
    {
        var cardPresenter = Instantiate(cardPrefab, position, rotation);
        cardPresenter.Card = card;
        return cardPresenter;
    }
}
