using UnityEngine;
[CreateAssetMenu(menuName = "Actions/CameraMoveAction")]
public class CameraMoveAction : ScriptableAction

//Tämä on action, jolla kameraa käännetään ja sijaintia vaihdetaan, kun pelaaja vaihtaa sijaintia, joku changeLocale olisi ollut toki osuvampi nimi
//ja tämän voisi korvata sillä myöhemmin, kun tehdään ei-prototyyppiversio.
{
    public int Turns;
    float angle()
    {
        return Turns * 90;
    }
    public override void PerformAction()
    {
        CameraAngleChangeInfo valueChangeAction = new CameraAngleChangeInfo();
        valueChangeAction.changeofFloat = angle();
        valueChangeAction.increments = Turns;
        thisEvent_Type = Event_Type.CAMERA_TURN;
        GameEventSystem.DoEvent(
            thisEvent_Type,
            valueChangeAction
            );
    }
}
