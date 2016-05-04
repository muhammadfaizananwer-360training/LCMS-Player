using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace ICP4.CommunicationLogic.CommunicationCommand.ShowCourseMaterial
{
    [XmlRootAttribute("Command")]
    public class ShowSupplementalMaterial : IDisposable
    {
        private string commandName;
        [XmlAttribute] 
        public string CommandName
        {
            get { return commandName; }
            set { commandName = value; }
        }

        private List<SupplementalMaterial> supplimentalMaterials;
        [XmlArrayAttribute("CommandData")]
        public List<SupplementalMaterial> SupplimentalMaterials
        {
            get { return supplimentalMaterials; }
            set { supplimentalMaterials = value; }
        }


        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
