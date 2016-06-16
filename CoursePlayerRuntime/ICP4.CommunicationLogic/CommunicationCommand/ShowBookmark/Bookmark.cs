using System;
using System.Collections.Generic;
using System.Text;

namespace ICP4.CommunicationLogic.CommunicationCommand.ShowBookmark
{
    public class Bookmark : IDisposable
    {
        private int bookMarkID;

        public int BookMarkID
        {
            get { return bookMarkID; }
            set { bookMarkID = value; }
        }

        private string bookMarkTitle;

        public string BookMarkTitle
        {
            get { return bookMarkTitle; }
            set { bookMarkTitle = value; }
        }

        private string bookMarkDate;

        public string BookMarkDate
        {
            get { return bookMarkDate; }
            set { bookMarkDate = value; }
        }

        private string bookMarkTime;

        public string BookMarkTime
        {
            get { return bookMarkTime; }
            set { bookMarkTime = value; }
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion

    }
}
