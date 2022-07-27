using UnityEngine;

/// <summary>
/// TODO: Display this card.
/// </summary>
[CreateAssetMenu]
public class CardStatistics : ScriptableObject
{
    [SerializeField]
    [Tooltip("Also known as ATK. Displayed on the lower-left of the card.")]
    int attack;

    [SerializeField]
    [Tooltip("Also known as HP. Displayed on the lower-right of the card.")]
    int health;

    /// <summary>
    /// The current health of the card when in play.
    /// </summary>
    int currentHealth;

    /// <summary>
    /// The upper-right portion of the card. Indicates the number of sacrifices needed to play the card.
    /// </summary>
    int summonCost;

    // POST-MVP TODO:
    //
    // Bones – Conversely, there are cards that cost bones. You earn bones
    // whenever your cards get destroyed by an opponent. This mana/resource
    // will be introduced as a mechanic after your first loss or “death”
    // against Leshy.

    void OnEnable()
    {
        currentHealth = health;
    }
}

public class Card : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
