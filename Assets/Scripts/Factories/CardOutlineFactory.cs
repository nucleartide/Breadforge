using UnityEngine;

public class CardOutlineFactory : MonoBehaviour
{
    [SerializeField]
    [NotNull]
    CardOutlinePresenter cardOutlinePrefab;

    [SerializeField]
    [NotNull]
    PlayingFieldPresenter playingFieldPresenter;

    [SerializeField]
    [NotNull]
    CurrentlySelectedCard currentlySelectedCard;

    [SerializeField]
    [NotNull]
    HandPresenter handPresenter;

    [SerializeField]
    [NotNull]
    GameViewPresenter gameViewPresenter;

    public CardOutlinePresenter Build(PlayingFieldSide playingFieldSide) => Build(playingFieldSide, Vector3.zero, Quaternion.identity);
    public CardOutlinePresenter Build(PlayingFieldSide playingFieldSide, Vector3 position, Quaternion rotation)
    {
        var o = Instantiate(cardOutlinePrefab, position, rotation);
        o.PlayingFieldSide = playingFieldSide;
        o.SelectedCard = currentlySelectedCard;
        o.PlayingFieldPresenter = playingFieldPresenter;
        o.HandPresenter = handPresenter;
        o.GameViewPresenter = gameViewPresenter;
        return o;
    }
}
