using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICP4.BusinessLogic.CourseManager
{
    public class CourseImage
    {
    }

    public class Price
    {
        public string value { get; set; }
        public string currency { get; set; }
        public string usage { get; set; }
        public string description { get; set; }
    }

    public class Price2
    {
        public string value { get; set; }
        public string currency { get; set; }
        public string usage { get; set; }
        public string description { get; set; }
    }

    public class Price3
    {
        public string value { get; set; }
        public string currency { get; set; }
        public string usage { get; set; }
        public string description { get; set; }
    }

    public class SKU
    {
        public string catalogEntryTypeCode { get; set; }
        public string buyable { get; set; }
        public string longDescription { get; set; }
        public List<Price3> price { get; set; }
        public string resourceId { get; set; }
        public List<string> parentCatalogGroupID { get; set; }
        public string name { get; set; }
        public string thumbnail { get; set; }
        public string uniqueID { get; set; }
        public string manufacturer { get; set; }
        public bool hasSingleSKU { get; set; }
        public string mfPartNumber_ntk { get; set; }
        public string partNumber { get; set; }
        public string storeID { get; set; }
        public string parentCatalogEntryID { get; set; }
        public string fullImage { get; set; }
        public string shortDescription { get; set; }
        public string price_USD { get; set; }
    }

    public class Price4
    {
        public string value { get; set; }
        public string currency { get; set; }
        public string usage { get; set; }
        public string description { get; set; }
    }

    public class Component
    {
        public string longDescription { get; set; }
        public string catalogEntryTypeCode { get; set; }
        public string buyable { get; set; }
        public List<Price4> price { get; set; }
        public string resourceId { get; set; }
        public string quantity { get; set; }
        public List<string> parentCatalogGroupID { get; set; }
        public string name { get; set; }
        public string thumbnail { get; set; }
        public string uniqueID { get; set; }
        public string price_USD { get; set; }
        public string manufacturer { get; set; }
        public bool hasSingleSKU { get; set; }
        public string mfPartNumber_ntk { get; set; }
        public string partNumber { get; set; }
        public string storeID { get; set; }
        public string parentCatalogEntryID { get; set; }
        public string shortDescription { get; set; }
        public string fullImage { get; set; }
    }

    public class MerchandisingAssociation
    {
        public string buyable { get; set; }
        public string catalogEntryTypeCode { get; set; }
        public string longDescription { get; set; }
        public List<Price2> price { get; set; }
        public string resourceId { get; set; }
        public string singleSKUCatalogEntryID { get; set; }
        public double quantity { get; set; }
        public string name { get; set; }
        public List<string> parentCatalogGroupID { get; set; }
        public string thumbnail { get; set; }
        public string uniqueID { get; set; }
        public string fullImage { get; set; }
        public string price_USD { get; set; }
        public string manufacturer { get; set; }
        public List<SKU> sKUs { get; set; }
        public int numberOfSKUs { get; set; }
        public bool hasSingleSKU { get; set; }
        public string mfPartNumber_ntk { get; set; }
        public string associationType { get; set; }
        public string storeID { get; set; }
        public string partNumber { get; set; }
        public string shortDescription { get; set; }
        public List<Component> components { get; set; }
        public string parentCatalogEntryID { get; set; }
    }

    public class UserData
    {
        public string WSTORE_ID { get; set; }
        public string WLCMS_THUMBNAIL { get; set; }
    }

    public class Price5
    {
        public string value { get; set; }
        public string currency { get; set; }
        public string usage { get; set; }
        public string description { get; set; }
    }

    public class SKU2
    {
        public string catalogEntryTypeCode { get; set; }
        public string buyable { get; set; }
        public string longDescription { get; set; }
        public List<Price5> price { get; set; }
        public string resourceId { get; set; }
        public List<string> parentCatalogGroupID { get; set; }
        public string name { get; set; }
        public string thumbnail { get; set; }
        public string uniqueID { get; set; }
        public string manufacturer { get; set; }
        public bool hasSingleSKU { get; set; }
        public string mfPartNumber_ntk { get; set; }
        public string partNumber { get; set; }
        public string storeID { get; set; }
        public string parentCatalogEntryID { get; set; }
    }

    public class CatalogEntryView
    {
        public string longDescription { get; set; }
        public string catalogEntryTypeCode { get; set; }
        public string buyable { get; set; }
        public List<Price> price { get; set; }
        public string singleSKUCatalogEntryID { get; set; }
        public string resourceId { get; set; }
        public List<string> parentCatalogGroupID { get; set; }
        public string name { get; set; }
        public string uniqueID { get; set; }
        public string fullImage { get; set; }
        public string thumbnail { get; set; }
        public string price_USD { get; set; }
        public string manufacturer { get; set; }
        public List<MerchandisingAssociation> merchandisingAssociations { get; set; }
        public List<UserData> UserData { get; set; }
        public List<SKU2> sKUs { get; set; }
        public int numberOfSKUs { get; set; }
        public bool hasSingleSKU { get; set; }
        public string mfPartNumber_ntk { get; set; }
        public string partNumber { get; set; }
        public string storeID { get; set; }
    }

    public class MetaData
    {
        public string price { get; set; }
    }

    public class RootObject
    {
        public int recordSetTotalMatches { get; set; }
        public int recordSetTotal { get; set; }
        public string resourceName { get; set; }
        public string resourceId { get; set; }
        public int recordSetStartNumber { get; set; }
        public string recordSetComplete { get; set; }
        public int recordSetCount { get; set; }
        public List<CatalogEntryView> catalogEntryView { get; set; }
        public MetaData metaData { get; set; }
    }


}
