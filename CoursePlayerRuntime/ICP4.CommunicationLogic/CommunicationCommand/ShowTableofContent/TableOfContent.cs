using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace ICP4.CommunicationLogic.CommunicationCommand.ShowTableofContent
{
   
    public class TableOfContent : IDisposable
    {


        private List<TOCItem> tocItems;
        public List<TOCItem> TOCItems
        {
            get { return tocItems; }
            set { tocItems = value; }
        }
        
        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
