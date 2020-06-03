using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BBAPricing.Forms;
using BBAPricing.Models;
using SAPbouiCOM;
using SAPbouiCOM.Framework;
using Application = SAPbouiCOM.Application;
using SAPbobsCOM;
using System.Globalization;

namespace BBAPricing.System_Forms
{
    [FormAttribute("149", "System Forms/SalesQuotation.b1f")]
    class SalesQuotation : SystemFormBase
    {
        public SalesQuotation()
        {
        }

        List<MasterBomModel> _masterBomModels;
        string docEntry;
        string itemCode;
        /// <summary>
        /// Initialize components. Called by framework after form created.
        /// </summary>
        public override void OnInitializeComponent()
        {
            this.Button0 = ((SAPbouiCOM.Button)(this.GetItem("Item_0").Specific));
            this.Button0.PressedAfter += new SAPbouiCOM._IButtonEvents_PressedAfterEventHandler(this.Button0_PressedAfter);
            this.Button1 = ((SAPbouiCOM.Button)(this.GetItem("Item_1").Specific));
            this.Button1.PressedAfter += new SAPbouiCOM._IButtonEvents_PressedAfterEventHandler(this.Button1_PressedAfter);
            this.OnCustomInitialize();

        }

        /// <summary>
        /// Initialize form event. Called by framework before form creation.
        /// </summary>
        public override void OnInitializeFormEvents()
        {
        }

        private SAPbouiCOM.Button Button0;

        private void OnCustomInitialize()
        {
            _masterBomModels = new List<MasterBomModel>();
        }

