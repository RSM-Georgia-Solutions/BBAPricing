using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBAPricing
{
    public static class Settings
    {
        public static string WorkingPriceList { get; set; }
        public static string RetailPriceList { get; set; }
        public static double HumanResourceCoefficient { get; set; }
        public static double DailyNormPerPerson { get; set; }
        public static int MtrlExcelIndex { get; set; }
        public static int ResourceExcelIndex { get; set; }
    }
}
