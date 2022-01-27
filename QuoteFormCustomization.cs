// **************************************************
// Custom code for QuoteForm
// Created: 23/05/2018 11:25:45 AM
// **************************************************

extern alias Erp_Adapters_DemandContract;


extern alias Erp_Contracts_BO_QuoteDtlSearch;
extern alias Erp_Contracts_BO_Quote;
extern alias Erp_Contracts_BO_Customer;
extern alias Erp_Contracts_BO_AlternatePart;
extern alias Erp_Contracts_BO_Part;
extern alias Erp_Contracts_BO_Vendor;
extern alias Erp_Contracts_BO_VendorPPSearch;
extern alias Erp_Contracts_BO_ShipTo;
extern alias Erp_Adapters_Quote;
extern alias Erp_Adapters_CustPriceListSearch;

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
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinToolbars;
using System.Collections;
using Ice.BO;
using Ice.Contracts;
using System.Drawing;
using System.Collections.Generic;

public class Script
{
	// ** Wizard Insert Location - Do Not Remove 'Begin/End Wizard Added Module Level Variables' Comments! **
	// Begin Wizard Added Module Level Variables **

	private UD01Adapter _ud01Adapter;
	private EpiDataView _edvOrderHed;
	private DataTable UD01_Column;
	private EpiDataView _edvUD01;
	private string _Key1UD01;
	private string _Key2UD01;
	private string _Key3UD01;
	private string _Key4UD01;
	private string _Key5UD01;
	private DataView OrderHed_DataView;
	private UD02Adapter _ud02Adapter;
	private EpiDataView _edvQuoteHed;
	private DataTable UD02_Column;
	private EpiDataView _edvUD02;
	private string _Key1UD02;
	private string _Key2UD02;
	private string _Key3UD02;
	private string _Key4UD02;
	private string _Key5UD02;
	private DataView QuoteHed_DataView;
	private EpiBaseAdapter oTrans_adapter;
	private EpiDataView edvQuoteDtl;
	private EpiDataView edvOrderDtl;
	private string FirstPLName = "";
	private string SecondPLName = "";
    private Erp.UI.Controls.Combos.CustPriceListSearchCombo priceListCmb;
	private EpiDataView edvQuoteHed;
	private EpiBaseAdapter oTrans_taskSetAdapter;
    private EpiUltraGrid eugQuoteLines;
    private Ice.Lib.Framework.EpiCurrencyConver currUnitPrice;
	// End Wizard Added Module Level Variables **

	// Add Custom Module Level Variables Here **

	public void InitializeCustomCode()
	{
		// ** Wizard Insert Location - Do not delete 'Begin/End Wizard Added Variable Initialization' lines **
		// Begin Wizard Added Variable Initialization
        this.priceListCmb = ((Erp.UI.Controls.Combos.CustPriceListSearchCombo)csm.GetNativeControlReference("808b62b6-5472-4e8f-a7a2-f760cb218b2b"));
		this.eugQuoteLines = ((EpiUltraGrid)csm.GetNativeControlReference("73bddcdc-c164-4d92-804b-7309ce973d36"));
        this.currUnitPrice = ((Ice.Lib.Framework.EpiCurrencyConver)csm.GetNativeControlReference("da1b4ed0-c1bb-4073-b6b3-08a366c128de"));
        this.eugQuoteLines.DisplayLayout.Bands[0].Columns["DocDspExpUnitPrice"].CellActivation=Activation.NoEdit;
		InitializeUD01Adapter();
		this._Key1UD01 = string.Empty;
		this._Key2UD01 = string.Empty;
		this._Key3UD01 = string.Empty;
		this._Key4UD01 = string.Empty;
		this._Key5UD01 = string.Empty;
		this.baseToolbarsManager.ToolClick += new Infragistics.Win.UltraWinToolbars.ToolClickEventHandler(this.baseToolbarsManager_ToolClickForUD01);
		this.QuoteForm.BeforeToolClick += new Ice.Lib.Framework.BeforeToolClickEventHandler(this.QuoteForm_BeforeToolClickForUD01);
		this.QuoteForm.AfterToolClick += new Ice.Lib.Framework.AfterToolClickEventHandler(this.QuoteForm_AfterToolClickForUD01);
		this.OrderHed_Row.EpiRowChanged += new EpiRowChanged(this.OrderHed_AfterRowChangeForUD01);
		this.OrderHed_DataView = this.OrderHed_Row.dataView;
		this.OrderHed_DataView.ListChanged += new ListChangedEventHandler(this.OrderHed_DataView_ListChangedForUD01);
		this.OrderHed_Row.BeforeResetDataView += new Ice.Lib.Framework.EpiDataView.BeforeResetDataViewDelegate(this.OrderHed_BeforeResetDataViewForUD01);
		this.OrderHed_Row.AfterResetDataView += new Ice.Lib.Framework.EpiDataView.AfterResetDataViewDelegate(this.OrderHed_AfterResetDataViewForUD01);
		InitializeUD02Adapter();
		this._Key1UD02 = string.Empty;
		this._Key2UD02 = string.Empty;
		this._Key3UD02 = string.Empty;
		this._Key4UD02 = string.Empty;
		this._Key5UD02 = string.Empty;
		this.baseToolbarsManager.ToolClick += new Infragistics.Win.UltraWinToolbars.ToolClickEventHandler(this.baseToolbarsManager_ToolClickForUD02);
		this.QuoteForm.BeforeToolClick += new Ice.Lib.Framework.BeforeToolClickEventHandler(this.QuoteForm_BeforeToolClickForUD02);
		this.QuoteForm.AfterToolClick += new Ice.Lib.Framework.AfterToolClickEventHandler(this.QuoteForm_AfterToolClickForUD02);
		this.QuoteHed_Row.EpiRowChanged += new EpiRowChanged(this.QuoteHed_AfterRowChangeForUD02);
		this.QuoteHed_DataView = this.QuoteHed_Row.dataView;
		this.QuoteHed_DataView.ListChanged += new ListChangedEventHandler(this.QuoteHed_DataView_ListChangedForUD02);
		this.QuoteHed_Row.BeforeResetDataView += new Ice.Lib.Framework.EpiDataView.BeforeResetDataViewDelegate(this.QuoteHed_BeforeResetDataViewForUD02);
		this.QuoteHed_Row.AfterResetDataView += new Ice.Lib.Framework.EpiDataView.AfterResetDataViewDelegate(this.QuoteHed_AfterResetDataViewForUD02);
		this.oTrans_adapter = ((EpiBaseAdapter)(this.csm.TransAdaptersHT["oTrans_adapter"]));
		this.oTrans_adapter.AfterAdapterMethod += new AfterAdapterMethod(this.oTrans_adapter_AfterAdapterMethod);
		this.UD01_Column.ColumnChanged += new DataColumnChangeEventHandler(this.UD01_AfterFieldChange);
		this.QuoteDtl_Column.ColumnChanged += new DataColumnChangeEventHandler(this.QuoteDtl_AfterFieldChange);
		this.QuoteHed_Column.ColumnChanged += new DataColumnChangeEventHandler(this.QuoteHed_AfterFieldChange);
		this.edvQuoteDtl = ((EpiDataView)(this.oTrans.EpiDataViews["QuoteDtl"]));
		this.edvQuoteDtl.EpiViewNotification += new EpiViewNotification(this.edvQuoteDtl_EpiViewNotification);
		this.oTrans_adapter.BeforeAdapterMethod += new BeforeAdapterMethod(this.oTrans_adapter_BeforeAdapterMethod);
		this.edvOrderDtl = ((EpiDataView)(this.oTrans.EpiDataViews["OrderDtl"]));
		this.edvOrderDtl.EpiViewNotification += new EpiViewNotification(this.edvOrderDtl_EpiViewNotification);
		this.edvQuoteHed = ((EpiDataView)(this.oTrans.EpiDataViews["QuoteHed"]));
		this.baseToolbarsManager.ToolClick += new Infragistics.Win.UltraWinToolbars.ToolClickEventHandler(this.baseToolbarsManager_ToolClick);
		this.baseToolbarsManager.Tools["ActionsMenu"].BeforeToolDropdown += new Infragistics.Win.UltraWinToolbars.BeforeToolDropdownEventHandler(this.actionsMenu_BeforeToolDropdown);
		// End Wizard Added Variable Initialization
		
		// Begin Wizard Added Custom Method Calls

		this.pbsBtnPartRev.Click += new System.EventHandler(this.pbsBtnPartRev_Click);
		this.comboHdrTyp.ValueChanged += new System.EventHandler(this.comboHdrTyp_ValueChanged);
		this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
		this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
		this.pbsBtnRetrieve.Click += new System.EventHandler(this.pbsBtnRetrieve_Click);
		CreateRowRuleQuoteDtlQuoteCommentNotEqual___();;
		SetExtendedProperties();
		this.uGridHdr.BeforeRowUpdate += new Infragistics.Win.UltraWinGrid.CancelableRowEventHandler(this.uGridHdr_BeforeRowUpdate);
		//this.pbsbtnFirstPriceLst.Click += new System.EventHandler(this.pbsbtnFirstPriceLst_Click);
		//this.pbsSecondPriceLst.Click += new System.EventHandler(this.pbsSecondPriceLst_Click);
		CreateRowRuleQuoteDtlDocDspExpUnitPriceEquals_0_00();;
		this.GALBtnDefaultPriceList.Click += new System.EventHandler(this.GALBtnDefaultPriceList_Click);
		this.GALBtnSave.Click += new System.EventHandler(this.GALBtnSave_Click);
		this.GALBtnDelete.Click += new System.EventHandler(this.GALBtnDelete_Click);
		this.uGridHdr.BeforeRowsDeleted += new Infragistics.Win.UltraWinGrid.BeforeRowsDeletedEventHandler(this.uGridHdr_BeforeRowsDeleted);
		CreateRowRuleQuoteHedQuotedEquals_false0();;
		CreateRowRuleQuoteHedQuotedEquals_true0();;
		CreateRowRuleQuoteDtlPbsPriceOverrideByUser_cEquals_false();
		CreateRowRuleQuoteHedQuotedEquals_true();
		this.btnDfCustPL.Click += new System.EventHandler(this.btnDfCustPL_Click);
        this.epiUltraComboAttnTo.BeforeDropDown += new System.ComponentModel.CancelEventHandler(this.epiUltraComboAttnTo_BeforeDropDown);
		// End Wizard Added Custom Method Calls
	}

