public class Rent
{
    #region Fields
    float rent;
    float waterCost;
    float electricityCost;
    #endregion
    #region getters
    public float getTotal()
    {
        return rent + waterCost + electricityCost;
    }
    #endregion
}
