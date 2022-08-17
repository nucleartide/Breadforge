using UnityEngine;

[CreateAssetMenu]
public class PlayingField : ScriptableObject
{
    [System.NonSerialized]
    public CardPresenter[] OpponentPreviewRow = new CardPresenter[4];

    [System.NonSerialized]
    public CardPresenter[] OpponentRow = new CardPresenter[4];

    [System.NonSerialized]
    public CardPresenter[] PlayerRow = new CardPresenter[4];
}
