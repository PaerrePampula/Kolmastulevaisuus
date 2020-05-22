using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    class CameraTurnAction : ScriptableAction
    {
    public float angle;

    public override void PerformAction()
    {
        FloatChangeInfo valueChangeAction = new FloatChangeInfo();
        valueChangeAction.changeofFloat = angle;
        EventSystem.Current.DoEvent(
            EventSystem.Event_Type.CAMERA_TURN,
            valueChangeAction
            );
    }
}

