using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAPbouiCOM;

namespace BBAPricing.Iterfaces
{
    public abstract class IFormController
    {
        public abstract void FillGridFromModel(Grid grid);
        public abstract void GetGridColumns();
        public abstract bool FillModelFromDb();
        public abstract void GenerateModel();
        public IForm Form { get; set; }

        public IFormController(IForm form)
        {
            Form = form;
        }

    }
}
