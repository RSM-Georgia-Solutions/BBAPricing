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
        public double Corian { get; set; }
        public double Neolith { get; set; }
        public double Furniture { get; set; }
        public double WorkingDaysMonthly { get; set; }
        public double ManufacuringOverhead { get; set; }
        public double AdministrativeOverhead { get; set; }
        public double CorianEmployee { get; set; }
        public double FurnitureEmployee { get; set; }
        public double NeolithEmployee { get; set; }
        public double TotalEmps { get; set; }
        public DateTime ChangeDate { get; set; }
        public string Version { get; set; }
        public List<PropertyInfo> Properies { get; set; }
        public OverheadParamsModel()
        {
            Properies = DiManager.GetPropInfo(typeof(OverheadParamsModel));
        }
        public virtual bool Equals(OverheadParamsModel obj)
        {
            return DailyWorkHours == obj.DailyWorkHours && AdministrativeOverhead == obj.AdministrativeOverhead
                                                        && Corian == obj.Corian && Neolith == obj.Neolith
                                                        && Furniture == obj.Furniture && WorkingDaysMonthly == obj.WorkingDaysMonthly
                                                        && TotalEmps == obj.TotalEmps && NeolithEmployee == obj.NeolithEmployee
                                                        && FurnitureEmployee == obj.FurnitureEmployee && CorianEmployee == obj.CorianEmployee
                                                        && ManufacuringOverhead == obj.ManufacuringOverhead;
        }
        public bool AddOrUpdate()
        {
            Recordset recSet = (Recordset)DiManager.Company.GetBusinessObject(BoObjectTypes.BoRecordset);
            recSet.DoQuery($"SELECT * FROM [@RSM_OVRHD_CLCB] WHERE U_Version = '{Version}'");
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
            int res;
            if (updateFlag)
            {
                res = userTable.Update();
            }
            else
            {
                res = userTable.Add();
            }

            var x = DiManager.Company.GetLastErrorDescription();
            return res == 0;
        }
    }


}
