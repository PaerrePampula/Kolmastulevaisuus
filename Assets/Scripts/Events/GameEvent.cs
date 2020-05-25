using UnityEngine;
using System.Collections;

public class GameEvent
{
    RandomEventScriptable scriptable;
    bool isUniqueEvent; //Tapahtuu vain kerran ikinä?
    bool hasFired; //Onko tämä event firenny ennenkin?
    FIRE_LOCATION fire_location;

    public GameEvent(RandomEventScriptable eventScriptable)
    {
        scriptable = eventScriptable;
        fire_location = eventScriptable.fire_location;
    }
    public RandomEventScriptable getData() //Hakee siis scriptablen eventistä tiedonhallintaa varten.
    {
        return scriptable;
    }
    public FIRE_LOCATION getFireLocation()
    {
        return fire_location;
    }
}
