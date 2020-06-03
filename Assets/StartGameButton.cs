using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameButton : MonoBehaviour
{
    RentableHome rentableHome;
    public void BeginGame()
    {
        RegisterPlayerHome();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);


    }
    void RegisterPlayerHome()
    {
        PlayerDataHolder.playerHome = rentableHome;

    }
    public void setRentable(RentableHome InforentableHome)
    {
        rentableHome = InforentableHome;
    }
}
