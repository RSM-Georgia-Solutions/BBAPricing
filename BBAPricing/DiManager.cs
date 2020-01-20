using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAPbobsCOM;
using System.Reflection;
using System.Globalization;

namespace BBAPricing
{
    public static class DiManager
    {
        public static double GetCurrencyRate(string curCode, DateTime date, Company xCompany)
        {
            try
            {

                if (GetLocalCurrencyCode(xCompany) == GetSystemCurrencyCode(xCompany) && curCode == GetSystemCurrencyCode(xCompany) || curCode == GetLocalCurrencyCode(xCompany))
                {
                    return 1.0;
                }
                Company oCompany = xCompany;
                SBObob oSbObob = (SBObob)oCompany.GetBusinessObject(BoObjectTypes.BoBridge);
                Recordset oRecordSet = oSbObob.GetCurrencyRate(curCode, date.Date);
                return double.Parse(oRecordSet.Fields.Item(0).Value.ToString(), CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                SAPbouiCOM.Framework.Application.SBO_Application.MessageBox(ex.Message, 1, "OK");
            }
            return 0;
        }
        public static string GetLocalCurrencyCode(SAPbobsCOM.Company xCompany)
        {
            try
            {
                Company oCompany = xCompany;

                SBObob oSbObob = (SBObob)oCompany.GetBusinessObject(BoObjectTypes.BoBridge);
                Recordset oRecordSet = oSbObob.GetLocalCurrency();
                return oRecordSet.Fields.Item(0).Value.ToString();
            }
            catch (Exception ex)
            {
                SAPbouiCOM.Framework.Application.SBO_Application.MessageBox(ex.Message, 1, "OK");
            }
            return "0";
        }
        public static string GetSystemCurrencyCode(Company xCompany)
        {
            try
            {
                Company oCompany = xCompany;

                SBObob oSbObob = (SBObob)oCompany.GetBusinessObject(BoObjectTypes.BoBridge);
                Recordset oRecordSet = oSbObob.GetSystemCurrency();
                return oRecordSet.Fields.Item(0).Value.ToString();
            }
            catch (Exception ex)
            {
                SAPbouiCOM.Framework.Application.SBO_Application.MessageBox(ex.Message, 1, "OK");
            }
            return "0";
        }
        public static List<PropertyInfo> GetPropInfo(Type classType)
        {
            List<PropertyInfo> props = classType.GetProperties().ToList();
            return props;
        }
        public static object GetPropValue(object src, string propName)
        {
            try
            {
                return src.GetType().GetProperty(propName).GetValue(src, null);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static Company Company => XCompany.Value;
        public static CompanyService CompanyService => companyService.Value;

        private static readonly Lazy<Company> XCompany =
            new Lazy<Company>(() => (Company)SAPbouiCOM.Framework
                .Application
                .SBO_Application
                .Company.GetDICompany());

        private static readonly Lazy<CompanyService> companyService =
            new Lazy<CompanyService>(() => Company.GetCompanyService());
        public static string CreateTable(string tableName, string tableDescription, BoUTBTableType tableType)
        {
            try
            {
                UserTablesMD oUTables = (UserTablesMD)Company.GetBusinessObject(BoObjectTypes.oUserTables);

                if (oUTables.GetByKey(tableName) == false)
                {
                    oUTables.TableName = tableName;
                    oUTables.TableDescription = tableDescription;
                    oUTables.TableType = tableType;
                    int ret = oUTables.Add();

                    return ret == 0 ? "" : Company.GetLastErrorDescription();
                }

                System.Runtime.InteropServices.Marshal.ReleaseComObject(oUTables);
                return string.Empty;

            }
            catch (Exception e)
            {
                return $"exeption : {e.Message} sap error : {Company.GetLastErrorDescription()}";
            }
            finally
            {
                GC.Collect();
            }

        }
        public static void AddFindForm(IUserObjectsMD oUserObjectMD, string name, string description, bool isEditable)
        {
            oUserObjectMD.FindColumns.Add();
            oUserObjectMD.FindColumns.ColumnAlias = "U_" + name;
            oUserObjectMD.FindColumns.ColumnDescription = description;

            oUserObjectMD.FormColumns.Add();
            oUserObjectMD.FormColumns.FormColumnAlias = "U_" + name;
            oUserObjectMD.FormColumns.FormColumnDescription = description;
            oUserObjectMD.FormColumns.Editable = isEditable ? BoYesNoEnum.tYES : BoYesNoEnum.tNO;
        }
        public static void AddKey(SAPbobsCOM.Company company, string tablename, string keyname, string fieldAlias, BoYesNoEnum IsUnique, string secondKeyAlias = "")
        {
            int result;
            UserKeysMD oUkey = (UserKeysMD)company.GetBusinessObject(BoObjectTypes.oUserKeys);
            try
            {

                oUkey.TableName = "@" + tablename;
                oUkey.KeyName = keyname;
                oUkey.Elements.ColumnAlias = fieldAlias;
                oUkey.Unique = IsUnique;
                oUkey.Elements.Add();
                if (secondKeyAlias != "")
                {
                    oUkey.Elements.ColumnAlias = secondKeyAlias;
                }
                result = oUkey.Add();

                if (result == 0)
                {
                    //  // System.Windows.Forms.MessageBox.Show("nice");
                }
                else
                {
                    string str = company.GetLastErrorDescription();
                    // System.Windows.Forms.MessageBox.Show("ERROR : " + _company.GetLastErrorDescription());
                }
            }
            catch (Exception ex)
            {
                // System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }
        public static int DbInsert(string tableName, Dictionary<string, dynamic> obj, SAPbobsCOM.Company company)
        {
            UserTable userTable = company.UserTables.Item(tableName);

            foreach (Field field in userTable.UserFields.Fields)
            {
                field.Value = string.Empty;
            }

            foreach (var field in obj)
            {
                userTable.UserFields.Fields.Item("U_" + field.Key).Value = field.Value;
            }
            int ret = userTable.Add();
            if (ret != 0)
            {
                SAPbouiCOM.Framework.Application.SBO_Application.SetStatusBarMessage("Error : " + company.GetLastErrorDescription(), SAPbouiCOM.BoMessageTime.bmt_Short);
            }

            return ret;
        }

        public static string CreateUdo(string udoCode, string udoDescription, string headerTable, string chidlTable, int position, int fatherMenuId)
        {
            SAPbobsCOM.UserObjectsMD udo = (SAPbobsCOM.UserObjectsMD)DiManager.Company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserObjectsMD);
            bool updateFlag = udo.GetByKey(udoCode);

            udo.Code = udoCode;
            udo.Name = udoDescription;
            udo.ObjectType = BoUDOObjType.boud_Document;
            udo.TableName = headerTable;
            udo.ChildTables.TableName = chidlTable;
            udo.ChildTables.Add();


            udo.CanCancel = BoYesNoEnum.tYES;
            udo.CanClose = BoYesNoEnum.tYES;
            udo.CanDelete = BoYesNoEnum.tNO;
            udo.CanFind = BoYesNoEnum.tYES;
            udo.MenuCaption = udoDescription;
            udo.CanCreateDefaultForm = BoYesNoEnum.tYES;
            udo.EnableEnhancedForm = BoYesNoEnum.tYES;
            udo.MenuItem = BoYesNoEnum.tYES;
            udo.Position = position;
            udo.FatherMenuID = fatherMenuId;
            udo.MenuUID = udoCode;

            udo.FormColumns.FormColumnAlias = "DocEntry";
            udo.FormColumns.FormColumnDescription = "DocEntry";
            udo.FormColumns.Add();




            return updateFlag ? udo.Update() != 0 ? Company.GetLastErrorDescription() : string.Empty : udo.Add() != 0 ? Company.GetLastErrorDescription() : string.Empty;
        }

        public static string CreateField(string tablename, string fieldname, string description, BoFieldTypes type, int size, bool isMandatory, bool isSapTable = false, string likedToTAble = "", string defaultValue = "", BoFldSubTypes subType = BoFldSubTypes.st_None, Dictionary<dynamic, dynamic> validValues = null)
        {
            // Get a new Recordset object
            Recordset oRecordSet = (Recordset)Company.GetBusinessObject(BoObjectTypes.BoRecordset);
            string sqlQuery = $"SELECT T0.TableID, T0.FieldID FROM CUFD T0 WHERE T0.TableID = '{tablename}' AND T0.AliasID = '{fieldname}'";
            oRecordSet.DoQuery(sqlQuery);
            var updateFlag = oRecordSet.RecordCount == 1;
            var fieldId = int.Parse(oRecordSet.Fields.Item("FieldID").Value.ToString(), CultureInfo.InvariantCulture);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(oRecordSet);

            UserFieldsMD oUfield = (UserFieldsMD)Company.GetBusinessObject(BoObjectTypes.oUserFields);
            if (updateFlag)
            {
                oUfield.GetByKey(tablename, fieldId);
            }
            try
            {
                oUfield.TableName = tablename;
                oUfield.Name = fieldname;
                oUfield.Description = description;
                oUfield.Type = type;
                oUfield.Mandatory = isMandatory ? BoYesNoEnum.tYES : BoYesNoEnum.tNO;
                oUfield.DefaultValue = defaultValue;
                if (type == BoFieldTypes.db_Float)
                {
                    oUfield.SubType = subType;
                }

                if (type == BoFieldTypes.db_Alpha || type == BoFieldTypes.db_Numeric)
                {
                    oUfield.EditSize = size;
                }

                if (validValues != null)
                {
                    foreach (KeyValuePair<dynamic, dynamic> keyValuePair in validValues)
                    {
                        oUfield.ValidValues.Value = keyValuePair.Key;
                        oUfield.ValidValues.Description = keyValuePair.Value;
                        oUfield.ValidValues.Add();
                    }
                }
                oUfield.LinkedTable = likedToTAble;
                int ret = updateFlag ? oUfield.Update() : oUfield.Add();
                if (ret == 0)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oUfield);
                    return string.Empty;
                }
                else
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oUfield);
                    return Company.GetLastErrorDescription();
                }
            }
            catch (Exception e)
            {
                return $"exeption : {e.Message}, Sap Error {Company.GetLastErrorDescription()}";
            }
            finally
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oUfield);
            }

        }

        public static void GetSettings()
        {
            Recordset recSet = (Recordset)Company.GetBusinessObject(BoObjectTypes.BoRecordset);
            recSet.DoQuery($"SELECT * FROM [@RSM_BBA_SETTINGS]");
            if (recSet.EoF)
            {
                return;
            }
            Settings.DailyNormPerPerson = (double) recSet.Fields.Item($"U_DailyNormPerPerson").Value;
            Settings.HumanResourceCoefficient = (double) recSet.Fields.Item($"U_DailyNormPerPerson").Value;
            Settings.RetailPriceList = recSet.Fields.Item($"U_RetailPriceList").Value.ToString();
            Settings.WorkingPriceList = recSet.Fields.Item($"U_WorkingPriceList").Value.ToString();
        }
    }
}
