/*
Company - Precise Business Solutions
Author - Meharban Singh Waraich
Date -
*/

var xRows = ttResults.ToList();

var orderAllocSvc = ServiceRenderer.GetService<Erp.Contracts.OrderAllocSvcContract>(this.Db);
Erp.Tablesets.OrderAllocListTableset orderAllocListTableset = new Erp.Tablesets.OrderAllocListTableset();
Erp.Tablesets.OrderAllocTableset orderAllocDS = new Erp.Tablesets.OrderAllocTableset();


int _mtlSeq = 0;
Action<Erp.Tablesets.OrderAllocTableset, Erp.Tablesets.OrderAllocRow> GenAllocTableset = (orderAllocTableset, row) =>
{
  var newRow = orderAllocTableset.OrderAlloc.NewRow();
  newRow["Company"] = row.Company;
  newRow["OrderNum"] = row.OrderNum;
  newRow["OrderLine"] = row.OrderLine;
  newRow["OrderRelNum"] = row.OrderRelNum;
  newRow["OrderNumLineRel"] = row.OrderNumLineRel;
  newRow["ReqDate"] = row.ReqDate;
  newRow["PartNum"] = row.PartNum;
  newRow["WarehouseCode"] = row.WarehouseCode;
  newRow["SelectForPicking"] = true;
  newRow["CustID"] = row.CustID;
  newRow["CustName"] = row.CustName;
  newRow["ShipToNum"] = row.ShipToNum;
  newRow["JobNum"] = row.JobNum;
  newRow["AssemblySeq"] = row.AssemblySeq;
  newRow["MtlSeq"] = row.MtlSeq;
  newRow["JobAssemblyMtl"] = row.JobAssemblyMtl;
  newRow["TFOrdNum"] = row.TFOrdNum;
  newRow["TFOrdLine"] = row.TFOrdLine;
  newRow["TFOrdNumTFOrdLine"] = row.TFOrdNumTFOrdLine;
  newRow["DemandType"] = row.DemandType;
  newRow["DemandTypeDesc"] = row.DemandTypeDesc;
  newRow["SysRowID"] = new Guid();
  newRow["DisplaySeq"] = row.DisplaySeq;
  newRow["FromPlant"] = row.TFOrdFromPlant;
  newRow["JobType"] = row.JobStatus;
  newRow["Plant"] = row.Plant;
  //newRow["PriorityCode"] = row.tf;
  newRow["RequestDate"] = row.ReqDate;
  newRow["ToPlant"] = row.TFOrdToPlant;
  newRow["ReadyToFulfill"] = false;
  newRow["PartDescription"] = row.PartDescription;
  newRow["RowMod"] = "A";
  orderAllocTableset.OrderAlloc.Add(newRow);
  
  _mtlSeq = row.MtlSeq;
};

Action<Erp.Tablesets.SlimOrderAllocTableset, Erp.Tablesets.OrderAllocRow> GenSlimOrderAllocDataSet = (slimOrderAllocDataSet, row) =>
{
  var newRow = slimOrderAllocDataSet.SlimOrderAlloc.NewRow();
  newRow["AssemblySeq"] = row.AssemblySeq;
  newRow["DemandType"] = row.DemandType;
  newRow["FulfillmentSeq"] = row.FulfillmentSeq;
  newRow["JobNum"] = row.JobNum;
  newRow["MtlSeq"] = row.MtlSeq;
  newRow["OrderLine"] = row.OrderLine;
  newRow["OrderNum"] = row.OrderNum;
  newRow["OrderRelNum"] = row.OrderRelNum;
  newRow["SelectedForAction"] = true;
  newRow["TFOrdLine"] =  row.TFOrdLine;
  newRow["TFOrdNum"] = row.TFOrdNum;
  newRow["RowMod"] = "A";
  slimOrderAllocDataSet.SlimOrderAlloc.Add(newRow);
};

