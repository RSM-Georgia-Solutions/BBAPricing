using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BBAPricing.Models
{
    public class MasterBomModel
    {
        public string Code { get; set; }
        public string CostCenter { get; set; }
        public string SalesQuotationDocEntry { get; set; }
        public int SalesQuotationDocNum { get; set; }
        public string ProjectCode { get; set; }
        public string ProjectName { get; set; }
        public double Quantity { get; set; }
        public string ParentItem { get; set; }
        public string CardCode { get; set; }
        public string OwnerCode { get; set; }
        public string OwnerName { get; set; }
        public DateTime CreateDate { get; set; }
        public string Version { get; set; }
        public double PriceForSquareMeter { get; set; }

        public List<MasterBomRowModel> Rows { get; set; }

        public List<PropertyInfo> Properies { get; set; }
        public MasterBomModel()
        {
            Rows = new List<MasterBomRowModel>();
            Properies = DiManager.GetPropInfo(typeof(MasterBomModel));
        }

        public bool Add()
        {
            UserTable userTable = DiManager.Company.UserTables.Item("RSM_MBOM");

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
            int res = userTable.Add();
            var x = DiManager.Company.GetLastErrorDescription();
            return res == 0;

        }
    }
}
