using System.Collections.Generic;
using UnityEngine;

public class Job : IStattable
{
    #region fields
    string _jobTitle;
    int _workHoursPerDay;
    float _payByHour;
    GameObject jobSite;
    public bool UniqueStat { get { return false; } }
    public StatType ThisStatType { get { return StatType.PlayerJob; } }
    List<GameEvent> jobEvents = new List<GameEvent>();

    #endregion
    #region constructors
    public Job(string title, float payByHour, JobSiteScriptable jobSiteScriptable, int workhoursPerDay = 0)
    {
        _jobTitle = title;
        _payByHour = payByHour;
        _workHoursPerDay = workhoursPerDay;
        jobSite = (jobSiteScriptable.jobSite != null) ? jobSiteScriptable.jobSite : null;

    }
    #endregion
    public float getMonthlyPaymentAmount()
    {
        int hoursPerMonth = _workHoursPerDay * 20;
        return hoursPerMonth * _payByHour;
    }
    public T getValue<T>()
    {
        return (T)(object)this;
    }
    public List<GameEvent> getJobEvents()
    {
        return jobEvents;
    }
    public void setJobEvents(List<GameEvent> gameEvents)
    {
        jobEvents = gameEvents;
    }
}
