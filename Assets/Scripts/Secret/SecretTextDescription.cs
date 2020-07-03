using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SecretTextDescription : MonoBehaviour
{
    [SerializeField]
    SecretTool sourceofSecret;
    [SerializeField]
    Gradient secretGradient;
    TextMeshPro textoutput;
    // Start is called before the first frame update
    private void OnEnable()
    {
        textoutput = GetComponent<TextMeshPro>();
        sourceofSecret.onNewSecret += updateSecret;
    }
    private void OnDisable()
    {
        sourceofSecret.onNewSecret -= updateSecret;
    }
    void updateSecret(int amount)
    {
        textoutput.text = amount.ToString();
        textoutput.color = secretGradient.Evaluate(amount/5f);
    }

}
