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
    private void OnEnable()
    {
        DateTimeSystem.OnMonthChange += PayRent;
    }

    private void OnDisable()
    {
        DateTimeSystem.OnMonthChange -= PayRent;
    }
    void PayRent()
    {
        PaerToolBox.changePlayerMoney(-PlayerDataHolder.PlayerRent.getTotal());
    }
    #endregion


}
