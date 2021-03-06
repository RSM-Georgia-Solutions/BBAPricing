
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using BBAPricing.Controllers;
using BBAPricing.Forms;
using BBAPricing.Models;
using ExcelImportDll;
using SAPbobsCOM;
using SAPbouiCOM;
using SAPbouiCOM.Framework;
using Application = SAPbouiCOM.Framework.Application;
using DataTable = System.Data.DataTable;

namespace BBAPricing
{

    [FormAttribute("BBAPricing.Import_Form", "Forms/Import Form.b1f")]
    class Import_Form : UserFormBase
    {
        private readonly SapBomModel SapBomModel;

        public Import_Form(SapBomModel sapBomModel)
        {
            SapBomModel = sapBomModel;
        }
        ExcelFileController excelFileController = new ExcelFileController();
        /// <summary>
        /// Initialize components. Called by framework after form created.
        /// </summary>
        public override void OnInitializeComponent()
        {
            this.Button0 = ((SAPbouiCOM.Button)(this.GetItem("Item_2").Specific));
            this.Button0.PressedAfter += new SAPbouiCOM._IButtonEvents_PressedAfterEventHandler(this.Button0_PressedAfter);
            this.EditText0 = ((SAPbouiCOM.EditText)(this.GetItem("Item_1").Specific));
            this.ComboBox0 = ((SAPbouiCOM.ComboBox)(this.GetItem("Item_5").Specific));
            this.Button1 = ((SAPbouiCOM.Button)(this.GetItem("Item_6").Specific));
            this.Button1.PressedAfter += new SAPbouiCOM._IButtonEvents_PressedAfterEventHandler(this.Button1_PressedAfter);
            this.PictureBox0 = ((SAPbouiCOM.PictureBox)(this.GetItem("Item_8").Specific));
            this.OptionBtn0 = ((SAPbouiCOM.OptionBtn)(this.GetItem("Item_3").Specific));
            this.OptionBtn1 = ((SAPbouiCOM.OptionBtn)(this.GetItem("Item_7").Specific));
            this.OnCustomInitialize();

        }

        /// <summary>
        /// Initialize form event. Called by framework before form creation.
        /// </summary>
        public override void OnInitializeFormEvents()
        {
        }

        private SAPbouiCOM.Button Button0;

        private void Button0_PressedAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            try
            {
                ShowFolder showFolder = new ShowFolder(EditText0, ComboBox0);
                showFolder.Open();
            }
            catch (Exception exception)
            {
                Application.SBO_Application.MessageBox($"{exception.Message}. {exception.InnerException?.Message}. {exception.InnerException?.InnerException?.Message}");
            }
        }

        private void OnCustomInitialize()
        {
            Assembly entryAssembly = Assembly.GetEntryAssembly();
            if (entryAssembly == null) return;
            var path = System.IO.Path.GetDirectoryName(entryAssembly.Location) + "\\Media\\Sap.bmp";
            PictureBox0.Picture = path;
            OptionBtn0.GroupWith(OptionBtn1.Item.UniqueID);
            OptionBtn0.Selected = true;
        }

        private SAPbouiCOM.EditText EditText0;
        private SAPbouiCOM.ComboBox ComboBox0;
        private SAPbouiCOM.Button Button1;

        private void Button1_PressedAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            SapBomModel.Rows.Clear();
            if (OptionBtn0.Selected)
            {
                try
                {
                    DataTable data = excelFileController.ReadExcelFile(ComboBox0.Selected.Description, EditText0.Value);
                    ImportMaterialsController.ParseDataTableToPricingMtrl(data, SapBomModel);
                    var key = ImportMaterialsController.ImportMtrl(SapBomModel);
                    Application.SBO_Application.OpenForm(BoFormObjectEnum.fo_ProductTree,"",key);

                }
                catch (Exception e)
                {
                    SAPbouiCOM.Framework.Application.SBO_Application.MessageBox(e.Message);
                }
            }
        }

        private SAPbouiCOM.PictureBox PictureBox0;
        private SAPbouiCOM.OptionBtn OptionBtn0;
        private SAPbouiCOM.OptionBtn OptionBtn1;
    }
}
