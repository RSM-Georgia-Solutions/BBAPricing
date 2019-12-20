using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SAPbobsCOM;

namespace BBAPricing.Models
{
    public class CommonElementsModel
    {
        public List<PropertyInfo> Properies { get; set; }

        public string ProjectCode { get; set; }
        public DateTime ChangeDate { get; set; }
        public string SalesQuotationDocEntry { get; set; }
        public double EmployeeQuantity { get; set; }
        public double DailyNormPerPerson { get; set; }
        public double QuantityOfDays { get; set; }
        public double HostelCostPerDay { get; set; }
        public double TotalHotelCost { get; set; }
        public double TotalCost { get; set; }
        public double TransportationAmount { get; set; }

        public CommonElementsModel()
        {
            Properies = DiManager.GetPropInfo(typeof(CommonElementsModel));
        }

        public bool AddOrUpdate()
        {
            Recordset recSet = (Recordset)DiManager.Company.GetBusinessObject(BoObjectTypes.BoRecordset);
            recSet.DoQuery($"SELECT * FROM [@RSM_COMMON_ELEM]");
            bool updateFlag = recSet.RecordCount > 0;

            UserTable userTable = DiManager.Company.UserTables.Item("RSM_COMMON_ELEM");
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
