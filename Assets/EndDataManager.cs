using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndDataManager : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI dataTextContainer;
    float receivedMoney = 0;
    float lostMoney = 0;
    int busts = 0;
    int timesofHunger = 0;
    (string, float, int) jobAndPayPlusHours;
    private void OnEnable()
    {
        PlayerEconomy.OnChangeFetch += addMoneyData;
        JobHandler.OnJobApply += addJobData;
        GameStateHandler.OnGameEnd += addDataToDescriptor;
        Flag.OnFlagFire += addTimesOfHunger;
    }
    private void OnDisable()
    {
        PlayerEconomy.OnChangeFetch -= addMoneyData;
        JobHandler.OnJobApply -= addJobData;
        GameStateHandler.OnGameEnd -= addDataToDescriptor;
        Flag.OnFlagFire -= addTimesOfHunger;

    }
    void addMoneyData(float amount)
    {
        switch (amount >= 0)
        {
            case true:
                receivedMoney += amount;
                break;
            case false:
                lostMoney += Mathf.Abs(amount);
                break;
        }
    }
    void addJobData(JobNotice jobNotice)
    {
        jobAndPayPlusHours = (jobNotice.scriptable.jobTitle, jobNotice.scriptable.payByHour, jobNotice.scriptable.workHoursPerDay * 3);
    }
    void addTimesOfHunger(Flag flag)
    {
        if (flag.FlagName == "INTENSE_HUNGER")
        {
            timesofHunger++;
        }
    }
    string getJobData()
    {
        if (jobAndPayPlusHours.Item1 == null)
        {
            return "Sinulla ei ollut työpaikkaa\n";
        }
        else
        {
            return "Sinun työpaikkasi oli " + jobAndPayPlusHours.Item1 + ", teit siellä tunnissa " + jobAndPayPlusHours.Item2 +
                "euroa rahaa, tunteja viikossa sinulla oli " + jobAndPayPlusHours.Item3 + "\n";
        }
    }
    string getStats()
    {
        return "Sinun hyvinvointi oli " + PlayerDataHolder.Current.Satisfaction.StatFloat + "/100\n" +
            "Sinun mukavuutesi oli " + PlayerDataHolder.Current.Comfortableness.StatFloat + "/100\n" +
            "Sinun sosiaalisuutesi oli " + PlayerDataHolder.Current.Social.StatFloat + "/100\n" +
            "Sinun opiskelutaitosi oli " + PlayerDataHolder.Current.Study.StatFloat + "/100\n" +
            "Sait pisteitä yhteensä " + Mathf.Round(PlayerDataHolder.Current.Ranking.StatFloat) + "/5000\n";
    }
    void addDataToDescriptor()
    {
        dataTextContainer.text = "Sait yhteensä rahaa: " + receivedMoney + " euroa\n";
        dataTextContainer.text += "Käytit rahaa/ maksoit laskuja:" + lostMoney +" euroa\n";
        dataTextContainer.text += getJobData();
        dataTextContainer.text += getStats();
        dataTextContainer.text += "Näit nälkää " + timesofHunger + " kertaa";


    }
}