	public void DestroyCustomCode()
	{
		// ** Wizard Insert Location - Do not delete 'Begin/End Wizard Added Object Disposal' lines **
		// Begin Wizard Added Object Disposal

		this.pbsBtnPartRev.Click -= new System.EventHandler(this.pbsBtnPartRev_Click);
		if ((this._ud01Adapter != null))
		{
			this._ud01Adapter.Dispose();
			this._ud01Adapter = null;
		}
		this._edvUD01 = null;
		this._edvOrderHed = null;
		this.UD01_Column = null;
		this._Key1UD01 = null;
		this._Key2UD01 = null;
		this._Key3UD01 = null;
		this._Key4UD01 = null;
		this._Key5UD01 = null;
		this.baseToolbarsManager.ToolClick -= new Infragistics.Win.UltraWinToolbars.ToolClickEventHandler(this.baseToolbarsManager_ToolClickForUD01);
		this.QuoteForm.BeforeToolClick -= new Ice.Lib.Framework.BeforeToolClickEventHandler(this.QuoteForm_BeforeToolClickForUD01);
		this.QuoteForm.AfterToolClick -= new Ice.Lib.Framework.AfterToolClickEventHandler(this.QuoteForm_AfterToolClickForUD01);
		this.OrderHed_Row.EpiRowChanged -= new EpiRowChanged(this.OrderHed_AfterRowChangeForUD01);
		this.OrderHed_DataView.ListChanged -= new ListChangedEventHandler(this.OrderHed_DataView_ListChangedForUD01);
		this.OrderHed_DataView = null;
		this.OrderHed_Row.BeforeResetDataView -= new Ice.Lib.Framework.EpiDataView.BeforeResetDataViewDelegate(this.OrderHed_BeforeResetDataViewForUD01);
		this.OrderHed_Row.AfterResetDataView -= new Ice.Lib.Framework.EpiDataView.AfterResetDataViewDelegate(this.OrderHed_AfterResetDataViewForUD01);
		if ((this._ud02Adapter != null))
		{
			this._ud02Adapter.Dispose();
			this._ud02Adapter = null;
		}
		this._edvUD02 = null;
		this._edvQuoteHed = null;
		this.UD02_Column = null;
		this._Key1UD02 = null;
		this._Key2UD02 = null;
		this._Key3UD02 = null;
		this._Key4UD02 = null;
		this._Key5UD02 = null;
		this.baseToolbarsManager.ToolClick -= new Infragistics.Win.UltraWinToolbars.ToolClickEventHandler(this.baseToolbarsManager_ToolClickForUD02);
		this.QuoteForm.BeforeToolClick -= new Ice.Lib.Framework.BeforeToolClickEventHandler(this.QuoteForm_BeforeToolClickForUD02);
		this.QuoteForm.AfterToolClick -= new Ice.Lib.Framework.AfterToolClickEventHandler(this.QuoteForm_AfterToolClickForUD02);
		this.QuoteHed_Row.EpiRowChanged -= new EpiRowChanged(this.QuoteHed_AfterRowChangeForUD02);
		this.QuoteHed_DataView.ListChanged -= new ListChangedEventHandler(this.QuoteHed_DataView_ListChangedForUD02);
		this.QuoteHed_DataView = null;
		this.QuoteHed_Row.BeforeResetDataView -= new Ice.Lib.Framework.EpiDataView.BeforeResetDataViewDelegate(this.QuoteHed_BeforeResetDataViewForUD02);
		this.QuoteHed_Row.AfterResetDataView -= new Ice.Lib.Framework.EpiDataView.AfterResetDataViewDelegate(this.QuoteHed_AfterResetDataViewForUD02);
		this.comboHdrTyp.ValueChanged -= new System.EventHandler(this.comboHdrTyp_ValueChanged);
		this.btnSave.Click -= new System.EventHandler(this.btnSave_Click);
		this.btnNew.Click -= new System.EventHandler(this.btnNew_Click);
		this.oTrans_adapter.AfterAdapterMethod -= new AfterAdapterMethod(this.oTrans_adapter_AfterAdapterMethod);
		this.oTrans_adapter = null;
		this.pbsBtnRetrieve.Click -= new System.EventHandler(this.pbsBtnRetrieve_Click);
		this.UD01_Column.ColumnChanged -= new DataColumnChangeEventHandler(this.UD01_AfterFieldChange);
		this.QuoteDtl_Column.ColumnChanged -= new DataColumnChangeEventHandler(this.QuoteDtl_AfterFieldChange);
		this.QuoteHed_Column.ColumnChanged -= new DataColumnChangeEventHandler(this.QuoteHed_AfterFieldChange);
		this.edvQuoteDtl.EpiViewNotification -= new EpiViewNotification(this.edvQuoteDtl_EpiViewNotification);
		this.edvQuoteDtl = null;
		this.oTrans_adapter.BeforeAdapterMethod -= new BeforeAdapterMethod(this.oTrans_adapter_BeforeAdapterMethod);
		this.edvOrderDtl.EpiViewNotification -= new EpiViewNotification(this.edvOrderDtl_EpiViewNotification);
		this.edvOrderDtl = null;
		this.uGridHdr.BeforeRowUpdate -= new Infragistics.Win.UltraWinGrid.CancelableRowEventHandler(this.uGridHdr_BeforeRowUpdate);
		//this.pbsbtnFirstPriceLst.Click -= new System.EventHandler(this.pbsbtnFirstPriceLst_Click);
		//this.pbsSecondPriceLst.Click -= new System.EventHandler(this.pbsSecondPriceLst_Click);
		this.GALBtnDefaultPriceList.Click -= new System.EventHandler(this.GALBtnDefaultPriceList_Click);
		this.GALBtnSave.Click -= new System.EventHandler(this.GALBtnSave_Click);
		this.GALBtnDelete.Click -= new System.EventHandler(this.GALBtnDelete_Click);

		this.uGridHdr.BeforeRowsDeleted -= new Infragistics.Win.UltraWinGrid.BeforeRowsDeletedEventHandler(this.uGridHdr_BeforeRowsDeleted);
		this.edvQuoteHed = null;
		this.baseToolbarsManager.ToolClick -= new Infragistics.Win.UltraWinToolbars.ToolClickEventHandler(this.baseToolbarsManager_ToolClick);
		this.btnDfCustPL.Click -= new System.EventHandler(this.btnDfCustPL_Click);
        this.epiUltraComboAttnTo.BeforeDropDown -= new System.ComponentModel.CancelEventHandler(this.epiUltraComboAttnTo_BeforeDropDown);
		// End Wizard Added Object Disposal

		// Begin Custom Code Disposal

		// End Custom Code Disposal
	}
    



	private void InitializeUD01Adapter()
	{
		// Create an instance of the Adapter.
		this._ud01Adapter = new UD01Adapter(this.oTrans);
		this._ud01Adapter.BOConnect();

		// Add Adapter Table to List of Views
		// This allows you to bind controls to the custom UD Table
		this._edvUD01 = new EpiDataView();
		this._edvUD01.dataView = new DataView(this._ud01Adapter.UD01Data.UD01);
		this._edvUD01.AddEnabled = true;
		//this._edvUD01.AddText = "New Quote Header";
		if ((this.oTrans.EpiDataViews.ContainsKey("UD01View") == false))
		{
			this.oTrans.Add("UD01View", this._edvUD01);
		}

		// Initialize DataTable variable
		this.UD01_Column = this._ud01Adapter.UD01Data.UD01;

		// Initialize EpiDataView field.
		this._edvOrderHed = ((EpiDataView)(this.oTrans.EpiDataViews["OrderHed"]));

		// Set the parent view / keys for UD child view
		string[] parentKeyFields = new string[1];
		string[] childKeyFields = new string[1];
		parentKeyFields[0] = "QuoteNum";
		childKeyFields[0] = "Key1";
		this._edvUD01.SetParentView(this._edvOrderHed, parentKeyFields, childKeyFields);

		if ((this.oTrans.PrimaryAdapter != null))
		{
			// this.oTrans.PrimaryAdapter.GetCurrentDataSet(Ice.Lib.Searches.DataSetMode.RowsDataSet).Tables.Add(this._edvUD01.dataView.Table.Clone())
		}

	}

	private void GetUD01Data(string key1, string key2, string key3, string key4, string key5)
	{
		if ((this._Key1UD01 != key1) || (this._Key2UD01 != key2) || (this._Key3UD01 != key3) || (this._Key4UD01 != key4) || (this._Key5UD01 != key5))
		{
			// Build where clause for search.
			string whereClause = "Key1 = \'" + key1 + "\' And Key2 = \'" + key2 + "\' And Key3 = \'" + key3 + "\' And Key4 = \'" + key4 + "\'";
			System.Collections.Hashtable whereClauses = new System.Collections.Hashtable(1);
			whereClauses.Add("UD01", whereClause);

			// Call the adapter search.
			SearchOptions searchOptions = SearchOptions.CreateRuntimeSearch(whereClauses, DataSetMode.RowsDataSet);
			this._ud01Adapter.InvokeSearch(searchOptions);

			if ((this._ud01Adapter.UD01Data.UD01.Rows.Count > 0))
			{
				this._edvUD01.Row = 0;
			} else
			{
				this._edvUD01.Row = -1;
			}

			// Notify that data was updated.
			this._edvUD01.Notify(new EpiNotifyArgs(this.oTrans, this._edvUD01.Row, this._edvUD01.Column));

			// Set key fields to their new values.
			this._Key1UD01 = key1;
			this._Key2UD01 = key2;
			this._Key3UD01 = key3;
			this._Key4UD01 = key4;
			this._Key5UD01 = key5;
		}
	}

	private void GetNewUD01Record()
	{
		DataRow parentViewRow = this._edvOrderHed.CurrentDataRow;
		// Check for existence of Parent Row.
		if ((parentViewRow == null))
		{
			return;
		}
		if (this._ud01Adapter.GetaNewUD01())
		{
			string quotenum = parentViewRow["QuoteNum"].ToString();

			// Get unique row count id for Key5
			int rowCount = this._ud01Adapter.UD01Data.UD01.Rows.Count;
			int lineNum = rowCount;
			bool goodIndex = false;
			while ((goodIndex == false))
			{
				// Check to see if index exists
				DataRow[] matchingRows = this._ud01Adapter.UD01Data.UD01.Select("Key5 = \'" + lineNum.ToString() + "\'");
				if ((matchingRows.Length > 0))
				{
					lineNum = (lineNum + 1);
				} else
				{
					goodIndex = true;
				}
			}

			// Set initial UD Key values
			DataRow editRow = this._ud01Adapter.UD01Data.UD01.Rows[(rowCount - 1)];
			editRow.BeginEdit();
			editRow["Key1"] = quotenum;
			editRow["Key2"] = string.Empty;
			editRow["Key3"] = string.Empty;
			editRow["Key4"] = string.Empty;
			editRow["Key5"] = lineNum.ToString();
			editRow.EndEdit();

			// Notify that data was updated.
			this._edvUD01.Notify(new EpiNotifyArgs(this.oTrans, (rowCount - 1), this._edvUD01.Column));
		}
	}

	private void SaveUD01Record()
	{
		// Save adapter data
		this._ud01Adapter.Update();
	}

	private void DeleteUD01Record()
	{
		// Check to see if deleted view is ancestor view
		bool isAncestorView = false;
		Ice.Lib.Framework.EpiDataView parView = this._edvUD01.ParentView;
		while ((parView != null))
		{
			if ((this.oTrans.LastView == parView))
			{
				isAncestorView = true;
				break;
			} else
			{
				parView = parView.ParentView;
			}
		}

		// If Ancestor View then delete all child rows
		if (isAncestorView)
		{
			DataRow[] drsDeleted = this._ud01Adapter.UD01Data.UD01.Select("Key1 = \'" + this._Key1UD01 + "\' AND Key2 = \'" + this._Key2UD01 + "\' AND Key3 = \'" + this._Key3UD01 + "\' AND Key4 = \'" + this._Key4UD01 + "\'");
			for (int i = 0; (i < drsDeleted.Length); i = (i + 1))
			{
				this._ud01Adapter.Delete(drsDeleted[i]);
			}
		} else
		{
			if ((this.oTrans.LastView == this._edvUD01))
			{
				if ((this._edvUD01.Row >= 0))
				{
					DataRow drDeleted = ((DataRow)(this._ud01Adapter.UD01Data.UD01.Rows[this._edvUD01.Row]));
					if ((drDeleted != null))
					{
						if (this._ud01Adapter.Delete(drDeleted))
						{
							if ((_edvUD01.Row > 0))
							{
								_edvUD01.Row = (_edvUD01.Row - 1);
							}

							// Notify that data was updated.
							this._edvUD01.Notify(new EpiNotifyArgs(this.oTrans, this._edvUD01.Row, this._edvUD01.Column));
						}
					}
				}
			}
		}
	}

	private void UndoUD01Changes()
	{
		this._ud01Adapter.UD01Data.RejectChanges();

		// Notify that data was updated.
		this._edvUD01.Notify(new EpiNotifyArgs(this.oTrans, this._edvUD01.Row, this._edvUD01.Column));
	}

	private void ClearUD01Data()
	{
		this._Key1UD01 = string.Empty;
		this._Key2UD01 = string.Empty;
		this._Key3UD01 = string.Empty;
		this._Key4UD01 = string.Empty;
		this._Key5UD01 = string.Empty;

		this._ud01Adapter.UD01Data.Clear();

		// Notify that data was updated.
		this._edvUD01.Notify(new EpiNotifyArgs(this.oTrans, this._edvUD01.Row, this._edvUD01.Column));
	}

	private void baseToolbarsManager_ToolClickForUD01(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs args)
	{
		// EpiMessageBox.Show(args.Tool.Key);
		switch (args.Tool.Key)
		{
			case "EpiAddNewNew UD01":
				GetNewUD01Record();
				break;
	
			case "ClearTool":
				pbsPriceListDescription.Text = "";
				GALBtnDefaultPriceList.ReadOnly = true;
				ClearUD01Data();
				QuoteHeader_Clear() ;
				break;
	
			case "UndoTool":
				UndoUD01Changes();
				break;
		}
	}

	private void QuoteForm_BeforeToolClickForUD01(object sender, Ice.Lib.Framework.BeforeToolClickEventArgs args)
	{
		// EpiMessageBox.Show(args.Tool.Key);
		switch (args.Tool.Key)
		{
			case "SaveTool":
				SaveUD01Record();
				break;
		}
	}

	private void QuoteForm_AfterToolClickForUD01(object sender, Ice.Lib.Framework.AfterToolClickEventArgs args)
	{
		// EpiMessageBox.Show(args.Tool.Key);
		switch (args.Tool.Key)
		{
			case "DeleteTool":
				if ((args.Cancelled == false))
				{
					DeleteUD01Record();
				}
				break;
		}
	}

	private void OrderHed_AfterRowChangeForUD01(EpiRowChangedArgs args)
	{
		// ** add AfterRowChange event handler
		string quotenum = args.CurrentView.dataView[args.CurrentRow]["QuoteNum"].ToString();
		GetUD01Data(quotenum, string.Empty, string.Empty, string.Empty, string.Empty);
	}

	private void OrderHed_DataView_ListChangedForUD01(object sender, ListChangedEventArgs args)
	{
		// ** add ListChanged event handler
		string quotenum = OrderHed_DataView[0]["QuoteNum"].ToString();
		GetUD01Data(quotenum, string.Empty, string.Empty, string.Empty, string.Empty);
	}

	private void OrderHed_BeforeResetDataViewForUD01(object sender, EventArgs args)
	{
		// ** remove ListChanged event handler
		this.OrderHed_DataView.ListChanged -= new ListChangedEventHandler(this.OrderHed_DataView_ListChangedForUD01);
	}

