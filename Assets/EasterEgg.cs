using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (Input.GetKeyDown(KeyCode.F))
        {
            eggEnable = true;
        }
        if (eggEnable == true && Input.GetMouseButtonDown(0))
        {


            Vector3 pointInScreen = transformCamera.ScreenToWorldPoint(Input.mousePosition);
            GameObject easterEggBox = Instantiate(easterEgg);
            easterEggBox.transform.position = new Vector3(pointInScreen.x, pointInScreen.y, pointInScreen.z);
            easterEggBox.GetComponent<Rigidbody>().AddForce(transformCamera.transform.forward * 500f);



        }
    }
}
