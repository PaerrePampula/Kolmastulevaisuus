using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankingSystem : MonoBehaviour
{
    void deliberateNewRankChangeBasedOnWeek()
    {
        float boostBasedOnStats = PlayerDataHolder.Current.Satisfaction.StatFloat +
            PlayerDataHolder.Current.Comfortableness.StatFloat;
        boostBasedOnStats *= 0.25f; //Statseista saa bonusrankkia
        float boostBasedOnEconomy = PlayerDataHolder.Current.PlayerMoney.getValue<float>();
        boostBasedOnEconomy *= 0.20f; //Rahasta saa myös boostia
        changeRank(boostBasedOnStats + boostBasedOnEconomy);
        changeStatRanks();
    }
    private void OnEnable()
    {
        LocationHandler.OnTurnEnd += deliberateNewRankChangeBasedOnWeek;
    }
    private void OnDisable()
    {
        LocationHandler.OnTurnEnd -= deliberateNewRankChangeBasedOnWeek;
    }

    void changeRank(float rankingChange)
    {
        PlayerDataHolder.Current.GeneralRanking.ChangeStat(rankingChange);
    }
    void changeStatRanks()
    {
        float studyBoost = PlayerDataHolder.Current.Study.StatFloat / 20.5f;
        PlayerDataHolder.Current.StudyRanking.ChangeStat(studyBoost);
        float socialBoost = PlayerDataHolder.Current.SocialRanking.StatFloat / 20.5f;
        PlayerDataHolder.Current.SocialRanking.ChangeStat(socialBoost);
        float homeBoost = PlayerDataHolder.Current.HomeRanking.StatFloat / 20.5f;
        PlayerDataHolder.Current.HomeRanking.ChangeStat(homeBoost);
    }
}
