using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Threading;
using System.Collections.Generic;
using System.Text;
using ICP4.BusinessLogic.CourseManager;
using _360Training.BusinessEntities;
using ICP4.BusinessLogic.DocuSignManager;

namespace ICP4.CoursePlayer
{
    public partial class OpenEmbeddedDocuSign : System.Web.UI.Page
    {
        DocuSignLearner dsld = null;
        protected string signerMessage;
        DocuSignHelper dsh = new DocuSignHelper();
        protected string docuSignUrl;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (Session["DocuSignLearnerData"] == null)
                {
                    using (CourseManager courseManager = new CourseManager())
                    {
                        dsld = courseManager.GetLearnerData();
                        Session["DocuSignLearnerData"] = dsld;
                    }
                }
                
            }
            if (!IsPostBack)
            {

                ManageEmbeddedDocuSign();


                if (!string.IsNullOrEmpty(docuSignUrl))
                {
                    signerMessage = "Have the signer fill out the Envelope";

                    Response.Redirect(docuSignUrl, true);
                }
            }
        }
        public void ManageEmbeddedDocuSign()
        {
           
            while (DocuSignStateHelper.TryCount < 3)
            {
                try
                {
                    if (!dsh.LoggedIn())
                    {
                        if (dsh.DoLogin())
                        {

                        }
                    }
                    docuSignUrl = dsh.DocuSignURL();
                    break;
                }
                catch (Exception ex1)
                {
                    ExceptionPolicyForLCMS.HandleException(ex1, "ICPException");
                 
                    if (DocuSignStateHelper.TryCount == 0)
                    {
                        DocuSignStateHelper.TryCount += 1;
                        //Response.Redirect(ResolveUrl("Alternate.aspx"), true);
                        
                        //ClientScript.RegisterStartupScript(Page.GetType(), "script", "DocuSignDownMesage();", false);
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "DocuSignDownMesage();", true);

                        break;
                    }
                    else
                    {
                        DocuSignStateHelper.TryCount += 1;
                        Thread.Sleep(15000);
                    }

                    
                    
                }
            }
            if (DocuSignStateHelper.TryCount >= 3)
            {
                Response.Redirect(ResolveUrl("Redirector.aspx?op=DocuSignDown"), true);
               
            }
            
        }

        protected void btnTryAgain_Click(object sender, EventArgs e)
        {
            ManageEmbeddedDocuSign();
            if (!string.IsNullOrEmpty(docuSignUrl))
            {
                signerMessage = "Have the signer fill out the Envelope";

                Response.Redirect(docuSignUrl, true);
            }
        }
        
    }
}

