using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndScreenUI : MonoBehaviour
{
    public PlayerGrade testGrade;
    public TextMeshProUGUI gradeText;
    float currentInt;
    [SerializeField]
    Gradient gradient;
    // Start is called before the first frame update
    void Start()
    {
        testGrade = Grader.getGrade(PlayerDataHolder.Current.GeneralRanking.StatFloat);
        StartCoroutine(startIncrementing(testGrade));
    }

    public IEnumerator startIncrementing(PlayerGrade test)
    {

        float t = 0.0f;
        int startValue = (int)PlayerGrade.F;
        int newvalue = startValue + (int)test;
        PlayerGrade goalGrade = (PlayerGrade)newvalue;
        while (gradeText.text != goalGrade.ToString())
        {

            currentInt = Mathf.Lerp(startValue, newvalue, t);
            float gradientValue = currentInt / 8;
            t += 0.65f * Time.fixedDeltaTime;
            PlayerGrade grade = (PlayerGrade)currentInt;
            gradeText.text = grade.ToString() ;
            gradeText.color = gradient.Evaluate(gradientValue);
            yield return null;

        }

    }
}
