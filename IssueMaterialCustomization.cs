// **************************************************
// Custom code for IssueMaterialForm
// Created: 23/03/2022 10:33:37 AM
// **************************************************

extern alias Erp_Adapters_JobEntry;


extern alias Erp_Adapters_PartRevSearch;
extern alias Erp_Adapters_InventoryAttributeSearch;


extern alias Erp_Contracts_BO_IssueReturn;
extern alias Erp_Contracts_BO_JobEntry;
extern alias Erp_Contracts_BO_JobAsmSearch;
extern alias Erp_Contracts_BO_JobMtlSearch;

using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using Erp.Adapters;
using Erp.UI;
using Ice.Lib;
using Ice.Adapters;
using Ice.Lib.Customization;
using Ice.Lib.ExtendedProps;
using Ice.Lib.Framework;
using Ice.Lib.Searches;
using Ice.UI.FormFunctions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;

public class Script
{

	private Ice.Lib.Customization.CustomScriptManager csm;

	private Erp.UI.App.IssueMaterialEntry.IssueMaterialTransaction oTrans;

	private Erp.UI.App.IssueMaterialEntry.IssueMaterialForm IssueMaterialForm;

	private Infragistics.Win.UltraWinToolbars.UltraToolbarsManager baseToolbarsManager;

	private System.Data.DataTable Client_Column;

	private Ice.Lib.Framework.EpiDataView CallContextClientData_Row;

	private System.Data.DataTable SelectedJobAsmbl_Column;

	private Ice.Lib.Framework.EpiDataView SelectList_Row;

	private System.Data.DataTable BpmData_Column;

	private Ice.Lib.Framework.EpiDataView CallContextBpmData_Row;

	private System.Data.DataTable IssueReturn_Column;

	private Ice.Lib.Framework.EpiDataView IM_Row;

	private System.Data.DataTable DoneTable_Column;

	private Ice.Lib.Framework.EpiDataView Done_Row;

	private System.Data.DataTable IssueReturnJobAsmbl_Column;

	private Ice.Lib.Framework.EpiDataView Select_Row;

	private Ice.Lib.Framework.EpiDockManagerPanel AddMaterial;

	private Ice.Lib.Framework.EpiDockManagerPanel AddMaterial_94b79866_def5_4fe7_9849_17e785738d77;

	private Ice.Lib.Framework.EpiBasePanel AddMaterialDetail;

	private Ice.Lib.Framework.EpiBasePanel AddMaterialDetail_e8d61967_7821_4516_bca5_25e75214cb7a;

	private Ice.Lib.Framework.EpiBasePanel AddMaterialList;

	private Ice.Lib.Framework.EpiBasePanel AddMaterialList_353be42e_7552_4bc6_b537_d5b0f75c8ef6;

	private Ice.Lib.Framework.EpiBasePanel AddMaterialJob;

	private Ice.Lib.Framework.EpiBasePanel AddMaterialJob_9a03ed19_5474_4757_b871_759e8fb677b4;

	private Ice.Lib.Framework.EpiGroupBox epiGroupBoxAddMaterialToJob;

	private Ice.Lib.Framework.EpiGroupBox epiGroupBoxAddMaterialToJob_820c0d42_d6d6_4c8f_87bb_e6551a409a1d;

	private Ice.Lib.Framework.EpiButton epiButtonAddMaterialToJob;

	private Ice.Lib.Framework.EpiButton epiButtonAddMaterialToJob_8313e6d9_09d1_4ee9_9afa_78800c2020ab;

	private Ice.Lib.Framework.EpiTextBox epiTextBoxAddMtlToJob;

	private Ice.Lib.Framework.EpiTextBox epiTextBoxAddMtlToJob_780fcd2c_a59b_41da_a21b_21461741466b;

	private Ice.Lib.Framework.EpiButton epiButtonAddMtlJobAsmbly;

	private Ice.Lib.Framework.EpiButton epiButtonAddMtlJobAsmbly_1a13222c_3a89_4917_8623_964cacf80dfd;

	private Ice.Lib.Framework.EpiButton epiButtonAddMtlJobOprt;

	private Ice.Lib.Framework.EpiButton epiButtonAddMtlJobOprt_5acc2861_5ec5_4fc9_97e0_13c9f41f2c0a;

	private Ice.Lib.Framework.EpiTextBox epiTextBoxAddMtlJobPartNum;

	private Ice.Lib.Framework.EpiTextBox epiTextBoxAddMtlJobPartNum_78122d04_c277_477e_9a73_1ec4a6c8ccc4;

	private Ice.Lib.Framework.EpiTextBox epiTextBoxAddMtlJobAssmDesc;

	private Ice.Lib.Framework.EpiTextBox epiTextBoxAddMtlJobAssmDesc_581bfe79_ed3e_4afb_ab76_48acd3781d29;

	private Ice.Lib.Framework.EpiButton epiButtonAddMtlJobAsmblDesc;

	private Ice.Lib.Framework.EpiButton epiButtonAddMtlJobAsmblDesc_49d535fa_7c8e_4b55_9bd6_8941fc1c802f;

	private Ice.Lib.Framework.EpiButton epiButtonAddMtlAssemDesc;

	private Ice.Lib.Framework.EpiButton epiButtonAddMtlAssemDesc_e3e50708_f001_4228_a8b1_6578cfec4db0;

	private Ice.Lib.Framework.EpiButton epiButtonAddMtlJobOpDesc;

	private Ice.Lib.Framework.EpiButton epiButtonAddMtlJobOpDesc_adf5458c_fe7e_4764_9e79_a3e0df6713ca;

	private Ice.Lib.Framework.EpiTextBox epiTextBoxAddMtlAssemDesc;

	private Ice.Lib.Framework.EpiTextBox epiTextBoxAddMtlAssemDesc_d6d097bf_1e01_4b4b_bc97_f222444a722e;

	private Ice.Lib.Framework.EpiTextBox epiTextBoxAddMtlJobMtlRelatedOperation;

	private Ice.Lib.Framework.EpiTextBox epiTextBoxAddMtlJobMtlRelatedOperation_07d99791_8075_4157_a296_c36366795760;

	private Ice.Lib.Framework.EpiTextBox epiTextBoxAddMtlJobAssmPart;

	private Ice.Lib.Framework.EpiTextBox epiTextBoxAddMtlJobAssmPart_3799e553_80f4_4bfa_bfa5_3a81cef961ed;

	private Ice.Lib.Framework.EpiUltraCombo AddMtlJobAssmCombo;

	private Ice.Lib.Framework.EpiUltraCombo AddMtlJobAssmCombo_f7ce82a6_d527_4791_a971_70ae239fe6ac;

	private Ice.Lib.Framework.EpiCombo epiComboAddMtlOperation;

	private Ice.Lib.Framework.EpiCombo epiComboAddMtlOperation_2fe7b06d_b4ed_49bb_bdfd_f4565fd6411c;

	private Ice.Lib.Framework.EpiGroupBox epiGroupBoxMtl;

	private Ice.Lib.Framework.EpiGroupBox epiGroupBoxMtl_1fc66d6d_50ab_4464_bdd5_a83736fe38e1;

	private Ice.Lib.Framework.EpiGroupBox epiGroupBoxNewMtl;

	private Ice.Lib.Framework.EpiGroupBox epiGroupBoxNewMtl_38b22734_ffcc_4725_b68a_3bbd00157a39;

	private Ice.Lib.Framework.EpiGroupBox epiGroupBoxQuantity;

	private Ice.Lib.Framework.EpiGroupBox epiGroupBoxQuantity_76a74529_4c48_490c_8d39_5abb83c9a21f;

	private Ice.Lib.Framework.EpiLabel epiLabelNewMtlSeq;

	private Ice.Lib.Framework.EpiLabel epiLabelNewMtlSeq_ac1b70a4_bc78_4bbc_9330_8ca431000848;

	private Ice.Lib.Framework.EpiNumericEditor epiNumericEditorC1;

	private Ice.Lib.Framework.EpiNumericEditor epiNumericEditorC1_3660592e_b84a_4f03_a655_8da9c97b4979;

	private Ice.Lib.Framework.EpiButton epiButtonMtlPartRev;

	private Ice.Lib.Framework.EpiButton epiButtonMtlPartRev_3dc12e34_2aac_4c2a_a102_5f7a8188ebc9;

	private Ice.Lib.Framework.EpiButton epiButtonNewMtlPartAttri;

	private Ice.Lib.Framework.EpiButton epiButtonNewMtlPartAttri_c3173445_2963_4ddc_b0aa_59857f218003;

	private Ice.Lib.Framework.EpiTextBox epiTextBoxNewMtlPart;

	private Ice.Lib.Framework.EpiTextBox epiTextBoxNewMtlPart_8b322e62_e8d0_412e_994e_3fa3e2213b5b;

	private Ice.Lib.Framework.EpiTextBox epiTextBoxNewMtlAttri;

	private Ice.Lib.Framework.EpiTextBox epiTextBoxNewMtlAttri_c8a4168e_4fee_43e8_97ad_5567dfb96521;

	private Ice.Lib.Framework.EpiTextBox epiTextBoxNewMtlPrtDesc;

	private Ice.Lib.Framework.EpiTextBox epiTextBoxNewMtlPrtDesc_d1a1c723_85cf_4874_8d96_141ce07f8b26;

	private Ice.Lib.Framework.EpiTextBox epiTextBoxNewMtlPrtAttDesc;

	private Ice.Lib.Framework.EpiTextBox epiTextBoxNewMtlPrtAttDesc_cb485618_7202_4176_b241_7bb1e0b4fb27;

	private Ice.Lib.Framework.EpiLabel epiLabelQtyParent;

	private Ice.Lib.Framework.EpiLabel epiLabelQtyParent_d8d9deaf_0415_4a0b_80e3_73c3a5908078;

	private Ice.Lib.Framework.EpiNumericEditor epiNumericEditorQtyParent;

	private Ice.Lib.Framework.EpiNumericEditor epiNumericEditorQtyParent_54d9e1c7_cdf2_4824_bcc6_c6cba2be5944;

	private Ice.Lib.Framework.EpiLabel epiLabelReqQty;

	private Ice.Lib.Framework.EpiLabel epiLabelReqQty_1cbfaeaf_3f61_454c_b2ec_df5e021d5f96;

	private Ice.Lib.Framework.EpiNumericEditor epiNumericEditorReqQty;

	private Ice.Lib.Framework.EpiNumericEditor epiNumericEditorReqQty_9db2022d_d97b_42b2_a99c_327a477fbcb5;

	private Ice.Lib.Framework.EpiLabel epiLabelUnitCost;

	private Ice.Lib.Framework.EpiLabel epiLabelUnitCost_361d90d8_0e6d_4998_85ec_8f18de7580f1;

	private Ice.Lib.Framework.EpiTextBox epiTextBoxReqUOM;

	private Ice.Lib.Framework.EpiTextBox epiTextBoxReqUOM_3399adea_451d_460d_8679_c1a40bcd2560;

	private Ice.Lib.Framework.EpiTextBox epiTextBoxUnitCostUOM;

	private Ice.Lib.Framework.EpiTextBox epiTextBoxUnitCostUOM_9bf2c373_371c_4143_8126_992bc1fc0d25;

	private Ice.Lib.Framework.EpiLabel epiLabelFixedQty;

	private Ice.Lib.Framework.EpiLabel epiLabelFixedQty_4727cc0b_3a5b_4311_a9de_e862ea9ebd17;

	private Ice.Lib.Framework.EpiCheckBox epiCheckBoxFixedQty;

	private Ice.Lib.Framework.EpiCheckBox epiCheckBoxFixedQty_a03a7a5a_3fe8_40f1_bbe3_e257cc902877;

	private Ice.Lib.Framework.EpiButton epiButtonNewMtl;

	private Ice.Lib.Framework.EpiButton epiButtonNewMtl_73af7677_2746_40ba_9dd2_4a084faa843d;

	private Ice.Lib.Framework.EpiButton epiButtonNewMtlAdd;

	private Ice.Lib.Framework.EpiButton epiButtonNewMtlAdd_d096b4f8_d380_4154_85f1_84d650116c9d;

	private Ice.Lib.Framework.EpiButton epiButtonNewMtlCancel;

	private Ice.Lib.Framework.EpiButton epiButtonNewMtlCancel_f1f8821f_daa0_4dcb_8bd3_aaa832df35b6;

	private Ice.Lib.Framework.EpiUltraCombo partRevisionCombo;

	private Ice.Lib.Framework.EpiUltraCombo partRevisionCombo_041a1e65_2486_4b1b_9667_51418417ba8c;

	private Ice.Lib.Framework.EpiCurrencyEditor epiCurrencyEditorMtlUnitCost;

	private Ice.Lib.Framework.EpiCurrencyEditor epiCurrencyEditorMtlUnitCost_64da9d10_2340_4282_a118_f2e4ac7d90c6;

	private Ice.Lib.Framework.EpiCombo epiComboPrtUOM;

	private Ice.Lib.Framework.EpiCombo epiComboPrtUOM_5fd6900a_a201_4c96_af98_9420f214b1b0;

	private Ice.Lib.Framework.EpiUltraGrid epiUltraGridNewMtl;

	private Ice.Lib.Framework.EpiUltraGrid epiUltraGridNewMtl_a626e87f_6e7e_4ce8_ab1b_eda2a1a9a5c0;

