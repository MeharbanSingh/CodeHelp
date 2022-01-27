// **************************************************
// Custom code for UD10Form
// Created: 25/08/2020 11:34:30 AM
// **************************************************
using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Generic;
using Erp.Adapters;
using Ice.BO;
using Ice.Core;
using Ice.UI;
using Ice.Lib;
using Ice.Adapters;
using Ice.Lib.Customization;
using Ice.Lib.ExtendedProps;
using Ice.Lib.Framework;
using Ice.Lib.Searches;
using Ice.UI.FormFunctions;
using System.Reflection;
using System.Linq;
using Infragistics.Win.UltraWinGrid;
using System.Globalization;
using Ice;

public class Script
{

	private Ice.Lib.Customization.CustomScriptManager csm;

	private Ice.UI.App.UD10Entry.Transaction oTrans;

	private Ice.UI.App.UD10Entry.UD10Form UD10Form;

	private Infragistics.Win.UltraWinToolbars.UltraToolbarsManager baseToolbarsManager;

	private System.Data.DataTable UD10Attch_Column;

	private Ice.Lib.Framework.EpiDataView AutoAttachUD10_Row;

	private System.Data.DataTable BpmData_Column;

	private Ice.Lib.Framework.EpiDataView CallContextBpmData_Row;

	private System.Data.DataTable Client_Column;

	private Ice.Lib.Framework.EpiDataView CallContextClientData_Row;

	private System.Data.DataTable UD10_Column;

	private Ice.Lib.Framework.EpiDataView UD10_Row;

	private Ice.Lib.Framework.EpiBasePanel pbsReverseCR;

	private Ice.Lib.Framework.EpiBasePanel pbsReverseCR_9da9d36f_74c7_45f2_b92e_c783bcc96321;

	private Ice.Lib.Framework.EpiBasePanel pbsReallocCR;

	private Ice.Lib.Framework.EpiBasePanel pbsReallocCR_09dab083_b2d2_44b1_ba2b_d3b997297334;

	private Ice.Lib.Framework.EpiLabel lblBN;

	private Ice.Lib.Framework.EpiLabel lblBN_152b9a77_59c7_4684_ba74_bbfb4b5d56fb;

	private Ice.Lib.Framework.EpiLabel lblED;

	private Ice.Lib.Framework.EpiLabel lblED_ba69c022_527c_40a0_a1de_493aafdf6dc1;

	private Ice.Lib.Framework.EpiLabel lblRef;

	private Ice.Lib.Framework.EpiLabel lblRef_5421c36f_623f_4370_95b9_1442aa7868ff;

	private Ice.Lib.Framework.EpiLabel lblTT;

	private Ice.Lib.Framework.EpiLabel lblTT_fd55eec6_18d5_47c3_bd39_3f21bb59fb23;

	private Ice.Lib.Framework.EpiTextBox txtBN;

	private Ice.Lib.Framework.EpiTextBox txtBN_f4f8d9e7_eb4e_48ee_a1e5_83e65407c685;

	private Ice.Lib.Framework.EpiDateTimeEditor dteED;

	private Ice.Lib.Framework.EpiDateTimeEditor dteED_6648c36d_7a31_4827_8edb_639d958f6351;

	private Ice.Lib.Framework.EpiTextBox txtRef;

	private Ice.Lib.Framework.EpiTextBox txtRef_9c52bcd0_6c51_497a_9902_3bdf75c4b902;

	private Ice.Lib.Framework.EpiCombo cTT;

	private Ice.Lib.Framework.EpiCombo cTT_0cd953be_5bd7_4d73_9018_be388739cd55;

	private Ice.Lib.Framework.EpiButton btnNewBatch;

	private Ice.Lib.Framework.EpiButton btnNewBatch_d72e5089_3b70_48dd_becd_6a4b79a41899;

	private Ice.Lib.Framework.EpiGroupBox gbReverseCR;

	private Ice.Lib.Framework.EpiGroupBox gbReverseCR_a1dce6eb_d86b_49c8_a4c2_9896f3e44f7b;

	private Ice.Lib.Framework.EpiLabel lblCustIDRCR;

	private Ice.Lib.Framework.EpiLabel lblCustIDRCR_82675383_5fe2_4399_bf8f_1ad6ab2da074;

	private Ice.Lib.Framework.EpiLabel lblCustNameRCR;

	private Ice.Lib.Framework.EpiLabel lblCustNameRCR_754aa08a_bf26_44b0_bcc9_85499189b473;

	private Ice.Lib.Framework.EpiLabel lblPaymentDateRCR;

	private Ice.Lib.Framework.EpiLabel lblPaymentDateRCR_4fc5f62e_fde1_4ecb_a5f5_c550c165016f;

	private Ice.Lib.Framework.EpiTextBox txtCustIDReverseCR;

	private Ice.Lib.Framework.EpiTextBox txtCustIDReverseCR_7bb56d18_3751_4158_88f5_b14a2e11ff02;

	private Ice.Lib.Framework.EpiTextBox txtCustNameReverseCR;

	private Ice.Lib.Framework.EpiTextBox txtCustNameReverseCR_b54ce727_6582_44a9_8a51_3935ed0f77b0;

	private Ice.Lib.Framework.EpiDateTimeEditor dtePaymentDate;

	private Ice.Lib.Framework.EpiDateTimeEditor dtePaymentDate_35e32fb2_4e4c_4d03_89da_5cfdc33daee9;

	private Ice.Lib.Framework.EpiButton btnSearchCustomerReverseCR;

	private Ice.Lib.Framework.EpiButton btnSearchCustomerReverseCR_1191f761_cce0_43a3_b1bc_c8739fef974d;

	private Ice.Lib.Framework.EpiUltraGrid ugPaymentGridReverseCR;

	private Ice.Lib.Framework.EpiUltraGrid ugPaymentGridReverseCR_02e4071d_31e5_4af3_9283_a1a6a3d19f14;

	private Ice.Lib.Framework.EpiButton btnSubmitReverseCR;

	private Ice.Lib.Framework.EpiButton btnSubmitReverseCR_3b004280_f70a_42c0_bc2c_3acffda4dd5f;

	private Ice.Lib.Framework.EpiButton btnSearchPayment;

	private Ice.Lib.Framework.EpiButton btnSearchPayment_53d59437_75c7_4a34_b5cf_40ac54170b60;

	private Ice.Lib.Framework.EpiGroupBox gbNegativeCE;

	private Ice.Lib.Framework.EpiGroupBox gbNegativeCE_39dca234_6254_41fe_90ef_e66c2aa78692;

	private Ice.Lib.Framework.EpiGroupBox gbPCE;

	private Ice.Lib.Framework.EpiGroupBox gbPCE_e98f4e59_ae1f_4db8_98de_6c8fe4be76af;

	private Ice.Lib.Framework.EpiLabel lblCustIDNCE;

	private Ice.Lib.Framework.EpiLabel lblCustIDNCE_613f351d_d546_404b_a5d5_eed09f8c128c;

	private Ice.Lib.Framework.EpiLabel lblCustNameNCE;

	private Ice.Lib.Framework.EpiLabel lblCustNameNCE_819e4258_c9fc_4373_80c8_6fc9cdea830f;

	private Ice.Lib.Framework.EpiLabel lblAdjAmtNCE;

	private Ice.Lib.Framework.EpiLabel lblAdjAmtNCE_7d2db45d_8006_4651_bc50_c999c4342924;

	private Ice.Lib.Framework.EpiTextBox txtCustIDNCE;

	private Ice.Lib.Framework.EpiTextBox txtCustIDNCE_f2f928f2_c5aa_4fb3_9b05_d5535f5f1d70;

	private Ice.Lib.Framework.EpiTextBox txtCustNameNCE;

	private Ice.Lib.Framework.EpiTextBox txtCustNameNCE_301cc56a_9bf0_4faa_92b6_d4f3ac4d2e22;

	private Ice.Lib.Framework.EpiNumericEditor neAdjAmtNCE;

	private Ice.Lib.Framework.EpiNumericEditor neAdjAmtNCE_d0f365f3_9a1b_448c_8d65_4f6ced2b6a2f;

	private Ice.Lib.Framework.EpiButton btnSearchCustomerNCE;

	private Ice.Lib.Framework.EpiButton btnSearchCustomerNCE_f6ee7c62_ac0a_4fe6_828c_62ca14f93c2b;

	private Ice.Lib.Framework.EpiTextBox txtCustIDPCE;

	private Ice.Lib.Framework.EpiTextBox txtCustIDPCE_8d7a65aa_4a92_4999_8f2f_0dd48df57b7c;

	private Ice.Lib.Framework.EpiTextBox txtCustNamePCE;

	private Ice.Lib.Framework.EpiTextBox txtCustNamePCE_75ccecd0_e064_4ea7_bc05_37a450618222;

	private Ice.Lib.Framework.EpiNumericEditor neAdjAmtPCE;

	private Ice.Lib.Framework.EpiNumericEditor neAdjAmtPCE_dea99851_dd8c_475b_800a_6caf2b140822;

	private Ice.Lib.Framework.EpiLabel lblCustIDPCE;

	private Ice.Lib.Framework.EpiLabel lblCustIDPCE_2c6f1664_c367_4f6a_a614_585951865695;

	private Ice.Lib.Framework.EpiLabel lblCustNamePCE;

	private Ice.Lib.Framework.EpiLabel lblCustNamePCE_ef43efe8_2106_422c_8301_a57b990adf70;

	private Ice.Lib.Framework.EpiLabel lblAdjAmtPCE;

	private Ice.Lib.Framework.EpiLabel lblAdjAmtPCE_bf6f6b58_cb7c_4b17_91e7_ef7c39a91aab;

	private Ice.Lib.Framework.EpiButton btnSearchCustomerPCE;

	private Ice.Lib.Framework.EpiButton btnSearchCustomerPCE_a3e2e420_0666_4f8e_88e8_5211570ecda1;

	private Ice.Lib.Framework.EpiButton btnSubmitReallocCR;

	private Ice.Lib.Framework.EpiButton btnSubmitReallocCR_06f44aed_d872_45d7_9229_b8d18d32c9b6;

	private Ice.Lib.Framework.EpiButton btnClear;

	private Ice.Lib.Framework.EpiButton btnClear_4f3238d3_f83c_4da3_86c0_83e08a28cbef;

	private Ice.Lib.Framework.EpiUltraGrid ugPaymentGridReallocateNCE;

	private Ice.Lib.Framework.EpiUltraGrid ugPaymentGridReallocateNCE_40bc58cd_c262_4daf_8d34_8ed1fbc81a14;

	private Ice.Lib.Framework.EpiButton btnSearchPaymentNCE;

	private Ice.Lib.Framework.EpiButton btnSearchPaymentNCE_92c74efd_72f9_43e8_965b_424ab7a958e6;

	private Ice.Lib.Framework.EpiLabel lblPaymentDate;

	private Ice.Lib.Framework.EpiLabel lblPaymentDate_e43d86f2_7f9e_49f1_ba7b_933a971ac3a3;

	private Ice.Lib.Framework.EpiDateTimeEditor epiDateTimeEditorPaymentDate;

	private Ice.Lib.Framework.EpiDateTimeEditor epiDateTimeEditorPaymentDate_938a6f03_ba18_4af9_b331_3ad0e810b5af;

	private Ice.Lib.Framework.EpiLabel lblRefInvoice;

	private Ice.Lib.Framework.EpiLabel lblRefInvoice_9ce4f5c4_6cab_4c23_8a2b_e04b140c1d81;

	private Ice.Lib.Framework.EpiTextBox txtRefInvc;

	private Ice.Lib.Framework.EpiTextBox txtRefInvc_64289281_a669_492d_822f_70661796133e;

	private Ice.Lib.Framework.EpiBasePanel pbsRefunds;

	private Ice.Lib.Framework.EpiBasePanel pbsRefunds_c5d96db4_cac2_4061_ad43_ce19ee7900b7;

	private Ice.Lib.Framework.EpiGroupBox grbBxRefunds;

	private Ice.Lib.Framework.EpiGroupBox grbBxRefunds_8b058e9d_6531_4621_8e3b_118a64130c3e;

	private Ice.Lib.Framework.EpiButton btnRfCust;

	private Ice.Lib.Framework.EpiButton btnRfCust_86fb4f18_164e_4704_8153_66faab8f9488;

	private Ice.Lib.Framework.EpiTextBox txtbxRfCust;

	private Ice.Lib.Framework.EpiTextBox txtbxRfCust_395757f7_7fde_43c3_82e9_4dedec73ab7b;

	private Ice.Lib.Framework.EpiLabel lblRfCustName;

	private Ice.Lib.Framework.EpiLabel lblRfCustName_1c4652dd_45f4_4441_a8ce_964dd2afff98;

	private Ice.Lib.Framework.EpiLabel lblRfDate;

	private Ice.Lib.Framework.EpiLabel lblRfDate_ff73df12_cca8_4069_8f25_f24735f22493;

	private Ice.Lib.Framework.EpiTextBox txtbxRfCustName;

	private Ice.Lib.Framework.EpiTextBox txtbxRfCustName_d8e70d8e_4cfe_4a50_8dc1_12a00703e782;

	private Ice.Lib.Framework.EpiLabel lblRfAmnt;

	private Ice.Lib.Framework.EpiLabel lblRfAmnt_9ffa4e89_8d30_4aef_987a_3444e54b76cf;

	private Ice.Lib.Framework.EpiLabel lblRfSelcAmnt;

	private Ice.Lib.Framework.EpiLabel lblRfSelcAmnt_cac2ca59_a5f2_4b20_a2ba_8d16bcdeeb13;

	private Ice.Lib.Framework.EpiButton btnRfSCrdNte;

	private Ice.Lib.Framework.EpiButton btnRfSCrdNte_efe8b6c8_7583_49e3_9e62_3675aa83c770;

	private Ice.Lib.Framework.EpiButton btnRfCancel;

	private Ice.Lib.Framework.EpiButton btnRfCancel_8a06374d_811d_4952_ba68_44ce6f410561;

	private Ice.Lib.Framework.EpiButton btnRfSubmit;

	private Ice.Lib.Framework.EpiButton btnRfSubmit_f5cc15f3_635a_4030_acd4_73af478a9fab;

	private Ice.Lib.Framework.EpiUltraGrid grdRefunds;

	private Ice.Lib.Framework.EpiUltraGrid grdRefunds_e256e64a_1a1f_48ce_a50c_0030aa0bd628;

	private Ice.Lib.Framework.EpiDateTimeEditor epiDateTimeEditorRfDate;

	private Ice.Lib.Framework.EpiDateTimeEditor epiDateTimeEditorRfDate_04bc487b_339c_4769_91db_e3da38b497f8;

	private Ice.Lib.Framework.EpiCurrencyEditor epiCurrencyEditorRfAmount;

	private Ice.Lib.Framework.EpiCurrencyEditor epiCurrencyEditorRfAmount_deaf2310_5bdd_48ac_8cc5_caf48ab49f80;

	private Ice.Lib.Framework.EpiCurrencyEditor epiCurrencyEditorRfSelcAmnt;

	private Ice.Lib.Framework.EpiCurrencyEditor epiCurrencyEditorRfSelcAmnt_bdca2b3b_8051_4014_8176_d119155e8876;

