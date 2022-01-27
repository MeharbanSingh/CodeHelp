// Decompiled with JetBrains decompiler
// Type: pbsConcurWebServices.PurchaseOrder
// Assembly: pbsConcurWebServices, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A862484-1C34-44D9-898F-45BB4053517A
// Assembly location: C:\Users\mehar.singh\OneDrive - Precise Business Solutions\Desktop\pbsConcurWebServices.dll

using System.IO;
using System.Xml.Serialization;

namespace pbsConcurWebServices
{
  [XmlRoot("PurchaseOrder")]
  public class PurchaseOrder
  {
    public string AmountWithoutVat { get; set; }

    public BillToAddress BillToAddress { get; set; }

    public string CurrencyCode { get; set; }

    public string Custom1 { get; set; }

    public string Custom10 { get; set; }

    public string Custom11 { get; set; }

    public string Custom12 { get; set; }

    public string Custom13 { get; set; }

    public string Custom14 { get; set; }

    public string Custom15 { get; set; }

    public string Custom16 { get; set; }

    public string Custom17 { get; set; }

    public string Custom18 { get; set; }

    public string Custom19 { get; set; }

    public string Custom2 { get; set; }

    public string Custom20 { get; set; }

    public string Custom21 { get; set; }

    public string Custom22 { get; set; }

    public string Custom23 { get; set; }

    public string Custom24 { get; set; }

    public string Custom3 { get; set; }

    public string Custom4 { get; set; }

    public string Custom5 { get; set; }

    public string Custom6 { get; set; }

    public string Custom7 { get; set; }

    public string Custom8 { get; set; }

    public string Custom9 { get; set; }

    public string Description { get; set; }

    public string DiscountPercentage { get; set; }

    public string DiscountTerms { get; set; }

    public string ID { get; set; }

    [XmlElement("LineItem")]
    public pbsConcurWebServices.LineItem[] LineItem { get; set; }

    public string Name { get; set; }

    public string NeededByDate { get; set; }

    public string OrderDate { get; set; }

    public string PaymentTerms { get; set; }

    public string PolicyExternalID { get; set; }

    public string PoVendorTaxId { get; set; }

    public string ProvincialTaxId { get; set; }

    public string PurchaseOrderNumber { get; set; }

    public string ReceiptType { get; set; }

    public string RequestedBy { get; set; }

    public string RequestedDeliveryDate { get; set; }

    public string Shipping { get; set; }

    public string ShippingDescription { get; set; }

    public string ShippingMethodKey { get; set; }

    public string ShippingTermsKey { get; set; }

    public ShipToAddress ShipToAddress { get; set; }

    public string Status { get; set; }

    public string Tax { get; set; }

    public string URI { get; set; }

    public string VatAmountOne { get; set; }

    public string VatAmountTwo { get; set; }

    public string VatRateOne { get; set; }

    public string VatRateTwo { get; set; }

    public string VendorAccountNumber { get; set; }

    public string VendorAddressCode { get; set; }

    public string VendorCode { get; set; }

    public static PurchaseOrder FromXmlString(string xml)
    {
      PurchaseOrder purchaseOrder = (PurchaseOrder) null;
      using (TextReader textReader = (TextReader) new StringReader(xml))
        purchaseOrder = (PurchaseOrder) new XmlSerializer(typeof (PurchaseOrder)).Deserialize(textReader);
      return purchaseOrder;
    }
  }
}
