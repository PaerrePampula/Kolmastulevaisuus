using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Uusi työpaikka", menuName = "Työpaikka")]
public class JobSiteScriptable : ScriptableObject //Toistaiseksi tämä on käyttämätön, ignore
{
    public GameObject jobSite;
    public JobTitle[] jobTitles;
}
[System.Serializable]
public class JobTitle
{
    public string jobTitle;
    public float pay;
}
