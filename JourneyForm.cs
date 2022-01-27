// **************************************************
// Custom code for UD07Form
// Created: 29/06/2020 12:23:03 PM
// **************************************************
using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
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
using System.Linq;
using System.Collections.Generic;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using System.Globalization;
using System.Reflection;
using Infragistics.Win.UltraWinToolbars;
using Erp.Proxy.Rpt;

public class Script
{
	// ** Wizard Insert Location - Do Not Remove 'Begin/End Wizard Added Module Level Variables' Comments! **
	// Begin Wizard Added Module Level Variables **
	

	string[] allocSOList = {"Selected","SEQ","Time","Order","CustID","Name","Address","City","Lines","Remaining","Allocated","Fulfillable","InPicking","Picked","Packed", "ShipVia","ShipDate","Status","PackNum", "JourneyNum", "UseOTS", "ShipToNum", "ShipToCustNum", "FullyFulfillable", "OTSHeader","CreditHold"};
	string[] unAllocSOList = {"Selected","SEQ","Time","Order","CustID","Name","Address","City","Lines","UnAlloc","Fulfillable","ShipVia", "ShipDate","Status", "FullyFulfillable", "UseOTS", "ShipToNum", "ShipToCustNum","CreditHold"};
	string[] allocTOList = {"Selected","SEQ","TOOrder","Transfer Order","From Site","To Site","Name","City","Stage","Lines","Allocated","InPicking","Picked","Packed","ShipDate","Status","PackNum", "JourneyNum"};
	string[] unAllocTOList = {"Selected","SEQ","TOOrder","Transfer Order","From Site","To Site","Name","City","Stage","Lines","UnAlloc","Fulfillable","ShipDate","Status", "FullyFulfillable"};
	string[] soDtlList = {"Selected","OrderNum","LRel","Part","Description","Qty","UOM", "Avail", "PO", "Due Date", "ShipToName", "Address1", "UseOTS","PackingSlip", "Calculated_ReservedQtyFlag", "Calculated_PickingQtyFlag", "Calculated_PackingQtyFlag"};
	string[] toDtlList = {"Selected","TO","Line","Part","Description","Qty","UOM", "Avail"};
    string[] btnList  = {"btnSubmitForPicking","btnPrinkPickSlip","btnPrintManifest","btnShipJourney","btnAllocSelectAll","btnAllocUnSelectAll","btnAllocUnAllocate","btnUnAllocApplyFilters","btnUnAllocSelectAll","btnUnAllocUnSelectAll","btnUnAllocAllocate","btnRetrieve","btnPartShip","btnUnAllocateLine"};
	EpiTreeView tv;
	List<EpiButton> fromBtnList;	

	EpiDataView edvAllocSO, edvSODtl, edvUnAllocSO, edvAllocTO, edvAllocUnAllocTO, edvTODtl, edvButtons;
	DynamicQueryAdapter dynamicQueryAdapter;
	QueryExecutionDataSet unAllocSOQEDS, allocSOQEDS, allocSODtlQEDS, allocTOQEDS, allocTODtlQEDS;
	DataSet unAllocSODS, allocSODS, allocSODtlDS, unAllocTODS, allocTODS, allocTODtlDS;
	private EpiBaseAdapter oTrans_adapter;
	private EpiDataView edvUD07;
    private ButtonTool ReprintManifest;
	private Ice.Core.Session session;
	private DataView UD07_DataView;
    private Ice.Proxy.Rpt.BAQReportImpl baqRptSvc;
    private Ice.Proxy.BO.DynamicReportImpl dynamicRptSvc;
	// End Wizard Added Module Level Variables **

	// Add Custom Module Level Variables Here **

	public void InitializeCustomCode()
	{
		// ** Wizard Insert Location - Do not delete 'Begin/End Wizard Added Variable Initialization' lines **
		// Begin Wizard Added Variable Initialization
		
			
		CreateEpiDataView(edvButtons, "buttons", btnList);
		CreateEpiDataView(edvAllocSO, "edvAllocSO", allocSOList);
		CreateEpiDataView(edvUnAllocSO, "edvUnAllocSO", unAllocSOList);
		//CreateEpiDataView(edvAllocTO, "edvAllocTO", allocTOList);
		//CreateEpiDataView(edvAllocUnAllocTO, "edvAllocUnAllocTO", unAllocTOList);
		CreateEpiDataView(edvSODtl, "edvSODtl", soDtlList);
		//CreateEpiDataView(edvTODtl, "edvTODtl", toDtlList);

		edvUD07 = (EpiDataView)oTrans.EpiDataViews["UD07"];
		//Connect Dynamic Query Adatper
        this.baqRptSvc = Ice.Lib.Framework.WCFServiceSupport.CreateImpl<Ice.Proxy.Rpt.BAQReportImpl>(oTrans.CoreSession, Ice.Proxy.Rpt.BAQReportImpl.UriPath);
        this.dynamicRptSvc = Ice.Lib.Framework.WCFServiceSupport.CreateImpl<Ice.Proxy.BO.DynamicReportImpl>(oTrans.CoreSession, Ice.Proxy.BO.DynamicReportImpl.UriPath);

		this.oTrans_adapter = ((EpiBaseAdapter)(this.csm.TransAdaptersHT["oTrans_adapter"]));
		this.oTrans_adapter.AfterAdapterMethod += new AfterAdapterMethod(this.oTrans_adapter_AfterAdapterMethod);
		this.baseToolbarsManager.ToolClick += new Infragistics.Win.UltraWinToolbars.ToolClickEventHandler(this.baseToolbarsManager_ToolClick);
		this.edvUD07 = ((EpiDataView)(this.oTrans.EpiDataViews["UD07"]));
		this.edvUD07.EpiViewNotification += new EpiViewNotification(this.edvUD07_EpiViewNotification);

		// Nav Tree Text
		
		tv = (Ice.Lib.Framework.EpiTreeView) csm.GetNativeControlReference("46c8ee42-a6f1-468b-95ba-6dfc13223ee2");
		tv.RootNodeText = "Journey";
		this.oTrans_adapter.BeforeAdapterMethod += new BeforeAdapterMethod(this.oTrans_adapter_BeforeAdapterMethod);
		this.UD07_DataView = this.UD07_Row.dataView;
		this.UD07_DataView.ListChanged += new ListChangedEventHandler(this.UD07_DataView_ListChanged);
		// End Wizard Added Variable Initialization

		// Begin Wizard Added Custom Method Calls

		this.btnApplyFilters.Click += new System.EventHandler(this.btnApplyFilters_Click);
		this.btnAllocate.Click += new System.EventHandler(this.btnAllocate_Click);
		this.btnUnAllocateSO.Click += new System.EventHandler(this.btnUnAllocateSO_Click);
		this.ugAllocatedSO.AfterRowActivate += new System.EventHandler(this.ugAllocatedSO_AfterRowActivate);
		this.btnPartShip.Click += new System.EventHandler(this.btnPartShip_Click);
		this.btnUnAllocateLine.Click += new System.EventHandler(this.btnUnAllocateLine_Click);
		this.btnPrintPickSlip.Click += new System.EventHandler(this.btnPrintPickSlip_Click);
		this.btnShipJourney.Click += new System.EventHandler(this.btnShipJourney_Click);
		this.btnPrintManifest.Click += new System.EventHandler(this.btnPrintManifest_Click);
		this.btnTOAllocate.Click += new System.EventHandler(this.btnTOAllocate_Click);
		this.btnTOUnAllocate.Click += new System.EventHandler(this.btnTOUnAllocate_Click);
		this.btnTOApplyFilter.Click += new System.EventHandler(this.btnTOApplyFilter_Click);
		this.btnUnAllocTOL.Click += new System.EventHandler(this.btnUnAllocTOL_Click);
		this.ugTOAlloc.AfterRowActivate += new System.EventHandler(this.ugTOAlloc_AfterRowActivate);
		this.udcRM.BeforeDropDown += new System.ComponentModel.CancelEventHandler(this.udcRM_BeforeDropDown);
		this.cbFulfillable.CheckedChanged += new System.EventHandler(this.cbFulfillable_CheckedChanged);
		this.cbTOFulfillable.CheckedChanged += new System.EventHandler(this.cbTOFulfillable_CheckedChanged);
		CreateRowRuleUD07CheckBox02Equals_true();;
		CreateRowRuleUD07CheckBox02Equals_true0();
        CreateRowRuleUD07CheckBox02Equals_false0();
        CreateRowRuleUD07Number20Equals_zero();
		SetExtendedProperties();
		this.cbShowAll.CheckedChanged += new System.EventHandler(this.cbShowAll_CheckedChanged);
		CreateRowRuleedvUnAllocSOFulfillableEquals_true();;
		CreateRowRuleedvAllocSOSelectedEquals_true();;
		this.btnRetrieveAllocSOLD.Click += new System.EventHandler(this.btnRetrieveAllocSOLD_Click);
		this.btnSelectAllAlocSO.Click += new System.EventHandler(this.btnSelectAllAlocSO_Click);
		this.btnUnSelectAllocSO.Click += new System.EventHandler(this.btnUnSelectAllocSO_Click);
		this.btnSelectUnAllocSO.Click += new System.EventHandler(this.btnSelectUnAllocSO_Click);
		this.btnUnSelectUnAllocSO.Click += new System.EventHandler(this.btnUnSelectUnAllocSO_Click);
		this.btnSubmitForPicking.Click += new System.EventHandler(this.btnSubmitForPicking_Click);
		// End Wizard Added Custom Method Calls
	}

