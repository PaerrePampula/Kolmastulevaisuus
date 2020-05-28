using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobHandler : MonoBehaviour
{
    Job _currentJob;
    static private JobHandler _Current; //Tietenkin tämänhetkinen eventsystem. On static, koska silloin sitä voi käsitellä mistä tahansa koodissa.
    static public JobHandler Current
    {
        get
        {
            if (_Current == null)
            {
                _Current = FindObjectOfType<JobHandler>();
            }
            return _Current;
        }
    }
    private void Start()
    {
        EventSystem.Current.RegisterListener(Event_Type.JOB_APPLY, applyJob);
    }
    void applyJob(EventInfo info)
    {
        JobInfo job = (JobInfo)info;
        Debug.Log(job.jobNotice.jobTitle);
    }
}
