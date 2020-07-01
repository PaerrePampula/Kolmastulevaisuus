using UnityEngine;

public static class HomeHandler
{
    #region Fields
    static Rent playerRent;
    static RentableHome playerHome;
    [RuntimeInitializeOnLoadMethod(loadType: RuntimeInitializeLoadType.BeforeSceneLoad)] //Ei runaa ilman tätä koska ei monobehaviouria.
    static void Init()
    {

        ResetButton.onReset += UnInit;
    }
    static void UnInit()
    {

        ResetButton.onReset -= UnInit;
    }
    #endregion


}