	public void DestroyCustomCode()
	{
		// ** Wizard Insert Location - Do not delete 'Begin/End Wizard Added Object Disposal' lines **
		// Begin Wizard Added Object Disposal

		this.btnApplyFilters.Click -= new System.EventHandler(this.btnApplyFilters_Click);
		this.btnAllocate.Click -= new System.EventHandler(this.btnAllocate_Click);
		this.btnUnAllocateSO.Click -= new System.EventHandler(this.btnUnAllocateSO_Click);
		this.oTrans_adapter.AfterAdapterMethod -= new AfterAdapterMethod(this.oTrans_adapter_AfterAdapterMethod);
		this.oTrans_adapter = null;
		this.ugAllocatedSO.AfterRowActivate -= new System.EventHandler(this.ugAllocatedSO_AfterRowActivate);
		this.btnPartShip.Click -= new System.EventHandler(this.btnPartShip_Click);
		this.btnUnAllocateLine.Click -= new System.EventHandler(this.btnUnAllocateLine_Click);
		this.btnPrintPickSlip.Click -= new System.EventHandler(this.btnPrintPickSlip_Click);
		this.btnShipJourney.Click -= new System.EventHandler(this.btnShipJourney_Click);
		this.btnPrintManifest.Click -= new System.EventHandler(this.btnPrintManifest_Click);
		this.baseToolbarsManager.ToolClick -= new Infragistics.Win.UltraWinToolbars.ToolClickEventHandler(this.baseToolbarsManager_ToolClick);
		this.edvUD07.EpiViewNotification -= new EpiViewNotification(this.edvUD07_EpiViewNotification);
		this.edvUD07 = null;
		this.btnTOAllocate.Click -= new System.EventHandler(this.btnTOAllocate_Click);
		this.btnTOUnAllocate.Click -= new System.EventHandler(this.btnTOUnAllocate_Click);
		this.btnTOApplyFilter.Click -= new System.EventHandler(this.btnTOApplyFilter_Click);
		this.btnUnAllocTOL.Click -= new System.EventHandler(this.btnUnAllocTOL_Click);
		this.ugTOAlloc.AfterRowActivate -= new System.EventHandler(this.ugTOAlloc_AfterRowActivate);
		this.udcRM.BeforeDropDown -= new System.ComponentModel.CancelEventHandler(this.udcRM_BeforeDropDown);
		this.oTrans_adapter.BeforeAdapterMethod -= new BeforeAdapterMethod(this.oTrans_adapter_BeforeAdapterMethod);
		this.cbFulfillable.CheckedChanged -= new System.EventHandler(this.cbFulfillable_CheckedChanged);
		this.cbTOFulfillable.CheckedChanged -= new System.EventHandler(this.cbTOFulfillable_CheckedChanged);
		this.cbShowAll.CheckedChanged -= new System.EventHandler(this.cbShowAll_CheckedChanged);
		this.UD07_DataView.ListChanged -= new ListChangedEventHandler(this.UD07_DataView_ListChanged);
		this.UD07_DataView = null;
        this.baqRptSvc=null;
        this.dynamicRptSvc=null;
		this.btnRetrieveAllocSOLD.Click -= new System.EventHandler(this.btnRetrieveAllocSOLD_Click);
		this.btnSelectAllAlocSO.Click -= new System.EventHandler(this.btnSelectAllAlocSO_Click);
		this.btnUnSelectAllocSO.Click -= new System.EventHandler(this.btnUnSelectAllocSO_Click);
		this.btnSelectUnAllocSO.Click -= new System.EventHandler(this.btnSelectUnAllocSO_Click);
		this.btnUnSelectUnAllocSO.Click -= new System.EventHandler(this.btnUnSelectUnAllocSO_Click);
		this.btnSubmitForPicking.Click -= new System.EventHandler(this.btnSubmitForPicking_Click);
		// End Wizard Added Object Disposal
		
		// Begin Custom Code Disposal

		// End Custom Code Disposal
	}

	#region Common Functions

	public void CreateEpiDataView(EpiDataView edv, string edvName, string[] colList)
	{
		edv = new EpiDataView();
		edv.dataView = CreateDataTable(colList).DefaultView;
		oTrans.Add(edvName, edv);
	}

	public DataTable CreateDataTable(string[] cols)
	{
		DataTable dt = new DataTable();
		foreach(var col in cols)
		{
			if(col.Equals("ShipDate"))
				dt.Columns.Add(CreateColumn(col, "System.DateTime"));
			else if(col.Equals("SEQ") || col.Equals("Order") || col.Equals("Lines") || col.Equals("Remaining") || col.Equals("Allocated") 
					|| col.Equals("InPicking") || col.Equals("Picked") || col.Equals("Packed") || col.Equals("Fulfillable") || col.Equals("Avail") 
					|| col.Equals("PackNum") || col.Equals("Delivery Time") || col.Equals("Qty"))
				dt.Columns.Add(CreateColumn(col, "System.Int32"));
			else if(col.Equals("Selected") || col.Equals("FullyFulfillable") || col.Equals("OTSHeader") || col.Equals("Calculated_ReservedQtyFlag") || col.Equals("Calculated_PickingQtyFlag") || col.Equals("Calculated_PackingQtyFlag") || col.Equals("Customer_CreditHold"))
				dt.Columns.Add(CreateColumn(col, "System.Boolean"));
			else
				dt.Columns.Add(CreateColumn(col, "System.String"));	
		}
		DataColumn dc = new DataColumn("SysRowID", typeof(Guid));
		dc.DefaultValue = Guid.NewGuid();
		dt.Columns.Add(dc);
		
		return dt;
	}
	
	public DataColumn CreateColumn(string colName, string type)
	{
		DataColumn dc = new DataColumn(colName, System.Type.GetType(type));
		return dc;
	}

	public void ConncetDQAdapter()
	{
		dynamicQueryAdapter = new DynamicQueryAdapter(this.oTrans);
    	dynamicQueryAdapter.BOConnect();
	}

	public void DisposeDQAdapter()
	{
		dynamicQueryAdapter.Dispose();
	}

