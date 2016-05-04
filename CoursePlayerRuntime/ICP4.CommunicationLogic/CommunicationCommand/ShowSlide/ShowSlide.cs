using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace ICP4.CommunicationLogic.CommunicationCommand.ShowSlide
{
   
    [XmlRootAttribute("Command")]
    public class ShowSlide : IDisposable
    {
        private string commandName;

        [XmlAttribute] 
        public string CommandName
        {
            get { return commandName; }
            set { commandName = value; }
        }

        private SlideMediaAsset mediaAsset;
        [XmlElement("CommandData")]
        public SlideMediaAsset MediaAsset
        {
            get { return mediaAsset; }
            set { mediaAsset = value; }
        }


        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion

    }
}
