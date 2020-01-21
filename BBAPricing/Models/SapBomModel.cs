using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAPbobsCOM;

namespace BBAPricing.Models
{
    public class SapBomModel
    {
        public string ProductNo { get; set; }
        public double Quantity { get; set; }
        public BoItemTreeTypes BomType { get; set; }
        public List<SapBomModelRow> Rows { get; set; }

        public SapBomModel()
        {
            Rows = new List<SapBomModelRow>();
        }
    }

    public class SapBomModelRow
    {
        public ProductionItemType Type { get; set; }
        public string ChildItem { get; set; }
        public double Quantity { get; set; }
        public string UnitOfMeasure { get; set; }
    }
}
