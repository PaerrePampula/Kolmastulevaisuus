using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class JobHandler
{
    #region Fields
    public delegate void JobApply(JobNotice jobNotice);
    public static event JobApply OnJobApply;
    static List<JobInfo> cachedJobNotices = new List<JobInfo>();
    static List<(JobInfo, float, float)> cachedJobProbabilities = new List<(JobInfo, float,float)>();
    #endregion
    #region MonobehaviourDefaults
    static JobHandler() 
    {
        GameEventSystem.RegisterListener(Event_Type.JOB_APPLY, cacheJob);
        LocationHandler.OnTurnEnd += checkJobApply;
    }

    #endregion
    static Job createJob(JobInfo info)
    {
        JobNoticeScriptable notice = info.jobNotice.scriptable;
        Job newJob = new Job(notice.jobTitle, notice.payByHour, notice.jobSite, notice.workHoursPerDay);
        return newJob;
    }
    static void registerJob(EventInfo info)
    {
        JobInfo job = (JobInfo)info;
        PlayerDataHolder.PlayerJob = createJob(job);

        OnJobApply?.Invoke(job.jobNotice);
        PaerToolBox.callNonUniqueStatChange(PlayerDataHolder.PlayerJob);

        createOnJobRegisterCall(PlayerDataHolder.PlayerJob);

    }
    static void dieRollTheJob()
    {
        float randomBetweenZeroAndOne = Random.Range(0, 1);
        var selected = cachedJobProbabilities.SingleOrDefault(x => (randomBetweenZeroAndOne >= x.Item2) && (randomBetweenZeroAndOne < x.Item3));
        registerJob(selected.Item1);
        cachedJobNotices.Clear();
    }
    static void checkJobApply()
    {
        cachedJobProbabilities.Clear();
        if (cachedJobNotices.Count > 0)
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
        }
        dieRollTheJob();
    }
    static void cacheJob(EventInfo eventInfo)
    {
        JobInfo job = (JobInfo)eventInfo;
        cachedJobNotices.Add(job);

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
}
