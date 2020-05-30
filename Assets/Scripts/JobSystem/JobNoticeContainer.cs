using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class JobNoticeContainer : MonoBehaviour
{
    #region Fields
    public List<JobNoticeScriptable> notices = new List<JobNoticeScriptable>();
    List<JobNotice> jobNotices = new List<JobNotice>();

    public delegate void JobsInitialized(List<JobNotice> jobNotices);
    public static event JobsInitialized OnJobsInitialized;
    #endregion

    #region MonobehaviourDefaults
    // Start is called before the first frame update
    private void OnEnable()
    {
        JobHandler.OnJobApply += updateNotices;
        JobSearcher.OnMenuInitialized += transferNoticeData;

    }
    private void OnDisable()
    {
        JobHandler.OnJobApply -= updateNotices;
    }
    void Start()
    {
        InitializeAllJobNotices();
    }
    #endregion

    void InitializeAllJobNotices()
    {
        for (int i = 0; i < notices.Count; i++)
        {
            JobNotice jobNotice = new JobNotice();
            jobNotice.scriptable = notices[i];
            jobNotices.Add(jobNotice);
        }
        OnJobsInitialized?.Invoke(jobNotices);
    }
    void updateNotices(JobNotice jobNotice)
    {
        var result = jobNotices.FirstOrDefault(notice => notice == jobNotice);
        jobNotices.Remove(result);
        OnJobsInitialized?.Invoke(jobNotices);
    }
    void transferNoticeData()
    {
        OnJobsInitialized?.Invoke(jobNotices);
    }
}
