using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BBAPricing.Models
{
    public class ResourceModel
    {
        public string Code { get; set; }
        public string SalesQuotationDocEntry { get; set; }
        public string ParentItemCode { get; set; }
        public string OperationKey { get; set; }
        public string ResourceCode { get; set; }
        public string ResourceName { get; set; }
        public string Uom { get; set; }
        public double Quantity { get; set; }
        public double TimeRequired { get; set; }
        public double StandartCost { get; set; }
        public double TotalStandartCost { get; set; }
        public double ResourceUnitPrice { get; set; }
        public double ResourceTotalPrice { get; set; }
        public double MarginPercent { get; set; }
        public double AmountOnUnit { get; set; }
        public double TotalAmount { get; set; }
        public double CostOfUnit { get; set; }
        public double PriceOfUnit { get; set; }
        public double MarginOfUnit { get; set; }
        public double InfoPercent { get; set; }
        public string Version { get; set; }
        public string OperationCode { get; set; }
        public string OperationName { get; set; }
        public string Currency { get; set; }
        public string UomResourceMain { get; set; }
        public double OtherQtyResource { get; set; }
        public List<PropertyInfo> Properies { get; set; }
        public ResourceModel()
        {
            Properies = DiManager.GetPropInfo(typeof(ResourceModel));
        }
        public bool Add()
        {
            UserTable userTable = DiManager.Company.UserTables.Item("RSM_RESOURCES");
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
