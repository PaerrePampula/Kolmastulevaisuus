using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#region Enums
public enum FIRE_LOCATION //Tänne on määritelty gameEventeille ja niiden scriptableille käytettävät sijainnit, missä eventit firee
    //Näitä käytetään myös sijaintien emptyissä eventloceissa (ei siis siinä sijainnin visuaalisessa gameobjektissa)
{
    SCHOOL,
    WORK,
    CITY,
    HOME,
    ANY
}
#endregion