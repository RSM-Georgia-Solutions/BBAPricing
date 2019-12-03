using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAPbobsCOM;
using System.Reflection;

namespace BBAPricing.Models
{
    public class OverheadParamsModel
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public double DailyWorkHours { get; set; }
        public double WorkingDaysMonthly { get; set; }
        public double ManufacuringOverhead { get; set; }
        public double AdministrativeOverhead { get; set; }
        public double CorianEmployee { get; set; }
        public double FurnitureEmployee { get; set; }
        public double NeolithEmployee { get; set; }
        public double TotalEmps { get; set; }
        public DateTime ChangeDate { get; set; }
        public List<PropertyInfo> Properies { get; set; }
        public OverheadParamsModel()
        {
            Properies = DiManager.GetPropInfo(typeof(OverheadParamsModel));
        }

        public bool AddOrUpdate()
        {
            Recordset recSet = (Recordset)DiManager.Company.GetBusinessObject(BoObjectTypes.BoRecordset);
            recSet.DoQuery($"SELECT * FROM [@RSM_OVRHD_CLCB]");
            bool updateFlag = recSet.RecordCount > 0;

            UserTable userTable = DiManager.Company.UserTables.Item("RSM_OVRHD_CLCB");
            if (updateFlag)
            {
                userTable.GetByKey(recSet.Fields.Item("Code").Value.ToString());
            }
            foreach (var prop in Properies)
            {
                var propName = prop.Name;
                object value = DiManager.GetPropValue(this, prop.Name);
                try
                {
                    userTable.UserFields.Fields.Item($"U_{prop.Name}").Value = value ?? string.Empty;
                }
                catch (Exception e)
                {
                }
            }
            int res = updateFlag ? userTable.Update() : userTable.Add();
            var x = DiManager.Company.GetLastErrorDescription();
            return res == 0;
        }
    }


}
