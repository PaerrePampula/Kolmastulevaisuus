using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameButton : MonoBehaviour
{
    RentableHome rentableHome;
    public void BeginGame()
    {
        //RegisterPlayerHome();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
    void RegisterPlayerHome()
    {
        RentLeaseForm form = new RentLeaseForm();
        form.rentable = rentableHome;
        GameEventSystem.Current.DoEvent(
            Event_Type.PLAYER_LEASES_HOME,
            form
            );
    }
}
