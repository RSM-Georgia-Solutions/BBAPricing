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

namespace BBAPricing.System_Forms
{
    [FormAttribute("149", "System Forms/SalesQuotation.b1f")]
    class SalesQuotation : SystemFormBase
    {
        public SalesQuotation()
        {
        }

        /// <summary>
        /// Initialize components. Called by framework after form created.
        /// </summary>
        public override void OnInitializeComponent()
        {
            this.Button0 = ((SAPbouiCOM.Button)(this.GetItem("Item_0").Specific));
            this.Button0.PressedAfter += new SAPbouiCOM._IButtonEvents_PressedAfterEventHandler(this.Button0_PressedAfter);
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
                SAPbouiCOM.Framework.Application.SBO_Application.MessageBox("საქონელი არა არის Bom");
                return;
            }

            var itemCode = dataSourceQut1.GetValue("ItemCode", row - 1);
            var docEntry = dataSourceQut1.GetValue("DocEntry", row - 1);
            MasterBomModel model = new MasterBomModel();
            Recordset rec = (Recordset)DiManager.Company.GetBusinessObject(BoObjectTypes.BoRecordset);
            rec.DoQuery($@"SELECT [@RSM_MBOM].Code [MBOMCode],[@RSM_MBOM_ROWS].Code [MBOMRowCode], [@RSM_MBOM].U_SalesQuotationDocEntry [SalesQuotationDocEntry], * FROM [@RSM_MBOM] 
JOIN[@RSM_MBOM_ROWS] on[@RSM_MBOM].U_SalesQuotationDocEntry = [@RSM_MBOM_ROWS].U_SalesQuotationDocEntry AND[@RSM_MBOM].U_ParentItem = [@RSM_MBOM_ROWS].U_ParentItemCode
 WHERE [@RSM_MBOM].U_SalesQuotationDocEntry = '{docEntry}' AND U_ParentItem = '{itemCode}' AND [@RSM_MBOM].U_Version = (SELECT MAX(U_Version)
                      FROM [@RSM_MBOM]
                      WHERE U_ParentItem = '{itemCode}'
                            AND U_SalesQuotationDocEntry = '{docEntry}') ");
            if (rec.EoF)
            {
                var cardCode = dataSourceOqut.GetValue("CardCode", row - 1);
                var costCenter = dataSourceQut1.GetValue("OcrCode", row - 1);
                var quantity = dataSourceQut1.GetValue("Quantity", row - 1);
                var docNum = dataSourceOqut.GetValue("DocNum", row - 1);
                var project = dataSourceOqut.GetValue("Project", row - 1);

                model = new MasterBomModel
                {
                    ProjectCode = project,
                    SalesQuotationDocEntry = (docEntry),
                    SalesQuotationDocNum = int.Parse(docNum),
                    Quantity = double.Parse(quantity),
                    CostCenter = costCenter,
                    ParentItem = itemCode,
                    CardCode = cardCode,
                    CreateDate = DateTime.Now,
                    OwnerCode = DiManager.Company.UserName,
                    Version = "1"                    
                };

                var res = model.Add();
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
                    masterBomRow.Add();
                    recSet.MoveNext();
                }
            }
            else
            {
                model = new MasterBomModel
                {
                    Code = rec.Fields.Item("MBOMCode").Value.ToString(),
                    ProjectCode = rec.Fields.Item("U_ProjectCode").Value.ToString(),
                    SalesQuotationDocEntry = rec.Fields.Item("U_SalesQuotationDocEntry").Value.ToString(),
                    SalesQuotationDocNum = (int)rec.Fields.Item("U_SalesQuotationDocNum").Value,
                    CostCenter = rec.Fields.Item("U_CostCenter").Value.ToString(),
                    ParentItem = itemCode,
                    CardCode = rec.Fields.Item("U_ParentItem").Value.ToString(),
                    CreateDate = (DateTime)rec.Fields.Item("U_CreateDate").Value,
                    OwnerCode = rec.Fields.Item("U_OwnerCode").Value.ToString(),
                    Version = rec.Fields.Item("U_Version").Value.ToString()
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
                    model.Rows.Add(masterBomRowModel);
                    rec.MoveNext();
                }
            }

            Pricing pricingForm = new Pricing();
            pricingForm.Refresh(model);
            pricingForm.Show();
        }
    }
}
