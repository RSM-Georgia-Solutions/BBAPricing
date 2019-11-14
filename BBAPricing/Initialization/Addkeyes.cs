using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAPbobsCOM;

namespace BBAPricing.Initialization
{
    public class Addkeyes :IRunnable
    {
        public void Run()
        {
            DiManager.AddKey(DiManager.Company,"RSM_ELEM", "ElemUnic", "Element", BoYesNoEnum.tYES);
            //DiManager.AddKey(DiManager.Company,"RSM_MBOM", "SqKey", "SalesQuotationDocEntry", BoYesNoEnum.tYES);
        }
    }
}
