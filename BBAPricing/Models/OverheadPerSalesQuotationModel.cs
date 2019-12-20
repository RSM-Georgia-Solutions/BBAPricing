using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BBAPricing.Models
{
    public class OverheadPerSalesQuotationModel
    {
        public List<PropertyInfo> Properies { get; set; }
        public string ParentItemCode { get; set; }
        public string SalesQuotationDocEntry { get; set; }
        public string Version { get; set; }
        public double RequiredResource { get; set; }
        public double UnitCost { get; set; }
        public double TotalCost { get; set; }
        public int Code { get; set; }
        public string Name{ get; set; }
        public OverheadPerSalesQuotationModel()
        {
            Properies = DiManager.GetPropInfo(typeof(OverheadPerSalesQuotationModel));
        }
        public bool AddOrUpdate()
        {
            Recordset recSet = (Recordset)DiManager.Company.GetBusinessObject(BoObjectTypes.BoRecordset);
           // recSet.DoQuery($"SELECT * FROM [@RSM_OVERHEADS_R] WHERE U_ComponentId = N'{ComponentId}'");
            bool updateFlag = recSet.RecordCount > 0;

            UserTable userTable = DiManager.Company.UserTables.Item("RSM_OVERHEADS_SQ");
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
