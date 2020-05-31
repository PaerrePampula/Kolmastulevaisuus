using UnityEngine;

public class HomeHandler : MonoBehaviour
{
    #region Fields
    Rent playerRent;
    RentableHome playerHome;

    static private HomeHandler _currentHomeHandler;
    static public HomeHandler currentHomeHandler
    {
        get
        {
            if (_currentHomeHandler == null)
            {
                _currentHomeHandler = FindObjectOfType<HomeHandler>();
            }
            return _currentHomeHandler;
        }
    }
    #endregion

    #region Getters
    public Rent getRent()
    {
        return playerRent;
    }
    #endregion
    #region MonobehaviourDefaults
    private void Awake()
    {
        GameEventSystem.Current.RegisterListener(Event_Type.PLAYER_LEASES_HOME, registerHome);
    }
    #endregion
    void registerHome(EventInfo info)
    {
        RentLeaseForm form = (RentLeaseForm)info;
        playerHome = form.rentable;
        Rent rent = new Rent(playerHome.getRentTotalForAMonth());
        playerRent = rent;
    }
}
