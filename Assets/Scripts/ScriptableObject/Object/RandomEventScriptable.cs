using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Uusi satunnainen event", menuName = "Satunnainen event tekstiboksi")]
public class RandomEventScriptable : ScriptableObject //Tämä on melko yksiselitteinen, lista stringejä ja dialogivalintoja, sekä scriptableactioneita
{
    public List<Flag> neededFlags;
    public PrereqPair[] Prerequisites;
    public FIRE_LOCATION[] fire_locations;
    public eventText[] eventTexts; //Eventexts on säiliö eventin kuvaustekstille, sekä sen valintabokseille
}
[System.Serializable]
public class eventText
{
    [TextArea (5, 15)]
    public string eventDialog; //kuvausteksti tapahtumasta

    public eventChoice[] eventDialogChoices; //kaikki valinnat, jota tapahtuman aikana voi tehdä.

}
[System.Serializable]
public class eventChoice
{
    public List<Flag> neededFlags;
    public PrereqPair[] Prerequisites;
    public int nextDialog; //-1 on poistuminen.
    public string choiceDescriptor;
    public ScriptableAction[] clickActions; //scriptableaction on tämänhetkinen implementointi tapahtumista, jotka tapahtuvat kun näppäintä painetaan.
    [Tooltip("Käytä tätä jos haluat luoda tiettyjä actioneita, jotka eivät vaadi valmiita assetteja.\n Hyvin alkeellinen eikä tue kaikkia actiontyyppejä")]
    public CustomAction[] customRunTimeActions;
    public randomChoiceCustomAction[] randomizedChoiceCustomActions;
    public Flag[] firedFlags;
    //Lisää tästä scriptableactionin alla.
    //Tälle on aivan varmasti parempikin ja selkeämpi tapa toteuttaa, mutta en toistaiseksi ole löytänyt /osannut tehdä sellaista.
}
[System.Serializable]
public class CustomAction
{

    public eventClassName eventClass;
    [Tooltip("FloatChange :\n" +
        "SimStat : Enumin nimi (Satisfaction, Comfortableness, Hunger, Ranking, Social, Study)\n"
        +"FlagFire : Flagin nimi\n"
        + "Purchase : Tuotteen nimi")]
    public string actionString1;
    public string actionString2;
    [Tooltip("FloatChange: Määrä\n"
        + "SimStat: Määrä\n"
        + "FlagFire: Mean time to happen \n"
        + "Purchase: Hinta")]
    public float actionFloat1;
    public float actionFloat2;
    [Tooltip ("Flag: Unique flag?")]
    public bool actionBool1;
    Event_Type event_Type;

    public void PerformAction()  //Super ugly mutta en osaa unity editorscriptingiä enkä näin pienen asian vuoksi ala opettelemaan.
    {

        switch (eventClass)
        {
            case eventClassName.FloatChange:
                FloatChangeInfo floatChangeInfo = new FloatChangeInfo();
                floatChangeInfo.changeofFloat = actionFloat1;
                event_Type = Event_Type.FLOAT_CHANGE;
                PerformActionWithEventInfo(event_Type, floatChangeInfo);

                break;
            case eventClassName.SimStat:
                SimStatType type = (SimStatType)System.Enum.Parse(typeof(SimStatType), actionString1, false);
                SimStatInfo simStatInfo = new SimStatInfo();
                simStatInfo.SimStatName = type;
                simStatInfo.StatChange = actionFloat1;
                event_Type = Event_Type.SIMSTAT_CHANGE;
                PerformActionWithEventInfo(event_Type, simStatInfo);
                break;
            case eventClassName.FlagFire:
                Flag flag = new Flag(actionString1, (int)actionFloat1, actionBool1);
                flag.FireFlag();

                break;
            case eventClassName.Purchase:
                PurchaseInfo purchaseInfo = new PurchaseInfo();
                purchaseInfo.purchaseCost = actionFloat1;
                purchaseInfo.purchaseName = actionString1;
                //event_Type = Event_Type.
                PerformActionWithEventInfo(event_Type, purchaseInfo);
                break;
            default:
                break;
        }

    }
    void PerformActionWithEventInfo(Event_Type eventType, EventInfo info)
    {
        GameEventSystem.DoEvent(
        eventType,
        info
        );
    }

}
[System.Serializable]
public class randomChoiceCustomAction
{

    public ParameteredCustomAction[] randomActions;
    public ParameteredCustomAction getParameteredCustomActionsByChance()
    {
        return NormalizedChanceGenerator.getSelection<ParameteredCustomAction>(randomActions);
    }
}
[System.Serializable]
public class ParameteredCustomAction : CustomAction, IChanceable
{
    public float baseChance;
    public SimStatType chanceIncreaseStat;
    public bool statEffectChanceIncreaseOn;
    public float getChance() //Jos bool on true, palauta arvo mukaanlukien stat kerroin, muuten palauta vaan base
    {
        return (statEffectChanceIncreaseOn) ?
            baseChance + (((baseChance / 3f) * (0.20f * PlayerDataHolder.Current.getStatByEnum(chanceIncreaseStat).StatFloat))) :
            baseChance;

    }
}