﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TaxBrackets //Kuumeisesti yritän miettiä tähän paljon järkevämpää ratkaisua mutta kuten vanha viisaus sanoo, Nothing more permanent than a temporary fix
{
    static float TaxBracketALower = 17600f;
    static float TaxBracketAUpper = 26400f;
    static float TaxBracketABase = 0.08f;
    static float TaxBracketAPercent = 6f;

    static float TaxBracketBLower = 26400f;
    static float TaxBracketBUpper = 43500f;
    static float TaxBracketBBase = 536f;
    static float TaxBracketBPercent = 0.1725f;

    static float TaxBracketCLower = 43500f;
    static float TaxBracketCUpper = 76100f;
    static float TaxBracketCBase = 3485.75f;
    static float TaxBracketCPercent = 0.2125f;

    static float TaxBracketDLower = 76100f;
    static float TaxBracketDUpper = Mathf.Infinity;
    static float TaxBracketDBase = 10413.25f;
    static float TaxBracketDPercent = 0.3125f;

    public static List<NationalIncomeTaxBracket> NationalIncomeTaxBrackets = new List<NationalIncomeTaxBracket>
    {
        new NationalIncomeTaxBracket(TaxBracketALower, TaxBracketAUpper, TaxBracketABase, TaxBracketAPercent),
        new NationalIncomeTaxBracket(TaxBracketBLower, TaxBracketBUpper, TaxBracketBBase, TaxBracketBPercent),
        new NationalIncomeTaxBracket(TaxBracketCLower, TaxBracketCUpper, TaxBracketCBase, TaxBracketCPercent),
        new NationalIncomeTaxBracket(TaxBracketDLower, TaxBracketDUpper, TaxBracketDBase, TaxBracketDPercent)
    };
}