using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SAPbobsCOM;

namespace BBAPricing.Models
{
    public class SettingsModel
    {
        public int Code { get; set; }
        public string WorkingPriceList { get; set; }
        public string RetailPriceList { get; set; }
        public double HumanResourceCoefficient { get; set; }
        public double DailyNormPerPerson { get; set; }
        public int ResourceExcelIndex { get; set; }
        public int MtrlExcelIndex { get; set; }
        public  string ProductCode { get; set; }
        public  string Quantity { get; set; }
        public  string Uom { get; set; }
        private List<PropertyInfo> Properies { get; }

        public SettingsModel()
        {
            Properies = DiManager.GetPropInfo(typeof(SettingsModel));
        }

        public bool AddOrUpdate()
        {
            Recordset recSet = (Recordset)DiManager.Company.GetBusinessObject(BoObjectTypes.BoRecordset);
            recSet.DoQuery($"SELECT * FROM [@RSM_BBA_SETTINGS]");
            bool updateFlag = recSet.RecordCount > 0;

            UserTable userTable = DiManager.Company.UserTables.Item("RSM_BBA_SETTINGS");
            if (updateFlag)
            {
                userTable.GetByKey(recSet.Fields.Item("Code").Value.ToString());
            }
            foreach (var prop in Properies)
            {
                object value = DiManager.GetPropValue(this, prop.Name);
                try
                {
                    userTable.UserFields.Fields.Item($"U_{prop.Name}").Value = value ?? string.Empty;
                }
                catch (Exception)
                {
                    // Model And Db MisMatch
                }
            }
            int res = updateFlag ? userTable.Update() : userTable.Add();
            return res == 0;
        }
    }
}
