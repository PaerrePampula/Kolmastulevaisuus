using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public static class NormalizedChanceGenerator
{
    public static T getSelection<T>(IChanceable[] appliedList)
    {
        List<(T, float, float)> endResultRanges = new List<(T, float, float)>(); //Lopputuloksen objekti-alaraja-yläraja chanssialue
        List<(T, float)> objectsAndTheirProbabilities = new List<(T, float)>(); //Objektit ja niiden tsänssiluvut

        float sumOfProbabilityMultipliers = 0; //Summa kaikkilla objektien tsänsseillä
        float normalizerValue = 1; //Normalize on arvoon yksi 
        //(Eli kaikki arvot on lopulta korkeintaan yksi)

        for (int i = 0; i < appliedList.Length; i++) //Summataan ym. summaan kaikki mahikset, ja tehdään tuplet objektista ja sen tsänssistä, sekä lisätään se listaan.
        {
            sumOfProbabilityMultipliers += appliedList[i].getChance();
            (T, float) objectAndChance = ((T)appliedList[i], appliedList[i].getChance());
            objectsAndTheirProbabilities.Add(objectAndChance);
        }

        normalizerValue = (normalizerValue / sumOfProbabilityMultipliers); //Hankitaan oikea normalizer


        for (int i = 0; i < objectsAndTheirProbabilities.Count; i++)
        {
            if (i == 0)
            {
                endResultRanges.Add((objectsAndTheirProbabilities[i].Item1, 0, (objectsAndTheirProbabilities[i].Item2 * normalizerValue))); //Jos on eka, ensimmäinen arvo on 0
            }
            else
            {
                endResultRanges.Add((objectsAndTheirProbabilities[i].Item1, endResultRanges[i - 1].Item3, (endResultRanges[i - 1].Item3 + objectsAndTheirProbabilities[i].Item2 * normalizerValue)));
                //perättäin sijoitellaan saadut arvot.
            }
        }
        foreach (var item in endResultRanges)
        {
            Debug.Log(item.Item2 * 100 + "-" + item.Item3 * 100); //Debug
        }
        float randomRange = Random.Range(0f, 1f); //valitaan väliltä 0-1 float arvo joka on meidän random valinta

        var selection = endResultRanges.SingleOrDefault(x => (randomRange >= x.Item2) && (randomRange < x.Item3)); //Etsitään meidän listasta se random joka vastaa random saatua arvoa

        return selection.Item1;
    }
}
