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
    static List<(JobInfo, float, float)> cachedJobProbabilities = new List<(JobInfo, float,float)>();
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
        Job newJob = new Job(notice.jobTitle, notice.payByHour, notice.jobSiteScene, notice.jobLengthInMonths , notice.workHoursPerDay);
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
        cachedJobProbabilities.Clear(); //Tyhjätään tän hetkinen lopputulos tälle metodille.
        if (cachedJobNotices.Count > 0) //Jos cachesta löytyy ilmoituksia, suoritetaan.
        {
            float sum = 0;
            List<(JobInfo, float)> allJobs = new List<(JobInfo, float)>();
            foreach (var job in cachedJobNotices)
            {
                sum += job.jobNotice.scriptable.successMultiplier;
                (JobInfo, float) pairMultiplier = (job ,job.jobNotice.scriptable.successMultiplier);
                allJobs.Add(pairMultiplier);
            }
            float normalizer = 1;
            normalizer = (normalizer / sum);
            List<(JobInfo, float, float)> ranges = new List<(JobInfo, float, float)>();
            for (int i = 0; i < allJobs.Count; i++)
            {
                if (i == 0)
                {
                    ranges.Add((allJobs[i].Item1, 0, (allJobs[i].Item2 * normalizer)));
                }
                else
                {
                    ranges.Add((allJobs[i].Item1, ranges[i - 1].Item3, (allJobs[i].Item2 * normalizer)));
                }

            }
            foreach (var item in allJobs)
            {
                Debug.Log(item.Item2 * 100);
            }
            cachedJobProbabilities = ranges;

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
        float randomBetweenZeroAndOne = Random.Range(0, 1);
        var selected = cachedJobProbabilities.SingleOrDefault(x => (randomBetweenZeroAndOne >= x.Item2) && (randomBetweenZeroAndOne < x.Item3));
        registerJob(selected.Item1);

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

        PlayerDataHolder.PlayerJob = newJob;
        OnJobApply?.Invoke(job.jobNotice);
        PaerToolBox.callNonUniqueStatChange(PlayerDataHolder.PlayerJob);

        createOnJobRegisterCall(PlayerDataHolder.PlayerJob);

        List<GameEvent> gameEvents = EventControl.createEvents(job.jobNotice.scriptable.jobEvents);

        newJob.setJobEvents(gameEvents);

        EventControl.AggregateNewGameEvents(gameEvents);
    }
    static void checkPlayerJobDuration()
    {
        if (PlayerDataHolder.PlayerJob != null)
        {
            if (PlayerDataHolder.PlayerJob.hasExpired())
            {
                endPlayerJob();
            }
        }

    }
    static void endPlayerJob()
    {
        OnJobEnd.Invoke();
        EventControl.removeEvents(PlayerDataHolder.PlayerJob.getJobEvents());
        PaerToolBox.callNonUniqueStatChange(PlayerDataHolder.PlayerJob);
        PlayerDataHolder.PlayerJob = null;
        Flag flag = new Flag("PLAYER_JOB_CONTRACT_END", 0, false);
        flag.FireFlag();

    }
}