	public void InitializeGlobalVariables(Ice.Lib.Customization.CustomScriptManager csm)
	{
		this.csm = csm;
		this.oTrans = ((Ice.UI.App.UD10Entry.Transaction)(this.csm.GetGlobalInstance("oTrans")));
		this.UD10Form = ((Ice.UI.App.UD10Entry.UD10Form)(this.csm.GetGlobalInstance("UD10Form")));
		this.baseToolbarsManager = ((Infragistics.Win.UltraWinToolbars.UltraToolbarsManager)(this.csm.GetGlobalInstance("baseToolbarsManager")));
		this.UD10Attch_Column = ((System.Data.DataTable)(this.csm.GetGlobalInstance("UD10Attch_Column")));
		this.AutoAttachUD10_Row = ((Ice.Lib.Framework.EpiDataView)(this.csm.GetGlobalInstance("AutoAttachUD10_Row")));
		this.BpmData_Column = ((System.Data.DataTable)(this.csm.GetGlobalInstance("BpmData_Column")));
		this.CallContextBpmData_Row = ((Ice.Lib.Framework.EpiDataView)(this.csm.GetGlobalInstance("CallContextBpmData_Row")));
		this.Client_Column = ((System.Data.DataTable)(this.csm.GetGlobalInstance("Client_Column")));
		this.CallContextClientData_Row = ((Ice.Lib.Framework.EpiDataView)(this.csm.GetGlobalInstance("CallContextClientData_Row")));
		this.UD10_Column = ((System.Data.DataTable)(this.csm.GetGlobalInstance("UD10_Column")));
		this.UD10_Row = ((Ice.Lib.Framework.EpiDataView)(this.csm.GetGlobalInstance("UD10_Row")));
		Ice.Lib.Customization.PersonalizeCustomizeManager personalizeCustomizeManager = this.csm.PersonalizeCustomizeManager;
		System.Windows.Forms.Control topControl = personalizeCustomizeManager.TopControl;
		topControl.FindForm().SuspendLayout();
		// Creating custom targets.
		this.pbsReverseCR = new Ice.Lib.Framework.EpiBasePanel();
		this.pbsReverseCR_9da9d36f_74c7_45f2_b92e_c783bcc96321 = this.pbsReverseCR;
		System.Collections.Hashtable customControls = personalizeCustomizeManager.CustControlMan.CustomControlsHT;
		customControls.Add("9da9d36f-74c7-45f2-b92e-c783bcc96321", this.pbsReverseCR);
		System.Collections.Hashtable controlsHT = personalizeCustomizeManager.ControlsHT;
		controlsHT.Add("9da9d36f-74c7-45f2-b92e-c783bcc96321", this.pbsReverseCR);
		this.pbsReverseCR.Name = "pbsReverseCR";
		this.pbsReverseCR.EpiGuid = "9da9d36f-74c7-45f2-b92e-c783bcc96321";
		this.pbsReallocCR = new Ice.Lib.Framework.EpiBasePanel();
		this.pbsReallocCR_09dab083_b2d2_44b1_ba2b_d3b997297334 = this.pbsReallocCR;
		customControls.Add("09dab083-b2d2-44b1-ba2b-d3b997297334", this.pbsReallocCR);
		controlsHT.Add("09dab083-b2d2-44b1-ba2b-d3b997297334", this.pbsReallocCR);
		this.pbsReallocCR.Name = "pbsReallocCR";
		this.pbsReallocCR.EpiGuid = "09dab083-b2d2-44b1-ba2b-d3b997297334";
		this.lblBN = new Ice.Lib.Framework.EpiLabel();
		this.lblBN_152b9a77_59c7_4684_ba74_bbfb4b5d56fb = this.lblBN;
		customControls.Add("152b9a77-59c7-4684-ba74-bbfb4b5d56fb", this.lblBN);
		controlsHT.Add("152b9a77-59c7-4684-ba74-bbfb4b5d56fb", this.lblBN);
		this.lblBN.Name = "lblBN";
		this.lblBN.EpiGuid = "152b9a77-59c7-4684-ba74-bbfb4b5d56fb";
		this.lblED = new Ice.Lib.Framework.EpiLabel();
		this.lblED_ba69c022_527c_40a0_a1de_493aafdf6dc1 = this.lblED;
		customControls.Add("ba69c022-527c-40a0-a1de-493aafdf6dc1", this.lblED);
		controlsHT.Add("ba69c022-527c-40a0-a1de-493aafdf6dc1", this.lblED);
		this.lblED.Name = "lblED";
		this.lblED.EpiGuid = "ba69c022-527c-40a0-a1de-493aafdf6dc1";
		this.lblRef = new Ice.Lib.Framework.EpiLabel();
		this.lblRef_5421c36f_623f_4370_95b9_1442aa7868ff = this.lblRef;
		customControls.Add("5421c36f-623f-4370-95b9-1442aa7868ff", this.lblRef);
		controlsHT.Add("5421c36f-623f-4370-95b9-1442aa7868ff", this.lblRef);
		this.lblRef.Name = "lblRef";
		this.lblRef.EpiGuid = "5421c36f-623f-4370-95b9-1442aa7868ff";
		this.lblTT = new Ice.Lib.Framework.EpiLabel();
		this.lblTT_fd55eec6_18d5_47c3_bd39_3f21bb59fb23 = this.lblTT;
		customControls.Add("fd55eec6-18d5-47c3-bd39-3f21bb59fb23", this.lblTT);
		controlsHT.Add("fd55eec6-18d5-47c3-bd39-3f21bb59fb23", this.lblTT);
		this.lblTT.Name = "lblTT";
		this.lblTT.EpiGuid = "fd55eec6-18d5-47c3-bd39-3f21bb59fb23";
		this.txtBN = new Ice.Lib.Framework.EpiTextBox();
		this.txtBN_f4f8d9e7_eb4e_48ee_a1e5_83e65407c685 = this.txtBN;
		customControls.Add("f4f8d9e7-eb4e-48ee-a1e5-83e65407c685", this.txtBN);
		controlsHT.Add("f4f8d9e7-eb4e-48ee-a1e5-83e65407c685", this.txtBN);
		this.txtBN.Name = "txtBN";
		this.txtBN.EpiGuid = "f4f8d9e7-eb4e-48ee-a1e5-83e65407c685";
		this.dteED = new Ice.Lib.Framework.EpiDateTimeEditor();
		this.dteED_6648c36d_7a31_4827_8edb_639d958f6351 = this.dteED;
		customControls.Add("6648c36d-7a31-4827-8edb-639d958f6351", this.dteED);
		controlsHT.Add("6648c36d-7a31-4827-8edb-639d958f6351", this.dteED);
		this.dteED.Name = "dteED";
		this.dteED.EpiGuid = "6648c36d-7a31-4827-8edb-639d958f6351";
		this.txtRef = new Ice.Lib.Framework.EpiTextBox();
		this.txtRef_9c52bcd0_6c51_497a_9902_3bdf75c4b902 = this.txtRef;
		customControls.Add("9c52bcd0-6c51-497a-9902-3bdf75c4b902", this.txtRef);
		controlsHT.Add("9c52bcd0-6c51-497a-9902-3bdf75c4b902", this.txtRef);
		this.txtRef.Name = "txtRef";
		this.txtRef.EpiGuid = "9c52bcd0-6c51-497a-9902-3bdf75c4b902";
		this.cTT = new Ice.Lib.Framework.EpiCombo();
		this.cTT_0cd953be_5bd7_4d73_9018_be388739cd55 = this.cTT;
		customControls.Add("0cd953be-5bd7-4d73-9018-be388739cd55", this.cTT);
		controlsHT.Add("0cd953be-5bd7-4d73-9018-be388739cd55", this.cTT);
		this.cTT.Name = "cTT";
		this.cTT.EpiGuid = "0cd953be-5bd7-4d73-9018-be388739cd55";
		this.btnNewBatch = new Ice.Lib.Framework.EpiButton();
		this.btnNewBatch_d72e5089_3b70_48dd_becd_6a4b79a41899 = this.btnNewBatch;
		customControls.Add("d72e5089-3b70-48dd-becd-6a4b79a41899", this.btnNewBatch);
		controlsHT.Add("d72e5089-3b70-48dd-becd-6a4b79a41899", this.btnNewBatch);
		this.btnNewBatch.Name = "btnNewBatch";
		this.btnNewBatch.EpiGuid = "d72e5089-3b70-48dd-becd-6a4b79a41899";
		this.gbReverseCR = new Ice.Lib.Framework.EpiGroupBox();
		this.gbReverseCR_a1dce6eb_d86b_49c8_a4c2_9896f3e44f7b = this.gbReverseCR;
		customControls.Add("a1dce6eb-d86b-49c8-a4c2-9896f3e44f7b", this.gbReverseCR);
		controlsHT.Add("a1dce6eb-d86b-49c8-a4c2-9896f3e44f7b", this.gbReverseCR);
		this.gbReverseCR.Name = "gbReverseCR";
		this.gbReverseCR.EpiGuid = "a1dce6eb-d86b-49c8-a4c2-9896f3e44f7b";
		this.lblCustIDRCR = new Ice.Lib.Framework.EpiLabel();
		this.lblCustIDRCR_82675383_5fe2_4399_bf8f_1ad6ab2da074 = this.lblCustIDRCR;
		customControls.Add("82675383-5fe2-4399-bf8f-1ad6ab2da074", this.lblCustIDRCR);
		controlsHT.Add("82675383-5fe2-4399-bf8f-1ad6ab2da074", this.lblCustIDRCR);
		this.lblCustIDRCR.Name = "lblCustIDRCR";
		this.lblCustIDRCR.EpiGuid = "82675383-5fe2-4399-bf8f-1ad6ab2da074";
		this.lblCustNameRCR = new Ice.Lib.Framework.EpiLabel();
		this.lblCustNameRCR_754aa08a_bf26_44b0_bcc9_85499189b473 = this.lblCustNameRCR;
		customControls.Add("754aa08a-bf26-44b0-bcc9-85499189b473", this.lblCustNameRCR);
		controlsHT.Add("754aa08a-bf26-44b0-bcc9-85499189b473", this.lblCustNameRCR);
		this.lblCustNameRCR.Name = "lblCustNameRCR";
		this.lblCustNameRCR.EpiGuid = "754aa08a-bf26-44b0-bcc9-85499189b473";
		this.lblPaymentDateRCR = new Ice.Lib.Framework.EpiLabel();
		this.lblPaymentDateRCR_4fc5f62e_fde1_4ecb_a5f5_c550c165016f = this.lblPaymentDateRCR;
		customControls.Add("4fc5f62e-fde1-4ecb-a5f5-c550c165016f", this.lblPaymentDateRCR);
		controlsHT.Add("4fc5f62e-fde1-4ecb-a5f5-c550c165016f", this.lblPaymentDateRCR);
		this.lblPaymentDateRCR.Name = "lblPaymentDateRCR";
		this.lblPaymentDateRCR.EpiGuid = "4fc5f62e-fde1-4ecb-a5f5-c550c165016f";
		this.txtCustIDReverseCR = new Ice.Lib.Framework.EpiTextBox();
		this.txtCustIDReverseCR_7bb56d18_3751_4158_88f5_b14a2e11ff02 = this.txtCustIDReverseCR;
		customControls.Add("7bb56d18-3751-4158-88f5-b14a2e11ff02", this.txtCustIDReverseCR);
		controlsHT.Add("7bb56d18-3751-4158-88f5-b14a2e11ff02", this.txtCustIDReverseCR);
		this.txtCustIDReverseCR.Name = "txtCustIDReverseCR";
		this.txtCustIDReverseCR.EpiGuid = "7bb56d18-3751-4158-88f5-b14a2e11ff02";
		this.txtCustNameReverseCR = new Ice.Lib.Framework.EpiTextBox();
		this.txtCustNameReverseCR_b54ce727_6582_44a9_8a51_3935ed0f77b0 = this.txtCustNameReverseCR;
		customControls.Add("b54ce727-6582-44a9-8a51-3935ed0f77b0", this.txtCustNameReverseCR);
		controlsHT.Add("b54ce727-6582-44a9-8a51-3935ed0f77b0", this.txtCustNameReverseCR);
		this.txtCustNameReverseCR.Name = "txtCustNameReverseCR";
		this.txtCustNameReverseCR.EpiGuid = "b54ce727-6582-44a9-8a51-3935ed0f77b0";
		this.dtePaymentDate = new Ice.Lib.Framework.EpiDateTimeEditor();
		this.dtePaymentDate_35e32fb2_4e4c_4d03_89da_5cfdc33daee9 = this.dtePaymentDate;
		customControls.Add("35e32fb2-4e4c-4d03-89da-5cfdc33daee9", this.dtePaymentDate);
		controlsHT.Add("35e32fb2-4e4c-4d03-89da-5cfdc33daee9", this.dtePaymentDate);
		this.dtePaymentDate.Name = "dtePaymentDate";
		this.dtePaymentDate.EpiGuid = "35e32fb2-4e4c-4d03-89da-5cfdc33daee9";
		this.btnSearchCustomerReverseCR = new Ice.Lib.Framework.EpiButton();
		this.btnSearchCustomerReverseCR_1191f761_cce0_43a3_b1bc_c8739fef974d = this.btnSearchCustomerReverseCR;
		customControls.Add("1191f761-cce0-43a3-b1bc-c8739fef974d", this.btnSearchCustomerReverseCR);
		controlsHT.Add("1191f761-cce0-43a3-b1bc-c8739fef974d", this.btnSearchCustomerReverseCR);
		this.btnSearchCustomerReverseCR.Name = "btnSearchCustomerReverseCR";
		this.btnSearchCustomerReverseCR.EpiGuid = "1191f761-cce0-43a3-b1bc-c8739fef974d";
		this.ugPaymentGridReverseCR = new Ice.Lib.Framework.EpiUltraGrid();
		this.ugPaymentGridReverseCR_02e4071d_31e5_4af3_9283_a1a6a3d19f14 = this.ugPaymentGridReverseCR;
		customControls.Add("02e4071d-31e5-4af3-9283-a1a6a3d19f14", this.ugPaymentGridReverseCR);
		controlsHT.Add("02e4071d-31e5-4af3-9283-a1a6a3d19f14", this.ugPaymentGridReverseCR);
		this.ugPaymentGridReverseCR.Name = "ugPaymentGridReverseCR";
		this.ugPaymentGridReverseCR.EpiGuid = "02e4071d-31e5-4af3-9283-a1a6a3d19f14";
		this.btnSubmitReverseCR = new Ice.Lib.Framework.EpiButton();
		this.btnSubmitReverseCR_3b004280_f70a_42c0_bc2c_3acffda4dd5f = this.btnSubmitReverseCR;
		customControls.Add("3b004280-f70a-42c0-bc2c-3acffda4dd5f", this.btnSubmitReverseCR);
		controlsHT.Add("3b004280-f70a-42c0-bc2c-3acffda4dd5f", this.btnSubmitReverseCR);
		this.btnSubmitReverseCR.Name = "btnSubmitReverseCR";
		this.btnSubmitReverseCR.EpiGuid = "3b004280-f70a-42c0-bc2c-3acffda4dd5f";
		this.btnSearchPayment = new Ice.Lib.Framework.EpiButton();
		this.btnSearchPayment_53d59437_75c7_4a34_b5cf_40ac54170b60 = this.btnSearchPayment;
		customControls.Add("53d59437-75c7-4a34-b5cf-40ac54170b60", this.btnSearchPayment);
		controlsHT.Add("53d59437-75c7-4a34-b5cf-40ac54170b60", this.btnSearchPayment);
		this.btnSearchPayment.Name = "btnSearchPayment";
		this.btnSearchPayment.EpiGuid = "53d59437-75c7-4a34-b5cf-40ac54170b60";
		this.gbNegativeCE = new Ice.Lib.Framework.EpiGroupBox();
		this.gbNegativeCE_39dca234_6254_41fe_90ef_e66c2aa78692 = this.gbNegativeCE;
		customControls.Add("39dca234-6254-41fe-90ef-e66c2aa78692", this.gbNegativeCE);
		controlsHT.Add("39dca234-6254-41fe-90ef-e66c2aa78692", this.gbNegativeCE);
		this.gbNegativeCE.Name = "gbNegativeCE";
		this.gbNegativeCE.EpiGuid = "39dca234-6254-41fe-90ef-e66c2aa78692";
		this.gbPCE = new Ice.Lib.Framework.EpiGroupBox();
		this.gbPCE_e98f4e59_ae1f_4db8_98de_6c8fe4be76af = this.gbPCE;
		customControls.Add("e98f4e59-ae1f-4db8-98de-6c8fe4be76af", this.gbPCE);
		controlsHT.Add("e98f4e59-ae1f-4db8-98de-6c8fe4be76af", this.gbPCE);
		this.gbPCE.Name = "gbPCE";
		this.gbPCE.EpiGuid = "e98f4e59-ae1f-4db8-98de-6c8fe4be76af";
		this.lblCustIDNCE = new Ice.Lib.Framework.EpiLabel();
		this.lblCustIDNCE_613f351d_d546_404b_a5d5_eed09f8c128c = this.lblCustIDNCE;
		customControls.Add("613f351d-d546-404b-a5d5-eed09f8c128c", this.lblCustIDNCE);
		controlsHT.Add("613f351d-d546-404b-a5d5-eed09f8c128c", this.lblCustIDNCE);
		this.lblCustIDNCE.Name = "lblCustIDNCE";
		this.lblCustIDNCE.EpiGuid = "613f351d-d546-404b-a5d5-eed09f8c128c";
		this.lblCustNameNCE = new Ice.Lib.Framework.EpiLabel();
		this.lblCustNameNCE_819e4258_c9fc_4373_80c8_6fc9cdea830f = this.lblCustNameNCE;
		customControls.Add("819e4258-c9fc-4373-80c8-6fc9cdea830f", this.lblCustNameNCE);
		controlsHT.Add("819e4258-c9fc-4373-80c8-6fc9cdea830f", this.lblCustNameNCE);
		this.lblCustNameNCE.Name = "lblCustNameNCE";
		this.lblCustNameNCE.EpiGuid = "819e4258-c9fc-4373-80c8-6fc9cdea830f";
		this.lblAdjAmtNCE = new Ice.Lib.Framework.EpiLabel();
		this.lblAdjAmtNCE_7d2db45d_8006_4651_bc50_c999c4342924 = this.lblAdjAmtNCE;
		customControls.Add("7d2db45d-8006-4651-bc50-c999c4342924", this.lblAdjAmtNCE);
		controlsHT.Add("7d2db45d-8006-4651-bc50-c999c4342924", this.lblAdjAmtNCE);
		this.lblAdjAmtNCE.Name = "lblAdjAmtNCE";
		this.lblAdjAmtNCE.EpiGuid = "7d2db45d-8006-4651-bc50-c999c4342924";
		this.txtCustIDNCE = new Ice.Lib.Framework.EpiTextBox();
		this.txtCustIDNCE_f2f928f2_c5aa_4fb3_9b05_d5535f5f1d70 = this.txtCustIDNCE;
		customControls.Add("f2f928f2-c5aa-4fb3-9b05-d5535f5f1d70", this.txtCustIDNCE);
		controlsHT.Add("f2f928f2-c5aa-4fb3-9b05-d5535f5f1d70", this.txtCustIDNCE);
		this.txtCustIDNCE.Name = "txtCustIDNCE";
		this.txtCustIDNCE.EpiGuid = "f2f928f2-c5aa-4fb3-9b05-d5535f5f1d70";
		this.txtCustNameNCE = new Ice.Lib.Framework.EpiTextBox();
		this.txtCustNameNCE_301cc56a_9bf0_4faa_92b6_d4f3ac4d2e22 = this.txtCustNameNCE;
		customControls.Add("301cc56a-9bf0-4faa-92b6-d4f3ac4d2e22", this.txtCustNameNCE);
		controlsHT.Add("301cc56a-9bf0-4faa-92b6-d4f3ac4d2e22", this.txtCustNameNCE);
		this.txtCustNameNCE.Name = "txtCustNameNCE";
		this.txtCustNameNCE.EpiGuid = "301cc56a-9bf0-4faa-92b6-d4f3ac4d2e22";
		this.neAdjAmtNCE = new Ice.Lib.Framework.EpiNumericEditor();
		this.neAdjAmtNCE_d0f365f3_9a1b_448c_8d65_4f6ced2b6a2f = this.neAdjAmtNCE;
		customControls.Add("d0f365f3-9a1b-448c-8d65-4f6ced2b6a2f", this.neAdjAmtNCE);
		controlsHT.Add("d0f365f3-9a1b-448c-8d65-4f6ced2b6a2f", this.neAdjAmtNCE);
		this.neAdjAmtNCE.Name = "neAdjAmtNCE";
		this.neAdjAmtNCE.EpiGuid = "d0f365f3-9a1b-448c-8d65-4f6ced2b6a2f";
		this.btnSearchCustomerNCE = new Ice.Lib.Framework.EpiButton();
		this.btnSearchCustomerNCE_f6ee7c62_ac0a_4fe6_828c_62ca14f93c2b = this.btnSearchCustomerNCE;
		customControls.Add("f6ee7c62-ac0a-4fe6-828c-62ca14f93c2b", this.btnSearchCustomerNCE);
		controlsHT.Add("f6ee7c62-ac0a-4fe6-828c-62ca14f93c2b", this.btnSearchCustomerNCE);
		this.btnSearchCustomerNCE.Name = "btnSearchCustomerNCE";
		this.btnSearchCustomerNCE.EpiGuid = "f6ee7c62-ac0a-4fe6-828c-62ca14f93c2b";
		this.txtCustIDPCE = new Ice.Lib.Framework.EpiTextBox();
		this.txtCustIDPCE_8d7a65aa_4a92_4999_8f2f_0dd48df57b7c = this.txtCustIDPCE;
		customControls.Add("8d7a65aa-4a92-4999-8f2f-0dd48df57b7c", this.txtCustIDPCE);
		controlsHT.Add("8d7a65aa-4a92-4999-8f2f-0dd48df57b7c", this.txtCustIDPCE);
		this.txtCustIDPCE.Name = "txtCustIDPCE";
		this.txtCustIDPCE.EpiGuid = "8d7a65aa-4a92-4999-8f2f-0dd48df57b7c";
		this.txtCustNamePCE = new Ice.Lib.Framework.EpiTextBox();
		this.txtCustNamePCE_75ccecd0_e064_4ea7_bc05_37a450618222 = this.txtCustNamePCE;
		customControls.Add("75ccecd0-e064-4ea7-bc05-37a450618222", this.txtCustNamePCE);
		controlsHT.Add("75ccecd0-e064-4ea7-bc05-37a450618222", this.txtCustNamePCE);
		this.txtCustNamePCE.Name = "txtCustNamePCE";
		this.txtCustNamePCE.EpiGuid = "75ccecd0-e064-4ea7-bc05-37a450618222";
		this.neAdjAmtPCE = new Ice.Lib.Framework.EpiNumericEditor();
		this.neAdjAmtPCE_dea99851_dd8c_475b_800a_6caf2b140822 = this.neAdjAmtPCE;
		customControls.Add("dea99851-dd8c-475b-800a-6caf2b140822", this.neAdjAmtPCE);
		controlsHT.Add("dea99851-dd8c-475b-800a-6caf2b140822", this.neAdjAmtPCE);
		this.neAdjAmtPCE.Name = "neAdjAmtPCE";
		this.neAdjAmtPCE.EpiGuid = "dea99851-dd8c-475b-800a-6caf2b140822";
		this.lblCustIDPCE = new Ice.Lib.Framework.EpiLabel();
		this.lblCustIDPCE_2c6f1664_c367_4f6a_a614_585951865695 = this.lblCustIDPCE;
		customControls.Add("2c6f1664-c367-4f6a-a614-585951865695", this.lblCustIDPCE);
		controlsHT.Add("2c6f1664-c367-4f6a-a614-585951865695", this.lblCustIDPCE);
		this.lblCustIDPCE.Name = "lblCustIDPCE";
		this.lblCustIDPCE.EpiGuid = "2c6f1664-c367-4f6a-a614-585951865695";
		this.lblCustNamePCE = new Ice.Lib.Framework.EpiLabel();
		this.lblCustNamePCE_ef43efe8_2106_422c_8301_a57b990adf70 = this.lblCustNamePCE;
		customControls.Add("ef43efe8-2106-422c-8301-a57b990adf70", this.lblCustNamePCE);
		controlsHT.Add("ef43efe8-2106-422c-8301-a57b990adf70", this.lblCustNamePCE);
		this.lblCustNamePCE.Name = "lblCustNamePCE";
		this.lblCustNamePCE.EpiGuid = "ef43efe8-2106-422c-8301-a57b990adf70";
		this.lblAdjAmtPCE = new Ice.Lib.Framework.EpiLabel();
		this.lblAdjAmtPCE_bf6f6b58_cb7c_4b17_91e7_ef7c39a91aab = this.lblAdjAmtPCE;
		customControls.Add("bf6f6b58-cb7c-4b17-91e7-ef7c39a91aab", this.lblAdjAmtPCE);
		controlsHT.Add("bf6f6b58-cb7c-4b17-91e7-ef7c39a91aab", this.lblAdjAmtPCE);
		this.lblAdjAmtPCE.Name = "lblAdjAmtPCE";
		this.lblAdjAmtPCE.EpiGuid = "bf6f6b58-cb7c-4b17-91e7-ef7c39a91aab";
		this.btnSearchCustomerPCE = new Ice.Lib.Framework.EpiButton();
		this.btnSearchCustomerPCE_a3e2e420_0666_4f8e_88e8_5211570ecda1 = this.btnSearchCustomerPCE;
		customControls.Add("a3e2e420-0666-4f8e-88e8-5211570ecda1", this.btnSearchCustomerPCE);
		controlsHT.Add("a3e2e420-0666-4f8e-88e8-5211570ecda1", this.btnSearchCustomerPCE);
		this.btnSearchCustomerPCE.Name = "btnSearchCustomerPCE";
		this.btnSearchCustomerPCE.EpiGuid = "a3e2e420-0666-4f8e-88e8-5211570ecda1";
		this.btnSubmitReallocCR = new Ice.Lib.Framework.EpiButton();
		this.btnSubmitReallocCR_06f44aed_d872_45d7_9229_b8d18d32c9b6 = this.btnSubmitReallocCR;
		customControls.Add("06f44aed-d872-45d7-9229-b8d18d32c9b6", this.btnSubmitReallocCR);
		controlsHT.Add("06f44aed-d872-45d7-9229-b8d18d32c9b6", this.btnSubmitReallocCR);
		this.btnSubmitReallocCR.Name = "btnSubmitReallocCR";
		this.btnSubmitReallocCR.EpiGuid = "06f44aed-d872-45d7-9229-b8d18d32c9b6";
		this.btnClear = new Ice.Lib.Framework.EpiButton();
		this.btnClear_4f3238d3_f83c_4da3_86c0_83e08a28cbef = this.btnClear;
		customControls.Add("4f3238d3-f83c-4da3-86c0-83e08a28cbef", this.btnClear);
		controlsHT.Add("4f3238d3-f83c-4da3-86c0-83e08a28cbef", this.btnClear);
		this.btnClear.Name = "btnClear";
		this.btnClear.EpiGuid = "4f3238d3-f83c-4da3-86c0-83e08a28cbef";
		this.ugPaymentGridReallocateNCE = new Ice.Lib.Framework.EpiUltraGrid();
		this.ugPaymentGridReallocateNCE_40bc58cd_c262_4daf_8d34_8ed1fbc81a14 = this.ugPaymentGridReallocateNCE;
		customControls.Add("40bc58cd-c262-4daf-8d34-8ed1fbc81a14", this.ugPaymentGridReallocateNCE);
		controlsHT.Add("40bc58cd-c262-4daf-8d34-8ed1fbc81a14", this.ugPaymentGridReallocateNCE);
		this.ugPaymentGridReallocateNCE.Name = "ugPaymentGridReallocateNCE";
		this.ugPaymentGridReallocateNCE.EpiGuid = "40bc58cd-c262-4daf-8d34-8ed1fbc81a14";
		this.btnSearchPaymentNCE = new Ice.Lib.Framework.EpiButton();
		this.btnSearchPaymentNCE_92c74efd_72f9_43e8_965b_424ab7a958e6 = this.btnSearchPaymentNCE;
		customControls.Add("92c74efd-72f9-43e8-965b-424ab7a958e6", this.btnSearchPaymentNCE);
		controlsHT.Add("92c74efd-72f9-43e8-965b-424ab7a958e6", this.btnSearchPaymentNCE);
		this.btnSearchPaymentNCE.Name = "btnSearchPaymentNCE";
		this.btnSearchPaymentNCE.EpiGuid = "92c74efd-72f9-43e8-965b-424ab7a958e6";
		this.lblPaymentDate = new Ice.Lib.Framework.EpiLabel();
		this.lblPaymentDate_e43d86f2_7f9e_49f1_ba7b_933a971ac3a3 = this.lblPaymentDate;
		customControls.Add("e43d86f2-7f9e-49f1-ba7b-933a971ac3a3", this.lblPaymentDate);
		controlsHT.Add("e43d86f2-7f9e-49f1-ba7b-933a971ac3a3", this.lblPaymentDate);
		this.lblPaymentDate.Name = "lblPaymentDate";
		this.lblPaymentDate.EpiGuid = "e43d86f2-7f9e-49f1-ba7b-933a971ac3a3";
		this.epiDateTimeEditorPaymentDate = new Ice.Lib.Framework.EpiDateTimeEditor();
		this.epiDateTimeEditorPaymentDate_938a6f03_ba18_4af9_b331_3ad0e810b5af = this.epiDateTimeEditorPaymentDate;
		customControls.Add("938a6f03-ba18-4af9-b331-3ad0e810b5af", this.epiDateTimeEditorPaymentDate);
		controlsHT.Add("938a6f03-ba18-4af9-b331-3ad0e810b5af", this.epiDateTimeEditorPaymentDate);
		this.epiDateTimeEditorPaymentDate.Name = "epiDateTimeEditorPaymentDate";
		this.epiDateTimeEditorPaymentDate.EpiGuid = "938a6f03-ba18-4af9-b331-3ad0e810b5af";
		this.lblRefInvoice = new Ice.Lib.Framework.EpiLabel();
		this.lblRefInvoice_9ce4f5c4_6cab_4c23_8a2b_e04b140c1d81 = this.lblRefInvoice;
		customControls.Add("9ce4f5c4-6cab-4c23-8a2b-e04b140c1d81", this.lblRefInvoice);
		controlsHT.Add("9ce4f5c4-6cab-4c23-8a2b-e04b140c1d81", this.lblRefInvoice);
		this.lblRefInvoice.Name = "lblRefInvoice";
		this.lblRefInvoice.EpiGuid = "9ce4f5c4-6cab-4c23-8a2b-e04b140c1d81";
		this.txtRefInvc = new Ice.Lib.Framework.EpiTextBox();
		this.txtRefInvc_64289281_a669_492d_822f_70661796133e = this.txtRefInvc;
		customControls.Add("64289281-a669-492d-822f-70661796133e", this.txtRefInvc);
		controlsHT.Add("64289281-a669-492d-822f-70661796133e", this.txtRefInvc);
		this.txtRefInvc.Name = "txtRefInvc";
		this.txtRefInvc.EpiGuid = "64289281-a669-492d-822f-70661796133e";
		this.pbsRefunds = new Ice.Lib.Framework.EpiBasePanel();
		this.pbsRefunds_c5d96db4_cac2_4061_ad43_ce19ee7900b7 = this.pbsRefunds;
		customControls.Add("c5d96db4-cac2-4061-ad43-ce19ee7900b7", this.pbsRefunds);
		controlsHT.Add("c5d96db4-cac2-4061-ad43-ce19ee7900b7", this.pbsRefunds);
		this.pbsRefunds.Name = "pbsRefunds";
		this.pbsRefunds.EpiGuid = "c5d96db4-cac2-4061-ad43-ce19ee7900b7";
		this.grbBxRefunds = new Ice.Lib.Framework.EpiGroupBox();
		this.grbBxRefunds_8b058e9d_6531_4621_8e3b_118a64130c3e = this.grbBxRefunds;
		customControls.Add("8b058e9d-6531-4621-8e3b-118a64130c3e", this.grbBxRefunds);
		controlsHT.Add("8b058e9d-6531-4621-8e3b-118a64130c3e", this.grbBxRefunds);
		this.grbBxRefunds.Name = "grbBxRefunds";
		this.grbBxRefunds.EpiGuid = "8b058e9d-6531-4621-8e3b-118a64130c3e";
		this.btnRfCust = new Ice.Lib.Framework.EpiButton();
		this.btnRfCust_86fb4f18_164e_4704_8153_66faab8f9488 = this.btnRfCust;
		customControls.Add("86fb4f18-164e-4704-8153-66faab8f9488", this.btnRfCust);
		controlsHT.Add("86fb4f18-164e-4704-8153-66faab8f9488", this.btnRfCust);
		this.btnRfCust.Name = "btnRfCust";
		this.btnRfCust.EpiGuid = "86fb4f18-164e-4704-8153-66faab8f9488";
		this.txtbxRfCust = new Ice.Lib.Framework.EpiTextBox();
		this.txtbxRfCust_395757f7_7fde_43c3_82e9_4dedec73ab7b = this.txtbxRfCust;
		customControls.Add("395757f7-7fde-43c3-82e9-4dedec73ab7b", this.txtbxRfCust);
		controlsHT.Add("395757f7-7fde-43c3-82e9-4dedec73ab7b", this.txtbxRfCust);
		this.txtbxRfCust.Name = "txtbxRfCust";
		this.txtbxRfCust.EpiGuid = "395757f7-7fde-43c3-82e9-4dedec73ab7b";
		this.lblRfCustName = new Ice.Lib.Framework.EpiLabel();
		this.lblRfCustName_1c4652dd_45f4_4441_a8ce_964dd2afff98 = this.lblRfCustName;
		customControls.Add("1c4652dd-45f4-4441-a8ce-964dd2afff98", this.lblRfCustName);
		controlsHT.Add("1c4652dd-45f4-4441-a8ce-964dd2afff98", this.lblRfCustName);
		this.lblRfCustName.Name = "lblRfCustName";
		this.lblRfCustName.EpiGuid = "1c4652dd-45f4-4441-a8ce-964dd2afff98";
		this.lblRfDate = new Ice.Lib.Framework.EpiLabel();
		this.lblRfDate_ff73df12_cca8_4069_8f25_f24735f22493 = this.lblRfDate;
		customControls.Add("ff73df12-cca8-4069-8f25-f24735f22493", this.lblRfDate);
		controlsHT.Add("ff73df12-cca8-4069-8f25-f24735f22493", this.lblRfDate);
		this.lblRfDate.Name = "lblRfDate";
		this.lblRfDate.EpiGuid = "ff73df12-cca8-4069-8f25-f24735f22493";
		this.txtbxRfCustName = new Ice.Lib.Framework.EpiTextBox();
		this.txtbxRfCustName_d8e70d8e_4cfe_4a50_8dc1_12a00703e782 = this.txtbxRfCustName;
		customControls.Add("d8e70d8e-4cfe-4a50-8dc1-12a00703e782", this.txtbxRfCustName);
		controlsHT.Add("d8e70d8e-4cfe-4a50-8dc1-12a00703e782", this.txtbxRfCustName);
		this.txtbxRfCustName.Name = "txtbxRfCustName";
		this.txtbxRfCustName.EpiGuid = "d8e70d8e-4cfe-4a50-8dc1-12a00703e782";
		this.lblRfAmnt = new Ice.Lib.Framework.EpiLabel();
		this.lblRfAmnt_9ffa4e89_8d30_4aef_987a_3444e54b76cf = this.lblRfAmnt;
		customControls.Add("9ffa4e89-8d30-4aef-987a-3444e54b76cf", this.lblRfAmnt);
		controlsHT.Add("9ffa4e89-8d30-4aef-987a-3444e54b76cf", this.lblRfAmnt);
		this.lblRfAmnt.Name = "lblRfAmnt";
		this.lblRfAmnt.EpiGuid = "9ffa4e89-8d30-4aef-987a-3444e54b76cf";
		this.lblRfSelcAmnt = new Ice.Lib.Framework.EpiLabel();
		this.lblRfSelcAmnt_cac2ca59_a5f2_4b20_a2ba_8d16bcdeeb13 = this.lblRfSelcAmnt;
		customControls.Add("cac2ca59-a5f2-4b20-a2ba-8d16bcdeeb13", this.lblRfSelcAmnt);
		controlsHT.Add("cac2ca59-a5f2-4b20-a2ba-8d16bcdeeb13", this.lblRfSelcAmnt);
		this.lblRfSelcAmnt.Name = "lblRfSelcAmnt";
		this.lblRfSelcAmnt.EpiGuid = "cac2ca59-a5f2-4b20-a2ba-8d16bcdeeb13";
		this.btnRfSCrdNte = new Ice.Lib.Framework.EpiButton();
		this.btnRfSCrdNte_efe8b6c8_7583_49e3_9e62_3675aa83c770 = this.btnRfSCrdNte;
		customControls.Add("efe8b6c8-7583-49e3-9e62-3675aa83c770", this.btnRfSCrdNte);
		controlsHT.Add("efe8b6c8-7583-49e3-9e62-3675aa83c770", this.btnRfSCrdNte);
		this.btnRfSCrdNte.Name = "btnRfSCrdNte";
		this.btnRfSCrdNte.EpiGuid = "efe8b6c8-7583-49e3-9e62-3675aa83c770";
		this.btnRfCancel = new Ice.Lib.Framework.EpiButton();
		this.btnRfCancel_8a06374d_811d_4952_ba68_44ce6f410561 = this.btnRfCancel;
		customControls.Add("8a06374d-811d-4952-ba68-44ce6f410561", this.btnRfCancel);
		controlsHT.Add("8a06374d-811d-4952-ba68-44ce6f410561", this.btnRfCancel);
		this.btnRfCancel.Name = "btnRfCancel";
		this.btnRfCancel.EpiGuid = "8a06374d-811d-4952-ba68-44ce6f410561";
		this.btnRfSubmit = new Ice.Lib.Framework.EpiButton();
		this.btnRfSubmit_f5cc15f3_635a_4030_acd4_73af478a9fab = this.btnRfSubmit;
		customControls.Add("f5cc15f3-635a-4030-acd4-73af478a9fab", this.btnRfSubmit);
		controlsHT.Add("f5cc15f3-635a-4030-acd4-73af478a9fab", this.btnRfSubmit);
		this.btnRfSubmit.Name = "btnRfSubmit";
		this.btnRfSubmit.EpiGuid = "f5cc15f3-635a-4030-acd4-73af478a9fab";
		this.grdRefunds = new Ice.Lib.Framework.EpiUltraGrid();
		this.grdRefunds_e256e64a_1a1f_48ce_a50c_0030aa0bd628 = this.grdRefunds;
		customControls.Add("e256e64a-1a1f-48ce-a50c-0030aa0bd628", this.grdRefunds);
		controlsHT.Add("e256e64a-1a1f-48ce-a50c-0030aa0bd628", this.grdRefunds);
		this.grdRefunds.Name = "grdRefunds";
		this.grdRefunds.EpiGuid = "e256e64a-1a1f-48ce-a50c-0030aa0bd628";
		this.epiDateTimeEditorRfDate = new Ice.Lib.Framework.EpiDateTimeEditor();
		this.epiDateTimeEditorRfDate_04bc487b_339c_4769_91db_e3da38b497f8 = this.epiDateTimeEditorRfDate;
		customControls.Add("04bc487b-339c-4769-91db-e3da38b497f8", this.epiDateTimeEditorRfDate);
		controlsHT.Add("04bc487b-339c-4769-91db-e3da38b497f8", this.epiDateTimeEditorRfDate);
		this.epiDateTimeEditorRfDate.Name = "epiDateTimeEditorRfDate";
		this.epiDateTimeEditorRfDate.EpiGuid = "04bc487b-339c-4769-91db-e3da38b497f8";
		this.epiCurrencyEditorRfAmount = new Ice.Lib.Framework.EpiCurrencyEditor();
		this.epiCurrencyEditorRfAmount_deaf2310_5bdd_48ac_8cc5_caf48ab49f80 = this.epiCurrencyEditorRfAmount;
		customControls.Add("deaf2310-5bdd-48ac-8cc5-caf48ab49f80", this.epiCurrencyEditorRfAmount);
		controlsHT.Add("deaf2310-5bdd-48ac-8cc5-caf48ab49f80", this.epiCurrencyEditorRfAmount);
		this.epiCurrencyEditorRfAmount.Name = "epiCurrencyEditorRfAmount";
		this.epiCurrencyEditorRfAmount.EpiGuid = "deaf2310-5bdd-48ac-8cc5-caf48ab49f80";
		this.epiCurrencyEditorRfSelcAmnt = new Ice.Lib.Framework.EpiCurrencyEditor();
		this.epiCurrencyEditorRfSelcAmnt_bdca2b3b_8051_4014_8176_d119155e8876 = this.epiCurrencyEditorRfSelcAmnt;
		customControls.Add("bdca2b3b-8051-4014-8176-d119155e8876", this.epiCurrencyEditorRfSelcAmnt);
		controlsHT.Add("bdca2b3b-8051-4014-8176-d119155e8876", this.epiCurrencyEditorRfSelcAmnt);
		this.epiCurrencyEditorRfSelcAmnt.Name = "epiCurrencyEditorRfSelcAmnt";
		this.epiCurrencyEditorRfSelcAmnt.EpiGuid = "bdca2b3b-8051-4014-8176-d119155e8876";
		// pbsReverseCR
		this.pbsReverseCR.Width = 1022;
		this.pbsReverseCR.Height = 433;
		this.pbsReverseCR.Controls.Add(this.gbReverseCR);
		this.pbsReverseCR.Controls.SetChildIndex(this.gbReverseCR, 0);
		this.pbsReverseCR.AutoScroll = true;
		System.Collections.Hashtable customSheets = personalizeCustomizeManager.CustControlMan.CustomSheetsHT;
		Infragistics.Win.UltraWinDock.DockableControlPane local1 = Ice.Lib.Customization.Designers.EpiCustomSheetDesigner.InitializeSheet(this.pbsReverseCR, "9da9d36f-74c7-45f2-b92e-c783bcc96321", customSheets);
		if ((local1 != null))
		{
			local1.Text = "";
			local1.TextTab = "Negative Receipt";
			Ice.Lib.Customization.Designers.EpiCustomSheetDesigner.AddCustomSheetToDockManager(personalizeCustomizeManager, local1, "baseDockManager1dff11bc-3024-4d17-acfc-b7af287e274b");
		}
		// pbsReallocCR
		this.pbsReallocCR.Controls.Add(this.gbNegativeCE);
		this.pbsReallocCR.Controls.Add(this.gbPCE);
		this.pbsReallocCR.Controls.Add(this.btnSubmitReallocCR);
		this.pbsReallocCR.Controls.SetChildIndex(this.gbPCE, 0);
		this.pbsReallocCR.Controls.SetChildIndex(this.gbNegativeCE, 1);
		this.pbsReallocCR.Controls.SetChildIndex(this.btnSubmitReallocCR, 2);
		this.pbsReallocCR.AutoScroll = true;
		Infragistics.Win.UltraWinDock.DockableControlPane local2 = Ice.Lib.Customization.Designers.EpiCustomSheetDesigner.InitializeSheet(this.pbsReallocCR, "09dab083-b2d2-44b1-ba2b-d3b997297334", customSheets);
		if ((local2 != null))
		{
			local2.Text = "";
			local2.TextTab = "Reallocate Cash Receipt";
			Ice.Lib.Customization.Designers.EpiCustomSheetDesigner.AddCustomSheetToDockManager(personalizeCustomizeManager, local2, "baseDockManager1dff11bc-3024-4d17-acfc-b7af287e274b");
		}
		// detailPanel1
		Ice.Lib.Framework.EpiBasePanel local3 = ((Ice.Lib.Framework.EpiBasePanel)(personalizeCustomizeManager.ControlsHT["d5488fbc-e47b-46b6-aa3e-9ab7d923315a"]));
		Ice.Lib.Framework.EpiGroupBox local4 = ((Ice.Lib.Framework.EpiGroupBox)(personalizeCustomizeManager.ControlsHT["c205404a-5523-4c79-8a60-d9271a99b21d"]));
		local3.Controls.SetChildIndex(local4, 0);
		System.Collections.Hashtable nativeSheets = personalizeCustomizeManager.NativeSheetsDCPsHT;
		Infragistics.Win.UltraWinDock.DockableControlPane local5 = Ice.Lib.Customization.Designers.EpiCustomSheetDesigner.GetDockableControlPane(local3, "d5488fbc-e47b-46b6-aa3e-9ab7d923315a", nativeSheets);
		// groupBox1
		local4.Top = 9;
		local4.Left = 9;
		local4.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left);
		local4.Width = 940;
		local4.Height = 218;
		local4.Controls.Add(this.lblBN);
		local4.Controls.Add(this.lblED);
		local4.Controls.Add(this.lblRef);
		local4.Controls.Add(this.lblTT);
		local4.Controls.Add(this.txtBN);
		local4.Controls.Add(this.dteED);
		local4.Controls.Add(this.txtRef);
		local4.Controls.Add(this.cTT);
		local4.Controls.Add(this.btnNewBatch);
		local4.Controls.Add(this.btnClear);
		local4.Controls.Add(this.lblRefInvoice);
		local4.Controls.Add(this.txtRefInvc);
		local4.Controls.SetChildIndex(this.cTT, 0);
		local4.Controls.SetChildIndex(this.lblTT, 1);
		local4.Controls.SetChildIndex(this.lblRefInvoice, 2);
		local4.Controls.SetChildIndex(this.txtRefInvc, 3);
		local4.Controls.SetChildIndex(this.txtRef, 4);
		local4.Controls.SetChildIndex(this.lblRef, 5);
		local4.Controls.SetChildIndex(this.lblED, 6);
		local4.Controls.SetChildIndex(this.txtBN, 6);
		local4.Controls.SetChildIndex(this.dteED, 6);
		local4.Controls.SetChildIndex(this.lblBN, 7);
		local4.Controls.SetChildIndex(this.btnClear, 8);
		local4.Controls.SetChildIndex(this.btnNewBatch, 9);
		Ice.Lib.Framework.EpiLabel local6 = ((Ice.Lib.Framework.EpiLabel)(personalizeCustomizeManager.ControlsHT["04f3b47b-383c-4f5e-aa3c-2ffd39fe2408"]));
		local4.Controls.SetChildIndex(local6, 10);
		Ice.Lib.Framework.EpiLabel local7 = ((Ice.Lib.Framework.EpiLabel)(personalizeCustomizeManager.ControlsHT["c1ff2f62-887f-479d-8525-9579e391b48d"]));
		local4.Controls.SetChildIndex(local7, 10);
		Ice.Lib.Framework.EpiLabel local8 = ((Ice.Lib.Framework.EpiLabel)(personalizeCustomizeManager.ControlsHT["443e1133-52a9-4161-bf57-c4600d1db5ed"]));
		local4.Controls.SetChildIndex(local8, 10);
		Ice.Lib.Framework.EpiLabel local9 = ((Ice.Lib.Framework.EpiLabel)(personalizeCustomizeManager.ControlsHT["d8f4aa34-2a56-4c6e-99c1-8fb86df2f572"]));
		local4.Controls.SetChildIndex(local9, 10);
		Ice.Lib.Framework.EpiLabel local10 = ((Ice.Lib.Framework.EpiLabel)(personalizeCustomizeManager.ControlsHT["0ea04446-4cbf-4d40-ad48-bf7b60fd2020"]));
		local4.Controls.SetChildIndex(local10, 10);
		Ice.Lib.Framework.EpiLabel local11 = ((Ice.Lib.Framework.EpiLabel)(personalizeCustomizeManager.ControlsHT["5ee3c271-ff03-47d1-bc6b-44c02b5258fb"]));
		local4.Controls.SetChildIndex(local11, 10);
		Ice.Lib.Framework.EpiTextBox local12 = ((Ice.Lib.Framework.EpiTextBox)(personalizeCustomizeManager.ControlsHT["368fd000-b055-4183-9d70-25d986650bd9"]));
		local4.Controls.SetChildIndex(local12, 10);
		// lblBN
		this.lblBN.Top = 20;
		this.lblBN.Left = 62;
		this.lblBN.Width = 136;
		this.lblBN.Height = 20;
		this.lblBN.Text = "Batch Number:";
		// lblED
		this.lblED.Top = 50;
		this.lblED.Left = 62;
		this.lblED.Width = 136;
		this.lblED.Height = 20;
		this.lblED.Text = "Effective Date:";
		// lblRef
		this.lblRef.Top = 80;
		this.lblRef.Left = 62;
		this.lblRef.Width = 136;
		this.lblRef.Height = 20;
		this.lblRef.Text = "Reference:";
		// lblTT
		this.lblTT.Top = 136;
		this.lblTT.Left = 58;
		this.lblTT.Width = 136;
		this.lblTT.Height = 20;
		this.lblTT.Text = "Transcation Type:";
		// txtBN
		this.txtBN.Top = 20;
		this.txtBN.Left = 204;
		this.txtBN.Width = 211;
		this.txtBN.Height = 20;
		this.txtBN.Text = "";
		this.txtBN.EpiBinding = "CashEntry.BatchNumber";
		this.txtBN.EpiLabel = "lblBN";
		this.txtBN.ReadOnly = true;
		// dteED
		this.dteED.Top = 50;
		this.dteED.Left = 204;
		this.dteED.Width = 211;
		this.dteED.EpiBinding = "CashEntry.EffectiveDate";
		this.dteED.EpiLabel = "lblED";
		// txtRef
		this.txtRef.Top = 80;
		this.txtRef.Left = 204;
		this.txtRef.Width = 211;
		this.txtRef.Height = 20;
		this.txtRef.Text = "";
		this.txtRef.EpiBinding = "CashEntry.Reference";
		this.txtRef.EpiLabel = "lblRef";
		// cTT
		this.cTT.Top = 138;
		this.cTT.Left = 201;
		this.cTT.Width = 211;
		this.cTT.Text = "";
		this.cTT.EpiBinding = "CashEntry.TransactionType";
		this.cTT.EpiLabel = "lblTT";
		this.cTT.AutoWidth = false;
		this.cTT.AutoWidthOption = ((Ice.Lib.Framework.AutoWidthOptions)(3));
		this.cTT.DisplayMember = "CodeDesc";
		this.cTT.EpiBOName = "Ice:BO:UserCodes";
		this.cTT.EpiColumns = new string[] {
				"CodeDesc"};
		this.cTT.EpiDataSetMode = ((Ice.Lib.Searches.DataSetMode)(1));
		this.cTT.EpiSort = "CodeID";
		this.cTT.SearchFilter = "CodeTypeID=\'pbsTranTyp\'";
		this.cTT.EpiTableName = "UDCodes";
		this.cTT.ValueMember = "CodeID";
		this.cTT.EpiAltSearchMethod = "";
		// btnNewBatch
		this.btnNewBatch.Top = 21;
		this.btnNewBatch.Left = 437;
		this.btnNewBatch.Width = 110;
		this.btnNewBatch.Height = 22;
		this.btnNewBatch.Text = "New Batch";
		// gbReverseCR
		this.gbReverseCR.Top = 9;
		this.gbReverseCR.Left = 9;
		this.gbReverseCR.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left);
		this.gbReverseCR.Width = 940;
		this.gbReverseCR.Height = 415;
		this.gbReverseCR.Text = "";
		this.gbReverseCR.Controls.Add(this.lblCustIDRCR);
		this.gbReverseCR.Controls.Add(this.lblCustNameRCR);
		this.gbReverseCR.Controls.Add(this.lblPaymentDateRCR);
		this.gbReverseCR.Controls.Add(this.txtCustIDReverseCR);
		this.gbReverseCR.Controls.Add(this.txtCustNameReverseCR);
		this.gbReverseCR.Controls.Add(this.dtePaymentDate);
		this.gbReverseCR.Controls.Add(this.btnSearchCustomerReverseCR);
		this.gbReverseCR.Controls.Add(this.ugPaymentGridReverseCR);
		this.gbReverseCR.Controls.Add(this.btnSubmitReverseCR);
		this.gbReverseCR.Controls.Add(this.btnSearchPayment);
		this.gbReverseCR.Controls.SetChildIndex(this.ugPaymentGridReverseCR, 0);
		this.gbReverseCR.Controls.SetChildIndex(this.dtePaymentDate, 1);
		this.gbReverseCR.Controls.SetChildIndex(this.lblPaymentDateRCR, 2);
		this.gbReverseCR.Controls.SetChildIndex(this.txtCustIDReverseCR, 3);
		this.gbReverseCR.Controls.SetChildIndex(this.lblCustIDRCR, 4);
		this.gbReverseCR.Controls.SetChildIndex(this.btnSearchPayment, 5);
		this.gbReverseCR.Controls.SetChildIndex(this.btnSubmitReverseCR, 6);
		this.gbReverseCR.Controls.SetChildIndex(this.btnSearchCustomerReverseCR, 7);
		this.gbReverseCR.Controls.SetChildIndex(this.txtCustNameReverseCR, 8);
		this.gbReverseCR.Controls.SetChildIndex(this.lblCustNameRCR, 9);
		// lblCustIDRCR
		this.lblCustIDRCR.Top = 9;
		this.lblCustIDRCR.Left = 62;
		this.lblCustIDRCR.Width = 136;
		this.lblCustIDRCR.Height = 20;
		this.lblCustIDRCR.Text = "Customer ID: ";
		// lblCustNameRCR
		this.lblCustNameRCR.Top = 40;
		this.lblCustNameRCR.Left = 62;
		this.lblCustNameRCR.Width = 136;
		this.lblCustNameRCR.Height = 20;
		this.lblCustNameRCR.Text = "Customer Name:";
		// lblPaymentDateRCR
		this.lblPaymentDateRCR.Top = 71;
		this.lblPaymentDateRCR.Left = 62;
		this.lblPaymentDateRCR.Width = 136;
		this.lblPaymentDateRCR.Height = 20;
		this.lblPaymentDateRCR.Text = "Payment Date:";
		// txtCustIDReverseCR
		this.txtCustIDReverseCR.Top = 9;
		this.txtCustIDReverseCR.Left = 204;
		this.txtCustIDReverseCR.Width = 211;
		this.txtCustIDReverseCR.Height = 20;
		this.txtCustIDReverseCR.Text = "";
		this.txtCustIDReverseCR.EpiBinding = "ReverseCashReceipt.CustID";
		this.txtCustIDReverseCR.EpiLabel = "lblCustIDRCR";
		// txtCustNameReverseCR
		this.txtCustNameReverseCR.Top = 40;
		this.txtCustNameReverseCR.Left = 204;
		this.txtCustNameReverseCR.Width = 211;
		this.txtCustNameReverseCR.Height = 20;
		this.txtCustNameReverseCR.Text = "";
		this.txtCustNameReverseCR.EpiBinding = "ReverseCashReceipt.Name";
		this.txtCustNameReverseCR.EpiLabel = "lblCustNameRCR";
		this.txtCustNameReverseCR.ReadOnly = true;
		// dtePaymentDate
		this.dtePaymentDate.Top = 71;
		this.dtePaymentDate.Left = 204;
		this.dtePaymentDate.Width = 211;
		this.dtePaymentDate.EpiBinding = "ReverseCashReceipt.PaymentDate";
		this.dtePaymentDate.EpiLabel = "lblPaymentDateRCR";
		// btnSearchCustomerReverseCR
		this.btnSearchCustomerReverseCR.Top = 10;
		this.btnSearchCustomerReverseCR.Left = 435;
		this.btnSearchCustomerReverseCR.Width = 110;
		this.btnSearchCustomerReverseCR.Height = 22;
		this.btnSearchCustomerReverseCR.Text = "Search Customer";
		// ugPaymentGridReverseCR
		this.ugPaymentGridReverseCR.Top = 101;
		this.ugPaymentGridReverseCR.Left = 22;
		this.ugPaymentGridReverseCR.Width = 901;
		this.ugPaymentGridReverseCR.Height = 278;
		this.ugPaymentGridReverseCR.Text = "Payment List";
		this.ugPaymentGridReverseCR.EpiBinding = "PaymentList";
		// btnSubmitReverseCR
		this.btnSubmitReverseCR.Top = 385;
		this.btnSubmitReverseCR.Left = 806;
		this.btnSubmitReverseCR.Width = 110;
		this.btnSubmitReverseCR.Height = 22;
		this.btnSubmitReverseCR.Text = "Submit";
		// btnSearchPayment
		this.btnSearchPayment.Top = 69;
		this.btnSearchPayment.Left = 808;
		this.btnSearchPayment.Width = 110;
		this.btnSearchPayment.Height = 22;
		this.btnSearchPayment.Text = "Search Payment";
		// gbNegativeCE
		this.gbNegativeCE.Top = 6;
		this.gbNegativeCE.Left = 9;
		this.gbNegativeCE.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left);
		this.gbNegativeCE.Width = 508;
		this.gbNegativeCE.Height = 394;
		this.gbNegativeCE.Text = "Negative Cash Entry";
		this.gbNegativeCE.Controls.Add(this.lblCustIDNCE);
		this.gbNegativeCE.Controls.Add(this.lblCustNameNCE);
		this.gbNegativeCE.Controls.Add(this.lblAdjAmtNCE);
		this.gbNegativeCE.Controls.Add(this.txtCustIDNCE);
		this.gbNegativeCE.Controls.Add(this.txtCustNameNCE);
		this.gbNegativeCE.Controls.Add(this.neAdjAmtNCE);
		this.gbNegativeCE.Controls.Add(this.btnSearchCustomerNCE);
		this.gbNegativeCE.Controls.Add(this.ugPaymentGridReallocateNCE);
		this.gbNegativeCE.Controls.Add(this.btnSearchPaymentNCE);
		this.gbNegativeCE.Controls.Add(this.lblPaymentDate);
		this.gbNegativeCE.Controls.Add(this.epiDateTimeEditorPaymentDate);
		this.gbNegativeCE.Controls.SetChildIndex(this.ugPaymentGridReallocateNCE, 0);
		this.gbNegativeCE.Controls.SetChildIndex(this.epiDateTimeEditorPaymentDate, 1);
		this.gbNegativeCE.Controls.SetChildIndex(this.lblPaymentDate, 2);
		this.gbNegativeCE.Controls.SetChildIndex(this.btnSearchPaymentNCE, 3);
		this.gbNegativeCE.Controls.SetChildIndex(this.neAdjAmtNCE, 4);
		this.gbNegativeCE.Controls.SetChildIndex(this.lblAdjAmtNCE, 5);
		this.gbNegativeCE.Controls.SetChildIndex(this.txtCustNameNCE, 6);
		this.gbNegativeCE.Controls.SetChildIndex(this.lblCustNameNCE, 7);
		this.gbNegativeCE.Controls.SetChildIndex(this.txtCustIDNCE, 8);
		this.gbNegativeCE.Controls.SetChildIndex(this.lblCustIDNCE, 9);
		this.gbNegativeCE.Controls.SetChildIndex(this.btnSearchCustomerNCE, 9);
		// gbPCE
		this.gbPCE.Top = 6;
		this.gbPCE.Left = 522;
		this.gbPCE.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left);
		this.gbPCE.Width = 460;
		this.gbPCE.Height = 394;
		this.gbPCE.Text = "Postive Cash Entry";
		this.gbPCE.Controls.Add(this.txtCustIDPCE);
		this.gbPCE.Controls.Add(this.txtCustNamePCE);
		this.gbPCE.Controls.Add(this.neAdjAmtPCE);
		this.gbPCE.Controls.Add(this.lblCustIDPCE);
		this.gbPCE.Controls.Add(this.lblCustNamePCE);
		this.gbPCE.Controls.Add(this.lblAdjAmtPCE);
		this.gbPCE.Controls.Add(this.btnSearchCustomerPCE);
		this.gbPCE.Controls.SetChildIndex(this.neAdjAmtPCE, 0);
		this.gbPCE.Controls.SetChildIndex(this.lblAdjAmtPCE, 1);
		this.gbPCE.Controls.SetChildIndex(this.btnSearchCustomerPCE, 2);
		this.gbPCE.Controls.SetChildIndex(this.txtCustNamePCE, 3);
		this.gbPCE.Controls.SetChildIndex(this.lblCustNamePCE, 4);
		this.gbPCE.Controls.SetChildIndex(this.txtCustIDPCE, 5);
		this.gbPCE.Controls.SetChildIndex(this.lblCustIDPCE, 6);
		// lblCustIDNCE
		this.lblCustIDNCE.Top = 42;
		this.lblCustIDNCE.Left = 8;
		this.lblCustIDNCE.Width = 136;
		this.lblCustIDNCE.Height = 20;
		this.lblCustIDNCE.Text = "Customer ID:";
		// lblCustNameNCE
		this.lblCustNameNCE.Top = 73;
		this.lblCustNameNCE.Left = 8;
		this.lblCustNameNCE.Width = 136;
		this.lblCustNameNCE.Height = 20;
		this.lblCustNameNCE.Text = "Customer Name:";
		// lblAdjAmtNCE
		this.lblAdjAmtNCE.Top = 136;
		this.lblAdjAmtNCE.Left = 5;
		this.lblAdjAmtNCE.Width = 136;
		this.lblAdjAmtNCE.Height = 20;
		this.lblAdjAmtNCE.Text = "Adjustment Amount:";
		// txtCustIDNCE
		this.txtCustIDNCE.Top = 42;
		this.txtCustIDNCE.Left = 150;
		this.txtCustIDNCE.Width = 117;
		this.txtCustIDNCE.Height = 20;
		this.txtCustIDNCE.Text = "";
		this.txtCustIDNCE.EpiBinding = "ReallocNegativeCashReceipt.CustID";
		this.txtCustIDNCE.EpiLabel = "lblCustIDNCE";
		// txtCustNameNCE
		this.txtCustNameNCE.Top = 73;
		this.txtCustNameNCE.Left = 150;
		this.txtCustNameNCE.Width = 211;
		this.txtCustNameNCE.Height = 20;
		this.txtCustNameNCE.Text = "";
		this.txtCustNameNCE.EpiBinding = "ReallocNegativeCashReceipt.Name";
		this.txtCustNameNCE.EpiLabel = "lblCustNameNCE";
		this.txtCustNameNCE.ReadOnly = true;
		// neAdjAmtNCE
		this.neAdjAmtNCE.NumericType = ((Infragistics.Win.UltraWinEditors.NumericType)(2));
		this.neAdjAmtNCE.MaskInput = "-nnnnnnnnnnnnnnn.nn";
		this.neAdjAmtNCE.Top = 136;
		this.neAdjAmtNCE.Left = 147;
		this.neAdjAmtNCE.Width = 211;
		this.neAdjAmtNCE.EpiBinding = "ReallocNegativeCashReceipt.AdjAmt";
		this.neAdjAmtNCE.EpiLabel = "lblAdjAmtNCE";
		this.neAdjAmtNCE.Nullable = true;
		// btnSearchCustomerNCE
		this.btnSearchCustomerNCE.Top = 39;
		this.btnSearchCustomerNCE.Left = 290;
		this.btnSearchCustomerNCE.Width = 110;
		this.btnSearchCustomerNCE.Height = 22;
		this.btnSearchCustomerNCE.Text = "Search Customer";
		// txtCustIDPCE
		this.txtCustIDPCE.Top = 42;
		this.txtCustIDPCE.Left = 157;
		this.txtCustIDPCE.Width = 117;
		this.txtCustIDPCE.Height = 20;
		this.txtCustIDPCE.Text = "";
		this.txtCustIDPCE.EpiBinding = "ReallocPositiveCashReceipt.CustID";
		this.txtCustIDPCE.EpiLabel = "lblCustIDPCE";
		// txtCustNamePCE
		this.txtCustNamePCE.Top = 73;
		this.txtCustNamePCE.Left = 157;
		this.txtCustNamePCE.Width = 211;
		this.txtCustNamePCE.Height = 20;
		this.txtCustNamePCE.Text = "";
		this.txtCustNamePCE.EpiBinding = "ReallocPositiveCashReceipt.Name";
		this.txtCustNamePCE.EpiLabel = "lblCustNamePCE";
		this.txtCustNamePCE.ReadOnly = true;
		// neAdjAmtPCE
		this.neAdjAmtPCE.NumericType = ((Infragistics.Win.UltraWinEditors.NumericType)(2));
		this.neAdjAmtPCE.MaskInput = "-nnnnnnnnnnnnnnn.nn";
		this.neAdjAmtPCE.Top = 105;
		this.neAdjAmtPCE.Left = 157;
		this.neAdjAmtPCE.Width = 211;
		this.neAdjAmtPCE.EpiBinding = "ReallocPositiveCashReceipt.AdjAmt";
		this.neAdjAmtPCE.EpiLabel = "lblAdjAmtPCE";
		this.neAdjAmtPCE.Nullable = true;
		// lblCustIDPCE
		this.lblCustIDPCE.Top = 42;
		this.lblCustIDPCE.Left = 15;
		this.lblCustIDPCE.Width = 136;
		this.lblCustIDPCE.Height = 20;
		this.lblCustIDPCE.Text = "Customer ID:";
		// lblCustNamePCE
		this.lblCustNamePCE.Top = 73;
		this.lblCustNamePCE.Left = 15;
		this.lblCustNamePCE.Width = 136;
		this.lblCustNamePCE.Height = 20;
		this.lblCustNamePCE.Text = "Customer Name:";
		// lblAdjAmtPCE
		this.lblAdjAmtPCE.Top = 105;
		this.lblAdjAmtPCE.Left = 15;
		this.lblAdjAmtPCE.Width = 136;
		this.lblAdjAmtPCE.Height = 20;
		this.lblAdjAmtPCE.Text = "Adjustment Amount: ";
		// btnSearchCustomerPCE
		this.btnSearchCustomerPCE.Top = 39;
		this.btnSearchCustomerPCE.Left = 297;
		this.btnSearchCustomerPCE.Width = 110;
		this.btnSearchCustomerPCE.Height = 22;
		this.btnSearchCustomerPCE.Text = "Search Customer";
		// btnSubmitReallocCR
		this.btnSubmitReallocCR.Top = 405;
		this.btnSubmitReallocCR.Left = 871;
		this.btnSubmitReallocCR.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left);
		this.btnSubmitReallocCR.Width = 110;
		this.btnSubmitReallocCR.Height = 22;
		this.btnSubmitReallocCR.Text = "Submit";
		// ice.core.session.dll
		// ice.contracts.bo.dynamicquery.dll
		// ice.contracts.lib.boreader.dll
		// btnClear
		this.btnClear.Top = 185;
		this.btnClear.Left = 809;
		this.btnClear.Width = 110;
		this.btnClear.Height = 22;
		this.btnClear.Text = "Clear Screen";
		// ugPaymentGridReallocateNCE
		this.ugPaymentGridReallocateNCE.Top = 164;
		this.ugPaymentGridReallocateNCE.Left = 13;
		this.ugPaymentGridReallocateNCE.Width = 480;
		this.ugPaymentGridReallocateNCE.Height = 211;
		this.ugPaymentGridReallocateNCE.Text = "Reallocate Payment List";
		this.ugPaymentGridReallocateNCE.EpiBinding = "ReallocatePaymentList";
		// btnSearchPaymentNCE
		this.btnSearchPaymentNCE.Top = 104;
		this.btnSearchPaymentNCE.Left = 377;
		this.btnSearchPaymentNCE.Width = 105;
		this.btnSearchPaymentNCE.Height = 23;
		this.btnSearchPaymentNCE.Text = "Search Payment";
		this.btnSearchPaymentNCE.EpiBinding = "";
		// lblPaymentDate
		this.lblPaymentDate.Top = 105;
		this.lblPaymentDate.Left = 53;
		this.lblPaymentDate.Width = 90;
		this.lblPaymentDate.Height = 20;
		this.lblPaymentDate.Text = "Payment Date:";
		// epiDateTimeEditorPaymentDate
		this.epiDateTimeEditorPaymentDate.Top = 105;
		this.epiDateTimeEditorPaymentDate.Left = 149;
		this.epiDateTimeEditorPaymentDate.Width = 210;
		this.epiDateTimeEditorPaymentDate.EpiBinding = "ReallocNegativeCashReceipt.PaymentDate";
		this.epiDateTimeEditorPaymentDate.EpiLabel = "lblPaymentDate";
		// lblRefInvoice
		this.lblRefInvoice.Top = 109;
		this.lblRefInvoice.Left = 124;
		this.lblRefInvoice.Width = 73;
		this.lblRefInvoice.Height = 20;
		this.lblRefInvoice.Text = "Ref Invoice:";
		// txtRefInvc
		this.txtRefInvc.Top = 109;
		this.txtRefInvc.Left = 203;
		this.txtRefInvc.Width = 212;
		this.txtRefInvc.Height = 20;
		this.txtRefInvc.Text = "";
		this.txtRefInvc.EpiBinding = "CashEntry.RefInvoice";
		this.txtRefInvc.EpiLabel = "lblRefInvoice";
		// erp.adapters.cashrec.dll
		// erp.contracts.bo.cashrec.dll
		// pbsRefunds
		this.pbsRefunds.Top = 20;
		this.pbsRefunds.Left = 0;
		this.pbsRefunds.Width = 1920;
		this.pbsRefunds.Height = 580;
		this.pbsRefunds.Controls.Add(this.grbBxRefunds);
		this.pbsRefunds.Controls.SetChildIndex(this.grbBxRefunds, 0);
		this.pbsRefunds.AutoScroll = true;
		Infragistics.Win.UltraWinDock.DockableControlPane local13 = Ice.Lib.Customization.Designers.EpiCustomSheetDesigner.InitializeSheet(this.pbsRefunds, "c5d96db4-cac2-4061-ad43-ce19ee7900b7", customSheets);
		if ((local13 != null))
		{
			local13.Text = "Refunds";
			local13.TextTab = "Refunds";
			Ice.Lib.Customization.Designers.EpiCustomSheetDesigner.AddCustomSheetToDockManager(personalizeCustomizeManager, local13, "baseDockManager1dff11bc-3024-4d17-acfc-b7af287e274b");
		}
		// grbBxRefunds
		this.grbBxRefunds.Top = 0;
		this.grbBxRefunds.Left = 0;
		this.grbBxRefunds.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left);
		this.grbBxRefunds.Width = 964;
		this.grbBxRefunds.Height = 510;
		this.grbBxRefunds.Text = "Refunds:";
		this.grbBxRefunds.Controls.Add(this.btnRfCust);
		this.grbBxRefunds.Controls.Add(this.txtbxRfCust);
		this.grbBxRefunds.Controls.Add(this.lblRfCustName);
		this.grbBxRefunds.Controls.Add(this.lblRfDate);
		this.grbBxRefunds.Controls.Add(this.txtbxRfCustName);
		this.grbBxRefunds.Controls.Add(this.lblRfAmnt);
		this.grbBxRefunds.Controls.Add(this.lblRfSelcAmnt);
		this.grbBxRefunds.Controls.Add(this.btnRfSCrdNte);
		this.grbBxRefunds.Controls.Add(this.btnRfCancel);
		this.grbBxRefunds.Controls.Add(this.btnRfSubmit);
		this.grbBxRefunds.Controls.Add(this.grdRefunds);
		this.grbBxRefunds.Controls.Add(this.epiDateTimeEditorRfDate);
		this.grbBxRefunds.Controls.Add(this.epiCurrencyEditorRfAmount);
		this.grbBxRefunds.Controls.Add(this.epiCurrencyEditorRfSelcAmnt);
		this.grbBxRefunds.Controls.SetChildIndex(this.epiCurrencyEditorRfSelcAmnt, 0);
		this.grbBxRefunds.Controls.SetChildIndex(this.epiCurrencyEditorRfAmount, 1);
		this.grbBxRefunds.Controls.SetChildIndex(this.epiDateTimeEditorRfDate, 2);
		this.grbBxRefunds.Controls.SetChildIndex(this.grdRefunds, 3);
		this.grbBxRefunds.Controls.SetChildIndex(this.txtbxRfCust, 4);
		this.grbBxRefunds.Controls.SetChildIndex(this.btnRfCancel, 5);
		this.grbBxRefunds.Controls.SetChildIndex(this.btnRfSubmit, 6);
		this.grbBxRefunds.Controls.SetChildIndex(this.btnRfSCrdNte, 7);
		this.grbBxRefunds.Controls.SetChildIndex(this.lblRfDate, 8);
		this.grbBxRefunds.Controls.SetChildIndex(this.lblRfAmnt, 8);
		this.grbBxRefunds.Controls.SetChildIndex(this.lblRfSelcAmnt, 8);
		this.grbBxRefunds.Controls.SetChildIndex(this.btnRfCust, 11);
		this.grbBxRefunds.Controls.SetChildIndex(this.txtbxRfCustName, 12);
		this.grbBxRefunds.Controls.SetChildIndex(this.lblRfCustName, 13);
		// btnRfCust
		this.btnRfCust.Top = 24;
		this.btnRfCust.Left = 32;
		this.btnRfCust.Width = 83;
		this.btnRfCust.Height = 23;
		this.btnRfCust.Text = "Customer";
		this.btnRfCust.EpiBinding = "Refunds.btnRfCust";
		// txtbxRfCust
		this.txtbxRfCust.Top = 26;
		this.txtbxRfCust.Left = 132;
		this.txtbxRfCust.Width = 188;
		this.txtbxRfCust.Height = 20;
		this.txtbxRfCust.Text = "";
		this.txtbxRfCust.EpiBinding = "Refunds.CustID";
		// lblRfCustName
		this.lblRfCustName.Top = 56;
		this.lblRfCustName.Left = 25;
		this.lblRfCustName.Width = 97;
		this.lblRfCustName.Height = 20;
		this.lblRfCustName.Text = "Customer Name";
		// lblRfDate
		this.lblRfDate.Top = 88;
		this.lblRfDate.Left = 40;
		this.lblRfDate.Width = 82;
		this.lblRfDate.Height = 20;
		this.lblRfDate.Text = "Refund Date";
		// txtbxRfCustName
		this.txtbxRfCustName.Top = 57;
		this.txtbxRfCustName.Left = 132;
		this.txtbxRfCustName.Width = 188;
		this.txtbxRfCustName.Height = 20;
		this.txtbxRfCustName.Text = "";
		this.txtbxRfCustName.EpiBinding = "Refunds.Name";
		this.txtbxRfCustName.EpiLabel = "lblRfCustName";
		// lblRfAmnt
		this.lblRfAmnt.Top = 120;
		this.lblRfAmnt.Left = 27;
		this.lblRfAmnt.Width = 95;
		this.lblRfAmnt.Height = 20;
		this.lblRfAmnt.Text = "Refund Amount";
		// lblRfSelcAmnt
		this.lblRfSelcAmnt.Top = 152;
		this.lblRfSelcAmnt.Left = 22;
		this.lblRfSelcAmnt.Width = 100;
		this.lblRfSelcAmnt.Height = 20;
		this.lblRfSelcAmnt.Text = "Selected Amount";
		// btnRfSCrdNte
		this.btnRfSCrdNte.Top = 120;
		this.btnRfSCrdNte.Left = 424;
		this.btnRfSCrdNte.Width = 183;
		this.btnRfSCrdNte.Height = 20;
		this.btnRfSCrdNte.Text = "Select Credit Note";
		this.btnRfSCrdNte.EpiBinding = "Refunds.btnRfSCrdNte";
		// btnRfCancel
		this.btnRfCancel.Top = 152;
		this.btnRfCancel.Left = 488;
		this.btnRfCancel.Width = 116;
		this.btnRfCancel.Height = 20;
		this.btnRfCancel.Text = "Cancel";
		this.btnRfCancel.EpiBinding = "Refunds.btnRfCancel";
		// btnRfSubmit
		this.btnRfSubmit.Top = 152;
		this.btnRfSubmit.Left = 336;
		this.btnRfSubmit.Width = 116;
		this.btnRfSubmit.Height = 20;
		this.btnRfSubmit.Text = "Submit";
		this.btnRfSubmit.EpiBinding = "Refunds.btnRfSubmit";
		// grdRefunds
		this.grdRefunds.Top = 200;
		this.grdRefunds.Left = 15;
		this.grdRefunds.Width = 933;
		this.grdRefunds.Height = 294;
		this.grdRefunds.Text = "CreditNotes";
		this.grdRefunds.EpiBinding = "RefundsList";
		// epiDateTimeEditorRfDate
		this.epiDateTimeEditorRfDate.Top = 88;
		this.epiDateTimeEditorRfDate.Left = 136;
		this.epiDateTimeEditorRfDate.Width = 182;
		this.epiDateTimeEditorRfDate.EpiBinding = "Refunds.RefundDate";
		// epiCurrencyEditorRfAmount
		this.epiCurrencyEditorRfAmount.Top = 120;
		this.epiCurrencyEditorRfAmount.Left = 136;
		this.epiCurrencyEditorRfAmount.Width = 177;
		this.epiCurrencyEditorRfAmount.EpiBinding = "Refunds.RefundAmount";
		// epiCurrencyEditorRfSelcAmnt
		this.epiCurrencyEditorRfSelcAmnt.Top = 152;
		this.epiCurrencyEditorRfSelcAmnt.Left = 136;
		this.epiCurrencyEditorRfSelcAmnt.Width = 176;
		this.epiCurrencyEditorRfSelcAmnt.EpiBinding = "Refunds.RefundSelectedAmount";
		// UD10Form
		Ice.Lib.Framework.EpiBaseForm local14 = ((Ice.Lib.Framework.EpiBaseForm)(personalizeCustomizeManager.ControlsHT["2dcd1674-5e34-4d98-b493-c75747027376"]));
		local14.Top = 143;
		local14.Left = 372;
		local14.Width = 1029;
		local14.Height = 791;
		local14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
		local14.Text = "Cash Entry";
		// mainPanel1
		Ice.Lib.Framework.EpiDockManagerPanel local15 = ((Ice.Lib.Framework.EpiDockManagerPanel)(personalizeCustomizeManager.ControlsHT["1dff11bc-3024-4d17-acfc-b7af287e274b"]));
		Infragistics.Win.UltraWinDock.DockableControlPane local16 = Ice.Lib.Customization.Designers.EpiCustomSheetDesigner.GetDockableControlPane(local15, "1dff11bc-3024-4d17-acfc-b7af287e274b", nativeSheets);
		if ((local16 != null))
		{
			local16.Text = "Cash Entry";
		}
		// lblKey1
		local6.Top = 12;
		local6.Left = 126;
		local6.Width = 174;
		local6.Height = 20;
		local6.Visible = false;
		// txtKeyField
		Ice.Lib.Framework.EpiTextBox local17 = ((Ice.Lib.Framework.EpiTextBox)(personalizeCustomizeManager.ControlsHT["46567b2e-6bc0-4967-be35-a0ec6843838f"]));
		local17.Visible = false;
		// lblKey2
		local7.Top = 36;
		local7.Left = 180;
		local7.Width = 120;
		local7.Height = 20;
		local7.Visible = false;
		// txtKeyField2
		Ice.Lib.Framework.EpiTextBox local18 = ((Ice.Lib.Framework.EpiTextBox)(personalizeCustomizeManager.ControlsHT["5feb5781-48c2-440b-845e-019bd6d897f3"]));
		local18.Visible = false;
		// lblKey3
		local8.Top = 60;
		local8.Left = 180;
		local8.Width = 120;
		local8.Height = 20;
		local8.TabStop = false;
		local8.Visible = false;
		// txtKeyField3
		Ice.Lib.Framework.EpiTextBox local19 = ((Ice.Lib.Framework.EpiTextBox)(personalizeCustomizeManager.ControlsHT["270f2a33-fb54-4f81-87eb-b76ea1c88653"]));
		local19.Visible = false;
		// lblKey4
		local9.Top = 84;
		local9.Left = 180;
		local9.Width = 120;
		local9.Height = 20;
		local9.Visible = false;
		// txtKeyField4
		Ice.Lib.Framework.EpiTextBox local20 = ((Ice.Lib.Framework.EpiTextBox)(personalizeCustomizeManager.ControlsHT["2a1d9451-d277-48be-92f6-abca977cde0c"]));
		local20.Visible = false;
		// lblKey5
		local10.Top = 108;
		local10.Left = 180;
		local10.Width = 120;
		local10.Height = 20;
		local10.Visible = false;
		// txtKeyField5
		Ice.Lib.Framework.EpiTextBox local21 = ((Ice.Lib.Framework.EpiTextBox)(personalizeCustomizeManager.ControlsHT["a6a32ec0-bc92-48cd-a6c0-7455927bf4aa"]));
		local21.Visible = false;
		// lblDesc
		local11.Top = 132;
		local11.Left = 180;
		local11.Width = 120;
		local11.Height = 20;
		local11.Visible = false;
		// txtDesc
		local12.Visible = false;
		// listPanel1
		Ice.Lib.Framework.EpiBasePanel local22 = ((Ice.Lib.Framework.EpiBasePanel)(personalizeCustomizeManager.ControlsHT["5bc5702d-bf5b-4bcd-8555-a5be760a5449"]));
		local22.Top = 0;
		local22.Left = 0;
		local22.Width = 736;
		local22.Height = 336;
		Infragistics.Win.UltraWinDock.DockableControlPane local23 = Ice.Lib.Customization.Designers.EpiCustomSheetDesigner.GetDockableControlPane(local22, "5bc5702d-bf5b-4bcd-8555-a5be760a5449", nativeSheets);
		if ((local23 != null))
		{
			local22.Visible = false;
			Ice.Lib.Customization.Designers.EpiCustomSheetDesigner.AddToSheetsVisToggleHT(personalizeCustomizeManager, local22);
		}
		// btnKeyField
		Ice.Lib.Framework.EpiButton local24 = ((Ice.Lib.Framework.EpiButton)(personalizeCustomizeManager.ControlsHT["3b06b8f2-a9d0-44d0-9aa1-4db6ddc60c77"]));
		local24.Visible = false;
		// Finishing control initialization.
		topControl.FindForm().ResumeLayout();
	}

	public void DestroyGlobalVariables()
	{
		this.pbsReverseCR = null;
		this.pbsReverseCR_9da9d36f_74c7_45f2_b92e_c783bcc96321 = null;
		this.pbsReallocCR = null;
		this.pbsReallocCR_09dab083_b2d2_44b1_ba2b_d3b997297334 = null;
		this.lblBN = null;
		this.lblBN_152b9a77_59c7_4684_ba74_bbfb4b5d56fb = null;
		this.lblED = null;
		this.lblED_ba69c022_527c_40a0_a1de_493aafdf6dc1 = null;
		this.lblRef = null;
		this.lblRef_5421c36f_623f_4370_95b9_1442aa7868ff = null;
		this.lblTT = null;
		this.lblTT_fd55eec6_18d5_47c3_bd39_3f21bb59fb23 = null;
		this.txtBN = null;
		this.txtBN_f4f8d9e7_eb4e_48ee_a1e5_83e65407c685 = null;
		this.dteED = null;
		this.dteED_6648c36d_7a31_4827_8edb_639d958f6351 = null;
		this.txtRef = null;
		this.txtRef_9c52bcd0_6c51_497a_9902_3bdf75c4b902 = null;
		this.cTT = null;
		this.cTT_0cd953be_5bd7_4d73_9018_be388739cd55 = null;
		this.btnNewBatch = null;
		this.btnNewBatch_d72e5089_3b70_48dd_becd_6a4b79a41899 = null;
		this.gbReverseCR = null;
		this.gbReverseCR_a1dce6eb_d86b_49c8_a4c2_9896f3e44f7b = null;
		this.lblCustIDRCR = null;
		this.lblCustIDRCR_82675383_5fe2_4399_bf8f_1ad6ab2da074 = null;
		this.lblCustNameRCR = null;
		this.lblCustNameRCR_754aa08a_bf26_44b0_bcc9_85499189b473 = null;
		this.lblPaymentDateRCR = null;
		this.lblPaymentDateRCR_4fc5f62e_fde1_4ecb_a5f5_c550c165016f = null;
		this.txtCustIDReverseCR = null;
		this.txtCustIDReverseCR_7bb56d18_3751_4158_88f5_b14a2e11ff02 = null;
		this.txtCustNameReverseCR = null;
		this.txtCustNameReverseCR_b54ce727_6582_44a9_8a51_3935ed0f77b0 = null;
		this.dtePaymentDate = null;
		this.dtePaymentDate_35e32fb2_4e4c_4d03_89da_5cfdc33daee9 = null;
		this.btnSearchCustomerReverseCR = null;
		this.btnSearchCustomerReverseCR_1191f761_cce0_43a3_b1bc_c8739fef974d = null;
		this.ugPaymentGridReverseCR = null;
		this.ugPaymentGridReverseCR_02e4071d_31e5_4af3_9283_a1a6a3d19f14 = null;
		this.btnSubmitReverseCR = null;
		this.btnSubmitReverseCR_3b004280_f70a_42c0_bc2c_3acffda4dd5f = null;
		this.btnSearchPayment = null;
		this.btnSearchPayment_53d59437_75c7_4a34_b5cf_40ac54170b60 = null;
		this.gbNegativeCE = null;
		this.gbNegativeCE_39dca234_6254_41fe_90ef_e66c2aa78692 = null;
		this.gbPCE = null;
		this.gbPCE_e98f4e59_ae1f_4db8_98de_6c8fe4be76af = null;
		this.lblCustIDNCE = null;
		this.lblCustIDNCE_613f351d_d546_404b_a5d5_eed09f8c128c = null;
		this.lblCustNameNCE = null;
		this.lblCustNameNCE_819e4258_c9fc_4373_80c8_6fc9cdea830f = null;
		this.lblAdjAmtNCE = null;
		this.lblAdjAmtNCE_7d2db45d_8006_4651_bc50_c999c4342924 = null;
		this.txtCustIDNCE = null;
		this.txtCustIDNCE_f2f928f2_c5aa_4fb3_9b05_d5535f5f1d70 = null;
		this.txtCustNameNCE = null;
		this.txtCustNameNCE_301cc56a_9bf0_4faa_92b6_d4f3ac4d2e22 = null;
		this.neAdjAmtNCE = null;
		this.neAdjAmtNCE_d0f365f3_9a1b_448c_8d65_4f6ced2b6a2f = null;
		this.btnSearchCustomerNCE = null;
		this.btnSearchCustomerNCE_f6ee7c62_ac0a_4fe6_828c_62ca14f93c2b = null;
		this.txtCustIDPCE = null;
		this.txtCustIDPCE_8d7a65aa_4a92_4999_8f2f_0dd48df57b7c = null;
		this.txtCustNamePCE = null;
		this.txtCustNamePCE_75ccecd0_e064_4ea7_bc05_37a450618222 = null;
		this.neAdjAmtPCE = null;
		this.neAdjAmtPCE_dea99851_dd8c_475b_800a_6caf2b140822 = null;
		this.lblCustIDPCE = null;
		this.lblCustIDPCE_2c6f1664_c367_4f6a_a614_585951865695 = null;
		this.lblCustNamePCE = null;
		this.lblCustNamePCE_ef43efe8_2106_422c_8301_a57b990adf70 = null;
		this.lblAdjAmtPCE = null;
		this.lblAdjAmtPCE_bf6f6b58_cb7c_4b17_91e7_ef7c39a91aab = null;
		this.btnSearchCustomerPCE = null;
		this.btnSearchCustomerPCE_a3e2e420_0666_4f8e_88e8_5211570ecda1 = null;
		this.btnSubmitReallocCR = null;
		this.btnSubmitReallocCR_06f44aed_d872_45d7_9229_b8d18d32c9b6 = null;
		this.btnClear = null;
		this.btnClear_4f3238d3_f83c_4da3_86c0_83e08a28cbef = null;
		this.ugPaymentGridReallocateNCE = null;
		this.ugPaymentGridReallocateNCE_40bc58cd_c262_4daf_8d34_8ed1fbc81a14 = null;
		this.btnSearchPaymentNCE = null;
		this.btnSearchPaymentNCE_92c74efd_72f9_43e8_965b_424ab7a958e6 = null;
		this.lblPaymentDate = null;
		this.lblPaymentDate_e43d86f2_7f9e_49f1_ba7b_933a971ac3a3 = null;
		this.epiDateTimeEditorPaymentDate = null;
		this.epiDateTimeEditorPaymentDate_938a6f03_ba18_4af9_b331_3ad0e810b5af = null;
		this.lblRefInvoice = null;
		this.lblRefInvoice_9ce4f5c4_6cab_4c23_8a2b_e04b140c1d81 = null;
		this.txtRefInvc = null;
		this.txtRefInvc_64289281_a669_492d_822f_70661796133e = null;
		this.pbsRefunds = null;
		this.pbsRefunds_c5d96db4_cac2_4061_ad43_ce19ee7900b7 = null;
		this.grbBxRefunds = null;
		this.grbBxRefunds_8b058e9d_6531_4621_8e3b_118a64130c3e = null;
		this.btnRfCust = null;
		this.btnRfCust_86fb4f18_164e_4704_8153_66faab8f9488 = null;
		this.txtbxRfCust = null;
		this.txtbxRfCust_395757f7_7fde_43c3_82e9_4dedec73ab7b = null;
		this.lblRfCustName = null;
		this.lblRfCustName_1c4652dd_45f4_4441_a8ce_964dd2afff98 = null;
		this.lblRfDate = null;
		this.lblRfDate_ff73df12_cca8_4069_8f25_f24735f22493 = null;
		this.txtbxRfCustName = null;
		this.txtbxRfCustName_d8e70d8e_4cfe_4a50_8dc1_12a00703e782 = null;
		this.lblRfAmnt = null;
		this.lblRfAmnt_9ffa4e89_8d30_4aef_987a_3444e54b76cf = null;
		this.lblRfSelcAmnt = null;
		this.lblRfSelcAmnt_cac2ca59_a5f2_4b20_a2ba_8d16bcdeeb13 = null;
		this.btnRfSCrdNte = null;
		this.btnRfSCrdNte_efe8b6c8_7583_49e3_9e62_3675aa83c770 = null;
		this.btnRfCancel = null;
		this.btnRfCancel_8a06374d_811d_4952_ba68_44ce6f410561 = null;
		this.btnRfSubmit = null;
		this.btnRfSubmit_f5cc15f3_635a_4030_acd4_73af478a9fab = null;
		this.grdRefunds = null;
		this.grdRefunds_e256e64a_1a1f_48ce_a50c_0030aa0bd628 = null;
		this.epiDateTimeEditorRfDate = null;
		this.epiDateTimeEditorRfDate_04bc487b_339c_4769_91db_e3da38b497f8 = null;
		this.epiCurrencyEditorRfAmount = null;
		this.epiCurrencyEditorRfAmount_deaf2310_5bdd_48ac_8cc5_caf48ab49f80 = null;
		this.epiCurrencyEditorRfSelcAmnt = null;
		this.epiCurrencyEditorRfSelcAmnt_bdca2b3b_8051_4014_8176_d119155e8876 = null;
		this.csm = null;
		this.oTrans = null;
		this.UD10Form = null;
		this.baseToolbarsManager = null;
		this.UD10Attch_Column = null;
		this.AutoAttachUD10_Row = null;
		this.BpmData_Column = null;
		this.CallContextBpmData_Row = null;
		this.Client_Column = null;
		this.CallContextClientData_Row = null;
		this.UD10_Column = null;
		this.UD10_Row = null;
	}

	public static string[] GetTranslatableStrings()
	{
		return new string[] {
				"",
				"Negative Receipt",
				"Reallocate Cash Receipt",
				"Batch Number:",
				"Effective Date:",
				"Reference:",
				"Transcation Type:",
				"New Batch",
				"Customer ID: ",
				"Customer Name:",
				"Payment Date:",
				"Search Customer",
				"Payment List",
				"Submit",
				"Search Payment",
				"Negative Cash Entry",
				"Postive Cash Entry",
				"Customer ID:",
				"Adjustment Amount:",
				"Adjustment Amount: ",
				"Clear Screen",
				"Reallocate Payment List",
				"Ref Invoice:",
				"Refunds",
				"Refunds:",
				"Customer",
				"Customer Name",
				"Refund Date",
				"Refund Amount",
				"Selected Amount",
				"Select Credit Note",
				"Cancel",
				"CreditNotes",
				"Cash Entry"};
	}

	public static string GetStringByID(string id)
	{
		return "";
	}
	// ** Wizard Insert Location - Do Not Remove 'Begin/End Wizard Added Module Level Variables' Comments! **
	// Begin Wizard Added Module Level Variables **
	Infragistics.Win.UltraWinDock.UltraDockManager dock;
	const string BAQName = "pbsReverseCR";
    const string RefundBAQName = "PbsOpenTransactions";
	Dictionary<string, List<Tuple<string, string, Type>>> edvDic;
	DynamicQueryAdapter dynamicQueryAdapter;
	public EpiDataView edvCashEntry, edvReverseCashReceipt, edvReallocNegativeCashReceipt, edvReallocPositiveCashReceipt, edvPaymentList,edvReallocatePaymentList,edvRefunds,edvRefundsList;
    private string info=string.Empty;
	// End Wizard Added Module Level Variables **

	// Add Custom Module Level Variables Here **

	public void InitializeCustomCode()
	{
		// ** Wizard Insert Location - Do not delete 'Begin/End Wizard Added Variable Initialization' lines **
		// Begin Wizard Added Variable Initialization
		
		ModifyUD10FormLayout();
		InitEpiDataViewDic();
		CreateEpiDataView();		
		edvCashEntry = this.oTrans.Factory("CashEntry");
		edvReverseCashReceipt = this.oTrans.Factory("ReverseCashReceipt");
		edvReallocNegativeCashReceipt = this.oTrans.Factory("ReallocNegativeCashReceipt");
		edvReallocPositiveCashReceipt = this.oTrans.Factory("ReallocPositiveCashReceipt");
		edvPaymentList = this.oTrans.Factory("PaymentList");
        edvReallocatePaymentList = this.oTrans.Factory("ReallocatePaymentList");
        edvRefunds = this.oTrans.Factory("Refunds");
        edvRefundsList = this.oTrans.Factory("RefundsList");

		
		// End Wizard Added Variable Initialization

		// Begin Wizard Added Custom Method Calls
		
		this.btnNewBatch.Click += new System.EventHandler(this.btnNewBatch_Click);
		SetExtendedProperties();
		this.cTT.ValueChanged += new System.EventHandler(this.cTT_ValueChanged);
		this.btnSearchCustomerReverseCR.Click += new System.EventHandler(this.btnSearchCustomerReverseCR_Click);
		this.btnSearchPayment.Click += new System.EventHandler(this.btnSearchPayment_Click);
        this.btnSearchPaymentNCE.Click += new System.EventHandler(this.btnSearchPaymentNCE_Click);
		this.btnSubmitReverseCR.Click += new System.EventHandler(this.btnSubmitReverseCR_Click);
		this.btnSubmitReallocCR.Click += new System.EventHandler(this.btnSubmitReallocCR_Click);
		this.txtCustIDReverseCR.AfterExitEditMode += new System.EventHandler(this.txtCustIDReverseCR_AfterExitEditMode);
		this.btnSearchCustomerNCE.Click += new System.EventHandler(this.btnSearchCustomerNCE_Click);
		this.btnSearchCustomerPCE.Click += new System.EventHandler(this.btnSearchCustomerPCE_Click);
		this.txtCustIDNCE.AfterExitEditMode += new System.EventHandler(this.txtCustIDNCE_AfterExitEditMode);
		this.txtCustIDPCE.AfterExitEditMode += new System.EventHandler(this.txtCustIDPCE_AfterExitEditMode);
        this.txtbxRfCust.AfterExitEditMode += new System.EventHandler(this.txtbxRfCust_AfterExitEditMode);
		this.edvReverseCashReceipt.EpiViewNotification += new EpiViewNotification(this.edvReverseCashReceipt_EpiViewNotification);
		this.edvReallocNegativeCashReceipt.EpiViewNotification += new EpiViewNotification(this.edvReallocNegativeCashReceipt_EpiViewNotification);
		this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
        this.ugPaymentGridReallocateNCE.CellChange += new Infragistics.Win.UltraWinGrid.CellEventHandler(this.ugPaymentGridReallocateNCE_CellChange);
        this.grdRefunds.CellChange += new Infragistics.Win.UltraWinGrid.CellEventHandler(this.grdRefunds_CellChange);
		this.btnRfCust.Click += new System.EventHandler(this.btnRfCust_Click);
		this.btnRfSCrdNte.Click += new System.EventHandler(this.btnRfSCrdNte_Click);
		this.btnRfSubmit.Click += new System.EventHandler(this.btnRfSubmit_Click);
		this.btnRfCancel.Click += new System.EventHandler(this.btnRfCancel_Click);
		// End Wizard Added Custom Method Calls
	}

	public void DestroyCustomCode()
	{
		// ** Wizard Insert Location - Do not delete 'Begin/End Wizard Added Object Disposal' lines **
		// Begin Wizard Added Object Disposal

		this.btnNewBatch.Click -= new System.EventHandler(this.btnNewBatch_Click);
		this.cTT.ValueChanged -= new System.EventHandler(this.cTT_ValueChanged);
		this.btnSearchCustomerReverseCR.Click -= new System.EventHandler(this.btnSearchCustomerReverseCR_Click);
		this.btnSearchPayment.Click -= new System.EventHandler(this.btnSearchPayment_Click);
        this.btnSearchPaymentNCE.Click -= new System.EventHandler(this.btnSearchPaymentNCE_Click);
		this.btnSubmitReverseCR.Click -= new System.EventHandler(this.btnSubmitReverseCR_Click);
		this.btnSubmitReallocCR.Click -= new System.EventHandler(this.btnSubmitReallocCR_Click);
		this.txtCustIDReverseCR.AfterExitEditMode -= new System.EventHandler(this.txtCustIDReverseCR_AfterExitEditMode);
		this.btnSearchCustomerNCE.Click -= new System.EventHandler(this.btnSearchCustomerNCE_Click);
		this.btnSearchCustomerPCE.Click -= new System.EventHandler(this.btnSearchCustomerPCE_Click);
		this.txtCustIDNCE.AfterExitEditMode -= new System.EventHandler(this.txtCustIDNCE_AfterExitEditMode);
		this.txtCustIDPCE.AfterExitEditMode -= new System.EventHandler(this.txtCustIDPCE_AfterExitEditMode);
        this.txtbxRfCust.AfterExitEditMode -= new System.EventHandler(this.txtbxRfCust_AfterExitEditMode);
		this.edvReverseCashReceipt.EpiViewNotification -= new EpiViewNotification(this.edvReverseCashReceipt_EpiViewNotification);
		this.edvReverseCashReceipt = null;
		this.edvReallocNegativeCashReceipt.EpiViewNotification -= new EpiViewNotification(this.edvReallocNegativeCashReceipt_EpiViewNotification);
		this.edvReallocNegativeCashReceipt = null;
		this.btnClear.Click -= new System.EventHandler(this.btnClear_Click);
        this.ugPaymentGridReallocateNCE.CellChange -= new Infragistics.Win.UltraWinGrid.CellEventHandler(this.ugPaymentGridReallocateNCE_CellChange);
        this.grdRefunds.CellChange -= new Infragistics.Win.UltraWinGrid.CellEventHandler(this.grdRefunds_CellChange);
		this.btnRfCust.Click -= new System.EventHandler(this.btnRfCust_Click);
		this.btnRfSCrdNte.Click -= new System.EventHandler(this.btnRfSCrdNte_Click);
		this.btnRfSubmit.Click -= new System.EventHandler(this.btnRfSubmit_Click);
		this.btnRfCancel.Click -= new System.EventHandler(this.btnRfCancel_Click);
		// End Wizard Added Object Disposal

		// Begin Custom Code Disposal

		// End Custom Code Disposal
	}
	
	#region FormLayout
	private void ModifyUD10FormLayout()
	{
		baseToolbarsManager.Visible = false;
		Object obj = typeof(Ice.UI.App.UD10Entry.UD10Form).InvokeMember("baseDockManager", BindingFlags.Instance | BindingFlags.GetField | BindingFlags.NonPublic, null, UD10Form, null);
        dock =  (Infragistics.Win.UltraWinDock.UltraDockManager)obj;                  
        dock.DockAreas[0].Panes[0].Closed = true;
	}
	#endregion FormLayout

	#region Form Events

	private void UD10Form_Load(object sender, EventArgs args)
	{
		this.ugPaymentGridReverseCR.DisplayLayout.AutoFitStyle = AutoFitStyle.ResizeAllColumns;
        this.ugPaymentGridReallocateNCE.DisplayLayout.AutoFitStyle = AutoFitStyle.ResizeAllColumns;
		this.dteED.MaxDate = DateTime.Today;
		this.dtePaymentDate.MaxDate = DateTime.Today;
        this.epiDateTimeEditorRfDate.MaxDate = DateTime.Today;
		this.neAdjAmtNCE.MaxValue = 0.00;
		this.neAdjAmtPCE.MinValue = 0.00;
        FormatRefundGrid();

		ToggleBtnState(this.btnSearchCustomerReverseCR, true);
		ToggleBtnState(this.btnSearchPayment, true);
		ToggleBtnState(this.btnSubmitReverseCR, true);
		ToggleBtnState(this.btnSearchCustomerNCE, true);
		ToggleBtnState(this.btnSearchCustomerPCE, true);
		ToggleBtnState(this.btnSubmitReallocCR, true);
        ToggleBtnState(this.btnSearchPaymentNCE,true);
	}
	
	#endregion From Events

	#region Custom CashEntry Layout
	
	private void InitEpiDataViewDic()
	{ 
		edvDic = new Dictionary<string, List<Tuple<string, string, Type>>>()
		{
			{
				"CashEntry", new List<Tuple<string, string, Type>>(){

																		new Tuple<string, string, Type>("BatchNumber", "Batch Number", System.Type.GetType("System.String")),
																		new Tuple<string, string, Type>("EffectiveDate", "Effective Date", System.Type.GetType("System.DateTime")),
																		new Tuple<string, string, Type>("Reference", "Reference", System.Type.GetType("System.String")),
                                                                        new Tuple<string, string, Type>("RefInvoice", "RefInvoice", System.Type.GetType("System.String")),
																		new Tuple<string, string, Type>("TransactionType", "Transaction Type", System.Type.GetType("System.Int32"))
																	}
			},

			{
				"ReverseCashReceipt" , new List<Tuple<string, string, Type>>(){

																		new Tuple<string, string, Type>("CustID", "CustID", System.Type.GetType("System.String")),
																		new Tuple<string, string, Type>("Name", "Name", System.Type.GetType("System.String")),
																		new Tuple<string, string, Type>("PaymentDate", "Payment Date", System.Type.GetType("System.DateTime"))
																	}
			},

			{
				"ReallocNegativeCashReceipt" , new List<Tuple<string, string, Type>>(){

																		new Tuple<string, string, Type>("CustID", "CustID", System.Type.GetType("System.String")),
																		new Tuple<string, string, Type>("Name", "Name", System.Type.GetType("System.String")),
                                                                        new Tuple<string, string, Type>("PaymentDate", "Payment Date", System.Type.GetType("System.DateTime")),
																		new Tuple<string, string, Type>("AdjAmt", "Adjustment Amount", System.Type.GetType("System.Decimal"))
                                                                        
																	}
			},

			{
				"ReallocPositiveCashReceipt" , new List<Tuple<string, string, Type>>(){

																		new Tuple<string, string, Type>("CustID", "CustID", System.Type.GetType("System.String")),
																		new Tuple<string, string, Type>("Name", "Name", System.Type.GetType("System.String")),
																		new Tuple<string, string, Type>("AdjAmt", "Adjustment Amount", System.Type.GetType("System.Decimal"))
																	}
			},
			{
				"PaymentList", ReturnGridColList(GetBAQDesignData(BAQName))
			},
            {
              "ReallocatePaymentList", ReturnGridColList(GetBAQDesignData(BAQName))
            },
            
            {
              "Refunds" , new List<Tuple<string, string, Type>>(){

																		new Tuple<string, string, Type>("CustID", "CustID", System.Type.GetType("System.String")),
																		new Tuple<string, string, Type>("Name", "Name", System.Type.GetType("System.String")),
																		new Tuple<string, string, Type>("RefundDate", "Refund Date", System.Type.GetType("System.DateTime")),
                                                                        new Tuple<string, string, Type>("RefundAmount", "Refund Amount", System.Type.GetType("System.Decimal")) ,
                                                                        new Tuple<string, string, Type>("RefundSelectedAmount", "Selected Amount", System.Type.GetType("System.Decimal")), 
                                                                        new Tuple<string, string, Type>("btnRfCust", "btnCustomer", System.Type.GetType("System.String")),
                                                                        new Tuple<string, string, Type>("btnRfSubmit", "btnSelectCreditNote", System.Type.GetType("System.String")),
                                                                        new Tuple<string, string, Type>("btnRfCancel", "btnSelectCreditNote", System.Type.GetType("System.String")),
                                                                        new Tuple<string, string, Type>("btnRfSCrdNte", "Select Credit Note", System.Type.GetType("System.String")) 
																	}
            },
            {
              "RefundsList", ReturnGridColList(GetBAQDesignData(RefundBAQName)) 
            }
            
            
		};
	}
	
	private void CreateEpiDataView()
	{
		foreach(KeyValuePair<string, List<Tuple<string, string, Type>>> item in edvDic)
		{
			EpiDataView edv = new EpiDataView();
			DataTable dt = CreateTable(item.Value);
			edv.dataView = dt.DefaultView;
			this.oTrans.Add(item.Key, edv);
		}
	}
	
	private DataTable CreateTable(List<Tuple<string, string, Type>> colList)
	{
		DataTable dt = new DataTable();
		foreach(var item in colList)
		{
			DataColumn dc = dt.Columns.Add(item.Item1, item.Item3);
			dc.Caption = item.Item2;
		}
		dt.Columns.Add("SysRowID", System.Type.GetType("System.Guid"));
		Guid guid = Guid.NewGuid();
		dt.Columns["SysRowID"].DefaultValue = guid;
		dt.Columns.Add("SysRevID", System.Type.GetType("System.String") );
		dt.Columns["SysRevID"].DefaultValue = guid.ToString();
		
		return dt;
	}

	private List<Tuple<string, string, Type>> ReturnGridColList(DataTable dt)
	{
		List<Tuple<string, string, Type>> colList = new List<Tuple<string, string, Type>>();
		foreach(DataRow row in dt.Rows)
		{
			Type type = null; 
			if(row["DataType"].ToString().Equals("bit"))
				type = System.Type.GetType("System.Boolean");
			else if(row["DataType"].ToString().Equals("decimal"))
				type = System.Type.GetType("System.Decimal");
			else if(row["DataType"].ToString().Equals("int"))
				type = System.Type.GetType("System.Int32");
			else if(row["DataType"].ToString().Equals("date") || row["DataType"].ToString().Equals("datetime"))
				type = System.Type.GetType("System.DateTime");
			else				
				type = System.Type.GetType("System.String");

			colList.Add(
						new Tuple<string, string, Type>(row["DisplayName"].ToString(), row["FieldLabel"].ToString(), type)
						);
		}

		return colList;
	}

	#endregion Custom CashEntry Layout

	#region Common Functions
	
	private DataTable GetBAQDesignData(string BAQName)
	{
		ConnectDynamicQueryAdapter();
		DataSet ds = dynamicQueryAdapter.GetQueryDesignData(BAQName);
		DisposeDynamicQueryAdapter();
		return ds.Tables["QueryField"];
	}
	
	private void ConnectDynamicQueryAdapter()
	{
		dynamicQueryAdapter = new DynamicQueryAdapter(this.oTrans);
		dynamicQueryAdapter.BOConnect();
	}

	private void DisposeDynamicQueryAdapter()
	{
		dynamicQueryAdapter.Dispose();
	}
	
	private void ClearEpiDataView(EpiDataView edv)
	{
		edv.dataView.Table.Rows.Clear();
		edv.Notify(new EpiNotifyArgs(oTrans, edv.dataView.Count-1, EpiTransaction.NotifyType.DeleteRow));
	}

	private void ClearCashEntryScreen()
	{
		foreach(DictionaryEntry edv in this.oTrans.EpiDataViews)
		{
			EpiDataView epiDataView = (EpiDataView)edv.Value;
			if(!epiDataView.ViewName.Equals("CallContextBpmData"))
			{
				epiDataView.dataView.Table.Rows.Clear();
				epiDataView.Notify(new EpiNotifyArgs(oTrans, epiDataView.dataView.Count-1, EpiTransaction.NotifyType.DeleteRow));
			}
		}
	}

	private void SetExtendedProperties()
	{
		// Begin Wizard Added EpiDataView Initialization
		// End Wizard Added EpiDataView Initialization

		// Begin Wizard Added Conditional Block
		if (edvCashEntry.dataView.Table.Columns.Contains("BatchNumber"))
			edvCashEntry.dataView.Table.Columns["BatchNumber"].ExtendedProperties["ReadOnly"] = true;
	
		if (edvReverseCashReceipt.dataView.Table.Columns.Contains("Name"))
			edvReverseCashReceipt.dataView.Table.Columns["Name"].ExtendedProperties["ReadOnly"] = true;

		if (edvReallocNegativeCashReceipt.dataView.Table.Columns.Contains("Name"))
			edvReallocNegativeCashReceipt.dataView.Table.Columns["Name"].ExtendedProperties["ReadOnly"] = true;

		if (edvReallocPositiveCashReceipt.dataView.Table.Columns.Contains("Name"))
			edvReallocPositiveCashReceipt.dataView.Table.Columns["Name"].ExtendedProperties["ReadOnly"] = true;
        
        if (edvRefunds.dataView.Table.Columns.Contains("Name"))
			edvRefunds.dataView.Table.Columns["Name"].ExtendedProperties["ReadOnly"] = true;
        
        if (edvRefunds.dataView.Table.Columns.Contains("RefundSelectedAmount"))
			edvRefunds.dataView.Table.Columns["RefundSelectedAmount"].ExtendedProperties["ReadOnly"] = true;
        
        foreach(DataColumn col in edvRefundsList.dataView.Table.Columns)
        {
          if(!col.ColumnName.Equals("Calculated_Selected") && !col.ColumnName.Equals("Calculated_ApplyAmount"))
          {
           col.ExtendedProperties["ReadOnly"] = true;
          }
        }
		// End Wizard Added Conditional Block
	}

	private DataRow CustomerSearch(string custID)
	{
		using(this.oTrans.PushDisposableStatusText("Searching Customer Details...", true))
		{
			Ice.Proxy.Lib.BOReaderImpl _bor = WCFServiceSupport.CreateImpl<Ice.Proxy.Lib.BOReaderImpl>((Ice.Core.Session)this.oTrans.Session, Epicor.ServiceModel.Channels.ImplBase<Ice.Contracts.BOReaderSvcContract>.UriPath);
			string whereClause = string.Format("CustID = '{0}'", custID);
			DataSet dsCustomAdapter = _bor.GetRows("Erp:BO:Customer", whereClause, "CustID,CustNum,Name");
			if(dsCustomAdapter.Tables[0].Rows.Count > 0)
			{
				return dsCustomAdapter.Tables[0].Rows[0];
			}
		}
		return null;
		
	}

	private DataTable RunBAQ(QueryExecutionDataSet dqds,string BAQName)	
	{
		bool more;
		ConnectDynamicQueryAdapter();
		var ds = dynamicQueryAdapter.GetList(BAQName, dqds, 0, 0, out more);
		DisposeDynamicQueryAdapter();

		return ds.Tables["Results"];
		
	}

	private QueryExecutionDataSet GetQueryExecutionDS(string BAQName)
	{
		ConnectDynamicQueryAdapter();
		var dqds = dynamicQueryAdapter.GetQueryExecutionParametersByID(BAQName);	
		DisposeDynamicQueryAdapter();

		return dqds;
	}

	private void Mapper(EpiDataView edv, DataTable dt, List<Tuple<string, string, Type>> colList)
	{
		foreach(DataRow row in dt.Rows)
		{
			var newRow = edv.dataView.Table.NewRow();
			newRow.BeginEdit();
			foreach(var item in colList)
			{
				newRow[item.Item1] = row[item.Item1];
			}
			newRow.EndEdit();
			edv.dataView.Table.Rows.Add(newRow);
		}
		edv.Notify(new EpiNotifyArgs(oTrans, edv.Row, edv.Column));
	}

	private DataRow SearchOnCustomerAdapterShowDialog()
	{
		bool recSelected;
		string whereClause = string.Empty;
		System.Data.DataSet dsCustomerAdapter = Ice.UI.FormFunctions.SearchFunctions.listLookup(this.oTrans, "CustomerAdapter", out recSelected, true, whereClause);
		if (recSelected)
		{
			System.Data.DataRow adapterRow = dsCustomerAdapter.Tables[0].Rows[0];

			return adapterRow;
		}
		return null;
	}

	private void ThrowCustomerNotFoundException()
	{
		throw new Ice.BLException("Invalid Customer ID");
	}

	private void ToggleBtnState(EpiButton btn, bool val)
	{
		btn.ReadOnly = val;
	}
	
	#endregion Common Functions

	#region CashEntry Functions
	
	private void btnNewBatch_Click(object sender, System.EventArgs args)
	{
		// ** Place Event Handling Code Here **
		GetNewBatch();
	}
	
	private void GetNewBatch()
	{
		if(edvCashEntry.dataView.Table.Rows.Count == 0)
		{
			DataRow newRow = edvCashEntry.dataView.Table.NewRow();
			EpiDataView edvCallContextBpmData = (EpiDataView)(oTrans.EpiDataViews["CallContextBpmData"]);
			edvCallContextBpmData.dataView[edvCallContextBpmData.Row]["Character01"] = "AutoGen";
			UD10Adapter ud10Adapter  = new UD10Adapter(this.oTrans);
			ud10Adapter.BOConnect();
			ud10Adapter.GetaNewUD10();
			ud10Adapter.Dispose();
	
			newRow.BeginEdit();
			newRow["BatchNumber"] = edvCallContextBpmData.dataView[edvCallContextBpmData.Row]["Character01"].ToString();
			newRow.EndEdit();
			edvCashEntry.dataView.Table.Rows.Add(newRow);
			edvCashEntry.Notify(new EpiNotifyArgs(this.oTrans, edvCashEntry.dataView.Count - 1, EpiTransaction.NotifyType.AddRow));
	
			edvCallContextBpmData.dataView[edvCallContextBpmData.Row]["Character01"] = string.Empty;
		}	
	}

	private void cTT_ValueChanged(object sender, System.EventArgs args)
	{
		// ** Place Event Handling Code Here **
		int value = Convert.ToInt32(this.cTT.Value);
		if(value == 1)
		{
			InitReverseCRView();
			this.txtCustIDReverseCR.Focus();
			ClearEpiDataView(edvReallocNegativeCashReceipt);
			ClearEpiDataView(edvReallocPositiveCashReceipt);
            ClearEpiDataView(edvRefunds);
            ClearEpiDataView(edvRefundsList);
		}
		else if(value==2)
		{
			InitReallocCRView();
			this.txtCustIDNCE.Focus();
			ClearEpiDataView(edvReverseCashReceipt);
			ClearEpiDataView(edvPaymentList);
            ClearEpiDataView(edvRefunds);
            ClearEpiDataView(edvRefundsList);
		}
        else if(value==3)
        {
            InitRefundsView();
			this.txtbxRfCust.Focus();
			ClearEpiDataView(edvReverseCashReceipt);
			ClearEpiDataView(edvPaymentList);
            ClearEpiDataView(edvReallocNegativeCashReceipt);
			ClearEpiDataView(edvReallocPositiveCashReceipt);
        }
		
	}
	
	private void btnClear_Click(object sender, System.EventArgs args)
	{
		ClearCashEntryScreen();
	}	

	#endregion CashEntry Functions 

	#region Reverse Cash Receipt Functions
	
	private void edvReverseCashReceipt_EpiViewNotification(EpiDataView view, EpiNotifyArgs args)
	{
		// ** Argument Properties and Uses **
		// view.dataView[args.Row]["FieldName"]
		// args.Row, args.Column, args.Sender, args.NotifyType
		// NotifyType.Initialize, NotifyType.AddRow, NotifyType.DeleteRow, NotifyType.InitLastView, NotifyType.InitAndResetTreeNodes
		if ((args.NotifyType == EpiTransaction.NotifyType.AddRow))
		{
			if ((args.Row > -1))
			{
				ToggleBtnState(this.btnSearchCustomerReverseCR, false);
				ToggleBtnState(this.btnSearchPayment, false);
				ToggleBtnState(this.btnSubmitReverseCR, false);
			}
		}
		if ((args.NotifyType == EpiTransaction.NotifyType.DeleteRow))
		{
			ToggleBtnState(this.btnSearchCustomerReverseCR, true);
			ToggleBtnState(this.btnSearchPayment, true);
			ToggleBtnState(this.btnSubmitReverseCR, true);
		
		}
	}

	private void InitReverseCRView()
	{
		DataRow newRow = edvReverseCashReceipt.dataView.Table.NewRow();
		
		edvReverseCashReceipt.dataView.Table.Rows.Add(newRow);
		edvReverseCashReceipt.Notify(new EpiNotifyArgs(this.oTrans, edvReverseCashReceipt.dataView.Count - 1, EpiTransaction.NotifyType.AddRow));
	}
	
	private void btnSearchCustomerReverseCR_Click(object sender, System.EventArgs args)
	{
		DataRow custRow = SearchOnCustomerAdapterShowDialog();
		if(custRow != null)
		{
			SetCustomerDetail(custRow, edvReverseCashReceipt);
		}
	}

	private void txtCustIDReverseCR_AfterExitEditMode(object sender, System.EventArgs args)
	{
		string custID = this.txtCustIDReverseCR.Text;
		if(!string.IsNullOrWhiteSpace(custID))
		{
			DataRow custRow = CustomerSearch(custID);
			if(custRow != null)
			{
				SetCustomerDetail(custRow, edvReverseCashReceipt);
			}else
			{
				ThrowCustomerNotFoundException();
			}
		}
	}

	private void SetCustomerDetail(DataRow custRow, EpiDataView edv)
	{
		DataRow row = edv.CurrentDataRow;
		row["CustID"] = custRow["CustID"];
		row["Name"] = custRow["Name"];
		edv.Notify(new EpiNotifyArgs(oTrans, edv.Row, edv.Column));
	}
	
	private void btnSearchPayment_Click(object sender, System.EventArgs args)
	{
		ClearEpiDataView(edvPaymentList);
		SearchPayments(edvReverseCashReceipt,"PaymentList",edvPaymentList);
	}
    private void btnSearchPaymentNCE_Click(object sender, System.EventArgs args)
    {
      ClearEpiDataView(edvReallocatePaymentList); 
      SearchPayments(edvReallocNegativeCashReceipt,"ReallocatePaymentList",edvReallocatePaymentList);
    }

	private void SearchPayments(EpiDataView edv, string ViewName,EpiDataView listView)
	{
		DataRow row = edv.dataView.Table.Rows[0];
		string custID = row["CustID"].ToString();
		DateTime paymentDate = Convert.ToDateTime(row["PaymentDate"]);
		if(string.IsNullOrWhiteSpace(custID))
			throw new Ice.BLException("Please enter Customer ID");
		if(string.IsNullOrWhiteSpace(paymentDate.ToString()))
			throw new Ice.BLException("Please enter Payment Date");
		QueryExecutionDataSet dqds = GetQueryExecutionDS(BAQName);
		dqds.Tables["ExecutionParameter"].Clear();
		dqds.Tables["ExecutionParameter"].Rows.Add(SetExecutionParameter(dqds.Tables["ExecutionParameter"], "CustID", custID, "nvarchar"));
		dqds.Tables["ExecutionParameter"].Rows.Add(SetExecutionParameter(dqds.Tables["ExecutionParameter"], "TranDate", paymentDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture), "Date"));
 
		DataTable dt = RunBAQ(dqds, BAQName);
		Mapper(listView, dt, edvDic[ViewName]);	
	}

	private DataRow SetExecutionParameter(DataTable dt, string paramID, string value, string dataType)
	{

		DataRow newRow = dt.NewRow();
		newRow["ParameterID"] = paramID;
		newRow["ParameterValue"] = value;
		newRow["ValueType"] = dataType;
		newRow["IsEmpty"] = false;
		newRow["SysRowID"] = Guid.NewGuid();
		newRow["RowMod"] = "A";
		return newRow;
	}	

	private void btnSubmitReverseCR_Click(object sender, System.EventArgs args)
	{
		EnumerableRowCollection collection = this.edvPaymentList.dataView.Table.AsEnumerable().Where(x => Convert.ToBoolean(x["Calculated_Select"]) == true);
		ReverseCR(collection);
	}

	private void IsValidCashEntryHead()
    {
      DataRow reCashEntry = edvCashEntry.CurrentDataRow;
      if(string.IsNullOrEmpty(reCashEntry["EffectiveDate"].ToString()))
			throw new Ice.BLException("Effective Date cannot be null or empty!");
      if(string.IsNullOrEmpty(reCashEntry["Reference"].ToString()))
			throw new Ice.BLException("Reference cannot be null or empty!");
    }

    private void miscInfo()
    {
      DataRow cashEntry = edvCashEntry.CurrentDataRow;
      DataRow reverseCashReceipt        = edvReverseCashReceipt.CurrentDataRow;
	  EpiDataView edvCallContextBpmData = (EpiDataView)(oTrans.EpiDataViews["CallContextBpmData"]);
	  edvCallContextBpmData.dataView[edvCallContextBpmData.Row]["Character01"] = reverseCashReceipt["CustID"];
      edvCallContextBpmData.dataView[edvCallContextBpmData.Row]["Character02"] = cashEntry["Reference"];	
      edvCallContextBpmData.dataView[edvCallContextBpmData.Row]["Character03"] = cashEntry["RefInvoice"];		
    }

	private void ReverseCR(EnumerableRowCollection collection)
	{
        using(this.oTrans.PushDisposableStatusText("Validating Input. Please wait...", true))
		{
            IsValidCashEntryHead();
		}
		using(this.oTrans.PushDisposableStatusText("Processing Cash Reversal. Please wait!!!", true))
		{
			DataRow cashEntryRow = edvCashEntry.CurrentDataRow;		
			ConnectDynamicQueryAdapter();
			DataSet ds = dynamicQueryAdapter.GetQueryEmptyResultSet(BAQName);
			ds.Tables["Results"].Clear();
			foreach(DataRow row in collection)
			{
				Guid guid = Guid.NewGuid();
				var newRow = ds.Tables["Results"].NewRow();
				newRow.BeginEdit();
                newRow["Calculated_BatchID"]=edvCashEntry.dataView[edvCashEntry.Row]["BatchNumber"];
				newRow["CashHead_GroupID"] = row["CashHead_GroupID"];
				newRow["CashHead_HeadNum"] = row["CashHead_HeadNum"];
				newRow["CashHead_ReversedReason"] = cashEntryRow["Reference"];
				newRow["CashHead_ReverseDate"] = cashEntryRow["EffectiveDate"];
                newRow["CashHead_DocTranAmt"]=row["CashHead_DocTranAmt"];
				newRow["RowIdent"] = guid.ToString();
				newRow["RowMod"] = "U";
				newRow["SysRowID"] = guid;
				newRow.EndEdit();
				ds.Tables["Results"].Rows.Add(newRow);
			}
            miscInfo();
			dynamicQueryAdapter.Update(BAQName, ds);
			DisposeDynamicQueryAdapter();
		}
		ClearCashEntryScreen();
	}	

	#endregion Reverse Cash Receipt Functions

	#region Realloc Cash Receipt

	private void InitReallocCRView()
	{
		DataRow newRow = edvReallocNegativeCashReceipt.dataView.Table.NewRow();
		DataRow newRowPositiveCR = edvReallocPositiveCashReceipt.dataView.Table.NewRow();

		newRow.BeginEdit();
		newRow["AdjAmt"] = 0.00;
		newRow.EndEdit();

		newRowPositiveCR.BeginEdit();
		newRowPositiveCR["AdjAmt"] = 0.00;
		newRowPositiveCR.EndEdit();

		edvReallocNegativeCashReceipt.dataView.Table.Rows.Add(newRow);

		edvReallocNegativeCashReceipt.Notify(new EpiNotifyArgs(this.oTrans, edvReallocNegativeCashReceipt.dataView.Count - 1, EpiTransaction.NotifyType.AddRow));

		edvReallocPositiveCashReceipt.dataView.Table.Rows.Add(newRowPositiveCR);
		edvReallocPositiveCashReceipt.Notify(new EpiNotifyArgs(this.oTrans, edvReallocPositiveCashReceipt.dataView.Count - 1, EpiTransaction.NotifyType.AddRow));
	}	

	private void IsValidInput()
	{
		DataRow reAllocNCR = edvReallocNegativeCashReceipt.CurrentDataRow;
		DataRow reAllocPCR = edvReallocPositiveCashReceipt.CurrentDataRow;
        DataRow reCashEntry = edvCashEntry.CurrentDataRow;
		
		if(reAllocNCR["CustID"].Equals(reAllocPCR["CustID"]))
			throw new Ice.BLException("Please select distinct customers.");
		if(Convert.ToDecimal(reAllocNCR["AdjAmt"]) == 0 || Convert.ToDecimal(reAllocPCR["AdjAmt"]) == 0)
			throw new Ice.BLException("Adjustment Amount equals to zero.");
		if((Convert.ToDecimal(reAllocNCR["AdjAmt"])+ Convert.ToDecimal(reAllocPCR["AdjAmt"])) != 0)
			throw new Ice.BLException("Sum of Negative adjustment amount and postive adjustment amount is not equal to zero.");
        

	}

  /*  private void UpdateCashHead(string batchNumber="")
    {
        CashRecAdapter cashRecAdapter  = new CashRecAdapter(this.oTrans);
	    cashRecAdapter.BOConnect();
      try
       {
	      foreach(UltraGridRow row in ugPaymentGridReallocateNCE.Rows.AsEnumerable().Where(x => Convert.ToBoolean(x.Cells["Calculated_Select"].Text) == true))
	       {
	            cashRecAdapter.GetByID(row.Cells["CashHead_GroupID"].Value.ToString(),Convert.ToInt32(row.Cells["CashHead_HeadNum"].Value));
	            if(cashRecAdapter.CashRecData.CashHead.Count>0)
	            {
	              string ipGroupID = cashRecAdapter.CashRecData.CashHead[cashRecAdapter.CashRecData.CashHead.Count-1]["GroupID"].ToString(),ipTableName="CashHead";
	              cashRecAdapter.CashRecData.CashHead[cashRecAdapter.CashRecData.CashHead.Count-1]["PbsReallocOrNegReceipt_c"] = "ReallocRcpt";
	              cashRecAdapter.CashRecData.CashHead[cashRecAdapter.CashRecData.CashHead.Count-1]["PbsRelatedDoc_c"] = batchNumber;
	              cashRecAdapter.CashRecData.CashHead[cashRecAdapter.CashRecData.CashHead.Count-1]["RowMod"] = "U";
	              Boolean updGroupTotals = false,opUpdateRan=false;
	              decimal opTotalCashReceived=0m, opTotalApplied=0m, opUnappliedBalance=0m, opTotalMisc=0m, opTotalDiscount=0m, opTotalDeposit=0m, opTotalARAmount=0m, opTotalWithhold=0m, opTotalWriteOff=0m;
	              int ipIgnoreValidation=0;
	              Boolean update = cashRecAdapter.UpdateMaster(ipGroupID, ipTableName, updGroupTotals, out opTotalCashReceived, out opTotalApplied, out opUnappliedBalance, out opTotalMisc, out opTotalDiscount, out opTotalDeposit, out opTotalARAmount, out opTotalWithhold, out opTotalWriteOff, out opUpdateRan, ipIgnoreValidation);   
	            }
	            
	       } 
       }catch (Exception ex)
        {
            throw new Ice.BLException(ex.Message);
        }
        finally
        {
           cashRecAdapter.Dispose();
        }  
    }*/
    
     private string GetGroupIDAndHeadNum()
    {
      string value = string.Empty;
      try
       {
	      foreach(UltraGridRow row in ugPaymentGridReallocateNCE.Rows.AsEnumerable().Where(x => Convert.ToBoolean(x.Cells["Calculated_Select"].Text) == true))
	       {
	            value+=row.Cells["CashHead_GroupID"].Value.ToString()+","+row.Cells["CashHead_HeadNum"].Value.ToString()+"~";
	            
	       } 
           if(!string.IsNullOrEmpty(value))
           {
              value.Remove(value.Length-1);
           }
           return value;  
       }catch (Exception ex)
        {
            throw new Ice.BLException(ex.Message);
        }
      
    }


	private void btnSubmitReallocCR_Click(object sender, System.EventArgs args)
	{
		using(this.oTrans.PushDisposableStatusText("Validating Input. Please wait...", true))
		{
			IsValidInput();
            IsValidCashEntryHead();
		}
		using(this.oTrans.PushDisposableStatusText("Reallocating Cash Receipt. Please wait...", true))
		{
			DataRow reAllocNCR = edvReallocNegativeCashReceipt.CurrentDataRow;
			DataRow reAllocPCR = edvReallocPositiveCashReceipt.CurrentDataRow;
			DataRow cashEntry = edvCashEntry.CurrentDataRow;
			EpiDataView edvCallContextBpmData = (EpiDataView)(oTrans.EpiDataViews["CallContextBpmData"]);
			edvCallContextBpmData.dataView[edvCallContextBpmData.Row]["Character01"] = "ReallocCR";
            edvCallContextBpmData.dataView[edvCallContextBpmData.Row]["Character06"] = cashEntry["RefInvoice"];
            edvCallContextBpmData.dataView[edvCallContextBpmData.Row]["Character07"] = cashEntry["Reference"];
			edvCallContextBpmData.dataView[edvCallContextBpmData.Row]["Character08"] = cashEntry["BatchNumber"];
			edvCallContextBpmData.dataView[edvCallContextBpmData.Row]["Character09"] = reAllocNCR["CustID"]; 
			edvCallContextBpmData.dataView[edvCallContextBpmData.Row]["Character10"] = reAllocPCR["CustID"];
			edvCallContextBpmData.dataView[edvCallContextBpmData.Row]["Number09"] = reAllocNCR["AdjAmt"];
			edvCallContextBpmData.dataView[edvCallContextBpmData.Row]["Number10"] = reAllocPCR["AdjAmt"];
	        edvCallContextBpmData.dataView[edvCallContextBpmData.Row]["Character05"]=GetGroupIDAndHeadNum();
			UD10Adapter ud10Adapter  = new UD10Adapter(this.oTrans);
			ud10Adapter.BOConnect();
			ud10Adapter.GetaNewUD10();
			ud10Adapter.Dispose();
           // UpdateCashHead(cashEntry["BatchNumber"].ToString());
		}
		ClearCashEntryScreen();
	}

	private void btnSearchCustomerNCE_Click(object sender, System.EventArgs args)
	{
		DataRow custRow = SearchOnCustomerAdapterShowDialog();
		if(custRow != null)
		{
			SetCustomerDetail(custRow, edvReallocNegativeCashReceipt);
		}
	}

	private void btnSearchCustomerPCE_Click(object sender, System.EventArgs args)
	{
		DataRow custRow = SearchOnCustomerAdapterShowDialog();
		if(custRow != null)
		{
			SetCustomerDetail(custRow, edvReallocPositiveCashReceipt);
		}
	}

	private void txtCustIDNCE_AfterExitEditMode(object sender, System.EventArgs args)
	{
		string custID = this.txtCustIDNCE.Text;
		if(!string.IsNullOrWhiteSpace(custID))
		{
			DataRow custRow = CustomerSearch(custID);
			if(custRow != null)
			{
				//if(CheckIfCustomerHasUR(Convert.ToInt32(custRow["CustNum"])))
					SetCustomerDetail(custRow, edvReallocNegativeCashReceipt);
				//else
					//throw new Ice.BLException("Customer does not have open unallocated receipt.");
			}else
			{
				ThrowCustomerNotFoundException();
			}
		}
	}

	private void txtCustIDPCE_AfterExitEditMode(object sender, System.EventArgs args)
	{
		string custID = this.txtCustIDPCE.Text;
		if(!string.IsNullOrWhiteSpace(custID))
		{
			DataRow custRow = CustomerSearch(custID);
			if(custRow != null)
			{
				SetCustomerDetail(custRow, edvReallocPositiveCashReceipt);
			}else
			{
				ThrowCustomerNotFoundException();
			}
		}
	}

	private void edvReallocNegativeCashReceipt_EpiViewNotification(EpiDataView view, EpiNotifyArgs args)
	{
		// ** Argument Properties and Uses **
		// view.dataView[args.Row]["FieldName"]
		// args.Row, args.Column, args.Sender, args.NotifyType
		// NotifyType.Initialize, NotifyType.AddRow, NotifyType.DeleteRow, NotifyType.InitLastView, NotifyType.InitAndResetTreeNodes
		if ((args.NotifyType == EpiTransaction.NotifyType.AddRow))
		{
			if ((args.Row > -1))
			{
				ToggleBtnState(this.btnSearchCustomerNCE, false);
				ToggleBtnState(this.btnSearchCustomerPCE, false);
				ToggleBtnState(this.btnSubmitReallocCR, false);
                ToggleBtnState(this.btnSearchPaymentNCE, false);
			}
		}
	
		if ((args.NotifyType == EpiTransaction.NotifyType.DeleteRow))
		{
			if ((args.Row > -1))
			{
				ToggleBtnState(this.btnSearchCustomerNCE, true);
				ToggleBtnState(this.btnSearchCustomerPCE, true);
				ToggleBtnState(this.btnSubmitReallocCR, true);
                ToggleBtnState(this.btnSearchPaymentNCE, true);
			}
		}
	}
	private bool CheckIfCustomerHasUR(int custNum)
	{
		Ice.Proxy.Lib.BOReaderImpl _bor = WCFServiceSupport.CreateImpl<Ice.Proxy.Lib.BOReaderImpl>((Ice.Core.Session)this.oTrans.Session, Epicor.ServiceModel.Channels.ImplBase<Ice.Contracts.BOReaderSvcContract>.UriPath);
		string whereClause = string.Format("CustNum = {0} AND OpenInvoice=1 AND InvoiceSuffix = 'UR'", custNum);
		DataSet dsInvoiceAdapter = _bor.GetList("Erp:BO:ARInvoice", whereClause, "OpenInvoice");
		if(dsInvoiceAdapter.Tables[0].Rows.Count > 0)
		{
			return true;
		}
		return false;
	}
