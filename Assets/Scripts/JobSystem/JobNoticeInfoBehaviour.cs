using TMPro;

public class JobNoticeInfoBehaviour : UiGeneric
{
    #region Fields
    public TextMeshProUGUI noticeText;
    public TextMeshProUGUI percentageIndicator;
    JobNotice _jobNotice;
    #endregion

    void insertInformation()
    {
        noticeText.text = _jobNotice.scriptable.jobDescriptionOnNotice;
        percentageIndicator.text = (_jobNotice.scriptable.chanceOfBeingHired.ToString() + "%");
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
