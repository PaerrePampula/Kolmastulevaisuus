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
        TextMeshPro textComp = prefabClone.GetComponent<TextMeshPro>();
        textComp.text = text;
        prefabClone.transform.position = objectTransform.position + Vector3.up*0.5f;
        prefabClone.transform.rotation = Quaternion.identity;

    }

    private void LateUpdate()
    {
        if (isHoveringOverHoverable)
        {
            prefabClone.transform.LookAt(prefabClone.transform.position + camera.transform.rotation * Vector3.forward, camera.transform.rotation * Vector3.up);
        }

    }
    private void Start()
    {
        
        camera = Camera.main;
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
