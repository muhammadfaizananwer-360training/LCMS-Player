using System;
using System.Collections.Generic;
using System.Text;
 
namespace ICP4.CommunicationLogic.CommunicationCommand.ShowEmbeddedAcknowledgment
{
    public class EmbeddedAcknowledgment : IDisposable
    {
        private string templateHtml;
        /// <summary>
        /// Template HTML to render
        /// </summary>
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
