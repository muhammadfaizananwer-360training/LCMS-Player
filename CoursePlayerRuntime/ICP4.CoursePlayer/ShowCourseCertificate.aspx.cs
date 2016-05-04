using System;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;
using ICP4.BusinessLogic.CourseManager;

namespace ICP4.CoursePlayer
{
    public partial class ShowCourseCertificate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DownloadCertificate();
        }
        public void DownloadCertificate()
        {
            String assetURL = string.Empty;
            String fileName = string.Empty;
            PdfReader pdfReader = null;
            PdfStamper stamper = null;
            MemoryStream mStream = null;
            AcroFields acroFields = null;
            FileInfo file = null;
            try
            {
        int courseID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"]);

                using (ICP4.BusinessLogic.CourseManager.CourseManager courseManager = new CourseManager())
                {
                    assetURL = courseManager.GetCertificateInfo(courseID);
                    
                    file = new FileInfo(assetURL);
                    fileName = file.Name;

                    mStream = new MemoryStream();
                    assetURL = ConfigurationManager.AppSettings["ICPFileSystem"] + assetURL;

                    pdfReader = new PdfReader(assetURL);
                    stamper = new PdfStamper(pdfReader, mStream);
                    acroFields = stamper.AcroFields;
                    bool setTF = courseManager.SetFormPDFFileds(ref acroFields);

                    if (setTF)
                    {
                        stamper.FormFlattening = true;
                        stamper.Writer.CloseStream = false;
                        stamper.Close();
                        //var fi = new FileInfo(@"E:\_My Projects\PDFBookMarkreader\Certificates\courseCompletionCertificate.pdf");                                   


                        Response.Clear();
                        Response.ClearContent();
                        Response.ClearHeaders();
                                                
                        Response.Buffer = true;
                        
                        //Response.AddHeader("Expires", "0");
                        Response.AddHeader("Cache-Control", "must-revalidate, post-check=0, pre-check=0");
                        //Response.AddHeader("Content-Type", "application/force-download");
                        //Response.AddHeader("Content-Type", "application/octet-stream");
                        Response.ContentType = "application/pdf";
                        Response.AddHeader("Pragma", "public");
                        Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName.ToString());
                        //Response.AddHeader("X-Download-Options", "noopen "); // For IE8
                        //Response.AddHeader("X-Content-Type-Options","nosniff");// For IE8
                        Response.AddHeader("content-length", mStream.Length.ToString());
                         
                        
                        Response.BinaryWrite(mStream.GetBuffer());
                        //Response.End();
                        Response.Flush();
                        Response.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message + "<br />" + ex.StackTrace);
            }
            finally
            {
                if (stamper != null)
                {
                    stamper.Close();
                }
                if (pdfReader != null)
                {
                    pdfReader.Close();
                }
                mStream.Close();
            }
        }
    }
}
