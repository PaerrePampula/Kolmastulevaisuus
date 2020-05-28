using UnityEngine;
using System.Collections;

public class Job
{
    string _jobTitle;
    int _workHoursPerDay;
    float _payByHour;
    GameObject jobSite;
    public Job(string title, float payByHour, JobSiteScriptable jobSiteScriptable, int workhoursPerDay = 0)
    {
        _jobTitle = title;
        _payByHour = payByHour;
        _workHoursPerDay = workhoursPerDay;
        jobSite = jobSiteScriptable.jobSite;
        
    }
}
