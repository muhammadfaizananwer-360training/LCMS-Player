using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace ICP4.CommunicationLogic.CommunicationCommand.ShowCourseMaterial
{
    [XmlRootAttribute("Command")]
    public class ShowCourseMaterial : IDisposable
    {
        private string commandName;
        [XmlAttribute] 
        public string CommandName
        {
            get { return commandName; }
            set { commandName = value; }
        }

        private List<CourseMaterial> courseMaterials;
        [XmlArrayAttribute("CommandData")]
        public List<CourseMaterial> CourseMaterials
        {
            get { return courseMaterials; }
            set { courseMaterials = value; }
        }


        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
