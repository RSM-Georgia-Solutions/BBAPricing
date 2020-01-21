using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAPbobsCOM;

namespace BBAPricing.Initialization
{
    public class CreateFields : IRunnable
    {
        public void Run()
        {


            DiManager.CreateField("@RSM_BBA_SETTINGS", "WorkingPriceList", "Working Price List", BoFieldTypes.db_Alpha, 250, false);
            DiManager.CreateField("@RSM_BBA_SETTINGS", "MtrlExcelIndex", "Mtrl Excel Index", BoFieldTypes.db_Numeric, 3, false);
            DiManager.CreateField("@RSM_BBA_SETTINGS", "ResourceExcelIndex", "ResourceExcelIndex", BoFieldTypes.db_Numeric, 3, false);
            DiManager.CreateField("@RSM_BBA_SETTINGS", "RetailPriceList", "Retail Price List", BoFieldTypes.db_Alpha, 250, false);
            DiManager.CreateField("@RSM_BBA_SETTINGS", "HumanResourceCoefficient", "HumanResource Coefficient", BoFieldTypes.db_Float, 250, false, false, "", "", BoFldSubTypes.st_Sum);
            DiManager.CreateField("@RSM_BBA_SETTINGS", "DailyNormPerPerson", "Daily Norm Per Person", BoFieldTypes.db_Float, 250, false, false, "", "", BoFldSubTypes.st_Sum);

            DiManager.CreateField("@RSM_MBOM_ROWS", "ElementID", "Element ID", BoFieldTypes.db_Alpha, 250, false);
            DiManager.CreateField("@RSM_MBOM_ROWS", "Element", "Element", BoFieldTypes.db_Alpha, 250, false);
            DiManager.CreateField("@RSM_MBOM_ROWS", "Cost", "Cost", BoFieldTypes.db_Float, 250, false, false, "", "", BoFldSubTypes.st_Sum);
            DiManager.CreateField("@RSM_MBOM_ROWS", "Price", "Price", BoFieldTypes.db_Float, 250, false, false, "", "", BoFldSubTypes.st_Price);
            DiManager.CreateField("@RSM_MBOM_ROWS", "Margin", "Margin", BoFieldTypes.db_Float, 250, false, false, "", "", BoFldSubTypes.st_Sum);
            DiManager.CreateField("@RSM_MBOM_ROWS", "FinalCustomerPrice", "Final Customer Price", BoFieldTypes.db_Float, 250, false, false, "", "", BoFldSubTypes.st_Sum);
            DiManager.CreateField("@RSM_MBOM_ROWS", "Percent", "%", BoFieldTypes.db_Float, 250, false, false, "", "", BoFldSubTypes.st_Percentage);
            DiManager.CreateField("@RSM_MBOM_ROWS", "I", "I", BoFieldTypes.db_Float, 250, false, false, "", "", BoFldSubTypes.st_Percentage);
            DiManager.CreateField("@RSM_MBOM_ROWS", "II", "II", BoFieldTypes.db_Float, 250, false, false, "", "", BoFldSubTypes.st_Percentage);
            DiManager.CreateField("@RSM_MBOM_ROWS", "III", "III", BoFieldTypes.db_Float, 250, false, false, "", "", BoFldSubTypes.st_Percentage);
            DiManager.CreateField("@RSM_MBOM_ROWS", "ParentItemCode", "Parent Item Code", BoFieldTypes.db_Alpha, 250, false);
            DiManager.CreateField("@RSM_MBOM_ROWS", "SalesQuotationDocEntry", "Sales Quotation DocEntry", BoFieldTypes.db_Alpha, 250, false);
            DiManager.CreateField("@RSM_MBOM_ROWS", "Version", "Version", BoFieldTypes.db_Alpha, 250, false);


            DiManager.CreateField("@RSM_MBOM", "CostCenter", "Cost Center", BoFieldTypes.db_Alpha, 250, false);
            DiManager.CreateField("@RSM_MBOM", "ParentItem", "Parent Item", BoFieldTypes.db_Alpha, 250, false);
            DiManager.CreateField("@RSM_MBOM", "SalesQuotationDocEntry", "Sales Quotation DocEntry", BoFieldTypes.db_Alpha, 250, false);
            DiManager.CreateField("@RSM_MBOM", "SalesQuotationDocNum", "Sales Quotation DocNum", BoFieldTypes.db_Numeric, 11, false);
            DiManager.CreateField("@RSM_MBOM", "ProjectCode", "Project Code", BoFieldTypes.db_Alpha, 250, false);
            DiManager.CreateField("@RSM_MBOM", "CardCode", "Card Code", BoFieldTypes.db_Alpha, 250, false);
            DiManager.CreateField("@RSM_MBOM", "ProjectName", "Project Name", BoFieldTypes.db_Alpha, 250, false);
            DiManager.CreateField("@RSM_MBOM", "CreateDate", "Create Date", BoFieldTypes.db_Date, 250, false);
            DiManager.CreateField("@RSM_MBOM", "Currency", "Currency", BoFieldTypes.db_Alpha, 250, false);
            DiManager.CreateField("@RSM_MBOM", "ExchangeRateDate", "Exchange Rate Date", BoFieldTypes.db_Date, 250, false);
            DiManager.CreateField("@RSM_MBOM", "Rate", "Rate", BoFieldTypes.db_Float, 250, false, false, "", "", BoFldSubTypes.st_Rate);
            DiManager.CreateField("@RSM_MBOM", "OwnerCode", "Owner Code", BoFieldTypes.db_Alpha, 250, false);
            DiManager.CreateField("@RSM_MBOM", "OwnerName", "Owner Name", BoFieldTypes.db_Alpha, 250, false);
            DiManager.CreateField("@RSM_MBOM", "Version", "Version", BoFieldTypes.db_Alpha, 250, false);
            DiManager.CreateField("@RSM_MBOM", "PriceForSquareMeter", "Price For Square Meter", BoFieldTypes.db_Float, 250, false, false, "", "", BoFldSubTypes.st_Price);
            DiManager.CreateField("@RSM_MBOM", "TotalSquareMeter", "Total Square Meter", BoFieldTypes.db_Float, 250, false, false, "", "", BoFldSubTypes.st_Quantity);
            DiManager.CreateField("@RSM_MBOM", "ReferenceFeePercentage", "Reference Fee Percentage", BoFieldTypes.db_Float, 250, false, false, "", "", BoFldSubTypes.st_Percentage);
            DiManager.CreateField("@RSM_MBOM", "Quantity", "Quantity", BoFieldTypes.db_Float, 250, false, false, "", "", BoFldSubTypes.st_Quantity);

            DiManager.CreateField("@RSM_ELEM", "Element", "Element", BoFieldTypes.db_Alpha, 250, false);

            DiManager.CreateField("@RSM_MTRL", "ComponentCode", "Component Code", BoFieldTypes.db_Alpha, 250, false);
            DiManager.CreateField("@RSM_MTRL", "ComponentName", "ComponentName", BoFieldTypes.db_Alpha, 250, false);
            DiManager.CreateField("@RSM_MTRL", "UnitOfMeasure", "Unit Of Measure", BoFieldTypes.db_Alpha, 250, false);
            DiManager.CreateField("@RSM_MTRL", "Quantity", "Quantity", BoFieldTypes.db_Float, 250, false, false, "", "", BoFldSubTypes.st_Quantity);
            DiManager.CreateField("@RSM_MTRL", "UnitCost", "Unit Cost", BoFieldTypes.db_Float, 250, false, false, "", "", BoFldSubTypes.st_Price);
            DiManager.CreateField("@RSM_MTRL", "TotalCost", "Total Cost", BoFieldTypes.db_Float, 250, false, false, "", "", BoFldSubTypes.st_Price);
            DiManager.CreateField("@RSM_MTRL", "UnitRetailPrice", "Unit" +
                                                                  "Retail" +
                                                                  "Price", BoFieldTypes.db_Float, 250, false, false, "", "", BoFldSubTypes.st_Price);
            DiManager.CreateField("@RSM_MTRL", "UnitWorkingPrice", "Unit" +
                                                                  "Working" +
                                                                  "Price", BoFieldTypes.db_Float, 250, false, false, "", "", BoFldSubTypes.st_Price);

            DiManager.CreateField("@RSM_MTRL", "DiscountPercentage", "Discount Percentage", BoFieldTypes.db_Float, 250, false, false, "", "", BoFldSubTypes.st_Percentage);
            DiManager.CreateField("@RSM_MTRL", "DiscountAmount", "Discount Amount", BoFieldTypes.db_Float, 250, false, false, "", "", BoFldSubTypes.st_Sum);
            DiManager.CreateField("@RSM_MTRL", "SharedPercentage", "Shared Percentage", BoFieldTypes.db_Float, 250, false, false, "", "", BoFldSubTypes.st_Percentage);
            DiManager.CreateField("@RSM_MTRL", "SharedDiscountAmount", "Shared Amount", BoFieldTypes.db_Float, 250, false, false, "", "", BoFldSubTypes.st_Sum);
            DiManager.CreateField("@RSM_MTRL", "MarginPercentage", "Margin Percentage", BoFieldTypes.db_Float, 250, false, false, "", "", BoFldSubTypes.st_Percentage);
            DiManager.CreateField("@RSM_MTRL", "MarginAmount", "Margin Amount", BoFieldTypes.db_Float, 250, false, false, "", "", BoFldSubTypes.st_Sum);
            DiManager.CreateField("@RSM_MTRL", "FinalCustomerPrice", "Final Customer Price", BoFieldTypes.db_Float, 250, false, false, "", "", BoFldSubTypes.st_Price);
            DiManager.CreateField("@RSM_MTRL", "FinalCustomerPriceTotal", "Final Customer Price Total", BoFieldTypes.db_Float, 250, false, false, "", "", BoFldSubTypes.st_Sum);
            DiManager.CreateField("@RSM_MTRL", "ParentItemCode", "Parent Item Code", BoFieldTypes.db_Alpha, 250, false);
            DiManager.CreateField("@RSM_MTRL", "SalesQuotationDocEntry", "SalesQuotationDocEntry", BoFieldTypes.db_Alpha, 250, false);
            DiManager.CreateField("@RSM_MTRL", "Version", "Version", BoFieldTypes.db_Alpha, 250, false);
            DiManager.CreateField("@RSM_MTRL", "Currency", "Currency", BoFieldTypes.db_Alpha, 250, false);
            DiManager.CreateField("@RSM_MTRL", "ConvertedCurrency", "Currency", BoFieldTypes.db_Alpha, 250, false);

            DiManager.CreateField("@RSM_OPERATIONS", "OperationCode", "Operation Code", BoFieldTypes.db_Alpha, 250, false);
            DiManager.CreateField("@RSM_OPERATIONS", "OperationName", "Operation Name", BoFieldTypes.db_Alpha, 250, false);
            DiManager.CreateField("@RSM_OPERATIONS", "ProcessingTime", "Processing Time", BoFieldTypes.db_Alpha, 250, false);
            DiManager.CreateField("@RSM_OPERATIONS", "Uom", "Unit Of Measure", BoFieldTypes.db_Alpha, 250, false);
            DiManager.CreateField("@RSM_OPERATIONS", "ResourceCode", "Resource Code", BoFieldTypes.db_Alpha, 250, false);
            DiManager.CreateField("@RSM_OPERATIONS", "ResourceName", "Operation Name", BoFieldTypes.db_Alpha, 250, false);
            DiManager.CreateField("@RSM_OPERATIONS", "ResourceType", "Resource Type", BoFieldTypes.db_Alpha, 250, false);

            DiManager.CreateField("@RSM_RESOURCES", "SalesQuotationDocEntry", "Sales Quotation DocEntry", BoFieldTypes.db_Alpha, 250, false);
            DiManager.CreateField("@RSM_RESOURCES", "ParentItemCode", "Parent ItemCode", BoFieldTypes.db_Alpha, 250, false);
            DiManager.CreateField("@RSM_RESOURCES", "OperationKey", "Operation Key", BoFieldTypes.db_Alpha, 250, false);
            DiManager.CreateField("@RSM_RESOURCES", "ResourceCode", "Resource Code", BoFieldTypes.db_Alpha, 250, false);
            DiManager.CreateField("@RSM_RESOURCES", "ResourceName", "Resource Name", BoFieldTypes.db_Alpha, 250, false);
            DiManager.CreateField("@RSM_RESOURCES", "Uom", "Unit Of Measure", BoFieldTypes.db_Alpha, 250, false);
            DiManager.CreateField("@RSM_RESOURCES", "Quantity", "Quantity", BoFieldTypes.db_Float, 250, false, false, "", "", BoFldSubTypes.st_Quantity);
            DiManager.CreateField("@RSM_RESOURCES", "TimeRequired", "Time Required", BoFieldTypes.db_Float, 250, false, false, "", "", BoFldSubTypes.st_Sum);
            DiManager.CreateField("@RSM_RESOURCES", "StandartCost", "Standart Cost", BoFieldTypes.db_Float, 250, false, false, "", "", BoFldSubTypes.st_Sum);
            DiManager.CreateField("@RSM_RESOURCES", "TotalStandartCost", "Total Standart Cost", BoFieldTypes.db_Float, 250, false, false, "", "", BoFldSubTypes.st_Sum);
            DiManager.CreateField("@RSM_RESOURCES", "ResourceUnitPrice", "Resource Unit Price", BoFieldTypes.db_Float, 250, false, false, "", "", BoFldSubTypes.st_Price);
            DiManager.CreateField("@RSM_RESOURCES", "ResourceTotalPrice", "Resource Total Price", BoFieldTypes.db_Float, 250, false, false, "", "", BoFldSubTypes.st_Sum);
            DiManager.CreateField("@RSM_RESOURCES", "MarginPercent", "Margin Percent", BoFieldTypes.db_Float, 250, false, false, "", "", BoFldSubTypes.st_Percentage);
            DiManager.CreateField("@RSM_RESOURCES", "AmountOnUnit", "Amount On Unit", BoFieldTypes.db_Float, 250, false, false, "", "", BoFldSubTypes.st_Sum);
            DiManager.CreateField("@RSM_RESOURCES", "TotalAmount", "Total Amount", BoFieldTypes.db_Float, 250, false, false, "", "", BoFldSubTypes.st_Sum);
            DiManager.CreateField("@RSM_RESOURCES", "CostOfUnit", "Cost Of Unit", BoFieldTypes.db_Float, 250, false, false, "", "", BoFldSubTypes.st_Sum);
            DiManager.CreateField("@RSM_RESOURCES", "PriceOfUnit", "Price Of Unit", BoFieldTypes.db_Float, 250, false, false, "", "", BoFldSubTypes.st_Sum);
            DiManager.CreateField("@RSM_RESOURCES", "MarginOfUnit", "Margin Of Unit", BoFieldTypes.db_Float, 250, false, false, "", "", BoFldSubTypes.st_Sum);
            DiManager.CreateField("@RSM_RESOURCES", "InfoPercent", "Info Percent", BoFieldTypes.db_Float, 250, false, false, "", "", BoFldSubTypes.st_Percentage);
            DiManager.CreateField("@RSM_RESOURCES", "Version", "Version", BoFieldTypes.db_Alpha, 250, false);
            DiManager.CreateField("@RSM_RESOURCES", "OperationCode", "Operation Code", BoFieldTypes.db_Alpha, 250, false);
            DiManager.CreateField("@RSM_RESOURCES", "OperationName", "Operation Name", BoFieldTypes.db_Alpha, 250, false);
            DiManager.CreateField("@RSM_RESOURCES", "Version", "Version", BoFieldTypes.db_Alpha, 250, false);
            DiManager.CreateField("@RSM_RESOURCES", "UomResourceMain", "Uom Resource Main", BoFieldTypes.db_Alpha, 250, false);
            DiManager.CreateField("@RSM_RESOURCES", "OtherQtyResource", "OtherQtyResource", BoFieldTypes.db_Float, 250, false, false, "", "", BoFldSubTypes.st_Quantity);

            DiManager.CreateField("@RSM_OVERHEADS", "ParentItemCode", "Parent Item Code", BoFieldTypes.db_Alpha, 250, false);
            DiManager.CreateField("@RSM_OVERHEADS", "SalesQuotationDocEntry", "SalesQuotationDocEntry", BoFieldTypes.db_Alpha, 250, false);
            DiManager.CreateField("@RSM_OVERHEADS", "Version", "Version", BoFieldTypes.db_Alpha, 250, false);
            DiManager.CreateField("@RSM_OVERHEADS", "NumberOfDays", "Number Of Days", BoFieldTypes.db_Float, 250, false, false, "", "", BoFldSubTypes.st_Quantity);
            DiManager.CreateField("@RSM_OVERHEADS", "ManufacturingOverhead", "Manufacturing Overhead", BoFieldTypes.db_Float, 250, false, false, "", "", BoFldSubTypes.st_Sum);
            DiManager.CreateField("@RSM_OVERHEADS", "AdministrativeOverhead", "Administrative Overhead", BoFieldTypes.db_Float, 250, false, false, "", "", BoFldSubTypes.st_Sum);
            DiManager.CreateField("@RSM_OVERHEADS", "WorkingHours", "Working Hours", BoFieldTypes.db_Float, 250, false, false, "", "", BoFldSubTypes.st_Quantity);
            DiManager.CreateField("@RSM_OVERHEADS", "ChangeDate", "Change Date", BoFieldTypes.db_Date, 222, false);
 
            DiManager.CreateField("@RSM_OVERHEADS_R", "Version", "Version", BoFieldTypes.db_Alpha, 250, false);
            DiManager.CreateField("@RSM_OVERHEADS_R", "Corian", "Corian", BoFieldTypes.db_Float, 222, false, false, "", "", BoFldSubTypes.st_Quantity);
            DiManager.CreateField("@RSM_OVERHEADS_R", "Neolith", "Neolith", BoFieldTypes.db_Float, 222, false, false, "", "", BoFldSubTypes.st_Quantity);
            DiManager.CreateField("@RSM_OVERHEADS_R", "Furniture", "Furniture", BoFieldTypes.db_Float, 222, false, false, "", "", BoFldSubTypes.st_Quantity);
            DiManager.CreateField("@RSM_OVERHEADS_R", "Total", "Total", BoFieldTypes.db_Float, 222, false, false, "", "", BoFldSubTypes.st_Quantity);
            DiManager.CreateField("@RSM_OVERHEADS_R", "ComponentId", "Component Id", BoFieldTypes.db_Alpha, 250, false);
            DiManager.CreateField("@RSM_OVERHEADS_R", "ComponentName", "Component Name", BoFieldTypes.db_Alpha, 250, false);
            DiManager.CreateField("@RSM_OVERHEADS_R", "ChangeDate", "Change Date", BoFieldTypes.db_Date, 222, false);


            DiManager.CreateField("@RSM_OVERHEAD_C", "Component", "Component", BoFieldTypes.db_Alpha, 250, false);


            DiManager.CreateField("@RSM_OVRHD_CLCB", "DailyWorkHours", "Daily Work Hours", BoFieldTypes.db_Float, 222, false, false, "", "", BoFldSubTypes.st_Quantity);
            DiManager.CreateField("@RSM_OVRHD_CLCB", "WorkingDaysMonthly", "WorkingDaysMonthly", BoFieldTypes.db_Float, 222, false, false, "", "", BoFldSubTypes.st_Quantity);
            DiManager.CreateField("@RSM_OVRHD_CLCB", "ManufacuringOverhead", "Manufacuring Overhead", BoFieldTypes.db_Float, 222, false, false, "", "", BoFldSubTypes.st_Sum);
            DiManager.CreateField("@RSM_OVRHD_CLCB", "AdministrativeOverhead", "Administrative Overhead", BoFieldTypes.db_Float, 222, false, false, "", "", BoFldSubTypes.st_Sum);
            DiManager.CreateField("@RSM_OVRHD_CLCB", "CorianEmployee", "Corian Employee", BoFieldTypes.db_Float, 222, false, false, "", "", BoFldSubTypes.st_Sum);
            DiManager.CreateField("@RSM_OVRHD_CLCB", "FurnitureEmployee", "Furniture Employee", BoFieldTypes.db_Float, 222, false, false, "", "", BoFldSubTypes.st_Sum);
            DiManager.CreateField("@RSM_OVRHD_CLCB", "NeolithEmployee", "Neolith Employee", BoFieldTypes.db_Float, 222, false, false, "", "", BoFldSubTypes.st_Sum);
            DiManager.CreateField("@RSM_OVRHD_CLCB", "TotalEmps", "Total Employees", BoFieldTypes.db_Float, 222, false, false, "", "", BoFldSubTypes.st_Sum);
            DiManager.CreateField("@RSM_OVRHD_CLCB", "ChangeDate", "Change Date", BoFieldTypes.db_Date, 222, false);
            DiManager.CreateField("@RSM_OVRHD_CLCB", "Corian", "Corian", BoFieldTypes.db_Float, 222, false, false, "", "", BoFldSubTypes.st_Sum);
            DiManager.CreateField("@RSM_OVRHD_CLCB", "Neolith", "Neolith", BoFieldTypes.db_Float, 222, false, false, "", "", BoFldSubTypes.st_Sum);
            DiManager.CreateField("@RSM_OVRHD_CLCB", "Furniture", "Furniture", BoFieldTypes.db_Float, 222, false, false, "", "", BoFldSubTypes.st_Sum);
            DiManager.CreateField("@RSM_OVRHD_CLCB", "Version", "Version", BoFieldTypes.db_Alpha, 250, false);


            DiManager.CreateField("@RSM_COMMON_ELEM", "ProjectCode", "Project Code", BoFieldTypes.db_Alpha, 250, false);
            DiManager.CreateField("@RSM_COMMON_ELEM", "ChangeDate", "Change Date", BoFieldTypes.db_Date, 222, false);
            DiManager.CreateField("@RSM_COMMON_ELEM", "SalesQuotationDocEntry", "SalesQuotationDocEntry", BoFieldTypes.db_Alpha, 250, false);
            DiManager.CreateField("@RSM_COMMON_ELEM", "EmployeeQuantity", "Employee Quantity", BoFieldTypes.db_Float, 222, false, false, "", "", BoFldSubTypes.st_Quantity);
            DiManager.CreateField("@RSM_COMMON_ELEM", "DailyNormPerPerson", "Daily Norm Per Person", BoFieldTypes.db_Float, 222, false, false, "", "", BoFldSubTypes.st_Sum);
            DiManager.CreateField("@RSM_COMMON_ELEM", "QuantityOfDays", "Quantity Of Days", BoFieldTypes.db_Float, 222, false, false, "", "", BoFldSubTypes.st_Quantity);
            DiManager.CreateField("@RSM_COMMON_ELEM", "HostelCostPerDay", "Hostel Cost Per Day", BoFieldTypes.db_Float, 222, false, false, "", "", BoFldSubTypes.st_Sum);
            DiManager.CreateField("@RSM_COMMON_ELEM", "TotalHotelCost", "Total Hotel Cost", BoFieldTypes.db_Float, 222, false, false, "", "", BoFldSubTypes.st_Sum);
            DiManager.CreateField("@RSM_COMMON_ELEM", "TotalCost", "Total Cost", BoFieldTypes.db_Float, 222, false, false, "", "", BoFldSubTypes.st_Sum);
            DiManager.CreateField("@RSM_COMMON_ELEM", "TransportationAmount", "Transportation Amount", BoFieldTypes.db_Float, 222, false, false, "", "", BoFldSubTypes.st_Sum);

            DiManager.CreateField("@RSM_OVERHEADS_SQ", "ParentItemCode", "Parent Item Code", BoFieldTypes.db_Alpha, 250, false);
            DiManager.CreateField("@RSM_OVERHEADS_SQ", "OverheadType", "Overhead Type", BoFieldTypes.db_Alpha, 250, false);
            DiManager.CreateField("@RSM_OVERHEADS_SQ", "SalesQuotationDocEntry", "SalesQuotationDocEntry", BoFieldTypes.db_Alpha, 250, false);
            DiManager.CreateField("@RSM_OVERHEADS_SQ", "Version", "Version", BoFieldTypes.db_Alpha, 250, false);
            DiManager.CreateField("@RSM_OVERHEADS_SQ", "RequiredResource", "Required Resource", BoFieldTypes.db_Float, 222, false, false, "", "", BoFldSubTypes.st_Sum);
            DiManager.CreateField("@RSM_OVERHEADS_SQ", "UnitCost", "Cost Per", BoFieldTypes.db_Float, 222, false, false, "", "", BoFldSubTypes.st_Sum);
            DiManager.CreateField("@RSM_OVERHEADS_SQ", "TotalCost", "Total Per", BoFieldTypes.db_Float, 222, false, false, "", "", BoFldSubTypes.st_Sum);

            //DiManager.CreateField("@RSM_TRIP", "ParentItemCode", "Parent Item Code", BoFieldTypes.db_Alpha, 250, false);
            //DiManager.CreateField("@RSM_TRIP", "OverheadType", "Overhead Type", BoFieldTypes.db_Alpha, 250, false);
            //DiManager.CreateField("@RSM_TRIP", "SalesQuotationDocEntry", "SalesQuotationDocEntry", BoFieldTypes.db_Alpha, 250, false);
            //DiManager.CreateField("@RSM_TRIP", "EmployeeQty", "Employee Qty", BoFieldTypes.db_Float, 222, false, false, "", "", BoFldSubTypes.st_Sum);
            //DiManager.CreateField("@RSM_TRIP", "DailyNorm", "Daily Norm", BoFieldTypes.db_Float, 222, false, false, "", "", BoFldSubTypes.st_Sum);
            //DiManager.CreateField("@RSM_TRIP", "HotelUnitCost", "Hotel Unit Cost", BoFieldTypes.db_Float, 222, false, false, "", "", BoFldSubTypes.st_Sum);
            //DiManager.CreateField("@RSM_TRIP", "HotelTotalCost", "Hotel Total Cost", BoFieldTypes.db_Float, 222, false, false, "", "", BoFldSubTypes.st_Sum);
            //DiManager.CreateField("@RSM_TRIP", "TotalCost", "Total Cost", BoFieldTypes.db_Float, 222, false, false, "", "", BoFldSubTypes.st_Sum);


            DiManager.CreateField("ITT1", "Operation", "Operation", BoFieldTypes.db_Alpha, 250, false);
            DiManager.CreateField("ITT1", "QtyOfBom", "Qty Of Bom", BoFieldTypes.db_Float, 222, false, true, "", "", BoFldSubTypes.st_Sum);
            DiManager.CreateField("OITM", "SBU", "SBU", BoFieldTypes.db_Alpha, 250, false, false,"","",BoFldSubTypes.st_None, new Dictionary<dynamic, dynamic>() { {"01","Corian"}, {"02","Neolith"}, { "03", "Furniture" } });
            DiManager.CreateField("OCRD", "SharedDiscount", "Shared Discount", BoFieldTypes.db_Float, 222, false, true, "", "", BoFldSubTypes.st_Percentage);
        }
    }
}
