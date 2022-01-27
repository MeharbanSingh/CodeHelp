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