Action<string> PrintPickSlip = (mtlSeq) =>
{
  var mtlQueueRptSvc = ServiceRenderer.GetService<Erp.Contracts.MtlQueueRptSvcContract>(this.Db);
  Erp.Tablesets.MtlQueueRptTableset mtlQueueRptTableset = mtlQueueRptSvc.GetNewParameters();
  
  var newRow = mtlQueueRptTableset.MtlQueueRptParam[0];
  /*
  newRow["Company"] = "";
  newRow["Plant"] = "";
  newRow["EmpID"] = "";
  newRow["MtlQueueSeq"] = "";
  newRow["PageBreak"] = "OrderNum";
  newRow["SortBy"] = "WhseBin";
  newRow["ReleaseForPickingSeq"] = ReleaseForPickingSeq;
  //newRow["OrderLineReleaseList"] = "";
  //newRow["BarCodes"] = true;
  newRow["SysRowID"] = Guid.NewGuid();
  newRow["AutoAction"] = "SSRSPREVIEW";
  //newRow["PrinterName"] = "";
 // newRow["AgentSchedNum"] = "";
  newRow["AgentID"] = "SystemAgent";
  newRow["AgentTaskNum"] = 0;
  newRow["RecurringTask"] = false;
  //newRow["RptPageSettings"] = "";
  //newRow["RptPrinterSettings"] = "";
  //newRow["RptVersion"] = "";
  //newRow["ReportStyleNum"] = 2;
  newRow["WorkstationID"] = Session.ClientComputerName.ToString() +" "+ Session.ClientTerminalID.ToString();
  //newRow["TaskNote"] = "";
  newRow["ArchiveCode"] = 1;
  newRow["DateFormat"] = "d/mm/yyyy";
  newRow["NumericFormat"] = ",.";
  newRow["SSRSRenderFormat"] = "PDF";
  newRow["ReportCurrencyCode"] = "AUD"; 
  newRow["ReportCultureCode"] = "en-AU";
  newRow["SSRSRenderFormat"] = "PDF";
  newRow["PrintReportParameters"] = false;
  newRow["SSRSEnableRouting"] = true;
  newRow["DesignMode"] = false;
  newRow["RowMod"] = "A";]
  */
        newRow["Company"] = "";
        newRow["Plant"] = "" ; // callContextClient.CurrentPlant;
        newRow["EmpID"] = "";
        newRow["MtlQueueSeq"] = mtlSeq;
                  //<ReleaseForPickingSeq>5536080</ReleaseForPickingSeq>
        newRow["PageBreak"] = "OrderNum";
        newRow["SortBy"] = "WhseBin";
        newRow["ReleaseForPickingSeq"] = 0;//ReleaseForPickingSeq;
        newRow["SysRowID"] = Guid.NewGuid();
        newRow["AutoAction"] = "SSRSPREVIEW";
        newRow["AgentID"] = "SystemAgent";
        newRow["AgentTaskNum"] = 0;
        newRow["RecurringTask"] = false;
        newRow["ReportStyleNum"] = 1001;
        newRow["WorkstationID"] = Session.ClientComputerName.ToString() +" "+ Session.ClientTerminalID.ToString();
        newRow["ArchiveCode"] = 1;
        newRow["DateFormat"] = "d/mm/yyyy";
        newRow["NumericFormat"] = ",.";
        newRow["SSRSRenderFormat"] = "PDF";
        newRow["ReportCurrencyCode"] = "AUD";
        newRow["ReportCultureCode"] = "en-AU";
        newRow["SSRSRenderFormat"] = "PDF";
        newRow["PrintReportParameters"] = false;
       // newRow["SSRSEnableRouting"] = true;
        newRow["DesignMode"] = false;
        newRow["RowMod"] = "A";
        
  //var newRptStyleRow = mtlQueueRptTableset.ReportStyle[0];
  //newRptStyleRow["ReportID"] = "MtlQueue";
  //newRptStyleRow["StyleNum"] = 1003;
  //newRptStyleRow["StyleDescription"] = "Standard - SSRS";
  //newRptStyleRow["RptTypeID"] = "SSRS";
  // newRptStyleRow["PrintProgram"] = "reports/MaterialQueue/MaterialQueue";
  //newRptStyleRow["RptDefID"] = "MtlQueue";
  //newRptStyleRow["ServerNum"] = 0;
  //newRptStyleRow["OutputLocation"] = "Database";
  //newRptStyleRow["SysRevID"] = "1269816271";
  //newRptStyleRow["SysRowID"] = new Guid("1be078ad-8547-466e-94c1-9f9d0117a043");
  //newRptStyleRow["SystemFlag"] = true;
  //newRptStyleRow["StructuredOutputEnabled"] = false;
  //newRptStyleRow["RequireSubmissionID"] = false;
  //newRptStyleRow["AllowResetAfterSubmit"] = false;
  //newRptStyleRow["HasBAQOrEI"] = false;
  //newRptStyleRow["BitFlag"] = 0;
  //newRptStyleRow["RowMod"] = "A";
  
  mtlQueueRptSvc.SubmitToAgent(mtlQueueRptTableset, "SystemAgent", 0, 0, "Erp.UIRpt.MtlQueueRpt");
};

