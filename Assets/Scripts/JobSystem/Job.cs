using System.Collections.Generic;
using UnityEngine;

public class Job : IStattable, Incomeable
{
    #region fields
    string _jobTitle;

    float _payByHour;
    (int, int) hoursRange;
    string jobsite;
    int currentHours;
    
    public bool UniqueStat { get { return false; } }
    public StatType ThisStatType { get { return StatType.PlayerJob; } }
    bool anExtra = false;
    public bool AnExtra { get => anExtra; set => anExtra = value; }

    List<GameEvent> jobEvents = new List<GameEvent>();
    System.DateTime jobStartDate;
    System.DateTime jobEndDate;
    #endregion
    #region constructors
    public Job(string title, float payByHour, string newjobsite, int jobDuration, int minRange, int maxRange)
    {
        _jobTitle = title;
        _payByHour = payByHour;
        hoursRange = (minRange, maxRange);
        jobsite = newjobsite;
        jobStartDate = DateTimeSystem.getCurrentDate();

        var newDate = DateTimeSystem.getCurrentDate().AddMonths(jobDuration);
        var finalDayint = System.DateTime.DaysInMonth(newDate.Year,newDate.Month);
        var finalDayDate = new System.DateTime(newDate.Year, newDate.Month, finalDayint);
        jobEndDate = finalDayDate;
        DateTimeSystem.OnMonthChange += resetHours;
        LocationHandler.OnTurnEnd += addHoursToJob;
        JobHandler.OnJobEnd += JobEnd;
        JobHandler.OnJobApply += JobChange;
        ResetButton.onReset += RemoveJob;


    }
    void resetHours()
    {
        currentHours = 0;
    }
    #endregion
    public float getMonthlyPaymentAmount()
    {

        return currentHours * _payByHour;
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
    void JobChange(JobNotice job)
    {
        RemoveJob();
    }
    void JobEnd()
    {
        RemoveJob();
    }
    void RemoveJob()
    {
        PlayerDataHolder.Current.IncomeSources.Remove(this);
        JobHandler.OnJobEnd -= JobEnd;
        JobHandler.OnJobApply -= JobChange;
        DateTimeSystem.OnMonthChange -= resetHours;
        ResetButton.onReset -= RemoveJob;
    }
    void addHoursToJob()
    {
        int hours = Random.Range(hoursRange.Item1, hoursRange.Item2 + 1);
        currentHours += hours;
    }
    float speculatedPay()
    {
        float speculationMin = ((hoursRange.Item1 * _payByHour)) * 4f;
        float speculationMax = ((hoursRange.Item2 * _payByHour)) * 4f;
        float avgSpeculation = (speculationMin + speculationMax) / 2f;
        return avgSpeculation;
    }
    public float getSpeculatedNetIncomeInAMonth()
    {
        return speculatedPay() * TaxationSystem.netPaymentPercentage();
    }

    public float getNetIncomeInAMonth()
    {
        return (currentHours * _payByHour) * TaxationSystem.netPaymentPercentage();
    }

    public float getGrossIncomeAmountInAMonth()
    {
        return speculatedPay();
    }
}
