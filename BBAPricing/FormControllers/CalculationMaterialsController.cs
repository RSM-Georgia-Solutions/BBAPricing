using BBAPricing.Models;
using SAPbobsCOM;
using SAPbouiCOM;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BBAPricing.Iterfaces;
using Application = SAPbouiCOM.Framework.Application;

namespace BBAPricing.FormControllers
{
    public class CalculationMaterialsController : IFormController
    {
        public bool HasErrors { get; set; }
        private MasterBomModel _MasterBomModel;
        private readonly List<MaterialModel> _materialModelsList;
        private readonly IForm _form;
        public Grid _grid => (Grid)_form.Items.Item("Item_0").Specific;

        public CalculationMaterialsController(MasterBomModel masterBomModel, IForm form) : base(form)
        {
            _MasterBomModel = masterBomModel;
            _form = form;
            _materialModelsList = new List<MaterialModel>();
        }
        public override void FillGridFromModel(Grid grid)
        {
            SAPbouiCOM.Framework.Application.SBO_Application.Forms.ActiveForm.Freeze(true);
            GetGridColumns();
            for (int i = 0; i < _materialModelsList.Count; i++)
            {
                var materialModel = _materialModelsList[i];
                grid.DataTable.Rows.Add();
                grid.DataTable.SetValue("Component Code", i, materialModel.ComponentCode);
                grid.DataTable.SetValue("Component Name", i, materialModel.ComponentName);
                grid.DataTable.SetValue("Unit Of Measure", i, materialModel.UnitOfMeasure);
                grid.DataTable.SetValue("Unit Cost", i, materialModel.UnitCost);
                grid.DataTable.SetValue("Quantity", i, materialModel.Quantity);
                grid.DataTable.SetValue("Total Cost", i, materialModel.TotalCost);
                grid.DataTable.SetValue("Unit Working Price", i, materialModel.UnitWorkingPrice);
                grid.DataTable.SetValue("Currency", i, materialModel.Currency);
                grid.DataTable.SetValue("Converted Currency", i, materialModel.ConvertedCurrency);
                grid.DataTable.SetValue("Unit Retail Price", i, materialModel.UnitRetailPrice);
                grid.DataTable.SetValue("Discount Percentage", i, materialModel.DiscountPercentage);
                grid.DataTable.SetValue("Discount Amount", i, materialModel.DiscountAmount);
                grid.DataTable.SetValue("Shared Percentage", i, materialModel.SharedPercentage);
                grid.DataTable.SetValue("Shared Discount Amount", i, materialModel.SharedDiscountAmount);
                grid.DataTable.SetValue("Margin Percentage", i, materialModel.MarginPercentage);
                grid.DataTable.SetValue("Margin Amount", i, materialModel.MarginAmount);
                grid.DataTable.SetValue("Final Customer Price Total", i, materialModel.FinalCustomerPriceTotal);
                grid.DataTable.SetValue("Final Customer Price", i, materialModel.FinalCustomerPrice);
            }
            _grid.DataTable.Rows.Remove(_grid.DataTable.Rows.Count - 1);
            _grid.Columns.Item("Component Code").Editable = false;
            _grid.Columns.Item("Component Name").Editable = false;
            _grid.Columns.Item("Total Cost").Editable = false;
            _grid.Columns.Item("Discount Amount").Editable = false;
            _grid.Columns.Item("Shared Discount Amount").Editable = false;
            _grid.Columns.Item("Final Customer Price Total").Editable = false;
            _grid.Columns.Item("Final Customer Price").Editable = false;
            _grid.Columns.Item("Margin Amount").Editable = false;
            _grid.Columns.Item("Currency").Editable = false;
            _grid.Columns.Item("Unit Of Measure").Editable = false;
            _grid.Columns.Item("Quantity").Editable = false;
            _grid.Columns.Item("Unit Cost").Editable = false;
            _grid.Columns.Item("Unit Retail Price").Editable = false;
            _grid.Columns.Item("Converted Currency").Editable = false;
            _grid.Columns.Item("Discount Percentage").Editable = false;
            _grid.Columns.Item("Margin Percentage").Editable = false;
            _grid.Columns.Item("Shared Percentage").Editable = true;
            _grid.Columns.Item("Unit Working Price").Editable = true;
            SAPbouiCOM.Framework.Application.SBO_Application.Forms.ActiveForm.Freeze(false);
        }
        public override void GetGridColumns()
        {
            Form.Freeze(true);
            string queryGrid =
    $@"SELECT TOP(0) U_ComponentCode AS [Component Code], 
       U_ComponentName AS [Component Name], 
       U_UnitOFMeasure AS [Unit Of Measure], 
       U_Quantity AS [Quantity], 
       U_UnitCost AS [Unit Cost], 
       U_TotalCost AS [Total Cost], 
       U_UnitRetailPrice AS [Unit Retail Price], 
       U_UnitWorkingPrice AS [Unit Working Price], 
       U_Currency AS [Currency],  
	   U_ConvertedCurrency as [Converted Currency],
       U_DiscountPercentage AS [Discount Percentage], 
       U_DiscountAmount AS [Discount Amount], 
       U_SharedPercentage AS [Shared Percentage], 
       U_SharedDiscountAmount AS [Shared Discount Amount], 
       U_MarginPercentage AS [Margin Percentage], 
       U_MarginAmount AS [Margin Amount], 
       U_FinalCustomerPrice AS [Final Customer Price], 
       U_FinalCustomerPriceTotal AS [Final Customer Price Total]
FROM [@RSM_MTRL]";
            _grid.DataTable.ExecuteQuery(queryGrid);
            Form.Freeze(false);
        }

