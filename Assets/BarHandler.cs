using System.Collections;
using System.Collections.Generic;
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

    public void BarStatChange(float value)
    {
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
