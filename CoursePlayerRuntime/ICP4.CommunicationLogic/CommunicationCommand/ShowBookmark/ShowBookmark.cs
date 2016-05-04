using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace ICP4.CommunicationLogic.CommunicationCommand.ShowBookmark
{
    [XmlRootAttribute("Command")]
    public class ShowBookmark : IDisposable
    {
        private string commandName;
        [XmlAttribute]
        public string CommandName
        {
            get { return commandName; }
            set { commandName = value; }
        }

        private List<Bookmark> bookmarks;
        [XmlArrayAttribute("CommandData")]
        public List<Bookmark> Bookmarks
        {
            get { return bookmarks; }
            set { bookmarks = value; }
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
