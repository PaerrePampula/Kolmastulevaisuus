using TMPro;
using UnityEngine;

public class DateTimeSystem : MonoBehaviour
{
    #region Fields
    static System.DateTime currentDate;
    static System.DateTime lastDate;
    [SerializeField]
    TextMeshProUGUI _thisdaytext;
    int weekIndicator = 1;
    [SerializeField]
    GameObject weekIndicatorObject;
    public delegate void MonthChange();
    public static event MonthChange OnMonthChange;

    #endregion
    #region MonobehaviourDefaults
    void Start()
    {

        currentDate = new System.DateTime(2020, 1, 9);
        SetDate();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ChangeWeek();
        }
    }

    private void OnEnable() //Kun tämä skripti aktivoituu, se automaattisesti tilaa Kukkaroskriptin OnIncrease tapahtuman
    {
        LocationHandler.OnTurnEnd += ChangeWeek;
    }

    private void OnDisable()
    {
        LocationHandler.OnTurnEnd -= ChangeWeek; //Jos tämä skripti poistuu, se ottaa sen tilauksen ensin pois. Miksi? Koska muuten tulisi null reference exceptioneita, jos tilaus on olemassa, mutta ei vastaanottajaa...
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

    void changeWeekIndicator()
    {
        weekIndicator++;
        GameObject go = Instantiate(weekIndicatorObject);
        go.transform.SetParent(MainCanvas.mainCanvas.transform);
        go.transform.localPosition = Vector3.zero;
        go.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = string.Format("Viikko" + weekIndicator);
    }
    public void ChangeWeek()
    {
        lastDate = currentDate;
        currentDate = currentDate.AddDays(7);
        SetDate();
        changeWeekIndicator();
        if (hasTheMonthChangedSinceLastTurn() == true)
        {
            OnMonthChange?.Invoke();
        }
        


    }
    public bool hasTheMonthChangedSinceLastTurn() //Käyttötarkoituksia mm. kelan kuukausittaisten tukien laskuun.
    {
        return (lastDate.Month != currentDate.Month) ? true : false;
    }
    public static string returnDayYearMonth(System.DateTime time)
    {
        return string.Format("{0}.{1}.{2}", time.Day, time.Month, time.Year);
    }
}
