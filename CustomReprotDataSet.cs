    1 	// **************************************************
    2 	// Custom code for AC_CustomerStatementReportForm
    3 	// Created: 2/17/2016 12:42:57 PM
    4 	// **************************************************
    5 	using System;
    6 	using System.ComponentModel;
    7 	using System.Data;
    8 	using System.Diagnostics;
    9 	using System.Windows.Forms;
   10 	using Erp.Adapters;
   11 	using Erp.BO;
   12 	using Erp.UI;
   13 	using Ice.Lib;
   14 	using Ice.Adapters;
   15 	using Ice.Lib.Customization;
   16 	using Ice.Lib.ExtendedProps;
   17 	using Ice.Lib.Framework;
   18 	using Ice.Lib.Searches;
   19 	using Ice.UI.FormFunctions;
   20 	using Ice.Lib.Report;
   21 	using System.Reflection;
   22 	using Infragistics.Win;
   23 	using Infragistics.Win.UltraWinGrid;
   24 	using Infragistics.Win.UltraWinEditors;
   25 	using Ice.Proxy;
   26 	using Ice.Core;
   27 	using Ice.BO;
   28 	using System.Linq;
   29 	using System.Collections.Generic;
   30 	
   31 	public class Script
   32 	{
   33 	        // ** Wizard Insert Location - Do Not Remove 'Begin/End Wizard Added Module Level Variables' Comments! **
   34 	        // Begin Wizard Added Module Level Variables **
   35 	
   36 	        private EpiDataView edvReportParam;
   37 	        private EpiBaseAdapter oTrans_adpTempCustomer;
   38 	        // End Wizard Added Module Level Variables **
   39 	
   40 	        // Add Custom Module Level Variables Here **
   41 	        private EpiDataView ReportParmsView = null;
   42 	        private EpiDataView ClientListView = null;
   43 	        private EpiDataView CustGroupListView = null;
   44 	        private CustomerListDataSet dsClientList = new CustomerListDataSet();
   45 	
   46 	
   47 	        //private EpiUltraGrid eugCustGrup = null;
   48 	
   49 	        public void InitializeCustomCode()
   50 	        {
   51 	                // ** Wizard Insert Location - Do not delete 'Begin/End Wizard Added Variable Initialization' lines **
   52 	                // Begin Wizard Added Variable Initialization
   53 	
   54 	        //this.AC_CustomerStatementReportForm.BeforeToolClick += new Ice.Lib.Framework.BeforeToolClickEventHandler(this.AC_CustomerStatementReportForm_BeforeToolClick);
   55 	        this.edvReportParam = ((EpiDataView)(this.oTrans.EpiDataViews["ReportParam"]));
   56 	        this.edvReportParam.EpiViewNotification += new EpiViewNotification(this.edvReportParam_EpiViewNotification);
   57 	        this.AC_CustomerStatementReportForm.BeforeToolClick += new Ice.Lib.Framework.BeforeToolClickEventHandler(this.AC_CustomerStatementReportForm_BeforeToolClick);
   58 	        // End Wizard Added Variable Initialization
   59 	
   60 	        // Begin Wizard Added Custom Method Calls
   61 	        dsClientList = (CustomerListDataSet)typeof(Erp.UI.Rpt.AC_CustomerStatementReport.Transaction).GetField("dsCustomerList", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(oTrans);
   62 	
   63 	        this.ReportParmsView = ((EpiDataView)(this.oTrans.EpiDataViews["ReportParam"]));
   64 	        this.ClientListView = ((EpiDataView)(this.oTrans.EpiDataViews["customerList"]));
   65 	        this.ClientListView.dataView = new DataView(this.dsClientList.CustomerList);
   66 	        this.CustGroupListView = new EpiDataView();
   67 	
   68 	        oTrans.Add("TerritoryCustGroup", CustGroupListView);
   69 	        
   70 	        var morePages = false;
   71 	        var opts = new SearchOptions(SearchMode.AutoSearch);
   72 	        var custGrupAdapter = new CustGrupAdapter(oTrans);
   73 	        custGrupAdapter.BOConnect();
   74 	        
   75 	        opts.SelectMode = SelectMode.MultiSelect;
   76 	        opts.DataSetMode = DataSetMode.ListDataSet;
   77 	
   78 	        var custGrups = custGrupAdapter.GetRows(opts, out morePages);
   79 	        if (custGrups != null) {
   80 	                this.CustGroupListView.dataView = new DataView(custGrups.Tables[0]);
   81 	        }
   82 	        
   83 	        //this.eugCustGrup = ((EpiUltraGrid)csm.GetNativeControlReference("2c3c43e0-1a3b-43fb-b6fb-2a0aab6afd2a"));
   84 	
   85 	        this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
   86 	        
   87 	        //this.cboTerritory.RowSelected += new Infragistics.Win.UltraWinGrid.RowSelectedEventHandler(this.cboTerritory_RowSelected);
   88 	
   89 	        this.eugCustGrup.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
   90 	        this.eugCustGrup.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.None;
   91 	        this.eugCustGrup.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
   92 	        this.eugCustGrup.EpiAllowPaste = false;
   93 	        this.eugCustGrup.EpiAllowPasteInsert = false;
   94 	        this.eugCustGrup.IsEpiReadOnly = true;
   95 	        this.eugCustGrup.ReadOnly = true;
   96 	
   97 	        this.epiUltraGrid1.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
   98 	        this.epiUltraGrid1.EpiAllowPaste = false;
   99 	        this.epiUltraGrid1.EpiAllowPasteInsert = false;
  100 	        this.epiUltraGrid1.IsEpiReadOnly = true;
  101 	        this.epiUltraGrid1.ReadOnly = true;
  102 	
  103 	        /*
  104 	        this.epiUltraGridC1.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
  105 	        this.epiUltraGridC1.EpiAllowPaste = false;
  106 	        this.epiUltraGridC1.EpiAllowPasteInsert = false;
  107 	        this.epiUltraGridC1.IsEpiReadOnly = true;
  108 	        this.epiUltraGridC1.ReadOnly = true;
  109 	        */
  110 	
  111 	
  112 	        ////oTrans.loadEpiDataView(ClientListView, "ClientList", (DataTable) this.dsClientList.CustomerList, false);
  113 	        //loadParams();
  114 	        SetExtendedProperties();
  115 	        this.btnRefresh2.Click += new System.EventHandler(this.btnRefresh2_Click);
  116 	        // End Wizard Added Custom Method Calls
  117 	        }
  118 	
  119 	        public void DestroyCustomCode()
  120 	        {
  121 	                // ** Wizard Insert Location - Do not delete 'Begin/End Wizard Added Object Disposal' lines **
  122 	                // Begin Wizard Added Object Disposal
  123 	
  124 	        //this.cboTerritory.RowSelected -= new Infragistics.Win.UltraWinGrid.RowSelectedEventHandler(this.cboTerritory_RowSelected);
  125 	        //this.AC_CustomerStatementReportForm.BeforeToolClick -= new Ice.Lib.Framework.BeforeToolClickEventHandler(this.AC_CustomerStatementReportForm_BeforeToolClick);
  126 	        this.btnRefresh.Click -= new System.EventHandler(this.btnRefresh_Click);
  127 	        this.edvReportParam.EpiViewNotification -= new EpiViewNotification(this.edvReportParam_EpiViewNotification);
  128 	        this.edvReportParam = null;
  129 	        this.btnRefresh2.Click -= new System.EventHandler(this.btnRefresh2_Click);
  130 	        this.AC_CustomerStatementReportForm.BeforeToolClick -= new Ice.Lib.Framework.BeforeToolClickEventHandler(this.AC_CustomerStatementReportForm_BeforeToolClick);
  131 	        // End Wizard Added Object Disposal
  132 	
  133 	        // Begin Custom Code Disposal
  134 	        this.ReportParmsView = null;
  135 	        this.ClientListView = null;
  136 	        // End Custom Code Disposal
  137 	
  138 	        AC_CustomerStatementReportAdapter a = (AC_CustomerStatementReportAdapter)oTrans.PrimaryAdapter;
  139 	        }
  140 	
  141 	//        private void cboTerritory_RowSelected(object sender, Infragistics.Win.UltraWinGrid.RowSelectedEventArgs args)
  142 	//        {
  143 	//                //this.LookupTerritory("", "", cboTerritory.Value.ToString());
  144 	//        }
  145 	
  146 	//        private void AC_CustomerStatementReportForm_BeforeToolClick(object sender, Ice.Lib.Framework.BeforeToolClickEventArgs args)
  147 	//        {
  148 	//                switch (args.Tool.Key) {
  149 	//                case "DefSaveTool":
  150 	////MessageBox.Show(args.Tool.Key);
  151 	//                                saveParams();
  152 	//                        break;
  153 	//                case "ProcessTool":
  154 	////MessageBox.Show(args.Tool.Key);
  155 	//                        preSubmit();
  156 	//                        break;
  157 	//                case "ClearTool":
  158 	////MessageBox.Show(args.Tool.Key);
  159 	//                        clearFilters();
  160 	//                        break;
  161 	//                }
  162 	//        }
  163 	
  164 	//    private void clearFilters()
  165 	//    {
  166 	//      this.dsClientList.Clear();
  167 	//    }
  168 	
  169 	//        void preSubmit() {
  170 	//// Using FaxSubject to store the list of Customer's by Territory [TerritoryCustNumList]
  171 	//                this.ReportParmsView.dataView[this.ReportParmsView.Row]["CustNumList"] = (object) EpiReportFunctions.CreateList((DataTable) dsClientList.CustomerList, "CustNum");
  172 	//        }
  173 	
  174 	//        void saveParams() {
  175 	//           if (!this.ReportParmsView.HasRow || this.ReportParmsView.Row < 0)
  176 	//                    return;
  177 	//
  178 	//// Using FaxSubject to store the list of Customer's by Territory [TerritoryCustNumList]
  179 	//            this.ReportParmsView.dataView[this.ReportParmsView.Row]["CustNumList"] = (object) EpiReportFunctions.CreateList((DataTable) dsClientList.CustomerList, "CustNum");
  180 	//        }
  181 	
  182 	//        void loadParams() {
  183 	//            this.clearFilters();
  184 	//            this.LookupTerritory("", ReportParmsView.dataView[this.ReportParmsView.Row]["CustNumList"].ToString(), "", "");
  185 	//        }
  186 	
  187 	        private void btnRefresh_Click(object sender, System.EventArgs args)
  188 	        {
  189 	                string groupCodes = "";
  190 	                string territoryId = "";
  191 	
  192 	        foreach (var row in eugCustGrup.Selected.Rows)
  193 	        {
  194 	        if (groupCodes.Length > 0) 
  195 	                groupCodes += "~";
  196 	                groupCodes += string.Format("'{0}'", row.Cells["GroupCode"].Value.ToString());
  197 	        }
  198 	
  199 	        //territoryId = cboTerritory.Value == null ? "" : cboTerritory.Value.ToString();
  200 	        
  201 	        foreach(UltraGridRow r in this.cboTerritory.CheckedRows)
  202 	        {
  203 	                //string temp = r.Cells["TerritoryID"].Value.ToString();
  204 	                //MessageBox.Show(temp);
  205 	                //this.LookupTerritory("","",groupCodes,temp);
  206 	                territoryId += r.Cells["TerritoryID"].Value.ToString() + "|";
  207 	        }
  208 	
  209 	        this.LookupTerritory("", "", groupCodes, territoryId);
  210 	        }
  211 	
  212 	        private bool LookupTerritory(string tableID, string loadString, string groupCode, string territoryId)
  213 	    {
  214 	      string str = "";
  215 	      bool flag = false;
  216 	          string[] multiTerritory = territoryId.Split('|');
  217 	      try
  218 	      {
  219 	        if (tableID.Length == 0 && loadString.Length == 0 && groupCode.Length == 0 && territoryId.Length == 0)
  220 	          return true;
  221 	
  222 	        if (tableID.Length > 0)
  223 	        {
  224 	            str = "CustID = '" + tableID + "' ";
  225 	        }
  226 	        else
  227 	        {
  228 	        if (groupCode.Length > 0 || territoryId.Length > 0) {
  229 	                groupCode = groupCode.Replace("~", ",");
  230 	                if (territoryId.Length > 0) {
  231 	                        if(multiTerritory.Length > 1)
  232 	                        {
  233 	                                str += "TerritoryID in (";
  234 	                                for(int i = 0; i < multiTerritory.Length; i++)
  235 	                                {
  236 	                                        /*
  237 	                                        if(multiTerritory[i] == "")
  238 	                                        {
  239 	                                                continue;
  240 	                                        }
  241 	                                        */
  242 	                                        if(i == multiTerritory.Length - 1)
  243 	                                        {
  244 	                                                str += "'" + multiTerritory[i] + "')";
  245 	                                        } else {
  246 	                                                str += "'" + multiTerritory[i] + "',";
  247 	                                        }
  248 	                                }
  249 	                        } else {
  250 	                                //str += string.Format("TerritoryID='{0}'", territoryId);
  251 	                                str += string.Format("TerritoryID='{0}'", multiTerritory[0]);
  252 	                        }
  253 	                }
  254 	                if (groupCode.Length > 0) {
  255 	                        if (str.Length > 0)
  256 	                                str += " and ";
  257 	                        str += string.Format("GroupCode in ({0})", groupCode);
  258 	                }
  259 	                flag = true;
  260 	            } else {
  261 	                    loadString = loadString.Replace("~", ",");
  262 	                    str = "lookup(CustID, '" + loadString.Replace("'", "''") + "') > 0";
  263 	        flag = true;
  264 	        }
  265 	        }
  266 	
  267 	        //MessageBox.Show(str);
  268 	        SearchOptions opts = new SearchOptions(SearchMode.AutoSearch);
  269 	        opts.SelectMode = SelectMode.MultiSelect;
  270 	        opts.DataSetMode = DataSetMode.ListDataSet;
  271 	        opts.PreLoadSearchFilter = str;
  272 	
  273 	        var obj = ProcessCaller.LaunchSearch((object) oTrans, "CustomerAdapter", opts);
  274 	        if (obj is DataSet)
  275 	        {
  276 	          DataSet dataSet = (DataSet) obj;
  277 	          if (dataSet.Tables[0].Rows.Count <= 0)
  278 	            return false;
  279 	          if (flag)
  280 	          {
  281 	            //this.TranBeginEdit();
  282 	            dsClientList.Clear();
  283 	            dsClientList.Merge(dataSet, true, MissingSchemaAction.Ignore);
  284 	        /*
  285 	        string columnName = "";
  286 	        foreach(DataColumn dc in dsClientList.Tables["CustomerList"].Columns)
  287 	        {
  288 	                columnName += dc.ColumnName + " ";
  289 	        }
  290 	        MessageBox.Show(columnName);
  291 	        */
  292 	            //this.TranEndEdit(this.clientListView.dataView[this.clientListView.dataView.Count - 1]);
  293 	            this.ClientListView.Notify(new EpiNotifyArgs((object) this, this.ClientListView.dataView.Count - 1, EpiTransaction.NotifyType.Initialize));
  294 	            this.ReportParmsView.Notify(new EpiNotifyArgs((object) this, this.ReportParmsView.Row, this.ReportParmsView.Column));
  295 	          }
  296 	          else if (ClientListView.HasRow)
  297 	          {
  298 	            //this.TranBeginEdit();
  299 	            foreach (DataColumn dataColumn in (InternalDataCollectionBase) dataSet.Tables[0].Columns)
  300 	            {
  301 	              if (dsClientList.CustomerList.Columns.Contains(dataColumn.ColumnName) && dataColumn.ColumnName != dsClientList.CustomerList.PrimaryKey[0].ColumnName)
  302 	                      ClientListView.dataView[ClientListView.Row][dataColumn.ColumnName] = dataSet.Tables[0].Rows[0][dataColumn.ColumnName];
  303 	            }
  304 	            //this.TranEndEdit(this.clientListView.dataView[this.clientListView.Row]);
  305 	          }
  306 	        }
  307 	      }
  308 	      catch (Exception ex)
  309 	      {
  310 	        ExceptionBox.Show(ex);
  311 	        return false;
  312 	      }
  313 	      return true;
  314 	    }
  315 	
  316 	        private void SetExtendedProperties()
  317 	        {
  318 	                // Begin Wizard Added EpiDataView Initialization
  319 	                EpiDataView edvReportParam = ((EpiDataView)(this.oTrans.EpiDataViews["ReportParam"]));
  320 	                // End Wizard Added EpiDataView Initialization
  321 	
  322 	        // Begin Wizard Added Conditional Block
  323 	        if (edvReportParam.dataView.Table.Columns.Contains("Ageing"))
  324 	        {
  325 	                // Begin Wizard Added ExtendedProperty Settings: edvReportParam-Ageing
  326 	        //        edvReportParam.dataView.Table.Columns["Ageing"].ExtendedProperties["ReadOnly"] = true;
  327 	                // End Wizard Added ExtendedProperty Settings: edvReportParam-Ageing
  328 	        }
  329 	        // End Wizard Added Conditional Block
  330 	        }
  331 	
  332 	        private void edvReportParam_EpiViewNotification(EpiDataView view, EpiNotifyArgs args)
  333 	        {
  334 	                // ** Argument Properties and Uses **
  335 	                // view.dataView[args.Row]["FieldName"]
  336 	                // args.Row, args.Column, args.Sender, args.NotifyType
  337 	                // NotifyType.Initialize, NotifyType.AddRow, NotifyType.DeleteRow, NotifyType.InitLastView, NotifyType.InitAndResetTreeNodes
  338 	                if ((args.NotifyType == EpiTransaction.NotifyType.Initialize))
  339 	                {
  340 	                        if ((args.Row > -1))
  341 	                        {
  342 	                                //view.dataView[args.Row].BeginEdit();
  343 	                                //view.dataView[args.Row]["Ageing"] = true;
  344 	                                //view.dataView[args.Row].EndEdit();
  345 	                                view.dataView[args.Row].BeginEdit();
  346 	                                view.dataView[args.Row]["Caption"] = "SLS";
  347 	                                view.dataView[args.Row].EndEdit();
  348 	                                view.dataView[args.Row].BeginEdit();
  349 	                                view.dataView[args.Row]["Remittance"] = true;
  350 	                                view.dataView[args.Row].EndEdit();
  351 	                        }
  352 	                }
  353 	        }
  354 	
  355 	
  356 	        private void AC_CustomerStatementReportForm_Load(object sender, EventArgs args)
  357 	        {
  358 	                // Add Event Handler Code
  359 	                MultiTerritory();
  360 	        }
  361 	
  362 	
  363 	        private void MultiTerritory()
  364 	        {
  365 	                UltraGridColumn checkBox = this.cboTerritory.DisplayLayout.Bands[0].Columns.Add();
  366 	                checkBox.Key = "Selected";
  367 	                checkBox.Header.Caption = string.Empty;
  368 	                //This allows end users to select / unselect ALL items
  369 	                checkBox.Header.CheckBoxVisibility = HeaderCheckBoxVisibility.Always;
  370 	                checkBox.DataType = typeof(bool);
  371 	                //Move the checkbox column to the first position.
  372 	                checkBox.Header.VisiblePosition = 0;
  373 	                this.cboTerritory.CheckedListSettings.CheckStateMember = "Selected";
  374 	                this.cboTerritory.CheckedListSettings.EditorValueSource = Infragistics.Win.EditorWithComboValueSource.CheckedItems;
  375 	                // Set up the control to use a custom list delimiter
  376 	                this.cboTerritory.CheckedListSettings.ListSeparator = " / ";
  377 	                // Set ItemCheckArea to Item, so that clicking directly on an item also checks the item
  378 	                this.cboTerritory.CheckedListSettings.ItemCheckArea = Infragistics.Win.ItemCheckArea.Item;
  379 	                this.cboTerritory.DisplayMember = "TerritoryDesc";
  380 	                this.cboTerritory.ValueMember = "TerritoryID";
  381 	
  382 	        string[] fields = new string[] {"Selected","TerritoryDesc"};
  383 	        this.cboTerritory.EpiColumns=fields;
  384 	        }
  385 	
  386 	        private void btnRefresh2_Click(object sender, System.EventArgs args)
  387 	        {
  388 	
  389 	        // ** Place Event Handling Code Here **
  390 	
  391 	        string territory, CTCD;
  392 	        territory = CTCD = "";
  393 	        bool flag = false;
  394 	
  395 	        if (!string.IsNullOrEmpty(this.baqComboC1.Text))
  396 	        {
  397 	                CTCD = this.baqComboC1.SelectedRow.Cells["AC_SegCT_SegCTCD"].Value.ToString();
  398 	        }
  399 	
  400 	        if (!string.IsNullOrEmpty(this.epiCombo1.Text))
  401 	        {
  402 	                territory = this.epiCombo1.Value.ToString();
  403 	        }
  404 	
  405 	        DynamicQueryAdapter dqa = new DynamicQueryAdapter(this.oTrans);
  406 	        dqa.BOConnect();
  407 	        
  408 	        QueryExecutionDataSet qeds = dqa.GetQueryExecutionParametersByID("pbs_CareType");
  409 	        qeds.ExecutionParameter.Clear();
  410 	
  411 	        if(!string.IsNullOrEmpty(CTCD))          qeds.ExecutionParameter.AddExecutionParameterRow("caretype", CTCD, "nvarchar", false, Guid.NewGuid(), "A");
  412 	        if(!string.IsNullOrEmpty(territory)) qeds.ExecutionParameter.AddExecutionParameterRow("territory", territory, "nvarchar", false, Guid.NewGuid(), "A");
  413 	
  414 	        dqa.ExecuteByID("pbs_CareType", qeds);
  415 	        var result = dqa.QueryResults;
  416 	
  417 	        getCust(result.Tables[0].Rows);
  418 	
  419 	        }
  420 	
  421 	        private void getCust(DataRowCollection rows)
  422 	        {
  423 	                dsClientList.Clear();
  424 	                
  425 	                using(DynamicQueryAdapter dqa = new DynamicQueryAdapter(this.oTrans))
  426 	                {
  427 	                        dqa.BOConnect();
  428 	                        QueryExecutionDataSet qeds = dqa.GetQueryExecutionParametersByID("pbs_CustAccounts");
  429 	
  430 	        foreach(DataRow row in rows)
  431 	        {
  432 	                string custID = (string)row["Customer_CustID"];
  433 	        
  434 	                qeds.ExecutionParameter.Clear();
  435 	
  436 	        qeds.ExecutionParameter.AddExecutionParameterRow("custid", custID+"%", "nvarchar", false, Guid.NewGuid(), "A");
  437 	        dqa.ExecuteByID("pbs_CustAccounts", qeds);
  438 	        DataTable result = dqa.QueryResults.Tables["Results"];
  439 	
  440 	        foreach(DataRow account in result.Rows)
  441 	        {
  442 	                dsClientList.CustomerList.Rows.Add();
  443 	                int currentIndex = dsClientList.CustomerList.Rows.Count-1;
  444 	                Erp.BO.CustomerListDataSet.CustomerListRow customerRow = dsClientList.CustomerList[currentIndex];
  445 	
  446 	        customerRow.Company         = (string)account["Customer_Company"];
  447 	        customerRow.CustID          = (string)account["Customer_CustID"];
  448 	        customerRow.Name                = (string)account["Customer_Name"];
  449 	        customerRow.GroupCode   = (string)account["Customer_GroupCode"];
  450 	        customerRow.ResaleID        = (string)account["Customer_ResaleID"];
  451 	        customerRow.TerritoryID = (string)account["Customer_TerritoryID"];
  452 	        }
  453 	
  454 	//                                for(int i = 0; i < result.Rows.Count; ++i)
  455 	//                                {
  456 	//                                        dsClientList.CustomerList.Rows.Add();
  457 	//                                        int currentIndex = dsClientList.CustomerList.Rows.Count-1;
  458 	//        
  459 	//                                        dsClientList.CustomerList[currentIndex].Company = customerAdapter.CustomerData.Customer[i].Company;
  460 	//                                        dsClientList.CustomerList[currentIndex].CustID = customerAdapter.CustomerData.Customer[i].CustID;
  461 	//                                        dsClientList.CustomerList[currentIndex].Name = customerAdapter.CustomerData.Customer[i].Name;
  462 	//                                        dsClientList.CustomerList[currentIndex].GroupCode = customerAdapter.CustomerData.Customer[i].GroupCode;
  463 	//                                        dsClientList.CustomerList[currentIndex].ResaleID = customerAdapter.CustomerData.Customer[i].ResaleID;
  464 	//                                        dsClientList.CustomerList[currentIndex].TerritoryID = customerAdapter.CustomerData.Customer[i].TerritoryID;
  465 	//                                }
  466 	        
  467 	                }
  468 	                }
  469 	                
  470 	                this.ClientListView.Notify(new EpiNotifyArgs((object) this, this.ClientListView.dataView.Count - 1, EpiTransaction.NotifyType.Initialize));
  471 	                this.ReportParmsView.Notify(new EpiNotifyArgs((object) this, this.ReportParmsView.Row, this.ReportParmsView.Column));
  472 	                
  473 	        }
  474 	
  475 	        private void AC_CustomerStatementReportForm_BeforeToolClick(object sender, Ice.Lib.Framework.BeforeToolClickEventArgs args)
  476 	        {
  477 	                if(args.Tool.Key == "PrintPreviewTool" ||
  478 	                   args.Tool.Key == "GenerateTool"     ||
  479 	                   args.Tool.Key == "EmailTool"        ||
  480 	                   args.Tool.Key == "PrintClientTool"  ||
  481 	                   args.Tool.Key == "PrintServerTool")
  482 	                {
  483 	                        EpiDataView clientList   = (EpiDataView)this.oTrans.EpiDataViews["clientList"];
  484 	                        EpiDataView CustGrpList  = (EpiDataView)this.oTrans.EpiDataViews["CustGrpList"];
  485 	                        EpiDataView customerList = (EpiDataView)this.oTrans.EpiDataViews["customerList"];
  486 	            EpiDataView ReportParam = (EpiDataView)this.oTrans.EpiDataViews["ReportParam"];
  487 	            int ReportStyle = Convert.ToInt32(ReportParam.dataView[ReportParam.Row]["ReportStyleNum"]);
  488 	            var edvCallContextBpmData = ((EpiDataView)(this.oTrans.EpiDataViews["CallContextBpmData"]));
  489 	
  490 	
  491 	
  492 	//                        dsClientList.Clear();
  493 	        
  494 	        if(clientList.dataView.Count < 1 && CustGrpList.dataView.Count < 1 && customerList.dataView.Count < 1)
  495 	        {
  496 	                var response = MessageBox.Show("You have not selected any parameters - all clients have been selected. Are you sure you want to proceed?","Warning", MessageBoxButtons.YesNo);
  497 	            throw new Exception("Submission cancelled (This is not an error. Please don't worry about it)");
  498 	                /*                                
  499 	                if(response == DialogResult.No)
  500 	                {
  501 	                        Erp.BO.CustomerListDataSet test = new Erp.BO.CustomerListDataSet();
  502 	                        DataSet ds = new DataSet();
  503 	                        ds                 = (DataSet) test;                                
  504 	                        var row    = ds.Tables[0].NewRow();
  505 	                    row["CustID"]  ="Dummy";
  506 	                    row["Company"] = "10BW";
  507 	                    row["CustNum"] = 0;
  508 	                    row["Name"]    = "Dummy Record";
  509 	                    ds.Tables[0].Rows.Add(row);
  510 	                dsClientList.Merge( ds, true, MissingSchemaAction.Ignore);
  511 	                return;
  512 	                //throw new Exception("Submission cancelled (This is not an error. Please don't worry about it)");
  513 	                //return;
  514 	                }
  515 	                */
  516 	                }
  517 	            else if(ReportStyle == 1003)
  518 	            {   
  519 	             try
  520 	               {
  521 	                Ice.Proxy.Lib.BOReaderImpl _bor = WCFServiceSupport.CreateImpl<Ice.Proxy.Lib.BOReaderImpl>((Ice.Core.Session)oTrans.Session, Epicor.ServiceModel.Channels.ImplBase<Ice.Contracts.BOReaderSvcContract>.UriPath);
  522 	                string agentID = string.Empty;
  523 	                using (var aSA = new SysAgentAdapter(oTrans))
  524 	                     {
  525 	                               aSA.BOConnect();
  526 	                               aSA.GetDefaultTaskAgentID(out agentID);
  527 	                               if (string.IsNullOrEmpty(agentID))
  528 	                              { 
  529 	                                agentID = "SystemTaskAgent";
  530 	                              }
  531 	                      } 
  532 	                List<string> custNums = new List<string>();
  533 	                List<string> NewCustNumList = new List<string>();
  534 	                if(customerList.dataView.Table.Rows.Count > 0 )
  535 	                {
  536 	                        foreach(DataRow row in customerList.dataView.Table.Rows )
  537 	                        {
  538 	                            custNums.Add(row["CustNum"].ToString());
  539 	                        }
  540 	                 }else
  541 	                  {  
  542 	                        foreach(DataRow row in clientList.dataView.Table.Rows )
  543 	                        {
  544 	                            custNums.Add(row["CustNum"].ToString());
  545 	                        }
  546 	                   }
  547 	                if(custNums.Count > 0)
  548 	                  {
  549 	                           String whereClause = String.Format("CustNum IN ({0})",string.Join(",",custNums));
  550 	                           
  551 	                           DataSet custds = _bor.GetRows("Erp:BO:Customer",whereClause,"CustNum,EmailAddress");
  552 	                           if(custds.Tables[0].Rows.Count > 0)
  553 	                           {
  554 	                             foreach( DataRow _row in custds.Tables[0].Rows)
  555 	                             {
  556 	                               if(string.IsNullOrEmpty(_row["EmailAddress"].ToString()))
  557 	                               {
  558 	                                  NewCustNumList.Add(_row["CustNum"].ToString());
  559 	                               }
  560 	                             }
  561 	                           }
  562 	                 }
  563 	                 
  564 	                var _CustNums =  string.Join("~",NewCustNumList);
  565 	               if(!string.IsNullOrEmpty(_CustNums))
  566 	               {
  567 	                 try
  568 	                    { 
  569 	                       
  570 	                          var custRptSvc = Ice.Lib.Framework.WCFServiceSupport.CreateImpl<Erp.Proxy.Rpt.AC_CustomerStatementRptImpl>(oTrans.CoreSession, Erp.Proxy.Rpt.AC_CustomerStatementRptImpl.UriPath);
  571 	                          custRptSvc.GetRptArchiveList();
  572 	                          var ds = custRptSvc.GetNewParameters();
  573 	                          custRptSvc.GetDefaults( ds);
  574 	                          var row = ds.AC_CustomerStatementParam[0];
  575 	                          if(row == null)
  576 	                  {        
  577 	                                return;
  578 	                              } 
  579 	                                 else
  580 	                             {
  581 	                       
  582 	                           
  583 	                          row.CustNumList = _CustNums;
  584 	                      row.AgeBy = ReportParam.dataView[ReportParam.Row]["AgeBy"].ToString();
  585 	                      row.Caption = ReportParam.dataView[ReportParam.Row]["Caption"].ToString();
  586 	                          row.BeginDate = (DateTime)ReportParam.dataView[ReportParam.Row]["BeginDate"];
  587 	                          row.EndDate = (DateTime)ReportParam.dataView[ReportParam.Row]["EndDate"];
  588 	                          row.AddressTo = ReportParam.dataView[ReportParam.Row]["AddressTo"].ToString();
  589 	                          row.Ageing = (Boolean)ReportParam.dataView[ReportParam.Row]["Ageing"];
  590 	                          row.Remittance = (Boolean)ReportParam.dataView[ReportParam.Row]["Remittance"];
  591 	              row.AutoAction = "SSRSPREVIEW";
  592 	              row.AgentSchedNum = 0;
  593 	              row.AgentID = agentID;
  594 	              row.ReportStyleNum = 1006;
  595 	              row.WorkstationID = Ice.Lib.Report.EpiReportFunctions.GetWorkStationID((Ice.Core.Session)oTrans.Session);
  596 	              row.DateFormat ="d/mm/yyyy";
  597 	              row.NumericFormat = ", .";
  598 	              row.ReportCurrencyCode ="AUD";
  599 	              row.ReportCultureCode="en-AU";
  600 	              row.SSRSRenderFormat="PDF";
  601 	              row.PrintReportParameters=false;
  602 	              row.SSRSEnableRouting=false;
  603 	              row.DesignMode=false;
  604 	             
  605 	                     }
  606 	                         custRptSvc.SubmitToAgent(ds,agentID,0,0,"Erp.UIRpt.AC_CustomerStatementReport");
  607 	                    }catch(Exception ex)
  608 	                     {
  609 	                       ExceptionBox.Show(ex);
  610 	                     }
  611 	            
  612 	                 
  613 	               }
  614 	              
  615 	               
  616 	            }catch(Exception ex)
  617 	                     {
  618 	                       ExceptionBox.Show(ex);
  619 	                     }
  620 	           }
  621 	        }
  622 	        }
  623 	}
