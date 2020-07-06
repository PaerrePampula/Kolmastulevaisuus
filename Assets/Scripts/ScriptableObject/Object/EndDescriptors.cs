using UnityEngine;
using System.Collections;
[CreateAssetMenu(fileName = "EndDescriptor", menuName = "EndDescriptor")]
public class EndDescriptorScriptable : ScriptableObject
{
    public RankingDescriptor[] rankingDescriptors;
    public RankingTitles[] rankingTitles;
}
[System.Serializable]
public class RankingDescriptor
{
    public SimStatType statToRank;
    [TextArea]
    public string text;
    public float minrange;
    public float maxrange;
}
[System.Serializable]
public class RankingTitles
{
    public RankingTitleStat[] titleStats;
    public string TitleName;
    public string ReasonDescriptor;
    public BoostingFlags[] boosterFlags;
    public float maximumStatScore = 300;
    public bool getApplicable()
    {
        bool applicable = false;
        if (getScore() > maximumStatScore)
        {
            return false;
        }
        if (titleStats.Length == 0)
        {
            if (getScore() == 0)
            {
                return false;
            }
            return true;
        }
        else
        {
            for (int i = 0; i < titleStats.Length; i++)
            {
                if ((PlayerDataHolder.Current.getStatByEnum(titleStats[i].affectedStat).StatFloat >= titleStats[i].minimumStatScore))
                {
                    applicable = true;
                }
                else
                {
                    return false;
                }
            }
        }
        return applicable;
    }
    public float getScore()
    {
        float score = 0;
        for (int i = 0; i < titleStats.Length; i++)
        {
            score += (PlayerDataHolder.Current.getStatByEnum(titleStats[i].affectedStat).StatFloat);
        }
        for (int i = 0; i < boosterFlags.Length; i++)
        {
            if (GlobalGameFlags.GetFlag(boosterFlags[i].FlagName) != null)
            {
                score += boosterFlags[i].flagBoost;
            }
        }
        return score;
    }
}
[System.Serializable]
public class RankingTitleStat
{
    public SimStatType affectedStat;

    public float minimumStatScore;



}
[System.Serializable]
public class BoostingFlags
{
    public string FlagName;
    public float flagBoost;
}
public class SortedRankingTitle
{
    public RankingTitles rankingTitles;
    public float points;
    public bool canBeApplied;

    public SortedRankingTitle(RankingTitles newrankingTitles)
    {
        rankingTitles = newrankingTitles;
        canBeApplied = rankingTitles.getApplicable();
        points = rankingTitles.getScore();
    }

}