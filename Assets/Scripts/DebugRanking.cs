using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugRanking : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable()
    {
        Stat.OnStatChange += ChangeText;
    }
    void ChangeText(float value, float valueChange, SimStatType type)
    {
        if (type == SimStatType.Ranking)
        {
            GetComponent<TextMeshProUGUI>().text = "Piilotetut rankingpisteet: " + value.ToString();
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
