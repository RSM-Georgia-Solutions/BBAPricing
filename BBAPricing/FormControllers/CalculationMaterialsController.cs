using BBAPricing.Models;
using SAPbobsCOM;
using SAPbouiCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBAPricing.FormControllers
{
    public class CalculationMaterialsController
    {
        private MasterBomModel _MasterBomModel;
        private readonly List<MaterialModel> _materialModelsList;
        private readonly IForm _form;
        public Grid _grid { get { return (Grid)_form.Items.Item("Item_0").Specific; } }
        public CalculationMaterialsController(MasterBomModel masterBomModel, IForm form)
        {
            _form = form;
            _MasterBomModel = masterBomModel;
            _materialModelsList = new List<MaterialModel>();
        }
        public void FillGridFromModel(Grid grid)
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
                grid.DataTable.SetValue("Unit Retail Price", i, materialModel.UnitRetailPrice);
                grid.DataTable.SetValue("Discount Percentage", i, materialModel.DiscountPercentage);
                grid.DataTable.SetValue("Discount Amount", i, materialModel.DiscountAmount);
                grid.DataTable.SetValue("Shared Percentage", i, materialModel.SharedDiscountPercentage);
                grid.DataTable.SetValue("Shared Discount Amount", i, materialModel.SharedDiscountAmount);
                grid.DataTable.SetValue("Margin Percentage", i, materialModel.MarginPercentage);
                grid.DataTable.SetValue("Margin Amount", i, materialModel.MarginAmount);
                grid.DataTable.SetValue("Final Customer Price Total", i, materialModel.FinalCustomerPriceTotal);
                grid.DataTable.SetValue("Final Customer Price", i, materialModel.FinalCustomerPrice);
            }
            SAPbouiCOM.Framework.Application.SBO_Application.Forms.ActiveForm.Freeze(false);
            _grid.DataTable.Rows.Remove(_grid.DataTable.Rows.Count - 1);
            _grid.Columns.Item("Component Code").Editable = false;
            _grid.Columns.Item("Component Name").Editable = false;
            _grid.Columns.Item("Total Cost").Editable = false;
            _grid.Columns.Item("Discount Amount").Editable = false;
            _grid.Columns.Item("Shared Discount Amount").Editable = false;
            _grid.Columns.Item("Final Customer Price Total").Editable = false;
            _grid.Columns.Item("Final Customer Price").Editable = false;
            _grid.Columns.Item("Margin Amount").Editable = false;
        }
        public void GetGridColumns()
        {
            string queryGrid =
    $@"SELECT top(0) U_ComponentCode as [Component Code], U_ComponentName as [Component Name], U_UnitOFMeasure as [Unit Of Measure], U_Quantity as [Quantity],
            U_UnitCost as [Unit Cost], U_TotalCost as [Total Cost], U_UnitRetailPrice as [Unit Retail Price],U_UnitWorkingPrice as [Unit Working Price],
            U_DiscountPercentage as [Discount Percentage], U_DiscountAmount as [Discount Amount], U_SharedPercentage as [Shared Percentage],
                U_SharedDiscountAmount as [Shared Discount Amount], U_MarginPercentage as [Margin Percentage], U_MarginAmount as [Margin Amount],
                U_FinalCustomerPrice as [Final Customer Price], U_FinalCustomerPriceTotal as [Final Customer Price Total]
            FROM [@RSM_MTRL]";
            _grid.DataTable.ExecuteQuery(queryGrid);
        }
        public void InsertListToDb()
        {
            foreach (var item in _materialModelsList)
            {
                var res = item.Add();
            }
        }
        public bool FillModelFromDb()
        {
            GetGridColumns();
            Recordset recSet2 = (Recordset)DiManager.Company.GetBusinessObject(BoObjectTypes.BoRecordset);
            recSet2.DoQuery($@"SELECT * FROM [@RSM_MTRL] WHERE U_ParentItemCode = '{_MasterBomModel.ParentItem}' AND U_SalesQuotationDocEntry = '{_MasterBomModel.SalesQuotationDocEntry}' AND U_Version = (SELECT MAX(U_Version)
                      FROM [@RSM_MTRL]
                      WHERE U_ParentItemCode = '{_MasterBomModel.ParentItem}'
                            AND U_SalesQuotationDocEntry = '{_MasterBomModel.SalesQuotationDocEntry}')");

            if (!recSet2.EoF)
            {
                while (!recSet2.EoF)
                {
                    MaterialModel materialModel = new MaterialModel();
                    materialModel.Quantity = (double)recSet2.Fields.Item("U_Quantity").Value;
                    materialModel.DiscountPercentage = (double)recSet2.Fields.Item("U_SharedPercentage").Value;
                    materialModel.ComponentCode = (string)recSet2.Fields.Item("U_ComponentCode").Value;
                    materialModel.ComponentName = (string)recSet2.Fields.Item("U_ComponentName").Value;
                    materialModel.UnitOfMeasure = (string)recSet2.Fields.Item("U_UnitOfMeasure").Value;
                    materialModel.UnitCost = (double)recSet2.Fields.Item("U_UnitCost").Value;
                    materialModel.UnitRetailPrice = (double)recSet2.Fields.Item("U_UnitRetailPrice").Value;
                    materialModel.UnitWorkingPrice = (double)recSet2.Fields.Item("U_UnitWorkingPrice").Value;
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
        public void FillModelFromGrid()
        {
            string version = (int.Parse(_MasterBomModel.Version) + 1).ToString();
            _materialModelsList.Clear();   
            for (int i = 0; i < _grid.DataTable.Rows.Count; i++)
            {
                MaterialModel materialModel = new MaterialModel();
                materialModel.ComponentCode = _grid.DataTable.GetValue("Component Code", i).ToString();
                materialModel.ComponentName = _grid.DataTable.GetValue("Component Name", i).ToString();
                materialModel.UnitOfMeasure = _grid.DataTable.GetValue("Unit Of Measure", i).ToString();
                materialModel.UnitCost = (double)_grid.DataTable.GetValue("Unit Cost", i);
                materialModel.UnitRetailPrice = (double)_grid.DataTable.GetValue("Unit Retail Price", i);
                materialModel.UnitWorkingPrice = (double)_grid.DataTable.GetValue("Unit Working Price", i);
                materialModel.Quantity = (double)_grid.DataTable.GetValue("Quantity", i);
                materialModel.TotalCost = Math.Round(materialModel.Quantity * materialModel.UnitCost, 4);
                materialModel.SharedDiscountPercentage = (double)_grid.DataTable.GetValue("Shared Percentage", i);
                materialModel.DiscountPercentage = (double)_grid.DataTable.GetValue("Discount Percentage", i);
                materialModel.DiscountAmount = Math.Round((materialModel.UnitRetailPrice * materialModel.Quantity *
                                                materialModel.DiscountPercentage) / 100, 4);
                materialModel.SharedDiscountAmount = Math.Round((materialModel.DiscountAmount * materialModel.SharedDiscountPercentage) / 100, 4);
                materialModel.MarginPercentage = Math.Round((materialModel.DiscountAmount - materialModel.SharedDiscountAmount) /
                                               (materialModel.UnitWorkingPrice * materialModel.Quantity +
                                                materialModel.DiscountAmount - materialModel.SharedDiscountAmount) * 100, 4);
                materialModel.MarginAmount = (Math.Round(materialModel.TotalCost / (1 - Math.Round(materialModel.MarginPercentage / 100, 4)), 4) * (Math.Round(materialModel.MarginPercentage / 100, 4)));
                materialModel.FinalCustomerPriceTotal = materialModel.TotalCost + materialModel.MarginAmount;
                materialModel.FinalCustomerPrice = materialModel.FinalCustomerPriceTotal / materialModel.Quantity;
                materialModel.Version = version;
                materialModel.SalesQuotationDocEntry = _MasterBomModel.SalesQuotationDocEntry;
                materialModel.ParentItemCode = _MasterBomModel.ParentItem;
                _materialModelsList.Add(materialModel);
            }
        }
        public void GenerateModel()
        {
            GetGridColumns();
            double totalCost = 0;
            double totalPrice = 0;
            double totalMargin = 0;
            double totalFinalCustomerPrice = 0;
            Recordset recSet = (Recordset)DiManager.Company.GetBusinessObject(BoObjectTypes.BoRecordset);
            string queryData = $@"SELECT Working.Code as [ItemCode], Working.ItemName, Working.InvntryUom, working.quantity, Working.Price as [Working Price], Retail.Price as [Retail Price] FROM (
            SELECT OITM.itemName, Code, OITM.InvntryUom, ITM1.Price, ListNum, ITT1.Quantity FROM ITT1
                JOIN OITM on OITM.ItemCode = ITT1.Code
            JOIN ITM1 on OITM.ItemCode = ITM1.ItemCode
            JOIN OPLN on ITM1.PriceList = OPLN.ListNum
            WHERE Father = '{_MasterBomModel.ParentItem}' AND OPLN.ListName = 'Unit Working Price') Working INNER JOIN
                (SELECT ITM1.Price, Code FROM ITT1
                JOIN OITM on OITM.ItemCode = ITT1.Code
            JOIN ITM1 on OITM.ItemCode = ITM1.ItemCode
            JOIN OPLN on ITM1.PriceList = OPLN.ListNum
            WHERE Father = '{_MasterBomModel.ParentItem}' AND OPLN.ListName = 'Unit Retail Price') Retail on Working.Code = Retail.Code";
            recSet.DoQuery(queryData);

            var bp = (BusinessPartners)DiManager.Company.GetBusinessObject(BoObjectTypes.oBusinessPartners);
            bp.GetByKey(_MasterBomModel.CardCode);
            var discount = (double)bp.UserFields.Fields.Item("U_SharedDiscount").Value;
            while (!recSet.EoF)
            {
                MaterialModel materialModel = new MaterialModel();
                materialModel.Quantity = (double)recSet.Fields.Item("Quantity").Value;
                materialModel.SharedDiscountPercentage = discount;
                materialModel.ComponentCode = (string)recSet.Fields.Item("ItemCode").Value;
                materialModel.ComponentName = (string)recSet.Fields.Item("ItemName").Value;
                materialModel.UnitOfMeasure = (string)recSet.Fields.Item("InvntryUom").Value;
                materialModel.UnitCost = (double)recSet.Fields.Item("Working Price").Value;
                materialModel.UnitRetailPrice = (double)recSet.Fields.Item("Retail Price").Value;
                materialModel.UnitWorkingPrice = (double)recSet.Fields.Item("Working Price").Value;
                materialModel.TotalCost = Math.Round(materialModel.Quantity * materialModel.UnitCost, 4);
                materialModel.DiscountPercentage = (Math.Round(materialModel.UnitRetailPrice - materialModel.UnitWorkingPrice, 4) /
                                                 materialModel.UnitRetailPrice * 100);
                materialModel.DiscountAmount = Math.Round((materialModel.UnitRetailPrice * materialModel.Quantity *
                                                materialModel.DiscountPercentage) / 100, 4);
                materialModel.SharedDiscountAmount =
                    Math.Round((materialModel.DiscountAmount * materialModel.SharedDiscountPercentage) / 100, 4);

                materialModel.MarginPercentage = Math.Round((materialModel.DiscountAmount - materialModel.SharedDiscountAmount) /
                                               (materialModel.UnitWorkingPrice * materialModel.Quantity +
                                                materialModel.DiscountAmount - materialModel.SharedDiscountAmount) * 100, 4);

                materialModel.MarginAmount = (Math.Round(materialModel.TotalCost / (1 - Math.Round(materialModel.MarginPercentage / 100, 4)), 4) * (Math.Round(materialModel.MarginPercentage / 100, 4)));
                materialModel.FinalCustomerPriceTotal = materialModel.TotalCost + materialModel.MarginAmount;
                materialModel.FinalCustomerPrice = materialModel.FinalCustomerPriceTotal / materialModel.Quantity;
                materialModel.SalesQuotationDocEntry = _MasterBomModel.SalesQuotationDocEntry;
                materialModel.ParentItemCode = _MasterBomModel.ParentItem;
                materialModel.Version = "1";
                _materialModelsList.Add(materialModel);
                totalCost = totalCost += materialModel.TotalCost;
                totalPrice = totalPrice += materialModel.UnitWorkingPrice;
                totalMargin = totalMargin += materialModel.MarginAmount;
                totalFinalCustomerPrice = totalFinalCustomerPrice += materialModel.FinalCustomerPriceTotal;
                recSet.MoveNext();
            }
            Recordset recSet3 = (Recordset)DiManager.Company.GetBusinessObject(BoObjectTypes.BoRecordset);
            recSet3.DoQuery($"UPDATE [@RSM_MBOM_ROWS] SET U_Cost = {totalCost}, U_Price = {totalPrice}, U_Margin = {totalMargin}, U_FinalCustomerPrice = {totalFinalCustomerPrice} WHERE U_ElementID = 'MTRLs' AND U_SalesQuotationDocEntry = '{_MasterBomModel.SalesQuotationDocEntry}' AND U_ParentItemCode = '{_MasterBomModel.ParentItem}' AND U_Version = '{_MasterBomModel.Version}'");
        }
        public void CalculateMaterials()
        {
            bool fromDb = FillModelFromDb();
            if (!fromDb)
            {
                GenerateModel();
                InsertListToDb();
            }
            FillGridFromModel(_grid);
            RefreshBom.Invoke();
        }
        public static Action RefreshBom;


        public void UpdateMaterials()
        {            
            string version = (int.Parse(_materialModelsList.First().Version) + 1).ToString();
            _materialModelsList.Clear();
            _MasterBomModel.Version = version;
            _MasterBomModel.Add();
            FillModelFromGrid();
            InsertListToDb();
            FillGridFromModel(_grid);
        }
    }
}

