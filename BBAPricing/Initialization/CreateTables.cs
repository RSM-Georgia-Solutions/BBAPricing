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
            DiManager.CreateTable("RSM_MTRL", "მატერიალები", BoUTBTableType.bott_NoObjectAutoIncrement);
            DiManager.CreateTable("RSM_RESOURCES", "რესურსები", BoUTBTableType.bott_NoObjectAutoIncrement);
            DiManager.CreateTable("RSM_OPERATIONS", "ოპერაციები", BoUTBTableType.bott_NoObjectAutoIncrement);
            DiManager.CreateTable("RSM_OPRESEMP", "კავშირები", BoUTBTableType.bott_NoObjectAutoIncrement);
            DiManager.CreateTable("RSM_OVERHEADS", "დამატებითი ხარჯი", BoUTBTableType.bott_NoObjectAutoIncrement);
            DiManager.CreateTable("RSM_OVERHEADS_R", "RSM OVERHEADS R", BoUTBTableType.bott_NoObjectAutoIncrement);
            DiManager.CreateTable("RSM_OVERHEAD_C", "RSM_OVERHEAD_C", BoUTBTableType.bott_NoObjectAutoIncrement);
            DiManager.CreateTable("RSM_OVRHD_CLCB", "RSM_OVRHD_CLCB", BoUTBTableType.bott_NoObjectAutoIncrement);
            DiManager.CreateTable("RSM_COMMON_ELEM", "RSM_COMMON_ELEM", BoUTBTableType.bott_NoObjectAutoIncrement);
            DiManager.CreateTable("RSM_OVERHEADS_SQ", "RSM OVERHEADS SQ", BoUTBTableType.bott_NoObjectAutoIncrement);
            DiManager.CreateTable("RSM_TRIP", "Business Trip", BoUTBTableType.bott_NoObjectAutoIncrement);
        }
    }
}

