using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BBAPricing.FormControllers;
using BBAPricing.Models;
using SAPbouiCOM;
using SAPbouiCOM.Framework;

namespace BBAPricing.Forms
{
    [FormAttribute("BBAPricing.Forms.CalculateAdministrativeOverheads", "Forms/CalculateAdministrativeOverheads.b1f")]
    class CalculateAdministrativeOverheads : UserFormBase
    {
        public CalculateAdministrativeOverheads()
        {
        }

        private CalculationAdministrativeOverheadsController AdministrativeOverheadsController;
        public CalculateAdministrativeOverheads(MasterBomModel masterBomModel)
        {
            AdministrativeOverheadsController = new CalculationAdministrativeOverheadsController(masterBomModel,UIAPIRawForm);
        }

        /// <summary>
        /// Initialize components. Called by framework after form created.
        /// </summary>
        public override void OnInitializeComponent()
        {
            this.StaticText0 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_0").Specific));
            this.EditText0 = ((SAPbouiCOM.EditText)(this.GetItem("Item_1").Specific));
            this.StaticText1 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_2").Specific));
            this.EditText1 = ((SAPbouiCOM.EditText)(this.GetItem("Item_3").Specific));
            this.StaticText2 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_4").Specific));
            this.EditText2 = ((SAPbouiCOM.EditText)(this.GetItem("Item_5").Specific));
            this.EditText3 = ((SAPbouiCOM.EditText)(this.GetItem("Item_6").Specific));
            this.OnCustomInitialize();

        }

        /// <summary>
        /// Initialize form event. Called by framework before form creation.
        /// </summary>
        public override void OnInitializeFormEvents()
        {
            this.VisibleAfter += new VisibleAfterHandler(this.Form_VisibleAfter);

        }

        private void Form_VisibleAfter(SBOItemEventArg pVal)
        {
            var calculationForm = SAPbouiCOM.Framework.Application.SBO_Application.Forms.ActiveForm;
            if (calculationForm.Title == "Administrative Overheads")
            {
                EditText3.Active = true;
                EditText3.Item.Left = 1000;
                EditText0.Item.Enabled = false;
                EditText1.Item.Enabled = false;
                EditText2.Item.Enabled = false;
                AdministrativeOverheadsController.CalculateAdministrativeOverheads();// TODO Increment Version
            }
        }

        private void OnCustomInitialize()
        {
        }

        private StaticText StaticText0;
        private EditText EditText0;
        private StaticText StaticText1;
        private EditText EditText1;
        private StaticText StaticText2;
        private EditText EditText2;
        private EditText EditText3;
    }
}
