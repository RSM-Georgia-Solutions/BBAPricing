using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BBAPricing.Models;
using SAPbobsCOM;
using SAPbouiCOM;
using Application = SAPbouiCOM.Framework.Application;

namespace BBAPricing.FormControllers
{
    public class CommonElementsController
    {
        private readonly IForm Form;
        private readonly List<MasterBomModel> MasterBomModel;
        public CommonElementsModel _Model;

        public CommonElementsController(IForm form, List<MasterBomModel> masterBomModel)
        {
            Form = form;
            MasterBomModel = masterBomModel;
            _Model = new CommonElementsModel();
        }

        public bool FillModelFromDb()
        {
            Recordset recSet = (Recordset)DiManager.Company.GetBusinessObject(BoObjectTypes.BoRecordset);
            recSet.DoQuery($"SELECT * FROM [@RSM_COMMON_ELEM]");
            if (recSet.EoF)
            {
                return false;
            }
            _Model.ProjectCode = recSet.Fields.Item("U_ProjectCode").Value.ToString();
            _Model.ChangeDate = (DateTime)recSet.Fields.Item("U_ChangeDate").Value;
            _Model.SalesQuotationDocEntry = recSet.Fields.Item("U_SalesQuotationDocEntry").Value.ToString();
            _Model.EmployeeQuantity = (double)recSet.Fields.Item("U_EmployeeQuantity").Value;
            _Model.DailyNormPerPerson = (double)recSet.Fields.Item("U_DailyNormPerPerson").Value;
            _Model.QuantityOfDays = (double)recSet.Fields.Item("U_QuantityOfDays").Value;
            _Model.HostelCostPerDay = (double)recSet.Fields.Item("U_HostelCostPerDay").Value;
            _Model.TotalHotelCost = (double)recSet.Fields.Item("U_TotalHotelCost").Value;
            _Model.TotalCost = (double)recSet.Fields.Item("U_TotalCost").Value;
            _Model.TransportationAmount = (double)recSet.Fields.Item("U_TransportationAmount").Value;
            return true;
        }

        public void FillFormFromModel()
        {
            SAPbouiCOM.Framework.Application.SBO_Application.Forms.ActiveForm.Freeze(true);
            ((StaticText)Form.Items.Item("Item_25").Specific).Caption = _Model.ProjectCode.ToString(CultureInfo.InvariantCulture);
            ((StaticText)Form.Items.Item("Item_22").Specific).Caption = _Model.ChangeDate.ToString("MM/dd/yyyy");
            ((StaticText)Form.Items.Item("Item_24").Specific).Caption = _Model.SalesQuotationDocEntry.ToString(CultureInfo.InvariantCulture);
            ((EditText)Form.Items.Item("Item_1").Specific).Value = _Model.TransportationAmount.ToString(CultureInfo.InvariantCulture);
            ((EditText)Form.Items.Item("Item_11").Specific).Value = _Model.EmployeeQuantity.ToString(CultureInfo.InvariantCulture);
            ((EditText)Form.Items.Item("Item_12").Specific).Value = _Model.DailyNormPerPerson.ToString(CultureInfo.InvariantCulture);
            ((EditText)Form.Items.Item("Item_13").Specific).Value = _Model.QuantityOfDays.ToString(CultureInfo.InvariantCulture);
            ((EditText)Form.Items.Item("Item_14").Specific).Value = _Model.HostelCostPerDay.ToString(CultureInfo.InvariantCulture);
            ((EditText)Form.Items.Item("Item_15").Specific).Value = _Model.TotalHotelCost.ToString(CultureInfo.InvariantCulture);
            ((EditText)Form.Items.Item("Item_16").Specific).Value = _Model.TotalCost.ToString(CultureInfo.InvariantCulture);
            Application.SBO_Application.Forms.ActiveForm.Freeze(false);
        }

        public void Refresh()
        {
            var qtyofDays = double.Parse(((EditText)Form.Items.Item("Item_13").Specific).Value, CultureInfo.InvariantCulture);
            var hotelCostPerDay = double.Parse(((EditText)Form.Items.Item("Item_14").Specific).Value, CultureInfo.InvariantCulture);
            var totalHotelCost = qtyofDays * hotelCostPerDay;

            var employeeQty = double.Parse(((EditText)Form.Items.Item("Item_11").Specific).Value, CultureInfo.InvariantCulture);
            var dailyNormPerPerson = double.Parse(((EditText)Form.Items.Item("Item_12").Specific).Value, CultureInfo.InvariantCulture);
            _Model.TotalHotelCost = totalHotelCost;
            _Model.TotalCost = totalHotelCost + qtyofDays * employeeQty * dailyNormPerPerson;

            ((EditText)Form.Items.Item("Item_15").Specific).Value = _Model.TotalHotelCost.ToString(CultureInfo.InvariantCulture);
            ((EditText)Form.Items.Item("Item_16").Specific).Value = _Model.TotalCost.ToString(CultureInfo.InvariantCulture);
        }

