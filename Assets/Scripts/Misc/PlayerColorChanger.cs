using UnityEngine;

public class PlayerColorChanger : MonoBehaviour //Tämä oli vain debugia ja event järjestelmän demonstroimista varten, poistan myöhemmin.
{
    // Start is called before the first frame update
    void Start()
    {
        GameEventSystem.RegisterListener(Event_Type.DEBUG_COLOR_CHANGE, changeColor);
    }

    void changeColor(EventInfo info)
    {
        ColorChangeInfo colorChangeInfo = (ColorChangeInfo)info;
        gameObject.GetComponent<Renderer>().material.color = colorChangeInfo.newColor;
    }
}