	private void OrderHed_AfterResetDataViewForUD01(object sender, EventArgs args)
	{
		// ** reassign DataView and add ListChanged event handler
		this.OrderHed_DataView = this.OrderHed_Row.dataView;
		this.OrderHed_DataView.ListChanged += new ListChangedEventHandler(this.OrderHed_DataView_ListChangedForUD01);
	}

	private void InitializeUD02Adapter()
	{
		// Create an instance of the Adapter.
		this._ud02Adapter = new UD02Adapter(this.oTrans);
		this._ud02Adapter.BOConnect();

		// Add Adapter Table to List of Views
		// This allows you to bind controls to the custom UD Table
		this._edvUD02 = new EpiDataView();
		this._edvUD02.dataView = new DataView(this._ud02Adapter.UD02Data.UD02);
		this._edvUD02.AddEnabled = true;
		//this._edvUD02.AddText = "New UD02";
		if ((this.oTrans.EpiDataViews.ContainsKey("UD02View") == false))
		{
			this.oTrans.Add("UD02View", this._edvUD02);
		}

		// Initialize DataTable variable
		this.UD02_Column = this._ud02Adapter.UD02Data.UD02;

		// Initialize EpiDataView field.
		this._edvQuoteHed = ((EpiDataView)(this.oTrans.EpiDataViews["QuoteHed"]));

		// Set the parent view / keys for UD child view
		string[] parentKeyFields = new string[1];
		string[] childKeyFields = new string[1];
		parentKeyFields[0] = "QuoteNum";
		childKeyFields[0] = "Key1";
		this._edvUD02.SetParentView(this._edvQuoteHed, parentKeyFields, childKeyFields);

		if ((this.oTrans.PrimaryAdapter != null))
		{
			// this.oTrans.PrimaryAdapter.GetCurrentDataSet(Ice.Lib.Searches.DataSetMode.RowsDataSet).Tables.Add(this._edvUD02.dataView.Table.Clone())
		}

	}

	private void GetUD02Data(string key1, string key2, string key3, string key4, string key5)
	{
		if ((this._Key1UD02 != key1) || (this._Key2UD02 != key2) || (this._Key3UD02 != key3) || (this._Key4UD02 != key4) || (this._Key5UD02 != key5))
		{
			// Build where clause for search.
			string whereClause = "Key1 = \'" + key1 + "\' And Key2 = \'" + key2 + "\' And Key3 = \'" + key3 + "\' And Key4 = \'" + key4 + "\'";
			System.Collections.Hashtable whereClauses = new System.Collections.Hashtable(1);
			whereClauses.Add("UD02", whereClause);

			// Call the adapter search.
			SearchOptions searchOptions = SearchOptions.CreateRuntimeSearch(whereClauses, DataSetMode.RowsDataSet);
			this._ud02Adapter.InvokeSearch(searchOptions);

			if ((this._ud02Adapter.UD02Data.UD02.Rows.Count > 0))
			{
				this._edvUD02.Row = 0;
			} else
			{
				this._edvUD02.Row = -1;
			}

			// Notify that data was updated.
			this._edvUD02.Notify(new EpiNotifyArgs(this.oTrans, this._edvUD02.Row, this._edvUD02.Column));

			// Set key fields to their new values.
			this._Key1UD02 = key1;
			this._Key2UD02 = key2;
			this._Key3UD02 = key3;
			this._Key4UD02 = key4;
			this._Key5UD02 = key5;
		}
	}

	private void GetNewUD02Record()
	{
		String qHdrVal = this._edvUD01.dataView[this._edvUD01.Row]["Key3"].ToString();
		String qDesc = this._edvUD01.dataView[this._edvUD01.Row]["Character01"].ToString();
		String qSeq = this._edvUD01.dataView[this._edvUD01.Row]["Number01"].ToString();

		DataRow parentViewRow = this._edvQuoteHed.CurrentDataRow;
		// Check for existence of Parent Row.
		if ((parentViewRow == null))
		{
			return;
		}
		if (this._ud02Adapter.GetaNewUD02())
		{
			string quotenum = parentViewRow["QuoteNum"].ToString();

			// Get unique row count id for Key5
			int rowCount = this._ud02Adapter.UD02Data.UD02.Rows.Count;
			int lineNum = rowCount;
			bool goodIndex = false;
			while ((goodIndex == false))
			{
				// Check to see if index exists
				DataRow[] matchingRows = this._ud02Adapter.UD02Data.UD02.Select("Key5 = \'" + lineNum.ToString() + "\'");
				if ((matchingRows.Length > 0))
				{
					lineNum = (lineNum + 1);
				} else
				{
					goodIndex = true;
				}
			}

			// Set initial UD Key values
			DataRow editRow = this._ud02Adapter.UD02Data.UD02.Rows[(rowCount - 1)];
			editRow.BeginEdit();
			editRow["Key1"] = quotenum;
			editRow["Key2"] = string.Empty;
			editRow["Key3"] = string.Empty;
			editRow["Key4"] = qHdrVal;
			editRow["Key5"] = lineNum.ToString();
			editRow["Character01"] = qDesc;
			editRow["Number01"] = qSeq; 
			editRow.EndEdit();

			// Notify that data was updated.
			this._edvUD02.Notify(new EpiNotifyArgs(this.oTrans, (rowCount - 1), this._edvUD02.Column));
		}
	}

	private void SaveUD02Record()
	{
		// Save adapter data
		this._ud02Adapter.Update();
	}

	private void DeleteUD02Record()
	{
		// Check to see if deleted view is ancestor view
		bool isAncestorView = false;
		Ice.Lib.Framework.EpiDataView parView = this._edvUD02.ParentView;
		while ((parView != null))
		{
			if ((this.oTrans.LastView == parView))
			{
				isAncestorView = true;
				break;
			} else
			{
				parView = parView.ParentView;
			}
		}

		// If Ancestor View then delete all child rows
		if (isAncestorView)
		{
			DataRow[] drsDeleted = this._ud02Adapter.UD02Data.UD02.Select("Key1 = \'" + this._Key1UD02 + "\' AND Key2 = \'" + this._Key2UD02 + "\' AND Key3 = \'" + this._Key3UD02 + "\' AND Key4 = \'" + this._Key4UD02 + "\'");
			for (int i = 0; (i < drsDeleted.Length); i = (i + 1))
			{
				this._ud02Adapter.Delete(drsDeleted[i]);
			}
		} else
		{
			if ((this.oTrans.LastView == this._edvUD02))
			{
				if ((this._edvUD02.Row >= 0))
				{
					DataRow drDeleted = ((DataRow)(this._ud02Adapter.UD02Data.UD02.Rows[this._edvUD02.Row]));
					if ((drDeleted != null))
					{
						if (this._ud02Adapter.Delete(drDeleted))
						{
							if ((_edvUD02.Row > 0))
							{
								_edvUD02.Row = (_edvUD02.Row - 1);
							}

							// Notify that data was updated.
							this._edvUD02.Notify(new EpiNotifyArgs(this.oTrans, this._edvUD02.Row, this._edvUD02.Column));
						}
					}
				}
			}
		}
	}

	private void UndoUD02Changes()
	{
		this._ud02Adapter.UD02Data.RejectChanges();

		// Notify that data was updated.
		this._edvUD02.Notify(new EpiNotifyArgs(this.oTrans, this._edvUD02.Row, this._edvUD02.Column));
	}

	private void ClearUD02Data()
	{
		this._Key1UD02 = string.Empty;
		this._Key2UD02 = string.Empty;
		this._Key3UD02 = string.Empty;
		this._Key4UD02 = string.Empty;
		this._Key5UD02 = string.Empty;

		this._ud02Adapter.UD02Data.Clear();

		// Notify that data was updated.
		this._edvUD02.Notify(new EpiNotifyArgs(this.oTrans, this._edvUD02.Row, this._edvUD02.Column));
	}

	private void baseToolbarsManager_ToolClickForUD02(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs args)
	{
		// EpiMessageBox.Show(args.Tool.Key);
		switch (args.Tool.Key)
		{
			case "EpiAddNewNew UD02":
				GetNewUD02Record();
				break;

			case "ClearTool":
				ClearUD02Data();
				break;

			case "UndoTool":
				UndoUD02Changes();
				break;
		}
	}

	private void QuoteForm_BeforeToolClickForUD02(object sender, Ice.Lib.Framework.BeforeToolClickEventArgs args)
	{
		// EpiMessageBox.Show(args.Tool.Key);
		switch (args.Tool.Key)
		{
			case "SaveTool":
				SaveUD02Record();
				break;
		}
	}

	private void QuoteForm_AfterToolClickForUD02(object sender, Ice.Lib.Framework.AfterToolClickEventArgs args)
	{
		// EpiMessageBox.Show(args.Tool.Key);
		switch (args.Tool.Key)
		{
			case "DeleteTool":
				if ((args.Cancelled == false))
				{
					DeleteUD02Record();
				}
				break;
		}
	}

	private void QuoteHed_AfterRowChangeForUD02(EpiRowChangedArgs args)
	{
		// ** add AfterRowChange event handler
		string quotenum = args.CurrentView.dataView[args.CurrentRow]["QuoteNum"].ToString();
		GetUD02Data(quotenum, string.Empty, string.Empty, string.Empty, string.Empty);
	}

	private void QuoteHed_DataView_ListChangedForUD02(object sender, ListChangedEventArgs args)
	{
		// ** add ListChanged event handler
		string quotenum = QuoteHed_DataView[0]["QuoteNum"].ToString();
		GetUD02Data(quotenum, string.Empty, string.Empty, string.Empty, string.Empty);
	}

	private void QuoteHed_BeforeResetDataViewForUD02(object sender, EventArgs args)
	{
		// ** remove ListChanged event handler
		this.QuoteHed_DataView.ListChanged -= new ListChangedEventHandler(this.QuoteHed_DataView_ListChangedForUD02);
	}

	private void QuoteHed_AfterResetDataViewForUD02(object sender, EventArgs args)
	{
		// ** reassign DataView and add ListChanged event handler
		this.QuoteHed_DataView = this.QuoteHed_Row.dataView;
		this.QuoteHed_DataView.ListChanged += new ListChangedEventHandler(this.QuoteHed_DataView_ListChangedForUD02);
	}

	private void SearchOnUD01AdapterFillDropDown()
	{
		// Wizard Generated Search Method
		// You will need to call this method from another method in custom code
		// For example, [Form]_Load or [Button]_Click
		Int32 quoteNum = (int)this._edvOrderHed.dataView[this._edvOrderHed.Row]["QuoteNum"];
		
		bool recSelected;
		string whereClause = "Key1 = '" + quoteNum.ToString() + "'";
		System.Data.DataSet dsUD01Adapter = Ice.UI.FormFunctions.SearchFunctions.listLookup(this.oTrans, "UD01Adapter", out recSelected, false, whereClause);
		if (recSelected)
		{
			// Set EpiUltraCombo Properties
			this.uComboQuoteHdr.ValueMember = "Key3";
			this.uComboQuoteHdr.DataSource = dsUD01Adapter;
			this.uComboQuoteHdr.DisplayMember = "Character01";
			string[] fields = new string[] {
					"Character01"};
			this.uComboQuoteHdr.SetColumnFilter(fields);
		}
	}

	private void SearchOnUD01AdapterFillDropDown_QuoteHeader()
	{
		// Wizard Generated Search Method
		// You will need to call this method from another method in custom code
		// For example, [Form]_Load or [Button]_Click
		this.uComboQtHdr.DataSource = "";
		EpiDataView epiQHedV = (EpiDataView)(oTrans.EpiDataViews["QuoteHed"]);
		Int32 qNum = (int)epiQHedV.dataView[epiQHedV.Row]["QuoteNum"];
		
		bool recSelected;
		string whereClause = "Key1 = '" + qNum.ToString() + "'";
		System.Data.DataSet dsUD01Adapter = Ice.UI.FormFunctions.SearchFunctions.listLookup(this.oTrans, "UD01Adapter", out recSelected, false, whereClause);
		if (recSelected)
		{
			// Set EpiUltraCombo Properties
			this.uComboQtHdr.ValueMember = "Key3";
			this.uComboQtHdr.DataSource = dsUD01Adapter;
			this.uComboQtHdr.DisplayMember = "Character01";
			string[] fields = new string[] {
					"Character01"};
			this.uComboQtHdr.SetColumnFilter(fields);
		}
	}


	private void comboHdrTyp_ValueChanged(object sender, System.EventArgs args)
	{
		// ** Place Event Handling Code Here **
		if(!string.IsNullOrEmpty(Convert.ToString(comboHdrTyp.Value)) && Convert.ToString(comboHdrTyp.Value) == "2")
		{
			SearchOnUD01AdapterFillDropDown();
		} 
	}


