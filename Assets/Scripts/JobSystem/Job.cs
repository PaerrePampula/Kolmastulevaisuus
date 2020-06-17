using System.Collections.Generic;
using UnityEngine;

public class Job : IStattable
{
    #region fields
    string _jobTitle;
    int _workHoursPerDay;
    float _payByHour;
    string jobsite;
    public bool UniqueStat { get { return false; } }
    public StatType ThisStatType { get { return StatType.PlayerJob; } }
    List<GameEvent> jobEvents = new List<GameEvent>();
    System.DateTime jobStartDate;
    System.DateTime jobEndDate;
    #endregion
    #region constructors
    public Job(string title, float payByHour, string newjobsite, int jobDuration,  int workhoursPerDay = 0)
    {
        _jobTitle = title;
        _payByHour = payByHour;
        _workHoursPerDay = workhoursPerDay;
        jobsite = newjobsite;
        jobStartDate = DateTimeSystem.getCurrentDate();

        var newDate = DateTimeSystem.getCurrentDate().AddMonths(jobDuration);
        var finalDayint = System.DateTime.DaysInMonth(newDate.Year,newDate.Month);
        var finalDayDate = new System.DateTime(newDate.Year, newDate.Month, finalDayint);
        jobEndDate = finalDayDate;

    }
    #endregion
    public float getMonthlyPaymentAmount()
    {
        int hoursPerMonth = _workHoursPerDay * 12;
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
    public bool hasExpired()
    {
        return (DateTimeSystem.getCurrentDate() > jobEndDate) ? true : false;
    }

}
