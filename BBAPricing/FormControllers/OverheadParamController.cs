using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BBAPricing.Iterfaces;
using BBAPricing.Models;
using SAPbobsCOM;
using SAPbouiCOM;
using Application = SAPbouiCOM.Framework.Application;

namespace BBAPricing.FormControllers
{
    public class OverheadParamController
    {
        public OverheadParamsModel DbModel;
        public IForm Form { get; set; }
        public OverheadParamsModel FormModel
        {
            get
            {
                OverheadParamsModel model = new OverheadParamsModel();
                model.Version = string.IsNullOrWhiteSpace(Form.DataSources.DBDataSources.Item(0).GetValue("U_Version",
                    Form.DataSources.DBDataSources.Item(0).Offset))
                    ? "1"
                    : Form.DataSources.DBDataSources.Item(0).GetValue("U_Version",
                        Form.DataSources.DBDataSources.Item(0).Offset);
                model.DailyWorkHours = double.Parse(Form.DataSources.DBDataSources.Item(0).GetValue("U_DailyWorkHours",
                    Form.DataSources.DBDataSources.Item(0).Offset), CultureInfo.InvariantCulture);
                model.WorkingDaysMonthly = double.Parse(Form.DataSources.DBDataSources.Item(0).GetValue("U_WorkingDaysMonthly",
                    Form.DataSources.DBDataSources.Item(0).Offset), CultureInfo.InvariantCulture);
                model.Corian = double.Parse(Form.DataSources.DBDataSources.Item(0).GetValue("U_Corian",
                    Form.DataSources.DBDataSources.Item(0).Offset), CultureInfo.InvariantCulture);
                model.Neolith = double.Parse(Form.DataSources.DBDataSources.Item(0).GetValue("U_Neolith",
                    Form.DataSources.DBDataSources.Item(0).Offset), CultureInfo.InvariantCulture);
                model.Furniture = double.Parse(Form.DataSources.DBDataSources.Item(0).GetValue("U_Furniture",
                    Form.DataSources.DBDataSources.Item(0).Offset), CultureInfo.InvariantCulture);
                model.AdministrativeOverhead = double.Parse(Form.DataSources.DBDataSources.Item(0).GetValue(
                    "U_AdministrativeOverhead",
                    Form.DataSources.DBDataSources.Item(0).Offset), CultureInfo.InvariantCulture);
                model.ManufacuringOverhead = double.Parse(Form.DataSources.DBDataSources.Item(0).GetValue(
                    "U_ManufacuringOverhead",
                    Form.DataSources.DBDataSources.Item(0).Offset), CultureInfo.InvariantCulture);
                model.CorianEmployee = double.Parse(Form.DataSources.DBDataSources.Item(0).GetValue("U_CorianEmployee",
                    Form.DataSources.DBDataSources.Item(0).Offset), CultureInfo.InvariantCulture);
                model.FurnitureEmployee = double.Parse(Form.DataSources.DBDataSources.Item(0).GetValue(
                    "U_FurnitureEmployee",
                    Form.DataSources.DBDataSources.Item(0).Offset), CultureInfo.InvariantCulture);
                model.NeolithEmployee = double.Parse(Form.DataSources.DBDataSources.Item(0).GetValue("U_NeolithEmployee",
                    Form.DataSources.DBDataSources.Item(0).Offset), CultureInfo.InvariantCulture);
                model.TotalEmps = double.Parse(Form.DataSources.DBDataSources.Item(0).GetValue("U_TotalEmps",
                    Form.DataSources.DBDataSources.Item(0).Offset), CultureInfo.InvariantCulture);
                model.ChangeDate = DateTime.ParseExact(string.IsNullOrWhiteSpace(Form.DataSources.DBDataSources.Item(0)
                        .GetValue("U_ChangeDate",
                            Form.DataSources.DBDataSources.Item(0).Offset))
                        ? DateTime.Now.ToString("yyyyMMdd")
                        : Form.DataSources.DBDataSources.Item(0).GetValue("U_ChangeDate",
                            Form.DataSources.DBDataSources.Item(0).Offset),
                    "yyyyMMdd",
                    CultureInfo.InvariantCulture);
                try
                {
                    model.Code = int.Parse(Form.DataSources.DBDataSources.Item(0).GetValue("Code",
                              Form.DataSources.DBDataSources.Item(0).Offset), CultureInfo.InvariantCulture);
                }
                catch (Exception)
                { 
                }
                model.Name = Form.DataSources.DBDataSources.Item(0).GetValue("Code",
                    Form.DataSources.DBDataSources.Item(0).Offset);
                return model;
            }
            set { FormModel = value; }
        }

