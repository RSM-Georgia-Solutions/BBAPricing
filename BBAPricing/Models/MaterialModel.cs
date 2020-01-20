using SAPbobsCOM;
using SAPbouiCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BBAPricing.Models
{
    public class MaterialModel
    {
        public string Code { get; set; }
        public string Article { get; set; }
        public string ComponentCode { get; set; }
        public string ComponentName { get; set; }
        public string UnitOfMeasure { get; set; }
        public double UnitCost { get; set; }
        public double TotalCost { get; set; }
        public double UnitRetailPrice { get; set; }
        public double UnitWorkingPrice { get; set; }
        public double DiscountPercentage { get; set; }
        public double DiscountAmount { get; set; }
        public double SharedPercentage { get; set; }
        public double SharedDiscountAmount { get; set; }
        public double MarginPercentage { get; set; }
        public double MarginAmount { get; set; }
        public double FinalCustomerPrice { get; set; }
        public double FinalCustomerPriceTotal { get; set; }
        public string Note { get; set; }
        public double Quantity { get; set; }
        public string SalesQuotationDocEntry { get; set; }
        public string ParentItemCode { get; set; }
        public string Version { get; set; }
        public string Currency { get; set; }
        public string ConvertedCurrency { get; set; }

        public List<PropertyInfo> Properies { get; set; }
        public MaterialModel()
        {
            Properies = DiManager.GetPropInfo(typeof(MaterialModel));
        }
        public bool Add()
        {
            UserTable userTable = DiManager.Company.UserTables.Item("RSM_MTRL");
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
            UserTable userTable = DiManager.Company.UserTables.Item("RSM_MTRL");
            userTable.GetByKey(Code);
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
