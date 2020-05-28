using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public struct PrereqPair
{
    public PreRequisite preRequisite;
    public string stringValue;
    public bool uniqueType;
    public PreReqTypes typeofPreReq;
}
public enum PreRequisite
{
    hasJob,
    hasSocialSecurity,
    hasHobby,
    hasMoney
}