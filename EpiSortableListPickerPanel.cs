// **************************************************
// Custom code for CustomerEntryForm
// Created: 25/10/2016 1:59:16 PM
// **************************************************

extern alias Erp_Contracts_BO_Vendor;


extern alias Erp_Contracts_BO_Customer;
extern alias Erp_Contracts_BO_BankAcct;
extern alias Erp_Contracts_BO_ShipTo;
extern alias Ice_Adapters_UD08;
extern alias Erp_Contracts_BO_PbsBuyCodes;
extern alias Erp_Adapters_PbsBuyCodes;

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

public class Script
{
	// ** Wizard Insert Location - Do Not Remove 'Begin/End Wizard Added Module Level Variables' Comments! **
	// Begin Wizard Added Module Level Variables **
  
	private EpiDataView edvShipTo , edvlables;
    private EpiSortableListPickerPanel labelsSwitchPickerControl;
	private EpiBaseAdapter oTrans_customerAdapter;
	private EpiDataView edvCustomer;
	// End Wizard Added Module Level Variables **

	// Add Custom Module Level Variables Here **

	public void InitializeCustomCode()
	{
		// ** Wizard Insert Location - Do not delete 'Begin/End Wizard Added Variable Initialization' lines **
		// Begin Wizard Added Variable Initialization
       
		labelsSwitchPickerControl = new EpiSortableListPickerPanel();
		SetuplabelSwitchPickerControlView();
        SetuplabelSwitchPickerControl();

		//epMultClmLstbx = new EpiMultiColumnListbox();
		//DeliveryMethod.Controls.Add(epMultClmLstbx);
		
		// SetupAttributView();
		this.edvShipTo = ((EpiDataView)(this.oTrans.EpiDataViews["ShipTo"]));
        this.edvCustomer = ((EpiDataView)(this.oTrans.EpiDataViews["Customer"]));
        
		this.edvShipTo.EpiViewNotification += new EpiViewNotification(this.edvShipTo_EpiViewNotification);
        this.oTrans_customerAdapter = ((EpiBaseAdapter)(this.csm.TransAdaptersHT["oTrans_customerAdapter"]));
		this.oTrans_customerAdapter.BeforeAdapterMethod += new BeforeAdapterMethod(this.oTrans_customerAdapter_BeforeAdapterMethod);
		this.edvCustomer.EpiViewNotification += new EpiViewNotification(this.edvCustomer_EpiViewNotification);
		// End Wizard Added Variable Initialization

		// Begin Wizard Added Custom Method Calls

		
		//CreateRowRuleCustomerPbsCsvSrv_cEquals_false();
		this.chkbxBuilder.CheckedChanged += new System.EventHandler(this.chkbxBuilder_CheckedChanged);
		// End Wizard Added Custom Method Calls
          CreateRowRuleCustomerPbsInactive_cEquals_true();
		  CreateRowRuleCustomerPbsInactive_cNotEqual_true();
		  CreateRowRuleShipToPbsInactive_cEquals_true();
		  CreateRowRuleShipToPbsInactive_cNotEqual_true();
	}

	public void DestroyCustomCode()
	{
		// ** Wizard Insert Location - Do not delete 'Begin/End Wizard Added Object Disposal' lines **
		// Begin Wizard Added Object Disposal

		this.edvShipTo.EpiViewNotification -= new EpiViewNotification(this.edvShipTo_EpiViewNotification);
		this.edvShipTo = null;
        labelsSwitchPickerControl.Dispose();
		
		this.oTrans_customerAdapter.BeforeAdapterMethod -= new BeforeAdapterMethod(this.oTrans_customerAdapter_BeforeAdapterMethod);
		this.oTrans_customerAdapter = null;
		this.chkbxBuilder.CheckedChanged -= new System.EventHandler(this.chkbxBuilder_CheckedChanged);
		this.edvCustomer.EpiViewNotification -= new EpiViewNotification(this.edvCustomer_EpiViewNotification);
		this.edvCustomer = null;
		// End Wizard Added Object Disposal

		// Begin Custom Code Disposal

		// End Custom Code Disposal
	}

