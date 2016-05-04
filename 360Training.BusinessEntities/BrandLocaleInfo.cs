using System;
using System.Collections.Generic;
using System.Text;

namespace _360Training.BusinessEntities
{
    public class BrandLocaleInfo
    {

        private int brandID;

        public int BrandID
        {
            get { return brandID; }
            set { brandID = value; }
        }
        
        private string brandName;
        public string BrandName
        {
            get { return brandName; }
            set { brandName = value; }
        }
        private int localeID;
        public int LocaleID
        {
            get { return localeID; }
            set { localeID = value; }
        }
        private List<LocaleResource> localeResourceList;

        public List<LocaleResource> LocaleResourceList
        {
            get { return localeResourceList; }
            set { localeResourceList = value; }
        }
 
    }
}
