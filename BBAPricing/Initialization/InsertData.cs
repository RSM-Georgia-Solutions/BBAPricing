using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBAPricing.Initialization
{
    public class InsertData : IRunnable
    {
        public void Run()
        {
            DiManager.DbInsert("RSM_ELEM",new Dictionary<string, dynamic> { { "Element", "MTRLs" } },DiManager.Company);
            DiManager.DbInsert("RSM_ELEM",new Dictionary<string, dynamic> { { "Element", "Machinery Resources" } },DiManager.Company);
            DiManager.DbInsert("RSM_ELEM",new Dictionary<string, dynamic> { { "Element", "Human Resources" } },DiManager.Company);
            DiManager.DbInsert("RSM_ELEM",new Dictionary<string, dynamic> { { "Element", "Bonus" } },DiManager.Company);
            DiManager.DbInsert("RSM_ELEM",new Dictionary<string, dynamic> { { "Element", "Manufacturing Overheads" } },DiManager.Company);
            DiManager.DbInsert("RSM_ELEM",new Dictionary<string, dynamic> { { "Element", "Administrative Overheads" } },DiManager.Company);
            DiManager.DbInsert("RSM_ELEM",new Dictionary<string, dynamic> { { "Element", "Reference Fee" } },DiManager.Company);
            DiManager.DbInsert("RSM_ELEM",new Dictionary<string, dynamic> { { "Element", "Business Trip" } },DiManager.Company);
            DiManager.DbInsert("RSM_ELEM",new Dictionary<string, dynamic> { { "Element", "Transportation" } },DiManager.Company);
        }
    }
}
