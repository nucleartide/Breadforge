using UnityEngine;

/// <summary>
/// Creates cards from a Card prefab.
/// </summary>
public class CardFactory : MonoBehaviour
{
    [SerializeField]
    [NotNull]
    CardPresenter cardPrefab;

    [SerializeField]
    [NotNull]
    GameViewPresenter gameViewPresenter;

    public CardPresenter Build(Card card) => Build(card, Vector3.zero, Quaternion.identity);
    public CardPresenter Build(Card card, Vector3 position, Quaternion rotation)
    {
        var cardPresenter = Instantiate(cardPrefab, position, rotation);
        cardPresenter.Card = card;
        if (gameViewPresenter == null)
        {
            throw new System.Exception("it's not working");
		}
        cardPresenter.GameViewPresenter = gameViewPresenter;
        Debug.Log(cardPresenter.GameViewPresenter);
        return cardPresenter;
    }
}
