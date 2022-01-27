using Epicor.Hosting;
using Erp.Tablesets;
using Ice;
using Ice.Assemblies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Epicor.Data;
using Erp.Tables;
using System.Data;
using System.Data.SqlClient;


namespace Erp.Lib.PbsPriceListShared
{
    public class EpicorPriceListHelper
    {
        public class EpicorPriceListHelperArgs
        {
            public Session Session;
            public ErpContext Db;
            public int GroupId;
        }

        public static void Generate(EpicorPriceListHelperArgs args)
        {
            var db = args.Db;
            var groupId = args.GroupId;

            var pbsPriceListGroups = (
                from g in db.PbsPriceListGroup
                join p in db.PbsPriceList on 
                    new { g.Company, GroupId = g.ID, Generated = false } equals 
                    new { p.Company, p.GroupId, p.Generated }
                where groupId == -1 || g.ID == groupId
                select g
                )
                .GroupBy(g => g.ID)
                .ToList();

            pbsPriceListGroups.ForEach(pbsPriceListGroup => GeneratePriceListGroup(args.Session, db, pbsPriceListGroup.First()));

            db.Validate();
        }

        public static void GeneratePriceListGroup(Session session, ErpContext db, PbsPriceListGroup pbsPriceListGroup)
        {
            var pbsPriceLists = db.PbsPriceList
                .Where(pl => pl.GroupId == pbsPriceListGroup.ID && pl.Generated == false)
                .ToList();

            pbsPriceLists.ForEach(pbsPriceList => GeneratePriceList(session, db, pbsPriceListGroup, pbsPriceList));
        }