        public void HendldeSettings()
        {
            FillModelFromForm();
            FillFormFromModel();
            _Model.AddOrUpdate();
        }

        private void FillModelFromForm()
        {
            var createDate = ((StaticText)Form.Items.Item("Item_22").Specific).Caption;
            _Model.ChangeDate = string.IsNullOrWhiteSpace(createDate) ? DateTime.Now : DateTime.ParseExact(createDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            _Model.ProjectCode = ((StaticText)Form.Items.Item("Item_25").Specific).Caption;
            _Model.SalesQuotationDocEntry = ((StaticText)Form.Items.Item("Item_24").Specific).Caption;
            _Model.TransportationAmount = double.Parse(((EditText)Form.Items.Item("Item_1").Specific).Value, CultureInfo.InvariantCulture);
            _Model.EmployeeQuantity = double.Parse(((EditText)Form.Items.Item("Item_11").Specific).Value, CultureInfo.InvariantCulture);
            _Model.DailyNormPerPerson = double.Parse(((EditText)Form.Items.Item("Item_12").Specific).Value, CultureInfo.InvariantCulture);
            _Model.QuantityOfDays = double.Parse(((EditText)Form.Items.Item("Item_13").Specific).Value, CultureInfo.InvariantCulture);
            _Model.HostelCostPerDay = double.Parse(((EditText)Form.Items.Item("Item_14").Specific).Value, CultureInfo.InvariantCulture);
            _Model.TotalHotelCost = double.Parse(((EditText)Form.Items.Item("Item_15").Specific).Value, CultureInfo.InvariantCulture);
            _Model.TotalCost = double.Parse(((EditText)Form.Items.Item("Item_16").Specific).Value, CultureInfo.InvariantCulture);
            Refresh();
        }

        public void GenerateModel()
        {
            _Model.ChangeDate = DateTime.Now;
            _Model.ProjectCode = MasterBomModel.First().ProjectCode;
            _Model.SalesQuotationDocEntry = MasterBomModel.First().SalesQuotationDocEntry;
        }

        public void UpdateMbom()
        {
            Recordset recSet =
                (Recordset)DiManager.Company.GetBusinessObject(BoObjectTypes.BoRecordset);
            recSet.DoQuery($"SELECT SUM(U_Cost) as [Sum], U_ParentItemCode FROM [@RSM_MBOM_ROWS]" +
                           $" WHERE U_SalesQuotationDocentry = '{MasterBomModel.First().SalesQuotationDocEntry}'" +
                           $" AND U_Version in (SELECT MAX(U_Version)FROM[@RSM_MBOM_ROWS]GROUP BY U_ParentItemCode) " +
                           $" AND U_ElementID in (N'Administrative Overheads',N'Human Resources',N'Machinery Resources',N'Manufacturing Overheads',N'MTRLs')" +
                           $" group by U_ParentItemCode");
            double totalSum = 0;
            while (!recSet.EoF)
            {
                var totalCostPerItem = (double)recSet.Fields.Item("Sum").Value;
                totalSum += totalCostPerItem;
                recSet.MoveNext();
            }
            recSet.MoveFirst();
            foreach (MasterBomModel masterBomModel in MasterBomModel)
            {
                var sum = (double) recSet.Fields.Item("Sum").Value;
                var trip = _Model.TotalCost / totalSum * sum;
                var transport = _Model.TransportationAmount / totalSum * (double)recSet.Fields.Item("Sum").Value;
                var mBomTripRow = masterBomModel.Rows.First(x => x.ElementID == "Business Trip" && x.ParentItemCode == masterBomModel.ParentItem);
                var mBomTransportRow = masterBomModel.Rows.First(x => x.ElementID == "Transportation" && x.ParentItemCode == masterBomModel.ParentItem);
                mBomTripRow.Cost = trip;
                mBomTransportRow.Cost = transport;
                recSet.MoveNext();
                masterBomModel.Update();
            }
        }
    }
}
