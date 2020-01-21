using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using BBAPricing.Models;
using SAPbobsCOM;
using SAPbouiCOM;
using SAPbouiCOM.Framework;
using Application = SAPbouiCOM.Application;

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
            var productNo = UIAPIRawForm.DataSources.DBDataSources.Item("OITT").GetValue("Code", 0);
            var treeTypeString =  UIAPIRawForm.DataSources.DBDataSources.Item("OITT").GetValue("TreeType", 0);
            BoItemTreeTypes treeType = BoItemTreeTypes.iProductionTree;
            switch (treeTypeString)
            {
                case "P":
                    treeType = BoItemTreeTypes.iProductionTree;
                    break;
                case "A":
                    treeType = BoItemTreeTypes.iAssemblyTree;
                    break;
                case "S":
                    treeType = BoItemTreeTypes.iSalesTree;
                    break;
                case "T":
                    treeType = BoItemTreeTypes.iTemplateTree;
                    break;

            }
            var quantity = double.Parse(UIAPIRawForm.DataSources.DBDataSources.Item("OITT").GetValue("Qauntity", 0), CultureInfo.InvariantCulture);
            if (string.IsNullOrWhiteSpace(productNo))
            {
                SAPbouiCOM.Framework.Application.SBO_Application.MessageBox("მიუთითეთ BOM - ის კოდი");
                return;
            }
            SapBomModel sapBomModel = new SapBomModel();
            sapBomModel.BomType = treeType;
            sapBomModel.ProductNo = productNo;
            sapBomModel.Quantity = quantity;
            Import_Form activeForm = new Import_Form(sapBomModel);
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
