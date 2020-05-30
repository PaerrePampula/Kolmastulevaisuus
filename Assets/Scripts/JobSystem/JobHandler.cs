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
    public delegate void JobApply(JobNotice jobNotice);
    public static event JobApply OnJobApply;
    private void Start()
    {
        GameEventSystem.Current.RegisterListener(Event_Type.JOB_APPLY, registerJob);
    }
    Job createJob(JobInfo info)
    {
        JobNoticeScriptable notice = info.jobNotice.scriptable;
        Job newJob = new Job(notice.jobTitle, notice.payByHour, notice.jobSite , notice.workHoursPerDay);
        return newJob;
    }
    void registerJob(EventInfo info)
    {
        JobInfo job = (JobInfo)info;
        _currentJob = createJob(job);

        OnJobApply?.Invoke(job.jobNotice);
        PaerToolBox.callOnStatChange(StatType.PlayerJob, job.jobNotice.scriptable.jobTitle, false);

        createOnJobRegisterCall(_currentJob);

    }
    void createOnJobRegisterCall(Job job)
    {
        JobRegisterInfo jobInfo = new JobRegisterInfo();
        jobInfo.job = job;
        GameEventSystem.Current.DoEvent(
            Event_Type.JOB_REGISTERED_TO_PLAYER,
            jobInfo
            );
    }
}
