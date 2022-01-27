var xRows = ttResults.ToList();

Action<string> PrintPickSlip = (mtlQueueSeq) =>
{
  var mtlQueueRptSvc = ServiceRenderer.GetService<Erp.Contracts.MtlQueueRptSvcContract>(this.Db);
  Erp.Tablesets.MtlQueueRptTableset mtlQueueRptTableset = mtlQueueRptSvc.GetNewParameters();
  
  var newRow = mtlQueueRptTableset.MtlQueueRptParam[0];
        newRow["Company"] = "";
        newRow["Plant"] = "" ; 
        newRow["EmpID"] = "";
        newRow["MtlQueueSeq"] = mtlQueueSeq;
        newRow["PageBreak"] = "OrderNum";
        newRow["SortBy"] = "WhseBin";
        newRow["ReleaseForPickingSeq"] = 0;
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
      //  newRow["SSRSEnableRouting"] = true;
        newRow["DesignMode"] = false;
        newRow["RowMod"] = "A";
   
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

string mtlSequences = string.Empty;
int pickingSeq=0;
if(xRows != null)
{
   foreach(var row in xRows)
   {
     foreach(var mtl_Row in (Db.MtlQueue.Where(w=>w.Company==Session.CompanyID && w.OrderNum==row.OrderHed_OrderNum)))
     {
       if(string.IsNullOrEmpty(mtlSequences))
       {
         mtlSequences = mtl_Row.MtlQueueSeq.ToString();
         pickingSeq=mtl_Row.ReleaseForPickingSeq;
       }
       else
       {
         mtlSequences +="~"+mtl_Row.MtlQueueSeq.ToString();
       }
       
     }
     if(!string.IsNullOrEmpty(mtlSequences))
     {
      PrintPickSlip(mtlSequences);
      mtlSequences = string.Empty;
     
     }
   }
   this.PublishInfoMessage("Report submit to the task agent",Ice.Common.BusinessObjectMessageType.Information,Ice.Bpm.InfoMessageDisplayMode.Individual,"","");
}