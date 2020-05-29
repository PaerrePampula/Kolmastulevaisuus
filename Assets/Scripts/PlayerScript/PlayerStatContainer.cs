﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerStatContainer : MonoBehaviour
{
    public List<PlayerStat> currentStats = new List<PlayerStat>();
    // Start is called before the first frame update
    static private PlayerStatContainer _Current;
    static public PlayerStatContainer Current
    {
        get
        {
            if (_Current == null)
            {
                _Current = FindObjectOfType<PlayerStatContainer>();
            }
            return _Current;
        }
    }
    void Start()
    {
        GameEventSystem.Current.RegisterListener(Event_Type.STATS_CALL, UpdateTable);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void UpdateTable(EventInfo eventInfo)
    {
        StatChangeInfo change = (StatChangeInfo)eventInfo;
        checkForDuplicatesAndInsertValue(change);

    }
    void checkForDuplicatesAndInsertValue(StatChangeInfo statChange)
    {

        if (statChange.playerStat.uniqueStat)
        {
            var searchForUniqueValue = currentStats.Single(stat => stat.statName == statChange.playerStat.statName);
            
            searchForUniqueValue.statValueString = statChange.playerStat.statValueString;
            Debug.Log(searchForUniqueValue.statValueString);
        }
        else
        {
            currentStats.Add(statChange.playerStat);
        }
        

    }
    public PlayerStat getPlayerStatByPrereq(PrereqPair pair)
    {
        for (int i = 0; i < currentStats.Count; i++)
        {
            if (currentStats[i].statName == pair.playerStat)
            {
                return currentStats[i];
            }

        }
        return null;
    }
}