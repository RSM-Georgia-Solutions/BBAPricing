using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BBAPricing.Models;
using SAPbobsCOM;
using SAPbouiCOM.Framework;
using SAPbouiCOM;
using BBAPricing.FormControllers;

namespace BBAPricing.Forms
{
    [FormAttribute("BBAPricing.Forms.Calculation", "Forms/CalculationMaterials.b1f")]
    class CalculationMaterials : UserFormBase
    {
        public CalculationMaterials()
        {
        }


        CalculationMaterialsController _materialControler;
        public CalculationMaterials(MasterBomModel model)
        {
            _materialControler = new CalculationMaterialsController(model, this.UIAPIRawForm);
        }

        /// <summary>
        /// Initialize components. Called by framework after form created.
        /// </summary>
        public override void OnInitializeComponent()
        {
            this.Grid0 = ((SAPbouiCOM.Grid)(this.GetItem("Item_0").Specific));
            this.Button0 = ((SAPbouiCOM.Button)(this.GetItem("Item_1").Specific));
            this.Button0.PressedAfter += new SAPbouiCOM._IButtonEvents_PressedAfterEventHandler(this.Button0_PressedAfter);
            this.OnCustomInitialize();

        }

        /// <summary>
        /// Initialize form event. Called by framework before form creation.
        /// </summary>
        public override void OnInitializeFormEvents()
        {
            this.VisibleAfter += new VisibleAfterHandler(this.Form_VisibleAfter);
        } 
        private void OnCustomInitialize()
        {
        }

        private SAPbouiCOM.Grid Grid0;

        private void Form_VisibleAfter(SAPbouiCOM.SBOItemEventArg pVal)
        {
            var calculationForm = SAPbouiCOM.Framework.Application.SBO_Application.Forms.ActiveForm;
            if (calculationForm.Title == "Materials Calculation")
            {
                _materialControler.CalculateMaterials();
            }
        }

        private SAPbouiCOM.Button Button0;

        private void Button0_PressedAfter(object sboObject, SBOItemEventArg pVal)
        {
            _materialControler.UpdateMaterialsFromForm();
        }

        private void Button1_PressedAfter(object sboObject, SBOItemEventArg pVal)
        {
            _materialControler.UpdateMaterialsFormFromPriceList();
        }
    }
}
