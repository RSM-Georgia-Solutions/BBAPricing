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
using Application = SAPbouiCOM.Framework.Application;

namespace BBAPricing.Forms
{
    [FormAttribute("BBAPricing.Forms.Pricing", "Forms/Pricing.b1f")]
    class Pricing : UserFormBase
    {
        public bool HasErrors { get; set; }
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
            this.StaticText10 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_24").Specific));
            this.EditText10 = ((SAPbouiCOM.EditText)(this.GetItem("Item_25").Specific));
            this.Button1 = ((SAPbouiCOM.Button)(this.GetItem("Item_26").Specific));
            this.Button1.PressedAfter += new SAPbouiCOM._IButtonEvents_PressedAfterEventHandler(this.Button1_PressedAfter);
            this.StaticText11 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_27").Specific));
            this.EditText11 = ((SAPbouiCOM.EditText)(this.GetItem("Item_28").Specific));
            this.Button2 = ((SAPbouiCOM.Button)(this.GetItem("Item_29").Specific));
            this.Button2.PressedAfter += new SAPbouiCOM._IButtonEvents_PressedAfterEventHandler(this.Button2_PressedAfter);
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
            EditText9.Value = MasterBomModel.Rate.ToString(CultureInfo.InvariantCulture);
            EditText10.Value = MasterBomModel.TotalSquareMeter.ToString(CultureInfo.InvariantCulture);
            EditText11.Value = MasterBomModel.ReferenceFeePercentage.ToString(CultureInfo.InvariantCulture);
            EditText6.Value = MasterBomModel.PriceForSquareMeter.ToString(CultureInfo.InvariantCulture);
            if (string.IsNullOrWhiteSpace(MasterBomModel.Currency))
            {
                ComboBox0.Select("GEL", BoSearchKey.psk_ByDescription);
            }
            else
            {
                ComboBox0.Select(MasterBomModel.Currency);
            }
            EditText3.Value = MasterBomModel.SalesQuotationDocEntry;
            EditText0.Item.Enabled = false;
            EditText1.Item.Enabled = false;
            EditText2.Item.Enabled = false;
            EditText4.Item.Enabled = false;
            EditText5.Item.Enabled = false;
            EditText7.Item.Enabled = false;
            EditText6.Item.Enabled = false;
            EditText3.Item.Left = 4000;
        }

