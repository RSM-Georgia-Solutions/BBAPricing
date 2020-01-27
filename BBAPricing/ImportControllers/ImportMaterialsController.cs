using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BBAPricing.Models;
using SAPbobsCOM;
using SAPbouiCOM;
using DataTable = System.Data.DataTable;

namespace BBAPricing.Controllers
{
    public class ImportMaterialsController
    {
        public static void ParseDataTableToPricingMtrl(DataTable data, SapBomModel sapBomModel)
        {
            List<DataRow> rows = data.AsEnumerable().ToList();
            object[] headersx = rows[Settings.MtrlExcelIndex - 1].ItemArray; //headers in actual excel
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

            foreach (DataRow row in rows.Skip(Settings.MtrlExcelIndex))
            {
                string productCode = row[excelIndexes["Article"]].ToString();
                var quantity = double.Parse(row[excelIndexes["Quantity in a product"]].ToString(), CultureInfo.InvariantCulture);
                string uom = row[excelIndexes["Meas. unit"]].ToString();
                sapBomModel.Rows.Add(new SapBomModelRow
                {
                    ChildItem = productCode,
                    Quantity = quantity,
                    UnitOfMeasure = uom,
                    Type = ProductionItemType.pit_Item
                });
            }
        }

        public static string ImportMtrl(SapBomModel sapBomModel)
        {
            var bom = (ProductTrees)DiManager.Company.GetBusinessObject(BoObjectTypes.oProductTrees);
            var updateFlag = bom.GetByKey(sapBomModel.ProductNo);
            bom.TreeCode = sapBomModel.ProductNo;
            bom.Quantity = sapBomModel.Quantity;
            bom.TreeType = sapBomModel.BomType;
            while (bom.Items.Count > 1)
            {
                bom.Items.Delete();
            }
            foreach (SapBomModelRow sapBomModelRow in sapBomModel.Rows)
            {
                bom.Items.ItemCode = sapBomModelRow.ChildItem;
                bom.Items.ItemType = sapBomModelRow.Type;
                bom.Items.Quantity = sapBomModelRow.Quantity;
                bom.Items.Add();
            }
            var res = updateFlag ? bom.Update() : bom.Add();
            if (res != 0)
            {
                var err = DiManager.Company.GetLastErrorDescription();
                throw new Exception(err);
            }
            return res == 0? DiManager.Company.GetNewObjectKey() : res.ToString();
        }
    }
}
