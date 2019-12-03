using BBAPricing.Models;
using SAPbouiCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBAPricing.FormControllers
{
    public class PricingController
    {
        private MasterBomModel MasterBomModel;
        private readonly IForm _form;
        public Grid _grid { get { return (Grid)_form.Items.Item("Item_0").Specific; } }

        public PricingController(MasterBomModel masterBomModel, IForm form)
        {
            _form = form;
            MasterBomModel = masterBomModel;
        }
    }
}
