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
        public string Currency { get; set; }
        public double Rate { get; set; }
        public DateTime ExchangeRateDate { get; set; }
        public double PriceForSquareMeter { get; set; }
        public double TotalSquareMeter { get; set; }
        public double ReferenceFeePercentage { get; set; }
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
            Recordset rec = (Recordset)DiManager.Company.GetBusinessObject(BoObjectTypes.BoRecordset);
            rec.DoQuery($"select IDENT_CURRENT ('@RSM_MBOM') as [Code]");
            Code = rec.Fields.Item("Code").Value.ToString();
            if (res != 0)
            {
                return false;
            }
            foreach (var row in Rows)
            {
                var rowResult = row.Add();
                if (res != 0)
                {
                    return false;
                }
            }
            return true;
        }

        public bool Update()
        {
            UserTable userTable = DiManager.Company.UserTables.Item("RSM_MBOM");
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
            if (res != 0)
            {
                return false;
            }
            foreach (var row in Rows)
            {
                var rowResult = row.Update();
                if (res != 0)
                {

                    return false;
                }
            }
            return true;
        }


    }
}
