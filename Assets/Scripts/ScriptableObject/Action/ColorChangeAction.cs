using UnityEngine;
[CreateAssetMenu(menuName = "Actions/ColorChange")]
public class ColorChangeAction : ScriptableAction
{

    public Color32 color;

    public override void PerformAction()
    {
        ColorChangeInfo valueChangeAction = new ColorChangeInfo();
        valueChangeAction.newColor = color;
        GameEventSystem.DoEvent(
            thisEvent_Type,
            valueChangeAction
            );
    }
}
