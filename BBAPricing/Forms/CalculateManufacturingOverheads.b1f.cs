using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BBAPricing.FormControllers;
using BBAPricing.Models;
using SAPbouiCOM.Framework;

namespace BBAPricing.Forms
{
    [FormAttribute("BBAPricing.Forms.CalculateManufacturingOverheads", "Forms/CalculateManufacturingOverheads.b1f")]
    class CalculateManufacturingOverheads : UserFormBase
    {
        public CalculateManufacturingOverheads()
        {
        }
        private CalculationManufacturingOverheadsController ManufacturingOverheadsController;
        public CalculateManufacturingOverheads(MasterBomModel masterBomModel)
        {
            ManufacturingOverheadsController = new CalculationManufacturingOverheadsController(masterBomModel, UIAPIRawForm);
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

        private SAPbouiCOM.StaticText StaticText0;

        private void OnCustomInitialize()
        {

        }

        private SAPbouiCOM.EditText EditText0;
        private SAPbouiCOM.StaticText StaticText1;
        private SAPbouiCOM.EditText EditText1;
        private SAPbouiCOM.StaticText StaticText2;
        private SAPbouiCOM.EditText EditText2;
        private SAPbouiCOM.EditText EditText3;

        private void Form_VisibleAfter(SAPbouiCOM.SBOItemEventArg pVal)
        {
            var calculationForm = SAPbouiCOM.Framework.Application.SBO_Application.Forms.ActiveForm;
            if (calculationForm.Title == "Manufacturing Overhead")
            {
                EditText3.Active = true;
                EditText3.Item.Left = 1000;
                EditText0.Item.Enabled = false;
                EditText1.Item.Enabled = false;
                EditText2.Item.Enabled = false;
                ManufacturingOverheadsController.CalculateAdministrativeOverheads();
            }
        }
    }
}
