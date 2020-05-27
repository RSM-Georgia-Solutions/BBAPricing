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
    public class CalculationMachinaryResourcesController : IFormController
    {
        public bool HasErrors { get; set; }
        private MasterBomModel MasterBomModel;
        private List<ResourceModel> _MachinaryResourceModelsList;
        private readonly IForm _form;
        public Grid _grid { get { return (Grid)_form.Items.Item("Item_0").Specific; } }
        public CalculationMachinaryResourcesController(MasterBomModel masterBomModel, IForm form) : base(form)
        {
            _form = form;
            MasterBomModel = masterBomModel;
            _MachinaryResourceModelsList = new List<ResourceModel>();
        }

        public override void GetGridColumns()
        {
            _form.Freeze(true);
            string queryData = $@"SELECT TOP(0)    
                U_ResourceCode as [ResourceCode],
                U_ResourceName          as ResourceName,
                U_Uom                   as Uom,
                U_OtherQtyResource                  as OtherQtyResource,
                U_UomResourceMain                  as UomResourceMain,
                U_Quantity              as Quantity,
                U_StandartCost          as StandartCost,
                U_TotalStandartCost     as TotalStandartCost,
                U_ResourceUnitPrice     as ResourceUnitPrice,
                U_ResourceTotalPrice    as ResourceTotalPrice,
                U_OperationCode         as OperationCode,
                U_OperationName         as OperationName,
                '               '        as Currency,
                U_MarginPercent         as MarginPercent,
                U_AmountOnUnit          as AmountOnUnit,
                U_TotalAmount           as TotalAmount,
                U_CostOfUnit            as CostOfUnit,
                U_PriceOfUnit           as PriceOfUnit,
                U_MarginOfUnit          as MarginOfUnit,
                U_InfoPercent           as InfoPercent
                    FROM [@RSM_RESOURCES]";
            _grid.DataTable.ExecuteQuery(queryData);
            _grid.Columns.Item("ResourceCode").Editable = false;
            _grid.Columns.Item("ResourceName").Editable = false;
            _grid.Columns.Item("Uom").Editable = false;
            _grid.Columns.Item("OtherQtyResource").Editable = false;
            _grid.Columns.Item("UomResourceMain").Editable = false;
            _grid.Columns.Item("Quantity").Editable = false;
            _grid.Columns.Item("StandartCost").Editable = false;
            _grid.Columns.Item("TotalStandartCost").Editable = false;
            _grid.Columns.Item("ResourceUnitPrice").Editable = true;
            _grid.Columns.Item("ResourceTotalPrice").Editable = false;
            _grid.Columns.Item("OperationCode").Editable = false;
            _grid.Columns.Item("OperationName").Editable = false;
            _grid.Columns.Item("Currency").Editable = false;
            _grid.Columns.Item("MarginPercent").Editable = false;
            _grid.Columns.Item("AmountOnUnit").Editable = false;
            _grid.Columns.Item("TotalAmount").Editable = false;
            _grid.Columns.Item("CostOfUnit").Editable = false;
            _grid.Columns.Item("PriceOfUnit").Editable = false;
            _grid.Columns.Item("MarginOfUnit").Editable = false;
            _grid.Columns.Item("InfoPercent").Editable = false;
            _form.Freeze(false);
        }

        public override bool FillModelFromDb()
        {
            GetGridColumns();
            Recordset recSet = (Recordset)DiManager.Company.GetBusinessObject(BoObjectTypes.BoRecordset);
            recSet.DoQuery($@"SELECT  * FROM [@RSM_RESOURCES]
                                JOIN [@RSM_OPERATIONS] ON [@RSM_RESOURCES].U_ResourceCode = [@RSM_OPERATIONS].U_ResourceCode 
                                AND [@RSM_OPERATIONS].U_OperationCode  = [@RSM_RESOURCES].U_OperationCode
                                WHERE U_ParentItemCode = N'{MasterBomModel.ParentItem}' 
                                    AND U_SalesQuotationDocEntry = '{MasterBomModel.SalesQuotationDocEntry}'
                                    AND U_Version = '{MasterBomModel.Version}' 
                                    AND U_ResourceType = 'M'");
            if (!recSet.EoF)
            {
                while (!recSet.EoF)
                {
                    ResourceModel model = new ResourceModel();
                    model.SalesQuotationDocEntry = MasterBomModel.SalesQuotationDocEntry;
                    model.ParentItemCode = MasterBomModel.ParentItem;
                    model.ResourceCode = recSet.Fields.Item("U_ResourceCode").Value.ToString();
                    model.ResourceName = recSet.Fields.Item("U_ResourceName").Value.ToString();
                    model.OperationCode = recSet.Fields.Item("U_OperationCode").Value.ToString();
                    model.OperationName = recSet.Fields.Item("U_OperationName").Value.ToString();
                    model.Uom = recSet.Fields.Item("U_Uom").Value.ToString();
                    model.Quantity = (double)recSet.Fields.Item("U_Quantity").Value;
                    model.StandartCost = (double)recSet.Fields.Item("U_StandartCost").Value;
                    model.TotalStandartCost = (double)recSet.Fields.Item("U_TotalStandartCost").Value;
                    model.ResourceUnitPrice = (double)recSet.Fields.Item("U_ResourceUnitPrice").Value;
                    model.ResourceTotalPrice = (double)recSet.Fields.Item("U_ResourceTotalPrice").Value;
                    model.MarginPercent = (double)recSet.Fields.Item("U_MarginPercent").Value;
                    model.AmountOnUnit = (double)recSet.Fields.Item("U_AmountOnUnit").Value;
                    model.TotalAmount = (double)recSet.Fields.Item("U_TotalAmount").Value;
                    model.CostOfUnit = (double)recSet.Fields.Item("U_CostOfUnit").Value;
                    model.PriceOfUnit = (double)recSet.Fields.Item("U_PriceOfUnit").Value;
                    model.MarginOfUnit = (double)recSet.Fields.Item("U_MarginOfUnit").Value;
                    model.InfoPercent = (double)recSet.Fields.Item("U_InfoPercent").Value;
                    model.OtherQtyResource = (double)recSet.Fields.Item("U_OtherQtyResource").Value;
                    model.UomResourceMain = recSet.Fields.Item("U_UomResourceMain").Value.ToString();
                    model.Version = recSet.Fields.Item("U_Version").Value.ToString();
                    _MachinaryResourceModelsList.Add(model);
                    recSet.MoveNext();
                }
                return true;
            }
            return false;
        }

        public override void GenerateModel()
        {
            Recordset recSet = (Recordset)DiManager.Company.GetBusinessObject(BoObjectTypes.BoRecordset);
            double totalCost = 0;
            double totalPrice = 0;
            double totalMargin = 0;
            double totalFinalCustomerPrice = 0;
            string query = $@"SELECT  Itt1.Code as [ResourceCode], 
                                   OITM.itemName as [ResourceName],   
       OUGP.UgpName as [UomResourceMain],   
                                   ITT1.U_QtyOfBom as [OtherQtyResource],                                 
                                   ITT1.Quantity,
                                    ORSC.StdCost1 + StdCost2 + StdCost3 + StdCost4 + StdCost5 + StdCost6 + StdCost7 + StdCost8 + StdCost9 + StdCost10  as [StandartCost],
                            	   (ORSC.StdCost1 + StdCost2 + StdCost3 + StdCost4 + StdCost5 + StdCost6 + StdCost7 + StdCost8 + StdCost9 + StdCost10) * ITT1.Quantity as [TotalStandartCost],
                            	   CASE
                   WHEN ITM1.Price = 0
                   THEN CASE
                            WHEN ITM1.AddPrice1 = 0
                            THEN ITM1.AddPrice2
                            ELSE ITM1.AddPrice1
                        END
                   ELSE ITM1.Price
               END as [ResourceUnitPrice],
                            	   CASE
                   WHEN ITM1.Price = 0
                   THEN CASE
                            WHEN ITM1.AddPrice1 = 0
                            THEN ITM1.AddPrice2
                            ELSE ITM1.AddPrice1
                        END
                   ELSE ITM1.Price
               END * ITT1.Quantity as [ResourceTotalPrice] ,	    
 ITT1.Currency,                           
	                               [@RSM_OPERATIONS].U_UOM as [Uom],
	                                   [@RSM_OPERATIONS].U_ResourceCode AS [ResourceCode], 
       [@RSM_OPERATIONS].U_OperationCode AS [OperationCode], 
       [@RSM_OPERATIONS].U_OperationName AS [OperationName], 
       [@RSM_OPERATIONS].U_ResourceName AS [ResourceName]                            
                            FROM ITT1
                                 JOIN [@RSM_OPERATIONS] on [@RSM_OPERATIONS].U_OperationCode = ITT1.U_Operation AND [@RSM_OPERATIONS].U_ResourceCode = ITT1.code
                                 JOIN OITM ON OITM.LinkRsc = ITT1.Code
                                 JOIN ITM1 ON OITM.ItemCode = ITM1.ItemCode   
                                 JOIN OPLN on ITM1.PriceList = OPLN.ListNum
   JOIN OUGP on OUGP.UgpEntry = OITM.UgpEntry
                            	 JOIN ORSC ON ORSC.VisResCode =  ITT1.Code WHERE Father = N'{MasterBomModel.ParentItem}' 
                                    AND OPLN.ListName = N'{Settings.RetailPriceList}'
                                    AND ORSC.ResType = 'M'
                                    AND U_ResourceType = 'M'";
            recSet.DoQuery(query);
            if (recSet.EoF)
            {
                Application.SBO_Application.SetStatusBarMessage(
                    "Unit Retail Price გაწერილი არ არის " +
                    " ან რესურსის ტიპი არაა მანქანა-დანადგარის" +
                    " ან ოპერაციის კოდი არ არის გაწერილი",
                    BoMessageTime.bmt_Short,
                    true);
                HasErrors = true;
                return;
            }
            HasErrors = false;

            while (!recSet.EoF)
            {
                ResourceModel resourceModel = new ResourceModel();
                resourceModel.SalesQuotationDocEntry = MasterBomModel.SalesQuotationDocEntry;
                resourceModel.ParentItemCode = MasterBomModel.ParentItem;
                resourceModel.ResourceCode = recSet.Fields.Item("ResourceCode").Value.ToString();
                resourceModel.ResourceName = recSet.Fields.Item("ResourceName").Value.ToString();
                resourceModel.OperationCode = recSet.Fields.Item("OperationCode").Value.ToString();
                resourceModel.OperationName = recSet.Fields.Item("OperationName").Value.ToString();
                resourceModel.Uom = recSet.Fields.Item("Uom").Value.ToString();
                resourceModel.Quantity = (double)recSet.Fields.Item("Quantity").Value;
                resourceModel.StandartCost = (double)recSet.Fields.Item("StandartCost").Value;
                resourceModel.TotalStandartCost = (double)recSet.Fields.Item("TotalStandartCost").Value;
                resourceModel.ResourceUnitPrice = (double)recSet.Fields.Item("ResourceUnitPrice").Value;
                resourceModel.ResourceTotalPrice = (double)recSet.Fields.Item("ResourceTotalPrice").Value;

                if (MasterBomModel.Currency != "GEL")
                {
                    resourceModel.TotalStandartCost /= MasterBomModel.Rate;
                    resourceModel.ResourceTotalPrice /= MasterBomModel.Rate;
                    resourceModel.ResourceUnitPrice /= MasterBomModel.Rate;
                }

                resourceModel.OtherQtyResource = (double)recSet.Fields.Item("OtherQtyResource").Value;
                resourceModel.UomResourceMain = recSet.Fields.Item("UomResourceMain").Value.ToString();
                resourceModel.MarginPercent = (resourceModel.ResourceTotalPrice - resourceModel.TotalStandartCost) / resourceModel.ResourceTotalPrice;
                resourceModel.AmountOnUnit = resourceModel.ResourceUnitPrice - resourceModel.StandartCost;
                resourceModel.TotalAmount = resourceModel.ResourceTotalPrice - resourceModel.TotalStandartCost;
                if (resourceModel.OtherQtyResource == 0)
                {
                    SAPbouiCOM.Framework.Application.SBO_Application.SetStatusBarMessage("Qty Of BOM არ არის შევსებული");
                    return;
                }
                resourceModel.CostOfUnit = resourceModel.TotalStandartCost / resourceModel.OtherQtyResource;
                resourceModel.PriceOfUnit = resourceModel.ResourceTotalPrice / resourceModel.OtherQtyResource;
                resourceModel.MarginOfUnit = resourceModel.PriceOfUnit - resourceModel.CostOfUnit;
                resourceModel.InfoPercent = resourceModel.MarginOfUnit / resourceModel.PriceOfUnit;
                resourceModel.Version = MasterBomModel.Version;
                resourceModel.Currency = recSet.Fields.Item("Currency").Value.ToString();
                _MachinaryResourceModelsList.Add(resourceModel);
                totalCost += resourceModel.TotalStandartCost;
                totalPrice += resourceModel.ResourceTotalPrice;
                totalMargin += resourceModel.MarginOfUnit;
                totalFinalCustomerPrice += resourceModel.ResourceTotalPrice;
                recSet.MoveNext();
            }
            var mtrlLine = MasterBomModel.Rows.First(x => x.ElementID == "Machinery Resources");
            mtrlLine.Cost = totalCost;
            mtrlLine.Price = totalPrice;
            mtrlLine.Margin = totalMargin;
            mtrlLine.FinalCustomerPrice = totalFinalCustomerPrice;
        }

        private void InsertMachinarLyistToDb()
        {
            foreach (var item in _MachinaryResourceModelsList)
            {
                var res = item.Add();
            }
            MasterBomModel.Update();
        }
        private void UpdateMachinarLyistToDb()
        {
            foreach (var item in _MachinaryResourceModelsList)
            {
                var res = item.Update();
            }
            MasterBomModel.Update();
        }

        public override void FillGridFromModel(Grid grid)
        {
            SAPbouiCOM.Framework.Application.SBO_Application.Forms.ActiveForm.Freeze(true);
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
                grid.DataTable.SetValue("OperationCode", i, machinaryResource.OperationCode);
                grid.DataTable.SetValue("OperationName", i, machinaryResource.OperationName);
                grid.DataTable.SetValue("Currency", i, MasterBomModel.Currency);
                grid.DataTable.SetValue("MarginPercent", i, machinaryResource.MarginPercent * 100);
                grid.DataTable.SetValue("AmountOnUnit", i, machinaryResource.AmountOnUnit);
                grid.DataTable.SetValue("TotalAmount", i, machinaryResource.TotalAmount);
                grid.DataTable.SetValue("CostOfUnit", i, machinaryResource.CostOfUnit);
                grid.DataTable.SetValue("PriceOfUnit", i, machinaryResource.PriceOfUnit);
                grid.DataTable.SetValue("MarginOfUnit", i, machinaryResource.MarginOfUnit);
                grid.DataTable.SetValue("InfoPercent", i, machinaryResource.InfoPercent * 100);
                grid.DataTable.SetValue("OtherQtyResource", i, machinaryResource.OtherQtyResource);
                grid.DataTable.SetValue("UomResourceMain", i, machinaryResource.UomResourceMain);
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
                if (HasErrors)
                {
                    return;
                }
                FillGridFromModel(_grid);
                InsertMachinarLyistToDb();
                RefreshBom.Invoke();
            }
            else
            {
                FillGridFromModel(_grid);
            }
        }

        public void UpdateResources()
        {
            string version = (int.Parse(_MachinaryResourceModelsList.First().Version, CultureInfo.InvariantCulture) + 1).ToString();
            _MachinaryResourceModelsList.Clear();
            MasterBomModel.Version = version;
            foreach (var row in MasterBomModel.Rows)
            {
                row.Version = version;
            }
            GenerateModel();
            FillGridFromModel(_grid);
            MasterBomModel.Add();
            InsertMaterialsListToDbNewForUpateButton();
            RefreshBom.Invoke();

        }
        public void FillResourceUnitPriceFromGrid()
        {
            for (int i = 0; i < _grid.DataTable.Rows.Count - 1; i++)
            {
                _MachinaryResourceModelsList[0].ResourceUnitPrice = (double)_grid.DataTable.GetValue("ResourceUnitPrice", i);
                _MachinaryResourceModelsList[0].ResourceTotalPrice = _MachinaryResourceModelsList[0].ResourceUnitPrice * _MachinaryResourceModelsList[0].Quantity;
            }
        }
        public void FillModelFromGrid()
        {
            for (int i = 0; i < _grid.DataTable.Rows.Count; i++)
            {
                ResourceModel resourceModel = new ResourceModel();
                resourceModel.ResourceCode = _grid.DataTable.GetValue("ResourceCode", i).ToString();
                resourceModel.ResourceName = _grid.DataTable.GetValue("ResourceName", i).ToString();
                resourceModel.OtherQtyResource = (double)_grid.DataTable.GetValue("OtherQtyResource", i);
                resourceModel.Uom = _grid.DataTable.GetValue("Uom", i).ToString();
                resourceModel.Quantity = (double)_grid.DataTable.GetValue("Quantity", i);
                resourceModel.StandartCost = (double)_grid.DataTable.GetValue("StandartCost", i);
                resourceModel.TotalStandartCost = (double)_grid.DataTable.GetValue("TotalStandartCost", i);
                resourceModel.ResourceUnitPrice = (double)_grid.DataTable.GetValue("ResourceUnitPrice", i);
                resourceModel.ResourceTotalPrice = resourceModel.ResourceUnitPrice * resourceModel.Quantity;
                resourceModel.OperationCode = _grid.DataTable.GetValue("OperationCode", i).ToString();
                resourceModel.OperationName = _grid.DataTable.GetValue("OperationName", i).ToString();
                resourceModel.Currency = _grid.DataTable.GetValue("Currency", i).ToString();
                resourceModel.CostOfUnit = (double)_grid.DataTable.GetValue("CostOfUnit", i);
                resourceModel.UomResourceMain = _grid.DataTable.GetValue("UomResourceMain", i).ToString();
                resourceModel.PriceOfUnit = resourceModel.ResourceTotalPrice / resourceModel.Quantity;
                resourceModel.MarginOfUnit = resourceModel.PriceOfUnit - resourceModel.CostOfUnit;
                resourceModel.InfoPercent = resourceModel.MarginOfUnit / resourceModel.PriceOfUnit;
                resourceModel.MarginPercent = (resourceModel.ResourceTotalPrice - resourceModel.TotalStandartCost) / resourceModel.ResourceTotalPrice;
                resourceModel.AmountOnUnit = resourceModel.ResourceUnitPrice - resourceModel.StandartCost;
                resourceModel.TotalAmount = resourceModel.ResourceTotalPrice - resourceModel.TotalStandartCost;
                resourceModel.Version = MasterBomModel.Version;
                resourceModel.SalesQuotationDocEntry = MasterBomModel.SalesQuotationDocEntry;
                resourceModel.ParentItemCode = MasterBomModel.ParentItem;
                _MachinaryResourceModelsList.Add(resourceModel);
                SAPbouiCOM.Framework.Application.SBO_Application.Forms.ActiveForm.Freeze(false);
            }
        }

        public void UpdateMachinaryResourcesFromForm()
        {
            string version = (int.Parse(_MachinaryResourceModelsList.First().Version, CultureInfo.InvariantCulture) + 1).ToString();
            _MachinaryResourceModelsList.Clear();
            MasterBomModel.Version = version;
            FillModelFromGrid();
            UpdateMasterBomRowTotals();
            foreach (var row in MasterBomModel.Rows)
            {
                row.Version = version;
            }
            InsertMaterialsListToDbNewForUpateButton();
            MasterBomModel.Add();
            GetGridColumns();
            FillGridFromModel(_grid);
            RefreshBom.Invoke();
        }

        private void UpdateMasterBomRowTotals()
        {
            double totalCost = 0;
            double totalPrice = 0;
            double totalMargin = 0;
            double totalFinalCustomerPrice = 0;
            foreach (var item in _MachinaryResourceModelsList)
            {
                totalCost += item.TotalStandartCost;
                totalPrice += item.ResourceTotalPrice;
                totalMargin += item.MarginOfUnit;
                totalFinalCustomerPrice += item.ResourceTotalPrice;
            }
            var mtrlLine = MasterBomModel.Rows.First(x => x.ElementID == "Machinery Resources");
            mtrlLine.Cost = totalCost;
            mtrlLine.Price = totalPrice;
            mtrlLine.Margin = totalMargin;
            mtrlLine.FinalCustomerPrice = totalFinalCustomerPrice;
        }

        private void InsertMaterialsListToDbNewForUpateButton()
        {
            foreach (var item in _MachinaryResourceModelsList)
            {
                var res = item.Add();
            }
        }
    }
}