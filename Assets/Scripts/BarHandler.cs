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
    [SerializeField]
    SimStatType simStatType;
    float currentFloat;

    private delegate float gottenValueMethod();

    public void BarStatChange(float value, float valueChange)
    {
        GameObject go = Instantiate(incText);
        string change = (valueChange > 0) ? valueChange + "+" : valueChange + "-";
        Color color = (valueChange > 0) ? Color.green : Color.red;
        go.GetComponent<TextMeshProUGUI>().text = change;
        go.GetComponent<TextMeshProUGUI>().color = color;
        go.transform.SetParent(transform);
        go.transform.localPosition = incTextOffset;
        StartCoroutine(startIncrementing(valueChange));


    }
    private void OnEnable()
    {

        switch (simStatType)
        {
            case SimStatType.Satisfaction:
                Satisfaction.OnSatisfactionChange += BarStatChange;
                break;
            default:
                break;
        }

    }
    private void OnDisable()
    {
        switch (simStatType)
        {
            case SimStatType.Satisfaction:
                Satisfaction.OnSatisfactionChange -= BarStatChange;
                break;
            default:
                break;
        }
    }
    public IEnumerator startIncrementing(float change)
    {

        float t = 0.0f;
        float originalvalue = slider.value;
        float newvalue = slider.value + change;
        while (slider.value != originalvalue + change)
        {
            currentFloat = Mathf.Lerp(originalvalue, newvalue, t);
            t += 0.50f * Time.deltaTime;
            slider.value = currentFloat;
            image.color = gradient.Evaluate(slider.normalizedValue);
            yield return null;

        }

    }
}
