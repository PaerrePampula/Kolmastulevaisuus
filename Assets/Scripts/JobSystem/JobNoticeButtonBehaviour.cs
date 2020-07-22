using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JobNoticeButtonBehaviour : MonoBehaviour
{
    #region Fields
    public TextMeshProUGUI buttonText;
    JobNotice _jobNotice;
    GameObject noticeInfoGraphic;
    #endregion
    #region MonobehaviourDefaults
    // Start is called before the first frame update
    void Start()
    {
        noticeInfoGraphic = Resources.Load<GameObject>("JobNoticeInfoUI");
    }
    #endregion
    public void setJobNotice(JobNotice jobNotice)
    {
        _jobNotice = jobNotice;
    }
    public void setButtonText(string jobTitle, float pay, int hoursMin, int hoursMax = 0)
    {
        string range = "";
        if (hoursMax != hoursMin)
        {
            range = hoursMin + "-" + hoursMax;
        }
        else
        {
            range = hoursMin.ToString();
        }
        buttonText.text = string.Format("{0}.\n{1}€ / 1h.\n{2}h / Viikko ", jobTitle, pay.ToString(), range);
    }
    public void displayJob()
    {
        GameObject go = Instantiate(noticeInfoGraphic);
        go.transform.SetParent(SceneCanvas.mainTransform.getMainCanvasTransform());
        go.transform.position = Vector3.zero;
        go.transform.localPosition = Vector3.zero;
        go.GetComponent<JobNoticeInfoBehaviour>().setJob(_jobNotice);
    }
}
