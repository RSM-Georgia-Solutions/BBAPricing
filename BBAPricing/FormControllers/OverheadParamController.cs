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
    public class OverheadParamController : IFormController
    {
        public OverheadParamsModel _Model;
        private DBDataSource _DataSource;
        public OverheadParamController(IForm form) : base(form)
        {
            _Model = new OverheadParamsModel();
            // _DataSource = form.DataSources.DBDataSources.Item("@RSM_OVRHD_CLCB");
        }
        public override void FillGridFromModel(Grid grid)
        {
            throw new NotImplementedException();
        }

        public void FillFormFromModel()
        {
            Application.SBO_Application.Forms.ActiveForm.Freeze(true);
            ((EditText)Form.Items.Item("Item_1").Specific).Value = _Model.WorkingDaysMonthly.ToString(CultureInfo.InvariantCulture);
            ((EditText)Form.Items.Item("Item_3").Specific).Value = _Model.DailyWorkHours.ToString(CultureInfo.InvariantCulture);
            ((EditText)Form.Items.Item("Item_15").Specific).Value = _Model.AdministrativeOverhead.ToString(CultureInfo.InvariantCulture);
            ((EditText)Form.Items.Item("Item_13").Specific).Value = _Model.ManufacuringOverhead.ToString(CultureInfo.InvariantCulture);
            ((EditText)Form.Items.Item("Item_5").Specific).Value = _Model.CorianEmployee.ToString(CultureInfo.InvariantCulture);
            ((EditText)Form.Items.Item("Item_9").Specific).Value = _Model.FurnitureEmployee.ToString(CultureInfo.InvariantCulture);
            ((EditText)Form.Items.Item("Item_7").Specific).Value = _Model.NeolithEmployee.ToString(CultureInfo.InvariantCulture);
            ((EditText)Form.Items.Item("Item_11").Specific).Value = _Model.TotalEmps.ToString(CultureInfo.InvariantCulture);
            ((EditText)Form.Items.Item("Item_20").Specific).Value = _Model.ChangeDate.ToString("yyyyMMdd");
            Application.SBO_Application.Forms.ActiveForm.Freeze(false);


        }

        public override void GetGridColumns()
        {
            throw new NotImplementedException();
        }

        public override bool FillModelFromDb()
        {
            Recordset recSet = (Recordset)DiManager.Company.GetBusinessObject(BoObjectTypes.BoRecordset);
            recSet.DoQuery($"SELECT * FROM [@RSM_OVRHD_CLCB]");
            if (recSet.EoF)
            {
                return false;
            }
            _Model.WorkingDaysMonthly = (double)recSet.Fields.Item("U_WorkingDaysMonthly").Value;
            _Model.DailyWorkHours = (double)recSet.Fields.Item("U_DailyWorkHours").Value;
            _Model.AdministrativeOverhead = (double)recSet.Fields.Item("U_AdministrativeOverhead").Value;
            _Model.ManufacuringOverhead = (double)recSet.Fields.Item("U_ManufacuringOverhead").Value;
            _Model.CorianEmployee = (double)recSet.Fields.Item("U_CorianEmployee").Value;
            _Model.FurnitureEmployee = (double)recSet.Fields.Item("U_FurnitureEmployee").Value;
            _Model.NeolithEmployee = (double)recSet.Fields.Item("U_NeolithEmployee").Value;
            _Model.TotalEmps = (double)recSet.Fields.Item("U_TotalEmps").Value;
            _Model.ChangeDate = (DateTime)recSet.Fields.Item("U_ChangeDate").Value;
            return true;
        }

        public override void GenerateModel()
        {
            throw new NotImplementedException();
        }


        public void Refresh()
        {
            var x1 = double.Parse(((EditText)Form.Items.Item("Item_5").Specific).Value, CultureInfo.InvariantCulture);
            var x2 = double.Parse(((EditText)Form.Items.Item("Item_9").Specific).Value, CultureInfo.InvariantCulture);
            var x3 = double.Parse(((EditText)Form.Items.Item("Item_7").Specific).Value, CultureInfo.InvariantCulture);
            _Model.TotalEmps = x1 + x2 + x3;
            ((EditText)Form.Items.Item("Item_11").Specific).Value = _Model.TotalEmps.ToString(CultureInfo.InvariantCulture);
        }

        public void FillModelFromForm()
        {
            _Model.WorkingDaysMonthly = double.Parse(((EditText)Form.Items.Item("Item_1").Specific).Value, CultureInfo.InvariantCulture);
            _Model.DailyWorkHours = double.Parse(((EditText)Form.Items.Item("Item_3").Specific).Value, CultureInfo.InvariantCulture);
            _Model.AdministrativeOverhead = double.Parse(((EditText)Form.Items.Item("Item_15").Specific).Value, CultureInfo.InvariantCulture);
            _Model.ManufacuringOverhead = double.Parse(((EditText)Form.Items.Item("Item_13").Specific).Value, CultureInfo.InvariantCulture);
            _Model.CorianEmployee = double.Parse(((EditText)Form.Items.Item("Item_5").Specific).Value, CultureInfo.InvariantCulture);
            _Model.FurnitureEmployee = double.Parse(((EditText)Form.Items.Item("Item_9").Specific).Value, CultureInfo.InvariantCulture);
            _Model.NeolithEmployee = double.Parse(((EditText)Form.Items.Item("Item_7").Specific).Value, CultureInfo.InvariantCulture);
            _Model.ChangeDate = DateTime.Now;
            _Model.TotalEmps = _Model.CorianEmployee + _Model.FurnitureEmployee + _Model.NeolithEmployee;
            Refresh();
        }

        public void HendldeSettings()
        {
            FillModelFromForm();
            FillFormFromModel();
            _Model.AddOrUpdate();
        }
    }
}
