using UnityEngine;
using System.Collections;

public class AsumisTuki : BaseTuki
{
    float maximumGrossIncomeWhereOmaVastuuDoesNotDecreaseSupportAmount = 726;
    public AsumisTuki(System.DateTime start, System.DateTime end, bool Monthly, typeOfSupport TypeOfSupport) : base(start, end, Monthly, TypeOfSupport) { }
    public override float CalculatedSupport()
    {

        float support = 0.8f * (HomeHandler.currentHomeHandler.getRent().getTotal() - CalculatedPerusOmaVastuu());
        return support;
    }
    float CalculatedPerusOmaVastuu() //Omavastuu ei vaikuta, jos tulot ovat liian pieniä...
    {
        float value = 0;
        float calculatedTotalIncome = 0;

        calculatedTotalIncome += PlayerEconomy.CurrentPlayerEconomy.getAllIncomeSourceGrossTotals(1);
        if (calculatedTotalIncome > maximumGrossIncomeWhereOmaVastuuDoesNotDecreaseSupportAmount)
        {
            value = 0.42f * (calculatedTotalIncome - (603 + 100)); //Pelkistetty laskukaava, mutta simulaatiossa oletetaan se, että opiskelijalla ei ole lapsia, tai muita ruokakuntalaisia. (asuu yksin)
            return value;
        }
        else
        {
            return value;
        }

    }
}
