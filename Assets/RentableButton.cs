using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RentableButton : MonoBehaviour
{
    #region Fields
    RentableHome rentableHome;
    [SerializeField]
    TextMeshProUGUI text;
    #endregion

    public void setRentable(RentableHome rentable)
    {
        rentableHome = rentable;
        setText();
    }
    void setText()
    {
        string generatedText = "";
        generatedText += rentableHome.Address + "\nKoko:" + rentableHome.Size + "m² ";
        generatedText += "\n" + rentableHome.RentableVuokraTyyppi + " " + rentableHome.RentableHuoneKoko;
        generatedText += "\nVuokra: " + rentableHome.BaseRent + " e/kk";
        text.text = generatedText;
    }
}