	public void InitializeGlobalVariables(Ice.Lib.Customization.CustomScriptManager csm)
	{
		this.csm = csm;
		this.oTrans = ((Erp.UI.App.IssueMaterialEntry.IssueMaterialTransaction)(this.csm.GetGlobalInstance("oTrans")));
		this.IssueMaterialForm = ((Erp.UI.App.IssueMaterialEntry.IssueMaterialForm)(this.csm.GetGlobalInstance("IssueMaterialForm")));
		this.baseToolbarsManager = ((Infragistics.Win.UltraWinToolbars.UltraToolbarsManager)(this.csm.GetGlobalInstance("baseToolbarsManager")));
		this.Client_Column = ((System.Data.DataTable)(this.csm.GetGlobalInstance("Client_Column")));
		this.CallContextClientData_Row = ((Ice.Lib.Framework.EpiDataView)(this.csm.GetGlobalInstance("CallContextClientData_Row")));
		this.SelectedJobAsmbl_Column = ((System.Data.DataTable)(this.csm.GetGlobalInstance("SelectedJobAsmbl_Column")));
		this.SelectList_Row = ((Ice.Lib.Framework.EpiDataView)(this.csm.GetGlobalInstance("SelectList_Row")));
		this.BpmData_Column = ((System.Data.DataTable)(this.csm.GetGlobalInstance("BpmData_Column")));
		this.CallContextBpmData_Row = ((Ice.Lib.Framework.EpiDataView)(this.csm.GetGlobalInstance("CallContextBpmData_Row")));
		this.IssueReturn_Column = ((System.Data.DataTable)(this.csm.GetGlobalInstance("IssueReturn_Column")));
		this.IM_Row = ((Ice.Lib.Framework.EpiDataView)(this.csm.GetGlobalInstance("IM_Row")));
		this.DoneTable_Column = ((System.Data.DataTable)(this.csm.GetGlobalInstance("DoneTable_Column")));
		this.Done_Row = ((Ice.Lib.Framework.EpiDataView)(this.csm.GetGlobalInstance("Done_Row")));
		this.IssueReturnJobAsmbl_Column = ((System.Data.DataTable)(this.csm.GetGlobalInstance("IssueReturnJobAsmbl_Column")));
		this.Select_Row = ((Ice.Lib.Framework.EpiDataView)(this.csm.GetGlobalInstance("Select_Row")));
		Ice.Lib.Customization.PersonalizeCustomizeManager personalizeCustomizeManager = this.csm.PersonalizeCustomizeManager;
		System.Windows.Forms.Control topControl = personalizeCustomizeManager.TopControl;
		topControl.FindForm().SuspendLayout();
		// Creating custom targets.
		this.AddMaterial = new Ice.Lib.Framework.EpiDockManagerPanel();
		this.AddMaterial_94b79866_def5_4fe7_9849_17e785738d77 = this.AddMaterial;
		System.Collections.Hashtable customControls = personalizeCustomizeManager.CustControlMan.CustomControlsHT;
		customControls.Add("94b79866-def5-4fe7-9849-17e785738d77", this.AddMaterial);
		System.Collections.Hashtable controlsHT = personalizeCustomizeManager.ControlsHT;
		controlsHT.Add("94b79866-def5-4fe7-9849-17e785738d77", this.AddMaterial);
		this.AddMaterial.Name = "AddMaterial";
		this.AddMaterial.EpiGuid = "94b79866-def5-4fe7-9849-17e785738d77";
		this.AddMaterialDetail = new Ice.Lib.Framework.EpiBasePanel();
		this.AddMaterialDetail_e8d61967_7821_4516_bca5_25e75214cb7a = this.AddMaterialDetail;
		customControls.Add("e8d61967-7821-4516-bca5-25e75214cb7a", this.AddMaterialDetail);
		controlsHT.Add("e8d61967-7821-4516-bca5-25e75214cb7a", this.AddMaterialDetail);
		this.AddMaterialDetail.Name = "AddMaterialDetail";
		this.AddMaterialDetail.EpiGuid = "e8d61967-7821-4516-bca5-25e75214cb7a";
		this.AddMaterialList = new Ice.Lib.Framework.EpiBasePanel();
		this.AddMaterialList_353be42e_7552_4bc6_b537_d5b0f75c8ef6 = this.AddMaterialList;
		customControls.Add("353be42e-7552-4bc6-b537-d5b0f75c8ef6", this.AddMaterialList);
		controlsHT.Add("353be42e-7552-4bc6-b537-d5b0f75c8ef6", this.AddMaterialList);
		this.AddMaterialList.Name = "AddMaterialList";
		this.AddMaterialList.EpiGuid = "353be42e-7552-4bc6-b537-d5b0f75c8ef6";
		this.AddMaterialJob = new Ice.Lib.Framework.EpiBasePanel();
		this.AddMaterialJob_9a03ed19_5474_4757_b871_759e8fb677b4 = this.AddMaterialJob;
		customControls.Add("9a03ed19-5474-4757-b871-759e8fb677b4", this.AddMaterialJob);
		controlsHT.Add("9a03ed19-5474-4757-b871-759e8fb677b4", this.AddMaterialJob);
		this.AddMaterialJob.Name = "AddMaterialJob";
		this.AddMaterialJob.EpiGuid = "9a03ed19-5474-4757-b871-759e8fb677b4";
		this.epiGroupBoxAddMaterialToJob = new Ice.Lib.Framework.EpiGroupBox();
		this.epiGroupBoxAddMaterialToJob_820c0d42_d6d6_4c8f_87bb_e6551a409a1d = this.epiGroupBoxAddMaterialToJob;
		customControls.Add("820c0d42-d6d6-4c8f-87bb-e6551a409a1d", this.epiGroupBoxAddMaterialToJob);
		controlsHT.Add("820c0d42-d6d6-4c8f-87bb-e6551a409a1d", this.epiGroupBoxAddMaterialToJob);
		this.epiGroupBoxAddMaterialToJob.Name = "epiGroupBoxAddMaterialToJob";
		this.epiGroupBoxAddMaterialToJob.EpiGuid = "820c0d42-d6d6-4c8f-87bb-e6551a409a1d";
		this.epiButtonAddMaterialToJob = new Ice.Lib.Framework.EpiButton();
		this.epiButtonAddMaterialToJob_8313e6d9_09d1_4ee9_9afa_78800c2020ab = this.epiButtonAddMaterialToJob;
		customControls.Add("8313e6d9-09d1-4ee9-9afa-78800c2020ab", this.epiButtonAddMaterialToJob);
		controlsHT.Add("8313e6d9-09d1-4ee9-9afa-78800c2020ab", this.epiButtonAddMaterialToJob);
		this.epiButtonAddMaterialToJob.Name = "epiButtonAddMaterialToJob";
		this.epiButtonAddMaterialToJob.EpiGuid = "8313e6d9-09d1-4ee9-9afa-78800c2020ab";
		this.epiTextBoxAddMtlToJob = new Ice.Lib.Framework.EpiTextBox();
		this.epiTextBoxAddMtlToJob_780fcd2c_a59b_41da_a21b_21461741466b = this.epiTextBoxAddMtlToJob;
		customControls.Add("780fcd2c-a59b-41da-a21b-21461741466b", this.epiTextBoxAddMtlToJob);
		controlsHT.Add("780fcd2c-a59b-41da-a21b-21461741466b", this.epiTextBoxAddMtlToJob);
		this.epiTextBoxAddMtlToJob.Name = "epiTextBoxAddMtlToJob";
		this.epiTextBoxAddMtlToJob.EpiGuid = "780fcd2c-a59b-41da-a21b-21461741466b";
		this.epiButtonAddMtlJobAsmbly = new Ice.Lib.Framework.EpiButton();
		this.epiButtonAddMtlJobAsmbly_1a13222c_3a89_4917_8623_964cacf80dfd = this.epiButtonAddMtlJobAsmbly;
		customControls.Add("1a13222c-3a89-4917-8623-964cacf80dfd", this.epiButtonAddMtlJobAsmbly);
		controlsHT.Add("1a13222c-3a89-4917-8623-964cacf80dfd", this.epiButtonAddMtlJobAsmbly);
		this.epiButtonAddMtlJobAsmbly.Name = "epiButtonAddMtlJobAsmbly";
		this.epiButtonAddMtlJobAsmbly.EpiGuid = "1a13222c-3a89-4917-8623-964cacf80dfd";
		this.epiButtonAddMtlJobOprt = new Ice.Lib.Framework.EpiButton();
		this.epiButtonAddMtlJobOprt_5acc2861_5ec5_4fc9_97e0_13c9f41f2c0a = this.epiButtonAddMtlJobOprt;
		customControls.Add("5acc2861-5ec5-4fc9-97e0-13c9f41f2c0a", this.epiButtonAddMtlJobOprt);
		controlsHT.Add("5acc2861-5ec5-4fc9-97e0-13c9f41f2c0a", this.epiButtonAddMtlJobOprt);
		this.epiButtonAddMtlJobOprt.Name = "epiButtonAddMtlJobOprt";
		this.epiButtonAddMtlJobOprt.EpiGuid = "5acc2861-5ec5-4fc9-97e0-13c9f41f2c0a";
		this.epiTextBoxAddMtlJobPartNum = new Ice.Lib.Framework.EpiTextBox();
		this.epiTextBoxAddMtlJobPartNum_78122d04_c277_477e_9a73_1ec4a6c8ccc4 = this.epiTextBoxAddMtlJobPartNum;
		customControls.Add("78122d04-c277-477e-9a73-1ec4a6c8ccc4", this.epiTextBoxAddMtlJobPartNum);
		controlsHT.Add("78122d04-c277-477e-9a73-1ec4a6c8ccc4", this.epiTextBoxAddMtlJobPartNum);
		this.epiTextBoxAddMtlJobPartNum.Name = "epiTextBoxAddMtlJobPartNum";
		this.epiTextBoxAddMtlJobPartNum.EpiGuid = "78122d04-c277-477e-9a73-1ec4a6c8ccc4";
		this.epiTextBoxAddMtlJobAssmDesc = new Ice.Lib.Framework.EpiTextBox();
		this.epiTextBoxAddMtlJobAssmDesc_581bfe79_ed3e_4afb_ab76_48acd3781d29 = this.epiTextBoxAddMtlJobAssmDesc;
		customControls.Add("581bfe79-ed3e-4afb-ab76-48acd3781d29", this.epiTextBoxAddMtlJobAssmDesc);
		controlsHT.Add("581bfe79-ed3e-4afb-ab76-48acd3781d29", this.epiTextBoxAddMtlJobAssmDesc);
		this.epiTextBoxAddMtlJobAssmDesc.Name = "epiTextBoxAddMtlJobAssmDesc";
		this.epiTextBoxAddMtlJobAssmDesc.EpiGuid = "581bfe79-ed3e-4afb-ab76-48acd3781d29";
		this.epiButtonAddMtlJobAsmblDesc = new Ice.Lib.Framework.EpiButton();
		this.epiButtonAddMtlJobAsmblDesc_49d535fa_7c8e_4b55_9bd6_8941fc1c802f = this.epiButtonAddMtlJobAsmblDesc;
		customControls.Add("49d535fa-7c8e-4b55-9bd6-8941fc1c802f", this.epiButtonAddMtlJobAsmblDesc);
		controlsHT.Add("49d535fa-7c8e-4b55-9bd6-8941fc1c802f", this.epiButtonAddMtlJobAsmblDesc);
		this.epiButtonAddMtlJobAsmblDesc.Name = "epiButtonAddMtlJobAsmblDesc";
		this.epiButtonAddMtlJobAsmblDesc.EpiGuid = "49d535fa-7c8e-4b55-9bd6-8941fc1c802f";
		this.epiButtonAddMtlAssemDesc = new Ice.Lib.Framework.EpiButton();
		this.epiButtonAddMtlAssemDesc_e3e50708_f001_4228_a8b1_6578cfec4db0 = this.epiButtonAddMtlAssemDesc;
		customControls.Add("e3e50708-f001-4228-a8b1-6578cfec4db0", this.epiButtonAddMtlAssemDesc);
		controlsHT.Add("e3e50708-f001-4228-a8b1-6578cfec4db0", this.epiButtonAddMtlAssemDesc);
		this.epiButtonAddMtlAssemDesc.Name = "epiButtonAddMtlAssemDesc";
		this.epiButtonAddMtlAssemDesc.EpiGuid = "e3e50708-f001-4228-a8b1-6578cfec4db0";
		this.epiButtonAddMtlJobOpDesc = new Ice.Lib.Framework.EpiButton();
		this.epiButtonAddMtlJobOpDesc_adf5458c_fe7e_4764_9e79_a3e0df6713ca = this.epiButtonAddMtlJobOpDesc;
		customControls.Add("adf5458c-fe7e-4764-9e79-a3e0df6713ca", this.epiButtonAddMtlJobOpDesc);
		controlsHT.Add("adf5458c-fe7e-4764-9e79-a3e0df6713ca", this.epiButtonAddMtlJobOpDesc);
		this.epiButtonAddMtlJobOpDesc.Name = "epiButtonAddMtlJobOpDesc";
		this.epiButtonAddMtlJobOpDesc.EpiGuid = "adf5458c-fe7e-4764-9e79-a3e0df6713ca";
		this.epiTextBoxAddMtlAssemDesc = new Ice.Lib.Framework.EpiTextBox();
		this.epiTextBoxAddMtlAssemDesc_d6d097bf_1e01_4b4b_bc97_f222444a722e = this.epiTextBoxAddMtlAssemDesc;
		customControls.Add("d6d097bf-1e01-4b4b-bc97-f222444a722e", this.epiTextBoxAddMtlAssemDesc);
		controlsHT.Add("d6d097bf-1e01-4b4b-bc97-f222444a722e", this.epiTextBoxAddMtlAssemDesc);
		this.epiTextBoxAddMtlAssemDesc.Name = "epiTextBoxAddMtlAssemDesc";
		this.epiTextBoxAddMtlAssemDesc.EpiGuid = "d6d097bf-1e01-4b4b-bc97-f222444a722e";
		this.epiTextBoxAddMtlJobMtlRelatedOperation = new Ice.Lib.Framework.EpiTextBox();
		this.epiTextBoxAddMtlJobMtlRelatedOperation_07d99791_8075_4157_a296_c36366795760 = this.epiTextBoxAddMtlJobMtlRelatedOperation;
		customControls.Add("07d99791-8075-4157-a296-c36366795760", this.epiTextBoxAddMtlJobMtlRelatedOperation);
		controlsHT.Add("07d99791-8075-4157-a296-c36366795760", this.epiTextBoxAddMtlJobMtlRelatedOperation);
		this.epiTextBoxAddMtlJobMtlRelatedOperation.Name = "epiTextBoxAddMtlJobMtlRelatedOperation";
		this.epiTextBoxAddMtlJobMtlRelatedOperation.EpiGuid = "07d99791-8075-4157-a296-c36366795760";
		this.epiTextBoxAddMtlJobAssmPart = new Ice.Lib.Framework.EpiTextBox();
		this.epiTextBoxAddMtlJobAssmPart_3799e553_80f4_4bfa_bfa5_3a81cef961ed = this.epiTextBoxAddMtlJobAssmPart;
		customControls.Add("3799e553-80f4-4bfa-bfa5-3a81cef961ed", this.epiTextBoxAddMtlJobAssmPart);
		controlsHT.Add("3799e553-80f4-4bfa-bfa5-3a81cef961ed", this.epiTextBoxAddMtlJobAssmPart);
		this.epiTextBoxAddMtlJobAssmPart.Name = "epiTextBoxAddMtlJobAssmPart";
		this.epiTextBoxAddMtlJobAssmPart.EpiGuid = "3799e553-80f4-4bfa-bfa5-3a81cef961ed";
		this.AddMtlJobAssmCombo = Ice.Lib.Customization.Designers.RetrieverComboDesigner.CreateRetriever("f7ce82a6-d527-4791-a971-70ae239fe6ac", "JobAsmSearch", "JobAsmSearchCombo", personalizeCustomizeManager);
		this.AddMtlJobAssmCombo_f7ce82a6_d527_4791_a971_70ae239fe6ac = this.AddMtlJobAssmCombo;
		customControls.Add("f7ce82a6-d527-4791-a971-70ae239fe6ac", this.AddMtlJobAssmCombo);
		controlsHT.Add("f7ce82a6-d527-4791-a971-70ae239fe6ac", this.AddMtlJobAssmCombo);
		this.AddMtlJobAssmCombo.Name = "AddMtlJobAssmCombo";
		this.AddMtlJobAssmCombo.EpiGuid = "f7ce82a6-d527-4791-a971-70ae239fe6ac";
		this.epiComboAddMtlOperation = new Ice.Lib.Framework.EpiCombo();
		this.epiComboAddMtlOperation_2fe7b06d_b4ed_49bb_bdfd_f4565fd6411c = this.epiComboAddMtlOperation;
		customControls.Add("2fe7b06d-b4ed-49bb-bdfd-f4565fd6411c", this.epiComboAddMtlOperation);
		controlsHT.Add("2fe7b06d-b4ed-49bb-bdfd-f4565fd6411c", this.epiComboAddMtlOperation);
		this.epiComboAddMtlOperation.Name = "epiComboAddMtlOperation";
		this.epiComboAddMtlOperation.EpiGuid = "2fe7b06d-b4ed-49bb-bdfd-f4565fd6411c";
		this.epiGroupBoxMtl = new Ice.Lib.Framework.EpiGroupBox();
		this.epiGroupBoxMtl_1fc66d6d_50ab_4464_bdd5_a83736fe38e1 = this.epiGroupBoxMtl;
		customControls.Add("1fc66d6d-50ab-4464-bdd5-a83736fe38e1", this.epiGroupBoxMtl);
		controlsHT.Add("1fc66d6d-50ab-4464-bdd5-a83736fe38e1", this.epiGroupBoxMtl);
		this.epiGroupBoxMtl.Name = "epiGroupBoxMtl";
		this.epiGroupBoxMtl.EpiGuid = "1fc66d6d-50ab-4464-bdd5-a83736fe38e1";
		this.epiGroupBoxNewMtl = new Ice.Lib.Framework.EpiGroupBox();
		this.epiGroupBoxNewMtl_38b22734_ffcc_4725_b68a_3bbd00157a39 = this.epiGroupBoxNewMtl;
		customControls.Add("38b22734-ffcc-4725-b68a-3bbd00157a39", this.epiGroupBoxNewMtl);
		controlsHT.Add("38b22734-ffcc-4725-b68a-3bbd00157a39", this.epiGroupBoxNewMtl);
		this.epiGroupBoxNewMtl.Name = "epiGroupBoxNewMtl";
		this.epiGroupBoxNewMtl.EpiGuid = "38b22734-ffcc-4725-b68a-3bbd00157a39";
		this.epiGroupBoxQuantity = new Ice.Lib.Framework.EpiGroupBox();
		this.epiGroupBoxQuantity_76a74529_4c48_490c_8d39_5abb83c9a21f = this.epiGroupBoxQuantity;
		customControls.Add("76a74529-4c48-490c-8d39-5abb83c9a21f", this.epiGroupBoxQuantity);
		controlsHT.Add("76a74529-4c48-490c-8d39-5abb83c9a21f", this.epiGroupBoxQuantity);
		this.epiGroupBoxQuantity.Name = "epiGroupBoxQuantity";
		this.epiGroupBoxQuantity.EpiGuid = "76a74529-4c48-490c-8d39-5abb83c9a21f";
		this.epiLabelNewMtlSeq = new Ice.Lib.Framework.EpiLabel();
		this.epiLabelNewMtlSeq_ac1b70a4_bc78_4bbc_9330_8ca431000848 = this.epiLabelNewMtlSeq;
		customControls.Add("ac1b70a4-bc78-4bbc-9330-8ca431000848", this.epiLabelNewMtlSeq);
		controlsHT.Add("ac1b70a4-bc78-4bbc-9330-8ca431000848", this.epiLabelNewMtlSeq);
		this.epiLabelNewMtlSeq.Name = "epiLabelNewMtlSeq";
		this.epiLabelNewMtlSeq.EpiGuid = "ac1b70a4-bc78-4bbc-9330-8ca431000848";
		this.epiNumericEditorC1 = new Ice.Lib.Framework.EpiNumericEditor();
		this.epiNumericEditorC1_3660592e_b84a_4f03_a655_8da9c97b4979 = this.epiNumericEditorC1;
		customControls.Add("3660592e-b84a-4f03-a655-8da9c97b4979", this.epiNumericEditorC1);
		controlsHT.Add("3660592e-b84a-4f03-a655-8da9c97b4979", this.epiNumericEditorC1);
		this.epiNumericEditorC1.Name = "epiNumericEditorC1";
		this.epiNumericEditorC1.EpiGuid = "3660592e-b84a-4f03-a655-8da9c97b4979";
		this.epiButtonMtlPartRev = new Ice.Lib.Framework.EpiButton();
		this.epiButtonMtlPartRev_3dc12e34_2aac_4c2a_a102_5f7a8188ebc9 = this.epiButtonMtlPartRev;
		customControls.Add("3dc12e34-2aac-4c2a-a102-5f7a8188ebc9", this.epiButtonMtlPartRev);
		controlsHT.Add("3dc12e34-2aac-4c2a-a102-5f7a8188ebc9", this.epiButtonMtlPartRev);
		this.epiButtonMtlPartRev.Name = "epiButtonMtlPartRev";
		this.epiButtonMtlPartRev.EpiGuid = "3dc12e34-2aac-4c2a-a102-5f7a8188ebc9";
		this.epiButtonNewMtlPartAttri = new Ice.Lib.Framework.EpiButton();
		this.epiButtonNewMtlPartAttri_c3173445_2963_4ddc_b0aa_59857f218003 = this.epiButtonNewMtlPartAttri;
		customControls.Add("c3173445-2963-4ddc-b0aa-59857f218003", this.epiButtonNewMtlPartAttri);
		controlsHT.Add("c3173445-2963-4ddc-b0aa-59857f218003", this.epiButtonNewMtlPartAttri);
		this.epiButtonNewMtlPartAttri.Name = "epiButtonNewMtlPartAttri";
		this.epiButtonNewMtlPartAttri.EpiGuid = "c3173445-2963-4ddc-b0aa-59857f218003";
		this.epiTextBoxNewMtlPart = new Ice.Lib.Framework.EpiTextBox();
		this.epiTextBoxNewMtlPart_8b322e62_e8d0_412e_994e_3fa3e2213b5b = this.epiTextBoxNewMtlPart;
		customControls.Add("8b322e62-e8d0-412e-994e-3fa3e2213b5b", this.epiTextBoxNewMtlPart);
		controlsHT.Add("8b322e62-e8d0-412e-994e-3fa3e2213b5b", this.epiTextBoxNewMtlPart);
		this.epiTextBoxNewMtlPart.Name = "epiTextBoxNewMtlPart";
		this.epiTextBoxNewMtlPart.EpiGuid = "8b322e62-e8d0-412e-994e-3fa3e2213b5b";
		this.epiTextBoxNewMtlAttri = new Ice.Lib.Framework.EpiTextBox();
		this.epiTextBoxNewMtlAttri_c8a4168e_4fee_43e8_97ad_5567dfb96521 = this.epiTextBoxNewMtlAttri;
		customControls.Add("c8a4168e-4fee-43e8-97ad-5567dfb96521", this.epiTextBoxNewMtlAttri);
		controlsHT.Add("c8a4168e-4fee-43e8-97ad-5567dfb96521", this.epiTextBoxNewMtlAttri);
		this.epiTextBoxNewMtlAttri.Name = "epiTextBoxNewMtlAttri";
		this.epiTextBoxNewMtlAttri.EpiGuid = "c8a4168e-4fee-43e8-97ad-5567dfb96521";
		this.epiTextBoxNewMtlPrtDesc = new Ice.Lib.Framework.EpiTextBox();
		this.epiTextBoxNewMtlPrtDesc_d1a1c723_85cf_4874_8d96_141ce07f8b26 = this.epiTextBoxNewMtlPrtDesc;
		customControls.Add("d1a1c723-85cf-4874-8d96-141ce07f8b26", this.epiTextBoxNewMtlPrtDesc);
		controlsHT.Add("d1a1c723-85cf-4874-8d96-141ce07f8b26", this.epiTextBoxNewMtlPrtDesc);
		this.epiTextBoxNewMtlPrtDesc.Name = "epiTextBoxNewMtlPrtDesc";
		this.epiTextBoxNewMtlPrtDesc.EpiGuid = "d1a1c723-85cf-4874-8d96-141ce07f8b26";
		this.epiTextBoxNewMtlPrtAttDesc = new Ice.Lib.Framework.EpiTextBox();
		this.epiTextBoxNewMtlPrtAttDesc_cb485618_7202_4176_b241_7bb1e0b4fb27 = this.epiTextBoxNewMtlPrtAttDesc;
		customControls.Add("cb485618-7202-4176-b241-7bb1e0b4fb27", this.epiTextBoxNewMtlPrtAttDesc);
		controlsHT.Add("cb485618-7202-4176-b241-7bb1e0b4fb27", this.epiTextBoxNewMtlPrtAttDesc);
		this.epiTextBoxNewMtlPrtAttDesc.Name = "epiTextBoxNewMtlPrtAttDesc";
		this.epiTextBoxNewMtlPrtAttDesc.EpiGuid = "cb485618-7202-4176-b241-7bb1e0b4fb27";
		this.epiLabelQtyParent = new Ice.Lib.Framework.EpiLabel();
		this.epiLabelQtyParent_d8d9deaf_0415_4a0b_80e3_73c3a5908078 = this.epiLabelQtyParent;
		customControls.Add("d8d9deaf-0415-4a0b-80e3-73c3a5908078", this.epiLabelQtyParent);
		controlsHT.Add("d8d9deaf-0415-4a0b-80e3-73c3a5908078", this.epiLabelQtyParent);
		this.epiLabelQtyParent.Name = "epiLabelQtyParent";
		this.epiLabelQtyParent.EpiGuid = "d8d9deaf-0415-4a0b-80e3-73c3a5908078";
		this.epiNumericEditorQtyParent = new Ice.Lib.Framework.EpiNumericEditor();
		this.epiNumericEditorQtyParent_54d9e1c7_cdf2_4824_bcc6_c6cba2be5944 = this.epiNumericEditorQtyParent;
		customControls.Add("54d9e1c7-cdf2-4824-bcc6-c6cba2be5944", this.epiNumericEditorQtyParent);
		controlsHT.Add("54d9e1c7-cdf2-4824-bcc6-c6cba2be5944", this.epiNumericEditorQtyParent);
		this.epiNumericEditorQtyParent.Name = "epiNumericEditorQtyParent";
		this.epiNumericEditorQtyParent.EpiGuid = "54d9e1c7-cdf2-4824-bcc6-c6cba2be5944";
		this.epiLabelReqQty = new Ice.Lib.Framework.EpiLabel();
		this.epiLabelReqQty_1cbfaeaf_3f61_454c_b2ec_df5e021d5f96 = this.epiLabelReqQty;
		customControls.Add("1cbfaeaf-3f61-454c-b2ec-df5e021d5f96", this.epiLabelReqQty);
		controlsHT.Add("1cbfaeaf-3f61-454c-b2ec-df5e021d5f96", this.epiLabelReqQty);
		this.epiLabelReqQty.Name = "epiLabelReqQty";
		this.epiLabelReqQty.EpiGuid = "1cbfaeaf-3f61-454c-b2ec-df5e021d5f96";
		this.epiNumericEditorReqQty = new Ice.Lib.Framework.EpiNumericEditor();
		this.epiNumericEditorReqQty_9db2022d_d97b_42b2_a99c_327a477fbcb5 = this.epiNumericEditorReqQty;
		customControls.Add("9db2022d-d97b-42b2-a99c-327a477fbcb5", this.epiNumericEditorReqQty);
		controlsHT.Add("9db2022d-d97b-42b2-a99c-327a477fbcb5", this.epiNumericEditorReqQty);
		this.epiNumericEditorReqQty.Name = "epiNumericEditorReqQty";
		this.epiNumericEditorReqQty.EpiGuid = "9db2022d-d97b-42b2-a99c-327a477fbcb5";
		this.epiLabelUnitCost = new Ice.Lib.Framework.EpiLabel();
		this.epiLabelUnitCost_361d90d8_0e6d_4998_85ec_8f18de7580f1 = this.epiLabelUnitCost;
		customControls.Add("361d90d8-0e6d-4998-85ec-8f18de7580f1", this.epiLabelUnitCost);
		controlsHT.Add("361d90d8-0e6d-4998-85ec-8f18de7580f1", this.epiLabelUnitCost);
		this.epiLabelUnitCost.Name = "epiLabelUnitCost";
		this.epiLabelUnitCost.EpiGuid = "361d90d8-0e6d-4998-85ec-8f18de7580f1";
		this.epiTextBoxReqUOM = new Ice.Lib.Framework.EpiTextBox();
		this.epiTextBoxReqUOM_3399adea_451d_460d_8679_c1a40bcd2560 = this.epiTextBoxReqUOM;
		customControls.Add("3399adea-451d-460d-8679-c1a40bcd2560", this.epiTextBoxReqUOM);
		controlsHT.Add("3399adea-451d-460d-8679-c1a40bcd2560", this.epiTextBoxReqUOM);
		this.epiTextBoxReqUOM.Name = "epiTextBoxReqUOM";
		this.epiTextBoxReqUOM.EpiGuid = "3399adea-451d-460d-8679-c1a40bcd2560";
		this.epiTextBoxUnitCostUOM = new Ice.Lib.Framework.EpiTextBox();
		this.epiTextBoxUnitCostUOM_9bf2c373_371c_4143_8126_992bc1fc0d25 = this.epiTextBoxUnitCostUOM;
		customControls.Add("9bf2c373-371c-4143-8126-992bc1fc0d25", this.epiTextBoxUnitCostUOM);
		controlsHT.Add("9bf2c373-371c-4143-8126-992bc1fc0d25", this.epiTextBoxUnitCostUOM);
		this.epiTextBoxUnitCostUOM.Name = "epiTextBoxUnitCostUOM";
		this.epiTextBoxUnitCostUOM.EpiGuid = "9bf2c373-371c-4143-8126-992bc1fc0d25";
		this.epiLabelFixedQty = new Ice.Lib.Framework.EpiLabel();
		this.epiLabelFixedQty_4727cc0b_3a5b_4311_a9de_e862ea9ebd17 = this.epiLabelFixedQty;
		customControls.Add("4727cc0b-3a5b-4311-a9de-e862ea9ebd17", this.epiLabelFixedQty);
		controlsHT.Add("4727cc0b-3a5b-4311-a9de-e862ea9ebd17", this.epiLabelFixedQty);
		this.epiLabelFixedQty.Name = "epiLabelFixedQty";
		this.epiLabelFixedQty.EpiGuid = "4727cc0b-3a5b-4311-a9de-e862ea9ebd17";
		this.epiCheckBoxFixedQty = new Ice.Lib.Framework.EpiCheckBox();
		this.epiCheckBoxFixedQty_a03a7a5a_3fe8_40f1_bbe3_e257cc902877 = this.epiCheckBoxFixedQty;
		customControls.Add("a03a7a5a-3fe8-40f1-bbe3-e257cc902877", this.epiCheckBoxFixedQty);
		controlsHT.Add("a03a7a5a-3fe8-40f1-bbe3-e257cc902877", this.epiCheckBoxFixedQty);
		this.epiCheckBoxFixedQty.Name = "epiCheckBoxFixedQty";
		this.epiCheckBoxFixedQty.EpiGuid = "a03a7a5a-3fe8-40f1-bbe3-e257cc902877";
		this.epiButtonNewMtl = new Ice.Lib.Framework.EpiButton();
		this.epiButtonNewMtl_73af7677_2746_40ba_9dd2_4a084faa843d = this.epiButtonNewMtl;
		customControls.Add("73af7677-2746-40ba-9dd2-4a084faa843d", this.epiButtonNewMtl);
		controlsHT.Add("73af7677-2746-40ba-9dd2-4a084faa843d", this.epiButtonNewMtl);
		this.epiButtonNewMtl.Name = "epiButtonNewMtl";
		this.epiButtonNewMtl.EpiGuid = "73af7677-2746-40ba-9dd2-4a084faa843d";
		this.epiButtonNewMtlAdd = new Ice.Lib.Framework.EpiButton();
		this.epiButtonNewMtlAdd_d096b4f8_d380_4154_85f1_84d650116c9d = this.epiButtonNewMtlAdd;
		customControls.Add("d096b4f8-d380-4154-85f1-84d650116c9d", this.epiButtonNewMtlAdd);
		controlsHT.Add("d096b4f8-d380-4154-85f1-84d650116c9d", this.epiButtonNewMtlAdd);
		this.epiButtonNewMtlAdd.Name = "epiButtonNewMtlAdd";
		this.epiButtonNewMtlAdd.EpiGuid = "d096b4f8-d380-4154-85f1-84d650116c9d";
		this.epiButtonNewMtlCancel = new Ice.Lib.Framework.EpiButton();
		this.epiButtonNewMtlCancel_f1f8821f_daa0_4dcb_8bd3_aaa832df35b6 = this.epiButtonNewMtlCancel;
		customControls.Add("f1f8821f-daa0-4dcb-8bd3-aaa832df35b6", this.epiButtonNewMtlCancel);
		controlsHT.Add("f1f8821f-daa0-4dcb-8bd3-aaa832df35b6", this.epiButtonNewMtlCancel);
		this.epiButtonNewMtlCancel.Name = "epiButtonNewMtlCancel";
		this.epiButtonNewMtlCancel.EpiGuid = "f1f8821f-daa0-4dcb-8bd3-aaa832df35b6";
		this.partRevisionCombo = Ice.Lib.Customization.Designers.RetrieverComboDesigner.CreateRetriever("041a1e65-2486-4b1b-9667-51418417ba8c", "PartRevSearch", "PartRevisionCombo", personalizeCustomizeManager);
		this.partRevisionCombo_041a1e65_2486_4b1b_9667_51418417ba8c = this.partRevisionCombo;
		customControls.Add("041a1e65-2486-4b1b-9667-51418417ba8c", this.partRevisionCombo);
		controlsHT.Add("041a1e65-2486-4b1b-9667-51418417ba8c", this.partRevisionCombo);
		this.partRevisionCombo.Name = "partRevisionCombo";
		this.partRevisionCombo.EpiGuid = "041a1e65-2486-4b1b-9667-51418417ba8c";
		this.epiCurrencyEditorMtlUnitCost = new Ice.Lib.Framework.EpiCurrencyEditor();
		this.epiCurrencyEditorMtlUnitCost_64da9d10_2340_4282_a118_f2e4ac7d90c6 = this.epiCurrencyEditorMtlUnitCost;
		customControls.Add("64da9d10-2340-4282-a118-f2e4ac7d90c6", this.epiCurrencyEditorMtlUnitCost);
		controlsHT.Add("64da9d10-2340-4282-a118-f2e4ac7d90c6", this.epiCurrencyEditorMtlUnitCost);
		this.epiCurrencyEditorMtlUnitCost.Name = "epiCurrencyEditorMtlUnitCost";
		this.epiCurrencyEditorMtlUnitCost.EpiGuid = "64da9d10-2340-4282-a118-f2e4ac7d90c6";
		this.epiComboPrtUOM = new Ice.Lib.Framework.EpiCombo();
		this.epiComboPrtUOM_5fd6900a_a201_4c96_af98_9420f214b1b0 = this.epiComboPrtUOM;
		customControls.Add("5fd6900a-a201-4c96-af98-9420f214b1b0", this.epiComboPrtUOM);
		controlsHT.Add("5fd6900a-a201-4c96-af98-9420f214b1b0", this.epiComboPrtUOM);
		this.epiComboPrtUOM.Name = "epiComboPrtUOM";
		this.epiComboPrtUOM.EpiGuid = "5fd6900a-a201-4c96-af98-9420f214b1b0";
		this.epiUltraGridNewMtl = new Ice.Lib.Framework.EpiUltraGrid();
		this.epiUltraGridNewMtl_a626e87f_6e7e_4ce8_ab1b_eda2a1a9a5c0 = this.epiUltraGridNewMtl;
		customControls.Add("a626e87f-6e7e-4ce8-ab1b-eda2a1a9a5c0", this.epiUltraGridNewMtl);
		controlsHT.Add("a626e87f-6e7e-4ce8-ab1b-eda2a1a9a5c0", this.epiUltraGridNewMtl);
		this.epiUltraGridNewMtl.Name = "epiUltraGridNewMtl";
		this.epiUltraGridNewMtl.EpiGuid = "a626e87f-6e7e-4ce8-ab1b-eda2a1a9a5c0";
		// AddMaterial
		this.AddMaterial.AutoScroll = true;
		System.Collections.Hashtable customSheets = personalizeCustomizeManager.CustControlMan.CustomSheetsHT;
		Infragistics.Win.UltraWinDock.DockableControlPane local1 = Ice.Lib.Customization.Designers.EpiCustomSheetDesigner.InitializeSheet(this.AddMaterial, "94b79866-def5-4fe7-9849-17e785738d77", customSheets);
		if ((local1 != null))
		{
			Ice.Lib.Customization.Designers.EpiDockManagerPanelDesigner.AddCustomDockManager(personalizeCustomizeManager, this.AddMaterial);
			local1.Text = "Add Material";
			local1.TextTab = "AddMaterial";
			Ice.Lib.Customization.Designers.EpiCustomSheetDesigner.AddCustomSheetToDockManager(personalizeCustomizeManager, local1, "baseDockManager2dcd1674-5e34-4d98-b493-c75747027376");
		}
		// AddMaterialDetail
		this.AddMaterialDetail.Controls.Add(this.epiGroupBoxMtl);
		this.AddMaterialDetail.Controls.SetChildIndex(this.epiGroupBoxMtl, 0);
		this.AddMaterialDetail.AutoScroll = true;
		Infragistics.Win.UltraWinDock.DockableControlPane local2 = Ice.Lib.Customization.Designers.EpiCustomSheetDesigner.InitializeSheet(this.AddMaterialDetail, "e8d61967-7821-4516-bca5-25e75214cb7a", customSheets);
		if ((local2 != null))
		{
			local2.Text = "Detail";
			local2.TextTab = "Detail";
			Ice.Lib.Customization.Designers.EpiCustomSheetDesigner.AddCustomSheetToDockManager(personalizeCustomizeManager, local2, "baseDockManager94b79866-def5-4fe7-9849-17e785738d77");
		}
		// AddMaterialList
		this.AddMaterialList.Controls.Add(this.epiUltraGridNewMtl);
		this.AddMaterialList.Controls.SetChildIndex(this.epiUltraGridNewMtl, 0);
		this.AddMaterialList.AutoScroll = true;
		Infragistics.Win.UltraWinDock.DockableControlPane local3 = Ice.Lib.Customization.Designers.EpiCustomSheetDesigner.InitializeSheet(this.AddMaterialList, "353be42e-7552-4bc6-b537-d5b0f75c8ef6", customSheets);
		if ((local3 != null))
		{
			local3.Text = "List";
			local3.TextTab = "List";
			Ice.Lib.Customization.Designers.EpiCustomSheetDesigner.AddCustomSheetToDockManager(personalizeCustomizeManager, local3, "baseDockManager94b79866-def5-4fe7-9849-17e785738d77");
		}
		// AddMaterialJob
		this.AddMaterialJob.Top = 23;
		this.AddMaterialJob.Left = 0;
		this.AddMaterialJob.Width = 2394;
		this.AddMaterialJob.Height = 374;
		this.AddMaterialJob.Controls.Add(this.epiGroupBoxAddMaterialToJob);
		this.AddMaterialJob.Controls.SetChildIndex(this.epiGroupBoxAddMaterialToJob, 0);
		this.AddMaterialJob.AutoScroll = true;
		Infragistics.Win.UltraWinDock.DockableControlPane local4 = Ice.Lib.Customization.Designers.EpiCustomSheetDesigner.InitializeSheet(this.AddMaterialJob, "9a03ed19-5474-4757-b871-759e8fb677b4", customSheets);
		if ((local4 != null))
		{
			local4.Text = "";
			local4.TextTab = "AddMaterialJob";
			Ice.Lib.Customization.Designers.EpiCustomSheetDesigner.AddCustomSheetToDockManager(personalizeCustomizeManager, local4, "baseDockManager94b79866-def5-4fe7-9849-17e785738d77");
		}
		// epiGroupBoxAddMaterialToJob
		this.epiGroupBoxAddMaterialToJob.Top = 0;
		this.epiGroupBoxAddMaterialToJob.Left = 0;
		this.epiGroupBoxAddMaterialToJob.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left);
		this.epiGroupBoxAddMaterialToJob.Width = 1484;
		this.epiGroupBoxAddMaterialToJob.Height = 214;
		this.epiGroupBoxAddMaterialToJob.Text = "To";
		this.epiGroupBoxAddMaterialToJob.Controls.Add(this.epiButtonAddMaterialToJob);
		this.epiGroupBoxAddMaterialToJob.Controls.Add(this.epiTextBoxAddMtlToJob);
		this.epiGroupBoxAddMaterialToJob.Controls.Add(this.epiButtonAddMtlJobAsmbly);
		this.epiGroupBoxAddMaterialToJob.Controls.Add(this.epiButtonAddMtlJobOprt);
		this.epiGroupBoxAddMaterialToJob.Controls.Add(this.epiTextBoxAddMtlJobPartNum);
		this.epiGroupBoxAddMaterialToJob.Controls.Add(this.epiTextBoxAddMtlJobAssmDesc);
		this.epiGroupBoxAddMaterialToJob.Controls.Add(this.epiButtonAddMtlJobAsmblDesc);
		this.epiGroupBoxAddMaterialToJob.Controls.Add(this.epiButtonAddMtlAssemDesc);
		this.epiGroupBoxAddMaterialToJob.Controls.Add(this.epiButtonAddMtlJobOpDesc);
		this.epiGroupBoxAddMaterialToJob.Controls.Add(this.epiTextBoxAddMtlAssemDesc);
		this.epiGroupBoxAddMaterialToJob.Controls.Add(this.epiTextBoxAddMtlJobMtlRelatedOperation);
		this.epiGroupBoxAddMaterialToJob.Controls.Add(this.epiTextBoxAddMtlJobAssmPart);
		this.epiGroupBoxAddMaterialToJob.Controls.Add(this.AddMtlJobAssmCombo);
		this.epiGroupBoxAddMaterialToJob.Controls.Add(this.epiComboAddMtlOperation);
		this.epiGroupBoxAddMaterialToJob.Controls.SetChildIndex(this.epiTextBoxAddMtlJobMtlRelatedOperation, 0);
		this.epiGroupBoxAddMaterialToJob.Controls.SetChildIndex(this.epiTextBoxAddMtlJobPartNum, 1);
		this.epiGroupBoxAddMaterialToJob.Controls.SetChildIndex(this.epiComboAddMtlOperation, 1);
		this.epiGroupBoxAddMaterialToJob.Controls.SetChildIndex(this.epiButtonAddMaterialToJob, 2);
		this.epiGroupBoxAddMaterialToJob.Controls.SetChildIndex(this.epiButtonAddMtlJobAsmblDesc, 2);
		this.epiGroupBoxAddMaterialToJob.Controls.SetChildIndex(this.AddMtlJobAssmCombo, 2);
		this.epiGroupBoxAddMaterialToJob.Controls.SetChildIndex(this.epiTextBoxAddMtlJobAssmDesc, 3);
		this.epiGroupBoxAddMaterialToJob.Controls.SetChildIndex(this.epiButtonAddMtlJobOpDesc, 3);
		this.epiGroupBoxAddMaterialToJob.Controls.SetChildIndex(this.epiButtonAddMtlJobOprt, 4);
		this.epiGroupBoxAddMaterialToJob.Controls.SetChildIndex(this.epiTextBoxAddMtlAssemDesc, 5);
		this.epiGroupBoxAddMaterialToJob.Controls.SetChildIndex(this.epiButtonAddMtlAssemDesc, 6);
		this.epiGroupBoxAddMaterialToJob.Controls.SetChildIndex(this.epiTextBoxAddMtlJobAssmPart, 7);
		this.epiGroupBoxAddMaterialToJob.Controls.SetChildIndex(this.epiButtonAddMtlJobAsmbly, 8);
		this.epiGroupBoxAddMaterialToJob.Controls.SetChildIndex(this.epiTextBoxAddMtlToJob, 13);
		// epiButtonAddMaterialToJob
		this.epiButtonAddMaterialToJob.Top = 41;
		this.epiButtonAddMaterialToJob.Left = 31;
		this.epiButtonAddMaterialToJob.Width = 95;
		this.epiButtonAddMaterialToJob.Height = 20;
		this.epiButtonAddMaterialToJob.Text = "Job..";
		this.epiButtonAddMaterialToJob.EpiBinding = "AddMtlToJob.btnJob";
		this.epiButtonAddMaterialToJob.EpiKeyField = true;
		// epiTextBoxAddMtlToJob
		this.epiTextBoxAddMtlToJob.Top = 41;
		this.epiTextBoxAddMtlToJob.Left = 144;
		this.epiTextBoxAddMtlToJob.Width = 149;
		this.epiTextBoxAddMtlToJob.Height = 20;
		this.epiTextBoxAddMtlToJob.Text = "";
		this.epiTextBoxAddMtlToJob.EpiBinding = "AddMtlToJob.JobNum";
		this.epiTextBoxAddMtlToJob.EpiLabel = "epiButtonAddMaterialToJob";
		this.epiTextBoxAddMtlToJob.EpiContextMenuKey = "JobHead.JobNum";
		this.epiTextBoxAddMtlToJob.EpiKeyField = true;
		// epiButtonAddMtlJobAsmbly
		this.epiButtonAddMtlJobAsmbly.Top = 74;
		this.epiButtonAddMtlJobAsmbly.Left = 35;
		this.epiButtonAddMtlJobAsmbly.Width = 95;
		this.epiButtonAddMtlJobAsmbly.Height = 20;
		this.epiButtonAddMtlJobAsmbly.Text = "Assembly";
		this.epiButtonAddMtlJobAsmbly.EpiBinding = "AddMtlToJob.btnAsm";
		// epiButtonAddMtlJobOprt
		this.epiButtonAddMtlJobOprt.Top = 109;
		this.epiButtonAddMtlJobOprt.Left = 34;
		this.epiButtonAddMtlJobOprt.Width = 95;
		this.epiButtonAddMtlJobOprt.Height = 20;
		this.epiButtonAddMtlJobOprt.Text = "Operation";
		this.epiButtonAddMtlJobOprt.EpiBinding = "AddMtlToJob.btnOpr";
		// epiTextBoxAddMtlJobPartNum
		this.epiTextBoxAddMtlJobPartNum.Top = 41;
		this.epiTextBoxAddMtlJobPartNum.Left = 325;
		this.epiTextBoxAddMtlJobPartNum.Width = 169;
		this.epiTextBoxAddMtlJobPartNum.Height = 20;
		this.epiTextBoxAddMtlJobPartNum.Text = "";
		this.epiTextBoxAddMtlJobPartNum.EpiBinding = "AddMtlToJob.JobHeadPartNum";
		this.epiTextBoxAddMtlJobPartNum.EpiContextMenuKey = "JobHead.PartNum";
		// epiTextBoxAddMtlJobAssmDesc
		this.epiTextBoxAddMtlJobAssmDesc.Top = 41;
		this.epiTextBoxAddMtlJobAssmDesc.Left = 668;
		this.epiTextBoxAddMtlJobAssmDesc.Width = 184;
		this.epiTextBoxAddMtlJobAssmDesc.Height = 20;
		this.epiTextBoxAddMtlJobAssmDesc.Text = "";
		this.epiTextBoxAddMtlJobAssmDesc.EpiBinding = "AddMtlToJob.JobAsmblPartDescription";
		this.epiTextBoxAddMtlJobAssmDesc.EpiContextMenuKey = "JobAsmbl.Descripion";
		// epiButtonAddMtlJobAsmblDesc
		this.epiButtonAddMtlJobAsmblDesc.Top = 41;
		this.epiButtonAddMtlJobAsmblDesc.Left = 541;
		this.epiButtonAddMtlJobAsmblDesc.Width = 96;
		this.epiButtonAddMtlJobAsmblDesc.Height = 20;
		((System.Windows.Forms.Control)(this.epiButtonAddMtlJobAsmblDesc)).Enabled = false;
		this.epiButtonAddMtlJobAsmblDesc.Text = "Description";
		// epiButtonAddMtlAssemDesc
		this.epiButtonAddMtlAssemDesc.Top = 74;
		this.epiButtonAddMtlAssemDesc.Left = 541;
		this.epiButtonAddMtlAssemDesc.Width = 96;
		this.epiButtonAddMtlAssemDesc.Height = 20;
		((System.Windows.Forms.Control)(this.epiButtonAddMtlAssemDesc)).Enabled = false;
		this.epiButtonAddMtlAssemDesc.Text = "Description";
		// epiButtonAddMtlJobOpDesc
		this.epiButtonAddMtlJobOpDesc.Top = 109;
		this.epiButtonAddMtlJobOpDesc.Left = 540;
		this.epiButtonAddMtlJobOpDesc.Width = 96;
		this.epiButtonAddMtlJobOpDesc.Height = 20;
		((System.Windows.Forms.Control)(this.epiButtonAddMtlJobOpDesc)).Enabled = false;
		this.epiButtonAddMtlJobOpDesc.Text = "Description";
		// epiTextBoxAddMtlAssemDesc
		this.epiTextBoxAddMtlAssemDesc.Top = 74;
		this.epiTextBoxAddMtlAssemDesc.Left = 668;
		this.epiTextBoxAddMtlAssemDesc.Width = 184;
		this.epiTextBoxAddMtlAssemDesc.Height = 20;
		this.epiTextBoxAddMtlAssemDesc.Text = "";
		this.epiTextBoxAddMtlAssemDesc.EpiBinding = "AddMtlToJob.AssemblyPartDesciption";
		// epiTextBoxAddMtlJobMtlRelatedOperation
		this.epiTextBoxAddMtlJobMtlRelatedOperation.Top = 109;
		this.epiTextBoxAddMtlJobMtlRelatedOperation.Left = 668;
		this.epiTextBoxAddMtlJobMtlRelatedOperation.Width = 184;
		this.epiTextBoxAddMtlJobMtlRelatedOperation.Height = 20;
		this.epiTextBoxAddMtlJobMtlRelatedOperation.Text = "";
		this.epiTextBoxAddMtlJobMtlRelatedOperation.EpiBinding = "AddMtlToJob.OpDesc";
		// epiTextBoxAddMtlJobAssmPart
		this.epiTextBoxAddMtlJobAssmPart.Top = 74;
		this.epiTextBoxAddMtlJobAssmPart.Left = 325;
		this.epiTextBoxAddMtlJobAssmPart.Width = 169;
		this.epiTextBoxAddMtlJobAssmPart.Height = 20;
		this.epiTextBoxAddMtlJobAssmPart.Text = "";
		this.epiTextBoxAddMtlJobAssmPart.EpiBinding = "AddMtlToJob.AssemblyPartNum";
		// erp.adapters.jobasmsearch.dll
		// erp.adapters.jobopersearch.dll
		// AddMtlJobAssmCombo
		this.AddMtlJobAssmCombo.Top = 74;
		this.AddMtlJobAssmCombo.Left = 144;
		this.AddMtlJobAssmCombo.Width = 149;
		this.AddMtlJobAssmCombo.Text = "";
		this.AddMtlJobAssmCombo.EpiBinding = "AddMtlToJob.AddMtlJobAssemSeq";
		// epiComboAddMtlOperation
		this.epiComboAddMtlOperation.Top = 106;
		this.epiComboAddMtlOperation.Left = 144;
		this.epiComboAddMtlOperation.Width = 149;
		this.epiComboAddMtlOperation.Text = "";
		this.epiComboAddMtlOperation.EpiBinding = "AddMtlToJob.OpCode";
		this.epiComboAddMtlOperation.AutoWidth = false;
		this.epiComboAddMtlOperation.AutoWidthOption = ((Ice.Lib.Framework.AutoWidthOptions)(1));
		this.epiComboAddMtlOperation.DisplayMember = "OprSeq";
		this.epiComboAddMtlOperation.EpiBOName = "Erp:BO:JobOperSearch";
		this.epiComboAddMtlOperation.EpiColumns = new string[] {
				"OprSeq",
				"OpCode",
				"OpCodeOpDesc"};
		this.epiComboAddMtlOperation.EpiDataSetMode = ((Ice.Lib.Searches.DataSetMode)(1));
		this.epiComboAddMtlOperation.EpiFilters = new string[] {
				"JobNum=\'?{JobCol}\'",
				"AssemblySeq=?{JobAsmCol,0}"};
		this.epiComboAddMtlOperation.EpiFiltersParams = new string[] {
				"JobCol =?[JobNum]",
				"JobAsmCol=?[AddMtlJobAssemSeq]"};
		this.epiComboAddMtlOperation.EpiSort = "OprSeq";
		this.epiComboAddMtlOperation.EpiTableName = "JobOper";
		this.epiComboAddMtlOperation.ValueMember = "OprSeq";
		this.epiComboAddMtlOperation.EpiAltSearchMethod = "";
		// erp.adapters.analysiscode.dll
		// epiGroupBoxMtl
		this.epiGroupBoxMtl.Top = 0;
		this.epiGroupBoxMtl.Left = 0;
		this.epiGroupBoxMtl.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left);
		this.epiGroupBoxMtl.Width = 1692;
		this.epiGroupBoxMtl.Height = 519;
		this.epiGroupBoxMtl.Text = "Material";
		this.epiGroupBoxMtl.Controls.Add(this.epiGroupBoxNewMtl);
		this.epiGroupBoxMtl.Controls.Add(this.epiGroupBoxQuantity);
		this.epiGroupBoxMtl.Controls.Add(this.epiButtonNewMtl);
		this.epiGroupBoxMtl.Controls.SetChildIndex(this.epiGroupBoxNewMtl, 0);
		this.epiGroupBoxMtl.Controls.SetChildIndex(this.epiGroupBoxQuantity, 1);
		this.epiGroupBoxMtl.Controls.SetChildIndex(this.epiButtonNewMtl, 2);
		// epiGroupBoxNewMtl
		this.epiGroupBoxNewMtl.Top = 88;
		this.epiGroupBoxNewMtl.Left = 18;
		this.epiGroupBoxNewMtl.Width = 1384;
		this.epiGroupBoxNewMtl.Height = 133;
		this.epiGroupBoxNewMtl.Text = "New Material";
		this.epiGroupBoxNewMtl.Controls.Add(this.epiLabelNewMtlSeq);
		this.epiGroupBoxNewMtl.Controls.Add(this.epiNumericEditorC1);
		this.epiGroupBoxNewMtl.Controls.Add(this.epiButtonMtlPartRev);
		this.epiGroupBoxNewMtl.Controls.Add(this.epiButtonNewMtlPartAttri);
		this.epiGroupBoxNewMtl.Controls.Add(this.epiTextBoxNewMtlPart);
		this.epiGroupBoxNewMtl.Controls.Add(this.epiTextBoxNewMtlAttri);
		this.epiGroupBoxNewMtl.Controls.Add(this.epiTextBoxNewMtlPrtDesc);
		this.epiGroupBoxNewMtl.Controls.Add(this.epiTextBoxNewMtlPrtAttDesc);
		this.epiGroupBoxNewMtl.Controls.Add(this.partRevisionCombo);
		this.epiGroupBoxNewMtl.Controls.SetChildIndex(this.epiTextBoxNewMtlPrtDesc, 0);
		this.epiGroupBoxNewMtl.Controls.SetChildIndex(this.epiTextBoxNewMtlAttri, 1);
		this.epiGroupBoxNewMtl.Controls.SetChildIndex(this.epiTextBoxNewMtlPrtAttDesc, 1);
		this.epiGroupBoxNewMtl.Controls.SetChildIndex(this.epiTextBoxNewMtlPart, 2);
		this.epiGroupBoxNewMtl.Controls.SetChildIndex(this.partRevisionCombo, 3);
		this.epiGroupBoxNewMtl.Controls.SetChildIndex(this.epiButtonNewMtlPartAttri, 4);
		this.epiGroupBoxNewMtl.Controls.SetChildIndex(this.epiButtonMtlPartRev, 5);
		this.epiGroupBoxNewMtl.Controls.SetChildIndex(this.epiNumericEditorC1, 7);
		this.epiGroupBoxNewMtl.Controls.SetChildIndex(this.epiLabelNewMtlSeq, 8);
		// epiGroupBoxQuantity
		this.epiGroupBoxQuantity.Top = 244;
		this.epiGroupBoxQuantity.Left = 18;
		this.epiGroupBoxQuantity.Width = 1386;
		this.epiGroupBoxQuantity.Height = 251;
		this.epiGroupBoxQuantity.Text = "Quantity";
		this.epiGroupBoxQuantity.Controls.Add(this.epiLabelQtyParent);
		this.epiGroupBoxQuantity.Controls.Add(this.epiNumericEditorQtyParent);
		this.epiGroupBoxQuantity.Controls.Add(this.epiLabelReqQty);
		this.epiGroupBoxQuantity.Controls.Add(this.epiNumericEditorReqQty);
		this.epiGroupBoxQuantity.Controls.Add(this.epiLabelUnitCost);
		this.epiGroupBoxQuantity.Controls.Add(this.epiTextBoxReqUOM);
		this.epiGroupBoxQuantity.Controls.Add(this.epiTextBoxUnitCostUOM);
		this.epiGroupBoxQuantity.Controls.Add(this.epiLabelFixedQty);
		this.epiGroupBoxQuantity.Controls.Add(this.epiCheckBoxFixedQty);
		this.epiGroupBoxQuantity.Controls.Add(this.epiButtonNewMtlAdd);
		this.epiGroupBoxQuantity.Controls.Add(this.epiButtonNewMtlCancel);
		this.epiGroupBoxQuantity.Controls.Add(this.epiCurrencyEditorMtlUnitCost);
		this.epiGroupBoxQuantity.Controls.Add(this.epiComboPrtUOM);
		this.epiGroupBoxQuantity.Controls.SetChildIndex(this.epiComboPrtUOM, 0);
		this.epiGroupBoxQuantity.Controls.SetChildIndex(this.epiCurrencyEditorMtlUnitCost, 1);
		this.epiGroupBoxQuantity.Controls.SetChildIndex(this.epiLabelUnitCost, 2);
		this.epiGroupBoxQuantity.Controls.SetChildIndex(this.epiNumericEditorQtyParent, 3);
		this.epiGroupBoxQuantity.Controls.SetChildIndex(this.epiLabelQtyParent, 4);
		this.epiGroupBoxQuantity.Controls.SetChildIndex(this.epiTextBoxUnitCostUOM, 5);
		this.epiGroupBoxQuantity.Controls.SetChildIndex(this.epiTextBoxReqUOM, 6);
		this.epiGroupBoxQuantity.Controls.SetChildIndex(this.epiNumericEditorReqQty, 7);
		this.epiGroupBoxQuantity.Controls.SetChildIndex(this.epiLabelReqQty, 8);
		this.epiGroupBoxQuantity.Controls.SetChildIndex(this.epiCheckBoxFixedQty, 9);
		this.epiGroupBoxQuantity.Controls.SetChildIndex(this.epiLabelFixedQty, 10);
		this.epiGroupBoxQuantity.Controls.SetChildIndex(this.epiButtonNewMtlCancel, 11);
		this.epiGroupBoxQuantity.Controls.SetChildIndex(this.epiButtonNewMtlAdd, 12);
		// epiLabelNewMtlSeq
		this.epiLabelNewMtlSeq.Top = 33;
		this.epiLabelNewMtlSeq.Left = 78;
		this.epiLabelNewMtlSeq.Width = 60;
		this.epiLabelNewMtlSeq.Height = 20;
		this.epiLabelNewMtlSeq.Text = "Mtl Seq:";
		// epiNumericEditorC1
		this.epiNumericEditorC1.Top = 33;
		this.epiNumericEditorC1.Left = 144;
		this.epiNumericEditorC1.Width = 73;
		this.epiNumericEditorC1.EpiBinding = "Material.MtlSeq";
		this.epiNumericEditorC1.EpiLabel = "epiLabelNewMtlSeq";
		this.epiNumericEditorC1.Nullable = true;
		// epiButtonMtlPartRev
		this.epiButtonMtlPartRev.Top = 62;
		this.epiButtonMtlPartRev.Left = 26;
		this.epiButtonMtlPartRev.Width = 110;
		this.epiButtonMtlPartRev.Height = 20;
		this.epiButtonMtlPartRev.Text = "Part/Rev..";
		this.epiButtonMtlPartRev.EpiBinding = "Material.btnPartRev";
		// epiButtonNewMtlPartAttri
		this.epiButtonNewMtlPartAttri.Top = 94;
		this.epiButtonNewMtlPartAttri.Left = 24;
		this.epiButtonNewMtlPartAttri.Width = 110;
		this.epiButtonNewMtlPartAttri.Height = 20;
		this.epiButtonNewMtlPartAttri.Text = "Attribute Set....";
		this.epiButtonNewMtlPartAttri.EpiBinding = "Material.btnAttribute";
		// epiTextBoxNewMtlPart
		this.epiTextBoxNewMtlPart.Top = 63;
		this.epiTextBoxNewMtlPart.Left = 144;
		this.epiTextBoxNewMtlPart.Width = 155;
		this.epiTextBoxNewMtlPart.Height = 20;
		this.epiTextBoxNewMtlPart.Text = "";
		this.epiTextBoxNewMtlPart.EpiBinding = "Material.PartNum";
		// epiTextBoxNewMtlAttri
		this.epiTextBoxNewMtlAttri.Top = 95;
		this.epiTextBoxNewMtlAttri.Left = 144;
		this.epiTextBoxNewMtlAttri.Width = 251;
		this.epiTextBoxNewMtlAttri.Height = 20;
		this.epiTextBoxNewMtlAttri.Text = "";
		this.epiTextBoxNewMtlAttri.EpiBinding = "Material.Attribute";
		this.epiTextBoxNewMtlAttri.EpiContextMenuKey = "DynAttrValueSet.ShortDescription";
		// epiTextBoxNewMtlPrtDesc
		this.epiTextBoxNewMtlPrtDesc.Top = 63;
		this.epiTextBoxNewMtlPrtDesc.Left = 430;
		this.epiTextBoxNewMtlPrtDesc.Width = 219;
		this.epiTextBoxNewMtlPrtDesc.Height = 20;
		this.epiTextBoxNewMtlPrtDesc.Text = "";
		this.epiTextBoxNewMtlPrtDesc.EpiBinding = "Material.PartDesc";
		// epiTextBoxNewMtlPrtAttDesc
		this.epiTextBoxNewMtlPrtAttDesc.Top = 95;
		this.epiTextBoxNewMtlPrtAttDesc.Left = 430;
		this.epiTextBoxNewMtlPrtAttDesc.Width = 219;
		this.epiTextBoxNewMtlPrtAttDesc.Height = 20;
		this.epiTextBoxNewMtlPrtAttDesc.Text = "";
		this.epiTextBoxNewMtlPrtAttDesc.EpiBinding = "Material.AttrDesc";
		// epiLabelQtyParent
		this.epiLabelQtyParent.Top = 37;
		this.epiLabelQtyParent.Left = 25;
		this.epiLabelQtyParent.Width = 73;
		this.epiLabelQtyParent.Height = 20;
		this.epiLabelQtyParent.Text = "Qty/Parent:";
		// epiNumericEditorQtyParent
		this.epiNumericEditorQtyParent.NumericType = ((Infragistics.Win.UltraWinEditors.NumericType)(2));
		this.epiNumericEditorQtyParent.MaskInput = "nnnnn.nnnnnnnn";
		this.epiNumericEditorQtyParent.Top = 37;
		this.epiNumericEditorQtyParent.Left = 104;
		this.epiNumericEditorQtyParent.Width = 154;
		this.epiNumericEditorQtyParent.EpiBinding = "Material.QtyParent";
		this.epiNumericEditorQtyParent.EpiLabel = "epiLabelQtyParent";
		this.epiNumericEditorQtyParent.Nullable = true;
		// epiLabelReqQty
		this.epiLabelReqQty.Top = 70;
		this.epiLabelReqQty.Left = 1;
		this.epiLabelReqQty.Width = 97;
		this.epiLabelReqQty.Height = 20;
		this.epiLabelReqQty.Text = "Required Qty:";
		// epiNumericEditorReqQty
		this.epiNumericEditorReqQty.MaskInput = "nnn,nnn,nnn,nnn";
		this.epiNumericEditorReqQty.Top = 70;
		this.epiNumericEditorReqQty.Left = 104;
		this.epiNumericEditorReqQty.Width = 154;
		this.epiNumericEditorReqQty.EpiBinding = "Material.ReqQty";
		this.epiNumericEditorReqQty.EpiLabel = "epiLabelReqQty";
		this.epiNumericEditorReqQty.Nullable = true;
		this.epiNumericEditorReqQty.FormatString = "###,###,###,##0.00";
		// epiLabelUnitCost
		this.epiLabelUnitCost.Top = 101;
		this.epiLabelUnitCost.Left = 38;
		this.epiLabelUnitCost.Width = 60;
		this.epiLabelUnitCost.Height = 20;
		this.epiLabelUnitCost.Text = "Unit Cost:";
		// epiTextBoxReqUOM
		this.epiTextBoxReqUOM.Top = 73;
		this.epiTextBoxReqUOM.Left = 292;
		this.epiTextBoxReqUOM.Width = 68;
		this.epiTextBoxReqUOM.Height = 20;
		this.epiTextBoxReqUOM.Text = "";
		this.epiTextBoxReqUOM.EpiBinding = "Material.ReqQtyUOM";
		// epiTextBoxUnitCostUOM
		this.epiTextBoxUnitCostUOM.Top = 101;
		this.epiTextBoxUnitCostUOM.Left = 292;
		this.epiTextBoxUnitCostUOM.Width = 68;
		this.epiTextBoxUnitCostUOM.Height = 20;
		this.epiTextBoxUnitCostUOM.Text = "";
		this.epiTextBoxUnitCostUOM.EpiBinding = "Material.UnitQtyUOM";
		// epiLabelFixedQty
		this.epiLabelFixedQty.Top = 39;
		this.epiLabelFixedQty.Left = 433;
		this.epiLabelFixedQty.Width = 77;
		this.epiLabelFixedQty.Height = 20;
		this.epiLabelFixedQty.Text = "Fixed Qty:";
		// epiCheckBoxFixedQty
		this.epiCheckBoxFixedQty.Top = 39;
		this.epiCheckBoxFixedQty.Left = 516;
		this.epiCheckBoxFixedQty.Width = 16;
		this.epiCheckBoxFixedQty.Height = 17;
		this.epiCheckBoxFixedQty.EpiBinding = "Material.FixedQty";
		this.epiCheckBoxFixedQty.EpiLabel = "epiLabelFixedQty";
		// epiButtonNewMtl
		this.epiButtonNewMtl.Top = 39;
		this.epiButtonNewMtl.Left = 38;
		this.epiButtonNewMtl.Width = 92;
		this.epiButtonNewMtl.Height = 20;
		this.epiButtonNewMtl.Text = "New Mtl..";
		// epiButtonNewMtlAdd
		this.epiButtonNewMtlAdd.Top = 166;
		this.epiButtonNewMtlAdd.Left = 53;
		this.epiButtonNewMtlAdd.Width = 102;
		this.epiButtonNewMtlAdd.Height = 22;
		this.epiButtonNewMtlAdd.Text = "Add";
		this.epiButtonNewMtlAdd.EpiBinding = "Material.btnAdd";
		// epiButtonNewMtlCancel
		this.epiButtonNewMtlCancel.Top = 166;
		this.epiButtonNewMtlCancel.Left = 201;
		this.epiButtonNewMtlCancel.Width = 102;
		this.epiButtonNewMtlCancel.Height = 22;
		this.epiButtonNewMtlCancel.Text = "Cancel";
		this.epiButtonNewMtlCancel.EpiBinding = "Material.btnCancel";
		// partRevisionCombo
		this.partRevisionCombo.Top = 63;
		this.partRevisionCombo.Left = 312;
		this.partRevisionCombo.Width = 76;
		this.partRevisionCombo.Text = "";
		this.partRevisionCombo.EpiBinding = "Material.PartRev";
		// erp.adapters.partrevsearch.dll
		// epiCurrencyEditorMtlUnitCost
		this.epiCurrencyEditorMtlUnitCost.Top = 101;
		this.epiCurrencyEditorMtlUnitCost.Left = 104;
		this.epiCurrencyEditorMtlUnitCost.Width = 154;
		this.epiCurrencyEditorMtlUnitCost.EpiBinding = "Material.UnitCost";
		this.epiCurrencyEditorMtlUnitCost.EpiLabel = "epiLabelUnitCost";
		// epiComboPrtUOM
		this.epiComboPrtUOM.Top = 37;
		this.epiComboPrtUOM.Left = 292;
		this.epiComboPrtUOM.Width = 68;
		this.epiComboPrtUOM.Text = "";
		this.epiComboPrtUOM.EpiBinding = "Material.QtyParentUOM";
		this.epiComboPrtUOM.AutoWidth = false;
		this.epiComboPrtUOM.AutoWidthOption = ((Ice.Lib.Framework.AutoWidthOptions)(1));
		this.epiComboPrtUOM.DescColumnName = "";
		this.epiComboPrtUOM.DisplayMember = "UOMCode";
		this.epiComboPrtUOM.EpiBOName = "Erp:BO:UOMSearch";
		this.epiComboPrtUOM.EpiColumns = new string[] {
				"UOMCode",
				"UOMDesc"};
		this.epiComboPrtUOM.EpiFilters = new string[] {
				"Active=True",
				"UOMClassID = \'?{UOMClassIDColName}\'",
				"PartNum = \'?{PartNumColName}\'",
				"TargetUOMCode = \'?{TargetUOMCodeColName}\'",
				"StockOnly = ?{StockOnly,false}",
				"UOMClassID = ?{UOMClassID}",
				"RetrieveAll = ?{RetrieveAll,false}"};
		this.epiComboPrtUOM.EpiFiltersParams = new string[] {
				"TargetUOMCodeColName=?[]",
				"UOMClassID=",
				"PartNumColName=?[PartNum]",
				"RetrieveAll=",
				"UOMClassIDColName=?[]",
				"StockOnly=true"};
		this.epiComboPrtUOM.EpiSort = "UOMCode";
		this.epiComboPrtUOM.EpiTableName = "UOMConvList";
		this.epiComboPrtUOM.ValueMember = "UOMCode";
		this.epiComboPrtUOM.EpiAltSearchMethod = "";
		// epiUltraGridNewMtl
		this.epiUltraGridNewMtl.Top = 0;
		this.epiUltraGridNewMtl.Left = 0;
		this.epiUltraGridNewMtl.Width = 1737;
		this.epiUltraGridNewMtl.Height = 629;
		this.epiUltraGridNewMtl.Text = "New Mtl";
		this.epiUltraGridNewMtl.EpiBinding = "Material";
		// erp.adapters.inventoryattributesearch.dll
		// erp.contracts.bo.inventoryattributesearch.dll
		// erp.adapters.jobentry.dll
		// erp.contracts.bo.jobentry.dll
		// erp.adapters.scheduleengine.dll
		// erp.contracts.bo.scheduleengine.dll
		// IssueMaterialForm
		Ice.Lib.Framework.EpiBaseForm local5 = ((Ice.Lib.Framework.EpiBaseForm)(personalizeCustomizeManager.ControlsHT["2dcd1674-5e34-4d98-b493-c75747027376"]));
		local5.Top = -8;
		local5.Left = -8;
		local5.Width = 2576;
		local5.Height = 1416;
		local5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
		// grpTo
		Ice.Lib.Framework.EpiGroupBox local6 = ((Ice.Lib.Framework.EpiGroupBox)(personalizeCustomizeManager.ControlsHT["6386495a-8b7d-43b4-ae7d-34ae4f6f8dba"]));
		Ice.Lib.Framework.EpiCombo local7 = ((Ice.Lib.Framework.EpiCombo)(personalizeCustomizeManager.ControlsHT["3c29bb2b-cc78-4cab-80c8-f004923fdbae"]));
		local6.Controls.SetChildIndex(local7, 0);
		Ice.Lib.Framework.EpiCombo local8 = ((Ice.Lib.Framework.EpiCombo)(personalizeCustomizeManager.ControlsHT["205cc183-a4c5-400d-8f94-36c886d62b6d"]));
		local6.Controls.SetChildIndex(local8, 1);
		Ice.Lib.Framework.EpiButton local9 = ((Ice.Lib.Framework.EpiButton)(personalizeCustomizeManager.ControlsHT["4c485316-6da1-4324-aaa6-d220ebca1b44"]));
		local6.Controls.SetChildIndex(local9, 2);
		Ice.Lib.Framework.EpiTextBox local10 = ((Ice.Lib.Framework.EpiTextBox)(personalizeCustomizeManager.ControlsHT["7f0f5455-24a8-4582-aca4-333041b6fe31"]));
		local6.Controls.SetChildIndex(local10, 3);
		Ice.Lib.Framework.EpiTextBox local11 = ((Ice.Lib.Framework.EpiTextBox)(personalizeCustomizeManager.ControlsHT["6e38f847-830d-400f-a3bd-937a4f1aee04"]));
		local6.Controls.SetChildIndex(local11, 4);
		Ice.Lib.Framework.EpiTextBox local12 = ((Ice.Lib.Framework.EpiTextBox)(personalizeCustomizeManager.ControlsHT["729c1e04-c080-456a-bc8b-fadc97dd989e"]));
		local6.Controls.SetChildIndex(local12, 5);
		// txtToJob
		Ice.Lib.Framework.EpiTextBox local13 = ((Ice.Lib.Framework.EpiTextBox)(personalizeCustomizeManager.ControlsHT["8eb12134-2ebc-448b-b3db-9ab81ecc4f66"]));
		local13.EpiBinding = "IM.ToJobNum";
		// cmbAssm
		// cmbMtl
		// txtToAssmPart
		// txtToMtlPart
		// txtToJobPart
		// btnToAssm
		// issueMtlList1
		Ice.Lib.Framework.EpiBasePanel local14 = ((Ice.Lib.Framework.EpiBasePanel)(personalizeCustomizeManager.ControlsHT["e595d872-7e91-4408-955b-22412b20a4e0"]));
		Ice.Lib.Framework.EpiUltraGrid local15 = ((Ice.Lib.Framework.EpiUltraGrid)(personalizeCustomizeManager.ControlsHT["52f78469-2c71-450d-ae80-c3deaee9e8b7"]));
		local14.Controls.SetChildIndex(local15, 0);
		System.Collections.Hashtable nativeSheets = personalizeCustomizeManager.NativeSheetsDCPsHT;
		Infragistics.Win.UltraWinDock.DockableControlPane local16 = Ice.Lib.Customization.Designers.EpiCustomSheetDesigner.GetDockableControlPane(local14, "e595d872-7e91-4408-955b-22412b20a4e0", nativeSheets);
		// ugdIssueMtl
		local15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
		// grpFrom
		Ice.Lib.Framework.EpiGroupBox local17 = ((Ice.Lib.Framework.EpiGroupBox)(personalizeCustomizeManager.ControlsHT["b69ca345-2830-4145-9042-c07811a2c839"]));
		Ice.Lib.Framework.EpiCombo local18 = ((Ice.Lib.Framework.EpiCombo)(personalizeCustomizeManager.ControlsHT["f6d3a633-0d74-40be-b1c1-fbbe20532fe2"]));
		local17.Controls.SetChildIndex(local18, 0);
		// cbouom
		// Finishing control initialization.
		topControl.FindForm().ResumeLayout();
	}

	public void DestroyGlobalVariables()
	{
		this.AddMaterial = null;
		this.AddMaterial_94b79866_def5_4fe7_9849_17e785738d77 = null;
		this.AddMaterialDetail = null;
		this.AddMaterialDetail_e8d61967_7821_4516_bca5_25e75214cb7a = null;
		this.AddMaterialList = null;
		this.AddMaterialList_353be42e_7552_4bc6_b537_d5b0f75c8ef6 = null;
		this.AddMaterialJob = null;
		this.AddMaterialJob_9a03ed19_5474_4757_b871_759e8fb677b4 = null;
		this.epiGroupBoxAddMaterialToJob = null;
		this.epiGroupBoxAddMaterialToJob_820c0d42_d6d6_4c8f_87bb_e6551a409a1d = null;
		this.epiButtonAddMaterialToJob = null;
		this.epiButtonAddMaterialToJob_8313e6d9_09d1_4ee9_9afa_78800c2020ab = null;
		this.epiTextBoxAddMtlToJob = null;
		this.epiTextBoxAddMtlToJob_780fcd2c_a59b_41da_a21b_21461741466b = null;
		this.epiButtonAddMtlJobAsmbly = null;
		this.epiButtonAddMtlJobAsmbly_1a13222c_3a89_4917_8623_964cacf80dfd = null;
		this.epiButtonAddMtlJobOprt = null;
		this.epiButtonAddMtlJobOprt_5acc2861_5ec5_4fc9_97e0_13c9f41f2c0a = null;
		this.epiTextBoxAddMtlJobPartNum = null;
		this.epiTextBoxAddMtlJobPartNum_78122d04_c277_477e_9a73_1ec4a6c8ccc4 = null;
		this.epiTextBoxAddMtlJobAssmDesc = null;
		this.epiTextBoxAddMtlJobAssmDesc_581bfe79_ed3e_4afb_ab76_48acd3781d29 = null;
		this.epiButtonAddMtlJobAsmblDesc = null;
		this.epiButtonAddMtlJobAsmblDesc_49d535fa_7c8e_4b55_9bd6_8941fc1c802f = null;
		this.epiButtonAddMtlAssemDesc = null;
		this.epiButtonAddMtlAssemDesc_e3e50708_f001_4228_a8b1_6578cfec4db0 = null;
		this.epiButtonAddMtlJobOpDesc = null;
		this.epiButtonAddMtlJobOpDesc_adf5458c_fe7e_4764_9e79_a3e0df6713ca = null;
		this.epiTextBoxAddMtlAssemDesc = null;
		this.epiTextBoxAddMtlAssemDesc_d6d097bf_1e01_4b4b_bc97_f222444a722e = null;
		this.epiTextBoxAddMtlJobMtlRelatedOperation = null;
		this.epiTextBoxAddMtlJobMtlRelatedOperation_07d99791_8075_4157_a296_c36366795760 = null;
		this.epiTextBoxAddMtlJobAssmPart = null;
		this.epiTextBoxAddMtlJobAssmPart_3799e553_80f4_4bfa_bfa5_3a81cef961ed = null;
		this.AddMtlJobAssmCombo = null;
		this.AddMtlJobAssmCombo_f7ce82a6_d527_4791_a971_70ae239fe6ac = null;
		this.epiComboAddMtlOperation = null;
		this.epiComboAddMtlOperation_2fe7b06d_b4ed_49bb_bdfd_f4565fd6411c = null;
		this.epiGroupBoxMtl = null;
		this.epiGroupBoxMtl_1fc66d6d_50ab_4464_bdd5_a83736fe38e1 = null;
		this.epiGroupBoxNewMtl = null;
		this.epiGroupBoxNewMtl_38b22734_ffcc_4725_b68a_3bbd00157a39 = null;
		this.epiGroupBoxQuantity = null;
		this.epiGroupBoxQuantity_76a74529_4c48_490c_8d39_5abb83c9a21f = null;
		this.epiLabelNewMtlSeq = null;
		this.epiLabelNewMtlSeq_ac1b70a4_bc78_4bbc_9330_8ca431000848 = null;
		this.epiNumericEditorC1 = null;
		this.epiNumericEditorC1_3660592e_b84a_4f03_a655_8da9c97b4979 = null;
		this.epiButtonMtlPartRev = null;
		this.epiButtonMtlPartRev_3dc12e34_2aac_4c2a_a102_5f7a8188ebc9 = null;
		this.epiButtonNewMtlPartAttri = null;
		this.epiButtonNewMtlPartAttri_c3173445_2963_4ddc_b0aa_59857f218003 = null;
		this.epiTextBoxNewMtlPart = null;
		this.epiTextBoxNewMtlPart_8b322e62_e8d0_412e_994e_3fa3e2213b5b = null;
		this.epiTextBoxNewMtlAttri = null;
		this.epiTextBoxNewMtlAttri_c8a4168e_4fee_43e8_97ad_5567dfb96521 = null;
		this.epiTextBoxNewMtlPrtDesc = null;
		this.epiTextBoxNewMtlPrtDesc_d1a1c723_85cf_4874_8d96_141ce07f8b26 = null;
		this.epiTextBoxNewMtlPrtAttDesc = null;
		this.epiTextBoxNewMtlPrtAttDesc_cb485618_7202_4176_b241_7bb1e0b4fb27 = null;
		this.epiLabelQtyParent = null;
		this.epiLabelQtyParent_d8d9deaf_0415_4a0b_80e3_73c3a5908078 = null;
		this.epiNumericEditorQtyParent = null;
		this.epiNumericEditorQtyParent_54d9e1c7_cdf2_4824_bcc6_c6cba2be5944 = null;
		this.epiLabelReqQty = null;
		this.epiLabelReqQty_1cbfaeaf_3f61_454c_b2ec_df5e021d5f96 = null;
		this.epiNumericEditorReqQty = null;
		this.epiNumericEditorReqQty_9db2022d_d97b_42b2_a99c_327a477fbcb5 = null;
		this.epiLabelUnitCost = null;
		this.epiLabelUnitCost_361d90d8_0e6d_4998_85ec_8f18de7580f1 = null;
		this.epiTextBoxReqUOM = null;
		this.epiTextBoxReqUOM_3399adea_451d_460d_8679_c1a40bcd2560 = null;
		this.epiTextBoxUnitCostUOM = null;
		this.epiTextBoxUnitCostUOM_9bf2c373_371c_4143_8126_992bc1fc0d25 = null;
		this.epiLabelFixedQty = null;
		this.epiLabelFixedQty_4727cc0b_3a5b_4311_a9de_e862ea9ebd17 = null;
		this.epiCheckBoxFixedQty = null;
		this.epiCheckBoxFixedQty_a03a7a5a_3fe8_40f1_bbe3_e257cc902877 = null;
		this.epiButtonNewMtl = null;
		this.epiButtonNewMtl_73af7677_2746_40ba_9dd2_4a084faa843d = null;
		this.epiButtonNewMtlAdd = null;
		this.epiButtonNewMtlAdd_d096b4f8_d380_4154_85f1_84d650116c9d = null;
		this.epiButtonNewMtlCancel = null;
		this.epiButtonNewMtlCancel_f1f8821f_daa0_4dcb_8bd3_aaa832df35b6 = null;
		this.partRevisionCombo = null;
		this.partRevisionCombo_041a1e65_2486_4b1b_9667_51418417ba8c = null;
		this.epiCurrencyEditorMtlUnitCost = null;
		this.epiCurrencyEditorMtlUnitCost_64da9d10_2340_4282_a118_f2e4ac7d90c6 = null;
		this.epiComboPrtUOM = null;
		this.epiComboPrtUOM_5fd6900a_a201_4c96_af98_9420f214b1b0 = null;
		this.epiUltraGridNewMtl = null;
		this.epiUltraGridNewMtl_a626e87f_6e7e_4ce8_ab1b_eda2a1a9a5c0 = null;
		this.csm = null;
		this.oTrans = null;
		this.IssueMaterialForm = null;
		this.baseToolbarsManager = null;
		this.Client_Column = null;
		this.CallContextClientData_Row = null;
		this.SelectedJobAsmbl_Column = null;
		this.SelectList_Row = null;
		this.BpmData_Column = null;
		this.CallContextBpmData_Row = null;
		this.IssueReturn_Column = null;
		this.IM_Row = null;
		this.DoneTable_Column = null;
		this.Done_Row = null;
		this.IssueReturnJobAsmbl_Column = null;
		this.Select_Row = null;
	}

	public static string[] GetTranslatableStrings()
	{
		return new string[] {
				"Add Material",
				"AddMaterial",
				"Detail",
				"List",
				"",
				"AddMaterialJob",
				"To",
				"Job..",
				"Assembly",
				"Operation",
				"Description",
				"Material",
				"New Material",
				"Quantity",
				"Mtl Seq:",
				"Part/Rev..",
				"Attribute Set....",
				"Qty/Parent:",
				"Required Qty:",
				"Unit Cost:",
				"Fixed Qty:",
				"New Mtl..",
				"Add",
				"Cancel",
				"New Mtl"};
	}

	public static string GetStringByID(string id)
	{
		return "";
	}
	// ** Wizard Insert Location - Do Not Remove 'Begin/End Wizard Added Module Level Variables' Comments! **
	// Begin Wizard Added Module Level Variables **

	// End Wizard Added Module Level Variables **

	// Add Custom Module Level Variables Here **
       private Dictionary<string, List<Tuple<string, string, Type>>> edvDic;
       private EpiDataView edvAddMtlToJob,edvNewMaterial;
       private Erp.UI.Controls.Combos.JobAsmSearchCombo JobAsmSearchCombo;
       private InventoryAttributeSearchAdapter inventoryAttributeSearchAdapter;
       private JobEntryAdapter jobEntryAdapter;
       private ScheduleEngineAdapter scheduleEngineAdapter;
       private Ice.Core.Session session;
       //private
	public void InitializeCustomCode()
	{
		// ** Wizard Insert Location - Do not delete 'Begin/End Wizard Added Variable Initialization' lines **
		// Begin Wizard Added Variable Initialization
           this.session = (Ice.Core.Session)this.oTrans.Session;
           this.inventoryAttributeSearchAdapter = new InventoryAttributeSearchAdapter(this.oTrans);
           this.jobEntryAdapter = new JobEntryAdapter(this.oTrans);
           this.scheduleEngineAdapter= new ScheduleEngineAdapter(this.oTrans);
           try
           {
             this.inventoryAttributeSearchAdapter.BOConnect();
             this.jobEntryAdapter.BOConnect();
             this.scheduleEngineAdapter.BOConnect();
           }
           catch (Exception ex)
           {
             ExceptionBox.Show(ex,EpiString.GetString("BOConnectError"));
           }
           
           InitEpiDataViewDic();
		   CreateEpiDataView();
          // AddJobAsmSearchCombo();
           this.edvAddMtlToJob = this.oTrans.Factory("AddMtlToJob");
           this.edvNewMaterial = this.oTrans.Factory("Material");
         
		this.IssueMaterialForm.BeforeToolClick += new Ice.Lib.Framework.BeforeToolClickEventHandler(this.IssueMaterialForm_BeforeToolClick);
		// End Wizard Added Variable Initialization

		// Begin Wizard Added Custom Method Calls
        SetExtendedProperties();
		this.epiButtonAddMaterialToJob.Click += new System.EventHandler(this.epiButtonAddMaterialToJob_Click);
		this.epiButtonAddMtlJobOprt.Click += new System.EventHandler(this.epiButtonAddMtlJobOprt_Click);
		this.epiButtonAddMtlJobAsmbly.Click += new System.EventHandler(this.epiButtonAddMtlJobAsmbly_Click);
        this.epiTextBoxAddMtlToJob.AfterExitEditMode += new System.EventHandler(this.epiTextBoxAddMtlToJob_AfterExitEditMode);
		this.AddMtlJobAssmCombo.BeforeDropDown += new System.ComponentModel.CancelEventHandler(this.AddMtlJobAssmCombo_BeforeDropDown);
        this.partRevisionCombo.BeforeDropDown += new System.ComponentModel.CancelEventHandler(this.partRevisionCombo_BeforeDropDown);
		this.epiButtonNewMtl.Click += new System.EventHandler(this.epiButtonNewMtl_Click);
		this.epiButtonMtlPartRev.Click += new System.EventHandler(this.epiButtonMtlPartRev_Click);
		this.epiButtonNewMtlPartAttri.Click += new System.EventHandler(this.epiButtonNewMtlPartAttri_Click);
		this.epiButtonNewMtlAdd.Click += new System.EventHandler(this.epiButtonNewMtlAdd_Click);
		this.epiButtonNewMtlCancel.Click += new System.EventHandler(this.epiButtonNewMtlCancel_Click);
        this.AddMtlJobAssmCombo.ValueChanged += new System.EventHandler(this.AddMtlJobAssmCombo_ValueChanged);
        this.epiComboAddMtlOperation.ValueChanged += new System.EventHandler(this.epiComboAddMtlOperation_ValueChanged);
        this.epiTextBoxNewMtlPart.AfterExitEditMode += new System.EventHandler(epiTextBoxNewMtlPart_AfterExitEditMode);
        this.epiComboPrtUOM.ValueChanged += new System.EventHandler(this.epiComboPrtUOM_ValueChanged);
		CreateRowRuleMaterialTrackInventoryAttributesEquals_false();;
		CreateRowRuleMaterialAttributeEquals_false();;
		// End Wizard Added Custom Method Calls
	}

	public void DestroyCustomCode()
	{
		// ** Wizard Insert Location - Do not delete 'Begin/End Wizard Added Object Disposal' lines **
		// Begin Wizard Added Object Disposal
        this.inventoryAttributeSearchAdapter.Dispose();
		this.epiButtonAddMaterialToJob.Click -= new System.EventHandler(this.epiButtonAddMaterialToJob_Click);
		this.epiButtonAddMtlJobOprt.Click -= new System.EventHandler(this.epiButtonAddMtlJobOprt_Click);
		this.epiButtonAddMtlJobAsmbly.Click -= new System.EventHandler(this.epiButtonAddMtlJobAsmbly_Click);
        this.epiTextBoxAddMtlToJob.AfterExitEditMode -= new System.EventHandler(this.epiTextBoxAddMtlToJob_AfterExitEditMode);
		this.IssueMaterialForm.BeforeToolClick -= new Ice.Lib.Framework.BeforeToolClickEventHandler(this.IssueMaterialForm_BeforeToolClick);
		this.AddMtlJobAssmCombo.BeforeDropDown -= new System.ComponentModel.CancelEventHandler(this.AddMtlJobAssmCombo_BeforeDropDown);
        this.partRevisionCombo.BeforeDropDown -= new System.ComponentModel.CancelEventHandler(this.partRevisionCombo_BeforeDropDown);
		this.epiButtonNewMtl.Click -= new System.EventHandler(this.epiButtonNewMtl_Click);
		this.epiButtonMtlPartRev.Click -= new System.EventHandler(this.epiButtonMtlPartRev_Click);
		this.epiButtonNewMtlPartAttri.Click -= new System.EventHandler(this.epiButtonNewMtlPartAttri_Click);
		this.epiButtonNewMtlAdd.Click -= new System.EventHandler(this.epiButtonNewMtlAdd_Click);
		this.epiButtonNewMtlCancel.Click -= new System.EventHandler(this.epiButtonNewMtlCancel_Click);
        this.AddMtlJobAssmCombo.ValueChanged -= new System.EventHandler(this.AddMtlJobAssmCombo_ValueChanged);
        this.epiComboAddMtlOperation.ValueChanged -= new System.EventHandler(this.epiComboAddMtlOperation_ValueChanged);
        this.epiTextBoxNewMtlPart.AfterExitEditMode -= new System.EventHandler(epiTextBoxNewMtlPart_AfterExitEditMode);
        this.epiComboPrtUOM.ValueChanged -= new System.EventHandler(this.epiComboPrtUOM_ValueChanged);
		// End Wizard Added Object Disposal

		// Begin Custom Code Disposal
           this.edvAddMtlToJob = null;
           this.edvNewMaterial=null;
           this.inventoryAttributeSearchAdapter.Dispose();
           this.jobEntryAdapter.Dispose();
           this.session = null;
           this.scheduleEngineAdapter.Dispose();
		// End Custom Code Disposal
	}

