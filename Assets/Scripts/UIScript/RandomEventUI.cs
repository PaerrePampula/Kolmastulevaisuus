using TMPro;
using UnityEngine;

public class RandomEventUI : MonoBehaviour //Toistaiseksi melko WIP ja makeshift, niin kommentoitu melko huonosti, kun etsin itekkin tälle järkevämpää pohjaa... :)
{
    #region Fields

    GameEvent gameEvent; //Ui eventin gameevent,josta haetaan tiedot

    [SerializeField]
    TextMeshProUGUI eventText; //Event-boksin tekstikomponentti tekstintäyttöä varten

    [SerializeField]
    Transform choiceContainer; //Event-boksin valintalaatikon transform

    eventText currentEventText; //String sisältö eventin kuvaukselle

    GameObject InstantiatedChoiceButton() => Instantiate(Resources.Load<GameObject>("ChoicePrototype"));
    #endregion
    #region MonobehaviourDefaults


    #endregion

    void dialogueData(int index = 0)
    {
        currentEventText = gameEvent.getData().eventTexts[index]; //Haetaan ensimmäinen event teksti käsiin.
        setTextToEvent(); //Sijoittaa ym. Tekstin UIseen
        populateChoiceContainer(); //Täyttää valintacontainerin valinnoilla
    }

    public void Init(GameEvent gameEvent, int index = 0)
    {
        this.gameEvent = gameEvent; //Asettaa boksille oikean eventtidatan.
        dialogueData(index);
    }
    public void populateChoiceContainer() //nimensä mukaan täyttää choicecontainerin button tyyppisillä valintanäppäimillä.
    {
        for (int i = 0; i < choiceContainer.childCount; i++)
        {
            Destroy(choiceContainer.GetChild(i).gameObject); //Tuhoaa jo valmiit lapset (synkkää...)
        }
        for (int i = 0; i < currentEventText.eventDialogChoices.Length; i++)
        {
            GameObject choice = InstantiatedChoiceButton();
            choice.GetComponent<ChoiceButton>().Init(currentEventText.eventDialogChoices[i],
                                                     choiceContainer,
                                                     currentEventText.eventDialogChoices[i].choiceDescriptor, this);//Haetaan toisesta metodista näppäin, jolle passataan se teksti, mitä halutaan valintanäppäimeen.

        }
    }

    public void AdvanceDialogTo(int index)
    {
        if (index < 0) //Jos seuraavan index on -1 tai alle, niin dialogista poistutaan.
        {
            Destroy(transform.gameObject);
            //Periaatteessa tänne voisi lisätä broadcastin tapahtumasta CAMERA_TURN, mutta
            //Voitaisiin toistaiseksi mahdollistaa perättäiset eventit sijainneissa, jos se onkin tarpeellista.
        }
        else
        {
            dialogueData(index); //Indeksin n dialogidata haetaan
        }
    }

    void setTextToEvent()
    {
        eventText.text = currentEventText.eventDialog; //Asetetaan tämän eventin teksti placeholder tekstin tilalle.
    }

}
