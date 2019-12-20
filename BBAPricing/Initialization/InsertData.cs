using System.Collections.Generic;

namespace BBAPricing.Initialization
{
    public class InsertData : IRunnable
    {
        public void Run()
        {
            DiManager.DbInsert("RSM_ELEM",new Dictionary<string, dynamic> { { "Element", "MTRLs" } },DiManager.Company);
            DiManager.DbInsert("RSM_ELEM",new Dictionary<string, dynamic> { { "Element", "Machinery Resources" } },DiManager.Company);
            DiManager.DbInsert("RSM_ELEM",new Dictionary<string, dynamic> { { "Element", "Human Resources" } },DiManager.Company);
           // DiManager.DbInsert("RSM_ELEM",new Dictionary<string, dynamic> { { "Element", "Bonus" } },DiManager.Company);
            DiManager.DbInsert("RSM_ELEM",new Dictionary<string, dynamic> { { "Element", "Manufacturing Overheads" } },DiManager.Company);
            DiManager.DbInsert("RSM_ELEM",new Dictionary<string, dynamic> { { "Element", "Administrative Overheads" } },DiManager.Company);
            DiManager.DbInsert("RSM_ELEM",new Dictionary<string, dynamic> { { "Element", "Business Trip" } },DiManager.Company);
            DiManager.DbInsert("RSM_ELEM",new Dictionary<string, dynamic> { { "Element", "Transportation" } },DiManager.Company);
            DiManager.DbInsert("RSM_ELEM",new Dictionary<string, dynamic> { { "Element", "Reference Fee" } },DiManager.Company);

            DiManager.DbInsert("RSM_OVERHEAD_C", new Dictionary<string, dynamic> { { "Component", "კაც/საათი" } }, DiManager.Company);
            DiManager.DbInsert("RSM_OVERHEAD_C", new Dictionary<string, dynamic> { { "Component", "Overhead Percent" } }, DiManager.Company);
            DiManager.DbInsert("RSM_OVERHEAD_C", new Dictionary<string, dynamic> { { "Component", "Manufacturing Overhead" } }, DiManager.Company);
            DiManager.DbInsert("RSM_OVERHEAD_C", new Dictionary<string, dynamic> { { "Component", "Administrative Overhead" } }, DiManager.Company);
            DiManager.DbInsert("RSM_OVERHEAD_C", new Dictionary<string, dynamic> { { "Component", "Manufacturing Overhead 1 კაც/საათზე" } }, DiManager.Company);
            DiManager.DbInsert("RSM_OVERHEAD_C", new Dictionary<string, dynamic> { { "Component", "Administrative Overhead 1 კაც/საათზე" } }, DiManager.Company);

        }
    }
}
