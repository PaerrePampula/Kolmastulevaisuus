using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugRanking : MonoBehaviour
{

    private void OnEnable()
    {
        Stat.OnStatChange += ChangeText;
    }
    private void OnDisable()
    {
        Stat.OnStatChange -= ChangeText;
    }
    void ChangeText(float value, float valueChange, SimStatType type)
    {
        if (type == SimStatType.Ranking)
        {
            GetComponent<TextMeshProUGUI>().text = "Piilotetut rankingpisteet: " + value.ToString();
        }

    }


}
