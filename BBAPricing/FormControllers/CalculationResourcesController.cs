using BBAPricing.Models;
using SAPbobsCOM;
using SAPbouiCOM;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBAPricing.FormControllers
{
    public class CalculationResourcesController
    {
        private MasterBomModel _MasterBomModel;
        private List<MachinaryResourceModel> _MachinaryResourceModelsList;
        private readonly IForm _form;
        public Grid _grid { get { return (Grid)_form.Items.Item("Item_0").Specific; } }
        public CalculationResourcesController(MasterBomModel masterBomModel, IForm form)
        {
            _form = form;
            _MasterBomModel = masterBomModel;
            _MachinaryResourceModelsList = new List<MachinaryResourceModel>();
        }

        public void GetGridColumns()
        {
            string queryData = $@"SELECT TOP(0) Code as [ResourceCode], 
                                   OITM.itemName as [ResourceName], 
                                   ITT1.Uom,     
                                   ITT1.Quantity,
                            	   ORSC.StdCost1 as [StandartCost],
                            	   ORSC.StdCost1 * ITT1.Quantity as [TotalStandartCost],
                            	   ITM1.Price as [ResourceUnitPrice],
                            	   ITM1.Price * ITT1.Quantity as [ResourceTotalPrice]	  
                            FROM ITT1
                                 JOIN OITM ON OITM.U_ResourseCode = ITT1.Code
                                 JOIN ITM1 ON OITM.ItemCode = ITM1.ItemCode   
                                 JOIN OPLN on ITM1.PriceList = OPLN.ListNum
                            	 JOIN ORSC ON ORSC.VisResCode =  ITT1.Code
                            WHERE Father = '{_MasterBomModel.ParentItem}' AND OPLN.ListName = 'Resource Unit Price'";
            _grid.DataTable.ExecuteQuery(queryData);
        }

        public bool FillModelFromDb()
        {
            GetGridColumns();
            Recordset recSet = (Recordset)DiManager.Company.GetBusinessObject(BoObjectTypes.BoRecordset);
            recSet.DoQuery($@"SELECT * FROM [@RSM_RESOURCES] WHERE U_ParentItemCode = '{_MasterBomModel.ParentItem}' AND U_SalesQuotationDocEntry = '{_MasterBomModel.SalesQuotationDocEntry}' AND U_Version = '{_MasterBomModel.Version}'");
            if (!recSet.EoF)
            {
                while (!recSet.EoF)
                {
                    MachinaryResourceModel machinaryModel = new MachinaryResourceModel();
                    machinaryModel.SalesQuotationDocEntry = _MasterBomModel.SalesQuotationDocEntry;
                    machinaryModel.ParentItemCode = _MasterBomModel.ParentItem;
                    machinaryModel.ResourceCode = recSet.Fields.Item("U_ResourceCode").Value.ToString();
                    machinaryModel.ResourceName = recSet.Fields.Item("U_ResourceName").Value.ToString();
                    machinaryModel.Uom = recSet.Fields.Item("U_Uom").Value.ToString();
                    machinaryModel.Quantity = (double)recSet.Fields.Item("U_Quantity").Value;
                    machinaryModel.StandartCost = (double)recSet.Fields.Item("U_StandartCost").Value;
                    machinaryModel.TotalStandartCost = (double)recSet.Fields.Item("U_TotalStandartCost").Value;
                    machinaryModel.ResourceUnitPrice = (double)recSet.Fields.Item("U_ResourceUnitPrice").Value;
                    machinaryModel.ResourceTotalPrice = (double)recSet.Fields.Item("U_ResourceTotalPrice").Value;
                    machinaryModel.MarginPercent = (double)recSet.Fields.Item("U_MarginPercent").Value;
                    machinaryModel.AmountOnUnit = (double)recSet.Fields.Item("U_AmountOnUnit").Value;
                    machinaryModel.TotalAmount = (double)recSet.Fields.Item("U_TotalAmount").Value;
                    machinaryModel.CostOfUnit = (double)recSet.Fields.Item("U_CostOfUnit").Value;
                    machinaryModel.PriceOfUnit = (double)recSet.Fields.Item("U_PriceOfUnit").Value;
                    machinaryModel.MarginOfUnit = (double)recSet.Fields.Item("U_MarginOfUnit").Value;
                    machinaryModel.InfoPercent = (double)recSet.Fields.Item("U_InfoPercent").Value;
                    machinaryModel.Version = recSet.Fields.Item("U_Version").Value.ToString();
                    _MachinaryResourceModelsList.Add(machinaryModel);
                    recSet.MoveNext();
                }
                return true;
            }
            return false;
        }

        public void GenerateModel()
        {
            GetGridColumns();
            Recordset recSet = (Recordset)DiManager.Company.GetBusinessObject(BoObjectTypes.BoRecordset);
            double totalCost = 0;
            double totalPrice = 0;
            double totalMargin = 0;
            double totalFinalCustomerPrice = 0;
            recSet.DoQuery($@"SELECT  Code as [ResourceCode], 
                                   OITM.itemName as [ResourceName], 
                                   ITT1.Uom,     
                                   ITT1.Quantity,
                            	   ORSC.StdCost1 as [StandartCost],
                            	   ORSC.StdCost1 * ITT1.Quantity as [TotalStandartCost],
                            	   ITM1.Price as [ResourceUnitPrice],
                            	   ITM1.Price * ITT1.Quantity as [ResourceTotalPrice] , *
                            FROM ITT1
                                 JOIN OITM ON OITM.U_ResourseCode = ITT1.Code
                                 JOIN ITM1 ON OITM.ItemCode = ITM1.ItemCode   
                                 JOIN OPLN on ITM1.PriceList = OPLN.ListNum
                            	 JOIN ORSC ON ORSC.VisResCode =  ITT1.Code WHERE Father = '{_MasterBomModel.ParentItem}' AND OPLN.ListName = 'Resource Unit Price'");

            while (!recSet.EoF)
            {
                MachinaryResourceModel machinaryResourceModel = new MachinaryResourceModel();
                machinaryResourceModel.SalesQuotationDocEntry = _MasterBomModel.SalesQuotationDocEntry;
                machinaryResourceModel.ParentItemCode = _MasterBomModel.ParentItem;
                machinaryResourceModel.ResourceCode = recSet.Fields.Item("ResourceCode").Value.ToString();
                machinaryResourceModel.ResourceName = recSet.Fields.Item("ResourceName").Value.ToString();
                machinaryResourceModel.Uom = recSet.Fields.Item("Uom").Value.ToString();
                machinaryResourceModel.Quantity = (double)recSet.Fields.Item("Quantity").Value;
                machinaryResourceModel.StandartCost = (double)recSet.Fields.Item("StandartCost").Value;
                machinaryResourceModel.TotalStandartCost = (double)recSet.Fields.Item("TotalStandartCost").Value;
                machinaryResourceModel.ResourceUnitPrice = (double)recSet.Fields.Item("ResourceUnitPrice").Value;
                machinaryResourceModel.ResourceTotalPrice = (double)recSet.Fields.Item("ResourceTotalPrice").Value;
                machinaryResourceModel.MarginPercent = (machinaryResourceModel.ResourceTotalPrice - machinaryResourceModel.TotalStandartCost) / machinaryResourceModel.ResourceTotalPrice;
                machinaryResourceModel.AmountOnUnit = machinaryResourceModel.ResourceUnitPrice - machinaryResourceModel.StandartCost;
                machinaryResourceModel.TotalAmount = machinaryResourceModel.ResourceTotalPrice - machinaryResourceModel.TotalStandartCost;
                machinaryResourceModel.CostOfUnit = machinaryResourceModel.TotalStandartCost / machinaryResourceModel.Quantity;
                machinaryResourceModel.PriceOfUnit = machinaryResourceModel.ResourceTotalPrice / machinaryResourceModel.Quantity;
                machinaryResourceModel.MarginOfUnit = machinaryResourceModel.PriceOfUnit - machinaryResourceModel.CostOfUnit;
                machinaryResourceModel.InfoPercent = machinaryResourceModel.MarginOfUnit - machinaryResourceModel.PriceOfUnit;
                machinaryResourceModel.Version = _MasterBomModel.Version;
                _MachinaryResourceModelsList.Add(machinaryResourceModel);
                totalCost = totalCost += machinaryResourceModel.TotalStandartCost;
                totalPrice = totalPrice += machinaryResourceModel.ResourceUnitPrice;
                totalMargin = totalMargin += machinaryResourceModel.MarginOfUnit;
                totalFinalCustomerPrice = totalFinalCustomerPrice += machinaryResourceModel.AmountOnUnit;
                recSet.MoveNext();
            }
            var mtrlLine = _MasterBomModel.Rows.First(x => x.ElementID == "Machinery Resources");
            mtrlLine.Cost = totalCost;
            mtrlLine.Price = totalPrice;
            mtrlLine.Margin = totalMargin;
            mtrlLine.FinalCustomerPrice = totalFinalCustomerPrice;
        }

        public void InsertMachinarLyistToDb()
        {
            foreach (var item in _MachinaryResourceModelsList)
            {
                var res = item.Add();
            }
        }

        public void FillGridFromModel(Grid grid)
        {
            SAPbouiCOM.Framework.Application.SBO_Application.Forms.ActiveForm.Freeze(true);
            GetGridColumns();
            for (int i = 0; i < _MachinaryResourceModelsList.Count; i++)
            {
                var machinaryResource = _MachinaryResourceModelsList[i];
                grid.DataTable.Rows.Add();
                grid.DataTable.SetValue("ResourceCode", i, machinaryResource.ResourceCode);
                grid.DataTable.SetValue("ResourceName", i, machinaryResource.ResourceName);
                grid.DataTable.SetValue("Uom", i, machinaryResource.Uom);
                grid.DataTable.SetValue("Quantity", i, machinaryResource.Quantity);
                grid.DataTable.SetValue("StandartCost", i, machinaryResource.StandartCost);
                grid.DataTable.SetValue("TotalStandartCost", i, machinaryResource.TotalStandartCost);
                grid.DataTable.SetValue("ResourceUnitPrice", i, machinaryResource.ResourceUnitPrice);
                grid.DataTable.SetValue("ResourceTotalPrice", i, machinaryResource.ResourceTotalPrice);
            }
            _grid.DataTable.Rows.Remove(_grid.DataTable.Rows.Count - 1);
            SAPbouiCOM.Framework.Application.SBO_Application.Forms.ActiveForm.Freeze(false);
        }
        public static Action RefreshBom;


        public void CalculateResources()
        {
            bool fromDb = FillModelFromDb();

            if (!fromDb)
            {
                GenerateModel();
                FillGridFromModel(_grid);
                InsertMachinarLyistToDb();
            }
            else
            {
                FillGridFromModel(_grid);
            }          
        }


        public void UpdateResources()
        {
            string version = (int.Parse(_MachinaryResourceModelsList.First().Version,CultureInfo.InvariantCulture) + 1).ToString();
            _MachinaryResourceModelsList.Clear();
            _MasterBomModel.Version = version;
            _MasterBomModel.Add();
            FillModelFromGrid();
            InsertMachinarLyistToDb();
            FillGridFromModel(_grid);
        }
        public void FillModelFromGrid()
        {
            string version = (int.Parse(_MachinaryResourceModelsList.First().Version, CultureInfo.InvariantCulture) + 1).ToString();
            _MachinaryResourceModelsList.Clear();
            _MasterBomModel.Version = version;
            _MasterBomModel.Add();
            for (int i = 0; i < _grid.DataTable.Rows.Count; i++)
            {
                MachinaryResourceModel machinaryResourceModel = new MachinaryResourceModel();
                machinaryResourceModel.ResourceCode = _grid.DataTable.GetValue("ResourceCode", i).ToString();
                machinaryResourceModel.ResourceName = _grid.DataTable.GetValue("ResourceName", i).ToString();
                machinaryResourceModel.Uom = _grid.DataTable.GetValue("Uom", i).ToString();
                machinaryResourceModel.Quantity = (double)_grid.DataTable.GetValue("Quantity", i);
                machinaryResourceModel.StandartCost = (double)_grid.DataTable.GetValue("StandartCost", i);
                machinaryResourceModel.TotalStandartCost = (double)_grid.DataTable.GetValue("TotalStandartCost", i);
                machinaryResourceModel.ResourceUnitPrice = (double)_grid.DataTable.GetValue("ResourceUnitPrice", i);
                machinaryResourceModel.ResourceTotalPrice = (double)_grid.DataTable.GetValue("ResourceTotalPrice", i);
                machinaryResourceModel.SalesQuotationDocEntry = _MasterBomModel.SalesQuotationDocEntry;
                machinaryResourceModel.ParentItemCode = _MasterBomModel.ParentItem;
                _MachinaryResourceModelsList.Add(machinaryResourceModel);
                SAPbouiCOM.Framework.Application.SBO_Application.Forms.ActiveForm.Freeze(false);
            }
        }
    }
}