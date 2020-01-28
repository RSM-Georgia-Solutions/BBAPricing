using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using BBAPricing.Forms;
using SAPbobsCOM;
using SAPbouiCOM.Framework;

namespace BBAPricing
{
    class Menu
    {
        public void AddMenuItems()
        {
            SAPbouiCOM.Menus oMenus = null;
            SAPbouiCOM.MenuItem oMenuItem = null;

            oMenus = Application.SBO_Application.Menus;

            SAPbouiCOM.MenuCreationParams oCreationPackage = null;
            oCreationPackage = ((SAPbouiCOM.MenuCreationParams)(Application.SBO_Application.CreateObject(SAPbouiCOM.BoCreatableObjectType.cot_MenuCreationParams)));
            oMenuItem = Application.SBO_Application.Menus.Item("43520"); // moudles'

            oCreationPackage.Type = SAPbouiCOM.BoMenuType.mt_POPUP;
            oCreationPackage.UniqueID = "BBAPricing";
            oCreationPackage.String = "BBAPricing";
            oCreationPackage.Enabled = true;
            oCreationPackage.Position = -1;

            Assembly entryAssembly = Assembly.GetEntryAssembly();
            if (entryAssembly != null)
            {
                var path = System.IO.Path.GetDirectoryName(entryAssembly.Location) + "\\Media\\m_BBA (2).jpg";
                oCreationPackage.Image = path;
            }

            oMenus = oMenuItem.SubMenus;

            try
            {
                //  If the manu already exists this code will fail
                oMenus.RemoveEx(oCreationPackage.UniqueID);
            }
            catch (Exception e)
            {
                oMenus.AddEx(oCreationPackage);
            }
            try
            {
                oMenus.AddEx(oCreationPackage);
            }
            catch (Exception)
            {
            }
            try
            {
                // Get the menu collection of the newly added pop-up item
                oMenuItem = Application.SBO_Application.Menus.Item("BBAPricing");
                oMenus = oMenuItem.SubMenus;

                oCreationPackage.Type = SAPbouiCOM.BoMenuType.mt_STRING;
                oCreationPackage.UniqueID = "BBAPricing.Forms.OverheadParams";
                oCreationPackage.String = "Overhead Params";
                oMenus.AddEx(oCreationPackage);

                oCreationPackage.Type = SAPbouiCOM.BoMenuType.mt_STRING;
                oCreationPackage.UniqueID = "BBAPricing.Forms.Overheads";
                oCreationPackage.String = "Overheads";
                oMenus.AddEx(oCreationPackage);


                // Create s sub menu
                oCreationPackage.Type = SAPbouiCOM.BoMenuType.mt_STRING;
                oCreationPackage.UniqueID = "BBAPricing.Forms.Pricing";
                oCreationPackage.String = "Pricing";
                oMenus.AddEx(oCreationPackage);
                // Create s sub menu
                //oCreationPackage.Type = SAPbouiCOM.BoMenuType.mt_STRING;
                //oCreationPackage.UniqueID = "BBAPricing.Import_Form";
                //oCreationPackage.String = "Import";
                //oMenus.AddEx(oCreationPackage);
                // Create s sub menu
                oCreationPackage.Type = SAPbouiCOM.BoMenuType.mt_STRING;
                oCreationPackage.UniqueID = "BBAPricing.Forms.Initialization";
                oCreationPackage.String = "Initialization";
                oMenus.AddEx(oCreationPackage);

                // Create s sub menu
                oCreationPackage.Type = SAPbouiCOM.BoMenuType.mt_STRING;
                oCreationPackage.UniqueID = "BBAPricing.Forms.SettingsForm";
                oCreationPackage.String = "Settings";
                oMenus.AddEx(oCreationPackage);
            }
            catch (Exception er)
            { //  Menu already exists
                Application.SBO_Application.SetStatusBarMessage("Menu Already Exists", SAPbouiCOM.BoMessageTime.bmt_Short, true);
            }
        }

        public void SBO_Application_MenuEvent(ref SAPbouiCOM.MenuEvent pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;

            try
            {
                //if (pVal.BeforeAction && pVal.MenuUID == "BBAPricing.Import_Form")
                //{
                //    Import_Form activeForm = new Import_Form();
                //    activeForm.Show();
                //}
                //else if (pVal.BeforeAction && pVal.MenuUID == "BBAPricing.Forms.Pricing")
                //{
                //    //Pricing activeForm = new Pricing();
                //    //activeForm.Show();
                //}
                if (pVal.BeforeAction && pVal.MenuUID == "BBAPricing.Forms.Initialization")
                {
                    InitializationForm activeForm = new InitializationForm();
                    activeForm.Show();
                }
                else if (pVal.BeforeAction && pVal.MenuUID == "BBAPricing.Forms.Overheads")
                {
                    Overheads activeForm = new Overheads();
                    activeForm.Show();
                }
                else if (pVal.BeforeAction && pVal.MenuUID == "BBAPricing.Forms.OverheadParams")
                {
                    OverheadParams activeForm = new OverheadParams();
                    activeForm.Show();
                }
                else if (pVal.BeforeAction && pVal.MenuUID == "BBAPricing.Forms.SettingsForm")
                {
                    SettingsForm activeForm = new SettingsForm();
                    activeForm.Show();
                }
                else if (pVal.BeforeAction && pVal.MenuUID == "1288")
                {
                    string formTittle = Application.SBO_Application.Forms.ActiveForm.Title;
                    var activeForm = Application.SBO_Application.Forms.ActiveForm;
                    if (formTittle == "Overhead Params")
                    {
                        if (activeForm.DataSources.DBDataSources.Item(0).Offset + 1 == activeForm.DataSources.DBDataSources.Item(0).Size)
                        {
                            return;
                        }
                        activeForm.DataSources.DBDataSources.Item(0).Offset =
                            activeForm.DataSources.DBDataSources.Item(0).Offset + 1;
                    }
                }
                else if (pVal.BeforeAction && pVal.MenuUID == "1289")
                {
                    string formTittle = Application.SBO_Application.Forms.ActiveForm.Title;
                    var activeForm = Application.SBO_Application.Forms.ActiveForm;
                    if (formTittle == "Overhead Params")
                    {
                        if (activeForm.DataSources.DBDataSources.Item(0).Offset == 0)
                        {
                            return;
                        }
                        activeForm.DataSources.DBDataSources.Item(0).Offset =
                            activeForm.DataSources.DBDataSources.Item(0).Offset - 1;
                    }
                }
                else if (pVal.BeforeAction && pVal.MenuUID == "1290")
                {
                    string formTittle = Application.SBO_Application.Forms.ActiveForm.Title;
                    var activeForm = Application.SBO_Application.Forms.ActiveForm;
                    if (formTittle == "Overhead Params")
                    {
                        activeForm.DataSources.DBDataSources.Item(0).Offset = 0;
                    }
                }
                else if (pVal.BeforeAction && pVal.MenuUID == "1291")
                {
                    string formTittle = Application.SBO_Application.Forms.ActiveForm.Title;
                    var activeForm = Application.SBO_Application.Forms.ActiveForm;
                    if (formTittle == "Overhead Params")
                    {
                        activeForm.DataSources.DBDataSources.Item(0).Offset =
                            activeForm.DataSources.DBDataSources.Item(0).Size - 1;
                    }
                }
            }
            catch (Exception ex)
            {
                Application.SBO_Application.MessageBox(ex.ToString(), 1, "Ok", "", "");
            }
        }

    }
}
