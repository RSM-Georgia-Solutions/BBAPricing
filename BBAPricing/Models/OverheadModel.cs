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

        public bool Add()
        {
            UserTable userTable = DiManager.Company.UserTables.Item("RSM_OVERHEADS_SQ");
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
            int res = userTable.Add();
            var x = DiManager.Company.GetLastErrorDescription();
            return res == 0;
        }
    }
}
