using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JobNoticeInfoBehaviour : MonoBehaviour
{
    public TextMeshProUGUI noticeText;
    JobNoticeScriptable _jobNotice;
    void insertInformation()
    {
        noticeText.text = _jobNotice.jobDescriptionOnNotice;
    }
    public void setJob(JobNoticeScriptable jobNotice)
    {
        _jobNotice = jobNotice;
        insertInformation();
    }
}