	private void SearchOnUD02AdapterFillDropDown_SectionHeader(string val)
	{
		// Wizard Generated Search Method
		// You will need to call this method from another method in custom code
		// For example, [Form]_Load or [Button]_Click
		Int32 quoteNum = (int)this._edvOrderHed.dataView[this._edvOrderHed.Row]["QuoteNum"];
		
		bool recSelected;
		string whereClause = "Key1 = '" + quoteNum.ToString() + "' AND Key3='"+val+"'";
		System.Data.DataSet dsUD02Adapter = Ice.UI.FormFunctions.SearchFunctions.listLookup(this.oTrans, "UD02Adapter", out recSelected, false, whereClause);
		if (recSelected)
		{
			// Set EpiUltraCombo Properties
			this.uComboQuoteHdr.ValueMember = "Key4";
			this.uComboQuoteHdr.DataSource = dsUD02Adapter;
			this.uComboQuoteHdr.DisplayMember = "Character01";
			string[] fields = new string[] {
					"Character01"};
			this.uComboQuoteHdr.SetColumnFilter(fields);
		}
	}	


	private void SearchOnUD02AdapterFillDropDown(string val)
	{
		// Wizard Generated Search Method
		// You will need to call this method from another method in custom code
		// For example, [Form]_Load or [Button]_Click
		this.uComboSecHrd.DataSource = "";
		EpiDataView epiQHedV = (EpiDataView)(oTrans.EpiDataViews["QuoteDtl"]);
		Int32 qNum = (int)epiQHedV.dataView[epiQHedV.Row]["QuoteNum"];
		bool recSelected;

		string whereClause = "Key1 = '" + qNum.ToString() + "' AND Key4='"+val+"'";
		System.Data.DataSet dsUD02Adapter = Ice.UI.FormFunctions.SearchFunctions.listLookup(this.oTrans, "UD02Adapter", out recSelected, false, whereClause);
		

		if (recSelected)
		{
			// Set EpiUltraCombo Properties
			this.uComboSecHrd.ValueMember = "Key3";
			this.uComboSecHrd.DataSource = dsUD02Adapter;
			this.uComboSecHrd.DisplayMember = "Character01";
			string[] fields = new string[] {
					"Character01"};
			this.uComboSecHrd.SetColumnFilter(fields);
		}
		if(dsUD02Adapter.Tables[0].Rows.Count < 1){
		
			uComboSecHrd.Enabled = false;
		}
		else{
			uComboSecHrd.Enabled = true;
		}
	}

	private void btnNew_Click(object sender, System.EventArgs args)
	{
		// ** Place Event Handling Code Here **
		Int32 quoteNum = (int)this._edvOrderHed.dataView[this._edvOrderHed.Row]["QuoteNum"];
		
		if(quoteNum.ToString() == "0"){
			oTrans.Update();
			GetNewUD01Record();
		}
		else 
		{
			GetNewUD01Record();
		}
	}

	private void QuoteHeader_Load(string qID) 
	{
		//MessageBox.Show(qID);
		DataTable _baqResultsData = null;
		try
		{
			DynamicQueryAdapter adBAQ = new DynamicQueryAdapter(oTrans);
			adBAQ.BOConnect(); // TODO: Review/remove
			var  ds = new Ice.BO.QueryExecutionDataSet();
                          
			// Parameter 1 - Should be processed on load with a filter value that returns no results.
			var paramRow = ds.ExecutionParameter.NewRow();             
            paramRow["ParameterID"] = "QuoteNum"; //This has to be the same Paramter in BAQ
            paramRow["ParameterValue"] = qID;
            paramRow["ValueType"] = "String"; //Same Type in BAQ
            paramRow["IsEmpty"] = "False";
            paramRow["RowMod"] = "A";
            ds.ExecutionParameter.Rows.Add(paramRow); 
			
			adBAQ.ExecuteByID("quoteHdr_SectionHdr", ds);
			_baqResultsData = adBAQ.QueryResults.Tables["Results"];
			uGridHdr.DataSource = _baqResultsData;
			
			this.uGridHdr.DisplayLayout.Bands[0].Columns["UD01_Key2"].Hidden = true;
			this.uGridHdr.DisplayLayout.Bands[0].Columns["UD01_Key3"].Hidden = true;
			this.uGridHdr.DisplayLayout.Bands[0].Columns["UD01_Key4"].Hidden = true;
			this.uGridHdr.DisplayLayout.Bands[0].Columns["UD01_Key5"].Hidden = true;
			this.uGridHdr.DisplayLayout.Bands[0].Columns["UD02_Key2"].Hidden = true;
			this.uGridHdr.DisplayLayout.Bands[0].Columns["UD02_Key3"].Hidden = true;
			this.uGridHdr.DisplayLayout.Bands[0].Columns["UD02_Key4"].Hidden = true;
			this.uGridHdr.DisplayLayout.Bands[0].Columns["UD02_Key5"].Hidden = true;
			this.uGridHdr.DisplayLayout.Bands[0].Columns["UD01_Number01"].Hidden = true;
			this.uGridHdr.DisplayLayout.Bands[0].Columns["UD02_Number01"].Hidden = true;
			this.uGridHdr.DisplayLayout.Bands[0].Columns["UD01_Number01"].MaskInput = "#";
			this.uGridHdr.DisplayLayout.Bands[0].Columns["UD02_Number01"].MaskInput = "n";
			this.uGridHdr.DisplayLayout.AutoFitStyle = AutoFitStyle.ResizeAllColumns;
			this.uGridHdr.UpdateMode = UpdateMode.OnRowChange;

			for(int i = 0; i < _baqResultsData.Rows.Count; i++)
			{
				if(String.IsNullOrEmpty(this.uGridHdr.Rows[i].Cells["UD02_Character01"].Value.ToString()))
				{
					this.uGridHdr.Rows[i].Cells["UD02_Character01"].Activation = Infragistics.Win.UltraWinGrid.Activation.Disabled;
					this.uGridHdr.Rows[i].Cells["Calculated_SecHedSeq"].Activation = Infragistics.Win.UltraWinGrid.Activation.Disabled;
				}
			}

		} catch (Exception ex)
		{
			MessageBox.Show(ex.ToString());
		}
	}

	private void QuoteHeader_Clear() 
	{
		uGridHdr.DataSource = null;
	}

	private void QuoteForm_Load(object sender, EventArgs args)
	{
		uComboSecHrd.Enabled = false;
		uGridHdr.DataSource = null;
		this.uComboSecHrd.Value = "";
		/*EpiUltraCombo*/
		this.uComboQuoteHdr.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList;
		/*EpiCombo*/
		this.comboQuoteBy.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList;

		//this.uComboSecHrd.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList;
		//this.uComboQtHdr.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList;

		PopupMenuTool quoteHeaderTool = (PopupMenuTool)this.baseToolbarsManager.Tools["QuoteHeaderTool"];
		ButtonTool demandContract = new ButtonTool("CreateDemandContractTool");
		demandContract.SharedProps.Caption = "Create Demand Contract";
		demandContract.SharedProps.Enabled = true;
		demandContract.SharedProps.Visible = true;
		demandContract.SharedProps.AppearancesSmall.Appearance.Image = EpiUIImages.SmallEnabledImages.Images[EpiUIImages.IndexOf("MenuForm")];
		baseToolbarsManager.Tools.Add(demandContract);
		quoteHeaderTool.Tools.Insert(quoteHeaderTool.Tools.Count-1, demandContract);
        SearchOnPersonAdapterFillDropDown();
	}	

	private void oTrans_adapter_AfterAdapterMethod(object sender, AfterAdapterMethodArgs args)
	{
		// ** Argument Properties and Uses **
		// ** args.MethodName **
		// ** Add Event Handler Code **

		// ** Use MessageBox to find adapter method name
		// EpiMessageBox.Show(args.MethodName);
		switch (args.MethodName)
		{
			case "GetByID":
				
				uGridHdr.DataSource = null;
				if(checkCvrltr.Checked) 
				{
					checkCharges.Enabled = true;
					checkCharges.Enabled = true;
					txtbxCvrLtrSbj.Enabled = true;
					comboQuoteBy.Enabled = true;
				}
				else 
				{
					
					checkCharges.Enabled = false;
					checkCharges.Enabled = false;
					txtbxCvrLtrSbj.Enabled = false;
					comboQuoteBy.Enabled = false;
				}
				pbsPriceListDescription.Text = "";
				//pbsSecPriceListDescription.Text = "";
				GALBtnDefaultPriceList.Enabled = true;
				break;
			case "GetNewQuoteHed":
				uGridHdr.DataSource = null;
				uComboSecHrd.Enabled = false;
				//pbsbtnFirstPriceLst.Enabled = true;
				//pbsSecondPriceLst.Enabled = true;
				GALBtnDefaultPriceList.Enabled = true;
				uComboSecHrd.Value = "";
				if(checkCvrltr.Checked) 
				{
					checkCharges.Enabled = true;
					checkCharges.Enabled = true;
					txtbxCvrLtrSbj.Enabled = true;
					comboQuoteBy.Enabled = true;
				}
				else 
				{
					
					checkCharges.Enabled = false;
					checkCharges.Enabled = false;
					txtbxCvrLtrSbj.Enabled = false;
					comboQuoteBy.Enabled = false;
				}
				pbsPriceListDescription.Text = "";
				//pbsSecPriceListDescription.Text = "";
				break;
			case "GetNewQuoteDtl":
				/*SearchOnUD01AdapterFillDropDown_QuoteHeader();
				if(string.IsNullOrEmpty(Convert.ToString(uComboSecHrd.Value)))
				{
					uComboSecHrd.Value = "";
					uComboSecHrd.Enabled = false;
				}else 
				{
					uComboSecHrd.Enabled = true;
				}*/
				break;
             case "Update":

				//SyncQuoteDtlPriceWithParentQuoteNum();

			 break;
             case "PreRefreshQty":
            
             break;
		}

	}
	
	private void pbsBtnRetrieve_Click(object sender, System.EventArgs args)
	{
		// ** Place Event Handling Code Here **
		Int32 quoteNum = (int)this._edvOrderHed.dataView[this._edvOrderHed.Row]["QuoteNum"];
		QuoteHeader_Load(quoteNum.ToString());
		
	}

	private void btnSave_Click(object sender, System.EventArgs args)
	{
		// ** Place Event Handling Code Here **
		if(string.IsNullOrEmpty(Convert.ToString(comboHdrTyp.Value)))
		{
			MessageBox.Show("Please Select Header Type");
		}else if(uComboQuoteHdr.Enabled && string.IsNullOrEmpty(Convert.ToString(uComboQuoteHdr.Value)))
		{
			MessageBox.Show("Please Select Quote Type");
		}else if(!string.IsNullOrEmpty(Convert.ToString(comboHdrTyp.Value)) && string.IsNullOrEmpty(Convert.ToString(txtboxHdrDsc.Value)))
		{
			MessageBox.Show("Please Enter Header Description");
		} 
		else 
		{
			if(comboHdrTyp.Value.ToString() == "1")
			{
				SaveUD01Record();
				//SearchOnUD01AdapterFillDropDown_QuoteHeader();
			}
			if(comboHdrTyp.Value.ToString() == "2")
			{
				GetNewUD02Record();
				SaveUD02Record();
			}
			ClearUD01Data();
			ClearUD02Data();
			Int32 quoteNum = (int)this._edvOrderHed.dataView[this._edvOrderHed.Row]["QuoteNum"];
			QuoteHeader_Load(quoteNum.ToString());	
		}
	}
    
   
	private void UD01_AfterFieldChange(object sender, DataColumnChangeEventArgs args)
	{
		// ** Argument Properties and Uses **
		// args.Row["FieldName"]
		// args.Column, args.ProposedValue, args.Row
		// Add Event Handler Code
		switch (args.Column.ColumnName)
		{
			case "Key2":
				if(Convert.ToString(comboHdrTyp.Value) == "2") {
					uComboQuoteHdr.Enabled = true;
				}
				else 
				{
					uComboQuoteHdr.Enabled = false;
				}
				break;
		}
	}

	private void QuoteDtl_AfterFieldChange(object sender, DataColumnChangeEventArgs args)
	{
		// ** Argument Properties and Uses **
		// args.Row["FieldName"]
		// args.Column, args.ProposedValue, args.Row
		// Add Event Handler Code
		switch (args.Column.ColumnName)
		{
			case "PartNum":
				args.Row["DspExpUnitPrice"]= 10.00;
				this.oTrans.NotifyAll();
			break;
			case "pbsQuoteHed_c":
				/*if(!string.IsNullOrEmpty(Convert.ToString(uComboQtHdr.Value))) 
				{
					string qHedValue = uComboQtHdr.Value.ToString();
					SearchOnUD02AdapterFillDropDown(qHedValue);
					args.Row["pbsSectionHed_c"]= "";
					
				}
				else 
				{
					uComboSecHrd.Enabled = false;
					uComboSecHrd.Value = "";
				}*/
				break;
           case"DocDspExpUnitPrice":
        
           break;
		}
	}


