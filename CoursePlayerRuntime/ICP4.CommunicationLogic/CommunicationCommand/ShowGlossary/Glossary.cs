using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace ICP4.CommunicationLogic.CommunicationCommand.ShowGlossary
{
    public class Glossary : IDisposable
    {
        private int glossaryID;
        [XmlAttribute] 
        public int GlossaryID
        {
            get { return glossaryID; }
            set { glossaryID = value; }
        }
        private string term;

        public string Term
        {
            get { return term; }
            set { term = value; }
        }


        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
       

    }
}
