using UnityEngine;
using System.Collections;

public interface IActionable
{
    Event_Type thisEvent_Type { get; set; }
    string Description { get; set; }
    void PerformAction();
}
