using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BBAPricing.Iterfaces;
using BBAPricing.Models;
using SAPbouiCOM;
using SAPbobsCOM;

namespace BBAPricing.FormControllers
{
    public class CalculationHumanResourcesController : IFormController
    {
        private readonly MasterBomModel MasterBomModel;
        private new readonly IForm Form;
        private readonly List<ResourceModel> HumanResources;
        private Grid Grid => (Grid)Form.Items.Item("Item_0").Specific;

        public CalculationHumanResourcesController(MasterBomModel masterBomModel, IForm form) : base(form)
        {
            MasterBomModel = masterBomModel;
            Form = form;
            HumanResources = new List<ResourceModel>();
        }

        public override void FillGridFromModel(Grid grid)
        {
            SAPbouiCOM.Framework.Application.SBO_Application.Forms.ActiveForm.Freeze(true);
            for (int i = 0; i < HumanResources.Count; i++)
            {
                var humanResource = HumanResources[i];
                grid.DataTable.Rows.Add();
                grid.DataTable.SetValue("ResourceCode", i, humanResource.ResourceCode);
                grid.DataTable.SetValue("ResourceName", i, humanResource.ResourceName);
                grid.DataTable.SetValue("Uom", i, humanResource.Uom);
                grid.DataTable.SetValue("Quantity", i, humanResource.Quantity);
                grid.DataTable.SetValue("StandartCost", i, humanResource.StandartCost);
                grid.DataTable.SetValue("TotalStandartCost", i, humanResource.TotalStandartCost);
                grid.DataTable.SetValue("ResourceUnitPrice", i, humanResource.ResourceUnitPrice);
                grid.DataTable.SetValue("ResourceTotalPrice", i, humanResource.ResourceTotalPrice);
                grid.DataTable.SetValue("OperationCode", i, humanResource.OperationCode);
                grid.DataTable.SetValue("OperationName", i, humanResource.OperationName);
                grid.DataTable.SetValue("Currency", i, MasterBomModel.Currency);
                grid.DataTable.SetValue("MarginPercent", i, humanResource.MarginPercent * 100);
                grid.DataTable.SetValue("AmountOnUnit", i, humanResource.AmountOnUnit);
                grid.DataTable.SetValue("TotalAmount", i, humanResource.TotalAmount);
                grid.DataTable.SetValue("CostOfUnit", i, humanResource.CostOfUnit);
                grid.DataTable.SetValue("PriceOfUnit", i, humanResource.PriceOfUnit);
                grid.DataTable.SetValue("MarginOfUnit", i, humanResource.MarginOfUnit);
                grid.DataTable.SetValue("InfoPercent", i, humanResource.InfoPercent * 100);
                grid.DataTable.SetValue("OtherQtyResource", i, humanResource.OtherQtyResource);
                grid.DataTable.SetValue("UomResourceMain", i, humanResource.UomResourceMain);
            }
            Grid.DataTable.Rows.Remove(Grid.DataTable.Rows.Count - 1);
            SAPbouiCOM.Framework.Application.SBO_Application.Forms.ActiveForm.Freeze(false);
        }

