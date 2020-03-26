using BBAPricing.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAPbobsCOM;
using SAPbouiCOM;
using Application = SAPbouiCOM.Framework.Application;

namespace BBAPricing.FormControllers
{
    public class CalculationManufacturingOverheadsController
    {
        public bool HasErrors { get; set; }
        private readonly MasterBomModel MasterBomModel;
        private OverheadModel OverheadModel;
        private OverheadModel OverheadModelDb;

        private new readonly IForm Form;

        public CalculationManufacturingOverheadsController(MasterBomModel masterBomModel, IForm form)
        {
            MasterBomModel = masterBomModel;
            Form = form;
            OverheadModel = new OverheadModel();
            OverheadModelDb = new OverheadModel();
        }
        public static Action RefreshBom;
        public void CalculateAdministrativeOverheads()
        {
            bool fromDb = FillModelFromDb();
            GenerateModel();
            if (HasErrors)
            {
                return;
            }
            bool isChanged = CompareVersions();
            OverheadModel.AddOrUpdate();
            FillGridFromModel(Form);
            if (isChanged)
            {
                if (fromDb)
                {
                    IncrementMasterVersion();
                }
                else
                {
                    MasterBomModel.Update();
                }
                RefreshBom.Invoke();
            }
        }

        private bool CompareVersions()
        {
            return OverheadModelDb.RequiredResource != OverheadModel.RequiredResource
                   || OverheadModelDb.UnitCost != OverheadModel.UnitCost
                   || OverheadModelDb.TotalCost != OverheadModel.TotalCost;
        }

        private void IncrementMasterVersion()
        {
            string version = (int.Parse(MasterBomModel.Version, CultureInfo.InvariantCulture) + 1).ToString();
            MasterBomModel.Version = version;
            foreach (var row in MasterBomModel.Rows)
            {
                row.Version = version;
            }
            MasterBomModel.Add();
        }

        private void FillGridFromModel(IForm form)
        {
            ((EditText)form.Items.Item("Item_1").Specific).Value = OverheadModel.RequiredResource.ToString(CultureInfo.InvariantCulture);
            ((EditText)form.Items.Item("Item_3").Specific).Value = OverheadModel.UnitCost.ToString(CultureInfo.InvariantCulture);
            ((EditText)form.Items.Item("Item_5").Specific).Value = OverheadModel.TotalCost.ToString(CultureInfo.InvariantCulture);
        }

        private void GenerateModel()
        {
            Recordset recForCmp =
                (Recordset)DiManager.Company.GetBusinessObject(BoObjectTypes.BoRecordset);
            recForCmp.DoQuery($"select * from [@RSM_OVERHEADS_R] WHERE U_ComponentId = N'Manufacturing Overhead 1 კაც/საათზე'");
            Recordset recSet =
                (Recordset)DiManager.Company.GetBusinessObject(BoObjectTypes.BoRecordset);
            string query = $"SELECT * FROM [@RSM_RESOURCES] JOIN OITM ON OITM.ItemCode = U_ParentItemCode JOIN ORSC ON ORSC.VisResCode =  [@RSM_RESOURCES].U_resourcecode   WHERE U_Version = (SELECT MAX(U_Version)FROM[@RSM_RESOURCES]GROUP BY U_ParentItemCode,U_SalesQuotationDocEntry Having U_SalesQuotationDocEntry = {MasterBomModel.SalesQuotationDocEntry} AND U_ParentItemCode = N'{MasterBomModel.ParentItem}') AND U_SalesQuotationDocEntry = {MasterBomModel.SalesQuotationDocEntry} AND U_ParentItemCode  = N'{MasterBomModel.ParentItem}' AND ORSC.ResType = 'L'";
            recSet.DoQuery(query);
            var type = recSet.Fields.Item("U_SBU").Value.ToString();
            double requiredResource = 0;
            double unitCost = 0;
            if (string.IsNullOrWhiteSpace(type))
            {
                Application.SBO_Application.SetStatusBarMessage($"SBU არ არის არჩეული საქონელი - {MasterBomModel.ParentItem}",
                    BoMessageTime.bmt_Short,
                    true);
                HasErrors = true;
                return;
            }

            HasErrors = false;
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
            HasErrors = false;
            while (!recSet.EoF)
            {
                // var timeRequired = (double)recSet.Fields.Item("U_TimeRequired").Value;
                var quantity = (double)recSet.Fields.Item("U_Quantity").Value;
                requiredResource += quantity;
                recSet.MoveNext();
            }
            double totalCost = requiredResource * unitCost;
            OverheadModel overheadModel = new OverheadModel();

            if (MasterBomModel.Currency != "GEL")
            {
                overheadModel.TotalCost /= MasterBomModel.Rate;
                overheadModel.UnitCost /= MasterBomModel.Rate;
            }
            else
            {
                overheadModel.UnitCost = unitCost;
                overheadModel.TotalCost = totalCost;
            }
            overheadModel.RequiredResource = requiredResource;
            overheadModel.Version = MasterBomModel.Version;
            overheadModel.ParentItemCode = MasterBomModel.ParentItem;
            overheadModel.SalesQuotationDocEntry = MasterBomModel.SalesQuotationDocEntry;
            overheadModel.OverheadType = "Manufacturing";
            OverheadModel = overheadModel;
            var mtrlLine = MasterBomModel.Rows.First(x => x.ElementID == "Manufacturing Overheads");
            mtrlLine.Cost = totalCost;
            mtrlLine.Price = totalCost;
            mtrlLine.Margin = totalCost;
            mtrlLine.FinalCustomerPrice = totalCost;
        }


        private bool FillModelFromDb()
        {
            Recordset recSet = (Recordset)DiManager.Company.GetBusinessObject(BoObjectTypes.BoRecordset);
            string query = $"select * from [@RSM_OVERHEADS_SQ] WHERE U_Version = '{MasterBomModel.Version}' " +
                           $"AND U_SalesQuotationDocEntry = {MasterBomModel.SalesQuotationDocEntry}" +
                           $" AND U_ParentItemCode = N'{MasterBomModel.ParentItem}' " +
                           $"AND U_OverheadType = 'Manufacturing'";
            recSet.DoQuery(query);
            if (!recSet.EoF)
            {
                OverheadModel model = new OverheadModel();
                model.ParentItemCode = recSet.Fields.Item("U_ParentItemCode").Value.ToString();
                model.SalesQuotationDocEntry = recSet.Fields.Item("U_SalesQuotationDocEntry").Value.ToString();
                model.Version = recSet.Fields.Item("U_Version").Value.ToString();
                model.RequiredResource = (double)recSet.Fields.Item("U_RequiredResource").Value;
                model.UnitCost = (double)recSet.Fields.Item("U_UnitCost").Value;
                model.TotalCost = (double)recSet.Fields.Item("U_TotalCost").Value;
                model.OverheadType = recSet.Fields.Item("U_OverheadType").Value.ToString();
                OverheadModelDb = model;
                return true;
            }
            return false;

        }

        private Grid Grid => (Grid)Form.Items.Item("Item_0").Specific;
    }
}
