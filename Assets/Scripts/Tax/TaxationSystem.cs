using System.Linq;
using UnityEngine;

public static class TaxationSystem 
    //HUOM: PELAAJA EI TODENNÄKÖISESTI TULE MAKSAMAAN OLLENKAAN VEROA PIENIEN TULOJEN TAKIA, 
    //JOTEN TÄMÄN TILALLE TULEE VAIN PAKOLLISET MAKSUT JA 60% VEROT JOS PELAAJA EI HAE JA TOIMITA
    //VEROKORTTIA
    //VANHA KOODIN VEROLASKENTA EI OTA HUOMIOON VÄHENNYKSIÄ, KAIKEN TÄMÄN LISÄYS ENSTEKSI ON MELKO OVERKILL
    //OSA-AIKAISILLE OPISKELIJOILLE, SILLÄ NIILLÄ YLEENSÄ VEROKORTISSA LUKEE 0%
{
    #region Fields
    static float genericAverageMunincipalTax = 0.21f; //Ei varsinaisesti perustu mihinkään tietyn kunnan verotukseen, mutta on melko lähellä sitä, mitä useimmissa veronmäärä on.
    static float elakeVakuutusPaymentPercentage = ConfigFileReader.getValue("TyottomyysMaksu");
    static float tyottomyysVakuutusPayment = ConfigFileReader.getValue("ElakeMaksu");
    static float calculatedPlayerNetRate = 0f;
    #endregion


    public static float netPaymentPercentage()
    {

        //Pelaajalla on verokortti, palauta tieto.
        return calculatedPlayerNetRate;
    }
    public static float getIncomeafterMandatoryPayments(float allIncomesTotalGross)
    {

        float incomeAfterMandatoryPayments = allIncomesTotalGross * (1 - elakeVakuutusPaymentPercentage - tyottomyysVakuutusPayment);
        calculatedPlayerNetRate = (incomeAfterMandatoryPayments / allIncomesTotalGross);
        return incomeAfterMandatoryPayments;

    }

}
