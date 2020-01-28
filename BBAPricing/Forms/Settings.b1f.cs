using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using BBAPricing.Models;
using SAPbobsCOM;
using SAPbouiCOM.Framework;
using BoMessageTime = SAPbouiCOM.BoMessageTime;
using BoStatusBarMessageType = SAPbouiCOM.BoStatusBarMessageType;
using EditText = SAPbouiCOM.EditText;

namespace BBAPricing.Forms
{
    [FormAttribute("BBAPricing.Forms.SettingsForm", "Forms/Settings.b1f")]
    class SettingsForm : UserFormBase
    {
        public SettingsModel _Model;
        public SettingsForm()
        {
            _Model = new SettingsModel();
        }

        /// <summary>
        /// Initialize components. Called by framework after form created.
        /// </summary>
        public override void OnInitializeComponent()
        {
            this.StaticText0 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_0").Specific));
            this.EditText0 = ((SAPbouiCOM.EditText)(this.GetItem("Item_1").Specific));
            this.StaticText1 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_2").Specific));
            this.EditText1 = ((SAPbouiCOM.EditText)(this.GetItem("Item_3").Specific));
            this.StaticText2 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_4").Specific));
            this.EditText2 = ((SAPbouiCOM.EditText)(this.GetItem("Item_5").Specific));
            this.Button0 = ((SAPbouiCOM.Button)(this.GetItem("Item_6").Specific));
            this.Button0.PressedAfter += new SAPbouiCOM._IButtonEvents_PressedAfterEventHandler(this.Button0_PressedAfter);
            this.StaticText3 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_7").Specific));
            this.EditText3 = ((SAPbouiCOM.EditText)(this.GetItem("Item_8").Specific));
            this.EditText4 = ((SAPbouiCOM.EditText)(this.GetItem("Item_9").Specific));
            this.StaticText4 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_10").Specific));
            this.EditText5 = ((SAPbouiCOM.EditText)(this.GetItem("Item_11").Specific));
            this.StaticText5 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_12").Specific));
            this.OnCustomInitialize();

        }

        /// <summary>
        /// Initialize form event. Called by framework before form creation.
        /// </summary>
        public override void OnInitializeFormEvents()
        {
            this.VisibleAfter += new VisibleAfterHandler(this.Form_VisibleAfter);

        }

        private void OnCustomInitialize()
        {
        }

        private SAPbouiCOM.StaticText StaticText0;
        private SAPbouiCOM.EditText EditText0;
        private SAPbouiCOM.StaticText StaticText1;
        private SAPbouiCOM.EditText EditText1;
        private SAPbouiCOM.StaticText StaticText2;
        private SAPbouiCOM.EditText EditText2;
        private SAPbouiCOM.Button Button0;

        public void HendldeSettings()
        {
            FillModelFromForm();
            FillFormFromModel();
            _Model.AddOrUpdate();
        }

        private void FillFormFromModel()
        {
            Application.SBO_Application.Forms.ActiveForm.Freeze(true);
            ((EditText)UIAPIRawForm.Items.Item("Item_1").Specific).Value = _Model.WorkingPriceList.ToString(CultureInfo.InvariantCulture);
            ((EditText)UIAPIRawForm.Items.Item("Item_3").Specific).Value = _Model.RetailPriceList.ToString(CultureInfo.InvariantCulture);
            ((EditText)UIAPIRawForm.Items.Item("Item_5").Specific).Value = _Model.HumanResourceCoefficient.ToString(CultureInfo.InvariantCulture);
            ((EditText)UIAPIRawForm.Items.Item("Item_8").Specific).Value = _Model.DailyNormPerPerson.ToString(CultureInfo.InvariantCulture);
            ((EditText)UIAPIRawForm.Items.Item("Item_9").Specific).Value = _Model.MtrlExcelIndex.ToString(CultureInfo.InvariantCulture);
            ((EditText)UIAPIRawForm.Items.Item("Item_11").Specific).Value = _Model.ResourceExcelIndex.ToString(CultureInfo.InvariantCulture);
            Application.SBO_Application.Forms.ActiveForm.Freeze(false);
        }

        private void FillModelFromForm()
        {
            _Model.WorkingPriceList = EditText0.Value;
            _Model.RetailPriceList = EditText1.Value;
            _Model.HumanResourceCoefficient = double.Parse(EditText2.Value, CultureInfo.InvariantCulture);
            _Model.DailyNormPerPerson = double.Parse(EditText3.Value, CultureInfo.InvariantCulture);
            _Model.MtrlExcelIndex = int.Parse(string.IsNullOrWhiteSpace(EditText4.Value) ? "0" : EditText4.Value, CultureInfo.InvariantCulture);
            _Model.ResourceExcelIndex = int.Parse(string.IsNullOrWhiteSpace(EditText5.Value) ? "0" : EditText5.Value, CultureInfo.InvariantCulture);
            Application.SBO_Application.StatusBar.SetSystemMessage("Success",BoMessageTime.bmt_Short,BoStatusBarMessageType.smt_Success);
        }

        private void Button0_PressedAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            HendldeSettings();
            DiManager.GetSettings();
        }

        private SAPbouiCOM.StaticText StaticText3;
        private EditText EditText3;

        private void Form_VisibleAfter(SAPbouiCOM.SBOItemEventArg pVal)
        {
            string tittle = SAPbouiCOM.Framework.Application.SBO_Application.Forms.ActiveForm.Title;
            if (tittle == "Settings")
            {
                var fromDb = FillModelFromDb();
                if (fromDb)
                {
                    FillFormFromModel();
                }
            }
        }

        private bool FillModelFromDb()
        {
            Recordset recSet =
                (Recordset)DiManager.Company.GetBusinessObject(BoObjectTypes.BoRecordset);
            recSet.DoQuery($"SELECT * FROM [@RSM_BBA_SETTINGS]");
            if (recSet.EoF)
            {
                return false;
            }
            _Model.WorkingPriceList = recSet.Fields.Item("U_WorkingPriceList").Value.ToString();
            _Model.RetailPriceList = recSet.Fields.Item("U_RetailPriceList").Value.ToString();
            _Model.HumanResourceCoefficient = (double)recSet.Fields.Item("U_HumanResourceCoefficient").Value;
            _Model.DailyNormPerPerson = (double)recSet.Fields.Item("U_DailyNormPerPerson").Value;
            _Model.MtrlExcelIndex = (int)recSet.Fields.Item("U_MtrlExcelIndex").Value;
            _Model.ResourceExcelIndex = (int)recSet.Fields.Item("U_ResourceExcelIndex").Value;
            return true;
        }

        private EditText EditText4;
        private SAPbouiCOM.StaticText StaticText4;
        private EditText EditText5;
        private SAPbouiCOM.StaticText StaticText5;
    }
}
