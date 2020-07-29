using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RentableUI : MonoBehaviour
{
    RentableHome rentable;
    Transform menuTransform;
    [SerializeField]
    TextMeshProUGUI addressHomeTypeRoomAmountSizeText;
    [SerializeField]
    TextMeshProUGUI longDescriptionText;
    [SerializeField]
    TextMeshProUGUI shortDescriptionText;
    [SerializeField]
    TextMeshProUGUI rentAmountText;
    [SerializeField]
    TextMeshProUGUI extrasInRentText;
    [SerializeField]
    StartGameButton startGameButton;
    public delegate void UiOpen(string gameObject);
    public static UiOpen onUiOpen;

    public void setRentable(RentableHome rentableHome, Transform NewmenuTransform)
    {
        rentable = rentableHome;
        setInfo();
        startGameButton.setRentable(rentable);
        menuTransform = NewmenuTransform;
        onUiOpen?.Invoke(rentable.PrefabForDisplay);
    }
    public void goBackToMenu() //Voisi olla kaikki tälläset jossain geneerisemmässäkin, mutta en usko että kovin monta eri tämän tyylistä menua peliin tarvitaan.
    {
        menuTransform.transform.gameObject.SetActive(true);
        Destroy(this.transform.gameObject);
    }
    void setInfo()
    {
        string addressText = "";
        addressText += rentable.Address + "\nKoko:" + rentable.Size + "m² ";
        addressText += "\n" + rentable.RentableVuokraTyyppi + " " + rentable.RentableHuoneKoko;
        addressHomeTypeRoomAmountSizeText.text = addressText;

        string rentText = "\nVuokra: " + rentable.BaseRent + " e/kk";
        rentAmountText.text = rentText;

        string longDescription = "";
        longDescription += rentable.LongDescription;
        longDescriptionText.text = longDescription;

        string shortForm = "";
        shortForm += rentable.ShortDescription;
        shortDescriptionText.text = shortForm;

        if (rentable.RentExtras.Length < 1)
        {
            extrasInRentText.text = "Ei lisäkuluja";
        }
        else
        {
            for (int i = 0; i < rentable.RentExtras.Length; i++)
            {
                extrasInRentText.text += rentable.RentExtras[i].extraName + ", hinta: " + rentable.RentExtras[i].extraCostPerMonth + "e/kk\n";
            }
        }

    }
}
