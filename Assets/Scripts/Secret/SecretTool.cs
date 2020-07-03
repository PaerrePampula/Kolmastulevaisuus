using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretTool : MonoBehaviour
{
    [SerializeField]
    Transform secretPoint;
    [SerializeField]
    public GameObject secretInstantiatedObject;
    [SerializeField]
    float secretInstaEffect;
    [SerializeField]
    int maxSecrets = 5;
    [SerializeField]
    int currentSecrets;
    bool makingSecrets;
    public bool secretAvailable = false;
    public delegate void SecretMake(int newsecret);
    public event SecretMake onNewSecret;

    // Start is called before the first frame update
    void Start()
    {
        currentSecrets = maxSecrets;
        onNewSecret?.Invoke(currentSecrets);
    }

    // Update is called once per frame
    void Update()
    {
        if (secretAvailable)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                deliberateSecret();
            }
        }

    }
    void createSecret()
    {
        currentSecrets--;
        onNewSecret?.Invoke(currentSecrets);
        GameObject secretPrefab = Instantiate(secretInstantiatedObject, secretPoint.position, secretPoint.rotation);
        secretPrefab.GetComponent<Rigidbody>().AddForce(secretPrefab.transform.TransformDirection(Vector3.forward*1750));
    }
    void deliberateSecret()
    {
        switch (currentSecrets > 0 && !makingSecrets)
        {
            case true:
                createSecret();
                break;
            case false:
                StartCoroutine(makeMoreSecrets());
                break;
        }
    }
    IEnumerator makeMoreSecrets()
    {
        yield return new WaitForSeconds(0.5f);
        currentSecrets = maxSecrets;
        onNewSecret?.Invoke(currentSecrets);
    }
}
