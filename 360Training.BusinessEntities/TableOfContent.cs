using System;
using System.Collections.Generic;
using System.Text;

namespace _360Training.BusinessEntities
{
    public class TableOfContent
    {
        //private List<ContentObject> contentObjects;
        //public List<ContentObject> ContentObjects
        //{
        //    get { return contentObjects; }
        //    set { contentObjects=value; }
        //}

        private List<TOCItem> tocitems;
        public List<TOCItem> TOCItems
        {
            get { return tocitems; }
            set { tocitems = value; }
        }
    }
}