        private void OnCustomInitialize()
        {
            CalculationMaterialsController.RefreshBom = FillForm;
            CalculationMachinaryResourcesController.RefreshBom = FillForm;
            CalculationHumanResourcesController.RefreshBom = FillForm;
            CalculationManufacturingOverheadsController.RefreshBom = FillForm;
            CalculationAdministrativeOverheadsController.RefreshBom = FillForm;
            CommonElements.RefreshBom = FillForm;
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
            var oldRaferenceFee = MasterBomModel.ReferenceFeePercentage;
            var oldTotalSquareMeter = MasterBomModel.TotalSquareMeter;
            var res = Grid0.DataTable.GetValue("Element", Grid0.GetDataTableRowIndex(rowindex)).ToString();
            MasterBomModel.ExchangeRateDate = DateTime.ParseExact(EditText8.Value, "yyyyMMdd", CultureInfo.InvariantCulture);
            MasterBomModel.Currency = ComboBox0.Value;
            MasterBomModel.Rate = double.Parse(EditText9.Value, CultureInfo.InvariantCulture);
            MasterBomModel.TotalSquareMeter = double.Parse(string.IsNullOrEmpty(EditText10.Value) ? "0" : EditText10.Value, CultureInfo.InvariantCulture);
            MasterBomModel.ReferenceFeePercentage = double.Parse(string.IsNullOrEmpty(EditText11.Value) ? "0" : EditText11.Value, CultureInfo.InvariantCulture);

            if (string.IsNullOrWhiteSpace(MasterBomModel.Code))
            {
                var result = MasterBomModel.Add();
            }

            else if (!string.IsNullOrWhiteSpace(MasterBomModel.Code) && (oldExchangeRateDate != MasterBomModel.ExchangeRateDate || oldCurrency != MasterBomModel.Currency
                || oldRate != MasterBomModel.Rate) || oldRaferenceFee != MasterBomModel.ReferenceFeePercentage || oldTotalSquareMeter != MasterBomModel.TotalSquareMeter)
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
            if (res == "Material OverHeads")
            {
                Recordset recForCmp =
                    (Recordset)DiManager.Company.GetBusinessObject(BoObjectTypes.BoRecordset);
                recForCmp.DoQuery($"select * from [@RSM_OVRHD_CLCB] Where U_Version = (SELECT MAX(convert (int, U_Version)) FROM [@RSM_OVRHD_CLCB])");
                Recordset recSet =
                    (Recordset)DiManager.Company.GetBusinessObject(BoObjectTypes.BoRecordset);
                string queryStr = $"SELECT * FROM [@RSM_RESOURCES] JOIN OITM ON OITM.ItemCode = U_ParentItemCode " +
                                  $"JOIN ORSC ON ORSC.VisResCode =  [@RSM_RESOURCES].U_resourcecode " +
                                  $" WHERE U_Version in (SELECT MAX(convert (int, U_Version)) FROM [@RSM_RESOURCES] JOIN ORSC ON ORSC.VisResCode =  [@RSM_RESOURCES].U_resourcecode  GROUP BY U_ParentItemCode,U_SalesQuotationDocEntry, ORSC.ResType having U_SalesQuotationDocEntry = {MasterBomModel.SalesQuotationDocEntry} " +
                                  $"AND U_ParentItemCode  = N'{MasterBomModel.ParentItem}' AND ORSC.ResType = 'L')" +
                                  $"AND U_SalesQuotationDocEntry = {MasterBomModel.SalesQuotationDocEntry} " +
                                  $"AND U_ParentItemCode  = N'{MasterBomModel.ParentItem}' AND ORSC.ResType = 'L'";
                recSet.DoQuery(queryStr);
                var type = recSet.Fields.Item("U_SBU").Value.ToString();
                if (string.IsNullOrWhiteSpace(type))
                {
                    Application.SBO_Application.SetStatusBarMessage($"SBU არ არის არჩეული საქონელი - N'{MasterBomModel.ParentItem}'",
                        BoMessageTime.bmt_Short,
                        true);
                    HasErrors = true;
                    return;
                }
                double requiredResource = 0;
                double unitCost = 0;
                switch (type)
                {
                    case "01":
                        unitCost = (double)recForCmp.Fields.Item("U_Corian").Value;
                        break;
                    case "02":
                        unitCost = (double)recForCmp.Fields.Item("U_Neolith").Value;
                        break;
                    case "03":
                        unitCost = (double)recForCmp.Fields.Item("U_Furniture").Value;
                        break;
                }
                if (unitCost <= 0)
                {
                    Application.SBO_Application.SetStatusBarMessage("შეავსეთ Overhead-ის პარამეტრები",
                         BoMessageTime.bmt_Short,
                         true);
                    HasErrors = true;
                    return;
                }
                if (MasterBomModel.TotalSquareMeter <= 0)
                {
                    Application.SBO_Application.SetStatusBarMessage("TotalSquareMeter არავალიდურია",
                        BoMessageTime.bmt_Short,
                        true);
                    HasErrors = true;
                    return;
                }
                HasErrors = false;
                var materialOverheadsCost = unitCost * MasterBomModel.TotalSquareMeter * MasterBomModel.Quantity;
                var mtrlLine = MasterBomModel.Rows.First(x => x.ElementID == "Material OverHeads");
                if (MasterBomModel.Currency != "GEL")
                {
                    materialOverheadsCost /= MasterBomModel.Rate;
                }
                if (mtrlLine.Cost != materialOverheadsCost)
                {
                    mtrlLine.Cost = materialOverheadsCost;
                    mtrlLine.Price = materialOverheadsCost;
                    mtrlLine.Margin = 0;
                    mtrlLine.FinalCustomerPrice = materialOverheadsCost;
                    if (mtrlLine.Cost == 0)
                    {
                        MasterBomModel.Update();
                    }
                    else
                    {
                        string version = (int.Parse(MasterBomModel.Version, CultureInfo.InvariantCulture) + 1).ToString();
                        MasterBomModel.Version = version;
                        foreach (var row in MasterBomModel.Rows)
                        {
                            row.Version = version;
                        }
                        MasterBomModel.Add();
                    }
                    FillForm();
                }
            }

            if (res == "SalaryFund")
            {
                Recordset recSet =
                     (Recordset)DiManager.Company.GetBusinessObject(BoObjectTypes.BoRecordset);
                recSet.DoQuery($"select U_SalaryFundPercent from OITM WHERE ItemCode = N'{MasterBomModel.ParentItem}'");
                double salaryFundPercent = (double)recSet.Fields.Item("U_SalaryFundPercent").Value;

                var SalaryFundlLine = MasterBomModel.Rows.First(x => x.ElementID == "SalaryFund");
                var humanResourcelLine = MasterBomModel.Rows.First(x => x.ElementID == "Human Resources");
                SalaryFundlLine.Cost = salaryFundPercent / 100 * humanResourcelLine.Cost;
                SalaryFundlLine.Margin = 0;
                SalaryFundlLine.FinalCustomerPrice = salaryFundPercent / 100 * humanResourcelLine.Cost;
                SalaryFundlLine.Price = salaryFundPercent / 100 * humanResourcelLine.Cost;
                MasterBomModel.Update();
                FillForm();
            }
            if (res == "Reference Fee")
            {
                var mBomReferenceFeeRow = MasterBomModel.Rows.First(x => x.ElementID == "Reference Fee" && x.ParentItemCode == MasterBomModel.ParentItem);
                var sumCostsExceptReferenceFee = MasterBomModel.Rows.Where(x => x.ElementID != "Reference Fee" && x.ElementID != "Totals" && x.ParentItemCode == MasterBomModel.ParentItem).Sum(x => x.FinalCustomerPrice);
                mBomReferenceFeeRow.Cost = Math.Round(sumCostsExceptReferenceFee * MasterBomModel.ReferenceFeePercentage / 100, 4); 
                mBomReferenceFeeRow.Price = 0;
                mBomReferenceFeeRow.Margin = 0;
                mBomReferenceFeeRow.FinalCustomerPrice = 0;
               
                FillForm();
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
        WHERE U_ParentItemCode = N'{MasterBomModel.ParentItem}'
              AND U_SalesQuotationDocEntry = '{MasterBomModel.SalesQuotationDocEntry}'
              AND U_Version =
        (
            SELECT MAX(convert (int, U_Version))
            FROM [@RSM_MBOM]
            WHERE U_ParentItem = N'{MasterBomModel.ParentItem}'
                  AND U_SalesQuotationDocEntry = N'{MasterBomModel.ParentItem}'
        )";
            Grid0.DataTable.ExecuteQuery(query);
        }
        private void FillGridFromModel()
        {
            GetGridColumns();
            Recordset recSet = (Recordset)DiManager.Company.GetBusinessObject(BoObjectTypes.BoRecordset);
            recSet.DoQuery($"SELECT * FROM [@RSM_ELEM]");
            int i = 0;
            while (!recSet.EoF)
            {
                UIAPIRawForm.Freeze(true);
                var elementId = recSet.Fields.Item("U_Element").Value.ToString();
                Grid0.DataTable.SetValue("Element", i, recSet.Fields.Item("U_Element").Value);
                Grid0.DataTable.SetValue("Cost", i, MasterBomModel.Rows.First(y => y.ElementID == elementId).Cost);
                Grid0.DataTable.SetValue("Price", i, MasterBomModel.Rows.First(y => y.ElementID == elementId).Price);
                Grid0.DataTable.SetValue("Margin", i, MasterBomModel.Rows.First(y => y.ElementID == elementId).Margin);
                Grid0.DataTable.SetValue("FinalCustomer Price", i, MasterBomModel.Rows.First(y => y.ElementID == elementId).FinalCustomerPrice);
                Grid0.DataTable.SetValue("%", i, MasterBomModel.Rows.First(y => y.ElementID == elementId).Percent);
                Grid0.DataTable.SetValue("I", i, MasterBomModel.Rows.First(y => y.ElementID == elementId).I);
                Grid0.DataTable.SetValue("II", i, MasterBomModel.Rows.First(y => y.ElementID == elementId).II);
                Grid0.DataTable.SetValue("III", i, MasterBomModel.Rows.First(y => y.ElementID == elementId).III);
                i++;
                Grid0.DataTable.Rows.Add();
                recSet.MoveNext();
                UIAPIRawForm.Freeze(false);
            }
            Grid0.Columns.Item("Element").Editable = false;
            Grid0.Columns.Item("Cost").Editable = false;
            Grid0.Columns.Item("Price").Editable = false;
            Grid0.Columns.Item("Margin").Editable = false;
            Grid0.Columns.Item("%").Editable = false;
            Grid0.Columns.Item("I").Editable = false;
            Grid0.Columns.Item("II").Editable = false;
            Grid0.Columns.Item("III").Editable = false;
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


        private StaticText StaticText10;
        private EditText EditText10;
        private Button Button1;

        public void UpdateModelFromForm()
        {
            for (int i = 0; i < Grid0.Rows.Count; i++)
            {
                var finalCustomerPrice = (double)Grid0.DataTable.GetValue("FinalCustomer Price", i);
                var element = Grid0.DataTable.GetValue("Element", i).ToString();
                MasterBomModel.Rows.FirstOrDefault(x => x.ElementID == element).FinalCustomerPrice = finalCustomerPrice;
            }
            MasterBomModel.TotalSquareMeter = double.Parse(EditText10.Value, CultureInfo.InvariantCulture);
            MasterBomModel.ReferenceFeePercentage = double.Parse(EditText11.Value, CultureInfo.InvariantCulture);
        }

        private void Button1_PressedAfter(object sboObject, SBOItemEventArg pVal)
        {
            try
            {
                UpdateModelFromForm();
                MasterBomModel.Update();
            }
            catch (Exception)
            {
                //not added yet
            }
        }

        private StaticText StaticText11;
        private EditText EditText11;
        private Button Button2;

        private void Button2_PressedAfter(object sboObject, SBOItemEventArg pVal)
        {
            var mBomTotals = MasterBomModel.Rows.First(x => x.ElementID == "Totals" && x.ParentItemCode == MasterBomModel.ParentItem);
            var sumPrice = Math.Round(MasterBomModel.Rows.Where(y => y.ElementID != "Totals").Sum(x => x.Price), 4);
            var sumMargin = Math.Round(MasterBomModel.Rows.Where(y => y.ElementID != "Totals").Sum(x => x.Margin), 4);
            var sumCost = Math.Round(MasterBomModel.Rows.Where(y => y.ElementID != "Totals").Sum(x => x.Cost), 4);
            var sumFinalCustomerPrice = Math.Round(MasterBomModel.Rows.Where(y => y.ElementID != "Totals").Sum(x => x.FinalCustomerPrice), 4);


            mBomTotals.Price = Math.Round(sumPrice, 4);
            mBomTotals.FinalCustomerPrice = Math.Round(sumFinalCustomerPrice, 4);
            mBomTotals.Margin = Math.Round(sumMargin, 4);
            mBomTotals.Cost = Math.Round(sumCost, 4);

            var mBomMtrlRow = MasterBomModel.Rows.First(x => x.ElementID == "MTRLs" && x.ParentItemCode == MasterBomModel.ParentItem);
            mBomMtrlRow.Percent = Math.Round(mBomMtrlRow.Cost / mBomTotals.Cost * 100, 4);

            var mBomSalaryFundRow = MasterBomModel.Rows.First(x => x.ElementID == "SalaryFund" && x.ParentItemCode == MasterBomModel.ParentItem);
            mBomSalaryFundRow.Percent = Math.Round(mBomSalaryFundRow.Cost / mBomTotals.Cost * 100, 4);

            var mBomMachinaryRow = MasterBomModel.Rows.First(x => x.ElementID == "Machinery Resources" && x.ParentItemCode == MasterBomModel.ParentItem);
            mBomMachinaryRow.Percent = Math.Round(mBomMachinaryRow.Cost / mBomTotals.Cost * 100, 4);

            var mBomHumanRow = MasterBomModel.Rows.First(x => x.ElementID == "Human Resources" && x.ParentItemCode == MasterBomModel.ParentItem);
            mBomHumanRow.Percent = Math.Round(mBomHumanRow.Cost / mBomTotals.Cost * 100, 4);

            var mBomManufacturingRow = MasterBomModel.Rows.First(x => x.ElementID == "Manufacturing Overheads" && x.ParentItemCode == MasterBomModel.ParentItem);
            mBomManufacturingRow.Percent = Math.Round(mBomManufacturingRow.Cost / mBomTotals.Cost * 100, 4);

            var mBomAdministrativeRow = MasterBomModel.Rows.First(x => x.ElementID == "Administrative Overheads" && x.ParentItemCode == MasterBomModel.ParentItem);
            mBomAdministrativeRow.Percent = Math.Round(mBomAdministrativeRow.Cost / mBomTotals.Cost * 100, 4);

            var mBomTripRow = MasterBomModel.Rows.First(x => x.ElementID == "Business Trip" && x.ParentItemCode == MasterBomModel.ParentItem);
            var mBomTransportRow = MasterBomModel.Rows.First(x => x.ElementID == "Transportation" && x.ParentItemCode == MasterBomModel.ParentItem);

            mBomTripRow.I = mBomTripRow.Margin / mBomTotals.Margin * 100;
            mBomTransportRow.I = mBomTransportRow.Margin / mBomTotals.Margin * 100;

            var mBomReferenceFeeRow = MasterBomModel.Rows.First(x => x.ElementID == "Reference Fee" && x.ParentItemCode == MasterBomModel.ParentItem);
            mBomReferenceFeeRow.Percent = Math.Round(mBomReferenceFeeRow.Cost / mBomTotals.Cost * 100, 4);

            mBomTripRow.II = mBomTripRow.Margin / mBomTripRow.FinalCustomerPrice * 100;
            if (mBomTransportRow.FinalCustomerPrice == 0)
            {
                Application.SBO_Application.MessageBox("Business Trip ხარჯები 0 ის ტოლია");
                return;
            }
            mBomTransportRow.II = mBomTransportRow.Margin / mBomTransportRow.FinalCustomerPrice * 100;



            mBomTripRow.III = mBomTripRow.Margin / mBomTotals.FinalCustomerPrice * 100;
            mBomTransportRow.III = mBomTransportRow.Margin / mBomTotals.FinalCustomerPrice * 100;

            mBomTripRow.Percent = Math.Round(mBomTripRow.Cost / mBomTotals.Cost * 100, 4);
            mBomTransportRow.Percent = Math.Round(mBomTransportRow.Cost / mBomTotals.Cost * 100, 4);


            var mBomMaterialOverHeadsRow = MasterBomModel.Rows.First(x => x.ElementID == "Material OverHeads" && x.ParentItemCode == MasterBomModel.ParentItem);
            mBomMaterialOverHeadsRow.Percent = Math.Round(mBomMaterialOverHeadsRow.Cost / mBomTotals.Cost * 100, 4);


            mBomTotals.Percent = MasterBomModel.Rows.Where(x => x.ElementID != "Totals" && x.ParentItemCode == MasterBomModel.ParentItem).Sum(x => x.Percent);
            var sumCosts = Math.Round(MasterBomModel.Rows.Where(y => y.ElementID != "Totals").Sum(x => x.Cost), 4);
            mBomTotals.Cost = Math.Round(sumCosts, 4);
            MasterBomModel.PriceForSquareMeter = sumFinalCustomerPrice / MasterBomModel.TotalSquareMeter;

            mBomMtrlRow.I = mBomMtrlRow.Margin / mBomTotals.Margin * 100;
            mBomSalaryFundRow.I = mBomSalaryFundRow.Margin / mBomTotals.Margin * 100;
            mBomMachinaryRow.I = mBomMachinaryRow.Margin / mBomTotals.Margin * 100;
            mBomHumanRow.I = mBomHumanRow.Margin / mBomTotals.Margin * 100;
            mBomAdministrativeRow.I = mBomAdministrativeRow.Margin / mBomTotals.Margin * 100;
            mBomMaterialOverHeadsRow.I = mBomMaterialOverHeadsRow.Margin / mBomTotals.Margin * 100;
           
            mBomManufacturingRow.I = mBomManufacturingRow.Margin / mBomTotals.Margin * 100;

            var sumI = Math.Round(MasterBomModel.Rows.Where(y => y.ElementID != "Totals").Sum(x => x.I), 4);
            mBomTotals.I = sumI;
            mBomMtrlRow.II = mBomMtrlRow.Margin / mBomMtrlRow.FinalCustomerPrice * 100;
            mBomSalaryFundRow.II = mBomSalaryFundRow.Margin / mBomSalaryFundRow.FinalCustomerPrice * 100;
            mBomMachinaryRow.II = mBomMachinaryRow.Margin / mBomMachinaryRow.FinalCustomerPrice * 100;
            mBomHumanRow.II = mBomHumanRow.Margin / mBomHumanRow.FinalCustomerPrice * 100;
            mBomAdministrativeRow.II = mBomAdministrativeRow.Margin / mBomAdministrativeRow.FinalCustomerPrice * 100;

            mBomMaterialOverHeadsRow.II = mBomMaterialOverHeadsRow.Margin / mBomMaterialOverHeadsRow.FinalCustomerPrice * 100;
            mBomManufacturingRow.II = mBomManufacturingRow.Margin / mBomManufacturingRow.FinalCustomerPrice * 100;
            var sumII = Math.Round(MasterBomModel.Rows.Where(y => y.ElementID != "Totals").Sum(x => x.II), 4);
            mBomTotals.II = sumII;
            mBomMtrlRow.III = mBomMtrlRow.Margin / mBomTotals.FinalCustomerPrice * 100;
            mBomSalaryFundRow.III = mBomSalaryFundRow.Margin / mBomTotals.FinalCustomerPrice * 100;
            mBomMachinaryRow.III = mBomMachinaryRow.Margin / mBomTotals.FinalCustomerPrice * 100;
            mBomHumanRow.III = mBomHumanRow.Margin / mBomTotals.FinalCustomerPrice * 100;
            mBomAdministrativeRow.III = mBomAdministrativeRow.Margin / mBomTotals.FinalCustomerPrice * 100;
            mBomMaterialOverHeadsRow.III = mBomMaterialOverHeadsRow.Margin / mBomTotals.FinalCustomerPrice * 100;            
            mBomManufacturingRow.III = mBomManufacturingRow.Margin / mBomTotals.FinalCustomerPrice * 100;
            var sumIII = Math.Round(MasterBomModel.Rows.Where(y => y.ElementID != "Totals").Sum(x => x.III), 4);
            mBomTotals.III = sumIII;
            string version = (int.Parse(MasterBomModel.Version, CultureInfo.InvariantCulture) + 1).ToString();
            MasterBomModel.Version = version;
            foreach (var row in MasterBomModel.Rows)
            {
                row.Version = version;
            }
            MasterBomModel.Add();
            FillForm();
        }
    }
}