        private void InsertMaterialsListToDb()
        {
            foreach (var item in _materialModelsList)
            {
                var res = item.Add();
            }
            _MasterBomModel.Update();
        }

        private void InsertMaterialsListToDbNewForUpateButton()
        {
            foreach (var item in _materialModelsList)
            {
                var res = item.Add();
            }
        }
        public override bool FillModelFromDb()
        {
            Recordset recSet2 = (Recordset)DiManager.Company.GetBusinessObject(BoObjectTypes.BoRecordset);
            recSet2.DoQuery($@"SELECT * FROM [@RSM_MTRL]  WHERE U_ParentItemCode = '{_MasterBomModel.ParentItem}' AND U_SalesQuotationDocEntry = '{_MasterBomModel.SalesQuotationDocEntry}' AND U_Version = '{_MasterBomModel.Version}'");

            if (!recSet2.EoF)
            {
                while (!recSet2.EoF)
                {
                    MaterialModel materialModel = new MaterialModel();
                    materialModel.Quantity = (double)recSet2.Fields.Item("U_Quantity").Value;
                    materialModel.SharedPercentage = (double)recSet2.Fields.Item("U_SharedPercentage").Value;
                    materialModel.ComponentCode = (string)recSet2.Fields.Item("U_ComponentCode").Value;
                    materialModel.ComponentName = (string)recSet2.Fields.Item("U_ComponentName").Value;
                    materialModel.UnitOfMeasure = (string)recSet2.Fields.Item("U_UnitOfMeasure").Value;
                    materialModel.UnitCost = (double)recSet2.Fields.Item("U_UnitCost").Value;
                    materialModel.UnitRetailPrice = (double)recSet2.Fields.Item("U_UnitRetailPrice").Value;
                    materialModel.UnitRetailPrice = (double)recSet2.Fields.Item("U_UnitRetailPrice").Value;
                    materialModel.UnitWorkingPrice = (double)recSet2.Fields.Item("U_UnitWorkingPrice").Value;
                    materialModel.UnitWorkingPrice = (double)recSet2.Fields.Item("U_UnitWorkingPrice").Value;
                    materialModel.Currency = recSet2.Fields.Item("U_Currency").Value.ToString();
                    materialModel.ConvertedCurrency = recSet2.Fields.Item("U_ConvertedCurrency").Value.ToString();
                    materialModel.TotalCost = (double)recSet2.Fields.Item("U_TotalCost").Value;
                    materialModel.DiscountPercentage = (double)recSet2.Fields.Item("U_DiscountPercentage").Value;
                    materialModel.DiscountAmount = (double)recSet2.Fields.Item("U_DiscountAmount").Value;
                    materialModel.SharedDiscountAmount = (double)recSet2.Fields.Item("U_SharedDiscountAmount").Value;
                    materialModel.MarginPercentage = (double)recSet2.Fields.Item("U_MarginPercentage").Value;
                    materialModel.MarginAmount = (double)recSet2.Fields.Item("U_MarginAmount").Value;
                    materialModel.FinalCustomerPriceTotal = (double)recSet2.Fields.Item("U_FinalCustomerPriceTotal").Value;
                    materialModel.FinalCustomerPrice = (double)recSet2.Fields.Item("U_FinalCustomerPrice").Value;
                    materialModel.SalesQuotationDocEntry = recSet2.Fields.Item("U_SalesQuotationDocEntry").Value.ToString();
                    materialModel.ParentItemCode = recSet2.Fields.Item("U_ParentItemCode").Value.ToString();
                    materialModel.Version = recSet2.Fields.Item("U_Version").Value.ToString();
                    _materialModelsList.Add(materialModel);
                    recSet2.MoveNext();
                }
                return true;
            }
            return false;
        }

