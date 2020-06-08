using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    [SerializeField]
    RandomEventScriptable tutorialStart;
    [SerializeField]
    List<RandomEventScriptable> flagEvents = new List<RandomEventScriptable>();
    private void OnEnable()
    {
        Flag.OnFlagFire += CheckForFlagEvents;
    }
    private void OnDisable()
    {
        Flag.OnFlagFire -= CheckForFlagEvents;
    }
    // Start is called before the first frame update
    void Start()
    {
        EventControl.RaiseAnEvent(tutorialStart);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void CheckForFlagEvents(Flag flag)
    {
        
        foreach (var flagEvent in flagEvents)
        {
            var Checkflag = flagEvent.neededFlags.SingleOrDefault(newflag => newflag.FlagName == flag.FlagName);
            if (Checkflag != null) EventControl.RaiseAnEvent(flagEvent);
        }
    }
}
