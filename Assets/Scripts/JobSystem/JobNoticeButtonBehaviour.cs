using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JobNoticeButtonBehaviour : MonoBehaviour
{
    public Text buttonText;
    JobNoticeScriptable _jobNotice;
    GameObject noticeInfoGraphic;
    // Start is called before the first frame update
    void Start()
    {
        noticeInfoGraphic = Resources.Load<GameObject>("JobNoticeInfoUI");
    }
    public void setJobNotice(JobNoticeScriptable jobNotice)
    {
        _jobNotice = jobNotice;
    }
    public void setButtonText(string jobTitle, float pay, int hours)
    {
        buttonText.text = string.Format("{0}.\n{1} euroa per tunti.\nSopimuksessa {2} tuntia viikossa", jobTitle, pay.ToString(), (hours * 5).ToString());
    }
    public void displayJob()
    {
        GameObject go = Instantiate(noticeInfoGraphic);
        go.transform.SetParent(MainCanvas.getMainCanvasTransform());
        go.transform.position = Vector3.zero;
        go.transform.localPosition = Vector3.zero;
        go.GetComponent<JobNoticeInfoBehaviour>().setJob(_jobNotice);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
