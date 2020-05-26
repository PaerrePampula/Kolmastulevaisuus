using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobSearcher : MonoBehaviour
{
    public Transform NoticePanel;
    GameObject noticeButton;
    public List<JobNoticeScriptable> notices = new List<JobNoticeScriptable>();
    List<Transform> InstantiatedNoticeUIButtons = new List<Transform>();

    private void Start()
    {
        noticeButton = Resources.Load<GameObject>("NoticeUIButton");
        aggregateAndDisplayNotices();
    }
    void aggregateAndDisplayNotices()
    {
        for (int i = 0; i < notices.Count; i++)
        {
            GameObject go = Instantiate(noticeButton);
            go.transform.SetParent(NoticePanel);
            InstantiatedNoticeUIButtons.Add(go.transform);
            go.GetComponent<JobNoticeButtonBehaviour>().setJobNotice(notices[i]);
            go.GetComponent<JobNoticeButtonBehaviour>().setButtonText(notices[i].jobTitle, notices[i].payByHour, notices[i].workHoursPerDay);
        }
    }
}
