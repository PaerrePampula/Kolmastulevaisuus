using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEditor;

public class HouseDisplayMesh : MonoBehaviour //low prio, mutta pitäis yhdistää BuyObjectRendMeshin kaa
{

    GameObject displayObject;
    PrefabbedHouse houseComponent;
    private void OnEnable()
    {
        houseComponent = GetComponent<PrefabbedHouse>();
        RentableUI.onUiOpen += instantiateNewHoverObject;
    }
    private void OnDisable()
    {
        RentableUI.onUiOpen -= instantiateNewHoverObject;
    }
    void instantiateNewHoverObject(string go)
    {

        Transform [] allTransforms = gameObject.GetComponentsInChildren<Transform>();
        var givenChoice = houseComponent.HousePrefabsOnDisplay.FirstOrDefault(x => x.HouseName == go);
        for (int i = 0; i < allTransforms.Length; i++)
        {
            allTransforms[i].gameObject.layer = 0;
        }
        Transform[] transforms = givenChoice.Source.GetComponentsInChildren<Transform>();
        for (int i = 0; i < transforms.Length; i++)
        {
            transforms[i].gameObject.layer = 15;
        }
        givenChoice.Source.layer = 15;
        //if (displayObject != null)
        //{
        //    Destroy(displayObject);
        //}
        //displayObject = Instantiate(go, transform);
        //displayObject.transform.localPosition = Vector3.zero;
        //displayObject.transform.rotation = Quaternion.identity;

    }
}
