using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuDataShuttle : MonoBehaviour
{
    public RentableHome shuttledHome;
    // Use this for initialization

    void Start()
    {

        PlayerDataHolder.playerHome = shuttledHome;

    }


}
