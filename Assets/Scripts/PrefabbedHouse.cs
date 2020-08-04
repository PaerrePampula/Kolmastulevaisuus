using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabbedHouse : MonoBehaviour
{
    [SerializeField]
    List<SingleHousePrefab> housePrefabsOnDisplay = new List<SingleHousePrefab>();

    public List<SingleHousePrefab> HousePrefabsOnDisplay { get => housePrefabsOnDisplay; set => housePrefabsOnDisplay = value; }
}
[System.Serializable]
public class SingleHousePrefab
{
    [SerializeField]
    string houseName;
    [SerializeField]
    GameObject source;

    public string HouseName { get => houseName; set => houseName = value; }
    public GameObject Source { get => source; set => source = value; }
}
