using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BBAPricing.Models
{
    public class MasterBomRowModel
    {
        public string Code { get; set; }
        public string ElementID { get; set; }
        public double Cost { get; set; }
        public double Price { get; set; }
        public double Margin { get; set; }
        public double FinalCustomerPrice { get; set; }
        public double Percent { get; set; }
        public double I { get; set; }
        public double II { get; set; }
        public double III { get; set; }
        public string ParentItemCode { get; set; }
        public string SalesQuotationDocEntry { get; set; }
        public string Version { get; set; }

        public MasterBomRowModel()
        {
            Properies = DiManager.GetPropInfo(typeof(MasterBomRowModel));
        }
        public List<PropertyInfo> Properies { get; set; }
        public bool Add()
        {
            UserTable userTable = DiManager.Company.UserTables.Item("RSM_MBOM_ROWS");
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

        public bool Update()
        {
            UserTable userTable = DiManager.Company.UserTables.Item("RSM_MBOM_ROWS");
            userTable.GetByKey(Code.ToString());
            foreach (var prop in Properies)
            {
                object value = DiManager.GetPropValue(this, prop.Name);
                try
                {
                    userTable.UserFields.Fields.Item($"U_{prop.Name}").Value = value ?? string.Empty;
                }
                catch (Exception e)
                {
                }
            }
            int res = userTable.Update();
            var x = DiManager.Company.GetLastErrorDescription();
            return res == 0;

        }
    }
}
