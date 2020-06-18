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
    float increments = 0;
    float targetvalue;
    float originalValue;
    float timer;
    private delegate float gottenValueMethod();

    public void BarStatChange(float value, float valueChange, SimStatType type)
    {
        if (type == simStatType)
        {
            GameObject go = Instantiate(incText);
            string change = (valueChange > 0) ? valueChange + "+" : valueChange + "-";
            Color color = (valueChange > 0) ? Color.green : Color.red;
            go.GetComponent<TextMeshProUGUI>().text = change;
            go.GetComponent<TextMeshProUGUI>().color = color;
            go.transform.SetParent(transform);
            go.transform.localPosition = incTextOffset;

            startIncrementing(valueChange);
        }



    }
    private void OnEnable()
    {
        Stat.OnStatChange += BarStatChange;
        targetvalue = slider.value;


    }
    private void OnDisable()
    {
        Stat.OnStatChange -= BarStatChange;

    }
    private void Update()
    {
        if (slider.value != targetvalue)
        {
            currentFloat = Mathf.Lerp(originalValue, targetvalue, timer);
            timer += 0.75f * Time.deltaTime;
            slider.value = currentFloat;
            image.color = gradient.Evaluate(slider.normalizedValue);
            if (slider.value == targetvalue)
            {
                originalValue = slider.value;
                timer = 0;
                increments = 0;
            }
        }
    }
    public void startIncrementing(float change)
    {
        increments = change;
        originalValue = slider.value;
        targetvalue += increments;


    }
    //public IEnumerator whiler(float t, float originalvalue)
    //{

    //    while (slider.value != targetvalue)
    //    {
    //        currentFloat = Mathf.Lerp(originalvalue, targetvalue, t);

    //        t += 0.50f * Time.deltaTime;
    //        slider.value = currentFloat;
    //        image.color = gradient.Evaluate(slider.normalizedValue);
    //        yield return null;

    //    }
    //}
}
