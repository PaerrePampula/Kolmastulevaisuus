public class Rent
{
    #region Fields
    float rent;
    float waterCost;
    float electricityCost;
    #endregion
    #region constructors
    public Rent(float rentamount, float watercost =0, float eleccost = 0)
    {
        rent = rentamount;
        waterCost = watercost;
        electricityCost = eleccost;
    }
    #endregion
    #region getters
    public float getTotal()
    {
        return rent + waterCost + electricityCost;
    }
    #endregion
}
