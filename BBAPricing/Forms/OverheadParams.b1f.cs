using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BBAPricing.FormControllers;
using BBAPricing.Models;
using SAPbobsCOM;
using SAPbouiCOM;
using SAPbouiCOM.Framework;

namespace BBAPricing.Forms
{
    [FormAttribute("BBAPricing.Forms.OverheadParams", "Forms/OverheadParams.b1f")]
    class OverheadParams : UserFormBase
    {
        public OverheadParams()
        {
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
            this.EditText2.ValidateAfter += new SAPbouiCOM._IEditTextEvents_ValidateAfterEventHandler(this.EditText2_ValidateAfter);
            this.StaticText3 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_6").Specific));
            this.EditText3 = ((SAPbouiCOM.EditText)(this.GetItem("Item_7").Specific));
            this.EditText3.ValidateAfter += new SAPbouiCOM._IEditTextEvents_ValidateAfterEventHandler(this.EditText3_ValidateAfter);
            this.StaticText4 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_8").Specific));
            this.EditText4 = ((SAPbouiCOM.EditText)(this.GetItem("Item_9").Specific));
            this.EditText4.ValidateAfter += new SAPbouiCOM._IEditTextEvents_ValidateAfterEventHandler(this.EditText4_ValidateAfter);
            this.StaticText5 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_10").Specific));
            this.EditText5 = ((SAPbouiCOM.EditText)(this.GetItem("Item_11").Specific));
            this.StaticText6 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_12").Specific));
            this.EditText6 = ((SAPbouiCOM.EditText)(this.GetItem("Item_13").Specific));
            this.StaticText7 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_14").Specific));
            this.EditText7 = ((SAPbouiCOM.EditText)(this.GetItem("Item_15").Specific));
            this.StaticText8 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_16").Specific));
            this.StaticText9 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_17").Specific));
            this.Button0 = ((SAPbouiCOM.Button)(this.GetItem("Item_18").Specific));
            this.Button0.PressedAfter += new SAPbouiCOM._IButtonEvents_PressedAfterEventHandler(this.Button0_PressedAfter);
            this.StaticText10 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_19").Specific));
            this.EditText8 = ((SAPbouiCOM.EditText)(this.GetItem("Item_20").Specific));
            this.StaticText11 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_21").Specific));
            this.StaticText12 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_24").Specific));
            this.StaticText13 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_26").Specific));
            this.StaticText14 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_27").Specific));
            this.StaticText15 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_28").Specific));
            this.EditText12 = ((SAPbouiCOM.EditText)(this.GetItem("Item_29").Specific));
            this.EditText13 = ((SAPbouiCOM.EditText)(this.GetItem("Item_30").Specific));
            this.EditText14 = ((SAPbouiCOM.EditText)(this.GetItem("Item_31").Specific));
            this.StaticText16 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_22").Specific));
            this.EditText9 = ((SAPbouiCOM.EditText)(this.GetItem("Item_23").Specific));
            this.OnCustomInitialize();

        }

        /// <summary>
        /// Initialize form event. Called by framework before form creation.
        /// </summary>
        public override void OnInitializeFormEvents()
        {
            this.VisibleAfter += new VisibleAfterHandler(this.Form_VisibleAfter);

        }

        private SAPbouiCOM.StaticText StaticText0;
        private DBDataSource _dataSource;
        private OverheadParamController OverheadParamController;
        private OverheadsController OverheadsController;
        private void OnCustomInitialize()
        {
            OverheadParamController = new OverheadParamController(UIAPIRawForm);
            UIAPIRawForm.EnableMenu("1288", true);
            UIAPIRawForm.EnableMenu("1289", true);
            UIAPIRawForm.EnableMenu("1290", true);
            UIAPIRawForm.EnableMenu("1291", true);
        }

        private SAPbouiCOM.EditText EditText0;
        private SAPbouiCOM.StaticText StaticText1;
        private SAPbouiCOM.EditText EditText1;
        private SAPbouiCOM.StaticText StaticText2;
        private SAPbouiCOM.EditText EditText2;
        private SAPbouiCOM.StaticText StaticText3;
        private SAPbouiCOM.EditText EditText3;
        private SAPbouiCOM.StaticText StaticText4;
        private SAPbouiCOM.EditText EditText4;
        private SAPbouiCOM.StaticText StaticText5;
        private SAPbouiCOM.EditText EditText5;
        private SAPbouiCOM.StaticText StaticText6;
        private SAPbouiCOM.EditText EditText6;
        private SAPbouiCOM.StaticText StaticText7;
        private SAPbouiCOM.EditText EditText7;
        private SAPbouiCOM.StaticText StaticText8;
        private SAPbouiCOM.StaticText StaticText9;
        private SAPbouiCOM.Button Button0;

        private void Button0_PressedAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            OverheadParamController.HendldeSettings();
            OverheadParamController.Refresh();
            OverheadsController = new OverheadsController(UIAPIRawForm,
                OverheadParamController.FormModel);
            OverheadsController.CalculateOverheads();
        }

        private void EditText2_ValidateAfter(object sboObject, SBOItemEventArg pVal)
        {
            OverheadParamController.Refresh();
        }

        private void EditText4_ValidateAfter(object sboObject, SBOItemEventArg pVal)
        {
            OverheadParamController.Refresh();

        }

        private void EditText3_ValidateAfter(object sboObject, SBOItemEventArg pVal)
        {
            OverheadParamController.Refresh();
        }

        private void Form_VisibleAfter(SBOItemEventArg pVal)
        {
            string tittle = SAPbouiCOM.Framework.Application.SBO_Application.Forms.ActiveForm.Title;
            if (tittle == "Overhead Params")
            {
                UIAPIRawForm.DataSources.DBDataSources.Item(0).Query();
                if (UIAPIRawForm.DataSources.DBDataSources.Item(0).Size != 0)
                {
                    UIAPIRawForm.DataSources.DBDataSources.Item(0).Offset = UIAPIRawForm.DataSources.DBDataSources.Item(0).Size - 1;
                }
                else
                {
                    UIAPIRawForm.DataSources.DBDataSources.Item(0).InsertRecord(0);
                }
            }
        }

        private StaticText StaticText10;
        private EditText EditText8;
        private StaticText StaticText11;
        private StaticText StaticText12;
        private StaticText StaticText13;
        private StaticText StaticText14;
        private StaticText StaticText15;
        private EditText EditText12;
        private EditText EditText13;
        private EditText EditText14;
        private StaticText StaticText16;
        private EditText EditText9;
    }
}
