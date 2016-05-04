using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace ICP4.CommunicationLogic.CommunicationCommand.ShowLocalizedInfo
{
   
    public class LocalizedInfo : IDisposable
    {
        private string headingTOC;

        public string HeadingTOC
        {
            get { return headingTOC; }
            set { headingTOC = value; }
        }
        private string headingGlossary;

        public string HeadingGlossary
        {
            get { return headingGlossary; }
            set { headingGlossary = value; }
        }
        private string headingCourseMaterial;

        public string HeadingCourseMaterial
        {
            get { return headingCourseMaterial; }
            set { headingCourseMaterial = value; }
        }
        private string headingBookmark;

        public string HeadingBookmark
        {
            get { return headingBookmark; }
            set { headingBookmark = value; }
        }
        private string imageCompanyLogo;

        public string ImageCompanyLogo
        {
            get { return imageCompanyLogo; }
            set { imageCompanyLogo = value; }
        }
        private string imageLogoutButton;

        public string ImageLogoutButton
        {
            get { return imageLogoutButton; }
            set { imageLogoutButton = value; }
        }


        private string loadingImage;

        public string LoadingImage
        {
            get { return loadingImage; }
            set { loadingImage = value; }
        }

        private string headingFooterNote;

        public string HeadingFooterNote
        {
            get { return headingFooterNote; }
            set { headingFooterNote = value; }
        } 



        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion


        
    }
}