#endregion Realloc Cash Receipt

	private void ugPaymentGridReallocateNCE_CellChange(object sender, Infragistics.Win.UltraWinGrid.CellEventArgs args)
	{
     
       switch(args.Cell.Column.ToString())
       {
          case"Calculated_Select":
          decimal value =0m;
          info=string.Empty;
          foreach(UltraGridRow row in ugPaymentGridReallocateNCE.Rows.AsEnumerable().Where(x => Convert.ToBoolean(x.Cells["Calculated_Select"].Text) == true))
          {
            value+=(decimal)row.Cells["CashHead_DocTranAmt"].Value;
          }
          //neAdjAmtNCE.Value=-value;
          DataRow reAllocNCR = edvReallocNegativeCashReceipt.CurrentDataRow;
          reAllocNCR.BeginEdit();
          reAllocNCR["AdjAmt"]=-value;
          reAllocNCR.EndEdit();
          break;
       }
	
	}
#region Refunds

    private void FormatRefundGrid()
    {
      EpiUltraGrid grd = grdRefunds;
      grd.DisplayLayout.AutoFitStyle = AutoFitStyle.ResizeAllColumns;
     
      UltraGridBand bnd = grd.DisplayLayout.Bands[0];
      bnd.Columns["Customer_CustID"].Hidden=true;
      bnd.Columns["Customer_Name"].Hidden=true;
      bnd.Columns["SysRowID"].Hidden=true;
      bnd.Columns["SysRevID"].Hidden=true;
      bnd.Columns["Calculated_GroupID"].Hidden=true;
      bnd.Columns["Calculated_Reference"].Hidden=true;
      int postion = 0;
      bnd.Columns["Calculated_Selected"].Header.VisiblePosition=++postion;
      bnd.Columns["InvcHead_InvoiceNum"].Header.VisiblePosition=++postion;
      bnd.Columns["InvcHead_DebitNote"].Header.VisiblePosition=++postion;
      bnd.Columns["Calculated_InvoiceAmount"].Header.VisiblePosition=++postion;
      bnd.Columns["Calculated_Balance"].Header.VisiblePosition=++postion;
      bnd.Columns["Calculated_ApplyAmount"].Header.VisiblePosition=++postion;
      bnd.Columns["Calculated_NewBalance"].Header.VisiblePosition=++postion;

    }
    
    private void grdRefunds_CellChange(object sender, Infragistics.Win.UltraWinGrid.CellEventArgs args)
	{
     
       switch(args.Cell.Column.ToString())
       {
          case"Calculated_Selected":
          decimal value =0m;
          info=string.Empty;
          foreach(UltraGridRow row in grdRefunds.Rows.AsEnumerable().Where(x => Convert.ToBoolean(x.Cells["Calculated_Selected"].Text) == true))
          {
            value+=(decimal)row.Cells["Calculated_ApplyAmount"].Value;
          }
          //neAdjAmtNCE.Value=-value;
          DataRow refundRow = edvRefunds.CurrentDataRow;
          refundRow.BeginEdit();
          refundRow["RefundSelectedAmount"]=value;
          refundRow.EndEdit();
          break;

          case "Calculated_ApplyAmount":
          UltraGridRow refund_row  = args.Cell.Row;
          refund_row.Cells["Calculated_NewBalance"].Value = ((Decimal)(refund_row.Cells["Calculated_Balance"].Value)) - ((Decimal)(refund_row.Cells["Calculated_ApplyAmount"].Value));
          break;
       }
	
	}

	

    private void txtbxRfCust_AfterExitEditMode(object sender, EventArgs e)
    {
        string custID = this.txtbxRfCust.Text;
		if(!string.IsNullOrWhiteSpace(custID))
		{
			DataRow custRow = CustomerSearch(custID);
			if(custRow != null)
			{
				SetCustomerDetail(custRow, edvRefunds);
			}else
			{
				ThrowCustomerNotFoundException();
			}
		}
    }
    
    private void InitRefundsView()
	{
		DataRow newRow = edvRefunds.dataView.Table.NewRow();
        newRow.BeginEdit();
        newRow["RefundDate"]=DateTime.Today;
        newRow.EndEdit();
		edvRefunds.dataView.Table.Rows.Add(newRow);
		edvRefunds.Notify(new EpiNotifyArgs(this.oTrans, edvRefunds.dataView.Count - 1, EpiTransaction.NotifyType.AddRow));
	}
	
    private void SearchOpenTransactions(EpiDataView edv, string ViewName,EpiDataView listView)
	{
		DataRow row = edv.dataView.Table.Rows[0];
		string custID = row["CustID"].ToString();
		if(string.IsNullOrWhiteSpace(custID))
			throw new Ice.BLException("Please enter Customer ID");
		QueryExecutionDataSet dqds = GetQueryExecutionDS(BAQName);
		dqds.Tables["ExecutionParameter"].Clear();
		dqds.Tables["ExecutionParameter"].Rows.Add(SetExecutionParameter(dqds.Tables["ExecutionParameter"], "CustID", custID, "nvarchar"));

		DataTable dt = RunBAQ(dqds, RefundBAQName);
		Mapper(listView, dt, edvDic[ViewName]);	
	}
    
    private void btnRfCust_Click(object sender, System.EventArgs args)
	{
		// ** Place Event Handling Code Here **
        DataRow custRow = SearchOnCustomerAdapterShowDialog();
		if(custRow != null)
		{
			SetCustomerDetail(custRow, edvRefunds);
		}
	}
   
    private void ValidatingRefundInput()
	{
		DataRow refund_row = this.edvRefunds.CurrentDataRow;
		if((Decimal)refund_row["RefundAmount"] < (Decimal)refund_row["RefundSelectedAmount"] )
		{
			throw new BLException("Selected amount cannot be more than the refund amount!!");
		}
	}
    private void refundSubmit(EnumerableRowCollection collection)
    {
      using(this.oTrans.PushDisposableStatusText("Validating Input. Please wait...", true))
	   {
			ValidatingRefundInput();
	   }
      using(this.oTrans.PushDisposableStatusText("Processing refunds. Please wait!!!", true))
		{
			DataRow refundRow = this.edvRefunds.CurrentDataRow;		
			ConnectDynamicQueryAdapter();
			DataSet ds = dynamicQueryAdapter.GetQueryEmptyResultSet(RefundBAQName);
			ds.Tables["Results"].Clear();
			foreach(DataRow row in collection)
			{
				Guid guid = Guid.NewGuid();
				var newRow = ds.Tables["Results"].NewRow();
				newRow.BeginEdit();
               // newRow["Calculated_BatchID"]=edvCashEntry.dataView[edvCashEntry.Row]["BatchNumber"];
				//newRow["CashHead_GroupID"] = row["CashHead_GroupID"];
				//newRow["CashHead_HeadNum"] = row["CashHead_HeadNum"];
				//newRow["CashHead_ReversedReason"] = refundRow["Reference"];
				////newRow["CashHead_ReverseDate"] = refundRow["EffectiveDate"];
              //  newRow["CashHead_DocTranAmt"]=row["CashHead_DocTranAmt"];
				newRow["RowIdent"] = guid.ToString();
				newRow["RowMod"] = "U";
				newRow["SysRowID"] = guid;
				newRow.EndEdit();
				ds.Tables["Results"].Rows.Add(newRow);
			}
			dynamicQueryAdapter.Update(RefundBAQName, ds);
			DisposeDynamicQueryAdapter();
		}
		ClearCashEntryScreen();
    }

	private void btnRfSCrdNte_Click(object sender, System.EventArgs args)
	{
		// ** Place Event Handling Code Here **
       ClearEpiDataView(edvRefundsList);
       SearchOpenTransactions(edvRefunds, "RefundsList", edvRefundsList);
	}

	private void btnRfSubmit_Click(object sender, System.EventArgs args)
	{
		// ** Place Event Handling Code Here **
        
	}

	private void btnRfCancel_Click(object sender, System.EventArgs args)
	{
		// ** Place Event Handling Code Here **
         ClearEpiDataView(edvRefunds);
         ClearEpiDataView(edvRefundsList);
	}
#endregion End Refunds
}




























