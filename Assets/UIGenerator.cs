using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGenerator : MonoBehaviour
{
    public string resourceToLoad;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void instantiateUIObject()
    {
        GameObject go = Instantiate(Resources.Load <GameObject> (resourceToLoad));
        go.transform.SetParent(MainCanvas.mainCanvas.transform);
        go.transform.localPosition = Vector3.zero;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
