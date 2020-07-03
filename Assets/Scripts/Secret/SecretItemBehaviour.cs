using UnityEngine;
using System.Collections;

public class SecretItemBehaviour : MonoBehaviour
{
    [SerializeField]
    Transform hand;
    private void OnEnable()
    {
        SecretPickUp.onItemEquip += equipItem;
    }
    private void OnDisable()
    {
        SecretPickUp.onItemEquip -= equipItem;
    }
    void equipItem(GameObject item)
    {
        item.transform.SetParent(hand);
        item.transform.localPosition = Vector3.zero;
        item.transform.localRotation = Quaternion.identity;
        item.GetComponent<SecretTool>().secretAvailable = true;
    }
}