        public static void GeneratePriceList(Session session, ErpContext db, PbsPriceListGroup pbsPriceListGroup, PbsPriceList pbsPriceList)
        {
            var priceLstTs = new PbsPriceListTableset();
            var priceLstRow = (Tablesets.PbsPriceListRow) null;
            var priceLstPartRow = (Tablesets.PbsPriceListPartRow) null;
            var priceLstSvc = ServiceRenderer.GetService<Erp.Contracts.PbsPriceListSvcContract>(db);

            if (pbsPriceListGroup == null || pbsPriceList == null)
                return;

            try
            {
                var cmd = "[Erp].[PbsEpicorPriceListGeneration]";

                var pCompany = new SqlParameter("@Company", SqlDbType.NVarChar)
                {
                    Value = session.CompanyID
                };
                var pUserId = new SqlParameter("@UserId", SqlDbType.NVarChar)
                {
                    Value = session.UserID
                };
                var pPriceListId = new SqlParameter("@PbsPriceListId", SqlDbType.Int)
                {
                    Value = pbsPriceList.ID
                };

                var sCommand = new SqlCommand(cmd, db.SqlConnection);
                sCommand.CommandType = CommandType.StoredProcedure;
                sCommand.Parameters.Add(pCompany);
                sCommand.Parameters.Add(pUserId);
                sCommand.Parameters.Add(pPriceListId);
                sCommand.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                throw;
            }

            try
            {
                string whereClausePbsPriceList = string.Format("PriceListCode='{0}'", pbsPriceList.PriceListCode);
                priceLstTs = priceLstSvc.GetRows(string.Empty, whereClausePbsPriceList, string.Empty, 0, 1, out bool morePages); //priceLstSvc.GetByID(Convert.ToInt32(pbsPriceList.GroupId));// priceLstSvc.GetByID(Convert.ToInt32(pbsPriceList.PriceListCode));
                //priceLstTs.PbsPriceList.Where(x => x.PriceListCode.Equals(pbsPriceList.PriceListCode)).FirstOrDefault().RowMod = Ice.IceRow.ROWSTATE_UPDATED;
                priceLstTs.PbsPriceList[0].RowMod = Ice.IceRow.ROWSTATE_UPDATED;

                priceLstSvc.Update(ref priceLstTs);
            }
            catch (Ice.Common.RecordNotFoundException rnfex)
            {
                throw;
            }
            catch (System.Exception ex)
            {
                throw;
            }

            #region EF/BO Centric code
            //if (priceLstTs.PriceLst.Count == 0)
            //{
            //    priceLstSvc.GetNewPriceLst(ref priceLstTs);
            //    priceLstRow = priceLstTs.PriceLst.First(r => r.ListCode.Equals(""));
            //    priceLstRow.ListCode = pbsPriceList.PriceListCode;
            //    priceLstRow.ListType = "P";
            //    priceLstRow.ListDescription = pbsPriceList.PriceListCode;
            //    priceLstRow.RowMod = IceRow.ROWSTATE_ADDED;
            //    priceLstRow.StartDate = pbsPriceListGroup.StartDate;
            //    priceLstRow.EndDate = pbsPriceListGroup.ExpiryDate;

            //    priceLstRow.SetUDField<string>("PbsPriceListType_c", pbsPriceList.Type);
            //    priceLstRow.SetUDField<string>("PbsPriceListCusId_c", pbsPriceList.CustId);
            //    priceLstRow.SetUDField<string>("PbsPriceListJobId_c", pbsPriceList.JobId);
            //    priceLstRow.SetUDField<int>("PbsPriceListBuyCode_c", pbsPriceList.BuyCode);
            //    priceLstRow.SetUDField<string>("PbsPriceListRegionCode_c", pbsPriceList.RegionCode);

            //    priceLstSvc.Update(ref priceLstTs);
            //}
            //else
            //{
            //    priceLstRow = priceLstTs.PriceLst.First();
            //}

            //var pbsParts = db.PbsPriceListPart.Where(p =>
            //    p.Company == pbsPriceList.Company &&
            //    p.PriceListID == pbsPriceList.ID
            //).ToList();

            //try
            //{
            //    // Remove any parts from the Epicor Price List Parts that don't exist in the 
            //    // PbsPriceListParts table
            //    while ((priceLstPartRow = priceLstTs.PriceLstParts.FirstOrDefault(part =>
            //               !pbsParts.Any(pbsPart => part.PartNum == pbsPart.PartNum && 
            //                                        part.UOMCode == pbsPart.UOM))) != null)
            //    {
            //        priceLstPartRow.RowMod = IceRow.ROWSTATE_DELETED;
            //        priceLstSvc.Update(ref priceLstTs);
            //    }
            //}
            //catch (System.Exception ex)
            //{
            //    throw;
            //}

            //pbsParts
            //    .GroupBy(p => new {p.PartNum, p.UOM})
            //    .ToList()
            //    .ForEach(p =>
            //    {
            //        var pbsPriceListPart = p.FirstOrDefault(p1 => p1.Quantity == 1);
            //        if (pbsPriceListPart == null) return;

            //        var list = p.OrderBy(p1 => p1.Quantity).ToList();

            //        try
            //        {
            //            var pbsQtyBrkPartIdx = 1;

            //            // Create or Update the Epicor Price List Part record
            //            priceLstPartRow = priceLstTs.PriceLstParts.FirstOrDefault(r =>
            //                r.PartNum == pbsPriceListPart.PartNum && r.UOMCode == pbsPriceListPart.UOM);
            //            if (priceLstPartRow == null)
            //            {
            //                var uomCode = pbsPriceListPart.UOM;
            //                var partUOMValid 
            //                    = db.Part.FirstOrDefault(part => part.Company == pbsPriceListPart.Company && part.PartNum == pbsPriceListPart.PartNum).TrackDimension == false
            //                    | db.PartUOM.Any(puom => 
            //                        puom.Company == pbsPriceListPart.Company && 
            //                        puom.PartNum == pbsPriceListPart.PartNum && 
            //                        puom.UOMCode == pbsPriceListPart.UOM && 
            //                        puom.TrackOnHand);
            //                if (!partUOMValid)
            //                    return;

            //                priceLstSvc.GetNewPriceLstParts(ref priceLstTs, priceLstRow.ListCode,
            //                    pbsPriceListPart.PartNum);
            //                priceLstPartRow =
            //                    priceLstTs.PriceLstParts.First(r => r.RowMod == IceRow.ROWSTATE_ADDED);
            //                priceLstPartRow.UOMCode = uomCode;
            //            }

            //            priceLstPartRow.BasePrice = pbsPriceListPart.BasePrice;
            //            priceLstPartRow.RowMod = priceLstPartRow.RowMod != IceRow.ROWSTATE_ADDED
            //                ? IceRow.ROWSTATE_UPDATED
            //                : priceLstPartRow.RowMod;
            //            priceLstSvc.Update(ref priceLstTs);

            //            list.ForEach(pbsQuantityBreakPart =>
            //            {
            //                if (pbsPriceListPart == pbsQuantityBreakPart)
            //                    return;

            //                var discount = 0M;
            //                var basePrice = Math.Round(pbsQuantityBreakPart.BasePrice, 2);
            //                if (basePrice == 0)
            //                    return;

            //                var plPartBrkRow = priceLstTs.PLPartBrk.FirstOrDefault(pb =>
            //                    pb.ListCode == pbsPriceList.PriceListCode &&
            //                    pb.PartNum == pbsPriceListPart.PartNum &&
            //                    pb.UOMCode == pbsPriceListPart.UOM &&
            //                    pb.Quantity == pbsQuantityBreakPart.Quantity);
            //                if (plPartBrkRow == null)
            //                {
            //                    priceLstSvc.GetNewPLPartBrk(ref priceLstTs, pbsPriceList.PriceListCode,
            //                        pbsPriceListPart.PartNum, pbsPriceListPart.UOM);

            //                    plPartBrkRow =
            //                        priceLstTs.PLPartBrk.FirstOrDefault(brk => brk.RowMod == IceRow.ROWSTATE_ADDED);
            //                }

            //                if (pbsQuantityBreakPart.BasePrice > 0 && pbsPriceListPart.BasePrice > 0)
            //                    discount = Math.Round(
            //                        (1- (pbsQuantityBreakPart.BasePrice / pbsPriceListPart.BasePrice)) * 100,
            //                        2);

            //                if (plPartBrkRow != null)
            //                {
            //                    plPartBrkRow.Quantity = pbsQuantityBreakPart.Quantity;
            //                    plPartBrkRow.DiscountPercent = discount;
            //                    plPartBrkRow.UnitPrice = basePrice;
            //                    plPartBrkRow.RowMod 
            //                        = plPartBrkRow.RowMod.Equals(Ice.IceRow.ROWSTATE_ADDED)
            //                        ? plPartBrkRow.RowMod
            //                        : Ice.IceRow.ROWSTATE_UPDATED;
            //                    priceLstSvc.Update(ref priceLstTs);

            //                    priceLstPartRow = priceLstTs.PriceLstParts.FirstOrDefault(r =>
            //                        r.PartNum == pbsPriceListPart.PartNum && r.UOMCode == pbsPriceListPart.UOM);
            //                }

            //                if (priceLstPartRow != null)
            //                {
            //                    priceLstPartRow[$"QtyBreak{pbsQtyBrkPartIdx}"] = pbsQuantityBreakPart.Quantity;
            //                    priceLstPartRow[$"UnitPrice{pbsQtyBrkPartIdx}"] = basePrice;
            //                    priceLstPartRow[$"DiscountPercent{pbsQtyBrkPartIdx}"] = discount;
            //                    priceLstPartRow.RowMod = Ice.IceRow.ROWSTATE_UPDATED;
            //                }

            //                pbsQtyBrkPartIdx++;
            //            });

            //            priceLstSvc.Update(ref priceLstTs);
            //        }
            //        catch (System.Exception ex)
            //        {
            //            throw new Exception(
            //                ex.Message + $": Unable to Create/Update Part {priceLstPartRow.PartNum}",
            //                ex);
            //        }
            //    });
            #endregion

            Attach(session, db, pbsPriceListGroup, pbsPriceList);
        }

