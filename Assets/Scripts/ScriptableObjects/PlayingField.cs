using UnityEngine;

public class PlayingField : ScriptableObject
{
    public CardPresenter[] OpponentPreviewRow = new CardPresenter[4];
    public CardPresenter[] OpponentRow = new CardPresenter[4];
    public CardPresenter[] PlayerRow = new CardPresenter[4];
}
