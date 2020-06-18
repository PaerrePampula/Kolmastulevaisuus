public class AsumisTuki : BaseTuki
{
    #region Fields
    float maximumGrossIncomeWhereOmaVastuuDoesNotDecreaseSupportAmount = 726;
    #endregion
    #region constructors
    public AsumisTuki(System.DateTime start, System.DateTime end, bool Monthly, typeOfSupport TypeOfSupport) : base(start, end, Monthly, TypeOfSupport) { }
    #endregion
    public override float CalculatedSupport()
    {

        float support = 0.8f * (PlayerDataHolder.Current.PlayerRent.getTotal() - CalculatedPerusOmaVastuu());
        return support;
    }
    float CalculatedPerusOmaVastuu() //Omavastuu ei vaikuta, jos tulot ovat liian pieniä... 
                                     //Oletus on siinä että pelaaja asuu yksin.
    {
        float value = 0;
        float calculatedTotalIncome = 0;

        calculatedTotalIncome += PlayerEconomy.getAllIncomeSourceGrossTotals(1);
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