Action<int> UpdateInPickingStatusOrderRel = (releaseForPickingSeq) =>
{

  var mtlQueueReferences = (from r in Db.MtlQueue
                            where r.Company == Session.CompanyID && r.ReleaseForPickingSeq == releaseForPickingSeq
                            select new {r.Reference, r.ReferencePrefix}).ToList();
  
  using (var txScope = IceContext.CreateDefaultTransactionScope())
  {
    foreach(var reference in mtlQueueReferences)
    {
      if(reference.ReferencePrefix.Equals("SO:"))
      {
        List<int> OLR = reference.Reference.Split('/').Select(System.Int32.Parse).ToList();
        int orderNum = OLR[0];
        int orderLine = OLR[1];
        int orderLineRel = OLR[2];
        
        foreach(var OrderRel in (from row in Db.OrderRel.With(LockHint.UpdLock) where
                row.Company == Session.CompanyID && row.OrderNum == orderNum && row.OrderLine == orderLine && row.OrderRelNum == orderLineRel
                select row))
        {
          OrderRel.pbsPickedPrinted_c = true;
        }
      }
      else
      {
        List<int> TOL = reference.Reference.Split('/').Select(System.Int32.Parse).ToList();
        string tfOrderNum = TOL[0].ToString();
        int tfOrderLine = TOL[1];
        
        foreach(var TFOrdDtl in (from row in Db.TFOrdDtl.With(LockHint.UpdLock) where
                row.Company == Session.CompanyID && row.TFOrdNum == tfOrderNum && row.TFOrdLine == tfOrderLine 
                select row))
        {
          TFOrdDtl.pbsPickedPrinted_c = true;
        }
      }
    }
    Db.Validate();
    txScope.Complete();
  }
};

