using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //JobInfo jobInfo = new JobInfo();
        //jobInfo.jobNotice = _jobNotice;
        //GameEventSystem.DoEvent(
        //    Event_Type.JOB_APPLY,
        //    jobInfo
        //    );
        float t1 = 15;
        float t2 = 85;
        float t3 = 50;
        float t4 = 100;
        float normalizer = 1 / (t1 + t2 + t3 + t4);
        Debug.Log(t1 * normalizer);
        Debug.Log(t2 * normalizer);
        Debug.Log(t3 * normalizer);
        Debug.Log(t4 * normalizer);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
