using System;
using System.Collections.Generic;
using System.Text;

namespace ICP4.CommunicationLogic.CommunicationCommand.ValidationIdentityQuestion
{
    public class ValidationIdentityQuestion : IDisposable
    {
        private string templateHtml;
        public string TemplateHtml
        {
            get { return templateHtml; }
            set { templateHtml = value; }
        }   

        #region IDisposable Members
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
