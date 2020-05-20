using UnityEngine;
using System.Collections;
[CreateAssetMenu(menuName = "Actions/ColorChange")]
public class colorChangeAction : ScriptableAction
{

    public Color32 color;

    public override void PerformAction()
    {
        ColorChangeInfo valueChangeAction = new ColorChangeInfo();
        valueChangeAction.newColor = color;
        EventSystem.Current.DoEvent(
            EventSystem.Event_Type.DEBUG_COLOR_CHANGE,
            valueChangeAction
            );
    }
}
