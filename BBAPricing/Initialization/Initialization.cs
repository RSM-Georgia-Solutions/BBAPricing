using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBAPricing.Initialization
{
    public class Initialization : IRunnable
    {
        public void Run()
        {
            IEnumerable<IRunnable> obects = new List<IRunnable>()
            {
                new CreateTables(),
                new CreateFields(),
                new Addkeyes(),
                new InsertData()
            };
            foreach (var obj in obects)
            {
                obj.Run();
            }
        }
    }
}