#region Creating Add material View
    
    private void InitEpiDataViewDic()
	{ 
		edvDic = new Dictionary<string, List<Tuple<string, string, Type>>>()
		{
			{
				"AddMtlToJob", new List<Tuple<string, string, Type>>(){
                                                                         //- JobHead.JobNum (input)
																		new Tuple<string, string, Type>("JobNum", "JobNum", System.Type.GetType("System.String")),
                                                                         //JobHead.PartNum (display) from Asm 0
																		new Tuple<string, string, Type>("JobHeadPartNum", "PartNum", System.Type.GetType("System.String")), 
                                                                          //JobAsmbl.Description (display) from Asm 0
																		new Tuple<string, string, Type>("JobAsmblPartDescription", "Description", System.Type.GetType("System.String")),
                                                                          //- JobAsmbl.AssemblySeq (input)
                                                                        new Tuple<string, string, Type>("AddMtlJobAssemSeq", "AddMtlJobAssemSeq", System.Type.GetType("System.Int32")), 
                                                                          // JobHead.PartNum (display) from Asm referenced by JobAsmbl.AssemblySeq value     
																		new Tuple<string, string, Type>("AssemblyPartNum", "PartNum", System.Type.GetType("System.String")),  
                                                                          //JobAsmbl.Description (display) from Asm referenced by JobAsmbl.AssemblySeq value      
                                                                        new Tuple<string, string, Type>("AssemblyPartDesciption", "PartNum", System.Type.GetType("System.String")), 
                                                                          // JobMtl.RelatedOperation (input)
                                                                        new Tuple<string, string, Type>("OpCode", "Operation Code", System.Type.GetType("System.Int32")), 
                                                                          //JobOper.OpCode (display) from JobMtl.RelatedOperation value
                                                                        new Tuple<string, string, Type>("OpDesc", "Operation Description", System.Type.GetType("System.String")),
                                                                        new Tuple<string, string, Type>("btnJob", "Job Search", System.Type.GetType("System.String")),
                                                                        new Tuple<string, string, Type>("btnAsm", "Assembly Search", System.Type.GetType("System.String")),
                                                                        new Tuple<string, string, Type>("btnOpr", "Operation Search", System.Type.GetType("System.String"))
																	}
			},

			{
				"Material" , new List<Tuple<string, string, Type>>(){

																		new Tuple<string, string, Type>("MtlSeq", "MtlSeq", System.Type.GetType("System.Int32")),
																		new Tuple<string, string, Type>("PartNum", "PartNum", System.Type.GetType("System.String")),
                                                                        new Tuple<string, string, Type>("PartIUM", "PartIUM", System.Type.GetType("System.String")),
																		new Tuple<string, string, Type>("PartRev", "Part Revision", System.Type.GetType("System.String")),
                                                                        new Tuple<string, string, Type>("PartDesc", "Description", System.Type.GetType("System.String")),
                                                                        new Tuple<string, string, Type>("AttrClassID", "AttrClassID", System.Type.GetType("System.String")),
                                                                        new Tuple<string, string, Type>("TrackInventoryAttributes", "TrackInventoryAttributes", System.Type.GetType("System.Boolean")),
                                                                        new Tuple<string, string, Type>("AttributeSetID", "AttributeSetID", System.Type.GetType("System.String")),
																		new Tuple<string, string, Type>("Attribute", "Attribute", System.Type.GetType("System.String")),
																		new Tuple<string, string, Type>("AttrDesc", "Attribute Description", System.Type.GetType("System.String")),
                                                                        new Tuple<string, string, Type>("QtyParent", "Qty Parent", System.Type.GetType("System.Decimal")),
																		new Tuple<string, string, Type>("QtyParentUOM", "QtyParentUOM", System.Type.GetType("System.String")),
                                                                        new Tuple<string, string, Type>("FixedQty", "FixedQty", System.Type.GetType("System.Boolean")),
																		new Tuple<string, string, Type>("ReqQty", "Required Qty", System.Type.GetType("System.Decimal")),
                                                                        new Tuple<string, string, Type>("ReqQtyUOM", "Required Qty UOM", System.Type.GetType("System.String")),
																		new Tuple<string, string, Type>("UnitCost", "UnitCost", System.Type.GetType("System.Decimal")),
																		new Tuple<string, string, Type>("UnitQtyUOM", "Unit Qty UOM", System.Type.GetType("System.String")),
                                                                        new Tuple<string, string, Type>("btnAdd", "Add", System.Type.GetType("System.String")),
                                                                        new Tuple<string, string, Type>("btnCancel", "Cancel", System.Type.GetType("System.String")),
                                                                        new Tuple<string, string, Type>("btnPartRev", "PartRevButton", System.Type.GetType("System.String")),
                                                                        new Tuple<string, string, Type>("btnAttribute", "AttributeButton", System.Type.GetType("System.String"))
																	}
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
    
    private void AddJobAsmSearchCombo()
    {
           this.JobAsmSearchCombo = new Erp.UI.Controls.Combos.JobAsmSearchCombo();
           epiGroupBoxAddMaterialToJob.Controls.Add(this.JobAsmSearchCombo);
           //this.JobAsmSearchCombo.EpiBinding="AddMtlToJob.AssemblySeq";
           this.JobAsmSearchCombo.Top = 76;
           this.JobAsmSearchCombo.Left = 144;
           this.JobAsmSearchCombo.Width = 61;
           this.JobAsmSearchCombo.Height = 20;
          // this.JobAsmSearchCombo.EpiFilters= new string[]{"JobNum = \'?{AddMtlJobNumColumn,\'\'}\'"};
          // this.JobAsmSearchCombo.EpiFiltersParams=new string[]{"AddMtlJobNumColumn=?[JobNum]"};
    }
    
     private void InitAddMaterialView()
    {
      DataRow newRow = this.edvAddMtlToJob.dataView.Table.NewRow();
      this.edvAddMtlToJob.dataView.Table.Rows.Add(newRow);
      newRow.BeginEdit();
      newRow["AddMtlJobAssemSeq"]=0;
      newRow["OpCode"]=0;
      newRow.EndEdit();
      this.edvAddMtlToJob.Notify(new EpiNotifyArgs(this.oTrans,this.edvAddMtlToJob.dataView.Count-1,EpiTransaction.NotifyType.AddRow));
    }
    
     private void InitNewMaterialView()
    {
      DataRow newRow = this.edvNewMaterial .dataView.Table.NewRow();
      newRow.BeginEdit();
      newRow["MtlSeq"]=0;
      newRow["PartNum"]=string.Empty;
      newRow["TrackInventoryAttributes"]=false;
      newRow["QtyParent"]=0m;
      newRow["ReqQty"]=0m;
      newRow["UnitCost"]=0m;
      newRow["QtyParentUOM"]="EA";
      newRow["ReqQtyUOM"]="EA";
      newRow["UnitQtyUOM"]="EA";
      newRow["AttributeSetID"]=0;
      newRow["Attribute"]=string.Empty;
      newRow["AttrDesc"]=string.Empty;
      newRow.EndEdit();
      this.edvNewMaterial .dataView.Table.Rows.Add(newRow);
      this.edvNewMaterial .Notify(new EpiNotifyArgs(this.oTrans,this.edvNewMaterial.dataView.Count-1,EpiTransaction.NotifyType.AddRow));
    }
    
     private void ClearEpiDataView(EpiDataView edv)
    {
      edv.dataView.Table.Rows.Clear();
      edv.Notify(new EpiNotifyArgs(this.oTrans,edv.dataView.Count-1, EpiTransaction.NotifyType.DeleteRow));
    }

    private void SetExtendedProperties()
	{
        if (edvAddMtlToJob.dataView.Table.Columns.Contains("JobHeadPartNum"))
			edvAddMtlToJob.dataView.Table.Columns["JobHeadPartNum"].ExtendedProperties["ReadOnly"] = true;
        
        if (edvAddMtlToJob.dataView.Table.Columns.Contains("JobAsmblPartDescription"))
			edvAddMtlToJob.dataView.Table.Columns["JobAsmblPartDescription"].ExtendedProperties["ReadOnly"] = true;
        
         if (edvAddMtlToJob.dataView.Table.Columns.Contains("AssemblyPartNum"))
			edvAddMtlToJob.dataView.Table.Columns["AssemblyPartNum"].ExtendedProperties["ReadOnly"] = true;
        
        if (edvAddMtlToJob.dataView.Table.Columns.Contains("AssemblyPartDesciption"))
			edvAddMtlToJob.dataView.Table.Columns["AssemblyPartDesciption"].ExtendedProperties["ReadOnly"] = true;
        
        if (edvAddMtlToJob.dataView.Table.Columns.Contains("OpDesc"))
			edvAddMtlToJob.dataView.Table.Columns["OpDesc"].ExtendedProperties["ReadOnly"] = true;
        
        if (edvNewMaterial.dataView.Table.Columns.Contains("MtlSeq"))
			edvNewMaterial.dataView.Table.Columns["MtlSeq"].ExtendedProperties["ReadOnly"] = true;
        
        if (edvNewMaterial.dataView.Table.Columns.Contains("PartDesc"))
			edvNewMaterial.dataView.Table.Columns["PartDesc"].ExtendedProperties["ReadOnly"] = true;
    
        if (edvNewMaterial.dataView.Table.Columns.Contains("AttrDesc"))
			edvNewMaterial.dataView.Table.Columns["AttrDesc"].ExtendedProperties["ReadOnly"] = true;

       // if (edvNewMaterial.dataView.Table.Columns.Contains("FixedQty"))
			//edvNewMaterial.dataView.Table.Columns["FixedQty"].ExtendedProperties["ReadOnly"] = true;
        
        if (edvNewMaterial.dataView.Table.Columns.Contains("ReqQty"))
			edvNewMaterial.dataView.Table.Columns["ReqQty"].ExtendedProperties["ReadOnly"] = true;
        
        if (edvNewMaterial.dataView.Table.Columns.Contains("ReqQtyUOM"))
			edvNewMaterial.dataView.Table.Columns["ReqQtyUOM"].ExtendedProperties["ReadOnly"] = true;
    
        //if (edvNewMaterial.dataView.Table.Columns.Contains("UnitCost"))
			//edvNewMaterial.dataView.Table.Columns["UnitCost"].ExtendedProperties["ReadOnly"] = true;

        if (edvNewMaterial.dataView.Table.Columns.Contains("UnitQtyUOM"))
			edvNewMaterial.dataView.Table.Columns["UnitQtyUOM"].ExtendedProperties["ReadOnly"] = true;
    }
#endregion

    private void epiTextBoxAddMtlToJob_AfterExitEditMode(object sender, System.EventArgs args)
	{
      if(!string.IsNullOrEmpty(epiTextBoxAddMtlToJob.Text.ToString()))
         SearchOnJobEntryAdapterShowDialog(string.Format("JobNum='{0}'",epiTextBoxAddMtlToJob.Text),false);
    }
    
     private void epiTextBoxNewMtlPart_AfterExitEditMode(object sender, System.EventArgs args)
    {
      if(!string.IsNullOrEmpty(epiTextBoxNewMtlPart.Text.ToString()))
      {
         SearchOnPartRevSearchAdapterShowDialog(false,string.Format("PartNum='{0}'",epiTextBoxNewMtlPart.Text));
         SearchOnPartCostSearchAdapterShowDialog(string.Format("PartNum='{0}'",epiTextBoxNewMtlPart.Text));
      } 
    }

    private void IssueMaterialForm_BeforeToolClick(object sender, Ice.Lib.Framework.BeforeToolClickEventArgs args)
	{
        switch(args.Tool.Key.ToString())
        {
          case "ClearTool":
            ClearEpiDataView(this.edvAddMtlToJob);
          break;
        }
	}


	private void AddMtlJobAssmCombo_BeforeDropDown(object sender, System.ComponentModel.CancelEventArgs args)
	{
		// ** Place Event Handling Code Here **
        AddMtlJobAssmCombo.DisplayLayout.Bands[0].ColumnFilters["JobNum"].FilterConditions.Clear();
        if(this.edvAddMtlToJob.HasRow && !string.IsNullOrEmpty(this.edvAddMtlToJob.CurrentDataRow["JobNum"].ToString()))
           AddMtlJobAssmCombo.DisplayLayout.Bands[0].ColumnFilters["JobNum"].FilterConditions.Add(Infragistics.Win.UltraWinGrid.FilterComparisionOperator.Equals, this.edvAddMtlToJob.CurrentDataRow["JobNum"].ToString());
	}
    
    private void  AddMtlJobAssmCombo_ValueChanged(object sender, System.EventArgs args)
    {
      SearchOnJobAsmSearchAdapterShowDialog(false,string.Format("JobNum='{0}' and AssemblySeq={1}",this.edvAddMtlToJob.CurrentDataRow["JobNum"].ToString(),Convert.ToInt32(AddMtlJobAssmCombo.Value)));
    }
    
    private void epiComboAddMtlOperation_ValueChanged(object sender, System.EventArgs args)
    {
      SearchOnJobOperSearchAdapterShowDialog(false,string.Format("JobNum='{0}' and AssemblySeq={1} and OprSeq={2}",this.edvAddMtlToJob.CurrentDataRow["JobNum"].ToString(),Convert.ToInt32(this.edvAddMtlToJob.CurrentDataRow["AddMtlJobAssemSeq"]),Convert.ToInt32(epiComboAddMtlOperation.Value)));
    }
    
    private void epiComboPrtUOM_ValueChanged(object sender, System.EventArgs args)
    {
      var row  = this.edvNewMaterial.CurrentDataRow;
      row.BeginEdit();
      row["ReqQtyUOM"]=epiComboPrtUOM.Text;
      row["UnitQtyUOM"]=epiComboPrtUOM.Text;
      row.EndEdit();
    }
    
    private void partRevisionCombo_BeforeDropDown(object sender , System.ComponentModel.CancelEventArgs args)
    {
        partRevisionCombo.DisplayLayout.Bands[0].ColumnFilters["PartNum"].FilterConditions.Clear();
        if(this.edvNewMaterial.HasRow && !string.IsNullOrEmpty(this.edvNewMaterial.CurrentDataRow["PartNum"].ToString()))
           partRevisionCombo.DisplayLayout.Bands[0].ColumnFilters["PartNum"].FilterConditions.Add(Infragistics.Win.UltraWinGrid.FilterComparisionOperator.Equals, this.edvNewMaterial.CurrentDataRow["PartNum"].ToString());
        
    }
    
    private bool validateAddToJobReq()
    {
       bool valid =false;
       if(this.edvAddMtlToJob.HasRow)
       {
         var row  = this.edvAddMtlToJob.CurrentDataRow;
         if(!string.IsNullOrEmpty(row["JobNum"].ToString()))
         {
             if(Convert.ToInt32(row["AddMtlJobAssemSeq"])>-1)
             {
                if(Convert.ToInt32(row["OpCode"])>-1)
                {
                  valid=true;
                }
             }
         }
       }
       return valid;
    }

     private bool validateNewMaterial()
    {
       bool valid =false;
       if(this.edvNewMaterial.HasRow)
       {
         var row  = this.edvNewMaterial.CurrentDataRow;
         if(string.IsNullOrEmpty(row["PartNum"].ToString()))
         {
            valid=false;
            throw new UIException("Please Select part!!!");
         }
         valid = true;
       }
       return valid;
    }
   
   
#region Buttons Event

	private void epiButtonAddMaterialToJob_Click(object sender, System.EventArgs args)
	{
		// ** Place Event Handling Code Here **
       SearchOnJobEntryAdapterShowDialog(string.Empty,true);
	}

	private void epiButtonAddMtlJobOprt_Click(object sender, System.EventArgs args)
	{
		// ** Place Event Handling Code Here **
        SearchOnJobOperSearchAdapterShowDialog(true);
	}

	private void epiButtonAddMtlJobAsmbly_Click(object sender, System.EventArgs args)
	{
		// ** Place Event Handling Code Here **
          SearchOnJobAsmSearchAdapterShowDialog(true);
	}
    
    private void epiButtonNewMtl_Click(object sender, System.EventArgs args)
	{
		// ** Place Event Handling Code Here **
        if(validateAddToJobReq())
           InitNewMaterialView();
        else
          MessageBox.Show("Please make sure Add To Job,Assembly and operation has selected");
	}

	private void epiButtonMtlPartRev_Click(object sender, System.EventArgs args)
	{
		// ** Place Event Handling Code Here **
         SearchOnPartRevSearchAdapterShowDialog(true);
	}

	private void epiButtonNewMtlPartAttri_Click(object sender, System.EventArgs args)
	{
		// ** Place Event Handling Code Here **
            attributeSetSearch();
	}

	private void epiButtonNewMtlAdd_Click(object sender, System.EventArgs args)
	{
		// ** Place Event Handling Code Here **
           if(validateAddToJobReq() && validateNewMaterial())
           { 
	         var row  = this.edvAddMtlToJob.CurrentDataRow;
             var mtlRow = this.edvNewMaterial.CurrentDataRow;
             
              decimal qtyParent = Convert.ToDecimal(mtlRow["QtyParent"]),unitCost=Convert.ToDecimal(mtlRow["UnitCost"]);
              string jobNum=row["JobNum"].ToString()
              ,mtlpartNum = mtlRow["PartNum"].ToString()
              ,partNum = mtlRow["PartNum"].ToString()
              ,qtyParentUOM=mtlRow["QtyParentUOM"].ToString(),description=mtlRow["PartDesc"].ToString()
              ,partdesc = string.Format("{0} {1} - {2}",qtyParent.ToString().TrimEnd('0').TrimEnd('.'),qtyParentUOM.ToString(),description)
              ,prtRev=mtlRow["PartRev"].ToString();
             
             bool createPartOnFlyJob=false,invtryTrack=Convert.ToBoolean(mtlRow["TrackInventoryAttributes"]);
             int asmbSeq = Convert.ToInt32(row["AddMtlJobAssemSeq"]),oprSeq=Convert.ToInt32(row["OpCode"])
             ,attrSetID=Convert.ToInt32(mtlRow["AttributeSetID"]);
             
         	if(jobNum.StartsWith("SRV"))
	         {
	          createPartOnFlyJob=true;
	          partNum="SRV-"+partNum;
	         } 
	         else if(jobNum.StartsWith("MNT"))
	         {
	           createPartOnFlyJob=true;
	           partNum="MNT-"+partNum;
	         }

	         if(InsertNewJobMtl(jobNum,asmbSeq, oprSeq,partNum,qtyParent,qtyParentUOM,attrSetID,prtRev,unitCost,createPartOnFlyJob,partdesc,invtryTrack))
             {
               if(createPartOnFlyJob && invtryTrack)
               {
                  if(CreateNewPartOntheFlyJob(jobNum,asmbSeq, oprSeq,partNum,mtlpartNum,partdesc,qtyParent,attrSetID,prtRev,unitCost,invtryTrack))
                  {
                   ClearEpiDataView(this.edvNewMaterial);
                   ClearEpiDataView(this.edvAddMtlToJob);
                  }
               }
               else
               {
	            MessageBox.Show("The material has succefully added to the Job");
                ClearEpiDataView(this.edvNewMaterial);
                ClearEpiDataView(this.edvAddMtlToJob);
               } 
              }
	          else
	            MessageBox.Show("Failed to add new material to the job!!!");
           }
           
          
	}

	private void epiButtonNewMtlCancel_Click(object sender, System.EventArgs args)
	{
		// ** Place Event Handling Code Here **
           if(DialogResult.Yes==MessageBox.Show("Are you Sure you want to cancel?","Confirmation",MessageBoxButtons.YesNo))
           {
               ClearEpiDataView(this.edvNewMaterial);
               ClearEpiDataView(this.edvAddMtlToJob);
           }
	}
    
    	private void SearchOnJobEntryAdapterShowDialog(string whereclause,bool showdialog)
	{
		// Wizard Generated Search Method
		// You will need to call this method from another method in custom code
		// For example, [Form]_Load or [Button]_Click

		bool recSelected;
		string whereClause = whereclause;
		System.Data.DataSet dsJobEntryAdapter = Ice.UI.FormFunctions.SearchFunctions.listLookup(this.oTrans, "JobEntryAdapter", out recSelected, showdialog, whereClause);
		if (recSelected)
		{
            InitAddMaterialView();
			System.Data.DataRow adapterRow = dsJobEntryAdapter.Tables[0].Rows[0];
			// Map Search Fields to Application Fields
			EpiDataView edvAddMtlToJob = ((EpiDataView)(this.oTrans.EpiDataViews["AddMtlToJob"]));
			System.Data.DataRow edvAddMtlToJobRow = edvAddMtlToJob.CurrentDataRow;
			if ((edvAddMtlToJobRow != null))
			{
				edvAddMtlToJobRow.BeginEdit();
				edvAddMtlToJobRow["JobNum"] = adapterRow["JobNum"];
                edvAddMtlToJobRow["JobHeadPartNum"] = adapterRow["PartNum"];
                edvAddMtlToJobRow["JobAsmblPartDescription"] = adapterRow["PartDescription"];
				edvAddMtlToJobRow.EndEdit();
			}
		}
	}

	private void SearchOnJobAsmSearchAdapterShowDialog(bool showdialog,string _whereClause="")
	{
		// Wizard Generated Search Method
		// You will need to call this method from another method in custom code
		// For example, [Form]_Load or [Button]_Click

		bool recSelected;
        string whereClause = string.Empty;
        if(string.IsNullOrEmpty(_whereClause))
		   whereClause = string.Format("JobNum='{0}'",this.edvAddMtlToJob.CurrentDataRow["JobNum"].ToString());
        else
          whereClause = _whereClause;
		System.Data.DataSet dsJobAsmSearchAdapter = Ice.UI.FormFunctions.SearchFunctions.listLookup(this.oTrans, "JobAsmSearchAdapter", out recSelected, showdialog , whereClause);
		if (recSelected)
		{
			System.Data.DataRow adapterRow = dsJobAsmSearchAdapter.Tables[0].Rows[0];

			// Map Search Fields to Application Fields
			EpiDataView edvAddMtlToJob = ((EpiDataView)(this.oTrans.EpiDataViews["AddMtlToJob"]));
			System.Data.DataRow edvAddMtlToJobRow = edvAddMtlToJob.CurrentDataRow;
			if ((edvAddMtlToJobRow != null))
			{
				edvAddMtlToJobRow.BeginEdit();
                edvAddMtlToJobRow["AddMtlJobAssemSeq"] = adapterRow["AssemblySeq"];;
                edvAddMtlToJobRow["AssemblyPartNum"] = adapterRow["PartNum"];
				edvAddMtlToJobRow["AssemblyPartDesciption"] = adapterRow["Description"];
				edvAddMtlToJobRow.EndEdit();
			}
		}
	}


 
	private void SearchOnJobOperSearchAdapterShowDialog(bool showdialog,string _whereClause="")
	{
		// Wizard Generated Search Method
		// You will need to call this method from another method in custom code
		// For example, [Form]_Load or [Button]_Click

		bool recSelected;
        string whereClause = string.Empty;
        if(string.IsNullOrEmpty(_whereClause))
		  whereClause = string.Format("JobNum='{0}' and AssemblySeq={1}",this.edvAddMtlToJob.CurrentDataRow["JobNum"].ToString(),Convert.ToInt32(this.edvAddMtlToJob.CurrentDataRow["AddMtlJobAssemSeq"]));
        else
          whereClause=_whereClause;
		System.Data.DataSet dsJobOperSearchAdapter = Ice.UI.FormFunctions.SearchFunctions.listLookup(this.oTrans, "JobOperSearchAdapter", out recSelected, showdialog, whereClause);
		if (recSelected)
		{
			System.Data.DataRow adapterRow = dsJobOperSearchAdapter.Tables[0].Rows[0];

			// Map Search Fields to Application Fields
			EpiDataView edvAddMtlToJob = ((EpiDataView)(this.oTrans.EpiDataViews["AddMtlToJob"]));
			System.Data.DataRow edvAddMtlToJobRow = edvAddMtlToJob.CurrentDataRow;
			if ((edvAddMtlToJobRow != null))
			{
				edvAddMtlToJobRow.BeginEdit();
				edvAddMtlToJobRow["OpCode"] = adapterRow["OprSeq"];
				edvAddMtlToJobRow["OpDesc"] = adapterRow["OpCodeOpDesc"];
				edvAddMtlToJobRow.EndEdit();
			}
		}
	}
    
	private void SearchOnPartRevSearchAdapterShowDialog(bool showdialog,string _whereClause="")
	{
		// Wizard Generated Search Method
		// You will need to call this method from another method in custom code
		// For example, [Form]_Load or [Button]_Click

		bool recSelected;
		string whereClause = string.Empty;
        if(!string.IsNullOrEmpty(_whereClause))
            whereClause=_whereClause;
		System.Data.DataSet dsPartRevSearchAdapter = Ice.UI.FormFunctions.SearchFunctions.listLookup(this.oTrans, "PartAdapter", out recSelected, showdialog, whereClause);
		if (recSelected)
		{
			System.Data.DataRow adapterRow = dsPartRevSearchAdapter.Tables[0].Rows[0];

			// Map Search Fields to Application Fields
			EpiDataView edvMaterial = ((EpiDataView)(this.oTrans.EpiDataViews["Material"]));
			System.Data.DataRow edvMaterialRow = edvMaterial.CurrentDataRow;
			if ((edvMaterialRow != null))
			{
				edvMaterialRow.BeginEdit();
				edvMaterialRow["PartNum"] = adapterRow["PartNum"];
                edvMaterialRow["PartIUM"]=adapterRow["IUM"];
                edvMaterialRow["QtyParentUOM"]=adapterRow["IUM"];
                edvMaterialRow["ReqQtyUOM"]=adapterRow["IUM"];;
                edvMaterialRow["UnitQtyUOM"]=adapterRow["IUM"];;
				edvMaterialRow["PartDesc"] = adapterRow["PartDescription"];
				edvMaterialRow["AttrClassID"] = adapterRow["AttrClassID"];
                edvMaterialRow["TrackInventoryAttributes"] = adapterRow["TrackInventoryAttributes"];
				edvMaterialRow.EndEdit();
                edvNewMaterial.Notify(new EpiNotifyArgs(this.oTrans, edvNewMaterial.Row, EpiTransaction.NotifyType.Initialize));
			}
		}
	}

#endregion


	private void attributeSetSearch()
    {
     
      string str = edvNewMaterial.dataView[edvNewMaterial.Row]["AttrClassID"].ToString();
      bool boolean = Convert.ToBoolean(edvNewMaterial.dataView[edvNewMaterial.Row]["TrackInventoryAttributes"]);
      if (string.IsNullOrEmpty(str))
        return;
      using (this.oTrans.PushDisposableStatusText(EpiString.GetString("retrievingData"), true))
      {
        this.inventoryAttributeSearchAdapter.ClearList();
        this.inventoryAttributeSearchAdapter.SearchAttrClassID = str;
        if (this.inventoryAttributeSearchAdapter.InvokeSearch(new SearchOptions(SearchMode.ShowDialog)
        {
          DataSetMode = DataSetMode.ListDataSet,
          SelectMode = SelectMode.SingleSelect,
          Like = "InventoryAttributeSearch.ShortDescription",
          SortByColumn = "ShortDescription",
          StartsWithColumn = "ShortDescription",
          PreLoadSearchFilter = "Active = 1"
        }) != DialogResult.OK)
          return;
        if (this.inventoryAttributeSearchAdapter.InventoryAttributeSearchList.InventoryAttributeSearchList.Rows.Count <= 0)
          return;
        try
        {
          
          DataRow inventoryAttributeSearch = (DataRow) this.inventoryAttributeSearchAdapter.InventoryAttributeSearchList.InventoryAttributeSearchList[0];
          DataRowView dataRowView = edvNewMaterial.dataView[edvNewMaterial.Row];
          dataRowView["Attribute"] = inventoryAttributeSearch["ShortDescription"];
          dataRowView["AttrDesc"] = inventoryAttributeSearch["Description"];
          dataRowView["AttributeSetID"] = inventoryAttributeSearch["AttributeSetID"];
          edvNewMaterial.Notify(new EpiNotifyArgs(this.oTrans, edvNewMaterial.Row, EpiTransaction.NotifyType.Initialize));
        }
        catch (Exception ex)
        {
          if (ex.GetBaseException() is UIException)
              return;
          ExceptionBox.Show(ex);
        }
      }
    }

	private void CreateRowRuleMaterialTrackInventoryAttributesEquals_false()
	{
		// Description: Attr
		// **** begin autogenerated code ****
		RuleAction disabledMaterial_btnAttribute = RuleAction.AddControlSettings(this.oTrans, "Material.btnAttribute", SettingStyle.Disabled);
		RuleAction[] ruleActions = new RuleAction[] {
				                                      disabledMaterial_btnAttribute};
		// Create RowRule and add to the EpiDataView.
		RowRule rrCreateRowRuleMaterialTrackInventoryAttributesEquals_false = new RowRule("Material.TrackInventoryAttributes", RuleCondition.Equals, false, ruleActions);
		((EpiDataView)(this.oTrans.EpiDataViews["Material"])).AddRowRule(rrCreateRowRuleMaterialTrackInventoryAttributesEquals_false);
		// **** end autogenerated code ****
	}


	private void CreateRowRuleMaterialAttributeEquals_false()
	{
		// Description: Attrdesc
		// **** begin autogenerated code ****
		//ControlSettings controlSettings1EpiReadOnly = new ControlSettings();
		//controlSettings1EpiReadOnly.SetStyleSetName("EpiReadOnly");
		RuleAction epireadonlyMaterial_Attribute = RuleAction.AddControlSettings(this.oTrans, "Material.Attribute", SettingStyle.ReadOnly);
		RuleAction[] ruleActions = new RuleAction[] {
				epireadonlyMaterial_Attribute};
		// Create RowRule and add to the EpiDataView.
		RowRule rrCreateRowRuleMaterialAttributeEquals_false = new RowRule("Material.TrackInventoryAttributes", RuleCondition.Equals, false, ruleActions);
		((EpiDataView)(this.oTrans.EpiDataViews["Material"])).AddRowRule(rrCreateRowRuleMaterialAttributeEquals_false);
		// **** end autogenerated code ****
	}

    private bool InsertNewJobMtl(string jobNum,int asmSeq, int oprSeq,string partNum,decimal qty,string uom,int attrSetID, string rev,decimal unitcost,bool createPOTFJob,string partdesc,bool inventryTrack)
    {
      
      try
        {
          this.jobEntryAdapter.JobEntryData.Clear();
          this.jobEntryAdapter.GetByID(jobNum);
          if(this.jobEntryAdapter.JobEntryData.JobHead.Count==0)
             return false;
          System.Guid ipJobAsmblRowID  = this.jobEntryAdapter.JobEntryData.JobAsmbl.AsEnumerable().Where(w=>w.Field<System.Int32>("AssemblySeq")==asmSeq).Select(s=>s.Field<System.Guid>("SysRowID")).FirstOrDefault();
          System.Guid ipBeforeMtlRowid = this.jobEntryAdapter.JobEntryData.JobMtl.AsEnumerable().Where(w=>w.Field<System.Int32>("AssemblySeq")==asmSeq && w.Field<System.Int32>("RelatedOperation")==oprSeq).OrderByDescending(o=>o.Field<System.Int32>("MtlSeq")).Select(s=>s.Field<System.Guid>("SysRowID")).FirstOrDefault();
          if(ipJobAsmblRowID == null && ipBeforeMtlRowid==null)
             return false;
          this.jobEntryAdapter.InsertNewJobMtl(ipJobAsmblRowID,"",oprSeq,0,ipBeforeMtlRowid);
          System.Guid SysRowID =  new System.Guid("00000000-0000-0000-0000-000000000000");
          string vMsgText = string.Empty, vMsgType=string.Empty,opMtlIssuedAction=string.Empty;
          bool vSubAvail = false, multipleMatch=false,opPartChgCompleted=false;
          this.jobEntryAdapter.ChangeJobMtlPartNum(true,ref partNum,SysRowID,"","",out vMsgText,out vSubAvail,out vMsgType,out multipleMatch,out opPartChgCompleted,out opMtlIssuedAction);
          this.jobEntryAdapter.JobEntryData.JobMtl[this.jobEntryAdapter.JobEntryData.JobMtl.Count-1].RevisionNum=rev;
          this.jobEntryAdapter.JobEntryData.JobMtl[this.jobEntryAdapter.JobEntryData.JobMtl.Count-1].QtyPer=qty;
          this.jobEntryAdapter.JobEntryData.JobMtl[this.jobEntryAdapter.JobEntryData.JobMtl.Count-1].IUM=uom;
          if(createPOTFJob)
          {
             this.jobEntryAdapter.JobEntryData.JobMtl[this.jobEntryAdapter.JobEntryData.JobMtl.Count-1].Description=partdesc;
             this.jobEntryAdapter.JobEntryData.JobMtl[this.jobEntryAdapter.JobEntryData.JobMtl.Count-1].dspBuyIt=false;
             this.jobEntryAdapter.JobEntryData.JobMtl[this.jobEntryAdapter.JobEntryData.JobMtl.Count-1].Direct=true;
          }
          else if(inventryTrack)
          {
             this.jobEntryAdapter.JobEntryData.JobMtl[this.jobEntryAdapter.JobEntryData.JobMtl.Count-1].AttributeSetID=attrSetID;
          }
          this.jobEntryAdapter.ChangeJobMtlQtyPer();
          this.jobEntryAdapter.ChangeJobMtlEstSplitCosts();
          if(unitcost>0m)
          {
           this.jobEntryAdapter.JobEntryData.JobMtl[this.jobEntryAdapter.JobEntryData.JobMtl.Count-1].EstUnitCost=unitcost;
           this.jobEntryAdapter.CalcJobMtlEstMtlBurUnitCost();
          } 
          this.jobEntryAdapter.Update();
        }
        catch (Exception ex)
        {
          ExceptionBox.Show(ex);
          return false;
        }
        return true;
      
    }

    private bool CreateNewPartOntheFlyJob(string jobNum,int asmSeq, int oprSeq,string partNum,string _mtlPartNum,string partdesc,decimal qty,int attrSetID,string rev,decimal unitcost,bool inventryTrack)
    {
       string vMessage=string.Empty,vMsgText=string.Empty,vMsgType=string.Empty,RefreshMessage=string.Empty,c_WarnLogTxt=string.Empty,MultiSchedTypeCodes=string.Empty,schedulingDirection=string.Empty;
       bool vSubAvail=false,multipleMatch=false,l_finished=false,schedulingMultiJobActive=false,minimizeWIPFlag=false,allowMoveJobsAcrossPlants=false,autoLoadParentJobs=false,autoLoadChildJobs=false,v_Engineered=true;
       int BWSchedStartTime=0;
       string ProposedOpCode = this.jobEntryAdapter.JobEntryData.JobOper.AsEnumerable().Where(w=>w.Field<System.Int32>("AssemblySeq")==asmSeq && w.Field<System.Int32>("OprSeq")==oprSeq).Select(s=>s.Field<System.String>("OpCode")).FirstOrDefault();
       int mtlSeq = this.jobEntryAdapter.JobEntryData.JobMtl.AsEnumerable().Where(w=>w.Field<System.Int32>("AssemblySeq")==asmSeq && w.Field<System.Int32>("RelatedOperation")==oprSeq && w.Field<System.String>("PartNum")==partNum).OrderByDescending(o=>o.Field<System.Int64>("SysRevID")).Select(s=>s.Field<System.Int32>("MtlSeq")).FirstOrDefault();
       string ium =  this.jobEntryAdapter.JobEntryData.JobMtl.AsEnumerable().Where(w=>w.Field<System.Int32>("AssemblySeq")==asmSeq && w.Field<System.Int32>("RelatedOperation")==oprSeq && w.Field<System.String>("PartNum")==partNum).OrderByDescending(o=>o.Field<System.Int64>("SysRevID")).Select(s=>s.Field<System.String>("IUM")).FirstOrDefault();
        try
        {
          this.jobEntryAdapter.JobEntryData.Clear();
          this.jobEntryAdapter.GetNewJobHead();
          var jobHeadRow =  this.jobEntryAdapter.JobEntryData.JobHead[this.jobEntryAdapter.JobEntryData.JobHead.Count-1];
          jobHeadRow.JobNum="SRV-"+mtlSeq.ToString();
          jobHeadRow.PartNum=partNum;
          jobHeadRow.PartDescription=partdesc;
          jobHeadRow.IUM=ium;
          jobHeadRow.ReqDueDate=DateTime.Now.Date;
          jobHeadRow.Plant = this.session.PlantID.ToString();
          this.jobEntryAdapter.Update(); 
          string currentJobNum = jobHeadRow.JobNum.ToString();
          string currentPartNum = jobHeadRow.PartNum.ToString();
          this.jobEntryAdapter.GetByID(currentJobNum);
          this.jobEntryAdapter.GetNewJobProd(currentJobNum,currentPartNum,0,0,0,string.Empty,string.Empty,0);
          var jobProdRow = this.jobEntryAdapter.JobEntryData.JobProd[this.jobEntryAdapter.JobEntryData.JobProd.Count-1];
          jobProdRow.MakeToType="JOB";
          jobProdRow.TargetJobNum=jobNum;
          jobProdRow.MtlPartNum=partNum;
          jobProdRow.WIPToMiscShipment=false; 
          jobProdRow.TargetMtlSeq=mtlSeq; 
          jobProdRow.MakeToJobQty=qty;
          this.jobEntryAdapter.Update();
          this.jobEntryAdapter.GetNewJobOper(currentJobNum,0);
          this.jobEntryAdapter.ChangeJobOperOpCode(ProposedOpCode,out RefreshMessage);
          this.jobEntryAdapter.Update();
          if(InsertNewJobMtl(currentJobNum,0,(int)this.jobEntryAdapter.JobEntryData.JobOper[this.jobEntryAdapter.JobEntryData.JobOper.Count-1].OprSeq,_mtlPartNum,1m,ium,attrSetID, rev,unitcost,false,string.Empty,inventryTrack))
          {
            // this.jobEntryAdapter.CheckEngineered(out v_Engineered);
             this.jobEntryAdapter.JobEntryData.JobHead[this.jobEntryAdapter.JobEntryData.JobHead.Count-1].JobEngineered=true;
             this.jobEntryAdapter.Update();    
             if(v_Engineered)
             {
               string sysRowID =  this.jobEntryAdapter.JobEntryData.JobHead[this.jobEntryAdapter.JobEntryData.JobHead.Count-1].SysRowID.ToString();
               if(JobScheduling(currentJobNum,DateTime.Now.Date,DateTime.Now.Date,session.CompanyID,false,true,sysRowID,v_Engineered))
               {
                 this.scheduleEngineAdapter.MoveJobItem(out l_finished, out c_WarnLogTxt);
                 if (c_WarnLogTxt.Length > 0)
                 {
                   int num = (int) EpiMessageBox.ShowLocale("ScheduleWarning", "ScheduleWarning_title", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, (object) c_WarnLogTxt);
                 }
                 this.jobEntryAdapter.JobEntryData.JobHead[this.jobEntryAdapter.JobEntryData.JobHead.Count-1].JobReleased=true;
                 this.jobEntryAdapter.JobEntryData.JobHead[this.jobEntryAdapter.JobEntryData.JobHead.Count-1].RowMod="U";
                 this.jobEntryAdapter.ChangeJobHeadJobReleased(); 
                 this.jobEntryAdapter.Update();
                 MessageBox.Show(string.Format("New Make To Job-{0} created for Job-{1} and MaterialSeq-{2}",currentJobNum,jobNum,mtlSeq.ToString()));
                 if(DialogResult.Yes==MessageBox.Show("Would you like to print JobTraveller?","Print",MessageBoxButtons.YesNo,MessageBoxIcon.Question))
                 {
                   PrintJobTrav(jobNum);
                 }
               }
             } 
             
          } 

        }
        catch (Exception ex)
        {
          ExceptionBox.Show(ex);
          return false;
        }
        return true;
    }
    
    public bool JobScheduling(
      string jobNumber,
      DateTime startDate,
      DateTime dueDate,
      string company,
      bool ignoreMtlConstraints,
      bool forward,
      string rowIdent,
      bool engineered)
    {
      bool flag = true;
      bool minimizeWIPFlag = false;
      bool schedulingMultiJobActive = false;
      bool allowMoveJobsAcrossPlants = false;
      bool autoLoadParentJobs = false;
      bool autoLoadChildJobs = false;
      int BWSchedStartTime = 0;
      string schedulingDirection = string.Empty;
      this.scheduleEngineAdapter.ScheduleEngineData.Clear();
      DataRow row = this.scheduleEngineAdapter.ScheduleEngineData.ScheduleEngine.NewRow();
      row["Company"] = (object) company;
      row["JobNum"] = (object) jobNumber;
      row["AssemblySeq"] = (object) 0;
      row["OprSeq"] = (object) 0;
      row["OpDtlSeq"] = (object) 0;
      this.scheduleEngineAdapter.GetSchedulingFlags(out schedulingMultiJobActive, out minimizeWIPFlag, out allowMoveJobsAcrossPlants, out autoLoadParentJobs, out autoLoadChildJobs, out BWSchedStartTime, out schedulingDirection);
      row["StartDate"] = (object) startDate;
      row["StartTime"] = (object) 0;
      row["EndDate"] = (object) dueDate;
      row["EndTime"] = (object) BWSchedStartTime;
      row["WhatIf"] = (object) false;
      row["Finite"] = (object) false;
      row["OverrideMtlCon"] = (object) ignoreMtlConstraints;
      row["OverRideHistDateSetting"] = (object) 2;
      row["SetupComplete"] = (object) false;
      row["ProductionComplete"] = (object) false;
      row["SchedTypeCode"] = (object) "JJ";
      row["ScheduleDirection"] = (object) schedulingDirection;
      row["RecalcExpProdYld"] = (object) false;
      row["UseSchedulingMultiJob"] = (object) schedulingMultiJobActive;
      row["SchedulingMultiJobMinimizeWIP"] = (object) minimizeWIPFlag;
      row["SchedulingMultiJobIgnoreLocks"] = (object) false;
      row["SysRowID"] = (object) rowIdent;
      row["RowMod"] = (object) "A";
      row["JobEngineered"] = (object) engineered;
      this.scheduleEngineAdapter.ScheduleEngineData.ScheduleEngine.Rows.Add(row);
      return flag;
    }
    
    private void PrintJobTrav(string jobNum)
    {
      try
        {
          if(!string.IsNullOrEmpty(jobNum))
          {
            ProcessCaller.LaunchForm((object)this.oTrans,"Erp.UIRpt.JobTrav",(object)new LaunchFormOptions()
            {
              ValueIn = jobNum
            });
          }
        }
        catch (Exception ex)
        {
         ExceptionBox.Show(ex);
        }
    }
   

	private void SearchOnPartCostSearchAdapterShowDialog(string _whereClause)
	{
		// Wizard Generated Search Method
		// You will need to call this method from another method in custom code
		// For example, [Form]_Load or [Button]_Click

		bool recSelected;
		string whereClause = _whereClause;
		System.Data.DataSet dsPartCostSearchAdapter = Ice.UI.FormFunctions.SearchFunctions.listLookup(this.oTrans, "PartCostSearchAdapter", out recSelected, false, whereClause);
		if (recSelected)
		{
			System.Data.DataRow adapterRow = dsPartCostSearchAdapter.Tables[0].Rows[0];

			// Map Search Fields to Application Fields
			EpiDataView edvMaterial = ((EpiDataView)(this.oTrans.EpiDataViews["Material"]));
			System.Data.DataRow edvMaterialRow = edvMaterial.CurrentDataRow;
			if ((edvMaterialRow != null))
			{
				edvMaterialRow.BeginEdit();
				edvMaterialRow["UnitCost"] = adapterRow["AvgMaterialCost"];
				edvMaterialRow.EndEdit();
                edvNewMaterial.Notify(new EpiNotifyArgs(this.oTrans, edvNewMaterial.Row, EpiTransaction.NotifyType.Initialize));
			}
		}
	}
}