	private void QuoteHed_AfterFieldChange(object sender, DataColumnChangeEventArgs args)
	{
		// ** Argument Properties and Uses **
		// args.Row["FieldName"]
		// args.Column, args.ProposedValue, args.Row
		// Add Event Handler Code
		switch (args.Column.ColumnName)
		{
			case "CustomerCustID":
                  // oTrans.Update();
	             //SyncQuoteDtlPriceWithParentQuoteNum();
				break;
	
			case "pbsCvrLtr_c":
				if(checkCvrltr.Checked) {
					checkCharges.Enabled = true;
					checkCharges.Enabled = true;
					txtbxCvrLtrSbj.Enabled = true;
					comboQuoteBy.Enabled = true;
				}
				else 
				{
					checkCharges.Enabled = false;
					checkCharges.Enabled = false;
					txtbxCvrLtrSbj.Enabled = false;
					comboQuoteBy.Enabled = false;
				}
				break;

		}
	}

	private void edvQuoteDtl_EpiViewNotification(EpiDataView view, EpiNotifyArgs args)
	{
		// ** Argument Properties and Uses **
		// view.dataView[args.Row]["FieldName"]
		// args.Row, args.Column, args.Sender, args.NotifyType
		// NotifyType.Initialize, NotifyType.AddRow, NotifyType.DeleteRow, NotifyType.InitLastView, NotifyType.InitAndResetTreeNodes
		if ((args.NotifyType == EpiTransaction.NotifyType.Initialize))
		{
			if ((args.Row > -1) )
			{
				if(!(string.IsNullOrEmpty(view.dataView[args.Row]["QuoteLine"].ToString())))
				{
				
						//SearchOnUD01AdapterFillDropDown_QuoteHeader();
						//EpiDataView epiQHedV = (EpiDataView)(oTrans.EpiDataViews["QuoteDtl"]);
						//string val = epiQHedV.dataView[epiQHedV.Row]["pbsQuoteHed_c"].ToString();
						//SearchOnUD02AdapterFillDropDown(val);
		
					
					if(checkCvrltr.Checked) 
					{
						checkCharges.Enabled = true;
						checkCharges.Enabled = true;
						txtbxCvrLtrSbj.Enabled = true;
						comboQuoteBy.Enabled = true;
					}
					else 
					{
						
						checkCharges.Enabled = false;
						checkCharges.Enabled = false;
						txtbxCvrLtrSbj.Enabled = false;
						comboQuoteBy.Enabled = false;
					}

	
				}

			}
		}
	}

	private void oTrans_adapter_BeforeAdapterMethod(object sender, BeforeAdapterMethodArgs args)
	{
		// ** Argument Properties and Uses **
		// ** args.MethodName **
		// ** Add Event Handler Code **

		// ** Use MessageBox to find adapter method name
		// EpiMessageBox.Show(args.MethodName);
		switch (args.MethodName)
		{
			case "Update":
				break;
			case "GetQuotedInfo":
					EpiDataView epiQHedV = (EpiDataView)(oTrans.EpiDataViews["QuoteHed"]);
					if(edvQuoteDtl.HasRow && (bool)epiQHedV.dataView[epiQHedV.Row]["Quoted"])
					{
						foreach(DataRow row in edvQuoteDtl.dataView.Table.Rows)
						{
							if(Convert.ToDecimal(row["DocDspExpUnitPrice"]) == 0.00M)
							{
								EpiMessageBox.Show(string.Format("Unit Price for line # {0} is Equal to 0", row["QuoteLine"]), "Warning Message");
							}
						}
					}
	
				break;
               case "PreRefreshQty":
               args.Cancel=true;
              
               //this.oTrans.SetCurrentEvent(TransactionEvent.None);
               break;
               case "ApplyQuoteHeadPropagatedFieldsToExistingQuoteDtls":
               args.Cancel=true;
               //this.oTrans.SetCurrentEvent(TransactionEvent.None);
               break;
		}

	}

	private void CreateRowRuleQuoteDtlQuoteCommentNotEqual___()
	{
		// Description: LineComments
		// **** begin autogenerated code ****
		RuleAction warningQuoteDtl_RowAction = RuleAction.AddRowSettings(this.oTrans, "QuoteDtl", true, SettingStyle.Warning);
		RuleAction[] ruleActions = new RuleAction[] {
				warningQuoteDtl_RowAction};
		// Create RowRule and add to the EpiDataView.
		RowRule rrCreateRowRuleQuoteDtlQuoteCommentNotEqual___ = new RowRule("QuoteDtl.QuoteComment", RuleCondition.NotEqual, "", ruleActions);
		((EpiDataView)(this.oTrans.EpiDataViews["QuoteDtl"])).AddRowRule(rrCreateRowRuleQuoteDtlQuoteCommentNotEqual___);
		// **** end autogenerated code ****
	}


	private void edvOrderDtl_EpiViewNotification(EpiDataView view, EpiNotifyArgs args)
	{
		// ** Argument Properties and Uses **
		// view.dataView[args.Row]["FieldName"]
		// args.Row, args.Column, args.Sender, args.NotifyType
		// NotifyType.Initialize, NotifyType.AddRow, NotifyType.DeleteRow, NotifyType.InitLastView, NotifyType.InitAndResetTreeNodes
		if ((args.NotifyType == EpiTransaction.NotifyType.Initialize))
		{
			if ((args.Row > -1))
			{
				if(view.dataView[view.Row]["QuoteComment"].ToString().Trim() != "")
				{
					pbsShpLineComment.Status = StatusTypes.Warning;
					pbsShpLineComment.EnabledCaption = "Line Comments";
					pbsShpLineComment.Visible = true;
	
				} else {
					pbsShpLineComment.Visible = false;
				}
			}
		}
	}
    
        private void SearchOnPersonAdapterFillDropDown()
	{
		// Wizard Generated Search Method
		// You will need to call this method from another method in custom code
		// For example, [Form]_Load or [Button]_Click

		bool recSelected;
		string whereClause = string.Empty;
		System.Data.DataSet dsPersonAdapter = Ice.UI.FormFunctions.SearchFunctions.listLookup(this.oTrans, "ContactAdapter", out recSelected, false, whereClause);
		if (recSelected)
		{
			// Set EpiUltraCombo Properties
			this.epiUltraComboAttnTo.ValueMember = "ContactName";
			this.epiUltraComboAttnTo.DataSource = dsPersonAdapter;
			this.epiUltraComboAttnTo.DisplayMember = "ContactName";
			string[] fields = new string[] {
					"ContactName"};
			this.epiUltraComboAttnTo.SetColumnFilter(fields);
		}
	}

    	private void epiUltraComboAttnTo_BeforeDropDown(object sender, System.ComponentModel.CancelEventArgs args)
	{
		// ** Place Event Handling Code Here **
           EpiDataView edvquoteHed = ((EpiDataView)(this.oTrans.EpiDataViews["QuoteHed"]));
           if(edvquoteHed.HasRow && epiUltraComboAttnTo.Rows.Count>0)
           {
             epiUltraComboAttnTo.Rows.ColumnFilters["CustNum"].FilterConditions.Clear();
             epiUltraComboAttnTo.Rows.ColumnFilters["CustNum"].FilterConditions.Add(FilterComparisionOperator.Equals,edvquoteHed.dataView[edvquoteHed.Row]["CustNum"]);
           }
           else if(epiUltraComboAttnTo.Rows.Count>0)
           {
             epiUltraComboAttnTo.Rows.ColumnFilters["CustNum"].FilterConditions.Clear();
           }
	}

	private void pbsBtnPartRev_Click(object sender, System.EventArgs args)
	{
		// ** Place Event Handling Code Here **
		EpiDataView _quoteDtlEDV = (EpiDataView) oTrans.EpiDataViews["QuoteDtl"];
		DataRow _quoteDtlDR = _quoteDtlEDV.dataView.Table.Rows[_quoteDtlEDV.Row];
		EpiBaseAdapter pA = (EpiBaseAdapter) oTrans.PrimaryAdapter;
		Erp.Adapters.QuoteAdapter qA = (Erp.Adapters.QuoteAdapter) pA;
		string _partNum = "126821"; //1
			
		var obj = ProcessCaller.LaunchAdvancedSearch(oTrans,"pbsPartSearch",true); // single select
		try 
		{ if (obj.GetType() == typeof(Hashtable))
		{
//			oTrans.ClearDataSets();
			Hashtable ht = (Hashtable)obj;
		
			foreach(DictionaryEntry entry in ht)
			{
				ArrayList a = (ArrayList) entry.Value;
				foreach(var aa in a)
				{
					_partNum = aa.ToString();					
				}
			}
		}
			// update dataset partnumber and update
			qA.QuoteData.QuoteDtl[_quoteDtlEDV.Row].PartNum = _partNum;
	
			qA.ChangePartNum( false,"");
			qA.Update();
			//_quoteDtlEDV.Notify(new EpiNotifyArgs(this, _quoteDtlEDV.Row, EpiTransaction.NotifyType.Initialize));
		} catch {}
	}

	private void SetExtendedProperties()
	{
		// Begin Wizard Added EpiDataView Initialization
		EpiDataView edvUD01View = ((EpiDataView)(this.oTrans.EpiDataViews["UD01View"]));
		EpiDataView edvQuoteDtl = ((EpiDataView)(this.oTrans.EpiDataViews["QuoteDtl"]));
		// End Wizard Added EpiDataView Initialization

		// Begin Wizard Added Conditional Block
		if (edvUD01View.dataView.Table.Columns.Contains("Number01"))
		{
			// Begin Wizard Added ExtendedProperty Settings: edvUD01View-Number01
			edvUD01View.dataView.Table.Columns["Number01"].ExtendedProperties["Format"] = "9";
			// End Wizard Added ExtendedProperty Settings: edvUD01View-Number01
		}
		EpiDataView edvQuoteHed = ((EpiDataView)(this.oTrans.EpiDataViews["QuoteHed"]));
		// End Wizard Added EpiDataView Initialization

		// Begin Wizard Added Conditional Block
		if (edvQuoteHed.dataView.Table.Columns.Contains("pbsQPriceList1_c"))
		{
			// Begin Wizard Added ExtendedProperty Settings: edvUD01View-Number01
			edvQuoteHed.dataView.Table.Columns["pbsQPriceList1_c"].ExtendedProperties["ReadOnly"] = true;
			// End Wizard Added ExtendedProperty Settings: edvUD01View-Number01
		}
		if (edvQuoteHed.dataView.Table.Columns.Contains("pbsQPriceList2_c"))
		{
			// Begin Wizard Added ExtendedProperty Settings: edvUD01View-Number01
			edvQuoteHed.dataView.Table.Columns["pbsQPriceList2_c"].ExtendedProperties["ReadOnly"] = true;
			// End Wizard Added ExtendedProperty Settings: edvUD01View-Number01
		}
		if (edvQuoteDtl.dataView.Table.Columns.Contains("GALQuoteHedDesc_c"))
		{
			// Begin Wizard Added ExtendedProperty Settings: edvQuoteDtl-GALQuoteHedDesc_c
			edvQuoteDtl.dataView.Table.Columns["GALQuoteHedDesc_c"].ExtendedProperties["ReadOnly"] = true;
			// End Wizard Added ExtendedProperty Settings: edvQuoteDtl-GALQuoteHedDesc_c

			edvQuoteDtl.dataView.Table.Columns["GALSectionHedDesc_c"].ExtendedProperties["ReadOnly"] = true;
		}
		if (edvQuoteHed.dataView.Table.Columns.Contains("PbsDemandContract_c"))
		{
			// Begin Wizard Added ExtendedProperty Settings: edvQuoteHed-PbsDemandContract_c
			edvQuoteHed.dataView.Table.Columns["PbsDemandContract_c"].ExtendedProperties["Like"] = "DmdCntHdr.DemandContract";
			// End Wizard Added ExtendedProperty Settings: edvQuoteHed-PbsDemandContract_c
		}
		if (edvQuoteDtl.dataView.Table.Columns.Contains("OverridePriceList"))
		{
			// Begin Wizard Added ExtendedProperty Settings: edvQuoteDtl-OverridePriceList
			edvQuoteDtl.dataView.Table.Columns["OverridePriceList"].ExtendedProperties["ReadOnly"] = true;
			// End Wizard Added ExtendedProperty Settings: edvQuoteDtl-OverridePriceList
		}
		// End Wizard Added Conditional Block
	}

	private void uGridHdr_BeforeRowUpdate(object sender, Infragistics.Win.UltraWinGrid.CancelableRowEventArgs args)
	{
		SaveUD01UD02();
	}

