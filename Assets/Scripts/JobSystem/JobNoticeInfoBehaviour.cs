using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
        JobInfo jobInfo = new JobInfo();
        jobInfo.jobNotice = _jobNotice;
        GameEventSystem.Current.DoEvent(
            Event_Type.JOB_APPLY,
            jobInfo
            );
    }
}
