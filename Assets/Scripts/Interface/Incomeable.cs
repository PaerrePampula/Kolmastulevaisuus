using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public interface Incomeable
{
    float getSpeculatedNetIncomeInAMonth();
    float getNetIncomeInAMonth();
    float getGrossIncomeAmountInAMonth();
    bool AnExtra { get; set; }

}

