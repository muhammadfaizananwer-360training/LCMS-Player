using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;


namespace ICP4.CommunicationLogic.CommunicationCommand.ShowTableofContent
{
    [XmlRootAttribute("Command")]
    public class ShowTableofContent : IDisposable
    {
        private string commandName;
        [XmlAttribute] 
        public string CommandName
        {
            get { return commandName; }
            set { commandName = value; }
        }

        private TableOfContent tableOfContent;
        [XmlElement("CommandData")]
        public TableOfContent TableOfContent
        {
            get { return tableOfContent; }
            set { tableOfContent = value; }
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion


    }
}
