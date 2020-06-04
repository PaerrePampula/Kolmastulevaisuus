using UnityEngine;
using System.Collections;
using TMPro;

public class HoverText : MonoBehaviour
{
    bool isHoveringOverHoverable = false;
    [SerializeField]
    GameObject hoverObjectPrefab;
    GameObject prefabClone;
    Camera camera;

    void setHover(bool hoverstate, string text = "", Transform objectTransform = null)
    {
        isHoveringOverHoverable = hoverstate;
        if (isHoveringOverHoverable == false)
        {
            Destroy(prefabClone);
            return;
        }

        prefabClone = Instantiate(hoverObjectPrefab);
        TextMeshProUGUI textComp = prefabClone.GetComponent<TextMeshProUGUI>();
        textComp.text = text;

        prefabClone.transform.position = camera.WorldToScreenPoint(objectTransform.position);
        prefabClone.transform.rotation = Quaternion.identity;
        prefabClone.transform.SetParent(transform);

    }
    private void Start()
    {
        
        camera = Camera.main; //Camera.main on melko kallis operaatio, haetaan se heti niin ei tarvii tehdä tätä aina uudestaan.
    }
    private void OnEnable()
    {
        WorldInteractive.OnHover += setHover;
    }
    private void OnDisable()
    {
        WorldInteractive.OnHover -= setHover;
    }
}
