// Decompiled with JetBrains decompiler
// Type: pbsConcurWebServices.POHelper
// Assembly: pbsConcurWebServices, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A862484-1C34-44D9-898F-45BB4053517A
// Assembly location: C:\Users\mehar.singh\OneDrive - Precise Business Solutions\Desktop\pbsConcurWebServices.dll

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace pbsConcurWebServices
{
  public class POHelper
  {
    public static void CreateNewPO(string url, string token, PurchaseOrder purchaseOrder)
    {
      string method = "POST";
      POHelper.SendHttpRequest(url, token, method, (object) purchaseOrder);
    }

    public static void UpdatePO(string url, string token, string Id, PurchaseOrder purchaseOrder)
    {
      string method = "PUT";
      POHelper.SendHttpRequest(string.Format("{0}/{1}", (object) url, (object) Id), token, method, (object) purchaseOrder);
    }

    public static void UpdatePOReceiptInformation(
      string url,
      string token,
      PurchaseOrderReceipt purchaseReceipt)
    {
      string method = "PUT";
      POHelper.SendHttpRequest(url, token, method, (object) purchaseReceipt);
    }

    public static HttpStatusCode SendHttpRequest(
      string url,
      string token,
      string method,
      object body)
    {
      HttpStatusCode httpStatusCode = HttpStatusCode.NoContent;
      string str1 = "application/xml";
      HttpWebRequest request = (HttpWebRequest) WebRequest.Create(url);
      request.Headers.Add("Authorization", "OAuth  " + token);
      request.Method = method;
      XmlSerializer xmlSerializer = new XmlSerializer(body.GetType());
      using (MemoryStream memoryStream1 = new MemoryStream())
      {
        MemoryStream memoryStream2 = memoryStream1;
        XmlWriterSettings settings = new XmlWriterSettings()
        {
          OmitXmlDeclaration = true
        };
        using (XmlWriter xmlWriter = XmlWriter.Create((Stream) memoryStream2, settings))
          xmlSerializer.Serialize(xmlWriter, body);
        memoryStream1.Position = 0L;
        string end = new StreamReader((Stream) memoryStream1).ReadToEnd();
        UTF8Encoding utF8Encoding = new UTF8Encoding();
        request.ContentLength = (long) utF8Encoding.GetByteCount(end);
        request.ContentType = str1;
        using (Stream requestStream = request.GetRequestStream())
          requestStream.Write(utF8Encoding.GetBytes(end), 0, utF8Encoding.GetByteCount(end));
        HttpWebResponse httpWebResponse = (HttpWebResponse) null;
        try
        {
          httpWebResponse = new SharedCode().GetResponse(request);
          httpStatusCode = httpWebResponse.StatusCode;
        }
        catch (WebException ex)
        {
          string str2 = string.Empty;
          WebResponse response = ex.Response;
          if (response != null)
          {
            using (Stream responseStream = response.GetResponseStream())
              str2 = new StreamReader(responseStream).ReadToEnd();
          }
          throw new Exception(str2 + Environment.NewLine + ex.Message, (Exception) ex);
        }
        finally
        {
          httpWebResponse?.Dispose();
        }
      }
      return httpStatusCode;
    }

    public static bool FindPurchaseOrder(string token, string url, string poNum)
    {
      ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
      bool flag = false;
      PurchaseOrder purchaseOrder = (PurchaseOrder) null;
      SharedCode sharedCode = new SharedCode();
      HttpWebRequest request = (HttpWebRequest) WebRequest.Create(string.Format("{0}/{1}", (object) url, (object) poNum));
      request.Headers.Add("Authorization", "OAuth  " + token);
      request.Method = "Get";
      HttpWebResponse httpWebResponse = (HttpWebResponse) null;
      try
      {
        httpWebResponse = sharedCode.GetResponse(request);
        if (httpWebResponse.StatusCode == HttpStatusCode.OK)
        {
          flag = true;
          purchaseOrder = PurchaseOrder.FromXmlString(new Utilities().getStringFromStream(httpWebResponse.GetResponseStream()));
        }
      }
      catch (WebException ex)
      {
        string str = string.Empty;
        WebResponse response = ex.Response;
        if (response != null)
        {
          using (Stream responseStream = response.GetResponseStream())
            str = new StreamReader(responseStream).ReadToEnd();
        }
        string message = str + Environment.NewLine + ex.Message;
        if (!message.Contains("Purchase Order Number does not exist in system"))
          throw new Exception(message, (Exception) ex);
        flag = false;
      }
      finally
      {
        httpWebResponse?.Dispose();
      }
      return flag;
    }

    public static void SyncPurchseOrder(
      string connectionstring,
      string poNum,
      string policyExternalId,
      string dummyBillToExternalId,
      string dummyShipToExternalId,
      string token,
      string url)
    {
      DataSet dataSet = new DataSet();
      try
      {
        using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(string.Format("{0};{1}", (object) string.Format("Select po_no,curr_key,vendor_no,Convert(date,date_of_order) as date_of_order , ship_name\r\n                                                , ship_address1,ship_address2,ship_address3,ship_city,ship_state, ship_zip,ship_country_cd\r\n                                                ,locations_all.location, locations_all.addr1,locations_all.addr2,locations_all.addr3,locations_all.city\r\n                                                , locations_all.state,locations_all.zip,locations_all.country_code, total_tax, status \r\n                                                , armOrder.LocationCode, location.GLSegmentCode1, location.GLSegmentCode2, location.GLSegmentCode3 \r\n                                                from purchase_all\r\n                                                     Left Outer join locations_all on (purchase_all.organization_id = locations_all.organization_id\r\n                                                \t\tand purchase_all.location = locations_all.location)\r\n                                                        Left Outer Join eReq.dbo.ProFormaOrder armOrder on ( purchase_all.po_no = armOrder.PurchaseOrderNo)\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t        Left Outer Join eReq.dbo.Location on (armOrder.LocationCode = location.LocationCode)\r\n                                                where po_no = '{0}'", (object) poNum), (object) string.Format("Select line,part_no,description,Cast(qty_ordered as decimal(10,2)) as qty_ordered, case when tax_code = 'GSTAPIN' then Cast((unit_cost - total_tax) as decimal(10,2)) else cast(unit_cost as decimal(10,2)) End as unit_cost\r\n\t, case when tax_code = 'GSTAPIN' then cast((curr_cost - total_tax) as decimal(10,2) ) else cast(curr_cost as decimal(10,2)) End curr_cost, cast(total_tax as decimal(10,2)) total_tax\r\n                                                    ,cast((qty_ordered * case when tax_code = 'GSTAPIN' then (unit_cost - total_tax) else unit_cost End) as decimal(10,2) ) as extCost, SUBSTRING(account_no,len(account_no)-3,4) as AccountNo\r\n                                                    ,tax_code  \r\n                                                 from pur_list where po_no = '{0}'", (object) poNum)), connectionstring))
          sqlDataAdapter.Fill(dataSet);
        if (dataSet != null && (dataSet.Tables[0] == null || dataSet.Tables[0].Rows.Count <= 0))
          throw new Exception(string.Format("PO Not found PONum: {0}", (object) poNum));
        if (dataSet != null && (dataSet.Tables[1] == null || dataSet.Tables[1].Rows.Count <= 0))
          throw new Exception(string.Format("PO Lines Not found PONum: {0}", (object) poNum));
        PurchaseOrder purchaseOrder = new PurchaseOrder();
        purchaseOrder.PurchaseOrderNumber = dataSet.Tables[0].Rows[0]["po_no"].ToString();
        purchaseOrder.PolicyExternalID = policyExternalId;
        purchaseOrder.CurrencyCode = dataSet.Tables[0].Rows[0]["curr_key"].ToString();
        purchaseOrder.VendorCode = dataSet.Tables[0].Rows[0]["vendor_no"].ToString();
        purchaseOrder.VendorAddressCode = dataSet.Tables[0].Rows[0]["vendor_no"].ToString();
        purchaseOrder.OrderDate = Convert.ToDateTime(dataSet.Tables[0].Rows[0]["date_of_order"]).ToString("yyyy-MM-dd");
        purchaseOrder.Name = dataSet.Tables[0].Rows[0]["po_no"].ToString();
        purchaseOrder.Description = dataSet.Tables[0].Rows[0]["po_no"].ToString();
        string empty1 = string.Empty;
        string empty2 = string.Empty;
        string empty3 = string.Empty;
        string empty4 = string.Empty;
        if (dataSet.Tables[0].Rows[0]["LocationCode"] != null)
          empty4 = dataSet.Tables[0].Rows[0]["LocationCode"].ToString();
        if (dataSet.Tables[0].Rows[0]["GLSegmentCode1"] != null)
          empty1 = dataSet.Tables[0].Rows[0]["GLSegmentCode1"].ToString();
        if (dataSet.Tables[0].Rows[0]["GLSegmentCode1"] != null)
          empty2 = dataSet.Tables[0].Rows[0]["GLSegmentCode2"].ToString();
        if (dataSet.Tables[0].Rows[0]["GLSegmentCode1"] != null)
          empty3 = dataSet.Tables[0].Rows[0]["GLSegmentCode3"].ToString();
        if (string.IsNullOrEmpty(empty1) || string.IsNullOrEmpty(empty2) || string.IsNullOrEmpty(empty3))
          throw new Exception(string.Format("Segment Codes are not valid for the location in ARM ({0})", (object) empty4));
        purchaseOrder.Custom1 = empty1;
        purchaseOrder.Custom2 = empty2;
        purchaseOrder.Custom3 = empty3;
        if (dataSet.Tables[0].Rows[0]["status"].ToString().Equals("C"))
          purchaseOrder.Status = "Closed";
        purchaseOrder.ReceiptType = "WQTY";
        purchaseOrder.Shipping = "0";
        purchaseOrder.Tax = dataSet.Tables[0].Rows[0]["total_tax"].ToString();
        BillToAddress billToAddress = new BillToAddress();
        billToAddress.ExternalID = dataSet.Tables[0].Rows[0]["po_no"].ToString();
        billToAddress.Name = dataSet.Tables[0].Rows[0]["location"].ToString();
        billToAddress.Address1 = dataSet.Tables[0].Rows[0]["addr1"].ToString();
        billToAddress.Address2 = dataSet.Tables[0].Rows[0]["addr2"].ToString();
        billToAddress.Address3 = dataSet.Tables[0].Rows[0]["addr3"].ToString();
        int num1 = dataSet.Tables[0].Rows[0]["city"] == null ? 1 : (string.IsNullOrEmpty(dataSet.Tables[0].Rows[0]["city"].ToString().Trim()) ? 1 : 0);
        billToAddress.City = num1 != 0 ? "Perth" : dataSet.Tables[0].Rows[0]["city"].ToString();
        int num2 = dataSet.Tables[0].Rows[0]["state"] == null ? 1 : (string.IsNullOrEmpty(dataSet.Tables[0].Rows[0]["state"].ToString().Trim()) ? 1 : 0);
        billToAddress.State = num2 != 0 ? "WA" : dataSet.Tables[0].Rows[0]["state"].ToString();
        int num3 = dataSet.Tables[0].Rows[0]["state"] == null ? 1 : (string.IsNullOrEmpty(dataSet.Tables[0].Rows[0]["state"].ToString().Trim()) ? 1 : 0);
        billToAddress.StateProvince = num3 != 0 ? "Western Australia" : "-";
        int num4 = dataSet.Tables[0].Rows[0]["zip"] == null ? 1 : (string.IsNullOrEmpty(dataSet.Tables[0].Rows[0]["zip"].ToString().Trim()) ? 1 : 0);
        billToAddress.PostalCode = num4 != 0 ? "6104" : dataSet.Tables[0].Rows[0]["zip"].ToString();
        int num5 = dataSet.Tables[0].Rows[0]["country_code"] == null ? 1 : (string.IsNullOrEmpty(dataSet.Tables[0].Rows[0]["country_code"].ToString().Trim()) ? 1 : 0);
        billToAddress.CountryCode = num5 != 0 ? "AU" : dataSet.Tables[0].Rows[0]["country_code"].ToString();
        purchaseOrder.BillToAddress = billToAddress;
        ShipToAddress shipToAddress = new ShipToAddress();
        shipToAddress.ExternalID = dataSet.Tables[0].Rows[0]["po_no"].ToString();
        shipToAddress.Name = dataSet.Tables[0].Rows[0]["ship_name"].ToString();
        shipToAddress.Address1 = dataSet.Tables[0].Rows[0]["ship_address1"].ToString();
        shipToAddress.Address2 = dataSet.Tables[0].Rows[0]["ship_address2"].ToString();
        shipToAddress.Address3 = dataSet.Tables[0].Rows[0]["ship_address3"].ToString();
        int num6 = dataSet.Tables[0].Rows[0]["ship_city"] == null ? 1 : (string.IsNullOrEmpty(dataSet.Tables[0].Rows[0]["ship_city"].ToString().Trim()) ? 1 : 0);
        shipToAddress.City = num6 != 0 ? "Perth" : dataSet.Tables[0].Rows[0]["ship_city"].ToString();
        int num7 = dataSet.Tables[0].Rows[0]["ship_state"] == null ? 1 : (string.IsNullOrEmpty(dataSet.Tables[0].Rows[0]["ship_state"].ToString().Trim()) ? 1 : 0);
        shipToAddress.State = num7 != 0 ? "WA" : dataSet.Tables[0].Rows[0]["ship_state"].ToString();
        int num8 = dataSet.Tables[0].Rows[0]["ship_state"] == null ? 1 : (string.IsNullOrEmpty(dataSet.Tables[0].Rows[0]["ship_state"].ToString().Trim()) ? 1 : 0);
        shipToAddress.StateProvince = num8 != 0 ? "Western Australia" : "-";
        int num9 = dataSet.Tables[0].Rows[0]["ship_zip"] == null ? 1 : (string.IsNullOrEmpty(dataSet.Tables[0].Rows[0]["ship_zip"].ToString().Trim()) ? 1 : 0);
        shipToAddress.PostalCode = num9 != 0 ? "6104" : dataSet.Tables[0].Rows[0]["ship_zip"].ToString();
        int num10 = dataSet.Tables[0].Rows[0]["ship_country_cd"] == null ? 1 : (string.IsNullOrEmpty(dataSet.Tables[0].Rows[0]["ship_country_cd"].ToString().Trim()) ? 1 : 0);
        shipToAddress.CountryCode = num10 != 0 ? "AU" : dataSet.Tables[0].Rows[0]["ship_country_cd"].ToString();
        purchaseOrder.ShipToAddress = shipToAddress;
        List<LineItem> lineItemList = new List<LineItem>();
        foreach (DataRow row in (InternalDataCollectionBase) dataSet.Tables[1].Rows)
        {
          LineItem lineItem = new LineItem();
          lineItem.ExternalID = row["line"].ToString();
          lineItem.LineNumber = row["line"].ToString();
          lineItem.SupplierPartID = row["part_no"].ToString();
          lineItem.AccountCode = row["AccountNo"].ToString();
          lineItem.Description = row["description"].ToString();
          lineItem.Quantity = row["qty_ordered"].ToString();
          lineItem.UnitPrice = row["unit_cost"].ToString();
          Allocation[] allocationArray = new Allocation[1]
          {
            new Allocation()
            {
              Amount = row["extCost"].ToString(),
              Percentage = "100",
              GrossAmount = row["extCost"].ToString()
            }
          };
          lineItem.Allocation = allocationArray;
          lineItemList.Add(lineItem);
        }
        LineItem[] array = lineItemList.ToArray();
        purchaseOrder.LineItem = ((IEnumerable<LineItem>) array).Count<LineItem>() > 0 ? array : throw new Exception("Line Items Array could not be built");
        if (!POHelper.FindPurchaseOrder(token, url, poNum))
          POHelper.CreateNewPO(url, token, purchaseOrder);
        else
          POHelper.UpdatePO(url, token, poNum, purchaseOrder);
      }
      catch
      {
        throw;
      }
    }

    public static void SyncPO(
      string connectionstring,
      string poNum,
      string policyExternalId,
      string companyId,
      string dummyShipToExternalId,
      string token,
      string url)
    {
      DataSet dataSet = new DataSet();
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendLine("Starting process");
      try
      {
        using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(string.Format("{0};{1}", (object) string.Format("Select a.PONum,a.CurrencyCode,b.VendorID,Convert(date,a.OrderDate) as date_of_order , a.ShipName\r\n\t                                            , a.ShipAddress1,a.ShipAddress2,a.ShipAddress3,a.ShipCity, a.ShipState,a.DueDate, a.ShipZIP,a.ShipCountryNum, e.ISOCode as shipCountryIsoCode\r\n\t                                            , a.PurPoint, c.Address1, c.Address2, c.Address3, c.City, c.State, c.Zip, c.CountryNum, d.ISOCode\r\n\t                                            , a.TotalTax, a.OpenOrder \r\n\t                                            --, armOrder.LocationCode, location.GLSegmentCode1, location.GLSegmentCode2, location.GLSegmentCode3 \r\n                                                ,f.SegValue2 as GLSegmentCode2, f.SegValue3 as GLSegmentCode3 \r\n\t                                            from Erp.POHeader a\r\n\t\t                                            Inner Join Erp.Vendor b on (a.Company = b.Company and a.VendorNum = b.VendorNum)\r\n\t\t                                            Inner Join Erp.VendorPP c on (a.Company = c.Company and a.VendorNum = c.VendorNum and a.PurPoint = c.PurPoint)\t\r\n\t\t                                            Left Outer Join Erp.Country d on (d.Company = c.Company and c.CountryNum = d.CountryNum)\r\n\t\t                                            Left Outer Join Erp.Country e on (e.Company = a.Company and e.CountryNum = a.ShipCountryNum) \r\n\t                                            --Left Outer Join ARM.dbo.ProFormaOrder armOrder on ( a.PONum = armOrder.PurchaseOrderNo)\r\n\t\r\n\t                                            --Left Outer Join ARM.dbo.Location on (armOrder.LocationCode = location.LocationCode)\r\n\r\n                                                Outer Apply (Select Top 1 Company, Cast(Key1 as integer) as PONum, SegValue2,SegValue3 from Erp.TranGLC Where Company = a.Company and Key1 = a.PONum and RelatedToFile = 'PORel') as f \r\n\r\n\t                                            where a.Company = '{0}' and a.PONum = {1}", (object) companyId, (object) poNum), (object) string.Format("Select POLine,PartNum,LineDesc,OrderQty,UnitCost,currencycode, PODetail.TotalTax\r\n\t                                            ,podetail.ExtCost , GLAccountDetails.SegValue1 as AccountNo \r\n\t                                            from PODetail \r\n\t                                            Inner Join POHeader on (PODetail.Company = POHeader.Company and PODetail.PONUM = POHeader.PONum) \r\n                                                Outer Apply (Select Top 1 Company, Cast(Key1 as integer) as PONum, SegValue1,SegValue2,SegValue3, GLAccount from Erp.TranGLC Where Company = PODetail.Company and Key1 = PODetail.PONum and Key2 = PODetail.POLine and RelatedToFile = 'PORel') as GLAccountDetails \r\n\t                                            where PODetail.Company = '{0}' and PODetail.PONUM = {1}", (object) companyId, (object) poNum)), connectionstring))
          sqlDataAdapter.Fill(dataSet);
        if (dataSet != null && (dataSet.Tables[0] == null || dataSet.Tables[0].Rows.Count <= 0))
          throw new Exception(string.Format("PO Not found PONum: {0}", (object) poNum));
        if (dataSet != null && (dataSet.Tables[1] == null || dataSet.Tables[1].Rows.Count <= 0))
          throw new Exception(string.Format("PO Lines Not found PONum: {0}", (object) poNum));
        stringBuilder.AppendLine(string.Format("Record retrieved. PO Header : {0}, PoLines : {1}", (object) dataSet.Tables[0].Rows.Count, (object) dataSet.Tables[1].Rows.Count));
        PurchaseOrder purchaseOrder = new PurchaseOrder();
        purchaseOrder.PurchaseOrderNumber = dataSet.Tables[0].Rows[0]["PONum"].ToString();
        purchaseOrder.PolicyExternalID = policyExternalId;
        purchaseOrder.CurrencyCode = dataSet.Tables[0].Rows[0]["CurrencyCode"].ToString();
        purchaseOrder.VendorCode = dataSet.Tables[0].Rows[0]["VendorID"].ToString();
        purchaseOrder.VendorAddressCode = dataSet.Tables[0].Rows[0]["VendorID"].ToString();
        purchaseOrder.OrderDate = Convert.ToDateTime(dataSet.Tables[0].Rows[0]["date_of_order"]).ToString("yyyy-MM-dd");
        purchaseOrder.Name = dataSet.Tables[0].Rows[0]["PONum"].ToString();
        purchaseOrder.Description = dataSet.Tables[0].Rows[0]["PONum"].ToString();
        stringBuilder.AppendLine("PO Header created");
        string empty1 = string.Empty;
        string empty2 = string.Empty;
        string empty3 = string.Empty;
        if (dataSet.Tables[0].Rows[0]["GLSegmentCode2"] != null)
          empty1 = dataSet.Tables[0].Rows[0]["GLSegmentCode2"].ToString();
        if (dataSet.Tables[0].Rows[0]["GLSegmentCode3"] != null)
          empty3 = dataSet.Tables[0].Rows[0]["GLSegmentCode3"].ToString();
        if (string.IsNullOrEmpty(empty1) || string.IsNullOrEmpty(empty3))
          throw new Exception(string.Format("Segment Codes are not valid for PO ({0})", (object) poNum));
        stringBuilder.AppendLine("Location record processed.");
        if (dataSet.Tables[0].Rows[0]["OpenOrder"] != null && !Convert.ToBoolean(dataSet.Tables[0].Rows[0]["OpenOrder"]))
          purchaseOrder.Status = "Closed";
        purchaseOrder.ReceiptType = "WQTY";
        purchaseOrder.Shipping = "0";
        purchaseOrder.Tax = dataSet.Tables[0].Rows[0]["TotalTax"].ToString();
        BillToAddress billToAddress = new BillToAddress();
        billToAddress.ExternalID = dataSet.Tables[0].Rows[0]["PONum"].ToString();
        billToAddress.Name = dataSet.Tables[0].Rows[0]["PurPoint"].ToString();
        billToAddress.Address1 = dataSet.Tables[0].Rows[0]["Address1"].ToString();
        billToAddress.Address2 = dataSet.Tables[0].Rows[0]["Address2"].ToString();
        billToAddress.Address3 = dataSet.Tables[0].Rows[0]["Address3"].ToString();
        int num1 = dataSet.Tables[0].Rows[0]["City"] == null ? 1 : (string.IsNullOrEmpty(dataSet.Tables[0].Rows[0]["City"].ToString().Trim()) ? 1 : 0);
        billToAddress.City = num1 != 0 ? "Perth" : dataSet.Tables[0].Rows[0]["City"].ToString();
        int num2 = dataSet.Tables[0].Rows[0]["State"] == null ? 1 : (string.IsNullOrEmpty(dataSet.Tables[0].Rows[0]["State"].ToString().Trim()) ? 1 : 0);
        billToAddress.State = num2 != 0 ? "WA" : dataSet.Tables[0].Rows[0]["State"].ToString();
        int num3 = dataSet.Tables[0].Rows[0]["State"] == null ? 1 : (string.IsNullOrEmpty(dataSet.Tables[0].Rows[0]["State"].ToString().Trim()) ? 1 : 0);
        billToAddress.StateProvince = num3 != 0 ? "Western Australia" : "-";
        int num4 = dataSet.Tables[0].Rows[0]["Zip"] == null ? 1 : (string.IsNullOrEmpty(dataSet.Tables[0].Rows[0]["Zip"].ToString().Trim()) ? 1 : 0);
        billToAddress.PostalCode = num4 != 0 ? "6104" : dataSet.Tables[0].Rows[0]["Zip"].ToString();
        int num5 = dataSet.Tables[0].Rows[0]["ISOCode"] == null ? 1 : (string.IsNullOrEmpty(dataSet.Tables[0].Rows[0]["ISOCode"].ToString().Trim()) ? 1 : 0);
        billToAddress.CountryCode = num5 != 0 ? "AU" : dataSet.Tables[0].Rows[0]["ISOCode"].ToString();
        purchaseOrder.BillToAddress = billToAddress;
        ShipToAddress shipToAddress = new ShipToAddress();
        shipToAddress.ExternalID = dataSet.Tables[0].Rows[0]["PONum"].ToString();
        shipToAddress.Name = dataSet.Tables[0].Rows[0]["ShipName"].ToString();
        shipToAddress.Address1 = dataSet.Tables[0].Rows[0]["ShipAddress1"].ToString();
        shipToAddress.Address2 = dataSet.Tables[0].Rows[0]["ShipAddress2"].ToString();
        shipToAddress.Address3 = dataSet.Tables[0].Rows[0]["ShipAddress3"].ToString();
        int num6 = dataSet.Tables[0].Rows[0]["ShipCity"] == null ? 1 : (string.IsNullOrEmpty(dataSet.Tables[0].Rows[0]["ShipCity"].ToString().Trim()) ? 1 : 0);
        shipToAddress.City = num6 != 0 ? "Perth" : dataSet.Tables[0].Rows[0]["ShipCity"].ToString();
        int num7 = dataSet.Tables[0].Rows[0]["ShipState"] == null ? 1 : (string.IsNullOrEmpty(dataSet.Tables[0].Rows[0]["ShipState"].ToString().Trim()) ? 1 : 0);
        shipToAddress.State = num7 != 0 ? "WA" : dataSet.Tables[0].Rows[0]["ShipState"].ToString();
        int num8 = dataSet.Tables[0].Rows[0]["ShipState"] == null ? 1 : (string.IsNullOrEmpty(dataSet.Tables[0].Rows[0]["ShipState"].ToString().Trim()) ? 1 : 0);
        shipToAddress.StateProvince = num8 != 0 ? "Western Australia" : "-";
        int num9 = dataSet.Tables[0].Rows[0]["ShipZip"] == null ? 1 : (string.IsNullOrEmpty(dataSet.Tables[0].Rows[0]["ShipZip"].ToString().Trim()) ? 1 : 0);
        shipToAddress.PostalCode = num9 != 0 ? "6104" : dataSet.Tables[0].Rows[0]["ShipZip"].ToString();
        int num10 = dataSet.Tables[0].Rows[0]["shipCountryIsoCode"] == null ? 1 : (string.IsNullOrEmpty(dataSet.Tables[0].Rows[0]["shipCountryIsoCode"].ToString().Trim()) ? 1 : 0);
        shipToAddress.CountryCode = num10 != 0 ? "AU" : dataSet.Tables[0].Rows[0]["shipCountryIsoCode"].ToString();
        purchaseOrder.ShipToAddress = shipToAddress;
        List<LineItem> lineItemList = new List<LineItem>();
        foreach (DataRow row in (InternalDataCollectionBase) dataSet.Tables[1].Rows)
        {
          LineItem lineItem = new LineItem();
          lineItem.ExternalID = row["POLine"].ToString();
          lineItem.LineNumber = row["POLine"].ToString();
          lineItem.SupplierPartID = row["PartNum"].ToString();
          lineItem.AccountCode = row["AccountNo"].ToString();
          lineItem.Description = row["LineDesc"].ToString();
          lineItem.Quantity = row["OrderQty"].ToString();
          lineItem.UnitPrice = row["UnitCost"].ToString();
          Allocation[] allocationArray = new Allocation[1]
          {
            new Allocation()
            {
              Amount = row["ExtCost"].ToString(),
              Percentage = "100",
              GrossAmount = row["ExtCost"].ToString()
            }
          };
          lineItem.Allocation = allocationArray;
          lineItemList.Add(lineItem);
        }
        LineItem[] array = lineItemList.ToArray();
        purchaseOrder.LineItem = ((IEnumerable<LineItem>) array).Count<LineItem>() > 0 ? array : throw new Exception("Line Items Array could not be built");
        if (!POHelper.FindPurchaseOrder(token, url, poNum))
          POHelper.CreateNewPO(url, token, purchaseOrder);
        else
          POHelper.UpdatePO(url, token, poNum, purchaseOrder);
      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message + Environment.NewLine + stringBuilder.ToString(), ex.InnerException);
      }
    }
  }
}
