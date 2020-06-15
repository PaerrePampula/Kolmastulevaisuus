﻿using UnityEngine;
using System.Collections;
[CreateAssetMenu(fileName = "Uusi ostettava", menuName = "Ostettava")]
public class BuyObjectScriptable : ScriptableObject
{
    public GameObject prefab;
    public string objectName;
    public float objectValue;
}
