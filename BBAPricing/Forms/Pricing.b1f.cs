using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPbobsCOM;
using SAPbouiCOM.Framework;
using BBAPricing.Models;
using SAPbouiCOM;
using BBAPricing.FormControllers;

namespace BBAPricing.Forms
{
    [FormAttribute("BBAPricing.Forms.Pricing", "Forms/Pricing.b1f")]
    class Pricing : UserFormBase
    {
        public Pricing()
        {
        }

        /// <summary>
        /// Initialize components. Called by framework after form created.
        /// </summary>
        public override void OnInitializeComponent()
        {
            this.Grid0 = ((SAPbouiCOM.Grid)(this.GetItem("Item_0").Specific));
            this.Grid0.ClickAfter += new SAPbouiCOM._IGridEvents_ClickAfterEventHandler(this.Grid0_ClickAfter);
            this.StaticText0 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_1").Specific));
            this.EditText0 = ((SAPbouiCOM.EditText)(this.GetItem("Item_2").Specific));
            this.StaticText1 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_3").Specific));
            this.EditText1 = ((SAPbouiCOM.EditText)(this.GetItem("Item_4").Specific));
            this.StaticText2 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_5").Specific));
            this.EditText2 = ((SAPbouiCOM.EditText)(this.GetItem("Item_6").Specific));
            this.EditText3 = ((SAPbouiCOM.EditText)(this.GetItem("Item_7").Specific));
            this.LinkedButton0 = ((SAPbouiCOM.LinkedButton)(this.GetItem("Item_8").Specific));
            this.Button0 = ((SAPbouiCOM.Button)(this.GetItem("Item_9").Specific));
            this.Button0.PressedAfter += new SAPbouiCOM._IButtonEvents_PressedAfterEventHandler(this.Button0_PressedAfter);
            this.EditText4 = ((SAPbouiCOM.EditText)(this.GetItem("Item_10").Specific));
            this.StaticText3 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_11").Specific));
            this.StaticText4 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_12").Specific));
            this.EditText5 = ((SAPbouiCOM.EditText)(this.GetItem("Item_13").Specific));
            this.StaticText5 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_14").Specific));
            this.EditText6 = ((SAPbouiCOM.EditText)(this.GetItem("Item_15").Specific));
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
        private MasterBomModel _model;
        public void Refresh(MasterBomModel model)
        {
            _model = model;
            EditText0.Value = model.CostCenter;
            EditText1.Value = model.SalesQuotationDocNum.ToString();
            EditText2.Value = model.ProjectCode;
            EditText4.Value = model.CreateDate.ToString("yyyyMMdd");
            EditText5.Value = model.OwnerCode;

            EditText3.Value = model.SalesQuotationDocEntry.ToString();
            EditText0.Item.Enabled = false;
            EditText1.Item.Enabled = false;
            EditText2.Item.Enabled = false;
            EditText4.Item.Enabled = false;
            EditText5.Item.Enabled = false;
            EditText3.Item.Left = 4000;
            RefreshGrid();
        }
        private void OnCustomInitialize()
        {
            CalculationMaterialsController.RefreshBom = RefreshGrid;
            CalculationResourcesController.RefreshBom = RefreshGrid;
        }

        private SAPbouiCOM.StaticText StaticText0;
        private SAPbouiCOM.EditText EditText0;
        private SAPbouiCOM.StaticText StaticText1;
        private SAPbouiCOM.EditText EditText1;
        private SAPbouiCOM.StaticText StaticText2;
        private SAPbouiCOM.EditText EditText2;
        private SAPbouiCOM.EditText EditText3;
        private SAPbouiCOM.LinkedButton LinkedButton0;

        private void Grid0_ClickAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            if (pVal.Row == -1)
            {
                return;
            }
            Grid0.Rows.SelectedRows.Clear();
            Grid0.Rows.SelectedRows.Add(pVal.Row);

        }

        private SAPbouiCOM.Button Button0;

        private void Button0_PressedAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            if (Grid0.Rows.SelectedRows.Count < 1)
            {
                return;
            }
            int rowindex = Grid0.Rows.SelectedRows.Item(0, BoOrderType.ot_RowOrder);
            var res = Grid0.DataTable.GetValue("Element", Grid0.GetDataTableRowIndex(rowindex)).ToString();
            if (res == "MTRLs")
            {
                CalculationMaterials calculationForm = new CalculationMaterials(_model);
                calculationForm.Show();
            }
            if (res == "Machinery Resources")
            {
                CalculationResources calculationForm = new CalculationResources(_model);
                calculationForm.Show();
            }
        }

        private EditText EditText4;
        private StaticText StaticText3;
        private StaticText StaticText4;
        private EditText EditText5;
        private StaticText StaticText5;
        private EditText EditText6;

        private void Form_VisibleAfter(SBOItemEventArg pVal)
        {
        }

        private void RefreshGrid()
        {
            Grid0.DataTable.ExecuteQuery($"SELECT U_Element [Element] , U_Cost [Cost], U_Price [Price], U_Margin [Margin], U_FinalCustomerPrice [FinalCustomer Price], U_Percent as [%], U_I as [I], U_II as [II], U_III as [III] FROM [@RSM_MBOM_ROWS] WHERE U_ParentItemCode = '{_model.ParentItem}' AND U_SalesQuotationDocEntry = {_model.SalesQuotationDocEntry}");
            Recordset recSet = (Recordset)DiManager.Company.GetBusinessObject(BoObjectTypes.BoRecordset);
            recSet.DoQuery($"SELECT Code, U_Element FROM [@RSM_ELEM]");
            int i = 0;
            while (!recSet.EoF)
            {             
                SAPbouiCOM.Framework.Application.SBO_Application.Forms.ActiveForm.Freeze(true);
                Grid0.DataTable.SetValue("Element", i, recSet.Fields.Item("U_Element").Value);
                SAPbouiCOM.Framework.Application.SBO_Application.Forms.ActiveForm.Freeze(false);
                i++;
                recSet.MoveNext();
            }
            Grid0.Columns.Item("Element").Editable = false;
        }
    }
}
