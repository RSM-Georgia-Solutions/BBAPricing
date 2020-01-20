using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPbouiCOM;
using SAPbouiCOM.Framework;

namespace BBAPricing.System_Forms
{
    [FormAttribute("672", "System Forms/BillOfMaterials.b1f")]
    class BillOfMaterials : SystemFormBase
    {
        public BillOfMaterials()
        {
        }

        /// <summary>
        /// Initialize components. Called by framework after form created.
        /// </summary>
        public override void OnInitializeComponent()
        {
            this.Button0 = ((SAPbouiCOM.Button)(this.GetItem("Item_0").Specific));
            this.Button0.ClickBefore += new SAPbouiCOM._IButtonEvents_ClickBeforeEventHandler(this.Button0_ClickBefore);
            this.Button0.PressedAfter += new SAPbouiCOM._IButtonEvents_PressedAfterEventHandler(this.Button0_PressedAfter);
            this.OnCustomInitialize();

        }

        /// <summary>
        /// Initialize form event. Called by framework before form creation.
        /// </summary>
        public override void OnInitializeFormEvents()
        {
            this.ClickAfter += new SAPbouiCOM.Framework.FormBase.ClickAfterHandler(this.Form_ClickAfter);
            this.ActivateAfter += new ActivateAfterHandler(this.Form_ActivateAfter);

        }

        private SAPbouiCOM.Button Button0;

        private void OnCustomInitialize()
        {
            if (UIAPIRawForm.Mode != BoFormMode.fm_ADD_MODE && UIAPIRawForm.Mode != BoFormMode.fm_UPDATE_MODE)
            {
                Button0.Item.Enabled = false;
            }
            else
            {
                Button0.Item.Enabled = true;
            }
        }

        private void Button0_PressedAfter(object sboObject, SBOItemEventArg pVal)
        {
            Import_Form activeForm = new Import_Form();
            activeForm.Show();
        }

        private void Form_ClickAfter(SBOItemEventArg pVal)
        {
            HandleItemsByMode();

        }



        private void Form_ActivateAfter(SBOItemEventArg pVal)
        {
            HandleItemsByMode();
        }

        private void HandleItemsByMode()
        {
            if (UIAPIRawForm.Mode != BoFormMode.fm_ADD_MODE && UIAPIRawForm.Mode != BoFormMode.fm_UPDATE_MODE && UIAPIRawForm.Mode != BoFormMode.fm_OK_MODE)
            {
                Button0.Item.Enabled = false;
            }
            else
            {
                Button0.Item.Enabled = true;
            }
        }

        private void Button0_ClickBefore(object sboObject, SBOItemEventArg pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            HandleItemsByMode();
        }
    }
}
