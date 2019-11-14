using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAPbobsCOM;

namespace BBAPricing.Initialization
{
    public class CreateTables : IRunnable
    {
        public void Run()
        {
            DiManager.CreateTable("RSM_MBOM", "მასტერ ბომი", BoUTBTableType.bott_NoObjectAutoIncrement);
            DiManager.CreateTable("RSM_MBOM_ROWS", "მასტერ ბომი ცხრილური", BoUTBTableType.bott_NoObjectAutoIncrement);
            DiManager.CreateTable("RSM_ELEM", "ელემენტები", BoUTBTableType.bott_NoObjectAutoIncrement);
            DiManager.CreateTable("RSM_MTRL", "მატერიალების კალკულაცია", BoUTBTableType.bott_NoObjectAutoIncrement);
            DiManager.CreateTable("RSM_RESOURCES", "რესურსები", BoUTBTableType.bott_NoObjectAutoIncrement);
            DiManager.CreateTable("RSM_OPERATIONS", "ოპერაციები", BoUTBTableType.bott_NoObjectAutoIncrement);
            DiManager.CreateTable("RSM_OPRESEMP", "კავშირები", BoUTBTableType.bott_NoObjectAutoIncrement);
        }
    }
}

