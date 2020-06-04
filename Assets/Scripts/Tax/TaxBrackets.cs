using System.Collections.Generic;
using UnityEngine;

public static class TaxBrackets //Kuumeisesti yritän miettiä tähän paljon järkevämpää ratkaisua mutta kuten vanha viisaus sanoo, Nothing more permanent than a temporary fix
                         //Nämä tiedot haettu https://www.veronmaksajat.fi/Palkka-ja-elake/Jarin-palkka/#75d32b1a 29.5.2020 tuloveroasteikosta.

{
    #region Fields
    //static float TaxBracketALower = 17600f;
    //static float TaxBracketAUpper = 26400f;
    //static float TaxBracketABase = 0.08f;
    //static float TaxBracketAPercent = 6f;

    //static float TaxBracketBLower = 26400f;
    //static float TaxBracketBUpper = 43500f;
    //static float TaxBracketBBase = 536f;
    //static float TaxBracketBPercent = 0.1725f;

    //static float TaxBracketCLower = 43500f;
    //static float TaxBracketCUpper = 76100f;
    //static float TaxBracketCBase = 3485.75f;
    //static float TaxBracketCPercent = 0.2125f;

    //static float TaxBracketDLower = 76100f;
    //static float TaxBracketDUpper = Mathf.Infinity;
    //static float TaxBracketDBase = 10413.25f;
    //static float TaxBracketDPercent = 0.3125f;
    #endregion
    public static List<NationalIncomeTaxBracket> NationalIncomeTaxBrackets = new List<NationalIncomeTaxBracket>
    {
        new NationalIncomeTaxBracket(ConfigFileReader.getValue("TaxBracketALower"), ConfigFileReader.getValue("TaxBracketAUpper")
            , ConfigFileReader.getValue("TaxBracketABase"), ConfigFileReader.getValue("TaxBracketAPercent")),
        new NationalIncomeTaxBracket(ConfigFileReader.getValue("TaxBracketBLower"), ConfigFileReader.getValue("TaxBracketBUpper")
            , ConfigFileReader.getValue("TaxBracketBBase"), ConfigFileReader.getValue("TaxBracketBPercent")),
        new NationalIncomeTaxBracket(ConfigFileReader.getValue("TaxBracketCLower"), ConfigFileReader.getValue("TaxBracketCUpper")
            , ConfigFileReader.getValue("TaxBracketCBase"), ConfigFileReader.getValue("TaxBracketCPercent")),
        new NationalIncomeTaxBracket(ConfigFileReader.getValue("TaxBracketDLower"), Mathf.Infinity
            , ConfigFileReader.getValue("TaxBracketDBase"), ConfigFileReader.getValue("TaxBracketDPercent"))
    };
}
