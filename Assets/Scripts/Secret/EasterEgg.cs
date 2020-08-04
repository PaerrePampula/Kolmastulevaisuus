using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EasterEgg : MonoBehaviour
{
    bool eggEnable = false;
    [SerializeField]
    GameObject easterEgg;
    [SerializeField]
    Camera transformCamera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (eggEnable == true && Input.GetMouseButtonDown(0))
        {

            Vector3 mousePos = Input.mousePosition;
            mousePos.z = transformCamera.nearClipPlane;
            Vector3 worldPosition = transformCamera.ScreenToWorldPoint(mousePos);

            GameObject easterEggBox = Instantiate(easterEgg);
            easterEggBox.transform.position = new Vector3(worldPosition.x, worldPosition.y, worldPosition.z);
            easterEggBox.GetComponent<Rigidbody>().AddForce(transformCamera.transform.forward * 500f);



        }

    }
}