	private void SaveUD01UD02()
	{
		// ** Place Event Handling Code Here **
		UltraGridRow row = this.uGridHdr.ActiveRow;
		if(null != row)
		{
			if(row.DataChanged)
			{
				if(row.Cells["UD01_Character01"].DataChanged || row.Cells["Calculated_QHedSeq"].DataChanged){
					UD01Adapter adapterUD01 = null;
					adapterUD01 = new UD01Adapter(this.oTrans);
					adapterUD01.BOConnect();	
					
					string key1 = this._edvOrderHed.dataView[this._edvOrderHed.Row]["QuoteNum"].ToString();
					string key2 = row.Cells["UD01_Key2"].Value.ToString();
					string key3 = row.Cells["UD01_Key3"].Value.ToString();
					string key4 = row.Cells["UD01_Key4"].Value.ToString();
					string key5 = row.Cells["UD01_Key5"].Value.ToString();
				
					adapterUD01.GetByID(key1, key2, key3, key4, key5);
					DataRow drt = adapterUD01.UD01Data.UD01[0];

					drt.BeginEdit();
					drt["Character01"] = row.Cells["UD01_Character01"].Value.ToString();
					drt["Number01"] = row.Cells["Calculated_QHedSeq"].Value.ToString();
					drt["RowMod"] = "U";
					drt.EndEdit();
			
					adapterUD01.Update();
			
					adapterUD01.Dispose();
                    
                     var edvQuoteDtl =  ((EpiDataView)this.oTrans.EpiDataViews["QuoteDtl"]);
                     foreach(DataRow QtdtlRow in edvQuoteDtl.dataView.Table.Rows)
                     {
                      if(QtdtlRow["pbsQuoteHed_c"].ToString()==row.Cells["UD01_Key3"].Value.ToString())
                      {
                         QtdtlRow.BeginEdit();
                         QtdtlRow["GALQuoteHedDesc_C"]= row.Cells["UD01_Character01"].Value.ToString();
                         QtdtlRow.EndEdit();
                      }
                       this.oTrans.Update();
                     }
			}

				if(row.Cells["UD02_Character01"].DataChanged || row.Cells["Calculated_SecHedSeq"].DataChanged ){
					
					UD02Adapter adapterUD02 = null;
					adapterUD02 = new UD02Adapter(this.oTrans);
					adapterUD02.BOConnect();	
					
					string key1 = this._edvOrderHed.dataView[this._edvOrderHed.Row]["QuoteNum"].ToString();
					string key2 = row.Cells["UD02_Key2"].Value.ToString();
					string key3 = row.Cells["UD02_Key3"].Value.ToString();
					string key4 = row.Cells["UD02_Key4"].Value.ToString();
					string key5 = row.Cells["UD02_Key5"].Value.ToString();
				
					adapterUD02.GetByID(key1, key2, key3, key4, key5);
					DataRow drt = adapterUD02.UD02Data.UD02[0];

					drt.BeginEdit();
					drt["Character01"] = row.Cells["UD02_Character01"].Value.ToString();
					drt["Number01"] = row.Cells["Calculated_SecHedSeq"].Value.ToString();
					drt["RowMod"] = "U";
					drt.EndEdit();
			
					adapterUD02.Update();
			
					adapterUD02.Dispose();
					
                    var edvQuoteDtl =  ((EpiDataView)this.oTrans.EpiDataViews["QuoteDtl"]);
                     foreach(DataRow QtdtlRow in edvQuoteDtl.dataView.Table.Rows)
                     {
                      if(QtdtlRow["pbsSectionHed_c"].ToString()==row.Cells["UD02_Key3"].Value.ToString())
                      {
                         QtdtlRow.BeginEdit();
                         QtdtlRow["GALSectionHedDesc_C"]= row.Cells["UD02_Character01"].Value.ToString();
                         QtdtlRow.EndEdit();
                      }
                       this.oTrans.Update();
                     }
				}
             
              
			}
		}
	}


	private void uGridHdr_BeforeRowsDeleted(object sender, Infragistics.Win.UltraWinGrid.BeforeRowsDeletedEventArgs args)
	{
		// Event handling code generated by wizard.
		args.Cancel = true;
		try
		{
			DeleteUD01UD02();
			//this.oTrans.Delete();
		} catch (System.Exception )
		{
		}
		Int32 quoteNum = (int)this._edvOrderHed.dataView[this._edvOrderHed.Row]["QuoteNum"];
		QuoteHeader_Load(quoteNum.ToString());	
	}

	private void DeleteUD01UD02()
	{
		// ** Place Event Handling Code Here **
		UltraGridRow row = this.uGridHdr.ActiveRow;
		string qteHed = row.Cells["UD01_Key3"].Value.ToString();
		int qteHedC = FindSectHeaders(qteHed);
		if(null != row)
		{
			// Both needs to be deleted. need to find count and determine if this is the last, then delete the header too, if not, leave the Qheader

			if(row.Cells["UD02_Key3"].Value.ToString() != "")
			{
				
				UD02Adapter adapterUD02 = null;
				adapterUD02 = new UD02Adapter(this.oTrans);
				adapterUD02.BOConnect();	
				
				string key1 = this._edvOrderHed.dataView[this._edvOrderHed.Row]["QuoteNum"].ToString();
				string key2 = row.Cells["UD02_Key2"].Value.ToString();
				string key3 = row.Cells["UD02_Key3"].Value.ToString();
				string key4 = row.Cells["UD02_Key4"].Value.ToString();
				string key5 = row.Cells["UD02_Key5"].Value.ToString();
			
				adapterUD02.GetByID(key1, key2, key3, key4, key5);
				adapterUD02.Delete(adapterUD02.UD02Data.UD02[0]);
		
				//adapterUD02.Update();
		
				adapterUD02.Dispose();	
			}
			
			if (qteHedC <=1)
			{
				if(row.Cells["UD01_Key3"].Value.ToString() != "")
				{
					UD01Adapter adapterUD01 = null;
					adapterUD01 = new UD01Adapter(this.oTrans);
					adapterUD01.BOConnect();	
					
					string key1 = this._edvOrderHed.dataView[this._edvOrderHed.Row]["QuoteNum"].ToString();
					string key2 = row.Cells["UD01_Key2"].Value.ToString();
					string key3 = row.Cells["UD01_Key3"].Value.ToString();
					string key4 = row.Cells["UD01_Key4"].Value.ToString();
					string key5 = row.Cells["UD01_Key5"].Value.ToString();
					
				
					adapterUD01.GetByID(key1, key2, key3, key4, key5);
					adapterUD01.Delete(adapterUD01.UD01Data.UD01[0]);
			
					//adapterUD02.Update();
			
					adapterUD01.Dispose();	
				}
			}		
		}
	}
/*	private string SearchOnPriceLstAdapterShowDialog()
	{
		// Wizard Generated Search Method
		// You will need to call this method from another method in custom code
		// For example, [Form]_Load or [Button]_Click
		string selectedLstCode="";
		bool recSelected;
		string whereClause = "StartDate <= GetDate() and EndDate >= GetDate()" ;//string.Empty;
		System.Data.DataSet dsPriceLstAdapter = Ice.UI.FormFunctions.SearchFunctions.listLookup(this.oTrans, "PriceLstAdapter", out recSelected, true, whereClause);
		if (recSelected)
		{
			System.Data.DataRow adapterRow = dsPriceLstAdapter.Tables[0].Rows[0];

			selectedLstCode = adapterRow["ListCode"].ToString() + "~" + adapterRow["ListDescription"].ToString() ;
			// Map Search Fields to Application Fields
*//*			EpiDataView edvQuoteHed = ((EpiDataView)(this.oTrans.EpiDataViews["QuoteHed"]));
			System.Data.DataRow edvQuoteHedRow = edvQuoteHed.CurrentDataRow;
			if ((edvQuoteHedRow != null))
			{
				edvQuoteHedRow.BeginEdit();
				edvQuoteHedRow["pbsQPriceList1_c"] = adapterRow["ListCode"];
				edvQuoteHedRow.EndEdit();
			}*//*
		}
		return selectedLstCode;
	} 
*/
/*	private void pbsbtnFirstPriceLst_Click(object sender, System.EventArgs args)
	{
		// ** Place Event Handling Code Here **
		string firstLstCode = SearchOnPriceLstAdapterShowDialog();
		string[] strList = firstLstCode.Split('~');
		EpiDataView edvQuoteHed = ((EpiDataView)(this.oTrans.EpiDataViews["QuoteHed"]));
		System.Data.DataRow edvQuoteHedRow = edvQuoteHed.CurrentDataRow;
		if(!string.IsNullOrWhiteSpace(firstLstCode))
		{
			if ((edvQuoteHedRow != null))
			{
				edvQuoteHedRow.BeginEdit();
				edvQuoteHedRow["pbsQPriceList1_c"] = strList[0];//adapterRow["ListCode"];
				edvQuoteHedRow["pbsQPriceList1Desc_c"] = strList[1];
				edvQuoteHedRow.EndEdit();
						
			}
			pbsPriceListDescription.Text = strList[1];
		}
	}

	private void pbsSecondPriceLst_Click(object sender, System.EventArgs args)
	{
		// ** Place Event Handling Code Here **
		string secondLstCode = SearchOnPriceLstAdapterShowDialog();
		string[] strList = secondLstCode.Split('~');
		//	EpiDataView edvQuoteHed = ((EpiDataView)(this.oTrans.EpiDataViews["QuoteHed"]));
		EpiDataView edvQuoteHed = ((EpiDataView)(this.oTrans.EpiDataViews["QuoteHed"]));
		System.Data.DataRow edvQuoteHedRow = edvQuoteHed.CurrentDataRow;
		if(!string.IsNullOrWhiteSpace(secondLstCode))
		{
			if ((edvQuoteHedRow != null))
			{
				edvQuoteHedRow.BeginEdit();
				edvQuoteHedRow["pbsQPriceList2_c"] = strList[0]; //secondLstCode; //adapterRow["ListCode"];
				edvQuoteHedRow["pbsQPriceList2Desc_c"] = strList[1];
				edvQuoteHedRow.EndEdit();
					
			}
			pbsSecPriceListDescription.Text = strList[1];
		}
	}
*/
	



	private void CreateRowRuleQuoteDtlDocDspExpUnitPriceEquals_0_00()
	{
		// Description: Unit Price
		// **** begin autogenerated code ****
		RuleAction warningQuoteDtl_RowAction = RuleAction.AddRowSettings(this.oTrans, "QuoteDtl", true, SettingStyle.Warning);
		RuleAction[] ruleActions = new RuleAction[] {
				warningQuoteDtl_RowAction};
		// Create RowRule and add to the EpiDataView.
		RowRule rrCreateRowRuleQuoteDtlDocDspExpUnitPriceEquals_0_00 = new RowRule("QuoteDtl.DocDspExpUnitPrice", RuleCondition.Equals, "0.00", ruleActions);
		((EpiDataView)(this.oTrans.EpiDataViews["QuoteDtl"])).AddRowRule(rrCreateRowRuleQuoteDtlDocDspExpUnitPriceEquals_0_00);
		// **** end autogenerated code ****
	}

	private void GALBtnDefaultPriceList_Click(object sender, System.EventArgs args)
	{
		SearchOnPriceLstAdapterShowDialog_1();
    }

	private string FindListDesc(string _listcode)
	{
		DynamicQueryAdapter dqa = new DynamicQueryAdapter(oTrans);
		dqa.BOConnect();

		
		QueryExecutionDataSet qeds = dqa.GetQueryExecutionParametersByID("GAL_QuotePriceListExt");
		qeds.ExecutionParameter.Clear();
		qeds.ExecutionParameter.AddExecutionParameterRow("qListCode", _listcode, "string", false, Guid.NewGuid(), "A");
		
		dqa.ExecuteByID("GAL_QuotePriceListExt", qeds);

		DataTable results = dqa.QueryResults.Tables["Results"];
		EpiDataView edvResults = new EpiDataView();
		edvResults.dataView = results.DefaultView;
		DataRow edvResultsDR = edvResults.dataView.Table.Rows[edvResults.Row];
		
		return edvResultsDR["PriceLst_ListDescription"].ToString();
	}
	
	private void UpdateQuoteDtl(string listCode)
	{
		bool trig = false;
		EpiDataView edvQuoteDtl = (EpiDataView) oTrans.EpiDataViews["QuoteDtl"];
		foreach(DataRow dr in edvQuoteDtl.dataView.Table.AsEnumerable().Where(w=> w.Field<System.Decimal>("DocListPrice") == w.Field<System.Decimal>("OrdBasedPrice")))
		{
			dr.BeginEdit();  
            dr["OverridePriceList"] = true;
			dr["BreakListCode"]=listCode;
			dr["PriceListCode"] = listCode; 
			dr.EndEdit();  
			oTrans.Update();
			this.priceListCmb.ResetText();
			trig = true;
		}

		if (trig)
		{
			oTrans.Update();
            this.priceListCmb.ForceRefreshList();
			
		}
	}

	private void GALBtnSave_Click(object sender, System.EventArgs args)
	{
		// ** Place Event Handling Code Here **
		SaveUD01UD02();

		Int32 quoteNum = (int)this._edvOrderHed.dataView[this._edvOrderHed.Row]["QuoteNum"];
		QuoteHeader_Load(quoteNum.ToString());	
		
	}
   