     private void SetuplabelSwitchPickerControl()
    {
		labelsSwitchPickerControl.Width =800;
        labelsSwitchPickerControl.Height =700;
		DeliveryMethod.Controls.Add(labelsSwitchPickerControl);
        labelsSwitchPickerControl.PreserveSourceList = false;
       labelsSwitchPickerControl.SourceString = "Available Columns";
       labelsSwitchPickerControl.TargetString = "Selected Columns";
		labelsSwitchPickerControl.HorizontalScroll.Enabled = true;

	   labelsSwitchPickerControl.InitDataBind("edvlabels", "CodeID", "LongDesc", '~', "PbsCustStmntFormat_c", "Customer", oTrans);
       
    

    }
 
    private void SetuplabelSwitchPickerControlView()
    {
      edvlables = new EpiDataView();
      edvlables.dataView = GetUserCodeData("InvSummary").DefaultView;
      oTrans.Add("edvlabels",edvlables); 

    }
    
    private DataTable GetUserCodeData(string CodeTypeID)
    {
       UserCodesAdapter adapterUserCodes = new UserCodesAdapter(this.oTrans);
	   adapterUserCodes.BOConnect();
       DataTable dt = new DataTable();
	   string whereClause = string.Format("CodeTypeID='{0}'", CodeTypeID);
		System.Collections.Hashtable myHash = new System.Collections.Hashtable();
		myHash.Add("UDCodes", whereClause);
		Ice.Lib.Searches.SearchOptions opts = Ice.Lib.Searches.SearchOptions.CreateRuntimeSearch(myHash, DataSetMode.RowsDataSet);          
		adapterUserCodes.InvokeSearch(opts);
		if (adapterUserCodes.UserCodesData.UDCodes.Rows.Count > 0)
		{
			dt =  adapterUserCodes.UserCodesData.UDCodes;
		}
        adapterUserCodes.Dispose();
		return dt;
		
		
    }

	private void edvShipTo_EpiViewNotification(EpiDataView view, EpiNotifyArgs args)
	{
		// ** Argument Properties and Uses **
		// view.dataView[args.Row]["FieldName"]
		// args.Row, args.Column, args.Sender, args.NotifyType
		// NotifyType.Initialize, NotifyType.AddRow, NotifyType.DeleteRow, NotifyType.InitLastView, NotifyType.InitAndResetTreeNodes
		if ((args.NotifyType == EpiTransaction.NotifyType.AddRow))
		{
           
            

			if ((args.Row > -1))
			{
				
         	   EpiDataView edvCust = (EpiDataView)(oTrans.EpiDataViews["Customer"]);
                view.dataView[args.Row]["pbsCustomerEarlyShipDays_c"]=(int)edvCust.dataView[edvCust.Row]["pbsCustomerEarlyShipDays_c"];
                view.dataView[args.Row]["ShortChar01"]=edvCust.dataView[edvCust.Row]["ShortChar01"];
                
               //view.dataView(args.Row)("pbsShipToEarlyText")="10";
			}
		}
	}

