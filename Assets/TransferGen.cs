using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransferGen : MonoBehaviour
{
    [SerializeField]
    GameObject transferMenuObject;
    [SerializeField]
    string source;
    [SerializeField]
    string destination;
    PlayerMoney sourceMoney;
    PlayerMoney destinationMoney;

    public void createTransferMenu()
    {
        GameObject calledObject = Instantiate(transferMenuObject);
        calledObject.transform.SetParent(this.gameObject.transform);
        calledObject.transform.localPosition = Vector3.zero;
        calledObject.transform.localScale = new Vector3(1, 1, 1);
        sourceMoney = (source == "Usable") ? sourceMoney = PlayerDataHolder.Current.PlayerMoney : sourceMoney = Bank.Current.SavedMoney;
        destinationMoney = (destination == "Usable") ? destinationMoney = PlayerDataHolder.Current.PlayerMoney : destinationMoney = Bank.Current.SavedMoney;
        calledObject.GetComponent<TransferBehaviour>().Initialize(sourceMoney, destinationMoney);
    }

}
