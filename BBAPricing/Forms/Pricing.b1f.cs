using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPbobsCOM;
using SAPbouiCOM.Framework;
using BBAPricing.Models;
using SAPbouiCOM;
using BBAPricing.FormControllers;
using System.Globalization;

namespace BBAPricing.Forms
{
    [FormAttribute("BBAPricing.Forms.Pricing", "Forms/Pricing.b1f")]
    class Pricing : UserFormBase
    {
        PricingController _pricingConroller;
        public Pricing()
        {
            _pricingConroller = new PricingController(MasterBomModel, UIAPIRawForm);
        }

        public MasterBomModel MasterBomModel { get; set; }
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
            this.EditText7 = ((SAPbouiCOM.EditText)(this.GetItem("Item_16").Specific));
            this.StaticText6 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_17").Specific));
            this.EditText8 = ((SAPbouiCOM.EditText)(this.GetItem("Item_18").Specific));
            this.StaticText7 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_19").Specific));
            this.StaticText8 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_20").Specific));
            this.EditText9 = ((SAPbouiCOM.EditText)(this.GetItem("Item_21").Specific));
            this.ComboBox0 = ((SAPbouiCOM.ComboBox)(this.GetItem("Item_22").Specific));
            this.StaticText9 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_23").Specific));
            this.OnCustomInitialize();

        }

        /// <summary>
        /// Initialize form event. Called by framework before form creation.
        /// </summary>
        public override void OnInitializeFormEvents()
        {

        }

        private SAPbouiCOM.Grid Grid0;
        public void FillForm()
        {
            FillHeader();
            FillGridFromModel();
        }

        private void FillHeader()
        {
            EditText0.Value = MasterBomModel.CostCenter;
            EditText1.Value = MasterBomModel.SalesQuotationDocNum.ToString();
            EditText2.Value = MasterBomModel.ProjectCode;
            EditText4.Value = MasterBomModel.CreateDate.ToString("yyyyMMdd");
            EditText5.Value = MasterBomModel.OwnerCode;
            EditText7.Value = MasterBomModel.Version;
            EditText8.Value = MasterBomModel.ExchangeRateDate.ToString("yyyyMMdd");
            EditText9.Value = MasterBomModel.Rate.ToString();
            ComboBox0.Select(MasterBomModel.Currency.ToString(), BoSearchKey.psk_ByValue);
            EditText3.Value = MasterBomModel.SalesQuotationDocEntry.ToString();
            EditText0.Item.Enabled = false;
            EditText1.Item.Enabled = false;
            EditText2.Item.Enabled = false;
            EditText4.Item.Enabled = false;
            EditText5.Item.Enabled = false;
            EditText7.Item.Enabled = false;
            EditText3.Item.Left = 4000;
        }

        private void OnCustomInitialize()
        {
            CalculationMaterialsController.RefreshBom = FillGridFromModel;
            CalculationMachinaryResourcesController.RefreshBom = FillGridFromModel;
            while (ComboBox0.ValidValues.Count > 0)
            {
                ComboBox0.ValidValues.Remove(0, BoSearchKey.psk_Index);
            }
            Recordset rec = (Recordset)DiManager.Company.GetBusinessObject(BoObjectTypes.BoRecordset);
            rec.DoQuery($"select CurrCode,CurrName from OCRN");
            while (!rec.EoF)
            {
                ComboBox0.ValidValues.Add(rec.Fields.Item("CurrCode").Value.ToString(), rec.Fields.Item("CurrName").Value.ToString());
                rec.MoveNext();
            }
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

            var oldExchangeRateDate = MasterBomModel.ExchangeRateDate;
            var oldCurrency = MasterBomModel.Currency;
            var oldRate = MasterBomModel.Rate;
            var res = Grid0.DataTable.GetValue("Element", Grid0.GetDataTableRowIndex(rowindex)).ToString();
            MasterBomModel.ExchangeRateDate = DateTime.ParseExact(EditText8.Value, "yyyyMMdd", CultureInfo.InvariantCulture);
            MasterBomModel.Currency = ComboBox0.Value;
            MasterBomModel.Rate = double.Parse(EditText9.Value, CultureInfo.InvariantCulture);

            if (string.IsNullOrWhiteSpace(MasterBomModel.Code))
            {
                var result = MasterBomModel.Add();
            }

            else if (!string.IsNullOrWhiteSpace(MasterBomModel.Code)
                && (oldExchangeRateDate != MasterBomModel.ExchangeRateDate
                || oldCurrency != MasterBomModel.Currency
                || oldRate != MasterBomModel.Rate))
            {
                MasterBomModel.Version = (double.Parse(MasterBomModel.Version) + 1).ToString(CultureInfo.InvariantCulture);
                foreach (var row in MasterBomModel.Rows)
                {
                    row.Version = MasterBomModel.Version;
                }
                var result = MasterBomModel.Add();
            }

            if (res == "MTRLs")
            {
                CalculationMaterials calculationForm = new CalculationMaterials(MasterBomModel);
                calculationForm.Show();
            }
            if (res == "Machinery Resources")
            {
                CalculationResources calculationForm = new CalculationResources(MasterBomModel);
                calculationForm.Show();
            }
            if (res == "Human Resources")
            {
                HumanResourcesCalculation calculationForm = new HumanResourcesCalculation(MasterBomModel);
                calculationForm.Show();
            }
            if (res == "Administrative Overheads")
            {
                CalculateAdministrativeOverheads calculationForm = new CalculateAdministrativeOverheads(MasterBomModel);
                calculationForm.Show();
            }
            if (res == "Manufacturing Overheads")
            {
                CalculateManufacturingOverheads calculationForm = new CalculateManufacturingOverheads(MasterBomModel);
                calculationForm.Show();
            }
        }