    private void CustomerDefaultPric(string custID)
    {
        DynamicQueryAdapter dqa = new DynamicQueryAdapter(oTrans);
		dqa.BOConnect();

		
		QueryExecutionDataSet qeds = dqa.GetQueryExecutionParametersByID("PbsCustomerDefaultPriceList");
		qeds.ExecutionParameter.Clear();
		qeds.ExecutionParameter.AddExecutionParameterRow("CustID", custID, "string", false, Guid.NewGuid(), "A");
		dqa.ExecuteByID("PbsCustomerDefaultPriceList", qeds);
	    DataTable results = dqa.QueryResults.Tables["Results"];
        if(results.Rows.Count>0)
        {
          if(!string.IsNullOrEmpty( pbstxtFirstPriceLst.Text) || !string.IsNullOrEmpty( pbsPriceListDescription.Text) )
          {
             if(DialogResult.Yes==MessageBox.Show("It will overwrite the priceListCode \n Are you sure?","Warning",MessageBoxButtons.YesNo))
             {

                edvQuoteHed.CurrentDataRow.BeginEdit();
				edvQuoteHed.CurrentDataRow["pbsQPriceList1_c"] = results.Rows[0]["PriceLst_ListCode"].ToString();
				edvQuoteHed.CurrentDataRow["pbsQPriceList1Desc_c"] =results.Rows[0]["PriceLst_ListDescription"].ToString();
				edvQuoteHed.CurrentDataRow.EndEdit();
                oTrans.Update();
             }
          }
          else
          {
                edvQuoteHed.CurrentDataRow.BeginEdit();
				edvQuoteHed.CurrentDataRow["pbsQPriceList1_c"] = results.Rows[0]["PriceLst_ListCode"].ToString();
				edvQuoteHed.CurrentDataRow["pbsQPriceList1Desc_c"] =results.Rows[0]["PriceLst_ListDescription"].ToString();
				edvQuoteHed.CurrentDataRow.EndEdit();
                oTrans.Update();
           }
        }
        else
        {
           MessageBox.Show("No price list found!!");
        }
        dqa.Dispose();
		
    }
    private void btnDfCustPL_Click(object sender, System.EventArgs args)
	{
		// ** Place Event Handling Code Here **
           if(edvQuoteHed.HasRow)
           {
             string custID = edvQuoteHed.CurrentDataRow["CustomerCustID"].ToString();
             if(!string.IsNullOrEmpty(custID))
             {
               CustomerDefaultPric(custID);
             }
             else
             {
                MessageBox.Show("Customer must be selected to get price list");
             }
           }
	}

    

	private int FindSectHeaders(string qHeader)
	{
		DynamicQueryAdapter dqa = new DynamicQueryAdapter(oTrans);
		dqa.BOConnect();

		
		QueryExecutionDataSet qeds = dqa.GetQueryExecutionParametersByID("GAL_QuoteSectHead");
		qeds.ExecutionParameter.Clear();
		qeds.ExecutionParameter.AddExecutionParameterRow("pQuoteHeadID", qHeader, "string", false, Guid.NewGuid(), "A");
		
		dqa.ExecuteByID("GAL_QuoteSectHead", qeds);
		// Key3 = SectHeadID, Key4 = QuoteHeadID
		DataTable results = dqa.QueryResults.Tables["Results"];
		EpiDataView edvResults = new EpiDataView();
		edvResults.dataView = results.DefaultView;
				
		return (int)edvResults.dataView.Table.Rows.Count;;
	}

    

	private int checkDeleteUD0102()
	{
		// -1 abort, 0 - no lines found, 1, lines found
		UltraGridRow row = this.uGridHdr.ActiveRow;
		string qteHed = row.Cells["UD01_Key3"].Value.ToString();
		string sectHed = row.Cells["UD02_Key3"].Value.ToString();

		int qteHedc = 0;
		int sectHedc =0;
		int checkDel = -1;
		//bool res = false;
		// Find quote dtl row with section/quote header
		for (int i = 0; i < edvQuoteDtl.dataView.Table.Rows.Count -1; i++ )
		{
			
			if(edvQuoteDtl.dataView.Table.Rows[i]["pbsSectionHed_c"].ToString() == sectHed && edvQuoteDtl.dataView.Table.Rows[i]["pbsQuoteHed_c"].ToString() == qteHed)
			{
				sectHedc++;
			} else if (edvQuoteDtl.dataView.Table.Rows[i]["pbsSectionHed_c"].ToString() == "" && edvQuoteDtl.dataView.Table.Rows[i]["pbsQuoteHed_c"].ToString() == qteHed)
			{
				qteHedc++;
			}
		}
		
		if (sectHedc + qteHedc > 0)
		{

			if (System.Windows.Forms.DialogResult.Yes == MessageBox.Show("There are Quote lines with these Quote/Section Headers. Are you sure you want to delete them and clear from the quote lines?","Quote Line Found!",MessageBoxButtons.YesNo))
				checkDel = 1;
			
			return checkDel;
		} else {
			if(System.Windows.Forms.DialogResult.Yes == MessageBox.Show("These Quote/Section Headers are not linked to quote lines. Are you sure you want to delete them?","Quote Line Found!",MessageBoxButtons.YesNo))
				checkDel = 0;
			return checkDel;
		}

		//return false;
	}

	private void clearQuoteHeadLines()
	{
		// -1 abort, 0 - no lines found, 1, lines found
		UltraGridRow row = this.uGridHdr.ActiveRow;
		string qteHed = row.Cells["UD01_Key3"].Value.ToString();
		string sectHed = row.Cells["UD02_Key3"].Value.ToString();

		int qteHedc = 0;
		int sectHedc =0;
		int checkDel = -1;
		//bool res = false;
		// Find quote dtl row with section/quote header
		for (int i = 0; i < edvQuoteDtl.dataView.Table.Rows.Count ; i++ )
		{
			if(qteHed != "" && sectHed != "") 
			{
				if(edvQuoteDtl.dataView.Table.Rows[i]["pbsSectionHed_c"].ToString() == sectHed && edvQuoteDtl.dataView.Table.Rows[i]["pbsQuoteHed_c"].ToString() == qteHed)
				{
					edvQuoteDtl.dataView.Table.Rows[i].BeginEdit();
					edvQuoteDtl.dataView.Table.Rows[i]["pbsSectionHed_c"] = "";
					edvQuoteDtl.dataView.Table.Rows[i]["pbsQuoteHed_c"] = "";
					edvQuoteDtl.dataView.Table.Rows[i]["GALSectionHedDesc_c"] = "";
					edvQuoteDtl.dataView.Table.Rows[i]["GALQuoteHedDesc_c"] = "";
					edvQuoteDtl.dataView.Table.Rows[i].EndEdit();
					sectHedc++;
				} else if (edvQuoteDtl.dataView.Table.Rows[i]["pbsSectionHed_c"].ToString() == "" && edvQuoteDtl.dataView.Table.Rows[i]["pbsQuoteHed_c"].ToString() == qteHed)
				{
					edvQuoteDtl.dataView.Table.Rows[i].BeginEdit();
					edvQuoteDtl.dataView.Table.Rows[i]["pbsQuoteHed_c"] = "";
					edvQuoteDtl.dataView.Table.Rows[i]["GALQuoteHedDesc_c"] = "";
					edvQuoteDtl.dataView.Table.Rows[i].EndEdit();
					qteHedc++;
				}
				
			} else if(qteHed != ""  && sectHed == "")
			{
				if (edvQuoteDtl.dataView.Table.Rows[i]["pbsSectionHed_c"].ToString() == "" && edvQuoteDtl.dataView.Table.Rows[i]["pbsQuoteHed_c"].ToString() == qteHed)
				{
					edvQuoteDtl.dataView.Table.Rows[i].BeginEdit();
					edvQuoteDtl.dataView.Table.Rows[i]["pbsQuoteHed_c"] = "";
					edvQuoteDtl.dataView.Table.Rows[i]["GALQuoteHedDesc_c"] = "";
					edvQuoteDtl.dataView.Table.Rows[i].EndEdit();
					qteHedc++;
				}
			}
		}
		
		if (sectHedc + qteHedc > 0)
		{
			oTrans.Update();
			//oTrans.NotifyAll(EpiTransaction.NotifyType.Initialize, edvQuoteDtl);
			this.edvQuoteDtl.Notify(new EpiNotifyArgs(oTrans,0, EpiTransaction.NotifyType.Initialize));
		}

		//return false;
	}

	private void GALBtnDelete_Click(object sender, System.EventArgs args)
	{
		// ** Place Event Handling Code Here **
		//checkDel  -1 abort, 0 - no lines found, 1, lines found
		bool del = false;
		int checkDel = -1;
		checkDel = checkDeleteUD0102();
		if (checkDel > 0 )
		{
			clearQuoteHeadLines();
			DeleteUD01UD02();
			del = true;
		} else if (checkDel > -1 )
		{
			clearQuoteHeadLines();
			DeleteUD01UD02();
			del = true;
		} else {
			MessageBox.Show("Delete aborted.");
		}
		if (del)
		{
			Int32 quoteNum = (int)this._edvOrderHed.dataView[this._edvOrderHed.Row]["QuoteNum"];
			QuoteHeader_Load(quoteNum.ToString());	
		}
	}

	public void setDropDown()
	{
		// Removed was casusing an issue
		Ice.Lib.Framework.EpiUltraGrid _myGrid = (Ice.Lib.Framework.EpiUltraGrid)csm.GetNativeControlReference("73bddcdc-c164-4d92-804b-7309ce973d36");
		_myGrid.DisplayLayout.Bands[0].Columns["pbsQuoteHed_c"].ValueList = uComboQtHdr;
		_myGrid.DisplayLayout.Bands[0].Columns["pbsQuoteHed_c"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownList;

		_myGrid.DisplayLayout.Bands[0].Columns["pbsSectionHed_c"].ValueList = uComboSecHrd;
		_myGrid.DisplayLayout.Bands[0].Columns["pbsSectionHed_c"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownList;

		uComboSecHrd.ForceRefreshList();
		uComboQtHdr.ForceRefreshList();

	}

	private void CreateRowRuleQuoteHedQuotedEquals_true0()
	{
		// Description: QuotedDisableHeaders
		// **** begin autogenerated code ****
		RuleAction[] ruleActions = new RuleAction[0];
		// Create RowRule and add to the EpiDataView.
		// Dummy Context Object
		object contextObject = null;
		RowRule rrCreateRowRuleQuoteHedQuotedEquals_true0 = new RowRule("QuoteHed.Quoted", RuleCondition.Equals, true, new RowRuleActionDelegate2(this.QuoteHedQuotedEqualstrue_CustomRuleAction), contextObject);
		((EpiDataView)(this.oTrans.EpiDataViews["QuoteHed"])).AddRowRule(rrCreateRowRuleQuoteHedQuotedEquals_true0);
		// **** end autogenerated code ****
	}

	

	private void QuoteHedQuotedEqualstrue_CustomRuleAction(Ice.Lib.ExtendedProps.RowRuleDelegateArgs args)
	{
		// ** RowRuleDelegateArgs Properties: args.Arg1, args.Arg2, args.Context, args.Row
		// ** put custom Rule Action logic here
		uGridHdr.Enabled = false;
		GALBtnSave.Enabled = false;
		GALBtnDelete.Enabled = false;
		btnNew.Enabled = false;
		btnSave.Enabled = false;
        btnDfCustPL.Enabled=false;

	}



	private void CreateRowRuleQuoteHedQuotedEquals_false0()
	{
		// Description: QuoteEnableHeaders
		// **** begin autogenerated code ****
		RuleAction[] ruleActions = new RuleAction[0];
		// Create RowRule and add to the EpiDataView.
		// Dummy Context Object
		object contextObject = null;
		RowRule rrCreateRowRuleQuoteHedQuotedEquals_false0 = new RowRule("QuoteHed.Quoted", RuleCondition.Equals, false, new RowRuleActionDelegate2(this.QuoteHedQuotedEqualsfalse_CustomRuleAction), contextObject);
		((EpiDataView)(this.oTrans.EpiDataViews["QuoteHed"])).AddRowRule(rrCreateRowRuleQuoteHedQuotedEquals_false0);
		// **** end autogenerated code ****
	}

	private void QuoteHedQuotedEqualsfalse_CustomRuleAction(Ice.Lib.ExtendedProps.RowRuleDelegateArgs args)
	{
		// ** RowRuleDelegateArgs Properties: args.Arg1, args.Arg2, args.Context, args.Row
		// ** put custom Rule Action logic here
		uGridHdr.Enabled = true;
		GALBtnSave.Enabled = true;
		GALBtnDelete.Enabled = true;
		btnNew.Enabled = true;
		btnSave.Enabled = true;
        btnDfCustPL.Enabled=true;
	}


	private void CreateRowRuleQuoteDtlPbsPriceOverrideByUser_cEquals_false()
	{
		// Description: EnableDspExpUnitPrice
		// **** begin autogenerated code ****
		//ControlSettings controlSettings1EpiReadOnly = new ControlSettings();
		//controlSettings1EpiReadOnly.SetStyleSetName("EpiReadOnly");
		RuleAction epireadonlyQuoteDtl_RowAction = RuleAction.AddControlSettings(this.oTrans, "QuoteDtl.DocDspExpUnitPrice", SettingStyle.ReadOnly);
		RuleAction[] ruleActions = new RuleAction[] {
				epireadonlyQuoteDtl_RowAction};
		// Create RowRule and add to the EpiDataView.
		RowRule rrCreateRowRuleQuoteDtlPbsPriceOverrideByUser_cEquals_false = new RowRule("QuoteDtl.PbsPriceOverrideByUser_c", RuleCondition.Equals, false, ruleActions);
		((EpiDataView)(this.oTrans.EpiDataViews["QuoteDtl"])).AddRowRule(rrCreateRowRuleQuoteDtlPbsPriceOverrideByUser_cEquals_false);
		// **** end autogenerated code ****
	}
    
    	
 

   

    	private void CreateRowRuleQuoteHedQuotedEquals_true()
	{
		// Description: QuotesQuoted
		// **** begin autogenerated code ****
		//ControlSettings controlSettings1EpiReadOnly = new ControlSettings();
		//controlSettings1EpiReadOnly.SetStyleSetName("EpiReadOnly");
		RuleAction epireadonlyQuoteHed_DateQuoted = RuleAction.AddControlSettings(this.oTrans, "QuoteHed.DateQuoted",SettingStyle.ReadOnly);
		//ControlSettings controlSettings2EpiReadOnly = new ControlSettings();
		//controlSettings2EpiReadOnly.SetStyleSetName("EpiReadOnly");
		RuleAction epireadonlyQuoteHed_ExpirationDate = RuleAction.AddControlSettings(this.oTrans, "QuoteHed.ExpirationDate", SettingStyle.ReadOnly);
		RuleAction[] ruleActions = new RuleAction[] {
				epireadonlyQuoteHed_DateQuoted,
				epireadonlyQuoteHed_ExpirationDate};
		// Create RowRule and add to the EpiDataView.
		RowRule rrCreateRowRuleQuoteHedQuotedEquals_true = new RowRule("QuoteHed.Quoted", RuleCondition.Equals, true, ruleActions);
		((EpiDataView)(this.oTrans.EpiDataViews["QuoteHed"])).AddRowRule(rrCreateRowRuleQuoteHedQuotedEquals_true);
		// **** end autogenerated code ****
	}

	private void SearchOnPriceLstAdapterShowDialog_1()
	{
		// Wizard Generated Search Method
		// You will need to call this method from another method in custom code
		// For example, [Form]_Load or [Button]_Click

		bool recSelected;
		string whereClause = string.Empty;
		System.Data.DataSet dsPriceLstAdapter = Ice.UI.FormFunctions.SearchFunctions.listLookup(this.oTrans, "PriceLstAdapter", out recSelected, true, whereClause);
		if (recSelected)
		{
			System.Data.DataRow adapterRow = dsPriceLstAdapter.Tables[0].Rows[0];

			// Map Search Fields to Application Fields
			EpiDataView edvQuoteHed = ((EpiDataView)(this.oTrans.EpiDataViews["QuoteHed"]));
			System.Data.DataRow edvQuoteHedRow = edvQuoteHed.CurrentDataRow;
			if ((edvQuoteHedRow != null))
			{
				edvQuoteHedRow.BeginEdit();
				edvQuoteHedRow["pbsQPriceList1_c"] = adapterRow["ListCode"];
				edvQuoteHedRow["pbsQPriceList1Desc_c"] = adapterRow["ListDescription"];
				edvQuoteHedRow.EndEdit();
			}
		}
	}

	private void baseToolbarsManager_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs args)
	{
		switch(args.Tool.Key)
		{
			case "CreateDemandContractTool":
				if(edvQuoteHed.dataView.Count < 1)
				{
					return;
				}

				DemandContractForm demandContractForm = new DemandContractForm(this.oTrans);
				demandContractForm.StartPosition = FormStartPosition.CenterScreen;			
				using(this.oTrans.PushDisposableStatusText("Generating demand contract...", true))
				{
					demandContractForm.ShowDialog();
				}
                oTrans.Update();
				break;
		}
	}

	private void actionsMenu_BeforeToolDropdown(object sender, Infragistics.Win.UltraWinToolbars.BeforeToolDropdownEventArgs e)
	{
		if(edvQuoteHed.dataView.Count < 1)
		{
			this.baseToolbarsManager.Tools["CreateDemandContractTool"].SharedProps.Enabled = false;
		}
		else
		{
			this.baseToolbarsManager.Tools["CreateDemandContractTool"].SharedProps.Enabled = true;
		}
	}	
}

#region Popup form for creating demand contract

public class DemandContractForm : EpiBaseForm
{
	private EpiTransaction quoteTransaction;

