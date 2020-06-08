using TMPro;

public class JobNoticeInfoBehaviour : UiGeneric
{
    #region Fields
    public TextMeshProUGUI noticeText;
    JobNotice _jobNotice;
    #endregion

    void insertInformation()
    {
        noticeText.text = _jobNotice.scriptable.jobDescriptionOnNotice;
    }
    public JobNoticeScriptable getScriptable()
    {
        return _jobNotice.scriptable;
    }
    public void setJob(JobNotice jobNotice)
    {
        _jobNotice = jobNotice;
        insertInformation();
    }
    public void applyForJob()
    {
        if (GlobalGameFlags.GetFlag("TUTORIAL_FIRSTJOB") == null)
        {
            Flag flag = new Flag("TUTORIAL_FIRSTJOB", 0, true);
            flag.FireFlag();
        }

        TimedActionRaise timedActionRaise = new TimedActionRaise(Event_Type.JOB_APPLY);

        JobInfo jobInfo = new JobInfo();
        jobInfo.jobNotice = _jobNotice;
        timedActionRaise.infoable = jobInfo;
        GameEventSystem.DoEvent(
            Event_Type.JOB_APPLY,
            jobInfo
            );
    }

}
