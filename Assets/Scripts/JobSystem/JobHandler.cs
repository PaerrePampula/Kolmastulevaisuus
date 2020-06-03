using UnityEngine;

public static class JobHandler
{
    #region Fields
    public delegate void JobApply(JobNotice jobNotice);
    public static event JobApply OnJobApply;
    #endregion
    #region MonobehaviourDefaults
    static JobHandler() 
    {
        GameEventSystem.RegisterListener(Event_Type.JOB_APPLY, registerJob);
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
