using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class JobHandler
{
    #region Fields
    public delegate void JobApply(JobNotice jobNotice);
    public static event JobApply OnJobApply;
    public delegate void JobEnd();
    public static event JobEnd OnJobEnd;
    public delegate void JobInfoChange();
    static List<JobInfo> cachedJobNotices = new List<JobInfo>();

    #endregion
    #region MonobehaviourDefaults
    static JobHandler() 
    {
        DateTimeSystem.OnMonthChange += checkPlayerJobDuration;
        GameEventSystem.RegisterListener(Event_Type.JOB_APPLY, cacheJob);
        LocationHandler.OnTurnEnd += checkJobApply;

    }

    
    #endregion
    static Job createJob(JobInfo info)
    {
        JobNoticeScriptable notice = info.jobNotice.scriptable;
        Job newJob = new Job(notice.jobTitle, notice.payByHour, notice.jobSiteScene, notice.jobLengthInMonths , notice.minimumHoursPerWeek, notice.maximumHoursPerWeek);
        return newJob;
    }
    static void createOnJobRegisterCall(Job job)
    {
        JobRegisterInfo jobInfo = new JobRegisterInfo();
        jobInfo.job = job;
        GameEventSystem.DoEvent(
            Event_Type.JOB_REGISTERED_TO_PLAYER,
            jobInfo
            );
    }

    //Työnhaku vaihe 1. Cachea työpaikka haku varastoon, jos niin vain voi...
    static void cacheJob(EventInfo eventInfo) //Cachetaan uudet työpaikkahaut jonnekin varastoon myöhempää työn haun tulosta varten.
    {
        JobInfo job = (JobInfo)eventInfo;

        if ((cachedJobNotices.SingleOrDefault(cachedJob => cachedJob.jobNotice == job.jobNotice) != null))
        {
            Flag flag = new Flag("JOB_ALREADY_APPLIED", 0, false);
            flag.FireFlag();
            return; //Pelaaja on jo hakenut tätä työpaikkaa, älä cachea sitä kahdesti.
        }


        //Jos meiltä ei löydy vielä jobhunt flagia. addaa se
        Flag jobHuntFlag = GlobalGameFlags.GetFlag("PLAYER_JOBHUNT");
        if (jobHuntFlag == null)
        {
            jobHuntFlag = new Flag("PLAYER_JOBHUNT", 0, false);
            GlobalGameFlags.addFlag(jobHuntFlag);
        }

        int randomInt = Random.Range(0, 100);
        if (randomInt <= job.jobNotice.scriptable.chanceOfBeingHired) //Jos pelaajalla on tsänssin mukaan mahdollisuus saada työpaikka, cachea tämä tulos
        {
            cachedJobNotices.Add(job);
        }


    }

    //Aikaa on kulunut, nyt koita generoida valituille työpaikoille tsänssit saada tämä työpaikka
    static void checkJobApply()
    {

        if (cachedJobNotices.Count > 0) //Jos cachesta löytyy ilmoituksia, suoritetaan.
        {
            dieRollTheJob();
        }
        else
        {
            Flag jobHuntFlag = GlobalGameFlags.GetFlag("PLAYER_JOBHUNT");
            if (jobHuntFlag != null)
            {
                Flag flag = new Flag("PLAYER_DID_NOT_GET_JOB",0,false);
                flag.FireFlag();
                GlobalGameFlags.DisposeFlag(jobHuntFlag);
            }
        }

    }

    //Käytä generoituja tsänssejä, ja valitse työpaikka.
    static void dieRollTheJob()
    {

        registerJob(NormalizedChanceGenerator.getSelection<JobInfo>(cachedJobNotices.ToArray()));

        Flag flag = new Flag("JOB_RECEIVED", 0, false);
        flag.FireFlag();

        GlobalGameFlags.DisposeFlag(GlobalGameFlags.GetFlag("PLAYER_JOBHUNT"));

        cachedJobNotices.Clear();
    }

    //Rekisteröi valittu työpaikka.
    static void registerJob(EventInfo info)
    {
        JobInfo job = (JobInfo)info;
        Job newJob = createJob(job);

        PlayerDataHolder.Current.PlayerJob = newJob;
        OnJobApply?.Invoke(job.jobNotice);
        PaerToolBox.callNonUniqueStatChange(PlayerDataHolder.Current.PlayerJob);

        createOnJobRegisterCall(PlayerDataHolder.Current.PlayerJob);

        List<GameEvent> gameEvents = EventControl.createEvents(job.jobNotice.scriptable.jobEvents);

        newJob.setJobEvents(gameEvents);

        EventControl.AggregateNewGameEvents(gameEvents);

    }
    static void checkPlayerJobDuration()
    {
        if (PlayerDataHolder.Current.PlayerJob != null)
        {
            if (PlayerDataHolder.Current.PlayerJob.hasExpired())
            {
                endPlayerJob();
            }
        }

    }
    static void endPlayerJob()
    {
        OnJobEnd.Invoke();
        EventControl.removeEvents(PlayerDataHolder.Current.PlayerJob.getJobEvents());
        PaerToolBox.callNonUniqueStatChange(PlayerDataHolder.Current.PlayerJob);
        PlayerDataHolder.Current.PlayerJob = null;
        Flag flag = new Flag("PLAYER_JOB_CONTRACT_END", 0, false);
        flag.FireFlag();

    }
}
