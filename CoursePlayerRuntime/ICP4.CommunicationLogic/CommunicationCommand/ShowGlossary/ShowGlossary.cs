using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;


namespace ICP4.CommunicationLogic.CommunicationCommand.ShowGlossary
{
    [XmlRootAttribute("Command")]
    public class ShowGlossary
    {
        private string commandName;

        [XmlAttribute] 
        public string CommandName
        {
            get { return commandName; }
            set { commandName = value; }
        }

        private List<Glossary> glossaries;
        [XmlArrayAttribute("CommandData")]
        public List<Glossary> Glossaries
        {
            get { return glossaries; }
            set { glossaries = value; }
        }
    }
}
