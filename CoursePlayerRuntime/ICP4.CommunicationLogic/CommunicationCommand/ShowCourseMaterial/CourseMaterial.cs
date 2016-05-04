using System;
using System.Collections.Generic;
using System.Text;

namespace ICP4.CommunicationLogic.CommunicationCommand.ShowCourseMaterial
{
    public class CourseMaterial : IDisposable
    {
        private int courseMaterialID;

        public int CourseMaterialID
        {
            get { return courseMaterialID; }
            set { courseMaterialID = value; }
        }

        private string courseMaterialTitle;

        public string CourseMaterialTitle
        {
            get { return courseMaterialTitle; }
            set { courseMaterialTitle = value; }
        }


        private string courseMaterialURL;

        public string CourseMaterialURL
        {
            get { return courseMaterialURL; }
            set { courseMaterialURL = value; }
        }

        private string courseMaterialIconUrl;

        public string CourseMaterialIconUrl
        {
            get { return courseMaterialIconUrl; }
            set { courseMaterialIconUrl = value; }
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