    private void CreateRowRuleCustomerPbsInactive_cEquals_true()
	{
		// Description: CustomerInactive
		// **** begin autogenerated code ****
		RuleAction disabledCustomer_PbsInactive_c = RuleAction.AddControlSettings(this.oTrans, "Customer.PbsInactive_cShape", SettingStyle.Disabled);
		RuleAction[] ruleActions = new RuleAction[] {
				disabledCustomer_PbsInactive_c};
		// Create RowRule and add to the EpiDataView.
		RowRule rrCreateRowRuleCustomerPbsInactive_cEquals_true = new RowRule("Customer.PbsInactive_c", RuleCondition.Equals, true, ruleActions);
		((EpiDataView)(this.oTrans.EpiDataViews["Customer"])).AddRowRule(rrCreateRowRuleCustomerPbsInactive_cEquals_true);
		// **** end autogenerated code ****
	}
	private void CreateRowRuleCustomerPbsInactive_cNotEqual_true()
	{
		// Description: CustomerPbsActive
		// **** begin autogenerated code ****
		ControlSettings controlSettings1EpiStyle_OK = new ControlSettings();
		controlSettings1EpiStyle_OK.SetStyleSetName("EpiStyle_OK");
		RuleAction epistyle_okCustomer_PbsInactive_c = RuleAction.AddControlSettings(this.oTrans, "Customer.PbsInactive_cShape", controlSettings1EpiStyle_OK);
		RuleAction[] ruleActions = new RuleAction[] {
				epistyle_okCustomer_PbsInactive_c};
		// Create RowRule and add to the EpiDataView.
		RowRule rrCreateRowRuleCustomerPbsInactive_cNotEqual_true = new RowRule("Customer.PbsInactive_c", RuleCondition.NotEqual, true, ruleActions);
		((EpiDataView)(this.oTrans.EpiDataViews["Customer"])).AddRowRule(rrCreateRowRuleCustomerPbsInactive_cNotEqual_true);
		// **** end autogenerated code ****
	}

	private void CreateRowRuleShipToPbsInactive_cEquals_true()
	{
		// Description: CustomerInactive
		// **** begin autogenerated code ****
		RuleAction disabledCustomer_PbsInactive_c = RuleAction.AddControlSettings(this.oTrans, "ShipTo.PbsInactive_cShape", SettingStyle.Disabled);
		RuleAction[] ruleActions = new RuleAction[] {
				disabledCustomer_PbsInactive_c};
		// Create RowRule and add to the EpiDataView.
		RowRule rrCreateRowRuleCustomerPbsInactive_cEquals_true = new RowRule("ShipTo.PbsInactive_c", RuleCondition.Equals, true, ruleActions);
		((EpiDataView)(this.oTrans.EpiDataViews["ShipTo"])).AddRowRule(rrCreateRowRuleCustomerPbsInactive_cEquals_true);
		// **** end autogenerated code ****
	}
	private void CreateRowRuleShipToPbsInactive_cNotEqual_true()
	{
		// Description: CustomerPbsActive
		// **** begin autogenerated code ****
		ControlSettings controlSettings1EpiStyle_OK = new ControlSettings();
		controlSettings1EpiStyle_OK.SetStyleSetName("EpiStyle_OK");
		RuleAction epistyle_okCustomer_PbsInactive_c = RuleAction.AddControlSettings(this.oTrans, "ShipTo.PbsInactive_cShape", controlSettings1EpiStyle_OK);
		RuleAction[] ruleActions = new RuleAction[] {
				epistyle_okCustomer_PbsInactive_c};
		// Create RowRule and add to the EpiDataView.
		RowRule rrCreateRowRuleCustomerPbsInactive_cNotEqual_true = new RowRule("ShipTo.PbsInactive_c", RuleCondition.NotEqual, true, ruleActions);
		((EpiDataView)(this.oTrans.EpiDataViews["ShipTo"])).AddRowRule(rrCreateRowRuleCustomerPbsInactive_cNotEqual_true);
		// **** end autogenerated code ****
	}


	


