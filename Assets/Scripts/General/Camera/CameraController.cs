using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    /*Tämän classin pointti on mm. käännellä kameraa sen jälkeen
      kun jokaisessa lokaatiossa on tehty joku päätös,
      vähän niinkuin samalla tavalla, miten asiakkaan
      alkuperäisessä inspispelissä
     */


    /// <summary>
    /// Kamerassa käytettyjä fieldejä
    /// </summary>
    public float rotationSpeedSlowdownFactor = 0.5f; //Osana kameran kääntämistä, isompi arvo tarkoittaa isompaa hidastusta kameran kääntönopeuteen, default on 0.5
    public bool turnRequest; //Tarvitaan boolina sitä varten, kun pelaaja pyytää kameran kääntämistä.
    Vector3 referenceRotation; //Tämä on pelaajan kameran kulma ennen kameran kääntämistä, asetetaan aina uudelleen kun kameran kääntämispyyntöä tehdään
    Vector3 endRotation; //Tämä on taas kameran kulma + 90 astetta y-akselilla. Asetetaan aina uudelleen, kun kameran kääntämispyyntöä tehdään
    float timelerped; //Tätä tarvitaan kameran lineaarisessa interpoloinnissa, lerpissä on se ongelma että se hidastuu kun lähestytään tarvittua rotaatiota, tämä korjaa sen niin, että tätä käytetään lerpin t muuttujassa osoittajana.
    private void Awake()
    {
        GameEventSystem.Current.RegisterListener(Event_Type.CAMERA_TURN, assignReferenceRotation);
    }
    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

        //Tämä on debugia varten. Jos painat näppäimistön R-näppäintä, classi kutsuu assignreferencerotationia.
        //Tämä ei tietenkään ole loppuversiossa ihan tämänkaltainen, kun kameran kääntäminen pitäisi varmaan sitoa siihen, kun jonkun tapahtuman valinta on valittu peliruuudulla...
        if (Input.GetKeyDown(KeyCode.R))
        {
            CameraAngleChangeInfo valueChangeAction = new CameraAngleChangeInfo();
            valueChangeAction.changeofFloat = 90;
            valueChangeAction.increments = 1;
            GameEventSystem.Current.DoEvent(
                Event_Type.CAMERA_TURN,
                valueChangeAction
                );
        }

        RequestATurn();        //Tämä pyörii kokoajan updaten sisällä, mutta erottelin sen omaan classiinsä koodin selkeyttämisen ja jäsentelyn vuoksi.
    }
    void RequestATurn() //Periaatteessa osa monobehaviourin Updatea edelleen, vaikka onkin eri classissa.
    {
        //Tämä bool asetetaan trueksi kun pelaaja painaa R-näppäintä.
        //Mitä boolin sisällä oleva komento tekee on se että kameran rotaatio asetetaan joka frame uudelleen niin, että rotaatio "slerpataan" = spherical interpolation, välillä alkurotaatio sekä loppurotaatio, riippuen ajasta
        //Alku ja loppurotaatio on muuten sama, mutta loppurotaatiossa kameraa käännetään y-akselin suhteen 90 astetta, eli tällä tavalla saadaan kameralle kääntyvyys joka neljälle "ilmansuunnalle".
        if (turnRequest == true)
        {
            timelerped += Time.deltaTime; //Joka framen jälkeen sen hetkinen ajan muutos lisätään timelerpediin, niin pysytään kärryillä kauan lerppiä on suoritettu.
            //Tässä lopulta käännetään kameraa, joka tapahtuu joka framella, kunnes kääntyminen on kokonaan suoritettu.
            //Kolmas parametri määrää kuinka nopeasti tämä tapahtuu (timelerped / rotationSpeedSlowdownFactor)...
            //Tätä ei voisi yksinkertaisesti muuttaa pelkällä transform.rotationin määrittelyllä, koska silloin muutos olisi välitön, eli melko ruman näköinen.
            transform.rotation = Quaternion.Slerp(Quaternion.Euler(referenceRotation.x, referenceRotation.y, referenceRotation.z), Quaternion.Euler(endRotation.x, endRotation.y, endRotation.z), timelerped / rotationSpeedSlowdownFactor);
        }
        if (transform.rotation == Quaternion.Euler(25, Quaternion.Euler(referenceRotation).eulerAngles.y + 90, 0))
        {
            turnRequest = false;
        }
    }
    void assignReferenceRotation(EventInfo info)
    {
        CameraAngleChangeInfo floatChangeInfo = (CameraAngleChangeInfo)info;
        referenceRotation = transform.rotation.eulerAngles;
        endRotation = Quaternion.Euler(referenceRotation.x, Mathf.Round(referenceRotation.y + floatChangeInfo.changeofFloat), referenceRotation.z).eulerAngles;
        turnRequest = true;
        timelerped = 0;
    }

}
