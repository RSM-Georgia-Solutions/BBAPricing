using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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
        private readonly CommonElementsModel Model;

        public CommonElementsController(IForm form, List<MasterBomModel> masterBomModel)
        {
            Form = form;
            MasterBomModel = masterBomModel;
            Model = new CommonElementsModel();
        }

        public bool FillModelFromDb()
        {
            Recordset recSet = (Recordset)DiManager.Company.GetBusinessObject(BoObjectTypes.BoRecordset);
            recSet.DoQuery($"SELECT * FROM [@RSM_COMMON_ELEM] WHERE U_SalesQuotationDocEntry = {MasterBomModel.First().SalesQuotationDocEntry}");
            if (recSet.EoF)
            {
                return false;
            }
            Model.ProjectCode = recSet.Fields.Item("U_ProjectCode").Value.ToString();
            Model.ChangeDate = (DateTime)recSet.Fields.Item("U_ChangeDate").Value;
            Model.SalesQuotationDocEntry = recSet.Fields.Item("U_SalesQuotationDocEntry").Value.ToString();
            Model.EmployeeQuantity = (double)recSet.Fields.Item("U_EmployeeQuantity").Value;
            Model.DailyNormPerPerson = (double)recSet.Fields.Item("U_DailyNormPerPerson").Value;
            Model.QuantityOfDays = (double)recSet.Fields.Item("U_QuantityOfDays").Value;
            Model.HostelCostPerDay = (double)recSet.Fields.Item("U_HostelCostPerDay").Value;
            Model.TotalHotelCost = (double)recSet.Fields.Item("U_TotalHotelCost").Value;
            Model.TotalCost = (double)recSet.Fields.Item("U_TotalCost").Value;
            Model.TransportationAmount = (double)recSet.Fields.Item("U_TransportationAmount").Value;
            return true;
        }

        public void FillFormFromModel()
        {
            Application.SBO_Application.Forms.ActiveForm.Freeze(true);
            ((StaticText)Form.Items.Item("Item_25").Specific).Caption = Model.ProjectCode.ToString(CultureInfo.InvariantCulture);
            ((StaticText)Form.Items.Item("Item_22").Specific).Caption = Model.ChangeDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
            ((StaticText)Form.Items.Item("Item_24").Specific).Caption = Model.SalesQuotationDocEntry.ToString(CultureInfo.InvariantCulture);
            ((EditText)Form.Items.Item("Item_1").Specific).Value = Model.TransportationAmount.ToString(CultureInfo.InvariantCulture);
            ((EditText)Form.Items.Item("Item_11").Specific).Value = Model.EmployeeQuantity.ToString(CultureInfo.InvariantCulture);
            ((EditText)Form.Items.Item("Item_12").Specific).Value = Model.DailyNormPerPerson.ToString(CultureInfo.InvariantCulture);
            ((EditText)Form.Items.Item("Item_13").Specific).Value = Model.QuantityOfDays.ToString(CultureInfo.InvariantCulture);
            ((EditText)Form.Items.Item("Item_14").Specific).Value = Model.HostelCostPerDay.ToString(CultureInfo.InvariantCulture);
            ((EditText)Form.Items.Item("Item_15").Specific).Value = Model.TotalHotelCost.ToString(CultureInfo.InvariantCulture);
            ((EditText)Form.Items.Item("Item_16").Specific).Value = Model.TotalCost.ToString(CultureInfo.InvariantCulture);
            Application.SBO_Application.Forms.ActiveForm.Freeze(false);
        }

        public void Refresh()
        {
            var qtyofDays = double.Parse(((EditText)Form.Items.Item("Item_13").Specific).Value, CultureInfo.InvariantCulture);
            var hotelCostPerDay = double.Parse(((EditText)Form.Items.Item("Item_14").Specific).Value, CultureInfo.InvariantCulture);
            var totalHotelCost = qtyofDays * hotelCostPerDay;

            var employeeQty = double.Parse(((EditText)Form.Items.Item("Item_11").Specific).Value, CultureInfo.InvariantCulture);
            var dailyNormPerPerson = double.Parse(((EditText)Form.Items.Item("Item_12").Specific).Value, CultureInfo.InvariantCulture);
            Model.TotalHotelCost = totalHotelCost;
            Model.TotalCost = totalHotelCost + qtyofDays * employeeQty * dailyNormPerPerson;

            ((EditText)Form.Items.Item("Item_15").Specific).Value = Model.TotalHotelCost.ToString(CultureInfo.InvariantCulture);
            ((EditText)Form.Items.Item("Item_16").Specific).Value = Model.TotalCost.ToString(CultureInfo.InvariantCulture);
        }

        public void HandldeSettings()
        {
            FillModelFromForm();
            FillFormFromModel();
            Model.AddOrUpdate();
        }

        private void FillModelFromForm()
        {
            var createDate = ((StaticText)Form.Items.Item("Item_22").Specific).Caption;
            Model.ChangeDate = string.IsNullOrWhiteSpace(createDate) ? DateTime.Now : DateTime.ParseExact(createDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            Model.ProjectCode = ((StaticText)Form.Items.Item("Item_25").Specific).Caption;
            Model.SalesQuotationDocEntry = ((StaticText)Form.Items.Item("Item_24").Specific).Caption;
            Model.TransportationAmount = double.Parse(((EditText)Form.Items.Item("Item_1").Specific).Value, CultureInfo.InvariantCulture);
            Model.EmployeeQuantity = double.Parse(((EditText)Form.Items.Item("Item_11").Specific).Value, CultureInfo.InvariantCulture);
            Model.DailyNormPerPerson = double.Parse(((EditText)Form.Items.Item("Item_12").Specific).Value, CultureInfo.InvariantCulture);
            Model.QuantityOfDays = double.Parse(((EditText)Form.Items.Item("Item_13").Specific).Value, CultureInfo.InvariantCulture);
            Model.HostelCostPerDay = double.Parse(((EditText)Form.Items.Item("Item_14").Specific).Value, CultureInfo.InvariantCulture);
            Model.TotalHotelCost = double.Parse(((EditText)Form.Items.Item("Item_15").Specific).Value, CultureInfo.InvariantCulture);
            Model.TotalCost = double.Parse(((EditText)Form.Items.Item("Item_16").Specific).Value, CultureInfo.InvariantCulture);
            Refresh();
        }

        public void GenerateModel()
        {
            Model.ChangeDate = DateTime.Now;
            Model.ProjectCode = MasterBomModel.First().ProjectCode;
            Model.SalesQuotationDocEntry = MasterBomModel.First().SalesQuotationDocEntry;
            Model.DailyNormPerPerson = Settings.DailyNormPerPerson;
        }

        public void UpdateMbom()
        {
            Recordset recSet =
                (Recordset)DiManager.Company.GetBusinessObject(BoObjectTypes.BoRecordset);
            string queryStr = $"SELECT SUM(U_Cost* U_Quantity) as [Sum], U_ParentItemCode, [@RSM_MBOM_ROWS].U_Version FROM [@RSM_MBOM_ROWS]  join [@RSM_MBOM] on " +
                              $"[@RSM_MBOM].U_SalesQuotationDocEntry = [@RSM_MBOM_ROWS].U_SalesQuotationDocEntry" +
                              $" AND [@RSM_MBOM].U_ParentItem = [@RSM_MBOM_ROWS].U_ParentItemCode " +
                              $" AND [@RSM_MBOM].U_Version = [@RSM_MBOM_ROWS].U_Version" +
                              $" WHERE [@RSM_MBOM_ROWS].U_SalesQuotationDocentry = '{MasterBomModel.First().SalesQuotationDocEntry}'" +
                              $" AND [@RSM_MBOM_ROWS].U_Version in (SELECT MAX(convert (int, U_Version)) FROM [@RSM_MBOM_ROWS]  where  U_SalesQuotationDocentry = '{MasterBomModel.First().SalesQuotationDocEntry}' GROUP BY U_ParentItemCode) " +
                              $" AND U_ElementID in" +
                              $" (N'Administrative Overheads',N'Human Resources',N'Machinery Resources',N'Manufacturing Overheads',N'MTRLs', N'Material OverHeads', N'SalaryFund')" +
                              $" Group By U_ParentItemCode, [@RSM_MBOM_ROWS].U_Version";
            recSet.DoQuery(queryStr);

            int versionCheck = 0;
            string item = string.Empty;
            while (!recSet.EoF)
            {
                string vrs = recSet.Fields.Item("U_Version").Value.ToString();
                string itm = recSet.Fields.Item("U_ParentItemCode").Value.ToString();
                int vrs2 = int.Parse(vrs);
                if (vrs2 > versionCheck)
                {
                    versionCheck = vrs2;
                    item = itm;
                }
                recSet.MoveNext();
            }

            double totalCostForCommonElems = 0;
            Dictionary<string, double> parentItemsAndSums = new Dictionary<string, double>();
            recSet.MoveFirst();
            while (!recSet.EoF)
            {
                var totalCostPerItem = (double)recSet.Fields.Item("Sum").Value;
                string parentItem = recSet.Fields.Item("U_ParentItemCode").Value.ToString();
                string versionXz = recSet.Fields.Item("U_Version").Value.ToString();
                int versionXzInt = int.Parse(versionXz);
                if (parentItem == item && versionXzInt != versionCheck)
                {
                    recSet.MoveNext();
                    continue;
                }
                parentItemsAndSums.Add(parentItem, totalCostPerItem);
                totalCostForCommonElems += totalCostPerItem;
                recSet.MoveNext();
            }

            recSet.MoveFirst();
            int lineId = 0;
            foreach (MasterBomModel masterBomModel in MasterBomModel)
            {
                double trip = 0;
                double transport = 0;
                if (parentItemsAndSums.Count != 0)
                {
                    trip = Math.Round(Model.TotalCost / totalCostForCommonElems * parentItemsAndSums[masterBomModel.ParentItem], 4);
                    transport = Math.Round(Model.TransportationAmount / totalCostForCommonElems * parentItemsAndSums[masterBomModel.ParentItem], 4);
                }

                var mBomTripRow = masterBomModel.Rows.First(x => x.ElementID == "Business Trip" && x.ParentItemCode == masterBomModel.ParentItem);
                var mBomTransportRow = masterBomModel.Rows.First(x => x.ElementID == "Transportation" && x.ParentItemCode == masterBomModel.ParentItem);
                var mBomTotals = masterBomModel.Rows.First(x => x.ElementID == "Totals" && x.ParentItemCode == masterBomModel.ParentItem);

                mBomTripRow.Cost = Math.Round(trip, 4);
                mBomTripRow.Price = Math.Round(trip, 4);
                mBomTripRow.Margin = Math.Round(trip, 4);
                mBomTripRow.FinalCustomerPrice = Math.Round(trip, 4);
                mBomTransportRow.Cost = Math.Round(transport, 4);
                mBomTransportRow.Price = Math.Round(transport, 4);
                mBomTransportRow.Margin = Math.Round(transport, 4);
                mBomTransportRow.FinalCustomerPrice = Math.Round(transport, 4);


                mBomTripRow.Percent = Math.Round(mBomTripRow.Cost / mBomTotals.Cost * 100, 4);
                mBomTransportRow.Percent = Math.Round(mBomTransportRow.Cost / mBomTotals.Cost * 100, 4);


                recSet.MoveNext();



                mBomTripRow.I = mBomTripRow.Margin / mBomTotals.Margin * 100;
                mBomTransportRow.I = mBomTransportRow.Margin / mBomTotals.Margin * 100;



                mBomTripRow.II = mBomTripRow.Margin / mBomTripRow.FinalCustomerPrice * 100;
                mBomTransportRow.II = mBomTransportRow.Margin / mBomTransportRow.FinalCustomerPrice * 100;



                mBomTripRow.III = mBomTripRow.Margin / mBomTotals.FinalCustomerPrice * 100;
                mBomTransportRow.III = mBomTransportRow.Margin / mBomTotals.FinalCustomerPrice * 100;

                var salesQuotation = (Documents)DiManager.Company.GetBusinessObject(BoObjectTypes.oQuotations);
                salesQuotation.GetByKey(int.Parse(masterBomModel.SalesQuotationDocEntry));
                salesQuotation.Lines.SetCurrentLine(lineId);
                salesQuotation.Lines.UnitPrice = Math.Round(MasterBomModel
                    .First(i => i.ParentItem == salesQuotation.Lines.ItemCode).PriceForSquareMeter / 1.18, 4);
                salesQuotation.Update();
                lineId++;
                string version = (int.Parse(masterBomModel.Version, CultureInfo.InvariantCulture) + 1).ToString();
                masterBomModel.Version = version;
                foreach (var row in masterBomModel.Rows)
                {
                    row.Version = version;
                }
                masterBomModel.Add();
            }
        }
    }
}
