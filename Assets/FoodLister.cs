using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodLister : MonoBehaviour
{

    [SerializeField]
    GameObject foodButton;
    private void OnEnable()
    {
        FoodPreparer.onFoodPrepare += ListFood;
        FoodItem.onFoodExpire += RemoveFood;
        updateFood();
    }
    // Update is called once per frame
    private void OnDisable()
    {
        FoodPreparer.onFoodPrepare -= ListFood;
        FoodItem.onFoodExpire -= RemoveFood;
    }
    private void Start()
    {

        updateFood();
    }
    void ListFood(FoodItem food)
    {

        updateFood();
    }
    void RemoveFood(FoodItem food)
    {

        updateFood();
    }
    void updateFood()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < PlayerDataHolder.Current.playerFoods.Count; i++)
        {
            GameObject go = Instantiate(foodButton);
            go.transform.SetParent(transform);
            GenericObjectHolder genericObjectHolder = go.GetComponent<GenericObjectHolder>();
            genericObjectHolder.item = PlayerDataHolder.Current.playerFoods[i];

        }
    }
}
