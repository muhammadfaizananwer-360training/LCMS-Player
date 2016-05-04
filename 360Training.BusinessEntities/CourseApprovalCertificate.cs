using System;
using System.Collections.Generic;
using System.Text;

namespace _360Training.BusinessEntities
{
    public class CourseApprovalCertificate
    {
        private int certificateID;
        public int CertificateID
        {
            get { return certificateID; }
            set { certificateID = value; }
        }

        private bool certificateenabled;
        public bool CertificateEnabled
        {
            get { return certificateenabled; }
            set { certificateenabled = value; }
        }
    }
}
