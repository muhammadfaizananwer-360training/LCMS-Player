using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace ICP4.CommunicationLogic.CommunicationCommand.ShowResourceInfo
{
   
    public class ResourceInfo: IDisposable
    {
        private string resourceKey;

        public string ResourceKey
        {
            get { return resourceKey; }
            set { resourceKey = value; }
        }
        private string resourceValue;

        public string ResourceValue
        {
            get { return resourceValue; }
            set { resourceValue = value; }
        }
      


        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion


        
    }
}
