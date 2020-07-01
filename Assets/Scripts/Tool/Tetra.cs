using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetra : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LightProbes.TetrahedralizeAsync();
        //Lightprobet ei toimi oikein ilman tätä uusissa scenekappaleissa main scenen kanssa.
    }

}
