using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreenGenerator : MonoBehaviour
{
    [SerializeField]
    Transform endScreen;


    // Start is called before the first frame update
    void Start()
    {
        EndScreenFadeOut.OnEnd += startEnd;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void startEnd()
    {
        endScreen.gameObject.SetActive(true);
    }
}