        private void FillModelFromGrid()
        {
            string version = _MasterBomModel.Version;
            _materialModelsList.Clear();
            double totalCost = 0;
            double totalPrice = 0;
            double totalMargin = 0;
            double totalFinalCustomerPrice = 0;
            for (int i = 0; i < _grid.DataTable.Rows.Count; i++)
            {
                MaterialModel materialModel = new MaterialModel();
                materialModel.ComponentCode = _grid.DataTable.GetValue("Component Code", i).ToString();
                materialModel.ComponentName = _grid.DataTable.GetValue("Component Name", i).ToString();
                materialModel.UnitOfMeasure = _grid.DataTable.GetValue("Unit Of Measure", i).ToString();
                materialModel.UnitCost = (double)_grid.DataTable.GetValue("Unit Cost", i);
                materialModel.UnitRetailPrice = (double)_grid.DataTable.GetValue("Unit Retail Price", i);
                materialModel.UnitWorkingPrice = (double)_grid.DataTable.GetValue("Unit Working Price", i);
                materialModel.Currency = _grid.DataTable.GetValue("Currency", i).ToString();
                materialModel.ConvertedCurrency = _grid.DataTable.GetValue("Currency", i).ToString();
                materialModel.Quantity = (double)_grid.DataTable.GetValue("Quantity", i);
                materialModel.SharedPercentage = (double)_grid.DataTable.GetValue("Shared Percentage", i);
                materialModel.DiscountPercentage = (double)_grid.DataTable.GetValue("Discount Percentage", i);

                if (_MasterBomModel.Currency == "GEL")
                {
                    var rate = DiManager.GetCurrencyRate(materialModel.Currency, _MasterBomModel.ExchangeRateDate, DiManager.Company);
                    if (_MasterBomModel.Currency != materialModel.Currency)
                    {
                        materialModel.UnitCost *= rate;
                        materialModel.UnitRetailPrice *= rate;
                        materialModel.UnitWorkingPrice *= rate;
                    }
                }
                else
                {
                    if (_MasterBomModel.Currency != materialModel.Currency)
                    {
                        materialModel.UnitCost /= _MasterBomModel.Rate;
                        materialModel.UnitRetailPrice /= _MasterBomModel.Rate;
                        materialModel.UnitWorkingPrice /= _MasterBomModel.Rate;
                    }
                }

                materialModel.TotalCost = Math.Round(materialModel.Quantity * materialModel.UnitCost, 4);

                ConvertPrices(materialModel);

                materialModel.Version = version;
                materialModel.SalesQuotationDocEntry = _MasterBomModel.SalesQuotationDocEntry;
                materialModel.ParentItemCode = _MasterBomModel.ParentItem;
                _materialModelsList.Add(materialModel);
                totalCost += materialModel.TotalCost;
                totalFinalCustomerPrice += materialModel.FinalCustomerPriceTotal;
                totalMargin += materialModel.MarginAmount;
                totalPrice += materialModel.UnitWorkingPrice;
                var mtrlLine = _MasterBomModel.Rows.First(x => x.ElementID == "MTRLs");
                mtrlLine.Cost = totalCost;
                mtrlLine.Price = totalPrice;
                mtrlLine.Margin = totalMargin;
                mtrlLine.FinalCustomerPrice = totalFinalCustomerPrice;
            }

        }
        public override void GenerateModel()
        {
            double totalCost = 0;
            double totalPrice = 0;
            double totalMargin = 0;
            double totalFinalCustomerPrice = 0;
            Recordset recSet = (Recordset)DiManager.Company.GetBusinessObject(BoObjectTypes.BoRecordset);
            string queryData = $@"SELECT Working.Code AS [ItemCode], 
       Working.ItemName, 
       Working.InvntryUom, 
       working.quantity, 
       Working.Price AS [Working Price], 
       Retail.Price AS [Retail Price], 
       Working.Currency,Type
FROM
(
    SELECT OITM.itemName,  ITT1.[Type],
           Code, 
           OITM.InvntryUom,
           CASE
               WHEN ITM1.Price = 0
               THEN CASE
                        WHEN ITM1.AddPrice1 = 0
                        THEN ITM1.AddPrice2
                        ELSE ITM1.AddPrice1
                    END
               ELSE ITM1.Price
           END AS Price, 
           ListNum, 
           ITT1.Quantity, 
		   CASE
               WHEN ITM1.Price = 0
               THEN CASE
                        WHEN ITM1.AddPrice1 = 0
                        THEN ITM1.Currency2
                        ELSE ITM1.Currency1
                    END
               ELSE ITM1.Currency
           END AS Currency 
           
    FROM ITT1
         JOIN OITM ON OITM.ItemCode = ITT1.Code
         JOIN ITM1 ON OITM.ItemCode = ITM1.ItemCode
         JOIN OPLN ON ITM1.PriceList = OPLN.ListNum
            WHERE Father = '{_MasterBomModel.ParentItem}' AND OPLN.ListName = '{Settings.WorkingPriceList}') Working INNER JOIN
                (SELECT CASE
               WHEN ITM1.Price = 0
               THEN CASE
                        WHEN ITM1.AddPrice1 = 0
                        THEN ITM1.AddPrice2
                        ELSE ITM1.AddPrice1
                    END
               ELSE ITM1.Price
           END AS Price,  Code FROM ITT1
                JOIN OITM on OITM.ItemCode = ITT1.Code
            JOIN ITM1 on OITM.ItemCode = ITM1.ItemCode
            JOIN OPLN on ITM1.PriceList = OPLN.ListNum
            WHERE Father = '{_MasterBomModel.ParentItem}' AND OPLN.ListName = '{Settings.RetailPriceList}') Retail on Working.Code = Retail.Code and type = 4";
            recSet.DoQuery(queryData);

            var bp = (BusinessPartners)DiManager.Company.GetBusinessObject(BoObjectTypes.oBusinessPartners);
            bp.GetByKey(_MasterBomModel.CardCode);
            double discount = 0;
            var discountstring = bp.UserFields.Fields.Item("U_SharedDiscount").Value.ToString();
            double.TryParse(discountstring, out discount);
            int i = 0;
            while (!recSet.EoF)
            {
                MaterialModel materialModel = new MaterialModel();
                materialModel.Quantity = (double)recSet.Fields.Item("Quantity").Value;
                materialModel.SharedPercentage = discount;
                materialModel.ComponentCode = (string)recSet.Fields.Item("ItemCode").Value;
                materialModel.ComponentName = (string)recSet.Fields.Item("ItemName").Value;
                materialModel.UnitOfMeasure = (string)recSet.Fields.Item("InvntryUom").Value;
                materialModel.UnitCost = (double)recSet.Fields.Item("Working Price").Value;
                materialModel.Currency = recSet.Fields.Item("Currency").Value.ToString();
                materialModel.UnitRetailPrice = (double)recSet.Fields.Item("Retail Price").Value;
                materialModel.UnitWorkingPrice = (double)recSet.Fields.Item("Working Price").Value;
                if (string.IsNullOrWhiteSpace(materialModel.Currency) || materialModel.UnitRetailPrice == 0 || materialModel.UnitWorkingPrice == 0)
                {
                    Application.SBO_Application.SetStatusBarMessage($"ფასი არ არის გაწერილი : საქონელი - {materialModel.ComponentCode } ",
                        BoMessageTime.bmt_Short,
                        true);
                    HasErrors = true;
                    return;
                }

                HasErrors = false;
                if (_MasterBomModel.Currency == "GEL")
                {
                    if (_MasterBomModel.Currency != materialModel.Currency)
                    {
                        var rate = DiManager.GetCurrencyRate(materialModel.Currency, _MasterBomModel.ExchangeRateDate, DiManager.Company);
                        materialModel.UnitCost *= rate;
                        materialModel.UnitRetailPrice *= rate;
                        materialModel.UnitWorkingPrice *= rate;
                    }
                }
                else
                {
                    if (_MasterBomModel.Currency != materialModel.Currency)
                    {
                        materialModel.UnitCost /= _MasterBomModel.Rate;
                        materialModel.UnitRetailPrice /= _MasterBomModel.Rate;
                        materialModel.UnitWorkingPrice /= _MasterBomModel.Rate;
                    }
                }
                ConvertPrices(materialModel);

                materialModel.SalesQuotationDocEntry = _MasterBomModel.SalesQuotationDocEntry;
                materialModel.ParentItemCode = _MasterBomModel.ParentItem;
                materialModel.ConvertedCurrency = _MasterBomModel.Currency;
                materialModel.Version = _MasterBomModel.Version;
                _materialModelsList.Add(materialModel);
                totalCost = totalCost += materialModel.TotalCost;
                totalPrice = totalPrice += materialModel.UnitWorkingPrice * materialModel.Quantity;
                totalMargin = totalMargin += materialModel.MarginAmount;
                totalFinalCustomerPrice = totalFinalCustomerPrice += materialModel.FinalCustomerPriceTotal;
                i++;
                recSet.MoveNext();
            }
            var mtrlLine = _MasterBomModel.Rows.First(x => x.ElementID == "MTRLs");
            mtrlLine.Cost = totalCost;
            mtrlLine.Price = totalPrice;
            mtrlLine.Margin = totalMargin;
            mtrlLine.FinalCustomerPrice = totalFinalCustomerPrice;
        }

