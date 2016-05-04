<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        // Code that runs on application startup

    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started
        PlayerUtil.debugMessage("Request Processed - Session Started");

        if (Request.QueryString["hb_source"]  != null && Request.QueryString["hb_source"].ToString().Equals("iframe"))
        {
            Session.Abandon();
        }
        
    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

        int course_id = Convert.ToInt32(Session["course_id"]);
        int student_id = Convert.ToInt32(Session["student_id"]);
        int epoch = Convert.ToInt32(Session["epoch"]);
        String learningSessionGUID = Session["learningSessionGUID"].ToString();
        
        TrackingService.TrackingService service = new TrackingService.TrackingService();
        service.LegacyStatsRecorder("", course_id, student_id, epoch, learningSessionGUID, 0);

        PlayerUtil.debugMessage( Session.SessionID + ": Request Processed : course_id =" + course_id + ", student_id=" + student_id + ", epcoh=" + epoch);
        

    }
       
</script>
