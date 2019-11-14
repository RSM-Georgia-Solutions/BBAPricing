using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BBAPricing.Models;
using SAPbouiCOM;
using DataTable = System.Data.DataTable;

namespace BBAPricing.Controllers
{
    public class MaterialsController
    {
        public static List<MaterialModel> ParseDataTAbleToPricing(DataTable data)
        {
            List<MaterialModel> result = new List<MaterialModel>();
            List<DataRow> rows = data.AsEnumerable().ToList();
            object[] headersx = rows[0].ItemArray; //headers in actual excel
            Dictionary<string, int> excelIndexes = new Dictionary<string, int>();

            for (int i = 0; i < headersx.Length; i++)
            {
                string header = headersx[i].ToString(); //current header
                try
                {
                    excelIndexes.Add(header, i);
                }
                catch (Exception e)
                {
                    SAPbouiCOM.Framework.Application.SBO_Application.SetStatusBarMessage("დუბლირებული ველები Excel-ში",
                         BoMessageTime.bmt_Short, true);
                }
            }

            foreach (DataRow row in rows.Skip(1))
            {
                string materialName = row[excelIndexes["Material name"]].ToString();
                string unitOfMeasure = row[excelIndexes["Meas. unit"]].ToString();
                string quantity = row[excelIndexes["Quantity in a product"]].ToString();
                string orederQuantity = row[excelIndexes["Quantity order quantity"]].ToString();
                string cost = row[excelIndexes["UnitCost"]].ToString();

                MaterialModel model = new MaterialModel();
                model.UnitCost = double.Parse(cost);
                model.ComponentCode = materialName;
                result.Add(model);
            }

            return result;
        }
    }
}
