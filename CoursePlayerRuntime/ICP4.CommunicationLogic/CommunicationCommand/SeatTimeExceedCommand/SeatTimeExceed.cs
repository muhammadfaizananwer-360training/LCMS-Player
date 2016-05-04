using System;
using System.Collections.Generic;
using System.Text;

namespace ICP4.CommunicationLogic.CommunicationCommand.SeatTimeExceedCommand
{
    public class SeatTimeExceedMessage
    {
        private string templateHtml;
        public string TemplateHtml
        {
            get { return templateHtml; }
            set { templateHtml = value; }
        }
    }
}
