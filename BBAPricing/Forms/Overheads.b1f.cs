using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPbouiCOM.Framework;
using BBAPricing.Models;
using BBAPricing.FormControllers;

namespace BBAPricing.Forms
{
    [FormAttribute("BBAPricing.Forms.Overheads", "Forms/Overheads.b1f")]
    class Overheads : UserFormBase
    {
        public Overheads()
        {
            OverheadsController = new OverheadsController(UIAPIRawForm,null);
        }

        OverheadsController OverheadsController;
        /// <summary>
        /// Initialize components. Called by framework after form created.
        /// </summary>
        public override void OnInitializeComponent()
        {
            this.Grid0 = ((SAPbouiCOM.Grid)(this.GetItem("Item_0").Specific));
            this.OnCustomInitialize();

        }

        /// <summary>
        /// Initialize form event. Called by framework before form creation.
        /// </summary>
        public override void OnInitializeFormEvents()
        {
            this.VisibleAfter += new VisibleAfterHandler(this.Form_VisibleAfter);

        }

        private SAPbouiCOM.Grid Grid0;

        private void OnCustomInitialize()
        {

        }

        public void Refresh()
        {
            Grid0.DataTable.ExecuteQuery($"SELECT U_ComponentId as [Component], U_Corian as [Corian], U_Neolith as [Neolith], U_Furniture as [Furniture]," +
                                         $" U_Total as [Total] FROM [@RSM_OVERHEADS_R]");
           // Grid0.Item.Enabled = false;
        }

        private void Form_VisibleAfter(SAPbouiCOM.SBOItemEventArg pVal)
        {
            var calculationForm = SAPbouiCOM.Framework.Application.SBO_Application.Forms.ActiveForm;
            if (calculationForm.Title == "Overheads")
            {
               Refresh();
            }
        }
    }
}
