using UnityEngine;
using System.Collections;
using TMPro;

public class GenericObjectHolder : MonoBehaviour
{
    public IButtonInteractableItem item;
    [SerializeField]
    TextMeshProUGUI textBox;
    public void onClick()
    {
        item.InteractWithItem();
        updateInteractableText();
    }
    public void updateInteractableText()
    {
        textBox.text = item.ToString();
    }
    private void Start()
    {
        updateInteractableText();
    }

}
