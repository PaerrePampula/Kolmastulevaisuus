using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DateTimeSystem : MonoBehaviour
{
    #region Fields
    static System.DateTime currentDate;
    static System.DateTime lastDate;
    public TextMeshProUGUI _thisdaytext;
    public delegate void MonthChange();
    public static event MonthChange OnMonthChange;
    #endregion
    #region MonobehaviourDefaults
    void Start()
    {

        currentDate = new System.DateTime(2020, 5, 1);
        SetDate();
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.A))
        {
            ChangeWeek();
        }
    }
    #endregion
    public static System.DateTime getCurrentDate()
    {
        return currentDate;
    }
    public void SetDate()
    {
        _thisdaytext.text = string.Format("{1}.{2}.{3}", currentDate.DayOfWeek.ToString(), currentDate.Day, currentDate.Month, currentDate.Year);
    }

    public void ChangeWeek()
    {
        lastDate = currentDate;
        currentDate = currentDate.AddDays(7);
        SetDate();
        if (hasTheMonthChangedSinceLastTurn() == true)
        {
            OnMonthChange?.Invoke();
        }
        Debug.Log(currentDate.DayOfWeek.ToString());

    }
    public bool hasTheMonthChangedSinceLastTurn() //Käyttötarkoituksia mm. kelan kuukausittaisten tukien laskuun.
    {
        return (lastDate.Month != currentDate.Month) ? true : false;
    }
}
