using UnityEngine;
using System.Collections;
[CreateAssetMenu(fileName = "EndDescriptor", menuName = "EndDescriptor")]
public class EndDescriptorScriptable : ScriptableObject
{
    public RankingDescriptor[] rankingDescriptors;
}
[System.Serializable]
public class RankingDescriptor
{
    public SimStatType statToRank;
    [TextArea]
    public string text;
    public float minrange;
    public float maxrange;
}