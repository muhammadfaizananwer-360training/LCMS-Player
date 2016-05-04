using System;
using System.Collections.Generic;
using System.Text;

namespace _360Training.BusinessEntities
{
    public class LegacyCertificateInfo
    {  
        private string certificateURL;

        public string CertificateURL
        {
            get { return certificateURL; }
            set { certificateURL = value; }
        }

        private int asvId;

        public int AsvId
        {
            get { return asvId; }
            set { asvId = value; }
        }

        private bool certificateExists;

        public bool CertificateExists
        {
            get { return certificateExists; }
            set { certificateExists = value; }
        }
    }

  
}
