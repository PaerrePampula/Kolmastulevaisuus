using UnityEngine;
using System.Collections;
using TMPro;

public class FloatNumberHelper : MonoBehaviour
{
    public static void createFloatingNumbers(GameObject incText, float valueChange, Transform transformParent, bool randomizedOffSetEffect, Vector3 incTextOffset)
    {
        GameObject go = Instantiate(incText);
        string change = (valueChange > 0) ? valueChange + "+" : valueChange + "-";
        Color color = (valueChange > 0) ? Color.green : Color.red;
        go.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = change;
        go.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = color;
        go.transform.SetParent(transformParent);
        go.transform.localPosition = (randomizedOffSetEffect) ? incTextOffset + new Vector3(Random.Range(-15, 15), Random.Range(-15, 15)) : incTextOffset;

    }
}