    public DemandContractForm()
    {
        InitializeComponent();
    }

    public DemandContractForm(EpiTransaction tran)
    {
        InitializeComponent();
		quoteTransaction = tran;
    }
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.dteContractStartDate = new EpiDateTimeEditor();
        this.dteContractEndDate = new EpiDateTimeEditor();
        this.lblContractStartDate = new EpiLabel();
        this.lblContractEndDate = new EpiLabel();
        this.btnCreateDemandContract = new EpiButton();
        this.SuspendLayout();
        // 
        // dteContractStartDate
        // 
        //this.dteContractStartDate.CustomFormat = "dd/MM/yyyy";
        //this.dteContractStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
        this.dteContractStartDate.Location = new System.Drawing.Point(160, 35);
        this.dteContractStartDate.Name = "dteContractStartDate";
        this.dteContractStartDate.Size = new System.Drawing.Size(119, 20);
        this.dteContractStartDate.TabIndex = 0;
        // 
        // dteContractEndDate
        // 
        //this.dteContractEndDate.CustomFormat = "dd/MM/yyyy";
        //this.dteContractEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
        this.dteContractEndDate.Location = new System.Drawing.Point(160, 61);
        this.dteContractEndDate.Name = "dteContractEndDate";
        this.dteContractEndDate.Size = new System.Drawing.Size(119, 20);
        this.dteContractEndDate.TabIndex = 1;
        // 
        // lblContractStartDate
        // 
        this.lblContractStartDate.AutoSize = true;
        this.lblContractStartDate.Location = new System.Drawing.Point(43, 41);
        this.lblContractStartDate.Name = "lblContractStartDate";
        this.lblContractStartDate.Size = new System.Drawing.Size(101, 13);
        this.lblContractStartDate.TabIndex = 2;
        this.lblContractStartDate.Text = "Contract Start Date:";
        // 
        // lblContractEndDate
        // 
        this.lblContractEndDate.AutoSize = true;
        this.lblContractEndDate.Location = new System.Drawing.Point(43, 67);
        this.lblContractEndDate.Name = "lblContractEndDate";
        this.lblContractEndDate.Size = new System.Drawing.Size(98, 13);
        this.lblContractEndDate.TabIndex = 3;
        this.lblContractEndDate.Text = "Contract End Date:";
        // 
        // btnCreateDemandContract
        // 
        this.btnCreateDemandContract.Location = new System.Drawing.Point(78, 106);
        this.btnCreateDemandContract.Name = "btnCreateDemandContract";
        this.btnCreateDemandContract.Size = new System.Drawing.Size(154, 39);
        this.btnCreateDemandContract.TabIndex = 4;
        this.btnCreateDemandContract.Text = "Create Demand Contract";
        //this.btnCreateDemandContract.UseVisualStyleBackColor = true;
        this.btnCreateDemandContract.Click += new System.EventHandler(this.btnCreateDemandContract_Click);
        // 
        // DemandContractForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(320, 157);
        this.Controls.Add(this.btnCreateDemandContract);
        this.Controls.Add(this.lblContractStartDate);
        this.Controls.Add(this.lblContractEndDate);
        this.Controls.Add(this.dteContractStartDate);
        this.Controls.Add(this.dteContractEndDate);
        this.Name = "DemandContractForm";
        this.Text = "Create Demand Contract";
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    private EpiDateTimeEditor dteContractStartDate;
    private EpiDateTimeEditor dteContractEndDate;
    private EpiLabel lblContractStartDate;
    private EpiLabel lblContractEndDate;
    private EpiButton btnCreateDemandContract;

    private void btnCreateDemandContract_Click(object sender, EventArgs e)
    {
		DemandContractAdapter demandAdapter = new DemandContractAdapter(this.quoteTransaction);
		demandAdapter.BOConnect();
		Dictionary<string, string> specialFieldsFromQuoteDtl = new Dictionary<string, string> {{"SellingExpectedUM", "SalesUM"}};
		
		/* Generate Demand Contract Header */
		EpiDataView quoteHedView = (EpiDataView)this.quoteTransaction.EpiDataViews["QuoteHed"];
		int demandContractNum = CreateDemandContractHeader(quoteHedView, ref demandAdapter, Convert.ToDateTime(this.dteContractStartDate.Value), Convert.ToDateTime(this.dteContractEndDate.Value));

		/* Generate Demand Contract Lines */
		EpiDataView quoteDtlView = (EpiDataView)this.quoteTransaction.EpiDataViews["QuoteDtl"];
		CreateDemandContractLines(quoteDtlView, demandContractNum, specialFieldsFromQuoteDtl, ref demandAdapter);	
	    
        //
        quoteHedView.dataView[quoteHedView.Row].BeginEdit();
        quoteHedView.dataView[quoteHedView.Row]["PbsDemandContract_c"]=quoteHedView.dataView[quoteHedView.Row]["QuoteNum"];
        quoteHedView.dataView[quoteHedView.Row]["ReasonType"]="W";
        quoteHedView.dataView[quoteHedView.Row]["ReasonCode"]="WON";
        quoteHedView.dataView[quoteHedView.Row].EndEdit();
		demandAdapter.Dispose();
		EpiMessageBox.Show("Demand contract generated");
  
		this.Close();
    }

	private int CreateDemandContractHeader(EpiDataView view, ref DemandContractAdapter adapter, DateTime? startDate = null, DateTime? endDate = null)
	{
		adapter.GetNewDmdCntHdr();
		Erp.BO.DemandContractDataSet ds = adapter.DemandContractData;
		DataRowView rowView = view.dataView[view.Row];

		ds.DmdCntHdr[0].DemandContract = Convert.ToString(rowView["QuoteNum"]);

		foreach(DataColumn col in view.dataView.Table.Columns)
		{
			if(ds.DmdCntHdr.Columns.Contains(col.ColumnName))
			{
				ds.DmdCntHdr[0][col.ColumnName] = rowView[col.ColumnName];
			}
		}

		if(startDate != null)
		{
			ds.DmdCntHdr[0].StartDate = (DateTime)startDate;
		}

		if(endDate != null)
		{
			ds.DmdCntHdr[0].EndDate = (DateTime)endDate;
		}

		if(startDate != null && endDate != null)
		{
			if(DateTime.Compare(Convert.ToDateTime(startDate), Convert.ToDateTime(endDate)) > 0)
			{
				throw new Exception("Start date cannot be after end date");
			}
		}

		adapter.Update();

		int result = ds.DmdCntHdr[0].DemandContractNum;
		return result;
	}

	private void CreateDemandContractLines(EpiDataView view, int contractNum, Dictionary<string, string> specialFields, ref DemandContractAdapter adapter)
	{
		Erp.BO.DemandContractDataSet ds = adapter.DemandContractData;
		foreach(DataRowView rowView in view.dataView)
		{
			adapter.GetNewDmdCntDtl(contractNum);
			ds.DmdCntDtl[ds.DmdCntDtl.Rows.Count - 1].MinCallOffQty = 1;
            ds.DmdCntDtl[ds.DmdCntDtl.Rows.Count - 1].SellingTotalContractQty=(decimal)rowView["OrderQty"];
            ds.DmdCntDtl[ds.DmdCntDtl.Rows.Count - 1].UsePriceList=false;
            ds.DmdCntDtl[ds.DmdCntDtl.Rows.Count - 1].DocUnitPrice=((decimal)rowView["BasePotential"]/(decimal)rowView["OrderQty"]);
			foreach(DataColumn col in view.dataView.Table.Columns)
			{
				if(ds.DmdCntDtl.Columns.Contains(col.ColumnName))
				{
					if(!string.Equals(col.ColumnName, "DemandContractNum"))
					{
						ds.DmdCntDtl[ds.DmdCntDtl.Rows.Count - 1][col.ColumnName] = rowView[col.ColumnName];
					}
				}

//				if(specialFields.ContainsKey(col.ColumnName))
//				{
//					ds.DmdCntDtl[ds.DmdCntDtl.Rows.Count - 1][specialFields[col.ColumnName]] = rowView[col.ColumnName]; QuoteDtl.SellingExpectedUM
//				}
			}
            System.Decimal ipReplaceValue =((System.Decimal)rowView["BasePotential"]/(decimal)rowView["OrderQty"]);
			string iPartNum = Convert.ToString(rowView["PartNum"]);
			string rowType = string.Empty;
			string serialWarning, questionString;
			bool multipleMatch;
			Guid sysRowID = ds.DmdCntDtl[ds.DmdCntDtl.Rows.Count-1].SysRowID;
			adapter.ChangeDmdCntDtlPartNum(ref iPartNum, sysRowID, rowType, out serialWarning, out questionString, out multipleMatch);
            adapter.ChangeDmdCntDtlProject(rowView["ProjectID"].ToString());
            adapter.ChangeCurrValue(ipReplaceValue,"D");
            
			adapter.Update();
		}
		

	}


}

#endregion Popup form for creating demand contract
