        private void ConvertPrices(MaterialModel materialModel)
        {
            materialModel.DiscountPercentage = Math.Round(materialModel.UnitRetailPrice - materialModel.UnitWorkingPrice, 4) /
                                               materialModel.UnitRetailPrice * 100;
            materialModel.TotalCost = Math.Round(materialModel.Quantity * materialModel.UnitCost, 4);
            materialModel.DiscountAmount = Math.Round(materialModel.UnitRetailPrice * materialModel.Quantity *
                                                      materialModel.DiscountPercentage / 100, 4);
            materialModel.SharedDiscountAmount = Math.Round(materialModel.DiscountAmount * materialModel.SharedPercentage / 100, 4);
            materialModel.MarginPercentage = Math.Round((materialModel.DiscountAmount - materialModel.SharedDiscountAmount) /
                                                        (materialModel.UnitWorkingPrice * materialModel.Quantity +
                                                         materialModel.DiscountAmount - materialModel.SharedDiscountAmount) * 100, 4);
            materialModel.MarginAmount = Math.Round(materialModel.TotalCost / (1 - Math.Round(materialModel.MarginPercentage / 100, 4)),
                                             4) * Math.Round(materialModel.MarginPercentage / 100, 4);
            materialModel.FinalCustomerPriceTotal = materialModel.TotalCost + materialModel.MarginAmount;
            materialModel.FinalCustomerPrice = materialModel.FinalCustomerPriceTotal / materialModel.Quantity;

        }