        public static void Attach(Session session, ErpContext db, PbsPriceListGroup pbsPriceListGroup, PbsPriceList pbsPriceList)
        {
            try
            {
                // Attach to Customer
                var customers = new List<Customer>();

                if (pbsPriceList.Type.Equals("Standard"))
                {
                    customers = db.Customer.Where(c => c.Company == pbsPriceList.Company && !c.CustID.StartsWith("POSCASH") &&
                                                    c.PbsInactive_c == false && c.PbsBuilder_c == false &&
                                                    c.PbsBuyCodeID_c == pbsPriceList.BuyCode && (pbsPriceList.CustId == string.Empty || c.CustID.Equals(pbsPriceList.CustId))
                                                  ).ToList();

                    if (pbsPriceList.BuyCode == 01 || pbsPriceList.BuyCode == 04 || pbsPriceList.BuyCode == 28)
                    {
                        var POSCashCustomers = (
                                               from c in db.Customer
                                               from t in db.SalesTer.Where(t =>
                                                   t.Company == c.Company &&
                                                   t.TerritoryID == c.TerritoryID).DefaultIfEmpty()
                                               where c.Company == pbsPriceList.Company && c.CustID.StartsWith("POSCASH") &&
                                                     c.PbsInactive_c == false &&
                                                     //c.PbsBuyCodeID_c == pbsPriceList.BuyCode &&
                                                     (pbsPriceList.CustId == string.Empty || c.CustID.Equals(pbsPriceList.CustId)) &&
                                                     t.RegionCode == pbsPriceList.RegionCode
                                               select c).ToList();

                        foreach (var posCashCustomer in POSCashCustomers)
                        {
                            customers.Add(posCashCustomer);
                        }
                    }
                    //else
                    //{
                    //    customers = (
                    //       from c in db.Customer
                    //       where c.Company == pbsPriceList.Company &&
                    //             c.PbsInactive_c == false &&
                    //             c.PbsBuyCodeID_c == pbsPriceList.BuyCode &&
                    //             (pbsPriceList.CustId == string.Empty || c.CustID.Equals(pbsPriceList.CustId))
                    //       select c).ToList();
                    //}
                }
                else
                {
                    //customers = db.Customer.Where(c => 
                    //  c.Company == pbsPriceList.Company &&
                    //  c.PbsInactive_c == false &&
                    //  ((pbsPriceList.Classification == null || c.PbsBuyingClass_c.Equals(pbsPriceList.Classification))
                    //      && (pbsPriceList.CustId == string.Empty || c.CustID.Equals(pbsPriceList.CustId)))
                    //  ).ToList();
                    customers = (
                       from c in db.Customer
                       from t in db.SalesTer.Where(t =>
                           t.Company == c.Company &&
                           t.TerritoryID == c.TerritoryID).DefaultIfEmpty()
                       where c.Company == pbsPriceList.Company &&
                             c.PbsInactive_c == false &&
                            ((pbsPriceList.Classification == null || c.PbsBuyingClass_c.Equals(pbsPriceList.Classification))
                                && (pbsPriceList.CustId == string.Empty || c.CustID.Equals(pbsPriceList.CustId))) &&
                             t.RegionCode == pbsPriceList.RegionCode
                       select c).ToList();
                }

                customers.ForEach(customer =>
                {
                    var customerSvc = ServiceRenderer.GetService<Erp.Contracts.CustomerSvcContract>(db);

                    var shipToNum = string.Empty;
                    var customerTs = customerSvc.GetCustomer(customer.CustID);
                    var customerRow = customerTs.Customer.First();

                    if (pbsPriceList.Type.Equals(PriceListType.Job.ToString()))
                    {
                        var shipTo = db.ShipTo.FirstOrDefault(s =>
                            s.Company == customer.Company &&
                            s.PbsInactive_c == false &&
                            s.CustNum == customer.CustNum &&
                            s.ShipToNum == shipToNum);
                        if (shipTo == null)
                            throw new BLException("The specified Ship To is not valid.");

                        shipToNum = pbsPriceList.ShipTo;
                    }

                    var priceListExists = db.CustomerPriceLst.Any(cp =>
                        cp.Company == pbsPriceList.Company &&
                        cp.CustNum == customer.CustNum &&
                        cp.ListCode == pbsPriceList.PriceListCode);

                    if (priceListExists == false)
                    {
                        if (!string.IsNullOrEmpty(shipToNum))
                        {
                            customerSvc.GetNewShipToPriceLst(ref customerTs, customerRow.CustNum, shipToNum);
                            var shipToPriceListRow = customerTs.ShipToPriceLst.First(p => p.RowMod == IceRow.ROWSTATE_ADDED);
                            shipToPriceListRow.ListCode = pbsPriceList.PriceListCode;
                            shipToPriceListRow.PriceListStartDate = pbsPriceListGroup.StartDate;
                            shipToPriceListRow.PriceListEndDate = pbsPriceListGroup.ExpiryDate;
                        }
                        else
                        {
                            customerSvc.GetNewCustomerPriceLst(ref customerTs, customerRow.CustNum, shipToNum);
                            var shipToPriceListRow = customerTs.CustomerPriceLst.First(p => p.RowMod == IceRow.ROWSTATE_ADDED);
                            shipToPriceListRow.ListCode = pbsPriceList.PriceListCode;
                            shipToPriceListRow.PriceListStartDate = pbsPriceListGroup.StartDate;
                            shipToPriceListRow.PriceListEndDate = pbsPriceListGroup.ExpiryDate;
                        }
                        customerSvc.Update(ref customerTs);

                        db.Validate();
                        
                        if (pbsPriceList.Type.Equals("Customer", StringComparison.InvariantCultureIgnoreCase))
                            CustomerPriceListToTop(db, pbsPriceList.Company, customer.CustNum, shipToNum, pbsPriceList.PriceListCode);
                    }

                    DeleteExpiredPriceLists(db, pbsPriceList.Company, customer.CustNum);
                });

                pbsPriceList.Generated = true;
            }
            catch (System.Exception ex)
            {
                throw;
            }
        }

