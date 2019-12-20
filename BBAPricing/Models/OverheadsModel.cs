using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SAPbobsCOM;

namespace BBAPricing.Models
{
    public class OverheadsModel
    {
        public int Code { get; set; }
        public string ParentItemCode { get; set; }
        public string SalesQuotationDocEntry { get; set; }
        public string Version { get; set; }
        public double Corian { get; set; }
        public double Neolith { get; set; }
        public double Furniture { get; set; }
        public double Total { get; set; }
        public string ComponentId { get; set; }
        public string ComponentName { get; set; }
        public string ElementId { get; set; }
        public DateTime ChangeDate { get; set; }
        public List<PropertyInfo> Properies { get; set; }

        public OverheadsModel()
        {
            Properies = DiManager.GetPropInfo(typeof(OverheadsModel));
        }

        public bool AddOrUpdate()
        {
            Recordset recSet = (Recordset)DiManager.Company.GetBusinessObject(BoObjectTypes.BoRecordset);
            recSet.DoQuery($"SELECT * FROM [@RSM_OVERHEADS_R] WHERE U_ComponentId = N'{ComponentId}'");
            bool updateFlag = recSet.RecordCount > 0;

            UserTable userTable = DiManager.Company.UserTables.Item("RSM_OVERHEADS_R");
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
