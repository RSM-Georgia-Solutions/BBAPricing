using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPbouiCOM.Framework;
using BBAPricing.Models;
using BBAPricing.FormControllers;

namespace BBAPricing.Forms
{
    [FormAttribute("BBAPricing.Forms.CalculationResources", "Forms/CalculationResources.b1f")]
    class CalculationResources : UserFormBase
    {
        public CalculationResources()
        {
        }

        CalculationMachinaryResourcesController _calculateResourcesController;

        public CalculationResources(MasterBomModel model)
        {
            _calculateResourcesController = new CalculationMachinaryResourcesController(model, UIAPIRawForm);
        }

        /// <summary>
        /// Initialize components. Called by framework after form created.
        /// </summary>
        public override void OnInitializeComponent()
        {
            this.Grid0 = ((SAPbouiCOM.Grid)(this.GetItem("Item_0").Specific));
            this.Button0 = ((SAPbouiCOM.Button)(this.GetItem("Item_1").Specific));
            this.Button0.PressedAfter += new SAPbouiCOM._IButtonEvents_PressedAfterEventHandler(this.Button0_PressedAfter);
            this.Button1 = ((SAPbouiCOM.Button)(this.GetItem("Item_2").Specific));
            this.Button1.PressedAfter += new SAPbouiCOM._IButtonEvents_PressedAfterEventHandler(this.Button1_PressedAfter);
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

        private void Form_VisibleAfter(SAPbouiCOM.SBOItemEventArg pVal)
        {
            var calculationForm = Application.SBO_Application.Forms.ActiveForm;
            if (calculationForm.Title == "Machinary Resources Calculation")
            {
                _calculateResourcesController.CalculateResources();
            }
        }

        private SAPbouiCOM.Button Button0;

        private void Button0_PressedAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
           _calculateResourcesController.UpdateResources();
        }

        private SAPbouiCOM.Button Button1;

        private void Button1_PressedAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            _calculateResourcesController.UpdateMachinaryResourcesFromForm();
        }
    }
}
