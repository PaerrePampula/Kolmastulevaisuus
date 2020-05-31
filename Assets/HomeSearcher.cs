using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeSearcher : MonoBehaviour
{
    #region Fields
    [SerializeField]
    List<RentableHomeScriptable> allScriptableHomes = new List<RentableHomeScriptable>();
    List<Transform> HomeUILElements = new List<Transform>();
    [SerializeField]
    string HomeUIELementPrefab;
    [SerializeField]
    Transform containerForHomeUIElementPrefabs;
    #endregion
    #region MonobehaviourDefaults
    private void Start()
    {
        GameObject prefab = Resources.Load<GameObject>(HomeUIELementPrefab);
        foreach (RentableHomeScriptable rentable in allScriptableHomes)
        {
            GameObject newRentableButton = Instantiate(prefab);
            RentableHome rentableHome = new RentableHome(rentable);
            newRentableButton.transform.SetParent(containerForHomeUIElementPrefabs);
            newRentableButton.GetComponent<RentableButton>().setRentable(rentableHome);
        }
    }
    #endregion
}
