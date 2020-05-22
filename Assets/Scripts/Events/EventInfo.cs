using UnityEngine;
using System.Collections;

public abstract class EventInfo
{
    string eventDebugInformation;
    //Debug.login käyttöön.
}
public class FloatChangeInfo : EventInfo
{
    public float changeofFloat;
}
public class ColorChangeInfo : EventInfo
{
    public Color32 newColor;
}
public class CameraAngleChangeInfo : EventInfo
{
    public int increments;
    public float changeofFloat;
}