	public DataSet RunUBAQ(string BAQName, QueryExecutionDataSet dqds, DateTime? fromDate = null, DateTime? toDate = null, string orderNum = "", string city = "", 
						   string custID = "", int journey = 0, string orderType = "", int shipTime = 0, int toShipTime = 0, string shipToName = "", string address = "" , 
						   bool useOTS = false, DateTime? shipDate = null, int shipbyTime = 99999)
	{
		ConncetDQAdapter();
		bool more;
		dqds = new QueryExecutionDataSet();
		if(fromDate != DateTime.MinValue)
		{
			DateTime startDate = Convert.ToDateTime(fromDate);
			dqds.Tables["ExecutionParameter"].Rows.Add("FromDate", startDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture), "Date", false, null, "A");
		}
		if(toDate != DateTime.MinValue)
		{
			DateTime endDate = Convert.ToDateTime(toDate);
			dqds.Tables["ExecutionParameter"].Rows.Add("toDate", endDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture), "Date", false, null, "A");
		}
		if(string.IsNullOrWhiteSpace(orderType))
		{
			if(Convert.ToInt32(orderNum) != 0)
				dqds.Tables["ExecutionParameter"].Rows.Add("OrderNum", orderNum, "int", false, null, "A");
		}
		else
		{
			if(!string.IsNullOrWhiteSpace(orderNum) && (Convert.ToInt32(orderNum) != 0))
			{
				dqds.Tables["ExecutionParameter"].Rows.Add("OrderNum", orderNum, "nvarchar", false, null, "A");
			}
		}
		if(!string.IsNullOrWhiteSpace(city))
			dqds.Tables["ExecutionParameter"].Rows.Add("City", "%"+city+"%", "nvarchar", false, null, "A");
		if(!string.IsNullOrWhiteSpace(custID))
			dqds.Tables["ExecutionParameter"].Rows.Add("CustID", custID, "nvarchar", false, null, "A");
		if(journey != 0)
			dqds.Tables["ExecutionParameter"].Rows.Add("JourneyNum", journey, "int", false, null, "A");
		if(shipTime != 0)
		{
			dqds.Tables["ExecutionParameter"].Rows.Add("ShipTime", shipTime, "int", false, null, "A");
		}
		if(toShipTime != 0)
		{
			dqds.Tables["ExecutionParameter"].Rows.Add("ToShipTime", toShipTime, "int", false, null, "A");
		}
		if(shipbyTime != 99999)
		{
			dqds.Tables["ExecutionParameter"].Rows.Add("ShipByTime", shipbyTime, "int", false, null, "A");
		}
		/*Filter for SODtl Grid*/
		if(!string.IsNullOrWhiteSpace(shipToName) && !string.IsNullOrWhiteSpace(address))
		{
			dqds.Tables["ExecutionParameter"].Rows.Add("UseOTS", useOTS, "bit", false, null, "A");
			if(shipDate != DateTime.MinValue)
			{
				DateTime shipmentDate = Convert.ToDateTime(shipDate);
				dqds.Tables["ExecutionParameter"].Rows.Add("ShipDate", shipmentDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture), "Date", false, null, "A");
			}
			if(useOTS)
			{
				dqds.Tables["ExecutionParameter"].Rows.Add("OTSName", shipToName, "nvarchar", false, null, "A");
				dqds.Tables["ExecutionParameter"].Rows.Add("OTSAddress", address, "nvarchar", false, null, "A");
			}else
			{
				dqds.Tables["ExecutionParameter"].Rows.Add("ShipToName", shipToName, "nvarchar", false, null, "A");
				dqds.Tables["ExecutionParameter"].Rows.Add("Address", address, "nvarchar", false, null, "A");
			}
		}
		
		dynamicQueryAdapter.GetList(BAQName, dqds, 0, 0, out more);
		DataSet resultDS = dynamicQueryAdapter.QueryResults;
		DisposeDQAdapter();
		return resultDS;

	}
	
	public void ClearEpiDataView(EpiDataView edv)
	{
		if(edv != null)
			edv.dataView.Table.Clear();
	}

	public void ToggleFormBtnState(EpiButton btn, bool val)
	{
		btn.Enabled = val;
	}

	public void ChangeCellAppearance(List<UltraGridRow> rows, int index)
	{
		foreach(var row in rows)
		{
			row.Cells[index].Appearance.BackColor = System.Drawing.Color.LightGreen;
		}
	}
	
	public void ToggleRowVisibility(List<UltraGridRow> rows, bool val)
	{
		foreach(var row in rows)
		{
			row.Hidden = val;
		}
	}

	private void ChangeGridRowState(List<UltraGridRow> collection, bool val)
	{
		foreach(UltraGridRow row in collection)
		{
			row.Hidden = val;
		}
	}

	private string ConvertToTime(int value)
	{
		TimeSpan result = TimeSpan.FromSeconds(value);
        return result.ToString();
		//DateTime time = DateTime.Parse(result.ToString("hh':'mm"));
		//return time.ToString("hh':'mm tt");
	}
	
	private int ConvertTimeToInt(string value)
	{
		if(!string.IsNullOrWhiteSpace(value))
		{
			DateTime parser = DateTime.Parse(value);
			string time24h = parser.ToString("HH:mm");
			return Convert.ToInt32(TimeSpan.Parse(time24h).TotalSeconds);
		}
		return 0;
	}
	
	#endregion Common Functions

	#region UnAllocSO Grid functions
	public void btnApplyFilters_Click(object sender, System.EventArgs args)
	{
		// ** Place Event Handling Code Here **
		ApplyFiltersSO();
	}

	public void ApplyFiltersSO()
	{
		DateTime? fromDate = Convert.ToDateTime(this.dteFD.Value), toDate = Convert.ToDateTime(this.dteTD.Value);
		string orderNum = (this.neOrder.Value.ToString())==string.Empty ? "0":this.neOrder.Value.ToString();//this.neOrder.Value != DBNull.Value? Convert.ToInt32(this.neOrder.Value): 0;
		string city = Convert.ToString(this.txtCity.Value), custID = Convert.ToString(this.txtCustID.Value);
		int shipTime = Convert.ToInt32(this.cDeliveryTime.Value);
		int toShipTime = Convert.ToInt32(this.cToDT.Value);
		unAllocSODS = RunUBAQ("pbsUnAllocatedSO", unAllocSOQEDS, fromDate, toDate, orderNum, city, custID, 0 ,"",shipTime,toShipTime);
		LoadUnAllocSO(unAllocSODS.Tables["Results"]);
		if(unAllocSODS.Tables["Results"].Rows.Count > 0)
		{
			List<UltraGridRow> rows = this.ugUnAllocatedSO.Rows.Where(x => Convert.ToBoolean(x.Cells["FullyFulfillable"].Value) == true).ToList();
			ChangeCellAppearance(rows, 10);
			if(this.cbFulfillable.Checked)
			{
				List<UltraGridRow> collection = this.ugUnAllocatedSO.Rows.AsEnumerable().Where(x => Convert.ToBoolean(x.Cells["FullyFulfillable"].Value) != true).ToList();
				ChangeGridRowState(collection, true);
			}
		}
	}
	
	public void LoadUnAllocSO(DataTable dt)
	{
		this.edvUnAllocSO = (EpiDataView)(oTrans.EpiDataViews["edvUnAllocSO"]);
		ClearEpiDataView(this.edvUnAllocSO);
		foreach(DataRow row in dt.Rows)
		{
			var newRow = this.edvUnAllocSO.dataView.AddNew();
			newRow.BeginEdit();
			newRow["Selected"] = row["Calculated_Selected"];
			newRow["SEQ"] = row["Calculated_SEQ"];
			newRow["Order"] = row["OrderHed_OrderNum"];
			newRow["CustID"] = row["Customer_CustID"];
			newRow["Name"] = row["Calculated_ShipToName"];
			newRow["Address"] = row["Calculated_ShipToAddress"];
			newRow["City"] = row["Calculated_ShipToCity"];
			newRow["Lines"] = row["Calculated_TotalLines"];
			newRow["UnAlloc"] = row["Calculated_UnAllocLines"];
			newRow["Fulfillable"] = row["Calculated_PACount"];
			newRow["ShipDate"] = row["OrderRel2_ReqDate"];
			newRow["FullyFulfillable"] = row["Calculated_FullyFulfillable"];
			newRow["UseOTS"] = row["OrderRel2_UseOTS"];
			newRow["ShipToCustNum"] = row["OrderRel2_ShipToCustNum"];
			newRow["ShipToNum"] = row["OrderRel2_ShipToNum"];
			newRow["ShipVia"] = row["ShipVia_Description"];
			//Convert Int to Time
			newRow["Time"] = ConvertToTime(Convert.ToInt32(row["OrderRel2_ShipbyTime"]));
            newRow["CreditHold"] = row["Customer_CreditHold"]; 
			newRow.EndEdit();
		}
	}

	private void btnAllocate_Click(object sender, System.EventArgs args)
	{
		// ** Place Event Handling Code Here **
		if(this.edvUnAllocSO.dataView.Table.Rows.Count > 0)
		{
			EnumerableRowCollection collection = this.edvUnAllocSO.dataView.Table.AsEnumerable().Where(x => Convert.ToBoolean(x["Selected"]) == true);
			AllocateSO(collection);
			
			// Refersh UnAllocSOGrid // Need to review this part as it'll create performance issues
			ApplyFiltersSO();
			//Refresh AllocSOGrid
			int journyNum = Convert.ToInt32(edvUD07.dataView[edvUD07.Row]["Key1"]);
			LoadAllocSO(journyNum);
		}
	}

	public void AllocateSO(EnumerableRowCollection collection)
	{	
		unAllocSODS.Clear();
		int journyNum = Convert.ToInt32(edvUD07.dataView[edvUD07.Row]["Key1"]);
		foreach(DataRow row in collection)
		{
			DataRow newRow = unAllocSODS.Tables["Results"].NewRow();
			newRow["OrderRel2_PbsJourneyNum_c"] =  journyNum;
			newRow["OrderHed_OrderNum"] = row["Order"];
			newRow["OrderRel2_ReqDate"] = row["ShipDate"];
			newRow["Calculated_ShipToName"] = row["Name"];
			newRow["Calculated_ShipToAddress"] = row["Address"];
			newRow["OrderRel2_UseOTS"] = row["UseOTS"];
			newRow["OrderRel2_ShipToCustNum"] = row["ShipToCustNum"];
			newRow["OrderRel2_ShipToNum"] = row["ShipToNum"];
			newRow["OrderRel2_ShipbyTime"] = ConvertTimeToInt(row["Time"].ToString());
			newRow["RowMod"] = "A";
		    newRow["SysRowID"] = Guid.NewGuid();
		    newRow["RowIdent"] = newRow["SysRowID"].ToString();
			unAllocSODS.Tables["Results"].Rows.Add(newRow);
		}
		ConncetDQAdapter();
		dynamicQueryAdapter.Update("pbsUnAllocatedSO", unAllocSODS);
		DisposeDQAdapter();
	}

	private void cbFulfillable_CheckedChanged(object sender, System.EventArgs args)
	{
		List<UltraGridRow> collection = this.ugUnAllocatedSO.Rows.AsEnumerable().Where(x => Convert.ToBoolean(x.Cells["FullyFulfillable"].Value) != true).ToList();	
		if(((EpiCheckBox)sender).Checked)
			ChangeGridRowState(collection, true);
		else
			ChangeGridRowState(collection, false);
	}
	#endregion UnAllocSO Grid Functions

	#region AllocSO
	private void btnUnAllocateSO_Click(object sender, System.EventArgs args)
	{
		// ** Place Event Handling Code Here **
		if(this.edvAllocSO.dataView.Table.Rows.Count > 0)
		{
			EnumerableRowCollection collection = this.edvAllocSO.dataView.Table.AsEnumerable().Where(x => Convert.ToBoolean(x["Selected"]) == true);
			UnAllocateSO(collection);
			int journyNum = Convert.ToInt32(edvUD07.dataView[edvUD07.Row]["Key1"]);
			LoadAllocSO(journyNum);
		}
	}

	public void LoadAllocSO(int journyNum)
	{
		allocSODS = RunUBAQ("pbsAllocSOExternal", allocSOQEDS, DateTime.MinValue, DateTime.MinValue, "0", "", "", journyNum);
		this.edvAllocSO = (EpiDataView)(oTrans.EpiDataViews["edvAllocSO"]);
		ClearEpiDataView(this.edvAllocSO);
		if(allocSODS.Tables["Results"].Rows.Count > 0)
		{
			foreach(DataRow row in allocSODS.Tables["Results"].Rows)
			{
				var newRow = this.edvAllocSO.dataView.AddNew();
				newRow.BeginEdit();
				newRow["Selected"] = row["PbsAllocSO_Calculated_Selected"];
				newRow["SEQ"] = row["PbsAllocSO_Calculated_SEQ"];
				newRow["Order"] = row["PbsAllocSO_OrderHed_OrderNum"];
				newRow["CustID"] = row["PbsAllocSO_Customer_CustID"];
				newRow["Name"] = row["PbsAllocSO_Calculated_ShipToName"];
				newRow["Address"] = row["PbsAllocSO_Calculated_ShipToAddress"];
				newRow["City"] = row["PbsAllocSO_Calculated_ShipToCity"];
				newRow["Lines"] = row["PbsAllocSO_Calculated_TotalLines"];
				newRow["Allocated"] = row["PbsAllocSO_Calculated_AllocatedLines"];
				newRow["Fulfillable"] = row["PbsAllocSO_Calculated_FulfillableSOLR"];
				newRow["InPicking"] = row["PbsAllocSO_Calculated_InPicking"];
				newRow["Picked"] = row["PbsAllocSO_Calculated_Picked"];
				newRow["Packed"] = row["PbsAllocSO_Calculated_Staged"];
				newRow["ShipDate"] = row["PbsAllocSO_OrderRel4_ReqDate"];
				newRow["PackNum"] = row["PbsAllocSO_ShipDtl_PackNum"];
				newRow["JourneyNum"] = row["PbsAllocSO_OrderRel4_PbsJourneyNum_c"];
				newRow["Remaining"] = row["PbsAllocSO_Calculated_Remaining"];
				newRow["Status"] = row["PbsAllocSO_ShipHead_ShipStatus"];
				newRow["Time"] = ConvertToTime(Convert.ToInt32(row["PbsAllocSO_OrderRel4_ShipbyTime"]));
				newRow["UseOTS"] = row["PbsAllocSO_OrderRel4_UseOTS"];
				newRow["ShipToCustNum"] = row["PbsAllocSO_OrderRel4_ShipToCustNum"];
				newRow["ShipToNum"] = row["PbsAllocSO_OrderRel4_ShipToNum"];
				newRow["FullyFulfillable"] = row["PbsAllocSO_Calculated_FullyFulfillable"];
				newRow["OTSHeader"] = row["PbsAllocSO_Calculated_OTSHeader"];
				newRow["ShipVia"] = row["PbsAllocSO_ShipVia_Description"];
                newRow["CreditHold"]=row["PbsAllocSO_Customer_CreditHold"];
				newRow.EndEdit();
			}
			List<UltraGridRow> rows = this.ugAllocatedSO.Rows.Where(x => Convert.ToBoolean(x.Cells["FullyFulfillable"].Value) == true).ToList();
			ChangeCellAppearance(rows, 11);
		}
		else
		{
			ClearEpiDataView(this.edvSODtl);
		}
	}	

	public void UnAllocateSO(EnumerableRowCollection collection)
	{
		allocSODS.Clear();
		foreach(DataRow row in collection)
		{
			DataRow newRow = allocSODS.Tables["Results"].NewRow();
			newRow["PbsAllocSO_OrderRel4_PbsJourneyNum_c"] =  row["JourneyNum"];
			newRow["PbsAllocSO_OrderHed_OrderNum"] = row["Order"];
			newRow["PbsAllocSO_OrderRel4_ReqDate"] = row["ShipDate"];
			newRow["PbsAllocSO_Calculated_ShipToName"] = row["Name"];
			newRow["PbsAllocSO_Calculated_ShipToAddress"] = row["Address"];
			newRow["PbsAllocSO_OrderRel4_UseOTS"] = row["UseOTS"];
			newRow["PbsAllocSO_OrderRel4_ShipToCustNum"] = row["ShipToCustNum"];
			newRow["PbsAllocSO_OrderRel4_ShipToNum"] = row["ShipToNum"];
			newRow["PbsAllocSO_OrderRel4_ShipbyTime"] = ConvertTimeToInt(row["Time"].ToString());
			newRow["RowMod"] = "A";
		    newRow["SysRowID"] = Guid.NewGuid();
		    newRow["RowIdent"] = newRow["SysRowID"].ToString();
			allocSODS.Tables["Results"].Rows.Add(newRow);
		}
		ConncetDQAdapter();
		dynamicQueryAdapter.Update("pbsAllocSOExternal", allocSODS);
		DisposeDQAdapter();
		
	}
	
	private void ugAllocatedSO_AfterRowActivate(object sender, System.EventArgs args)
	{
        ClearEpiDataView(edvSODtl);
		// ** Place Event Handling Code Here **\	
		/*var row = ugAllocatedSO.ActiveRow;
		string orderNum = row.Cells["Order"].Value.ToString(), shipToName = row.Cells["Name"].Value.ToString(), address = row.Cells["Address"].Value.ToString();
		bool useOTS = Convert.ToBoolean(row.Cells["UseOTS"].Value);
		DateTime shipDate = Convert.ToDateTime(row.Cells["ShipDate"].Value);
		int shipbyTime = ConvertTimeToInt(row.Cells["Time"].Value.ToString()),JourneyNum= Convert.ToInt32(row.Cells["JourneyNum"].Value.ToString());
		LoadAllocSODtl(orderNum, shipToName, address, useOTS, shipDate, shipbyTime,JourneyNum);*/
	}

	#endregion AllocSO

	#region Form Events
	private void oTrans_adapter_AfterAdapterMethod(object sender, AfterAdapterMethodArgs args)
	{
		// ** Argument Properties and Uses **
		// ** args.MethodName **
		// ** Add Event Handler Code **

		// ** Use MessageBox to find adapter method name
		// EpiMessageBox.Show(args.MethodName)
		switch (args.MethodName)
		{
			case "GetByID":
				
				break;
		}

	}
	
	private void oTrans_adapter_BeforeAdapterMethod(object sender, BeforeAdapterMethodArgs args)
	{
		switch (args.MethodName)
		{
			case "Update":
				Ice.Core.Session session = (Ice.Core.Session)UD07Form.Session;
				EpiTextBox journeyNum = (EpiTextBox)csm.GetNativeControlReference("46567b2e-6bc0-4967-be35-a0ec6843838f");
				TimeSpan result = TimeSpan.FromSeconds(Convert.ToInt32(this.cDispTime.Value));
				DateTime time = DateTime.Parse(result.ToString("hh':'mm"));
				string description = string.Format("{0}-{1}-{2}-{3}-{4}", this.baqCLoadShip.Text, this.udcRM.Text, DateTime.Parse(this.dteDP.Value.ToString()).ToString("yyMMdd"), time.ToString("HH''mm"), journeyNum.Text);
				var row = edvUD07.dataView[edvUD07.Row];
                if((Decimal)row["Number20"]==0)
                {
                  throw new UIException("Load/StageID cannot be 0 !");
                }
				row.BeginEdit();
				row["Character01"] = description;
				row["Character06"] = session.PlantID;
				row.EndEdit();
			break;
           
		}

	}

	private void baseToolbarsManager_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs args)
	{
		if(args.Tool.Key.ToString().Equals("ClearTool"))
		{
			ClearEpiDataView(edvAllocSO);
			ClearEpiDataView(edvSODtl);
			ClearEpiDataView(edvUnAllocSO);
			ClearEpiDataView(edvAllocTO);
			ClearEpiDataView(edvAllocUnAllocTO);
		}
        if(args.Tool.Key.ToString().Equals("ReprintManifest"))
        {
	          System.Collections.ArrayList packNums = new System.Collections.ArrayList();
			  EnumerableRowCollection collection = this.edvAllocSO.dataView.Table.AsEnumerable().Where(x => System.DBNull.Value != x["PackNum"]);
			string packNums2 = "";
			foreach(DataRow row in collection)
			{
				packNums.Add(row["PackNum"].ToString());

				packNums.Add(row["PackNum"].ToString());
				if (packNums2 == "")
				{
					packNums2 = row["PackNum"].ToString();
				} else {
					packNums2 += "," + row["PackNum"].ToString();
				}
			}

			if(packNums != null && packNums.Count > 0)
			{
				//LaunchFormOptions lfo = new LaunchFormOptions();
				//lfo.ContextValue = packNums;
				//ProcessCaller.LaunchForm(oTrans, "pbsLJPS", lfo);
				printGALBAQManifest(packNums2,"PbsManifestReport","PbsManifest");
			}
			else
			{
				MessageBox.Show("No Pack Found!!!");
			}
        }
	}
    
	private void UD07Form_Load(object sender, EventArgs args)
	{
		this.session = (Ice.Core.Session)UD07Form.Session;
		if (UseJourneyLoad().Equals(false))
		{
			if (MessageBox.Show("This site is not configured to use Journey Load", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
			{
				UD07Form.BeginInvoke(new MethodInvoker(UD07Form.Close));
			}
		}

		fromBtnList = new List<EpiButton>();
			EpiButton[] buttons = {btnPrintPickSlip, btnPrintManifest, btnShipJourney, btnPartShip, btnUnAllocateLine, btnUnAllocateSO, btnAllocate, btnApplyFilters,
							   btnTOUnAllocate, btnTOAllocate, btnTOApplyFilter,btnRetrieveAllocSOLD,btnSelectAllAlocSO,btnUnSelectAllocSO,btnSelectUnAllocSO,btnUnSelectUnAllocSO,btnSubmitForPicking};
			fromBtnList.AddRange(buttons);

			foreach (EpiButton btn in fromBtnList)
			{
				ToggleFormBtnState(btn, false);
			}

			var tvNodeBinding = tv.EpiTreeBindings;
			tvNodeBinding[0] = new EpiTreeBinding("UD07.Character01");
			this.ugUnAllocatedSO.DisplayLayout.AutoFitStyle = AutoFitStyle.ResizeAllColumns;
			this.ugUnAllocTO.DisplayLayout.AutoFitStyle = AutoFitStyle.ResizeAllColumns;
			AddToolButton(ReprintManifest, "ReprintManifest", "Reprint Manifest", true, true);


	}

	private bool UseJourneyLoad()
	{
		bool autoRelease = false;
		Ice.Proxy.Lib.BOReaderImpl bOReader = Ice.Lib.Framework.WCFServiceSupport.CreateImpl<Ice.Proxy.Lib.BOReaderImpl>((Ice.Core.Session)oTrans.Session, Epicor.ServiceModel.Channels.ImplBase<Ice.Contracts.BOReaderSvcContract>.UriPath);
		var ds = bOReader.GetRows("Erp:BO:PlantConfCtrl", "PlantConfCtrl.Plant =" + "'" + session.PlantID + "'", "Plant,PbsUseJourneyLoad_c");
		if (ds != null && ds.Tables["PlantConfCtrl"].Rows.Count > 0)
		{
			autoRelease = (bool)ds.Tables["PlantConfCtrl"].Rows[0]["PbsUseJourneyLoad_c"];
		}
		return autoRelease;
	}

	#region Add Button 
	private void AddToolButton(ButtonTool newButton,string buttonName, string caption,bool enabled, bool visible)
    {
       newButton =  new ButtonTool(buttonName);
       newButton.SharedProps.Caption = caption;
       newButton.SharedProps.Enabled=enabled;
       newButton.SharedProps.Visible = visible;
       if(!baseToolbarsManager.Tools.Exists(buttonName))
       {
         baseToolbarsManager.Tools.Add(newButton);
        ((Infragistics.Win.UltraWinToolbars.PopupMenuTool)baseToolbarsManager.Tools["ActionsMenu"]).Tools.Add(newButton);
       }
      // ButtonTool.ToolClick += new Infragistics.Win.UltraWinToolbars.ToolClickEventHandler(ButtonTool_ToolClick);

    }
    #endregion
    int NewJourneyNum=0;
	private void edvUD07_EpiViewNotification(EpiDataView view, EpiNotifyArgs args)
	{
		// ** Argument Properties and Uses **
		// view.dataView[args.Row]["FieldName"]
		// args.Row, args.Column, args.Sender, args.NotifyType
		// NotifyType.Initialize, NotifyType.AddRow, NotifyType.DeleteRow, NotifyType.InitLastView, NotifyType.InitAndResetTreeNodes
		if ((args.NotifyType == EpiTransaction.NotifyType.Initialize))
		{
			if ((args.Row > -1))
			{
				bool shipStatus = Convert.ToBoolean(edvUD07.dataView[edvUD07.Row]["CheckBox02"]);
				if(!shipStatus)
				{
					foreach(EpiButton btn in fromBtnList)
					{
						ToggleFormBtnState(btn, true);
					}
				}
				int journyNum = Convert.ToInt32(edvUD07.dataView[edvUD07.Row]["Key1"]);
				LoadAllocSO(journyNum);
                if(NewJourneyNum != journyNum)
                {
                 NewJourneyNum=journyNum;
                 ClearEpiDataView(this.edvUnAllocSO);
                }
				
			}
		}
	}

	private void udcRM_BeforeDropDown(object sender, System.ComponentModel.CancelEventArgs args)
	{
		var CCCData = oTrans.Factory("CallContextClientData");
		var plant = CCCData.dataView[CCCData.Row]["CurrentPlant"].ToString();
	
		udcRM.Rows.ColumnFilters["Character03"].FilterConditions.Clear();
		udcRM.Rows.ColumnFilters["Character03"].FilterConditions.Add(Infragistics.Win.UltraWinGrid.FilterComparisionOperator.Equals,plant);
	}
	
	private void CreateRowRuleUD07CheckBox02Equals_true()
	{
		// Description: ShipStatusRR
		// **** begin autogenerated code ****
		RuleAction disabledUD07_CheckBox02 = RuleAction.AddControlSettings(this.oTrans, "UD07.CheckBox02", SettingStyle.Disabled);
        RuleAction disabledUD07_Character01 = RuleAction.AddControlSettings(this.oTrans, "UD07.Character01", SettingStyle.Disabled);
        RuleAction disabledUD07_ShortChar01 = RuleAction.AddControlSettings(this.oTrans, "UD07.ShortChar01", SettingStyle.Disabled);
        RuleAction disabledUD07_Number20 = RuleAction.AddControlSettings(this.oTrans, "UD07.Number20", SettingStyle.Disabled);
        RuleAction disabledUD07_Date03 = RuleAction.AddControlSettings(this.oTrans, "UD07.Date03", SettingStyle.Disabled);
        RuleAction disabledUD07_Character10 = RuleAction.AddControlSettings(this.oTrans, "UD07.Character10", SettingStyle.Disabled);
        RuleAction disabledUD07_Character04 = RuleAction.AddControlSettings(this.oTrans, "UD07.Character04", SettingStyle.Disabled);
		RuleAction[] ruleActions = new RuleAction[] {
				                                      disabledUD07_CheckBox02,
                                                      disabledUD07_Character01,
                                                      disabledUD07_ShortChar01,
                                                      disabledUD07_Number20,
                                                      disabledUD07_Date03,
                                                      disabledUD07_Character10,
                                                      disabledUD07_Character04};
		// Create RowRule and add to the EpiDataView.
		RowRule rrCreateRowRuleUD07CheckBox02Equals_true = new RowRule("UD07.CheckBox02", RuleCondition.Equals, true, ruleActions);
		((EpiDataView)(this.oTrans.EpiDataViews["UD07"])).AddRowRule(rrCreateRowRuleUD07CheckBox02Equals_true);
		// **** end autogenerated code ****
	}
    
    private void CreateRowRuleUD07Number20Equals_zero()
	{
		// Description: ToggleBtnState
		// **** begin autogenerated code ****
		RuleAction[] ruleActions = new RuleAction[0];
		// Create RowRule and add to the EpiDataView.
		// Dummy Context Object
		object contextObject = null;
		RowRule rrCreateRowRuleUD07Number20Equals_zero = new RowRule("UD07.Number20", RuleCondition.Equals, 0, new RowRuleActionDelegate2(this.UD07Number20Equals_zero_CustomRuleAction), contextObject);
		((EpiDataView)(this.oTrans.EpiDataViews["UD07"])).AddRowRule(rrCreateRowRuleUD07Number20Equals_zero);
		// **** end autogenerated code ****
	}

	private void UD07Number20Equals_zero_CustomRuleAction(Ice.Lib.ExtendedProps.RowRuleDelegateArgs args)
	{
		// ** RowRuleDelegateArgs Properties: args.Arg1, args.Arg2, args.Context, args.Row
		// ** put custom Rule Action logic here
		Decimal LoadStageID = (Decimal)edvUD07.dataView[edvUD07.Row]["Number20"];
				
		foreach(EpiButton btn in fromBtnList)
		{
			if(LoadStageID==0)
			{
				ToggleFormBtnState(btn, false);
			}
			else
				ToggleFormBtnState(btn, true);
		}
	}

	private void CreateRowRuleUD07CheckBox02Equals_true0()
	{
		// Description: ToggleBtnState
		// **** begin autogenerated code ****
		RuleAction[] ruleActions = new RuleAction[0];
		// Create RowRule and add to the EpiDataView.
		// Dummy Context Object
		object contextObject = null;
		RowRule rrCreateRowRuleUD07CheckBox02Equals_true0 = new RowRule("UD07.CheckBox02", RuleCondition.Equals, true, new RowRuleActionDelegate2(this.UD07CheckBox02Equalstrue_CustomRuleAction), contextObject);
		((EpiDataView)(this.oTrans.EpiDataViews["UD07"])).AddRowRule(rrCreateRowRuleUD07CheckBox02Equals_true0);
		// **** end autogenerated code ****
	}

	private void UD07CheckBox02Equalstrue_CustomRuleAction(Ice.Lib.ExtendedProps.RowRuleDelegateArgs args)
	{
		// ** RowRuleDelegateArgs Properties: args.Arg1, args.Arg2, args.Context, args.Row
		// ** put custom Rule Action logic here
		bool shipStatus = Convert.ToBoolean(edvUD07.dataView[edvUD07.Row]["CheckBox02"]);
		
        if(shipStatus)
        {
            epiShapeShipped.Status = StatusTypes.Warning;
            epiShapeShipped.EnabledCaption="Shipped";
        }
        else
        {
          epiShapeShipped.Status = StatusTypes.OK;
            epiShapeShipped.EnabledCaption="Not Shipped";
        }		
		foreach(EpiButton btn in fromBtnList)
		{
			if(shipStatus)
			{  
				ToggleFormBtnState(btn, false);
			}
			else
            {
				ToggleFormBtnState(btn, true);
            }
		}
	}
    
   private void CreateRowRuleUD07CheckBox02Equals_false0()
	{
		// Description: ToggleBtnState
		// **** begin autogenerated code ****
		RuleAction[] ruleActions = new RuleAction[0];
		// Create RowRule and add to the EpiDataView.
		// Dummy Context Object
		object contextObject = null;
		RowRule rrCreateRowRuleUD07CheckBox02Equals_false0 = new RowRule("UD07.CheckBox02", RuleCondition.Equals, false, new RowRuleActionDelegate2(this.UD07CheckBox02EqualsFalse_CustomRuleAction), contextObject);
		((EpiDataView)(this.oTrans.EpiDataViews["UD07"])).AddRowRule(rrCreateRowRuleUD07CheckBox02Equals_false0);
		// **** end autogenerated code ****
	}

	private void UD07CheckBox02EqualsFalse_CustomRuleAction(Ice.Lib.ExtendedProps.RowRuleDelegateArgs args)
	{
		// ** RowRuleDelegateArgs Properties: args.Arg1, args.Arg2, args.Context, args.Row
		// ** put custom Rule Action logic here
		bool shipStatus = Convert.ToBoolean(edvUD07.dataView[edvUD07.Row]["CheckBox02"]);
		
        if(shipStatus)
        {
            epiShapeShipped.Status = StatusTypes.Warning;
            epiShapeShipped.EnabledCaption="Shipped";
        }
        else
        {
            epiShapeShipped.Status = StatusTypes.OK;
            epiShapeShipped.EnabledCaption="Not Shipped";
        }		
		
	}

	#endregion Form Events
	
	#region SODetail Allocation

	public void LoadAllocSODtl(string orderNum, string shipToName, string address, bool useOTS, DateTime shipDate, int shipbyTime, int JourneyNum)
	{
		allocSODtlDS = RunUBAQ("pbsAllocSODtl", allocSODtlQEDS, DateTime.MinValue, DateTime.MinValue, orderNum, "", "", JourneyNum, "", 0, 0, shipToName, address, useOTS, shipDate, shipbyTime);
		this.edvSODtl = (EpiDataView)(oTrans.EpiDataViews["edvSODtl"]);
		ClearEpiDataView(this.edvSODtl);
		// Need to review this code
		var allocSODtlDSMod = allocSODtlDS.Tables["Results"].AsEnumerable().Where(x => Convert.ToInt32(x["OrderRel_OrderNum"]) == Convert.ToInt32(orderNum) 
																					&& x["Calculated_ShipToName"].ToString() == shipToName
																					);
		foreach(DataRow row in allocSODtlDSMod)
		{
			var newRow = this.edvSODtl.dataView.AddNew();
			newRow.BeginEdit();
			newRow["Selected"] = row["Calculated_Selected"];
			newRow["OrderNum"] = row["OrderRel_OrderNum"];
			newRow["LRel"] = row["Calculated_LRel"];
			newRow["Part"] = row["OrderRel_PartNum"];
			newRow["Description"] = row["OrderDtl_LineDesc"];
			newRow["Qty"] = row["OrderRel_OurStockQty"];
			newRow["UOM"] = row["OrderRel_IUM"];
			newRow["Avail"] = row["Calculated_AvailableQty"];
			newRow["PO"] = row["OrderRel_PONum"];
			newRow["Due Date"] = row["PORel_DueDate"];
			newRow["ShipToName"] = row["ShipTo_Name"];
			newRow["Address1"] = row["ShipTo_Address1"];
			newRow["UseOTS"] = row["OrderRel_UseOTS"];
            newRow["PackingSlip"]= row["ShipDtl_PackNum"];
            newRow["Calculated_ReservedQtyFlag"]= row["Calculated_ReservedQtyFlag"];
            newRow["Calculated_PickingQtyFlag"]= row["Calculated_PickingQtyFlag"];
            newRow["Calculated_ReservedQtyFlag"]= row["Calculated_ReservedQtyFlag"];
			newRow.EndEdit();

		}
		if(allocSODtlDS.Tables["Results"].Rows.Count > 0)
		{
			List<UltraGridRow> rows = this.ugSODtl.Rows.Where(x => Convert.ToDecimal(x.Cells["Qty"].Value) == Convert.ToDecimal(x.Cells["Avail"].Value)).ToList();
			ChangeCellAppearance(rows, 7);
			rows = this.ugSODtl.Rows.Where(x => !Convert.ToBoolean(x.Cells["Calculated_ReservedQtyFlag"].Value)).ToList();
			//ToggleRowVisibility(rows, true);
			
		}
	}

	public void btnPartShip_Click(object sender, System.EventArgs args)
	{
		// ** Place Event Handling Code Here **
		EnumerableRowCollection collection = this.edvSODtl.dataView.Table.AsEnumerable().Where(x => Convert.ToBoolean(x["Selected"]) == true);
		PartShip(collection);
	}

	public void PartShip(EnumerableRowCollection collection)
	{
		allocSODtlDS.Clear();
		foreach(DataRow row in collection)
		{
			if(Convert.ToInt32(row["Avail"]) != 0)
			{
				DataRow newRow = allocSODtlDS.Tables["Results"].NewRow();
				newRow["Calculated_LRel"] =  row["LRel"];
				newRow["OrderRel_OrderNum"] = row["OrderNum"];
				newRow["OrderRel_SellingReqQty"] = row["Qty"];
				newRow["Calculated_AvailableQty"] = row["Avail"];
				newRow["RowMod"] = "A";
			    newRow["SysRowID"] = Guid.NewGuid();
			    newRow["RowIdent"] = newRow["SysRowID"].ToString();
				allocSODtlDS.Tables["Results"].Rows.Add(newRow);
			}
			else
			{
				MessageBox.Show(string.Format("Can not part ship L.Rel {0}. Available quantity is 0.",row["LRel"]));
			}
		}
		if(allocSODtlDS.Tables["Results"].Rows.Count > 0)
		{
			ConncetDQAdapter();
			dynamicQueryAdapter.Update("pbsAllocSODtl", allocSODtlDS);
			DisposeDQAdapter();
		}
		var gridRow = ugAllocatedSO.ActiveRow;
		string orderNum = gridRow.Cells["Order"].Value.ToString(), shipToName = gridRow.Cells["Name"].Value.ToString(), address = gridRow.Cells["Address"].Value.ToString();
		bool useOTS = Convert.ToBoolean(gridRow.Cells["UseOTS"].Value);
		DateTime shipDate = Convert.ToDateTime(gridRow.Cells["ShipDate"].Value);
		int shipbyTime = ConvertTimeToInt(gridRow.Cells["Time"].Value.ToString()),JourneyNum= Convert.ToInt32(gridRow.Cells["JourneyNum"].Value.ToString());
		LoadAllocSODtl(orderNum, shipToName, address, useOTS, shipDate, shipbyTime,JourneyNum);
	}

	public void UnAllocSODtlLRel(EnumerableRowCollection collection)
	{
		allocSODtlDS.Clear();
		foreach(DataRow row in collection)
		{
          if(string.IsNullOrEmpty(row["PackingSlip"].ToString()))
          {
			DataRow newRow = allocSODtlDS.Tables["Results"].NewRow();
			newRow["Calculated_LRel"] =  row["LRel"];
			newRow["OrderRel_OrderNum"] = row["OrderNum"];
			newRow["RowMod"] = "A";
		    newRow["SysRowID"] = Guid.NewGuid();
		    newRow["RowIdent"] = newRow["SysRowID"].ToString();
			allocSODtlDS.Tables["Results"].Rows.Add(newRow);
          }
          else
          {
            EpiMessageBox.Show("Release is already packed and cannot be unallocated!");
            break;
          }
		}
		ConncetDQAdapter();
		dynamicQueryAdapter.RunCustomAction("pbsAllocSODtl", "UnAllocSOLRel", allocSODtlDS, false);
		DisposeDQAdapter();
	}

	private void btnUnAllocateLine_Click(object sender, System.EventArgs args)
	{
		EnumerableRowCollection collection = this.edvSODtl.dataView.Table.AsEnumerable().Where(x => Convert.ToBoolean(x["Selected"]) == true);
		UnAllocSODtlLRel(collection);
		
		// Need to be reveiwed;
		var row = ugAllocatedSO.ActiveRow;
		string orderNum = row.Cells["Order"].Value.ToString(), shipToName = row.Cells["Name"].Value.ToString(), address = row.Cells["Address"].Value.ToString();
		bool useOTS = Convert.ToBoolean(row.Cells["UseOTS"].Value);
		DateTime shipDate = Convert.ToDateTime(row.Cells["ShipDate"].Value);
		int shipbyTime = ConvertTimeToInt(row.Cells["Time"].Value.ToString()),JourneyNum= Convert.ToInt32(row.Cells["JourneyNum"].Value.ToString());
		LoadAllocSODtl(orderNum, shipToName, address, useOTS, shipDate, shipbyTime,JourneyNum);

		//Refresh Alloc SO Grid
		int journyNum = Convert.ToInt32(edvUD07.dataView[edvUD07.Row]["Key1"]);
		LoadAllocSO(journyNum);
	}
	
	#endregion
     
    private void AutoRefresh()
    {
      FormFunctions.LoadSplash("Refreshing....");
      MethodInfo mi = typeof(EpiBaseForm).GetMethod("handleToolClick", BindingFlags.Instance | BindingFlags.NonPublic);
      mi.Invoke(UD07Form, new object[]{"RefreshTool",new Infragistics.Win.UltraWinToolbars.ToolClickEventArgs(UD07Form.MainToolManager.Tools["RefreshTool"],null)});
      FormFunctions.CloseSplash();
    }

	#region Ship Journey
	private void btnShipJourney_Click(object sender, System.EventArgs args)
	{
       //Check if edvSODtl rows have packNum
        // var allocatedAndStagedCount = this.edvAllocSO.dataView.Table.AsEnumerable().Count(x => x["Allocated"] != x["Packed"]);
       //  if(allocatedAndStagedCount==0)
        // {
	       var checkAllPackStaged = this.edvAllocSO.dataView.Table.AsEnumerable().Count(x =>x["Status"].ToString()!= "STAGED");
	       if(checkAllPackStaged==0)
	       {
			var unPackedSOLR = this.edvAllocSO.dataView.Table.AsEnumerable().Count(x => System.DBNull.Value == x["PackNum"]); //|| Convert.ToInt32(x["Allocated"]) != Convert.ToInt32(x["Packed"]));
	 	   if(unPackedSOLR == 0 )
			{	
				//var totalPackedSOLR = this.edvAllocSO.dataView.Table.AsEnumerable().GroupBy(x => x["Order"]).Select(row => new {Stage = row.Sum(y => Convert.ToInt32(y["Packed"])),
			   																												//AllocQty = row.Select(y => Convert.ToInt32(y["Allocated"])).FirstOrDefault() }).FirstOrDefault();
             int unMatchedStageAndAllocQty =   this.edvAllocSO.dataView.Table.AsEnumerable().GroupBy(x => x["Order"]).Select(row => new {Stage = row.Sum(y => Convert.ToInt32(y["Packed"])),
																																AllocQty = row.Sum(y => Convert.ToInt32(y["Allocated"])) })
                                                                                                                               .Where(w=>w.Stage != w.AllocQty).Count();
				//if(totalPackedSOLR.Stage == totalPackedSOLR.AllocQty)
                if(unMatchedStageAndAllocQty ==0)
				{
					var confirm = MessageBox.Show("Are you sure you want to ship this journey?","Confirm!!",MessageBoxButtons.YesNo);
			        if(confirm==DialogResult.Yes)
			         {
			           edvUD07.dataView[edvUD07.Row].BeginEdit();
                       edvUD07.dataView[edvUD07.Row]["ShortChar02"]="Approved"
			           edvUD07.dataView[edvUD07.Row]["CheckBox02"]=true;
			           edvUD07.dataView[edvUD07.Row].EndEdit();
			           this.oTrans.Update();
			
					   //EnumerableRowCollection collection = this.edvAllocSO.dataView.Table.AsEnumerable().Where(x => System.DBNull.Value != x["PackNum"]);
					   //ShipJourney(collection);
		
					   foreach(EpiButton btn in fromBtnList)
						{
							ToggleFormBtnState(btn, true);
						}
			         }

				}
				else
		        {
					MessageBox.Show("You have items allocated to the Journey that are not packed, they must be removed to ship the journey");
		        }
			}
	       }
	       else
	       {
	          MessageBox.Show("Cannot ship this journey!\r\n Please remove unstaged Packs from this journey and try again.");
	       }
           AutoRefresh();
        // }
      // else
      // {
       //  MessageBox.Show("You have items allocated to the Journey that are not staged, they must be removed to ship the journey");
     //  }
      // AutoRefresh();
	}
	
	public void ShipJourney(EnumerableRowCollection collection)
	{
		allocSODS.Clear();
		foreach(DataRow row in collection)
		{
			DataRow newRow = allocSODS.Tables["Results"].NewRow();
			newRow["PbsAllocSO_ShipDtl_PackNum"] = row["PackNum"];
			newRow["RowMod"] = "A";
		    newRow["SysRowID"] = Guid.NewGuid();
		    newRow["RowIdent"] = newRow["SysRowID"].ToString();
			allocSODS.Tables["Results"].Rows.Add(newRow);
		}
		ConncetDQAdapter();
		dynamicQueryAdapter.RunCustomAction("pbsAllocSOExternal", "ShipJourney", allocSODS, false);
		DisposeDQAdapter();
	}
	
	#endregion Ship Journey

	#region Print Manifest
	private void btnPrintManifest_Click(object sender, System.EventArgs args)
	{
		// ** Place Event Handling Code Here **
		System.Collections.ArrayList packNums = new System.Collections.ArrayList();
		EnumerableRowCollection collection = this.edvAllocSO.dataView.Table.AsEnumerable().Where(x => System.DBNull.Value != x["PackNum"]);
		string packNums2 = "";
       
		foreach(DataRow row in collection)
		{
			packNums.Add(row["PackNum"].ToString());
			if (packNums2 == "")
			{
				packNums2 = row["PackNum"].ToString();
			} else {
				packNums2 += "," + row["PackNum"].ToString();
			}
		}
	
		if(packNums != null && packNums.Count > 0)
		{
			//LaunchFormOptions lfo = new LaunchFormOptions();
			//lfo.ContextValue = packNums;
			//ProcessCaller.LaunchForm(oTrans, "pbsLJPS", lfo);
			//MessageBox.Show(packNums2);
            printGALBAQManifest(packNums2,"PbsManifestReport","PbsManifest");
			//printManifest(packNums2);
		}
		else
		{
			MessageBox.Show("No Pack Found!!!");
		}
	}
	#endregion Print Manifest
    
    private string GetSystemAgent()
  {
    string agentID = string.Empty;
    using (var aSA = new SysAgentAdapter(oTrans))
    {
         aSA.BOConnect();
         aSA.GetDefaultTaskAgentID(out agentID);
         if (string.IsNullOrEmpty(agentID))
         { 
            agentID = "SystemTaskAgent";
         }
    } 
     return agentID;
   }

    private void printGALBAQManifest(string packNums,string baqID,string baqRptID)
    {
       Ice.Rpt.BAQReportDataSet baqDS = baqRptSvc.GetNewBAQReportParam(baqRptID);
		//Set up parameters
	    Ice.BO.DynamicReportDataSet rptDS = dynamicRptSvc.GetByID(baqRptID);
        rptDS.BAQRptFilter[0].FilterValue	  = packNums;

		baqDS.BAQReportParam[0].ReportID	  = baqRptID;
		baqDS.BAQReportParam[0].BAQID		 = baqID;
		baqDS.BAQReportParam[0].BAQRptID	  = baqRptID;
		baqDS.BAQReportParam[0].AutoAction    = "SSRSPREVIEW";
		baqDS.BAQReportParam[0].WorkstationID = Ice.Lib.Report.EpiReportFunctions.GetWorkStationID((Ice.Core.Session)oTrans.Session);
		baqDS.BAQReportParam[0].Filter1	   = rptDS.GetXml();
        baqDS.BAQReportParam[0].Filter2	  = packNums;
		baqDS.BAQReportParam[0].SSRSRenderFormat  = "PDF";
        baqDS.BAQReportParam[0].ReportStyleNum = 1;                                   
		//baqDS.BAQReportParam[0].SSRSEnableRouting = true;
		baqRptSvc.SubmitToAgent(baqDS,GetSystemAgent(),0,0,"Ice.UIRpt.BAQReport;PbsManifest");
        MessageBox.Show("Manifest report submitted!!");
    }

	private void printManifest(string packnums)
	{
		PackingSlipPrintImpl packingSlipPrintSvc = Ice.Lib.Framework.WCFServiceSupport.CreateImpl<Erp.Proxy.Rpt.PackingSlipPrintImpl>(oTrans.CoreSession, Erp.Proxy.Rpt.PackingSlipPrintImpl.UriPath);
//		this.packingSlipPrintSvc = Ice.Lib.Framework.WCFServiceSupport.CreateImpl<Erp.Proxy.Rpt.PackingSlipPrintImpl>(oTrans.CoreSession, Erp.Proxy.Rpt.PackingSlipPrintImpl.UriPath);

		packingSlipPrintSvc.GetRptArchiveList();
		var ds = packingSlipPrintSvc.GetNewParameters();
		packingSlipPrintSvc.GetDefaults(ds);
		var newRow = ds.PackingSlipParam[0];
		if (newRow == null)
		{
			return;
		}
		else
		{
			newRow.PackNumList = packnums;
			newRow.SysRowID = Guid.NewGuid();
			newRow.AutoAction = "SSRSPREVIEW";
			newRow.AgentID = "SystemAgent";
			newRow.AgentTaskNum = 0;
			newRow.RecurringTask = false;
			newRow.ReportStyleNum = 5003;
			newRow.WorkstationID = Ice.Lib.Report.EpiReportFunctions.GetWorkStationID((Ice.Core.Session)oTrans.Session);
			//newRow.ArchiveCode = 1;
			newRow.DateFormat = "d/mm/yyyy";
			newRow.NumericFormat = ",.";
			newRow.SSRSRenderFormat = "PDF";
			newRow.ReportCurrencyCode = "AUD";
			newRow.ReportCultureCode = "en-AU";
			newRow.SSRSRenderFormat = "PDF";
			newRow.PrintReportParameters = false;
			//newRow.SSRSEnableRouting = true;
			newRow.DesignMode = false;
			newRow.RowMod = "A";

		}
		packingSlipPrintSvc.SubmitToAgent(ds, "SystemAgent", 0, 0, "Erp.UIRpt.PackingSlipPrint");
			
	}
	
	#region Unallocated TO Grid Func
	
	private void btnTOAllocate_Click(object sender, System.EventArgs args)
	{
		if(this.edvAllocUnAllocTO.dataView.Table.Rows.Count > 0)
		{
			EnumerableRowCollection collection = this.edvAllocUnAllocTO.dataView.Table.AsEnumerable().Where(x => Convert.ToBoolean(x["Selected"]) == true);
			AllocateTO(collection);
			
			// Refersh UnAllocSOGrid // Need to review this part as it'll create performance issues
			ApplyFiltersTO();
			//Refresh AllocSOGrid
			int journyNum = Convert.ToInt32(edvUD07.dataView[edvUD07.Row]["Key1"]);
			LoadAllocTO(journyNum);
		}
	}

	private void btnTOApplyFilter_Click(object sender, System.EventArgs args)
	{
		ApplyFiltersTO();
	}

	public void ApplyFiltersTO()
	{
		DateTime? fromDate = Convert.ToDateTime(this.dteTOFD.Value), toDate = Convert.ToDateTime(this.dteTOTD.Value);
		string orderNum = this.neTO.Value.ToString();//this.neTO.Value != DBNull.Value? Convert.ToInt32(this.neTO.Value): 0;
		string city = Convert.ToString(this.txtTOCity.Value), custID = Convert.ToString(this.txtTO.Value);
	
		unAllocTODS = RunUBAQ("pbsUnAllocTO", unAllocSOQEDS, fromDate, toDate, orderNum, city, custID, 0, "TO");
		LoadUnAllocTO(unAllocTODS.Tables["Results"]);
		if(unAllocTODS.Tables["Results"].Rows.Count > 0)
		{
			List<UltraGridRow> rows = this.ugUnAllocTO.Rows.Where(x => Convert.ToBoolean(x.Cells["FullyFulfillable"].Value) == true).ToList();
			ChangeCellAppearance(rows, 10);
			if(this.cbTOFulfillable.Checked)
			{
				List<UltraGridRow> collection = this.ugUnAllocTO.Rows.AsEnumerable().Where(x => Convert.ToBoolean(x.Cells["FullyFulfillable"].Value) != true).ToList();
				ChangeGridRowState(collection, true);
			}
		}
	}
	
	public void LoadUnAllocTO(DataTable dt)
	{
		this.edvAllocUnAllocTO = (EpiDataView)(oTrans.EpiDataViews["edvAllocUnAllocTO"]);
		ClearEpiDataView(this.edvAllocUnAllocTO);
		foreach(DataRow row in dt.Rows)
		{
			var newRow = this.edvAllocUnAllocTO.dataView.AddNew();
			newRow.BeginEdit();
			newRow["Selected"] = row["Calculated_Selected"];
			newRow["SEQ"] = row["Calculated_SEQ"];
			//newRow["TOOrder"] = row["OrderHed_OrderNum"];
			newRow["Transfer Order"] = row["TFOrdDtl2_TFOrdNum"];
			newRow["From Site"] = row["TFOrdDtl2_Plant"];
			newRow["To Site"] = row["TFOrdDtl2_ToPlant"];
			newRow["Name"] = row["Plant_Name"];
			newRow["City"] = row["Plant_City"];
			newRow["Lines"] = row["Calculated_TotalTOL"];
			newRow["UnAlloc"] = row["Calculated_UnAllocTOLines"];
			newRow["Fulfillable"] = row["Calculated_FulfillableTOL"];
			newRow["ShipDate"] = row["TFOrdDtl2_RequestDate"];
			newRow["FullyFulfillable"] = row["Calculated_FullyFulfillable"];
			newRow.EndEdit();

		}
	}

	public void AllocateTO(EnumerableRowCollection collection)
	{	
		unAllocTODS.Clear();
		int journyNum = Convert.ToInt32(edvUD07.dataView[edvUD07.Row]["Key1"]);
		foreach(DataRow row in collection)
		{
			DataRow newRow = unAllocTODS.Tables["Results"].NewRow();
			newRow["TFOrdDtl2_PbsJourneyNum_c"] =  journyNum;
			newRow["TFOrdDtl2_TFOrdNum"] = row["Transfer Order"];
			newRow["TFOrdDtl2_RequestDate"] = row["ShipDate"];
			newRow["RowMod"] = "A";
		    newRow["SysRowID"] = Guid.NewGuid();
		    newRow["RowIdent"] = newRow["SysRowID"].ToString();
			unAllocTODS.Tables["Results"].Rows.Add(newRow);
		}
		ConncetDQAdapter();
		dynamicQueryAdapter.Update("pbsUnAllocTO", unAllocTODS);
		DisposeDQAdapter();
	}
	
	private void cbTOFulfillable_CheckedChanged(object sender, System.EventArgs args)
	{
		List<UltraGridRow> collection = this.ugUnAllocTO.Rows.AsEnumerable().Where(x => Convert.ToBoolean(x.Cells["FullyFulfillable"].Value) != true).ToList();	
		if(((EpiCheckBox)sender).Checked)
			ChangeGridRowState(collection, true);
		else
			ChangeGridRowState(collection, false);
	}
	#endregion Unallocated TO Grid Func

	#region Allocated TO Grid Func

	private void btnTOUnAllocate_Click(object sender, System.EventArgs args)
	{
		if(this.edvAllocTO.dataView.Table.Rows.Count > 0)
		{
			EnumerableRowCollection collection = this.edvAllocTO.dataView.Table.AsEnumerable().Where(x => Convert.ToBoolean(x["Selected"]) == true);
			UnAllocateTO(collection);
			int journyNum = Convert.ToInt32(edvUD07.dataView[edvUD07.Row]["Key1"]);
			LoadAllocTO(journyNum);
		}
	}

	public void LoadAllocTO(int journyNum)
	{
		allocTODS = RunUBAQ("pbsAllocTOL", allocTOQEDS, DateTime.MinValue, DateTime.MinValue, "0", "", "", journyNum);
		this.edvAllocTO = (EpiDataView)(oTrans.EpiDataViews["edvAllocTO"]);
		ClearEpiDataView(this.edvAllocTO);
		foreach(DataRow row in allocTODS.Tables["Results"].Rows)
		{
			var newRow = this.edvAllocTO.dataView.AddNew();
			newRow.BeginEdit();
			newRow["Selected"] = row["Calculated_Selected"];
			newRow["SEQ"] = row["Calculated_SEQ"];
			//newRow["TOOrder"] = row["OrderHed_OrderNum"];
			newRow["Transfer Order"] = row["TFOrdDtl4_TFOrdNum"];
			newRow["From Site"] = row["TFOrdDtl4_Plant"];
			newRow["To Site"] = row["TFOrdDtl4_ToPlant"];
			newRow["Name"] = row["Plant_Name"];
			newRow["City"] = row["Plant_City"];
			newRow["Lines"] = row["Calculated_TotalLines"];
			newRow["Allocated"] = row["Calculated_AllocatedTOL"];
			newRow["InPicking"] = row["Calculated_InPicking"];
			newRow["Picked"] = row["Calculated_Picked"];
			newRow["Packed"] = row["Calculated_Staged"];
			newRow["ShipDate"] = row["TFOrdDtl4_RequestDate"];
			newRow["PackNum"] = row["TFShipDtl_PackNum"];
			newRow["JourneyNum"] = row["TFOrdDtl4_PbsJourneyNum_c"];
			newRow.EndEdit();

		}
	}
	
	public void UnAllocateTO(EnumerableRowCollection collection)
	{
		allocTODS.Clear();
		foreach(DataRow row in collection)
		{
			DataRow newRow = allocTODS.Tables["Results"].NewRow();
			newRow["TFOrdDtl4_PbsJourneyNum_c"] =  row["JourneyNum"];
			newRow["TFOrdDtl4_TFOrdNum"] = row["Transfer Order"];
			newRow["TFOrdDtl4_RequestDate"] = row["ShipDate"];
			newRow["RowMod"] = "A";
		    newRow["SysRowID"] = Guid.NewGuid();
		    newRow["RowIdent"] = newRow["SysRowID"].ToString();
			allocTODS.Tables["Results"].Rows.Add(newRow);
		}
		ConncetDQAdapter();
		dynamicQueryAdapter.Update("pbsAllocTOL", allocTODS);
		DisposeDQAdapter();
		
	}	

	#endregion Allocated TO Grid Func

	#region Allocated TO Detial Grid Func

	private void btnUnAllocTOL_Click(object sender, System.EventArgs args)
	{
		// ** Place Event Handling Code Here **
	}
	
	private void ugTOAlloc_AfterRowActivate(object sender, System.EventArgs args)
	{
		var row = ugTOAlloc.ActiveRow;
		LoadAllocTODtl(row.Cells["Transfer Order"].Value.ToString());
	}
	
	public void LoadAllocTODtl(string orderNum)
	{
		allocTODtlDS = RunUBAQ("pbsAllocTOLDtl", allocTODtlQEDS, DateTime.MinValue, DateTime.MinValue, orderNum, "", "", 0, "TS");
		this.edvTODtl = (EpiDataView)(oTrans.EpiDataViews["edvTODtl"]);
		ClearEpiDataView(this.edvTODtl);
		foreach(DataRow row in allocTODtlDS.Tables["Results"].Rows)
		{
			var newRow = this.edvTODtl.dataView.AddNew();
			newRow.BeginEdit();
			newRow["Selected"] = row["Calculated_Selected"];
			newRow["TO"] = row["TFOrdDtl_TFOrdNum"];
			newRow["Line"] = row["TFOrdDtl_TFOrdLine"];
			newRow["Part"] = row["TFOrdDtl_PartNum"];
			newRow["Description"] = row["Part_PartDescription"];
			newRow["Qty"] = row["TFOrdDtl_SellingShippedQty"];
			newRow["UOM"] = row["TFOrdDtl_SellingQtyUOM"];
			newRow["Avail"] = row["Calculated_AvailableQty"];
			newRow.EndEdit();

		}
	}
	#endregion Allocated TO Detial Grid Func

	#region Print Pickslip
     
     private void btnPrintPickSlip_Click(object sender, System.EventArgs args)
	{
	   EnumerableRowCollection allocSOCollection = null;
	   if(this.edvAllocSO.dataView.Table.Rows.Count > 0)
		  allocSOCollection = this.edvAllocSO.dataView.Table.AsEnumerable().Where(x => Convert.ToBoolean(x["Selected"]) == true);
	   printMtlReport(allocSOCollection);
		
	}
	private void btnSubmitForPicking_Click(object sender, System.EventArgs args)
	{
		// ** Place Event Handling Code Here **
       EnumerableRowCollection allocSOCollection = null, allocTOCollection = null;
		if(this.edvAllocSO.dataView.Table.Rows.Count > 0)
			allocSOCollection = this.edvAllocSO.dataView.Table.AsEnumerable().Where(x => Convert.ToBoolean(x["Selected"]) == true);
		
		ReserverAndReleaseForPicking(allocSOCollection, allocTOCollection);
		int journyNum = Convert.ToInt32(edvUD07.dataView[edvUD07.Row]["Key1"]);
		LoadAllocSO(journyNum);
	}
	
    private void printMtlReport(EnumerableRowCollection allocSOCollection)
    {
       allocSODS.Clear();
       int journyNum = Convert.ToInt32(edvUD07.dataView[edvUD07.Row]["Key1"]);
	   if(allocSOCollection != null)
	   {
         foreach(DataRow row in allocSOCollection)
	     {
				DataRow newRow = allocSODS.Tables["Results"].NewRow();
				newRow["PbsAllocSO_OrderHed_OrderNum"] = row["Order"];
				newRow["PbsAllocSO_OrderRel4_ReqDate"] = row["ShipDate"];
				newRow["PbsAllocSO_Calculated_ShipToName"] = row["Name"];
				newRow["PbsAllocSO_Calculated_ShipToAddress"] = row["Address"];
				newRow["PbsAllocSO_OrderRel4_UseOTS"] = row["UseOTS"];
				newRow["PbsAllocSO_OrderRel4_ShipToCustNum"] = row["ShipToCustNum"];
				newRow["PbsAllocSO_OrderRel4_ShipToNum"] = row["ShipToNum"];
				newRow["PbsAllocSO_OrderRel4_ShipbyTime"] = ConvertTimeToInt(row["Time"].ToString());
                newRow["PbsAllocSO_OrderRel4_PbsJourneyNum_c"] = journyNum;
				newRow["PbsAllocSO_Calculated_OTSHeader"] = row["OTSHeader"];
				newRow["RowMod"] = "A";
			    newRow["SysRowID"] = Guid.NewGuid();
			    newRow["RowIdent"] = newRow["SysRowID"].ToString();
				allocSODS.Tables["Results"].Rows.Add(newRow);
			}
	  }  
      if(allocSODS.Tables["Results"].Rows.Count > 0)
		{
			ConncetDQAdapter();
			dynamicQueryAdapter.RunCustomAction("pbsAllocSOExternal", "PrintMtlQueueReport", allocSODS, false);
			DisposeDQAdapter();
		}
		else
			MessageBox.Show("No Record Selected!!!");
    }

	public void ReserverAndReleaseForPicking(EnumerableRowCollection allocSOCollection, EnumerableRowCollection allocTOCollection)
	{
		allocSODS.Clear();
        int journyNum = Convert.ToInt32(edvUD07.dataView[edvUD07.Row]["Key1"]);
		if(allocSOCollection != null)
		{
            
            
			foreach(DataRow row in allocSOCollection)
			{
				DataRow newRow = allocSODS.Tables["Results"].NewRow();
				newRow["PbsAllocSO_OrderHed_OrderNum"] = row["Order"];
				newRow["PbsAllocSO_OrderRel4_ReqDate"] = row["ShipDate"];
				newRow["PbsAllocSO_Calculated_ShipToName"] = row["Name"];
				newRow["PbsAllocSO_Calculated_ShipToAddress"] = row["Address"];
				newRow["PbsAllocSO_OrderRel4_UseOTS"] = row["UseOTS"];
				newRow["PbsAllocSO_OrderRel4_ShipToCustNum"] = row["ShipToCustNum"];
				newRow["PbsAllocSO_OrderRel4_ShipToNum"] = row["ShipToNum"];
				newRow["PbsAllocSO_OrderRel4_ShipbyTime"] = ConvertTimeToInt(row["Time"].ToString());
                newRow["PbsAllocSO_OrderRel4_PbsJourneyNum_c"] = journyNum;
				newRow["PbsAllocSO_Calculated_OTSHeader"] = row["OTSHeader"];
				newRow["RowMod"] = "A";
			    newRow["SysRowID"] = Guid.NewGuid();
			    newRow["RowIdent"] = newRow["SysRowID"].ToString();
				allocSODS.Tables["Results"].Rows.Add(newRow);
			}
		}
	
		if(allocTOCollection != null)
		{
			foreach(DataRow row in allocTOCollection)
			{
				DataRow newRow = allocSODS.Tables["Results"].NewRow();
				newRow["Calculated_TO"] = row["Transfer Order"];	
				newRow["Calculated_TORequestDate"] = row["ShipDate"];
				newRow["Calculated_DemandType"] = "TO";
                newRow["OrderRel4_PbsJourneyNum_c"] = journyNum;
				newRow["RowMod"] = "A";
			    newRow["SysRowID"] = Guid.NewGuid();
			    newRow["RowIdent"] = newRow["SysRowID"].ToString();
				allocSODS.Tables["Results"].Rows.Add(newRow);
			}
		}
		
		if(allocSODS.Tables["Results"].Rows.Count > 0)
		{
			ConncetDQAdapter();
			dynamicQueryAdapter.RunCustomAction("pbsAllocSOExternal", "ReserveAndReleaseForPicking", allocSODS, false);
			DisposeDQAdapter();
		}
		else
			MessageBox.Show("No Record Selected!!!");
	}

	#endregion Print Pickslip

	private void SetExtendedProperties()
	{
		// Begin Wizard Added EpiDataView Initialization
		EpiDataView edvedvAllocSO = ((EpiDataView)(this.oTrans.EpiDataViews["edvAllocSO"]));
		// End Wizard Added EpiDataView Initialization

		// Begin Wizard Added Conditional Block
		if (edvedvAllocSO.dataView.Table.Columns.Contains("Order"))
		{
			// Begin Wizard Added ExtendedProperty Settings: edvedvAllocSO-Order
			edvedvAllocSO.dataView.Table.Columns["Order"].ExtendedProperties["Like"] = "OrderHed.OrderNum";
			edvedvAllocSO.dataView.Table.Columns["Order"].ExtendedProperties["EpiContextMenuKey"] = "OrderHed.OrderNum";
			// End Wizard Added ExtendedProperty Settings: edvedvAllocSO-Order
			
		}
        if(edvedvAllocSO.dataView.Table.Columns.Contains("PackNum"))
        {
          // Begin Wizard Added ExtendedProperty Settings: edvedvAllocSO-PackNum
			edvedvAllocSO.dataView.Table.Columns["PackNum"].ExtendedProperties["Like"] = "ShipHead.PackNum";
			edvedvAllocSO.dataView.Table.Columns["PackNum"].ExtendedProperties["EpiContextMenuKey"] = "ShipHead.PackNum";
			// End Wizard Added ExtendedProperty Settings: edvedvAllocSO-PackNum
        }
		// End Wizard Added Conditional Block
	}

	private void cbShowAll_CheckedChanged(object sender, System.EventArgs args)
	{
		List<UltraGridRow> rows = null;
	/*	if(this.cbShowAll.Checked)
		{
			rows = this.ugSODtl.Rows.Select(x => x).ToList();//Where(x => !Convert.ToBoolean(x.Cells["Calculated_ReservedQtyFlag"].Value)).ToList();
			ToggleRowVisibility(rows, false);
		}
		else
		{
			rows = this.ugSODtl.Rows.Where(x => !Convert.ToBoolean(x.Cells["Calculated_ReservedQtyFlag"].Value)).ToList();
			ToggleRowVisibility(rows, true);
		}*/
	}

	private void CreateRowRuleedvUnAllocSOFulfillableEquals_true()
	{
		// Description: HighlightCreditHold
		// **** begin autogenerated code ****
		RuleAction erroredvUnAllocSO_RowAction = RuleAction.AddRowSettings(this.oTrans, "edvUnAllocSO", false, SettingStyle.Error);
		RuleAction[] ruleActions = new RuleAction[] {
				erroredvUnAllocSO_RowAction};
		// Create RowRule and add to the EpiDataView.
		RowRule rrCreateRowRuleedvUnAllocSOFulfillableEquals_true = new RowRule("edvUnAllocSO.CreditHold", RuleCondition.Equals, true, ruleActions);
		((EpiDataView)(this.oTrans.EpiDataViews["edvUnAllocSO"])).AddRowRule(rrCreateRowRuleedvUnAllocSOFulfillableEquals_true);
		// **** end autogenerated code ****
	}


	private void CreateRowRuleedvAllocSOSelectedEquals_true()
	{
		// Description: HighlightCreditHold
		// **** begin autogenerated code ****
		RuleAction erroredvAllocSO_RowAction = RuleAction.AddRowSettings(this.oTrans, "edvAllocSO", false, SettingStyle.Error);
		RuleAction[] ruleActions = new RuleAction[] {
				erroredvAllocSO_RowAction};
		// Create RowRule and add to the EpiDataView.
		RowRule rrCreateRowRuleedvAllocSOSelectedEquals_true = new RowRule("edvAllocSO.CreditHold", RuleCondition.Equals, true, ruleActions);
		((EpiDataView)(this.oTrans.EpiDataViews["edvAllocSO"])).AddRowRule(rrCreateRowRuleedvAllocSOSelectedEquals_true);
		// **** end autogenerated code ****
	}


	private void UD07_DataView_ListChanged(object sender, ListChangedEventArgs args)
	{
		// ** Argument Properties and Uses **
		// UD07_DataView[0]["FieldName"]
		// args.ListChangedType, args.NewIndex, args.OldIndex
		// ListChangedType.ItemAdded, ListChangedType.ItemChanged, ListChangedType.ItemDeleted, ListChangedType.ItemMoved, ListChangedType.Reset
		// Add Event Handler Code
           // EpiMessageBox.Show(UD07_DataView.Count.ToString());
         if(UD07_DataView.Count==0)
         {
             epiShapeShipped.Status = StatusTypes.OK;
             epiShapeShipped.EnabledCaption="";
         }
	}

	private void btnRetrieveAllocSOLD_Click(object sender, System.EventArgs args)
	{
		// ** Place Event Handling Code Here **
  
		// ** Place Event Handling Code Here **\	
		var row = ugAllocatedSO.ActiveRow;
		string orderNum = row.Cells["Order"].Value.ToString(), shipToName = row.Cells["Name"].Value.ToString(), address = row.Cells["Address"].Value.ToString();
		bool useOTS = Convert.ToBoolean(row.Cells["UseOTS"].Value);
		DateTime shipDate = Convert.ToDateTime(row.Cells["ShipDate"].Value);
		int shipbyTime = ConvertTimeToInt(row.Cells["Time"].Value.ToString()),JourneyNum= Convert.ToInt32(row.Cells["JourneyNum"].Value.ToString());
		LoadAllocSODtl(orderNum, shipToName, address, useOTS, shipDate, shipbyTime,JourneyNum);
	
	}
    
    

    private void SelectUnSelect(EpiUltraGrid eug,bool flag)
    { 
      this.oTrans.PushStatusText("Selecting/UnSelecting", true);
      foreach(var row in eug.Rows)
      {      
        row.Cells["Selected"].Value = flag;     
      }  
      this.oTrans.PopStatus();
    } 
	private void btnSelectAllAlocSO_Click(object sender, System.EventArgs args)
	{
		// ** Place Event Handling Code Here **
         SelectUnSelect(this.ugAllocatedSO,true);
	}

	private void btnUnSelectAllocSO_Click(object sender, System.EventArgs args)
	{
		// ** Place Event Handling Code Here **
         SelectUnSelect(this.ugAllocatedSO,false);
	}

	private void btnSelectUnAllocSO_Click(object sender, System.EventArgs args)
	{
		// ** Place Event Handling Code Here **
         SelectUnSelect(this.ugUnAllocatedSO,true);
	}

	private void btnUnSelectUnAllocSO_Click(object sender, System.EventArgs args)
	{
		// ** Place Event Handling Code Here **
         SelectUnSelect(this.ugUnAllocatedSO,false);
	}
    
   
}







