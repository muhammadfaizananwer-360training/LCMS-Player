using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace ICP4.CommunicationLogic.CommunicationCommand.ShowTableofContent
{
   
    public class TableContentLesson : IDisposable
    {
        private int lessonID;

        [XmlAttribute] 
        public int LessonID
        {
            get { return lessonID; }
            set { lessonID = value; }
        }
        private string lessonName;

        public string LessonName
        {
            get { return lessonName; }
            set { lessonName = value; }
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
