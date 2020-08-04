using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;
using System;

public class EndDataManager : MonoBehaviour
{
    
    public TextMeshProUGUI dataTextContainer;

    public TextMeshProUGUI nonAbstractDescriptor;
    public TextMeshProUGUI playerTitle;
    public TextMeshProUGUI palyerTitleDescription;
    public EndDescriptorScriptable descriptors;


    List<SortedRankingTitle> rankingTitles;

    float receivedMoney = 0;
    float lostMoney = 0;
    int busts = 0;
    int timesofHunger = 0;
    (string, float, int) jobAndPayPlusHours;
    private void OnEnable()
    {
        rankingTitles = new List<SortedRankingTitle>();
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
    void findDataForNonAbstractDescriptors()
    {

        for (int i = 0; i < 3; i++)
        {
            Stat comparedStat = PlayerDataHolder.Current.Social;
            switch (i)
            {
                case 0:

                    break;
                case 1:
                    comparedStat = PlayerDataHolder.Current.Study;
                    break;
                case 2:
                    comparedStat = PlayerDataHolder.Current.Comfortableness;
                    break;
            }
            var listing = descriptors.rankingDescriptors.FirstOrDefault(givenListing => (givenListing.statToRank == comparedStat.SimStatType) && (PaerToolBox.isBetweenOrAsBig(comparedStat.StatFloat, givenListing.minrange, givenListing.maxrange, true)));

            nonAbstractDescriptor.text += listing.text + "\n\n";
            chooseTitle();

        }
    }
    void chooseTitle()
    {
        for (int i = 0; i < descriptors.rankingTitles.Length; i++)
        {
            SortedRankingTitle sortedRankingTitle = new SortedRankingTitle(descriptors.rankingTitles[i]);
            if (sortedRankingTitle.canBeApplied)
            {
                rankingTitles.Add(sortedRankingTitle);
            }

        }
        var maximumValue = rankingTitles.Max(given => given.points);
        var maximumObject = rankingTitles.FirstOrDefault(maxObject => maxObject.points == maximumValue);
        playerTitle.text = maximumObject.rankingTitles.TitleName;
        palyerTitleDescription.text = maximumObject.rankingTitles.ReasonDescriptor;
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
        jobAndPayPlusHours = (jobNotice.scriptable.jobTitle, jobNotice.scriptable.payByHour, jobNotice.scriptable.minimumHoursPerWeek);
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
                "euroa rahaa, tunteja viikossa sinulla oli vähintään " + jobAndPayPlusHours.Item3 + "\n";
        }
    }
    string getStats()
    {
        return "Sinun hyvinvointi oli " + PlayerDataHolder.Current.Satisfaction.StatFloat + "/100\n" +
            "Sinun mukavuutesi oli " + PlayerDataHolder.Current.Comfortableness.StatFloat + "/100\n" +
            "Sinun sosiaalisuutesi oli " + PlayerDataHolder.Current.Social.StatFloat + "/100\n" +
            "Sinun opiskelutaitosi oli " + PlayerDataHolder.Current.Study.StatFloat + "/100\n" +
            "Pelipisteitä sait yhteensä " + Mathf.Round(PlayerDataHolder.Current.GeneralRanking.StatFloat) + "/5000\n";
    }
    void addDataToDescriptor()
    {
        findDataForNonAbstractDescriptors();
        dataTextContainer.text = "Sait yhteensä rahaa: " + receivedMoney + " euroa\n";
        dataTextContainer.text += "Käytit rahaa/ maksoit laskuja:" + lostMoney +" euroa\n";
        dataTextContainer.text += getJobData();
        dataTextContainer.text += getStats();
        dataTextContainer.text += "Näit nälkää " + timesofHunger + " kertaa";

    }
}
