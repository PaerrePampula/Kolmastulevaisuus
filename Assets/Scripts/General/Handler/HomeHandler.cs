using UnityEngine;

public class HomeHandler : MonoBehaviour
{
    #region Fields
    Rent playerRent;

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
    private void Start()
    {
        //Tämä on vain default debug vuokra, ei jää tähän.
        Rent rent = new Rent(550,0,0);
        registerNewRent(rent);
        
    }
    #endregion
    void registerNewRent(Rent rent)
    {
        playerRent = rent;
    }
}
