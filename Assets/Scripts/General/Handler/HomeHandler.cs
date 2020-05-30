using UnityEngine;

public class HomeHandler : MonoBehaviour
{
    #region Fields
    Rent rent;

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
        return rent;
    }
    #endregion
    #region MonobehaviourDefaults
    private void Start()
    {
        rent = new Rent(550,0,0);
        
    }
    #endregion
}
