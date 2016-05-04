using System;
using System.Collections.Generic;
using System.Text;

namespace ICP4.CommunicationLogic.CommunicationCommand.ShowCourseMaterial
{
    public class SupplementalMaterial : IDisposable
    {
        private int supplementalMaterialID;

        public int SupplementalMaterialID
        {
            get { return supplementalMaterialID; }
            set { supplementalMaterialID = value; }
        }

        private string supplementalMaterialTitle;

        public string SupplementalMaterialTitle
        {
            get { return supplementalMaterialTitle; }
            set { supplementalMaterialTitle = value; }
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
