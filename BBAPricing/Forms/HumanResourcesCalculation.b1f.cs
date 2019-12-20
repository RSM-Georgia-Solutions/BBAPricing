﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BBAPricing.FormControllers;
using BBAPricing.Models;
using SAPbouiCOM.Framework;

namespace BBAPricing.Forms
{
    [FormAttribute("BBAPricing.Forms.HumanResourcesCalculation", "Forms/HumanResourcesCalculation.b1f")]
    class HumanResourcesCalculation : UserFormBase
    {
        CalculationHumanResourcesController calculationHumanResourcesController;
        public HumanResourcesCalculation(MasterBomModel masterBomModel)
        {
             calculationHumanResourcesController = new CalculationHumanResourcesController(masterBomModel,UIAPIRawForm);
        }

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

        private void Form_VisibleAfter(SAPbouiCOM.SBOItemEventArg pVal)
        {
            var calculationForm = Application.SBO_Application.Forms.ActiveForm;
            if (calculationForm.Title == "Human Resources Calculation")
            {
                calculationHumanResourcesController.CalculateResources();
            }
        }
    }
}
