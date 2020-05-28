using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatContainer : MonoBehaviour
{
    public List<PrereqPair> currentStats = new List<PrereqPair>();
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
        GameEventSystem.Current.RegisterListener(Event_Type.PREREQ_UPDATE, UpdateTable);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void UpdateTable(EventInfo eventInfo)
    {
        PreRequisiteChange change = (PreRequisiteChange)eventInfo;
        checkForDuplicates(change);

    }
    void checkForDuplicates(PreRequisiteChange preRequisiteChange)
    {
        for (int i = 0; i < currentStats.Count; i++)
        {
            for (int x = 0; x < preRequisiteChange.prereqs.Length; x++)
            {
                if ((currentStats[i].uniqueType == false) && currentStats[i].preRequisite == preRequisiteChange.prereqs[x].preRequisite)
                {
                    currentStats.RemoveAt(i);
                    currentStats.Add(preRequisiteChange.prereqs[x]);
                }
                else
                {
                    currentStats.Add(preRequisiteChange.prereqs[x]);
                }
            }

        }
    }
    public PrereqPair getPairByPreReq(PrereqPair pair)
    {
        for (int i = 0; i < currentStats.Count; i++)
        {
            if (currentStats[i].preRequisite == pair.preRequisite)
            {
                return currentStats[i];
            }

        }
        return currentStats[0];
    }
}