if(xRows != null)
{
           
  bool pickSlipPrint = false, TOFlag = false, SOFlag = false;
  
  
  string whereClausePartDtl = string.Empty, whereClauseTFOrdDtl = string.Empty, whereClauseTFOrdHed = string.Empty, whereClausePartAlloc = "NoFilter,NoFilter,NoFilter,NoFilter"
  ,whereClauseWaveOrder = string.Empty;
  
  bool morePages, opCalcFulfillOnSearch, opFWBFulfillFromDemandWhseOnly, opFWBLimitedRefresh;
        int pageSize = 0;
        int absolutePage = 0;
        
  int Journey = xRows[0].OrderRel4_PbsJourneyNum_c;
  string waveWhereClause = string.Empty, orderHedWhereClause = string.Empty, orderDtlWhereClause = string.Empty, orderRelWhereClause = string.Empty, customerWhereClause = string.Empty
        ,partAllocWhereClause = "NoFilter,NoFilter,NoFilter,NoFilter", countryWhereClause = string.Empty, shipToWhereClause = string.Empty, creditHoldClause = "NoFilter", i_SortByOrder = string.Empty,i_SortByWarehouse = string.Empty, i_SortByAllocation = string.Empty, NO_Company = string.Empty;
  using(var txScope = Ice.IceContext.CreateDefaultTransactionScope())
  {
    var splitCollection = xRows.Where(x => x.Calculated_OTSHeader != true).GroupBy(x => x.OrderRel4_UseOTS);
    var otsHeaderCollection = xRows.Where(x => x.Calculated_OTSHeader == true);
    foreach(var collection in splitCollection)
    {
        string whereClause = string.Empty, reqDateWhereClause = string.Empty, shipNameWhereClause = string.Empty, shipAddrWhereClause = string.Empty, OSTShipNameWhereClause = string.Empty, OSTShipAddrWhereClause = string.Empty,
        toWhereClause = string.Empty, toreqDateWhereClause = string.Empty, shipbyTimeWhereClause = string.Empty;
        
        waveWhereClause = string.Empty; orderHedWhereClause = string.Empty; orderDtlWhereClause = string.Empty; orderRelWhereClause = string.Empty; customerWhereClause = string.Empty
        ;partAllocWhereClause = "NoFilter,NoFilter,NoFilter,NoFilter"; countryWhereClause = string.Empty; shipToWhereClause = string.Empty; creditHoldClause = "NoFilter"; i_SortByOrder = string.Empty
        ;i_SortByWarehouse = string.Empty; i_SortByAllocation = string.Empty; NO_Company = string.Empty;
                
          whereClause += string.Format( "OrderRel.OrderNum IN (");
          reqDateWhereClause += string.Format("OrderRel.ReqDate IN (");
          shipbyTimeWhereClause += string.Format("OrderRel.ShipbyTime IN(");
          //orderHedWhereClause += string.Format("OrderHed.OrderNum IN(");
                  
          shipNameWhereClause += string.Format(" ShipTo.Name IN(");
          shipAddrWhereClause += string.Format(" ShipTo.Address1 IN(");
          
          OSTShipNameWhereClause += string.Format("OrderRel.OTSName IN (");
          OSTShipAddrWhereClause += string.Format("OrderRel.OTSAddress1 IN (");
          
          foreach(var row in collection)
          {
            whereClause += string.Format("'{0}',", row.OrderHed_OrderNum);
            reqDateWhereClause += string.Format("'{0}',", row.OrderRel4_ReqDate.Value.ToString("MM/dd/yyyy"));
            shipbyTimeWhereClause += string.Format("'{0}',", row.OrderRel4_ShipbyTime);
            //orderHedWhereClause += string.Format("'{0}',", row.Calculated_ShipToName);
            
            if(collection.Key)
            {
              OSTShipNameWhereClause += string.Format("'{0}',",row.Calculated_ShipToName);
              OSTShipAddrWhereClause += string.Format("'{0}',",row.Calculated_ShipToAddress);
            }
            else
            {
              shipNameWhereClause += string.Format("'{0}',",row.Calculated_ShipToName);
              shipAddrWhereClause += string.Format("'{0}',",row.Calculated_ShipToAddress);
            }
            
          }
          //orderHedWhereClause = orderHedWhereClause.Remove(orderHedWhereClause.Length - 1);
          whereClause = whereClause.Remove(whereClause.Length - 1);
          reqDateWhereClause = reqDateWhereClause.Remove(reqDateWhereClause.Length - 1);
          shipbyTimeWhereClause = shipbyTimeWhereClause.Remove(shipbyTimeWhereClause.Length - 1);
          
          OSTShipNameWhereClause = OSTShipNameWhereClause.Remove(OSTShipNameWhereClause.Length - 1);
          OSTShipAddrWhereClause = OSTShipAddrWhereClause.Remove(OSTShipAddrWhereClause.Length - 1);
          
          shipNameWhereClause = shipNameWhereClause.Remove(shipNameWhereClause.Length - 1);
          shipAddrWhereClause = shipAddrWhereClause.Remove(shipAddrWhereClause.Length - 1);
          
          //orderHedWhereClause += string.Format(")");
          whereClause += string.Format(")");
          reqDateWhereClause += string.Format(")");
          shipbyTimeWhereClause += string.Format(")");
          
          OSTShipNameWhereClause += string.Format(")");
          OSTShipAddrWhereClause += string.Format(")");
          
          shipNameWhereClause += string.Format(")");
          shipAddrWhereClause += string.Format(")");
          
          if(collection.Key)
          {
            orderRelWhereClause = string.Format("{0} AND {1} AND {2} AND {3} AND {4}", whereClause, reqDateWhereClause, shipbyTimeWhereClause, OSTShipNameWhereClause, OSTShipAddrWhereClause);
          }
          else
          {
            orderRelWhereClause = string.Format("{0} AND {1} AND {2} AND OrderRel.UseOTS = 0", whereClause, reqDateWhereClause, shipbyTimeWhereClause);
            shipToWhereClause = string.Format("{0} and {1}", shipNameWhereClause, shipAddrWhereClause);
          }
        
        /*{
          orderHedWhereClause += string.Format("OrderHed.OrderNum IN(");
          foreach(var row in collection)
          {
            orderHedWhereClause += string.Format("'{0}',", row.OrderHed_OrderNum);
          }
          orderHedWhereClause = orderHedWhereClause.Remove(orderHedWhereClause.Length - 1);
          orderHedWhereClause += string.Format(")");
          //throw new BLException(orderHedWhereClause.ToString());
        }*/
        
        
        //throw new BLException(orderHedWhereClause.ToString());
        
        
        Erp.Tablesets.OrderAllocListTableset soListTS = orderAllocSvc.GetListOfOrders(waveWhereClause,orderHedWhereClause, orderDtlWhereClause, orderRelWhereClause, customerWhereClause, partAllocWhereClause, countryWhereClause, shipToWhereClause,                          creditHoldClause, i_SortByOrder, i_SortByWarehouse, i_SortByAllocation, pageSize, absolutePage, out morePages, NO_Company);
        Erp.Tablesets.OrderAllocTableset soDS = orderAllocSvc.OrderAllocationGetRows(soListTS, 0);
        foreach(var row in soDS.OrderAlloc)
        { 
          var orderRel = Db.OrderRel.Where(x=>x.Company==row.Company && x.OrderNum==row.OrderNum && x.OrderLine==row.OrderLine && x.OrderRelNum==row.OrderRelNum && x.PbsJourneyNum_c==Journey).Count();
          if(orderRel > 0 && (row.AvailablePercent == 100 || row.ReservedQtyPct ==100) )
          {  
            
             GenAllocTableset(orderAllocDS, row);
            
          }
        }
      //var ds = GetOrderAllocList(collection);
      
    }
     waveWhereClause = string.Empty; orderHedWhereClause = string.Empty; orderDtlWhereClause = string.Empty; orderRelWhereClause = string.Empty; customerWhereClause = string.Empty
        ;partAllocWhereClause = "NoFilter,NoFilter,NoFilter,NoFilter"; countryWhereClause = string.Empty; shipToWhereClause = string.Empty; creditHoldClause = "NoFilter"; i_SortByOrder = string.Empty
        ;i_SortByWarehouse = string.Empty; i_SortByAllocation = string.Empty; NO_Company = string.Empty;
        
        orderHedWhereClause += string.Format("OrderHed.OrderNum IN(");
    if(otsHeaderCollection.Count() > 0)
    {
      foreach(var coll in otsHeaderCollection)
      {   
          orderHedWhereClause += string.Format("'{0}',", coll.OrderHed_OrderNum);
          
            
      }
          orderHedWhereClause = orderHedWhereClause.Remove(orderHedWhereClause.Length - 1);
          orderHedWhereClause += string.Format(")");
          
          //System.IO.File.WriteAllText(@"C:\temp\CR29Logs\log.txt",orderHedWhereClause);
          Erp.Tablesets.OrderAllocListTableset dsList = orderAllocSvc.GetListOfOrders(waveWhereClause,orderHedWhereClause, orderDtlWhereClause, orderRelWhereClause, customerWhereClause, partAllocWhereClause, countryWhereClause, shipToWhereClause,                          creditHoldClause, i_SortByOrder, i_SortByWarehouse, i_SortByAllocation, pageSize, absolutePage, out morePages, NO_Company);
          Erp.Tablesets.OrderAllocTableset ds = orderAllocSvc.OrderAllocationGetRows(dsList, 0);
          foreach(var row in ds.OrderAlloc)
          { 
            var orderRel = Db.OrderRel.Where(x=>x.Company==row.Company && x.OrderNum==row.OrderNum && x.OrderLine==row.OrderLine && x.OrderRelNum==row.OrderRelNum && x.PbsJourneyNum_c==Journey).Count();
            if(orderRel > 0 && (row.AvailablePercent == 100 || row.ReservedQtyPct ==100) )
            {  
               foreach(var r in otsHeaderCollection)
               {
                if(row.ShipToName.Equals(r.Calculated_ShipToName))
                  GenAllocTableset(orderAllocDS, row);
                }
            }
          }
     }
    //throw new BLException(orderAllocDS.OrderAlloc.Count.ToString());
      int counter = 0;
     
      foreach(Erp.Tablesets.OrderAllocRow row in orderAllocDS.OrderAlloc)
      {
        orderAllocDS.OrderAlloc[counter].SelectedForAction = true;
        orderAllocDS.OrderAlloc[counter].SelectForPicking = true;
        orderAllocDS.OrderAlloc[counter].RowMod = "U";
        counter++;
      }
      string cMessageText, cIPWhseList = string.Empty, cWhseType = "primaryonly";
      int iReleaseForPickingSeq = 0;
      bool iReleased =  false,  o_Success = false;
      
      orderAllocSvc.AutoPick(ref orderAllocDS, cIPWhseList, out o_Success, out cMessageText);
      orderAllocSvc.SubmitForPicking(ref orderAllocDS, 0, null, false, out cMessageText, out iReleaseForPickingSeq, out iReleased);
      orderAllocSvc.MtlQueueUpdate(iReleaseForPickingSeq);   
      
      //this.PublishInfoMessage(iReleaseForPickingSeq.ToString(),Ice.Common.BusinessObjectMessageType.Information, Ice.Bpm.InfoMessageDisplayMode.Individual,"","");
      if(iReleaseForPickingSeq != 0)
      {
        string mtlSequences = string.Empty;
         
         var submitForPickingOrderes = (from mtl in Db.MtlQueue where mtl.ReleaseForPickingSeq==iReleaseForPickingSeq group mtl by new {mtl.OrderNum,mtl.ReleaseForPickingSeq} into grp select new {grp.Key.OrderNum,grp.Key.ReleaseForPickingSeq}).ToList();
         
         foreach(var row in submitForPickingOrderes)
         { 
          foreach(var mtl_Row in (Db.MtlQueue.Where(w=>w.Company==Session.CompanyID && w.OrderNum==row.OrderNum && w.ReleaseForPickingSeq==row.ReleaseForPickingSeq)))
          {  
           if(string.IsNullOrEmpty(mtlSequences))
           {
             mtlSequences = mtl_Row.MtlQueueSeq.ToString();
           }
           else
           {
           mtlSequences +="~"+mtl_Row.MtlQueueSeq.ToString();
           }
          }
          if(!string.IsNullOrEmpty(mtlSequences))
          {
           PrintPickSlip(mtlSequences);
           mtlSequences=string.Empty;
          }  
        } 
   
       UpdateInPickingStatusOrderRel(iReleaseForPickingSeq);
       this.PublishInfoMessage("Report(s) has submitted to the task agent.",Ice.Common.BusinessObjectMessageType.Information, Ice.Bpm.InfoMessageDisplayMode.Individual,"","");
      }  
      if(!string.IsNullOrWhiteSpace(cMessageText))
          this.PublishInfoMessage(string.Format("{0}", cMessageText),Ice.Common.BusinessObjectMessageType.Information, Ice.Bpm.InfoMessageDisplayMode.Individual,"","");
          
    txScope.Complete();
  }
}