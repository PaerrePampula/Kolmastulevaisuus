using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class TaxationSystem : MonoBehaviour
{
    float genericAverageMunincipalTax = 0.21f; //Ei varsinaisesti perustu mihinkään tietyn kunnan verotukseen, mutta on melko lähellä sitä, mitä useimmissa veronmäärä on.
    static float PlayerCalculatedTaxRate;
    static private TaxationSystem _taxationSystem;
    static public TaxationSystem taxationSystem
    {
        get
        {
            if (_taxationSystem == null)
            {
                _taxationSystem = FindObjectOfType<TaxationSystem>();
            }
            return _taxationSystem;
        }
    }
    public static float getPlayerTaxRateForIncome()
    {
        return PlayerCalculatedTaxRate;
    }
    public static float getPlayerTaxRateInverse()
    {
        return 1 - PlayerCalculatedTaxRate;
    }
    public float getIncomeAfterTaxes(float allIncomesTotalGross)
    {

        float IncomeAfterNationalTaxes = getIncomeAfterNationalTax(allIncomesTotalGross);
        float InComeAfterMunincipalTaxes = IncomeAfterNationalTaxes *= (1f - genericAverageMunincipalTax);

        return InComeAfterMunincipalTaxes; //Kerrotaan saatu arvo valtionverojen jälkeen vielä kunnallisveron jälkeen jäävällä prosenttimäärällä, 1 = 100%

    }
    public void calculateTaxRate(float allIncomesTotalGross)
    {
        float IncomeAfterNationalTaxes = getIncomeAfterNationalTax(allIncomesTotalGross);
        float InComeAfterMunincipalTaxes = IncomeAfterNationalTaxes *= (1f - genericAverageMunincipalTax);
        PlayerCalculatedTaxRate = 1 - (InComeAfterMunincipalTaxes / allIncomesTotalGross);
    }
    float getIncomeAfterNationalTax(float gross)
    {
        NationalIncomeTaxBracket foundBracket = TaxBrackets.NationalIncomeTaxBrackets.SingleOrDefault
                    (bracket => PaerToolBox.isBetween(gross, bracket.getLower(), bracket.getUpper(), true) == true);

        if (foundBracket == null) //Ansiot ovat alle minkään bracketin
        {
            return gross;
        }
        else //Ansio kuuluu tiettyyn bracketiin.
        {
            float taxAmount = gross - foundBracket.getLower();
            taxAmount *= foundBracket.getPercentileTax();
            taxAmount += foundBracket.getBottomLineTax();
            
            return gross - taxAmount;
        }

    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
}