	private void CreateRowRuleCustomerPbsCsvSrv_cEquals_false()
	{
		// Description: UseInvoiceSummary
		// **** begin autogenerated code ****
		//ControlSettings controlSettings1EpiReadOnly = new ControlSettings();
		//controlSettings1EpiReadOnly.SetStyleSetName("EpiReadOnly");
		RuleAction epireadonlyCustomer_pbsStatementDeliveryMethod_c = RuleAction.AddControlSettings(this.oTrans, "Customer.pbsStatementDeliveryMethod_c", SettingStyle.ReadOnly);
        RuleAction epireadonlyCustomer_pbsInvoiceDeliveryMethod_c = RuleAction.AddControlSettings(this.oTrans, "Customer.pbsInvoiceDeliveryMethod_c", SettingStyle.ReadOnly);
        RuleAction epireadonlyCustomer_PbsInvSummPeriodicity_c = RuleAction.AddControlSettings(this.oTrans, "Customer.PbsInvSummPeriodicity_c", SettingStyle.ReadOnly);
        RuleAction epireadonlyCustomer_pbsStatementEmail_c = RuleAction.AddControlSettings(this.oTrans, "Customer.pbsStatementEmail_c", SettingStyle.ReadOnly);
        RuleAction epireadonlyCustomer_PbsInvoiceEmail_c = RuleAction.AddControlSettings(this.oTrans, "Customer.PbsInvoiceEmail_c", SettingStyle.ReadOnly);
		RuleAction[] ruleActions = new RuleAction[] {
				epireadonlyCustomer_pbsStatementDeliveryMethod_c
                ,epireadonlyCustomer_pbsInvoiceDeliveryMethod_c
                ,epireadonlyCustomer_PbsInvSummPeriodicity_c
                ,epireadonlyCustomer_pbsStatementEmail_c
                ,epireadonlyCustomer_PbsInvoiceEmail_c};
		// Create RowRule and add to the EpiDataView.
		RowRule rrCreateRowRuleCustomerPbsCsvSrv_cEquals_false = new RowRule("Customer.PbsCsvSrv_c", RuleCondition.Equals, false, ruleActions);
		((EpiDataView)(this.oTrans.EpiDataViews["Customer"])).AddRowRule(rrCreateRowRuleCustomerPbsCsvSrv_cEquals_false);
		// **** end autogenerated code ****
	}


	private void oTrans_customerAdapter_BeforeAdapterMethod(object sender, BeforeAdapterMethodArgs args)
	{
		// ** Argument Properties and Uses **
		// ** args.MethodName **
		// ** Add Event Handler Code **

		// ** Use MessageBox to find adapter method name
		// EpiMessageBox.Show(args.MethodName)
		switch (args.MethodName)
		{
			case "Update":
				// DialogResult dialogResult = EpiMessageBox.Show("Cancel Update?", "Cancel", MessageBoxButtons.YesNo);
				// if ((dialogResult == DialogResult.Yes))
				// {
				// 	args.Cancel = true;
				// } else
				// {
				// 	DoSomethingElse();
				// }
              var row  = this.edvCustomer.CurrentDataRow;
              if(((Boolean)row["PbsCsvSrv_c"]))
              {
                 if(string.IsNullOrEmpty(row["PbsInvSummPeriodicity_c"].ToString()) || string.IsNullOrEmpty(row["PbsInvoiceEmail_c"].ToString()))
                 {
                    args.Cancel=true;
                    throw new UIException("Please make sure InvoiceSummary/InvoiceEmail is not empty");
                 }
              }
				break;
		}

	}

	private void chkbxBuilder_CheckedChanged(object sender, System.EventArgs args)
	{
		// ** Place Event Handling Code Here **
         if(((EpiCheckBox)sender).Checked)
         {
           epiShapeBuilder.Status = StatusTypes.OK;
           epiShapeBuilder.EnabledCaption="Builder";
         }
         else
         {
           epiShapeBuilder.Status = StatusTypes.Warning;
            epiShapeBuilder.EnabledCaption="Not Builder";
         }
	}

    
	private void edvCustomer_EpiViewNotification(EpiDataView view, EpiNotifyArgs args)
	{
		// ** Argument Properties and Uses **
		// view.dataView[args.Row]["FieldName"]
		// args.Row, args.Column, args.Sender, args.NotifyType
		// NotifyType.Initialize, NotifyType.AddRow, NotifyType.DeleteRow, NotifyType.InitLastView, NotifyType.InitAndResetTreeNodes
		if ((args.NotifyType == EpiTransaction.NotifyType.Initialize))
		{
			if ((args.Row > -1))
			{
              
                 if(((EpiCheckBox)chkbxBuilder).Checked)
		         {
		           epiShapeBuilder.Status = StatusTypes.OK;
		           epiShapeBuilder.EnabledCaption="Builder";
		         }
		         else
		         {
		           epiShapeBuilder.Status = StatusTypes.Warning;
		            epiShapeBuilder.EnabledCaption="Not Builder";
		         }
			}
		}
	}
}





















































