﻿using System;
using System.Collections.Generic;
using System.Text;
using BBAPricing.Forms;
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

            oMenus = oMenuItem.SubMenus;

            try
            {
                //  If the manu already exists this code will fail
                oMenus.AddEx(oCreationPackage);
            }
            catch (Exception e)
            {

            }

            try
            {
                // Get the menu collection of the newly added pop-up item
                oMenuItem = Application.SBO_Application.Menus.Item("BBAPricing");
                oMenus = oMenuItem.SubMenus;
                // Create s sub menu
                oCreationPackage.Type = SAPbouiCOM.BoMenuType.mt_STRING;
                oCreationPackage.UniqueID = "BBAPricing.Forms.Pricing";
                oCreationPackage.String = "Pricing";
                oMenus.AddEx(oCreationPackage);
                // Create s sub menu
                oCreationPackage.Type = SAPbouiCOM.BoMenuType.mt_STRING;
                oCreationPackage.UniqueID = "BBAPricing.Import_Form";
                oCreationPackage.String = "Import";
                oMenus.AddEx(oCreationPackage);
                // Create s sub menu
                oCreationPackage.Type = SAPbouiCOM.BoMenuType.mt_STRING;
                oCreationPackage.UniqueID = "BBAPricing.Forms.Initialization";
                oCreationPackage.String = "Initialization";
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
                if (pVal.BeforeAction && pVal.MenuUID == "BBAPricing.Import_Form")
                {
                    Import_Form activeForm = new Import_Form();
                    activeForm.Show();
                }
                else if (pVal.BeforeAction && pVal.MenuUID == "BBAPricing.Forms.Pricing")
                {
                    //Pricing activeForm = new Pricing();
                    //activeForm.Show();
                }
                else if (pVal.BeforeAction && pVal.MenuUID == "BBAPricing.Forms.Initialization")
                {
                    InitializationForm activeForm = new InitializationForm();
                    activeForm.Show();
                }
            }
            catch (Exception ex)
            {
                Application.SBO_Application.MessageBox(ex.ToString(), 1, "Ok", "", "");
            }
        }

    }
}
