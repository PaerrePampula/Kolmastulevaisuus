using System.Linq;
using UnityEngine;

public class TaxationSystem : MonoBehaviour
{
    #region Fields
    float genericAverageMunincipalTax = 0.21f; //Ei varsinaisesti perustu mihinkään tietyn kunnan verotukseen, mutta on melko lähellä sitä, mitä useimmissa veronmäärä on.
    float PlayerCalculatedTaxRate = 0f;
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
    #endregion
    #region Getters
    public float getPlayerTaxRateForIncome()
    {
        return PlayerCalculatedTaxRate;
    }
    public float getPlayerTaxRateInverse()
    {
        float inverse = 1f;
        inverse -= PlayerCalculatedTaxRate;
        return inverse;
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
    #endregion
}
