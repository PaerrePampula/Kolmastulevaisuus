using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RandomEventUI : MonoBehaviour //Toistaiseksi melko WIP ja makeshift, niin kommentoitu melko huonosti, kun etsin itekkin tälle järkevämpää pohjaa... :)
{
    GameEvent gameEvent;
    public TextMeshProUGUI eventText; //Boksin eventin teksti.
    public Transform choiceContainer;
    GameObject choiceButton; //valmis resurssi jota käytetään valintojen näppäimenä
    eventText currentEventText;

    public void setRandomEvent(GameEvent gameEvent)
    {
        this.gameEvent = gameEvent; //Asettaa boksille oikean eventtidatan (tekstit, valinnat).
    }
    public void populateChoiceContainer() //nimensä mukaan täyttää choicecontainerin button tyyppisillä valintanäppäimillä.
    {
        for (int i = 0; i < choiceContainer.childCount; i++)
        {
            Destroy(choiceContainer.GetChild(i).gameObject); //Tuhoaa jo valmiit lapset (synkkää...)
        }
        for (int i = 0; i < currentEventText.eventDialogChoices.Length; i++)
        {
            GameObject choice = instantiatedChoiceButton(currentEventText.eventDialogChoices[i].choiceDescriptor);
            choice.GetComponent<ChoiceButton>().setButtonEventChoice(currentEventText.eventDialogChoices[i]);//Haetaan toisesta metodista näppäin, jolle passataan se teksti, mitä halutaan valintanäppäimeen.
            choice.transform.SetParent(choiceContainer);
        }
    }
    GameObject instantiatedChoiceButton(string appliedTextToChoice) //Luo jokaiselle näppäimen pyynnölle sen varsinaisen näppäimen. argumentti 1 on scriptableobjectissa määritelty valinnan string.
    {
        GameObject button = Instantiate(Resources.Load<GameObject>("ChoicePrototype"));
        ChoiceButton choiceButton = button.GetComponent<ChoiceButton>();
        choiceButton.setChoiceText(appliedTextToChoice);

        button.GetComponent<ChoiceButton>().setButtonGrandParentScript(this);
        return button;
    }
    public void AdvanceDialogTo(int index)
    {
        if (index < 0) //Jos seuraavan index on -1 tai alle, niin dialogista poistutaan.
        {
            Destroy(transform.gameObject);
            PointAndClickMovement.setMovementStatus(true);
            //Periaatteessa tänne voisi lisätä broadcastin tapahtumasta CAMERA_TURN, mutta
            //Voitaisiin toistaiseksi mahdollistaa perättäiset eventit sijainneissa, jos se onkin tarpeellista.
        }
        else
        {
            currentEventText = gameEvent.getData().eventTexts[index];
            setTextToEvent();
            populateChoiceContainer();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        currentEventText = gameEvent.getData().eventTexts[0]; //Haetaan ensimmäinen event teksti ja asetetaan sen tämänhetkiseksi tekstivalinnaksi.
        setTextToEvent();
        populateChoiceContainer();

    }
    void setTextToEvent()
    {
        eventText.text = currentEventText.eventDialog; //Asetetaan tämän eventin teksti placeholder tekstin tilalle.
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