        public OverheadParamController(IForm form)
        {
            DbModel = new OverheadParamsModel();
            DbModel.Version = "1.0";
            Form = form;
        }
        public void FillFormFromModel()
        {
            Application.SBO_Application.Forms.ActiveForm.Freeze(true);
            ((EditText)Form.Items.Item("Item_1").Specific).Value = FormModel.WorkingDaysMonthly.ToString(CultureInfo.InvariantCulture);
            ((EditText)Form.Items.Item("Item_3").Specific).Value = FormModel.DailyWorkHours.ToString(CultureInfo.InvariantCulture);
            ((EditText)Form.Items.Item("Item_15").Specific).Value = FormModel.AdministrativeOverhead.ToString(CultureInfo.InvariantCulture);
            ((EditText)Form.Items.Item("Item_13").Specific).Value = FormModel.ManufacuringOverhead.ToString(CultureInfo.InvariantCulture);
            ((EditText)Form.Items.Item("Item_5").Specific).Value = FormModel.CorianEmployee.ToString(CultureInfo.InvariantCulture);
            ((EditText)Form.Items.Item("Item_9").Specific).Value = FormModel.FurnitureEmployee.ToString(CultureInfo.InvariantCulture);
            ((EditText)Form.Items.Item("Item_7").Specific).Value = FormModel.NeolithEmployee.ToString(CultureInfo.InvariantCulture);
            ((EditText)Form.Items.Item("Item_11").Specific).Value = FormModel.TotalEmps.ToString(CultureInfo.InvariantCulture);
            ((EditText)Form.Items.Item("Item_29").Specific).Value = FormModel.Corian.ToString(CultureInfo.InvariantCulture);
            ((EditText)Form.Items.Item("Item_30").Specific).Value = FormModel.Neolith.ToString(CultureInfo.InvariantCulture);
            ((EditText)Form.Items.Item("Item_31").Specific).Value = FormModel.Furniture.ToString(CultureInfo.InvariantCulture);
            ((EditText)Form.Items.Item("Item_23").Specific).Value = FormModel.Version.ToString(CultureInfo.InvariantCulture);
            ((EditText)Form.Items.Item("Item_20").Specific).Value = FormModel.ChangeDate.ToString("yyyyMMdd");
            Application.SBO_Application.Forms.ActiveForm.Freeze(false);
        }
        public bool FillModelFromDb()
        {
            Recordset recSet = (Recordset)DiManager.Company.GetBusinessObject(BoObjectTypes.BoRecordset);
            recSet.DoQuery($"SELECT * FROM [@RSM_OVRHD_CLCB] WHERE U_Version = (SELECT MAX(convert (int, U_Version)) FROM [@RSM_OVRHD_CLCB])");
            if (recSet.EoF)
            {
                return false;
            }
            DbModel.WorkingDaysMonthly = (double)recSet.Fields.Item("U_WorkingDaysMonthly").Value;
            DbModel.DailyWorkHours = (double)recSet.Fields.Item("U_DailyWorkHours").Value;
            DbModel.Corian = (double)recSet.Fields.Item("U_Corian").Value;
            DbModel.Neolith = (double)recSet.Fields.Item("U_Neolith").Value;
            DbModel.Furniture = (double)recSet.Fields.Item("U_Furniture").Value;
            DbModel.AdministrativeOverhead = (double)recSet.Fields.Item("U_AdministrativeOverhead").Value;
            DbModel.ManufacuringOverhead = (double)recSet.Fields.Item("U_ManufacuringOverhead").Value;
            DbModel.CorianEmployee = (double)recSet.Fields.Item("U_CorianEmployee").Value;
            DbModel.FurnitureEmployee = (double)recSet.Fields.Item("U_FurnitureEmployee").Value;
            DbModel.NeolithEmployee = (double)recSet.Fields.Item("U_NeolithEmployee").Value;
            DbModel.TotalEmps = (double)recSet.Fields.Item("U_TotalEmps").Value;
            DbModel.ChangeDate = (DateTime)recSet.Fields.Item("U_ChangeDate").Value;
            DbModel.Version = recSet.Fields.Item("U_Version").Value.ToString();
            return true;
        }

