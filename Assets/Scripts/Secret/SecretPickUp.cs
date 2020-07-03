using UnityEngine;
using System.Collections;

public class SecretPickUp : MonoBehaviour
{
    [SerializeField]
    GameObject pickUp;
    public delegate void itemEquip(GameObject item);
    public static event itemEquip onItemEquip;
    // Use this for initialization
    void Start()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        GameObject newPickUp = Instantiate(pickUp);
        Destroy(gameObject);
        onItemEquip?.Invoke(newPickUp);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
