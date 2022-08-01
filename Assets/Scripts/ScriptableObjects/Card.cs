using UnityEngine;

[CreateAssetMenu]
public class Card : ScriptableObject
{
    /// <summary>
    /// Also known as ATK. Displayed on the lower-left of the card
    /// </summary>
    [field: SerializeField]
    public int Attack
    {
        get;
        set;
    }

    /// <summary>
    /// Also known as HP. Displayed on the lower-right of the card
    ///
    /// Note: you should use .CurrentHealth to access an in-play card's health.
    /// </summary>
    [SerializeField]
    int Health;

    /// <summary>
    /// The current health of the card when in play.
    /// </summary>
    public int CurrentHealth
    {
        get;
        set;
    }

    /// <summary>
    /// The upper-right portion of the card. Indicates the number of sacrifices needed to play the card.
    /// </summary>
    [field: SerializeField]
    public int SummonCost
    {
        get;
        set;
    }

    [field: SerializeField]
    public string Name
    {
        get;
        set;
    }

    void OnEnable()
    {
        CurrentHealth = Health;
    }
}
