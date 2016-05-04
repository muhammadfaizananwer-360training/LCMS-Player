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
using ICP4.CommunicationLogic.CommunicationCommand;
using ICP4.BusinessLogic.CacheManager;
using ICP4.BusinessLogic.ICPCourseService;
using ICP4.BusinessLogic.ICPAssessmentService;
using _360Training.BusinessEntities;
using ICP4.BusinessLogic.IntegerationManager;
using ICP4.BusinessLogic.ICPTrackingService;
using System.Text;

namespace ICP4.CoursePlayer
{
    public partial class ViewAssessmentResult : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    if (Request.QueryString["enrollmentID"] != null)
                    {
                        hdnEnrollmentID.Value = Request.QueryString["enrollmentID"].ToString();
                    }
                    AssessmentResult();
                }
            }
            catch (Exception exp)
            {
                Response.Write(exp.ToString());
            }
        }

        private void AssessmentResult()
        {
            string brandCode = null;
            string variant = null;

            try
            {
                ICP4.BusinessLogic.ICPAssessmentService.AssessmentItemResult[] assessmentItemResults;
                StringBuilder sb = new StringBuilder();
 
                string CorrectAnswerChoiceLabel = string.Empty;
                string CorrectAnswerChoiceColor = string.Empty;
                string CorrectAnswerChoiceHighlightColor = string.Empty;
                string IncorrectAnswerChoiceLabel = string.Empty;
                string IncorrectAnswerChoiceColor = string.Empty;
                string IncorrectAnswerChoiceHighlightColor = string.Empty;
                string assessmentResultContent = string.Empty;
                string assessmentResultContentPass = string.Empty;
                string assessmentResultContentFail = string.Empty;

                ICP4.BusinessLogic.ICPTrackingService.TrackingService trackingService = new ICP4.BusinessLogic.ICPTrackingService.TrackingService();
                trackingService.Url = ConfigurationManager.AppSettings["ICPTrackingService"];
                trackingService.Timeout = Convert.ToInt32(ConfigurationManager.AppSettings["ICPCourseServiceTimeout"]);
                brandCode = trackingService.GetEnrollmentIDBrandcodeVariant(Convert.ToInt32(hdnEnrollmentID.Value), out variant);

                using (ICP4.BusinessLogic.ICPAssessmentService.AssessmentService assessmentService = new ICP4.BusinessLogic.ICPAssessmentService.AssessmentService())
                {
                    assessmentService.Url = ConfigurationManager.AppSettings["ICPAssessmentService"];
                    assessmentService.Timeout = Convert.ToInt32(ConfigurationManager.AppSettings["ICPCourseServiceTimeout"]);
                    assessmentItemResults = assessmentService.GetLearnerAssessmentItemResults(Convert.ToInt32(hdnEnrollmentID.Value), _360Training.BusinessEntities.AssessmentConfiguration.ASSESSMENTYPE_POSTASSESSMET);
                }

                using (ICP4.BusinessLogic.CacheManager.CacheManager cacheManager = new ICP4.BusinessLogic.CacheManager.CacheManager())
                {

                    using (ICP4.BusinessLogic.ICPBrandingService.BrandingService brandingService = new ICP4.BusinessLogic.ICPBrandingService.BrandingService())
                    {

                        ICP4.BusinessLogic.ICPBrandingService.BrandLocaleInfo brandLocaleInfo = new ICP4.BusinessLogic.ICPBrandingService.BrandLocaleInfo();                    
                        brandingService.Url = System.Configuration.ConfigurationManager.AppSettings["ICPBrandingService"];
                        brandingService.Timeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["ICPCourseServiceTimeout"]);

                        brandLocaleInfo = cacheManager.GetIFBrandInfoExistInCache(brandCode, variant);

                        if (brandLocaleInfo == null)
                        {
                            brandLocaleInfo = brandingService.GetBrandLocaleInfo(brandCode, variant);
                            cacheManager.CreateCourseBrandInfoInCache(brandCode, variant, brandLocaleInfo);
                        }
                    }
               
                    CorrectAnswerChoiceLabel = cacheManager.GetResourceValueByResourceKey(ICP4.BusinessLogic.BrandManager.ResourceKeyNames.ChartAssessmentResultPassLabel, brandCode, variant);
                    CorrectAnswerChoiceColor = cacheManager.GetResourceValueByResourceKey(ICP4.BusinessLogic.BrandManager.ResourceKeyNames.ChartAssessmentResultPassColor, brandCode, variant);
                    CorrectAnswerChoiceHighlightColor = cacheManager.GetResourceValueByResourceKey(ICP4.BusinessLogic.BrandManager.ResourceKeyNames.ChartAssessmentResultPassHighlightColor, brandCode, variant);
                    IncorrectAnswerChoiceLabel = cacheManager.GetResourceValueByResourceKey(ICP4.BusinessLogic.BrandManager.ResourceKeyNames.ChartAssessmentResultFailLabel, brandCode, variant);
                    IncorrectAnswerChoiceColor = cacheManager.GetResourceValueByResourceKey(ICP4.BusinessLogic.BrandManager.ResourceKeyNames.ChartAssessmentResultFailColor, brandCode, variant);
                    IncorrectAnswerChoiceHighlightColor = cacheManager.GetResourceValueByResourceKey(ICP4.BusinessLogic.BrandManager.ResourceKeyNames.ChartAssessmentResultFailHighlightColor, brandCode, variant);
                }

                string ChartData = string.Empty;
                string ChartScript = string.Empty;

                if (assessmentItemResults != null && assessmentItemResults.Length > 0)
                {
                    sb.Append("<table cellspacing=\"0\" cellpadding=\"1\" width=\"95%\" style=\"border-left:1px solid #eaeaea;border-right:1px solid #eaeaea;border-bottom:1px solid #eaeaea;border-top:1px solid #eaeaea;border-width:0px\">");
                    foreach (ICP4.BusinessLogic.ICPAssessmentService.AssessmentItemResult assessmentItemResult in assessmentItemResults)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td class='assessmentResultText assessmentResultborder' align='left'>&nbsp;&nbsp;&nbsp;" + assessmentItemResult.MajorCategory.ToString() + "</td>");
                        sb.Append("<td class='assessmentResultText assessmentResultborder' align='center'>" + string.Format("{0:0.00}", assessmentItemResult.AnswerCorrectPercentage) + " %</td>");
                        sb.Append("<td class='assessmentResultText assessmentResultborder' align='center' width='18%'><div id='canvasholder" + assessmentItemResult.AssessmentItemResultlID.ToString() + "'><canvas id='chartarea" + assessmentItemResult.AssessmentItemResultlID.ToString() + "' width='150' height='85'/></div></td>");
                        ChartData += "var pieData" + assessmentItemResult.AssessmentItemResultlID.ToString() + " = [{value: " + string.Format("{0:0.00}", assessmentItemResult.AnswerCorrectPercentage) + ",color:\"" + CorrectAnswerChoiceColor + "\",highlight: \"" + CorrectAnswerChoiceHighlightColor + "\",label: \"" + CorrectAnswerChoiceLabel + "\"},{value: " + string.Format("{0:0.00}", assessmentItemResult.AnswerInCorrectPercentage) + ",color: \"" + IncorrectAnswerChoiceColor + "\",highlight: \"" + IncorrectAnswerChoiceHighlightColor + "\",label: \"" + IncorrectAnswerChoiceLabel + "\"},];" + "\n";
                        ChartScript += "ctx = document.getElementById('chartarea" + assessmentItemResult.AssessmentItemResultlID.ToString() + "').getContext('2d');" + "\n new Chart(ctx).Pie(pieData" + assessmentItemResult.AssessmentItemResultlID.ToString() + ");";
                        sb.Append("</tr>");
                    }
                    sb.Append("<tr><td><script type='text/javascript'>var ctx;" + ChartData.ToString() + "\n" + ChartScript + "</script></td></tr>");
                    sb.Append("</table>");
                }
                ltrlAssessmentResult.Text = sb.ToString(); 
            }
            catch (Exception exp)
            {
                Response.Write(brandCode + " " + variant + exp.ToString());
            }
        }
    }
}
