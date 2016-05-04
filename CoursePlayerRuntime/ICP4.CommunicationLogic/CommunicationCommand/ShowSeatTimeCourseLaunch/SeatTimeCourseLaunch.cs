using System;
using System.Collections.Generic;
using System.Text;

namespace ICP4.CommunicationLogic.CommunicationCommand.ShowSeatTimeCourseLaunch
{
    public class SeatTimeCourseLaunch: IDisposable
    {
        private int maximumseattimeinhour;
        public int Maximumseattimeinhour
        {
            get { return maximumseattimeinhour; }
            set { maximumseattimeinhour = value; }
        }

        private int totalHoursSpentinDay;
        public int TotalHoursSpentinDay
        {
            get { return totalHoursSpentinDay; }
            set { totalHoursSpentinDay = value; }
        }

        private string maximumSeatTimeCourseLaunchHeading;
        public string MaximumSeatTimeCourseLaunchHeading
        {
            get { return maximumSeatTimeCourseLaunchHeading; }
            set { maximumSeatTimeCourseLaunchHeading = value; }
        }

        private string maximumSeatTimeCourseLaunchCustomMessage;
        public string MaximumSeatTimeCourseLaunchCustomMessage
        {
            get { return maximumSeatTimeCourseLaunchCustomMessage; }
            set { maximumSeatTimeCourseLaunchCustomMessage = value; }
        }

        private string templateHtml;
        public string TemplateHtml
        {
            get { return templateHtml; }
            set { templateHtml = value; }
        }

        private string maximumSeatTimeCourseLaunchImageUrl;
        public string MaximumSeatTimeCourseLaunchImageUrl
        {
            get { return maximumSeatTimeCourseLaunchImageUrl; }
            set { maximumSeatTimeCourseLaunchImageUrl = value; }
        }

        #region IDisposable Members
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
