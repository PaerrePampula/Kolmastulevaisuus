using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BarHandler : MonoBehaviour
{
    [SerializeField]
    bool randomizedOffSetEffect;
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
            FloatNumberHelper.createFloatingNumbers(incText, valueChange, transform, randomizedOffSetEffect, incTextOffset);
            startIncrementing(value);
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
 
        originalValue = slider.value;
        targetvalue = change;


    }

}
