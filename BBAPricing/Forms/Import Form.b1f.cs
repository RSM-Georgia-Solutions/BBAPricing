
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using BBAPricing.Controllers;
using BBAPricing.Forms;
using ExcelImportDll;
using SAPbouiCOM.Framework;

namespace BBAPricing
{

    [FormAttribute("BBAPricing.Import_Form", "Forms/Import Form.b1f")]
    class Import_Form : UserFormBase
    {
        public Import_Form()
        {
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
        }

        private SAPbouiCOM.EditText EditText0;
        private SAPbouiCOM.ComboBox ComboBox0;
        private SAPbouiCOM.Button Button1;

        private void Button1_PressedAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            try
            {
                DataTable data;
                data = excelFileController.ReadExcelFile(ComboBox0.Selected.Description, EditText0.Value);
                var  x = MaterialsController.ParseDataTAbleToPricing(data);
            }
            catch (Exception e)
            {
 
            }
            CalculationMaterials calc = new CalculationMaterials();
            calc.Show();

        }

        private SAPbouiCOM.PictureBox PictureBox0;
    }
}