        static void DeleteExpiredPriceLists(ErpContext db, string company, int custNum)
        {
            using (var scape = ErpContext.CreateDefaultTransactionScope())
            {
                var expiryDate = DateTime.Now.Date;
                expiryDate = expiryDate.AddMonths(-1).AddDays(-expiryDate.Date.Day + 1);

                // Delete Price Lists
                var plSvc = ServiceRenderer.GetService<Erp.Contracts.PbsPriceListSvcContract>(db);
                var expiredPriceLists = (
                    from pl in db.PriceLst
                    where pl.Company == company
                       && pl.EndDate < expiryDate
                    select pl
                    ).ToList();
                expiredPriceLists.ForEach(pl => {
                    db.CustomerPriceLst
                        .Where(cpl => cpl.Company == company && cpl.ListCode == pl.ListCode && cpl.CustNum == custNum)
                        .ToList()
                        .ForEach(db.CustomerPriceLst.Delete);

                    // Don't delete the Epicor Price List
                    //plSvc.DeleteByID(pl.ListCode);
                });

                db.Validate();

                scape.Complete();
            }
        }

        static void CustomerPriceListToTop(ErpContext db, string company, int custNum, string shipToNum, string priceListCode)
        {
            using (var scape = ErpContext.CreateDefaultTransactionScope())
            {
                var expiryDate = DateTime.Now.Date;
                expiryDate = expiryDate.AddMonths(-1).AddDays(-expiryDate.Date.Day + 1);

                // Delete Price Lists
                var plSvc = ServiceRenderer.GetService<Erp.Contracts.PbsPriceListSvcContract>(db);
                var expiredPriceLists = (
                    from cpl in db.CustomerPriceLst
                    join pl in db.PriceLst on
                            new { cpl.Company, cpl.ListCode } equals
                            new { pl.Company, pl.ListCode }
                    where cpl.Company == company
                       && cpl.CustNum == custNum
                       && cpl.ShipToNum.Equals(shipToNum)
                       && pl.EndDate <= expiryDate
                    select new { cpl, pl }
                    ).ToList();
                expiredPriceLists.ForEach(epl => {
                    // Possible cause of the "Cannot delete referenced record" error.
                    //plSvc.DeleteByID(epl.pl.ListCode);

                    db.CustomerPriceLst.Delete(epl.cpl);
                });

                db.Validate();

                var custPriceLists = db.CustomerPriceLst.Where(cpl =>
                    cpl.Company == company &&
                    cpl.CustNum == custNum &&
                    cpl.ShipToNum.Equals(shipToNum))
                    .OrderBy(x => x.SeqNum)
                    .ToList();
                if (custPriceLists.Count == 0)
                    return;

                var priceList = custPriceLists.FirstOrDefault(x => x.ListCode.Equals(priceListCode));
                if (priceList == null)
                    return;

                var newOrder = new List<string>();
                newOrder.Add(priceList.ListCode);
                foreach (var pl in custPriceLists)
                {
                    if (pl.ListCode.Equals(priceListCode))
                        continue;
                    newOrder.Add(pl.ListCode);
                }

                for (var i = 0; i < custPriceLists.Count; i++)
                    custPriceLists[i].ListCode = i >= newOrder.Count
                        ? custPriceLists[i].ListCode
                        : newOrder[i];

                db.Validate();

                scape.Complete();
            }

        }
    }
}
