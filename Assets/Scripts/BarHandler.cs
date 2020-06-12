using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BarHandler : MonoBehaviour
{
    [SerializeField]
    Slider slider;
    [SerializeField]
    Image image;
    [SerializeField]
    Gradient gradient;
    [SerializeField]
    GameObject incText;
    [SerializeField]
    Vector3 incTextOffset;

    public void BarStatChange(float value, float valueChange)
    {
        GameObject go = Instantiate(incText);
        string change = (valueChange > 0) ? valueChange + "+" : valueChange + "-";
        Color color = (valueChange > 0) ? Color.green : Color.red;
        go.GetComponent<TextMeshProUGUI>().text = change;
        go.GetComponent<TextMeshProUGUI>().color = color;
        go.transform.SetParent(transform);
        go.transform.localPosition = incTextOffset;
        slider.value = value;
        image.color = gradient.Evaluate(slider.normalizedValue);

    }
    private void OnEnable()
    {
        Satisfaction.OnSatisfactionChange += BarStatChange;
    }
    private void OnDisable()
    {
        Satisfaction.OnSatisfactionChange -= BarStatChange;
    }
}
