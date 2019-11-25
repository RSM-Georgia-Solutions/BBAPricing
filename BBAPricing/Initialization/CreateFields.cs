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
            DiManager.CreateField("@RSM_MBOM_ROWS", "ElementID", "Element ID", BoFieldTypes.db_Alpha,250,false);
            DiManager.CreateField("@RSM_MBOM_ROWS", "Element", "Element", BoFieldTypes.db_Alpha,250,false);
            DiManager.CreateField("@RSM_MBOM_ROWS", "Cost", "Cost", BoFieldTypes.db_Float,250,false,false,"","",BoFldSubTypes.st_Sum);
            DiManager.CreateField("@RSM_MBOM_ROWS", "Price", "Price", BoFieldTypes.db_Float,250, false, false, "", "", BoFldSubTypes.st_Price);
            DiManager.CreateField("@RSM_MBOM_ROWS", "Margin", "Margin", BoFieldTypes.db_Float,250, false, false, "", "", BoFldSubTypes.st_Sum);
            DiManager.CreateField("@RSM_MBOM_ROWS", "FinalCustomerPrice", "Final Customer Price", BoFieldTypes.db_Float,250, false, false, "", "", BoFldSubTypes.st_Sum);
            DiManager.CreateField("@RSM_MBOM_ROWS", "Percent", "%", BoFieldTypes.db_Float, 250, false, false, "", "", BoFldSubTypes.st_Percentage);
            DiManager.CreateField("@RSM_MBOM_ROWS", "I", "I", BoFieldTypes.db_Float, 250, false, false, "", "", BoFldSubTypes.st_Percentage);
            DiManager.CreateField("@RSM_MBOM_ROWS", "II", "II", BoFieldTypes.db_Float, 250, false, false, "", "", BoFldSubTypes.st_Percentage);
            DiManager.CreateField("@RSM_MBOM_ROWS", "III", "III", BoFieldTypes.db_Float, 250, false, false, "", "", BoFldSubTypes.st_Percentage);
            DiManager.CreateField("@RSM_MBOM_ROWS", "ParentItemCode", "Parent Item Code", BoFieldTypes.db_Alpha, 250, false);
            DiManager.CreateField("@RSM_MBOM_ROWS", "SalesQuotationDocEntry", "Sales Quotation DocEntry", BoFieldTypes.db_Alpha, 250, false);
            DiManager.CreateField("@RSM_MBOM_ROWS", "Version", "Version", BoFieldTypes.db_Alpha, 250, false);


            DiManager.CreateField("@RSM_MBOM", "CostCenter", "Cost Center", BoFieldTypes.db_Alpha, 250, false);
            DiManager.CreateField("@RSM_MBOM", "ParentItem", "Parent Item", BoFieldTypes.db_Alpha, 250, false);
            DiManager.CreateField("@RSM_MBOM", "SalesQuotationDocEntry", "Sales Quotation DocEntry", BoFieldTypes.db_Alpha, 250,false);
            DiManager.CreateField("@RSM_MBOM", "SalesQuotationDocNum", "Sales Quotation DocNum", BoFieldTypes.db_Numeric, 11, false);
            DiManager.CreateField("@RSM_MBOM", "ProjectCode", "Project Code", BoFieldTypes.db_Alpha, 250, false);
            DiManager.CreateField("@RSM_MBOM", "ProjectName", "Project Name", BoFieldTypes.db_Alpha, 250, false);
            DiManager.CreateField("@RSM_MBOM", "CreateDate", "Create Date", BoFieldTypes.db_Date, 250, false);
            DiManager.CreateField("@RSM_MBOM", "Currency", "Currency", BoFieldTypes.db_Alpha, 250, false);
            DiManager.CreateField("@RSM_MBOM", "ExchangeRateDate", "Exchange Rate Date", BoFieldTypes.db_Date, 250, false);
            DiManager.CreateField("@RSM_MBOM", "Rate", "Rate", BoFieldTypes.db_Float, 250, false, false, "","",BoFldSubTypes.st_Rate);
            DiManager.CreateField("@RSM_MBOM", "OwnerCode", "Owner Code", BoFieldTypes.db_Alpha, 250, false);
            DiManager.CreateField("@RSM_MBOM", "OwnerName", "Owner Name", BoFieldTypes.db_Alpha, 250, false);
            DiManager.CreateField("@RSM_MBOM", "Version", "Version", BoFieldTypes.db_Alpha, 250, false);
            DiManager.CreateField("@RSM_MBOM", "PriceForSquareMeter", "Price For Square Meter", BoFieldTypes.db_Float, 250, false, false, "", "", BoFldSubTypes.st_Price);

            DiManager.CreateField("@RSM_ELEM", "Element", "Element", BoFieldTypes.db_Alpha, 250, false);  

            DiManager.CreateField("@RSM_MTRL", "ComponentCode", "Component Code", BoFieldTypes.db_Alpha, 250, false);
            DiManager.CreateField("@RSM_MTRL", "ComponentName", "ComponentName", BoFieldTypes.db_Alpha, 250, false);
            DiManager.CreateField("@RSM_MTRL", "UnitOfMeasure", "Unit Of Measure", BoFieldTypes.db_Alpha, 250, false);
            DiManager.CreateField("@RSM_MTRL", "Quantity", "Quantity", BoFieldTypes.db_Float, 250, false,false,"","",BoFldSubTypes.st_Quantity);
            DiManager.CreateField("@RSM_MTRL", "UnitCost", "Unit Cost", BoFieldTypes.db_Float, 250, false,false,"","",BoFldSubTypes.st_Price);
            DiManager.CreateField("@RSM_MTRL", "TotalCost", "Total Cost", BoFieldTypes.db_Float, 250, false,false,"","",BoFldSubTypes.st_Price);
            DiManager.CreateField("@RSM_MTRL", "UnitRetailPrice", "Unit" +
                                                                  "Retail" +
                                                                  "Price", BoFieldTypes.db_Float, 250, false,false,"","",BoFldSubTypes.st_Price);
            DiManager.CreateField("@RSM_MTRL", "UnitWorkingPrice", "Unit" +
                                                                  "Working" +
                                                                  "Price", BoFieldTypes.db_Float, 250, false, false, "", "", BoFldSubTypes.st_Price);

            DiManager.CreateField("@RSM_MTRL", "UnitRetailPriceConverted", "Unit" +
                                                       "Retail" +
                                                       "Price", BoFieldTypes.db_Float, 250, false, false, "", "", BoFldSubTypes.st_Price);
            DiManager.CreateField("@RSM_MTRL", "UnitWorkingPriceConverted", "Unit" +
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


            DiManager.CreateField("@RSM_OPERATIONS", "OperationCode", "Operat  ion Code", BoFieldTypes.db_Alpha, 250, false);          
            DiManager.CreateField("@RSM_OPERATIONS", "OperationName", "Operation Name", BoFieldTypes.db_Alpha, 250, false);
            DiManager.CreateField("@RSM_OPERATIONS", "ProcessingTime", "Processing Time", BoFieldTypes.db_Alpha, 250, false);
            DiManager.CreateField("@RSM_OPERATIONS", "Uom", "Unit Of Measure", BoFieldTypes.db_Alpha, 250, false);

            DiManager.CreateField("@RSM_OPRESEMP", "ResourceCode", "Resource Code", BoFieldTypes.db_Alpha, 250, false);
            DiManager.CreateField("@RSM_OPRESEMP", "ResourceType", "Resource Type", BoFieldTypes.db_Alpha, 250, false);
            DiManager.CreateField("@RSM_OPRESEMP", "OperationCode", "Operation Code", BoFieldTypes.db_Alpha, 250, false);

            DiManager.CreateField("@RSM_RESOURCES", "SalesQuotationDocEntry", "Sales Quotation DocEntry", BoFieldTypes.db_Alpha, 250, false);            
            DiManager.CreateField("@RSM_RESOURCES", "ParentItemCode", "Parent ItemCode", BoFieldTypes.db_Alpha, 250, false);
            DiManager.CreateField("@RSM_RESOURCES", "OperationKey", "Operation Key", BoFieldTypes.db_Alpha, 250, false);
            DiManager.CreateField("@RSM_RESOURCES", "ResourceCode", "Resource Code", BoFieldTypes.db_Alpha, 250, false);
            DiManager.CreateField("@RSM_RESOURCES", "ResourceName", "Resource Name", BoFieldTypes.db_Alpha, 250, false);
            DiManager.CreateField("@RSM_RESOURCES", "Uom", "Unit Of Measure", BoFieldTypes.db_Alpha, 250, false);
            DiManager.CreateField("@RSM_RESOURCES", "Quantity", "Quantity", BoFieldTypes.db_Float, 250, false,false,"","",BoFldSubTypes.st_Quantity);
            DiManager.CreateField("@RSM_RESOURCES", "TimeRequired", "Time Required", BoFieldTypes.db_Float, 250, false,false,"","",BoFldSubTypes.st_Sum);
            DiManager.CreateField("@RSM_RESOURCES", "StandartCost", "Standart Cost", BoFieldTypes.db_Float, 250, false,false,"","",BoFldSubTypes.st_Sum);
            DiManager.CreateField("@RSM_RESOURCES", "TotalStandartCost", "Total Standart Cost", BoFieldTypes.db_Float, 250, false,false,"","",BoFldSubTypes.st_Sum);
            DiManager.CreateField("@RSM_RESOURCES", "ResourceUnitPrice", "Resource Unit Price", BoFieldTypes.db_Float, 250, false,false,"","",BoFldSubTypes.st_Price);
            DiManager.CreateField("@RSM_RESOURCES", "ResourceTotalPrice", "Resource Total Price", BoFieldTypes.db_Float, 250, false,false,"","",BoFldSubTypes.st_Sum);
            DiManager.CreateField("@RSM_RESOURCES", "MarginPercent", "Margin Percent", BoFieldTypes.db_Float, 250, false,false,"","",BoFldSubTypes.st_Percentage);
            DiManager.CreateField("@RSM_RESOURCES", "AmountOnUnit", "Amount On Unit", BoFieldTypes.db_Float, 250, false, false, "", "", BoFldSubTypes.st_Sum);
            DiManager.CreateField("@RSM_RESOURCES", "TotalAmount", "Total Amount", BoFieldTypes.db_Float, 250, false, false, "", "", BoFldSubTypes.st_Sum);
            DiManager.CreateField("@RSM_RESOURCES", "CostOfUnit", "Cost Of Unit", BoFieldTypes.db_Float, 250, false, false, "", "", BoFldSubTypes.st_Sum);
            DiManager.CreateField("@RSM_RESOURCES", "PriceOfUnit", "Price Of Unit", BoFieldTypes.db_Float, 250, false, false, "", "", BoFldSubTypes.st_Sum);
            DiManager.CreateField("@RSM_RESOURCES", "MarginOfUnit", "Margin Of Unit", BoFieldTypes.db_Float, 250, false, false, "", "", BoFldSubTypes.st_Sum);
            DiManager.CreateField("@RSM_RESOURCES", "InfoPercent", "Info Percent", BoFieldTypes.db_Float, 250, false, false, "", "", BoFldSubTypes.st_Percentage);
            DiManager.CreateField("@RSM_RESOURCES", "Version", "Version", BoFieldTypes.db_Alpha, 250, false);



        }
    }
}
