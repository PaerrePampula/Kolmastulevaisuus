[System.Serializable]
public class PlayerStat
{
    public StatType statName;
    public string statValueString;
    public float statValueToFloat()
    {
        return float.Parse(statValueString);
    }
    public bool booleanValue;
    public bool uniqueStat;
}
