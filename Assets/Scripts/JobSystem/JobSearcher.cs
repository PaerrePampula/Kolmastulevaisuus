﻿using System.Collections.Generic;
using UnityEngine;

public class JobSearcher : MonoBehaviour
{
    #region Fields
    public Transform NoticePanel;
    GameObject noticeButton;
    List<Transform> InstantiatedNoticeUIButtons = new List<Transform>();

    public delegate void MenuInitialize();
    public static event MenuInitialize OnMenuInitialized;
    #endregion

    #region MonobehaviourDefaults
    private void OnEnable()
    {
        JobNoticeContainer.OnJobsInitialized += aggregateAndDisplayNotices;
    }
    private void OnDisable()
    {
        JobNoticeContainer.OnJobsInitialized -= aggregateAndDisplayNotices;
    }
    private void Start()
    {
        noticeButton = Resources.Load<GameObject>("NoticeUIButton");
        OnMenuInitialized?.Invoke();
    }
    #endregion
    void aggregateAndDisplayNotices(List<JobNotice> notices)
    {
        InstantiatedNoticeUIButtons.Clear();
        foreach (Transform transform in NoticePanel)
        {
            Destroy(transform.gameObject);
        }
        if (notices.Count > 0)
        {
            for (int i = 0; i < notices.Count; i++)
            {
                GameObject go = Instantiate(noticeButton);
                go.transform.SetParent(NoticePanel);
                go.transform.localScale = new Vector3(1, 1, 1);
                InstantiatedNoticeUIButtons.Add(go.transform);
                go.transform.GetChild(1).GetComponent<JobNoticeButtonBehaviour>().setJobNotice(notices[i]);
                go.transform.GetChild(1).GetComponent<JobNoticeButtonBehaviour>().setButtonText(notices[i].scriptable.jobTitle, notices[i].scriptable.payByHour, notices[i].scriptable.minimumHoursPerWeek, notices[i].scriptable.maximumHoursPerWeek);
            }
        }
              

    }
}
