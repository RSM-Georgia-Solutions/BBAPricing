using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BBAPricing.FormControllers;
using BBAPricing.Models;
using SAPbobsCOM;
using SAPbouiCOM.Framework;

namespace BBAPricing.Forms
{
    [FormAttribute("BBAPricing.Forms.CommonElements", "Forms/CommonElements.b1f")]
    class CommonElements : UserFormBase
    {
        private readonly List<MasterBomModel> MasterBomModel;
        private CommonElementsController CommonElementsController;

        public CommonElements(List<MasterBomModel> masterBomModel)
        {
            MasterBomModel = masterBomModel;
            CommonElementsController = new CommonElementsController(UIAPIRawForm, MasterBomModel);
        }

        /// <summary>
        /// Initialize components. Called by framework after form created.
        /// </summary>
        public override void OnInitializeComponent()
        {
            this.StaticText0 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_0").Specific));
            this.EditText0 = ((SAPbouiCOM.EditText)(this.GetItem("Item_1").Specific));
            this.StaticText1 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_2").Specific));
            this.StaticText2 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_3").Specific));
            this.StaticText3 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_4").Specific));
            this.StaticText4 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_5").Specific));
            this.StaticText5 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_6").Specific));
            this.StaticText6 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_7").Specific));
            this.StaticText7 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_8").Specific));
            this.EditText2 = ((SAPbouiCOM.EditText)(this.GetItem("Item_11").Specific));
            this.EditText2.ValidateAfter += new SAPbouiCOM._IEditTextEvents_ValidateAfterEventHandler(this.EditText2_ValidateAfter);
            this.EditText3 = ((SAPbouiCOM.EditText)(this.GetItem("Item_12").Specific));
            this.EditText3.ValidateAfter += new SAPbouiCOM._IEditTextEvents_ValidateAfterEventHandler(this.EditText3_ValidateAfter);
            this.EditText4 = ((SAPbouiCOM.EditText)(this.GetItem("Item_13").Specific));
            this.EditText4.ValidateAfter += new SAPbouiCOM._IEditTextEvents_ValidateAfterEventHandler(this.EditText4_ValidateAfter);
            this.EditText5 = ((SAPbouiCOM.EditText)(this.GetItem("Item_14").Specific));
            this.EditText5.ValidateAfter += new SAPbouiCOM._IEditTextEvents_ValidateAfterEventHandler(this.EditText5_ValidateAfter);
            this.EditText6 = ((SAPbouiCOM.EditText)(this.GetItem("Item_15").Specific));
            this.EditText7 = ((SAPbouiCOM.EditText)(this.GetItem("Item_16").Specific));
            this.Button0 = ((SAPbouiCOM.Button)(this.GetItem("Item_18").Specific));
            this.Button0.PressedAfter += new SAPbouiCOM._IButtonEvents_PressedAfterEventHandler(this.Button0_PressedAfter);
            this.StaticText9 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_19").Specific));
            this.StaticText10 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_21").Specific));
            this.StaticText11 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_22").Specific));
            this.StaticText12 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_23").Specific));
            this.StaticText13 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_24").Specific));
            this.StaticText14 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_25").Specific));
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

        private void OnCustomInitialize()
        {
            GetItem("Item_15").Enabled = false;
            GetItem("Item_16").Enabled = false;
        }

        private SAPbouiCOM.EditText EditText0;
        private SAPbouiCOM.StaticText StaticText1;
        private SAPbouiCOM.StaticText StaticText2;
        private SAPbouiCOM.StaticText StaticText3;
        private SAPbouiCOM.StaticText StaticText4;
        private SAPbouiCOM.StaticText StaticText5;
        private SAPbouiCOM.StaticText StaticText6;
        private SAPbouiCOM.StaticText StaticText7;
        private SAPbouiCOM.EditText EditText2;
        private SAPbouiCOM.EditText EditText3;
        private SAPbouiCOM.EditText EditText4;
        private SAPbouiCOM.EditText EditText5;
        private SAPbouiCOM.EditText EditText6;
        private SAPbouiCOM.EditText EditText7;
        private SAPbouiCOM.Button Button0;
        private SAPbouiCOM.StaticText StaticText9;
        private SAPbouiCOM.StaticText StaticText10;
        private SAPbouiCOM.StaticText StaticText11;
        private SAPbouiCOM.StaticText StaticText12;
        private SAPbouiCOM.StaticText StaticText13;
        private SAPbouiCOM.StaticText StaticText14;

        private void Form_VisibleAfter(SAPbouiCOM.SBOItemEventArg pVal)
        {
            string tittle = SAPbouiCOM.Framework.Application.SBO_Application.Forms.ActiveForm.Title;
            if (tittle == "Common Elements")
            {
                var fromDb = CommonElementsController.FillModelFromDb();
                if (!fromDb)
                {
                    CommonElementsController.GenerateModel();
                }
                CommonElementsController.FillFormFromModel();
            }
        }

        public static Action RefreshBom;
        private void Button0_PressedAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            CommonElementsController.HandldeSettings();
            CommonElementsController.Refresh();
            CommonElementsController.UpdateMbom();
            try
            {
                RefreshBom.Invoke();
            }
            catch (Exception)
            {
                //Form is Closed
            }
        }

        private void EditText2_ValidateAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            CommonElementsController.Refresh();
        }

        private void EditText3_ValidateAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            CommonElementsController.Refresh();
        }

        private void EditText4_ValidateAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            CommonElementsController.Refresh();

        }

        private void EditText5_ValidateAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            CommonElementsController.Refresh();
        }


    }
}