        private EditText EditText4;
        private StaticText StaticText3;
        private StaticText StaticText4;
        private EditText EditText5;
        private StaticText StaticText5;
        private EditText EditText6;
        public void GetGridColumns()
        {
            string query = $@"SELECT TOP(0) U_Element [Element], 
               U_Cost [Cost], 
               U_Price [Price], 
               U_Margin [Margin], 
               U_FinalCustomerPrice [FinalCustomer Price], 
               U_Percent AS [%], 
               U_I AS [I], 
               U_II AS [II], 
               U_III AS [III]
        FROM [@RSM_MBOM_ROWS]
        WHERE U_ParentItemCode = '{MasterBomModel.ParentItem}'
              AND U_SalesQuotationDocEntry = '{MasterBomModel.SalesQuotationDocEntry}'
              AND U_Version =
        (
            SELECT MAX(U_Version)
            FROM [@RSM_MBOM]
            WHERE U_ParentItem = '{MasterBomModel.ParentItem}'
                  AND U_SalesQuotationDocEntry = '{MasterBomModel.ParentItem}'
        )";
            Grid0.DataTable.ExecuteQuery(query);
        }
        private void FillGridFromModel()
        {
            GetGridColumns();
            Recordset recSet = (Recordset)DiManager.Company.GetBusinessObject(BoObjectTypes.BoRecordset);
            recSet.DoQuery($"SELECT Code, U_Element FROM [@RSM_ELEM]");
            int i = 0;
            while (!recSet.EoF)
            {
                SAPbouiCOM.Framework.Application.SBO_Application.Forms.ActiveForm.Freeze(true);
                Grid0.DataTable.SetValue("Element", i, recSet.Fields.Item("U_Element").Value);
                SAPbouiCOM.Framework.Application.SBO_Application.Forms.ActiveForm.Freeze(false);
                Grid0.DataTable.SetValue("Cost", i, MasterBomModel.Rows[i].Cost);
                Grid0.DataTable.SetValue("Price", i, MasterBomModel.Rows[i].Price);
                Grid0.DataTable.SetValue("Margin", i, MasterBomModel.Rows[i].Margin);
                Grid0.DataTable.SetValue("FinalCustomer Price", i, MasterBomModel.Rows[i].FinalCustomerPrice);
                Grid0.DataTable.SetValue("I", i, MasterBomModel.Rows[i].I);
                Grid0.DataTable.SetValue("II", i, MasterBomModel.Rows[i].II);
                Grid0.DataTable.SetValue("III", i, MasterBomModel.Rows[i].III);
                i++;
                Grid0.DataTable.Rows.Add();
                recSet.MoveNext();
            }
            Grid0.Columns.Item("Element").Editable = false;
            Grid0.DataTable.Rows.Remove(Grid0.DataTable.Rows.Count - 1);
        }

        private EditText EditText7;
        private StaticText StaticText6;
        private EditText EditText8;
        private StaticText StaticText7;
        private StaticText StaticText8;
        private EditText EditText9;
        private ComboBox ComboBox0;
        private StaticText StaticText9;
    }
}
