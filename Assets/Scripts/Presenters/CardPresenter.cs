using UnityEngine;
using TMPro;

public class CardPresenter : MonoBehaviour
{
    [SerializeField]
    CardStatistics cardStatistics;

    [SerializeField]
    TMP_Text attackText;

    [SerializeField]
    TMP_Text healthText;

    [SerializeField]
    TMP_Text summonCostText;

    void Start()
    {
        attackText.text = "ATK: " + cardStatistics.Attack.ToString();
        healthText.text = "HP: " + cardStatistics.CurrentHealth.ToString();
        summonCostText.text = "COST: " + cardStatistics.SummonCost.ToString();
    }
}
