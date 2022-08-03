using UnityEngine;
using TMPro;

public class CardPresenter : MonoBehaviour
{
    [field: SerializeField]
    public Card Card
    {
        private get;
        set;
    }

    public string Identifier = System.Guid.NewGuid().ToString();

    [SerializeField]
    TMP_Text attackText;

    [SerializeField]
    TMP_Text healthText;

    [SerializeField]
    TMP_Text summonCostText;

    [SerializeField]
    TMP_Text nameText;

    void Update()
    {
        attackText.text = "ATK: " + Card.Attack.ToString();
        healthText.text = "HP: " + Card.CurrentHealth.ToString();
        summonCostText.text = "COST: " + Card.SummonCost.ToString();
        nameText.text = Card.Name;
    }
}