        public void CalculateMaterials()
        {
            GetGridColumns();
            bool fromDb = FillModelFromDb();
            if (!fromDb)
            {
                GenerateModel();
                if (HasErrors)
                {
                    return;
                }
                FillGridFromModel(_grid);
                InsertMaterialsListToDb();
                RefreshBom.Invoke();
            }
            else
            {
                FillGridFromModel(_grid);
            }
        }
        public static Action RefreshBom;


        public void UpdateMaterialsFromForm()
        {
            string version = (int.Parse(_materialModelsList.First().Version, CultureInfo.InvariantCulture) + 1).ToString();
            _materialModelsList.Clear();
            _MasterBomModel.Version = version;
            FillModelFromGrid();
            foreach (var row in _MasterBomModel.Rows)
            {
                row.Version = version;
            }
            InsertMaterialsListToDbNewForUpateButton();
            _MasterBomModel.Add();
            FillGridFromModel(_grid);
            RefreshBom.Invoke();
        }
        public void UpdateMaterialsFormFromPriceList()
        {
            string version = (int.Parse(_materialModelsList.First().Version, CultureInfo.InvariantCulture) + 1).ToString();
            _materialModelsList.Clear();
            _MasterBomModel.Version = version;
            GenerateModel();
            foreach (var row in _MasterBomModel.Rows)
            {
                row.Version = version;
            }
            InsertMaterialsListToDb();
            _MasterBomModel.Add();
            FillGridFromModel(_grid);
            RefreshBom.Invoke();
        }
    }
}