        public override void GetGridColumns()
        {
            Form.Freeze(true);
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
            Grid.DataTable.ExecuteQuery(queryData);
            Grid.Columns.Item("ResourceCode").Editable = false;
            Grid.Columns.Item("ResourceName").Editable = false;
            Grid.Columns.Item("Uom").Editable = false;
            Grid.Columns.Item("OtherQtyResource").Editable = false;
            Grid.Columns.Item("UomResourceMain").Editable = false;
            Grid.Columns.Item("Quantity").Editable = false;
            Grid.Columns.Item("StandartCost").Editable = false;
            Grid.Columns.Item("TotalStandartCost").Editable = false;
            Grid.Columns.Item("ResourceUnitPrice").Editable = true;
            Grid.Columns.Item("ResourceTotalPrice").Editable = false;
            Grid.Columns.Item("OperationCode").Editable = false;
            Grid.Columns.Item("OperationName").Editable = false;
            Grid.Columns.Item("Currency").Editable = false;
            Grid.Columns.Item("MarginPercent").Editable = false;
            Grid.Columns.Item("AmountOnUnit").Editable = false;
            Grid.Columns.Item("TotalAmount").Editable = false;
            Grid.Columns.Item("CostOfUnit").Editable = false;
            Grid.Columns.Item("PriceOfUnit").Editable = false;
            Grid.Columns.Item("MarginOfUnit").Editable = false;
            Grid.Columns.Item("InfoPercent").Editable = false;
            Form.Freeze(false);
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
                                    AND U_Version = N'{MasterBomModel.Version}' 
                                    AND U_ResourceType = 'L'");
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
                    HumanResources.Add(model);
                    recSet.MoveNext();
                }
                return true;
            }
            return false;
        }

        public override void GenerateModel()
        {
            GetGridColumns();
            Recordset recSet = (Recordset)DiManager.Company.GetBusinessObject(BoObjectTypes.BoRecordset);
            double totalCost = 0;
            double totalPrice = 0;
            double totalMargin = 0;
            double totalFinalCustomerPrice = 0;
            string query = $@"SELECT Itt1.Code AS [ResourceCode], 
       OITM.itemName AS [ResourceName], 
       OUGP.UgpName AS [UomResourceMain], 
       ITT1.U_QtyOfBom AS [OtherQtyResource], 
       ITT1.Quantity, 
       ORSC.StdCost1 + StdCost2 + StdCost3 + StdCost4 + StdCost5 + StdCost6 + StdCost7 + StdCost8 + StdCost9 + StdCost10 AS [StandartCost], 
       (ORSC.StdCost1 + StdCost2 + StdCost3 + StdCost4 + StdCost5 + StdCost6 + StdCost7 + StdCost8 + StdCost9 + StdCost10) * ITT1.Quantity AS [TotalStandartCost],
       CASE
           WHEN ITM1.Price = 0
           THEN CASE
                    WHEN ITM1.AddPrice1 = 0
                    THEN ITM1.AddPrice2
                    ELSE ITM1.AddPrice1
                END
           ELSE ITM1.Price
       END AS [ResourceUnitPrice],
       CASE
           WHEN ITM1.Price = 0
           THEN CASE
                    WHEN ITM1.AddPrice1 = 0
                    THEN ITM1.AddPrice2
                    ELSE ITM1.AddPrice1
                END
           ELSE ITM1.Price
       END * ITT1.Quantity AS [ResourceTotalPrice], 
       ITT1.Currency, 
       [@RSM_OPERATIONS].U_UOM AS [Uom], 
       [@RSM_OPERATIONS].U_ResourceCode AS [ResourceCode], 
       [@RSM_OPERATIONS].U_OperationCode AS [OperationCode], 
       [@RSM_OPERATIONS].U_OperationName AS [OperationName], 
       [@RSM_OPERATIONS].U_ResourceName AS [ResourceName]
FROM ITT1
     JOIN [@RSM_OPERATIONS] ON [@RSM_OPERATIONS].U_OperationCode = ITT1.U_Operation
                               AND [@RSM_OPERATIONS].U_ResourceCode = ITT1.code
     JOIN OITM ON OITM.LinkRsc = ITT1.Code
     JOIN ITM1 ON OITM.ItemCode = ITM1.ItemCode
     JOIN OPLN ON ITM1.PriceList = OPLN.ListNum
     JOIN OUGP ON OUGP.UgpEntry = OITM.UgpEntry
     JOIN ORSC ON ORSC.VisResCode = ITT1.Code WHERE Father = N'{MasterBomModel.ParentItem}' 
                                    AND OPLN.ListName = N'{Settings.RetailPriceList}' 
                                    AND ORSC.ResType = 'L'
                                    AND U_ResourceType = 'L'";
            recSet.DoQuery(query);

            while (!recSet.EoF)
            {
                ResourceModel resourceModel = new ResourceModel();
                resourceModel.SalesQuotationDocEntry = MasterBomModel.SalesQuotationDocEntry;
                resourceModel.ParentItemCode = MasterBomModel.ParentItem;
                resourceModel.ResourceCode = recSet.Fields.Item("ResourceCode").Value.ToString();
                resourceModel.ResourceName = recSet.Fields.Item("ResourceName").Value.ToString();
                resourceModel.OperationCode = recSet.Fields.Item("OperationCode").Value.ToString();
                resourceModel.OperationName = recSet.Fields.Item("OperationName").Value.ToString();
                resourceModel.UomResourceMain = recSet.Fields.Item("UomResourceMain").Value.ToString();
                resourceModel.OtherQtyResource = (double)recSet.Fields.Item("OtherQtyResource").Value;
                if (resourceModel.OtherQtyResource == 0)
                {
                    SAPbouiCOM.Framework.Application.SBO_Application.MessageBox("ოპერაციის  რაოდენობა 0 ის ტოლია");
                    generateMethodSucces = false;
                    return;
                }
                resourceModel.Uom = recSet.Fields.Item("Uom").Value.ToString();
                resourceModel.Quantity = (double)recSet.Fields.Item("Quantity").Value;
                resourceModel.StandartCost = (double)recSet.Fields.Item("StandartCost").Value;
                if (resourceModel.StandartCost == 0)
                {
                    SAPbouiCOM.Framework.Application.SBO_Application.SetStatusBarMessage("რესურსების Standard Cost N1 არ არის შევსებული");
                }
                resourceModel.TotalStandartCost = (double)recSet.Fields.Item("TotalStandartCost").Value;
                resourceModel.ResourceUnitPrice = (double)recSet.Fields.Item("ResourceUnitPrice").Value;


               

                if (MasterBomModel.Currency != "GEL")
                {
                    resourceModel.TotalStandartCost /= MasterBomModel.Rate;
                    resourceModel.ResourceTotalPrice /= MasterBomModel.Rate;
                    resourceModel.ResourceUnitPrice /= MasterBomModel.Rate;
                    resourceModel.StandartCost /= MasterBomModel.Rate;
                }
                resourceModel.AmountOnUnit = resourceModel.ResourceUnitPrice - resourceModel.StandartCost;
                resourceModel.ResourceTotalPrice = resourceModel.ResourceUnitPrice * resourceModel.Quantity;
                resourceModel.TotalAmount = resourceModel.ResourceTotalPrice - resourceModel.TotalStandartCost;
                resourceModel.MarginPercent = (resourceModel.ResourceTotalPrice - resourceModel.TotalStandartCost) / resourceModel.ResourceTotalPrice;
                resourceModel.CostOfUnit = resourceModel.TotalStandartCost / resourceModel.OtherQtyResource;
                resourceModel.PriceOfUnit = resourceModel.ResourceTotalPrice / resourceModel.OtherQtyResource;
                resourceModel.MarginOfUnit = resourceModel.PriceOfUnit - resourceModel.CostOfUnit;
                resourceModel.InfoPercent = resourceModel.MarginOfUnit / resourceModel.PriceOfUnit;
                resourceModel.Version = MasterBomModel.Version;
                HumanResources.Add(resourceModel);
                totalCost += resourceModel.TotalStandartCost;
                totalPrice += resourceModel.ResourceTotalPrice;
                totalMargin += resourceModel.MarginOfUnit;
                totalFinalCustomerPrice += resourceModel.ResourceTotalPrice;
                recSet.MoveNext();
            }
            var mtrlLine = MasterBomModel.Rows.First(x => x.ElementID == "Human Resources");
            mtrlLine.Cost = totalCost;
            mtrlLine.Price = totalPrice;
            mtrlLine.Margin = totalMargin;
            mtrlLine.FinalCustomerPrice = totalFinalCustomerPrice;
            generateMethodSucces = true;
        }
        public static Action RefreshBom;

        bool generateMethodSucces;
        public void CalculateResources()
        {
            GetGridColumns();
            bool fromDb = FillModelFromDb();
            if (!fromDb)
            {
                GenerateModel();
                if (!generateMethodSucces)
                {
                    return;
                }
                FillGridFromModel(Grid);
                InsertMaterialsListToDb();
                RefreshBom.Invoke();
            }
            else
            {
                FillGridFromModel(Grid);
            }
        }

        private void InsertMaterialsListToDb()
        {
            foreach (var item in HumanResources)
            {
                var res = item.Add();
            }

            MasterBomModel.Update();
        }

        public void UpdateResources()
        {
            string version = (int.Parse(HumanResources.First().Version, CultureInfo.InvariantCulture) + 1).ToString();
            HumanResources.Clear();
            MasterBomModel.Version = version;
            foreach (var row in MasterBomModel.Rows)
            {
                row.Version = version;
            }
            GenerateModel();
            FillGridFromModel(Grid);
            MasterBomModel.Add();
            InsertMaterialsListToDb();
            RefreshBom.Invoke();
        }

        public void UpdateHumanResourcesFromForm()
        {
            string version = (int.Parse(HumanResources.First().Version, CultureInfo.InvariantCulture) + 1).ToString();
            HumanResources.Clear();
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
            FillGridFromModel(Grid);
            RefreshBom.Invoke();
        }
        private void UpdateMasterBomRowTotals()
        {
            double totalCost = 0;
            double totalPrice = 0;
            double totalMargin = 0;
            double totalFinalCustomerPrice = 0;
            foreach (var item in HumanResources)
            {
                totalCost += item.TotalStandartCost;
                totalPrice += item.ResourceTotalPrice;
                totalMargin += item.MarginOfUnit;
                totalFinalCustomerPrice += item.ResourceTotalPrice;
            }
            var mtrlLine = MasterBomModel.Rows.First(x => x.ElementID == "Human Resources");
            mtrlLine.Cost = totalCost;
            mtrlLine.Price = totalPrice;
            mtrlLine.Margin = totalMargin;
            mtrlLine.FinalCustomerPrice = totalFinalCustomerPrice;
        }

        private void FillModelFromGrid()
        {

            for (int i = 0; i < Grid.DataTable.Rows.Count; i++)
            {
                ResourceModel resourceModel = new ResourceModel();
                resourceModel.ResourceCode = Grid.DataTable.GetValue("ResourceCode", i).ToString();
                resourceModel.ResourceName = Grid.DataTable.GetValue("ResourceName", i).ToString();
                resourceModel.OtherQtyResource = (double)Grid.DataTable.GetValue("OtherQtyResource", i);
                resourceModel.Uom = Grid.DataTable.GetValue("Uom", i).ToString();
                resourceModel.Quantity = (double)Grid.DataTable.GetValue("Quantity", i);
                resourceModel.StandartCost = (double)Grid.DataTable.GetValue("StandartCost", i);
                resourceModel.TotalStandartCost = (double)Grid.DataTable.GetValue("TotalStandartCost", i);
                resourceModel.ResourceUnitPrice = (double)Grid.DataTable.GetValue("ResourceUnitPrice", i);
                resourceModel.ResourceTotalPrice = resourceModel.ResourceUnitPrice * resourceModel.Quantity;
                resourceModel.OperationCode = Grid.DataTable.GetValue("OperationCode", i).ToString();
                resourceModel.OperationName = Grid.DataTable.GetValue("OperationName", i).ToString();
                resourceModel.Currency = Grid.DataTable.GetValue("Currency", i).ToString();
                resourceModel.CostOfUnit = (double)Grid.DataTable.GetValue("CostOfUnit", i);
                resourceModel.UomResourceMain = Grid.DataTable.GetValue("UomResourceMain", i).ToString();
                resourceModel.PriceOfUnit = resourceModel.ResourceTotalPrice / resourceModel.OtherQtyResource;
                resourceModel.MarginOfUnit = resourceModel.PriceOfUnit - resourceModel.CostOfUnit;
                resourceModel.InfoPercent = resourceModel.MarginOfUnit / resourceModel.PriceOfUnit;
                resourceModel.MarginPercent = (resourceModel.ResourceTotalPrice - resourceModel.TotalStandartCost) / resourceModel.ResourceTotalPrice;
                resourceModel.AmountOnUnit = resourceModel.ResourceUnitPrice - resourceModel.StandartCost;
                resourceModel.TotalAmount = resourceModel.ResourceTotalPrice - resourceModel.TotalStandartCost;

                resourceModel.Version = MasterBomModel.Version;
                resourceModel.SalesQuotationDocEntry = MasterBomModel.SalesQuotationDocEntry;
                resourceModel.ParentItemCode = MasterBomModel.ParentItem;
                HumanResources.Add(resourceModel);
                SAPbouiCOM.Framework.Application.SBO_Application.Forms.ActiveForm.Freeze(false);
            }

        }

        private void InsertMaterialsListToDbNewForUpateButton()
        {
            foreach (var item in HumanResources)
            {
                var res = item.Add();
            }
        }

    }
}