        public void Refresh()
        {
            try
            {
                var x1 = double.Parse(((EditText)Form.Items.Item("Item_5").Specific).Value, CultureInfo.InvariantCulture);
                var x2 = double.Parse(((EditText)Form.Items.Item("Item_9").Specific).Value, CultureInfo.InvariantCulture);
                var x3 = double.Parse(((EditText)Form.Items.Item("Item_7").Specific).Value, CultureInfo.InvariantCulture);
                DbModel.TotalEmps = x1 + x2 + x3;
                ((EditText)Form.Items.Item("Item_11").Specific).Value = DbModel.TotalEmps.ToString(CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
            }
        }
        public void HendldeSettings()
        {
            bool fromdb = FillModelFromDb();
            if (!fromdb)
            {
                FillModelFromForm();
                FormModel.AddOrUpdate();
                return;
            }
            FillFormFromModel();
            CompareDbToForm();
            FormModel.AddOrUpdate();
            Form.DataSources.DBDataSources.Item(0).Query();
            Form.DataSources.DBDataSources.Item(0).Offset = Form.DataSources.DBDataSources.Item(0).Size - 1;
        }

        private void FillModelFromForm()
        {
            DbModel.WorkingDaysMonthly = double.Parse(Form.DataSources.DBDataSources.Item(0).GetValue("U_WorkingDaysMonthly", 0),CultureInfo.InvariantCulture);
            DbModel.DailyWorkHours = double.Parse(Form.DataSources.DBDataSources.Item(0).GetValue("U_DailyWorkHours", 0), CultureInfo.InvariantCulture);
            DbModel.Corian = double.Parse(Form.DataSources.DBDataSources.Item(0).GetValue("U_Corian", 0), CultureInfo.InvariantCulture);
            DbModel.Neolith = double.Parse(Form.DataSources.DBDataSources.Item(0).GetValue("U_Neolith", 0), CultureInfo.InvariantCulture);
            DbModel.Furniture = double.Parse(Form.DataSources.DBDataSources.Item(0).GetValue("U_Furniture", 0), CultureInfo.InvariantCulture);
            DbModel.AdministrativeOverhead = double.Parse(Form.DataSources.DBDataSources.Item(0).GetValue("U_AdministrativeOverhead", 0), CultureInfo.InvariantCulture);
            DbModel.ManufacuringOverhead = double.Parse(Form.DataSources.DBDataSources.Item(0).GetValue("U_ManufacuringOverhead", 0), CultureInfo.InvariantCulture);
            DbModel.CorianEmployee = double.Parse(Form.DataSources.DBDataSources.Item(0).GetValue("U_CorianEmployee", 0), CultureInfo.InvariantCulture);
            DbModel.FurnitureEmployee = double.Parse(Form.DataSources.DBDataSources.Item(0).GetValue("U_FurnitureEmployee", 0), CultureInfo.InvariantCulture);
            DbModel.NeolithEmployee = double.Parse(Form.DataSources.DBDataSources.Item(0).GetValue("U_NeolithEmployee", 0), CultureInfo.InvariantCulture);
            DbModel.TotalEmps = double.Parse(Form.DataSources.DBDataSources.Item(0).GetValue("U_TotalEmps", 0), CultureInfo.InvariantCulture);
            DbModel.ChangeDate = DateTime.Now;
            DbModel.Version = "1";
        }

        private void CompareDbToForm()
        {
            if (!DbModel.Equals(FormModel))
            {
                var version = (double.Parse(FormModel.Version) + 1).ToString(CultureInfo.InvariantCulture);
                Form.DataSources.DBDataSources.Item(0).SetValue("U_Version", Form.DataSources.DBDataSources.Item(0).Size - 1, version);
            }
        }
    }
}
