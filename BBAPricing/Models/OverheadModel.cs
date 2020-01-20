using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SAPbobsCOM;

namespace BBAPricing.Models
{
    public class OverheadModel
    {
        public double RequiredResource { get; set; }
        public double UnitCost { get; set; }
        public double TotalCost { get; set; }
        public string ParentItemCode { get; set; }
        public string SalesQuotationDocEntry { get; set; }
        public string Version { get; set; }
        public string OverheadType { get; set; }

        public List<PropertyInfo> Properies { get; set; }
        public OverheadModel()
        {
            Properies = DiManager.GetPropInfo(typeof(OverheadModel));
        }

        public bool AddOrUpdate()
        {
            Recordset recSet = (Recordset)DiManager.Company.GetBusinessObject(BoObjectTypes.BoRecordset);

            recSet.DoQuery($"select Code from [@RSM_OVERHEADS_SQ] Where U_ParentItemCode = N'{ParentItemCode}' " +
                           $"AND U_SalesQuotationDocEntry = {SalesQuotationDocEntry} AND U_Version = '{Version}' AND U_OverheadType = '{OverheadType}'");
            string code = recSet.Fields.Item("Code").Value.ToString();
            bool updateFlag = recSet.RecordCount > 0;

            UserTable userTable = DiManager.Company.UserTables.Item("RSM_OVERHEADS_SQ");
            if (updateFlag)
            {
                userTable.GetByKey(code);
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
