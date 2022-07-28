using UnityEngine;

[CreateAssetMenu]
public class CardStatistics : ScriptableObject
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

    // POST-MVP TODO:
    //
    // Bones – Conversely, there are cards that cost bones. You earn bones
    // whenever your cards get destroyed by an opponent. This mana/resource
    // will be introduced as a mechanic after your first loss or “death”
    // against Leshy.

    void OnEnable()
    {
        CurrentHealth = Health;
    }
}