        public bool GenerateModel(Form activeForm)
        {
            MasterBomModel model;
            var dataSourceQut1 = activeForm.DataSources.DBDataSources.Item("QUT1");
            var dataSourceOqut = activeForm.DataSources.DBDataSources.Item("OQUT");
            var matrix = (Matrix)activeForm.Items.Item("38").Specific;
            var row = matrix.GetNextSelectedRow();

            Recordset rec = (Recordset)DiManager.Company.GetBusinessObject(BoObjectTypes.BoRecordset);
            rec.DoQuery($@"SELECT [@RSM_MBOM].Code [MBOMCode],[@RSM_MBOM_ROWS].Code [MBOMRowCode], [@RSM_MBOM].U_SalesQuotationDocEntry [SalesQuotationDocEntry], * FROM [@RSM_MBOM] 
JOIN[@RSM_MBOM_ROWS] on[@RSM_MBOM].U_SalesQuotationDocEntry = [@RSM_MBOM_ROWS].U_SalesQuotationDocEntry AND[@RSM_MBOM].U_ParentItem = [@RSM_MBOM_ROWS].U_ParentItemCode AND [@RSM_MBOM_ROWS].U_Version = [@RSM_MBOM].U_Version 
 WHERE [@RSM_MBOM].U_SalesQuotationDocEntry = '{docEntry}' AND U_ParentItem = N'{itemCode}' AND [@RSM_MBOM].U_Version = (SELECT MAX(convert (int, U_Version))
                      FROM [@RSM_MBOM]
                      WHERE U_ParentItem = N'{itemCode}'
                            AND U_SalesQuotationDocEntry = '{docEntry}') ");
            if (rec.EoF)
            {

                var cardCode = dataSourceOqut.GetValue("CardCode", 0);
                var dateString = dataSourceOqut.GetValue("DocDate", 0);
                var date = DateTime.ParseExact(dateString, "yyyyMMdd", CultureInfo.InvariantCulture);
                var costCenter = dataSourceQut1.GetValue("OcrCode", row - 1);
                var quantity = dataSourceQut1.GetValue("Quantity", row - 1);
                var ownerCode = dataSourceQut1.GetValue("OwnerCode", row - 1);
                Recordset recEmployee =(Recordset) DiManager.Company.GetBusinessObject(BoObjectTypes.BoRecordset);
                recEmployee.DoQuery($"SELECT Concat(FirstName, ' ', lastName) as [Owner Name] FROM OHEM WHERE empID = '{ownerCode}'");
                var ownerName = recEmployee.Fields.Item("Owner Name").Value.ToString();
                var docNum = dataSourceOqut.GetValue("DocNum", 0);
                var project = dataSourceOqut.GetValue("Project", 0);

                model = new MasterBomModel
                {
                    ProjectCode = project,
                    SalesQuotationDocEntry = (docEntry),
                    SalesQuotationDocNum = int.Parse(docNum),
                    Quantity = double.Parse(quantity, CultureInfo.InvariantCulture),
                    Currency = "GEL",
                    ExchangeRateDate = date,
                    Rate = 1.0,
                    CostCenter = costCenter,
                    ParentItem = itemCode,
                    CardCode = cardCode,
                    CreateDate = DateTime.Now,
                    OwnerCode = ownerName,
                    Version = "1"
                };

                _masterBomModels.Add(model);
                Recordset recSet = (Recordset)DiManager.Company.GetBusinessObject(BoObjectTypes.BoRecordset);
                recSet.DoQuery($"SELECT Code, U_Element FROM [@RSM_ELEM]");
                while (!recSet.EoF)
                {
                    MasterBomRowModel masterBomRow = new MasterBomRowModel
                    {
                        SalesQuotationDocEntry = model.SalesQuotationDocEntry.ToString(),
                        ParentItemCode = model.ParentItem,
                        Version = "1",
                        ElementID = recSet.Fields.Item("U_Element").Value.ToString()
                    };
                    model.Rows.Add(masterBomRow);
                    recSet.MoveNext();
                }
                return true;
            }
            else return false;
        }

        public bool FillModelsFromDb(int rowCount)
        {
            Recordset rec = (Recordset)DiManager.Company.GetBusinessObject(BoObjectTypes.BoRecordset);
            rec.DoQuery($"SELECT  distinct U_SalesQuotationDocEntry, U_ParentItem FROM [@RSM_MBOM] WHERE U_SalesQuotationDocEntry = {docEntry}");
            var recordCount = rec.RecordCount;
            rec.DoQuery(
                $@"SELECT  [@RSM_MBOM].Code [MBOMCode],[@RSM_MBOM_ROWS].Code [MBOMRowCode], [@RSM_MBOM].U_SalesQuotationDocEntry [SalesQuotationDocEntry], *
FROM [@RSM_MBOM]
     JOIN [@RSM_MBOM_ROWS] ON [@RSM_MBOM].U_SalesQuotationDocEntry = [@RSM_MBOM_ROWS].U_SalesQuotationDocEntry
                              AND [@RSM_MBOM].U_ParentItem = [@RSM_MBOM_ROWS].U_ParentItemCode
                              AND [@RSM_MBOM_ROWS].U_Version = [@RSM_MBOM].U_Version
WHERE [@RSM_MBOM].U_SalesQuotationDocEntry = '{docEntry}'
      AND  CONCAT([@RSM_MBOM_ROWS].U_Version,U_ParentItem) IN (    SELECT CONCAT(MAX(convert (int, U_Version)),U_ParentItemCode)
    FROM [@RSM_MBOM_ROWS]
    GROUP BY U_ParentItemCode, U_SalesQuotationDocEntry  having U_SalesQuotationDocEntry = '18')
 ");
            if (recordCount != rowCount)
            {
                return false;
            }
            List<MasterBomModel> models = new List<MasterBomModel>();
            List<MasterBomRowModel> rows = new List<MasterBomRowModel>();
            while (!rec.EoF)
            {
                MasterBomModel model = new MasterBomModel
                {
                    Code = rec.Fields.Item("MBOMCode").Value.ToString(),
                    ProjectCode = rec.Fields.Item("U_ProjectCode").Value.ToString(),
                    SalesQuotationDocEntry = rec.Fields.Item("U_SalesQuotationDocEntry").Value.ToString(),
                    SalesQuotationDocNum = (int)rec.Fields.Item("U_SalesQuotationDocNum").Value,
                    CostCenter = rec.Fields.Item("U_CostCenter").Value.ToString(),
                    ParentItem = rec.Fields.Item("U_ParentItemCode").Value.ToString(),
                    CardCode = rec.Fields.Item("U_CardCode").Value.ToString(),
                    CreateDate = (DateTime)rec.Fields.Item("U_CreateDate").Value,
                    OwnerCode = rec.Fields.Item("U_OwnerCode").Value.ToString(),
                    Version = rec.Fields.Item("U_Version").Value.ToString(),
                    Rate = (double)rec.Fields.Item("U_Rate").Value,
                    ExchangeRateDate = (DateTime)rec.Fields.Item("U_ExchangeRateDate").Value,
                    TotalSquareMeter = (double)rec.Fields.Item("U_TotalSquareMeter").Value,
                    PriceForSquareMeter = (double)rec.Fields.Item("U_PriceForSquareMeter").Value,
                    ReferenceFeePercentage = (double)rec.Fields.Item("U_ReferenceFeePercentage").Value,
                    Currency = rec.Fields.Item("U_Currency").Value.ToString(),
                    Quantity = (double)rec.Fields.Item("U_Quantity").Value
                };
                models.Add(model);
                MasterBomRowModel masterBomRowModel = new MasterBomRowModel();
                masterBomRowModel.Code = rec.Fields.Item("MBOMRowCode").Value.ToString();
                masterBomRowModel.Cost = (double)rec.Fields.Item("U_Cost").Value;
                masterBomRowModel.Price = (double)rec.Fields.Item("U_Price").Value;
                masterBomRowModel.Margin = (double)rec.Fields.Item("U_Margin").Value;
                masterBomRowModel.Percent = (double)rec.Fields.Item("U_Percent").Value;
                masterBomRowModel.FinalCustomerPrice = (double)rec.Fields.Item("U_FinalCustomerPrice").Value;
                masterBomRowModel.I = (double)rec.Fields.Item("U_I").Value;
                masterBomRowModel.II = (double)rec.Fields.Item("U_II").Value;
                masterBomRowModel.III = (double)rec.Fields.Item("U_III").Value;
                masterBomRowModel.SalesQuotationDocEntry =
                    rec.Fields.Item("SalesQuotationDocEntry").Value.ToString();
                masterBomRowModel.ParentItemCode = rec.Fields.Item("U_ParentItemCode").Value.ToString();
                masterBomRowModel.Version = rec.Fields.Item("U_Version").Value.ToString();
                masterBomRowModel.ElementID = rec.Fields.Item("U_ElementID").Value.ToString();
                rows.Add(masterBomRowModel);
                rec.MoveNext();
            }

            var headers = models.GroupBy(x => new { x.Version, x.ParentItem });
            foreach (var bomModels in headers)
            {
                bomModels.First().Rows.AddRange(rows.Where(x => x.ParentItemCode == bomModels.First().ParentItem));
                _masterBomModels.Add(bomModels.First());
            }
            return _masterBomModels.Count == rowCount;

        }
        public bool FillItemModelFromDb()
        {
            Recordset rec = (Recordset)DiManager.Company.GetBusinessObject(BoObjectTypes.BoRecordset);
            rec.DoQuery($@"SELECT [@RSM_MBOM].Code [MBOMCode],[@RSM_MBOM_ROWS].Code [MBOMRowCode], [@RSM_MBOM].U_SalesQuotationDocEntry [SalesQuotationDocEntry], * FROM [@RSM_MBOM] 
JOIN[@RSM_MBOM_ROWS] on[@RSM_MBOM].U_SalesQuotationDocEntry = [@RSM_MBOM_ROWS].U_SalesQuotationDocEntry AND[@RSM_MBOM].U_ParentItem = [@RSM_MBOM_ROWS].U_ParentItemCode AND [@RSM_MBOM_ROWS].U_Version = [@RSM_MBOM].U_Version 
 WHERE [@RSM_MBOM].U_SalesQuotationDocEntry = '{docEntry}' AND U_ParentItem = N'{itemCode}' AND [@RSM_MBOM].U_Version = (SELECT  MAX(convert (int, U_Version))
                      FROM [@RSM_MBOM]
                      WHERE U_ParentItem = N'{itemCode}'
                            AND U_SalesQuotationDocEntry = '{docEntry}') ");
            MasterBomModel model;
            if (!rec.EoF)
            {
                model = new MasterBomModel
                {
                    Code = rec.Fields.Item("MBOMCode").Value.ToString(),
                    ProjectCode = rec.Fields.Item("U_ProjectCode").Value.ToString(),
                    SalesQuotationDocEntry = rec.Fields.Item("U_SalesQuotationDocEntry").Value.ToString(),
                    SalesQuotationDocNum = (int)rec.Fields.Item("U_SalesQuotationDocNum").Value,
                    CostCenter = rec.Fields.Item("U_CostCenter").Value.ToString(),
                    ParentItem = itemCode,
                    CardCode = rec.Fields.Item("U_CardCode").Value.ToString(),
                    CreateDate = (DateTime)rec.Fields.Item("U_CreateDate").Value,
                    OwnerCode = rec.Fields.Item("U_OwnerCode").Value.ToString(),
                    Version = rec.Fields.Item("U_Version").Value.ToString(),
                    Rate = (double)rec.Fields.Item("U_Rate").Value,
                    PriceForSquareMeter = (double)rec.Fields.Item("U_PriceForSquareMeter").Value,
                    TotalSquareMeter = (double)rec.Fields.Item("U_TotalSquareMeter").Value,
                    ReferenceFeePercentage = (double)rec.Fields.Item("U_ReferenceFeePercentage").Value,
                    ExchangeRateDate = (DateTime)rec.Fields.Item("U_ExchangeRateDate").Value,
                    Currency = rec.Fields.Item("U_Currency").Value.ToString(),
                    Quantity = (double)rec.Fields.Item("U_Quantity").Value,
                };
                while (!rec.EoF)
                {
                    MasterBomRowModel masterBomRowModel = new MasterBomRowModel();

                    masterBomRowModel.Code = rec.Fields.Item("MBOMRowCode").Value.ToString();
                    masterBomRowModel.Cost = (double)rec.Fields.Item("U_Cost").Value;
                    masterBomRowModel.Price = (double)rec.Fields.Item("U_Price").Value;
                    masterBomRowModel.Margin = (double)rec.Fields.Item("U_Margin").Value;
                    masterBomRowModel.Percent = (double)rec.Fields.Item("U_Percent").Value;
                    masterBomRowModel.FinalCustomerPrice = (double)rec.Fields.Item("U_FinalCustomerPrice").Value;
                    masterBomRowModel.I = (double)rec.Fields.Item("U_I").Value;
                    masterBomRowModel.II = (double)rec.Fields.Item("U_II").Value;
                    masterBomRowModel.III = (double)rec.Fields.Item("U_III").Value;
                    masterBomRowModel.SalesQuotationDocEntry = rec.Fields.Item("SalesQuotationDocEntry").Value.ToString();
                    masterBomRowModel.ParentItemCode = rec.Fields.Item("U_ParentItemCode").Value.ToString();
                    masterBomRowModel.Version = rec.Fields.Item("U_Version").Value.ToString();
                    masterBomRowModel.ElementID = rec.Fields.Item("U_ElementID").Value.ToString();
                    model.Rows.Add(masterBomRowModel);
                    rec.MoveNext();
                }
                _masterBomModels.Add(model);
                return true;
            }
            return false;
        }

        private void Button0_PressedAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            var activeForm = SAPbouiCOM.Framework.Application.SBO_Application.Forms.ActiveForm;
            var dataSourceQut1 = activeForm.DataSources.DBDataSources.Item("QUT1");
            var dataSourceOqut = activeForm.DataSources.DBDataSources.Item("OQUT");
            var matrix = (Matrix)activeForm.Items.Item("38").Specific;
            var row = matrix.GetNextSelectedRow();
            if (row == -1)
            {
                return;
            }
            var bomType = dataSourceQut1.GetValue("TreeType", row - 1);
            if (bomType != "P")
            {
                SAPbouiCOM.Framework.Application.SBO_Application.MessageBox("საქონელი არ არის Bom-ის ტიპის");
                return;
            }
            itemCode = dataSourceQut1.GetValue("ItemCode", row - 1);
            docEntry = dataSourceQut1.GetValue("DocEntry", row - 1);
            _masterBomModels.Clear();
            var fromDb = FillItemModelFromDb();
            if (!fromDb)
            {
                GenerateModel(activeForm);
            }

            Pricing pricingForm = new Pricing();
            pricingForm.MasterBomModel = _masterBomModels.First();
            pricingForm.FillForm();
            pricingForm.Show();
        }

        private Button Button1;

        private void Button1_PressedAfter(object sboObject, SBOItemEventArg pVal)
        {
            var activeForm = SAPbouiCOM.Framework.Application.SBO_Application.Forms.ActiveForm;
            var dataSourceOqut = activeForm.DataSources.DBDataSources.Item("OQUT");
            var matrix = (Matrix)activeForm.Items.Item("38").Specific;
            docEntry = dataSourceOqut.GetValue("DocEntry", 0);
            _masterBomModels.Clear();
            var fromDb = FillModelsFromDb(matrix.RowCount - 1);
            if (!fromDb)
            {
                SAPbouiCOM.Framework.Application.SBO_Application.SetStatusBarMessage("ჯერ გააკეთეთ განფასება",
                    BoMessageTime.bmt_Short,
                    true);
                return;
            }
            CommonElements commonElements = new CommonElements(_masterBomModels);
            commonElements.Show();
        }
    }
}
