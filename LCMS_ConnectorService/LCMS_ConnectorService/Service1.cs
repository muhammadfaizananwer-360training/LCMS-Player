using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.Timers;
using System.Configuration;
using System.Data;

namespace LCMS_ConnectorService
{
    public partial class Service1 : ServiceBase
    {
        //Initialize the timer
        Timer timer = new Timer();
        LegacyData legacyData = null;
        int course_id;
        int student_id;
        int epoch;
        string playerVersion;
        string learningSessionGuid;
        


        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                Trace.WriteLine("OnStart");
                Trace.Flush();

                timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
                timer.Interval = Convert.ToInt64(ConfigurationManager.AppSettings["TimeInterval"]);
                timer.Enabled = true;
                legacyData = new LegacyData();
                
                Trace.WriteLine("OnStart1");
                Trace.Flush();
            }
            catch (Exception exp)
            {
                Trace.WriteLine(exp.ToString());
                Trace.Flush();
            }

        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            DataTable dataTable = legacyData.Get_LCMS_StudentCourse();
            foreach(DataRow dataRow in dataTable.Rows)
            {
                course_id = Convert.ToInt32(dataRow["course_id"]);
                student_id = Convert.ToInt32(dataRow["student_id"]);
                epoch = Convert.ToInt32(dataRow["epoch"]);
                playerVersion = Convert.ToString(dataRow["playerVersion"]);
                learningSessionGuid = Convert.ToString(dataRow["learningSession_id"]);
                
                //Tracking Service
                TrackingService.TrackingService trackingService = new LCMS_ConnectorService.TrackingService.TrackingService();
                trackingService.Url = ConfigurationManager.AppSettings["TrackingServiceURL"];
                
                trackingService.LegacyStatsRecorderCompleted += new LCMS_ConnectorService.TrackingService.LegacyStatsRecorderCompletedEventHandler(trackingService_LegacyStatsRecorderCompleted);
                trackingService.LegacyStatsRecorderAsync(learningSessionGuid, course_id, student_id, epoch, playerVersion, 0);

                Trace.WriteLine("course_id:" + course_id + ", student_id:" + student_id + ",epoch:" + epoch + ",learningSessionGuid:" + learningSessionGuid + ",playerVersion:" + playerVersion + " : TimeStamp : " + DateTime.Now.ToString());
                Trace.Flush();
            }
        }

        void trackingService_LegacyStatsRecorderCompleted(object sender, AsyncCompletedEventArgs e)
        {
            Trace.WriteLine("Completed");
            Trace.Flush();
        }

        protected override void OnStop()
        {
            timer.Enabled = false;
            Trace.WriteLine("OnStop");
            Trace.Flush();

        }
    }
}
