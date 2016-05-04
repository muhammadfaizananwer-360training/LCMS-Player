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
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;

namespace ICP4.CoursePlayer
{
    public partial class CacheInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack) 
            {
                if (hdnAuthentication.Value.Equals("true"))
                {
                    if (Request.QueryString["cacheitem"] != null && !Request.QueryString["cacheitem"].Equals("") && Request.QueryString["loggedin"] != null && !Request.QueryString["loggedin"].Equals(""))
                    {
                        hdnAuthentication.Value = "true";
                        displayCacheItems(Request.QueryString["cacheitem"].ToString());
                    }
                }
                else
                {
                    if (Request.QueryString["cacheitem"] != null && !Request.QueryString["cacheitem"].Equals("") && Request.QueryString["loggedin"] != null && !Request.QueryString["loggedin"].Equals(""))
                    {
                        hdnAuthentication.Value = "true";
                        displayCacheItems(Request.QueryString["cacheitem"].ToString());
                    }
                    else
                    {
                        if (hdnAuthentication.Value.Equals("false"))
                        {
                            PnlAuthentication.Visible = true;
                            PnlCacheInfo.Visible = false;
                        }
                    }
                }
            }


        }

        protected void btnSearchCourse_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table width='100%'>");
            sb.Append("<tr>");
            sb.Append("<td>Cache Key Name</td>");
            sb.Append("<td>Cache Type</td>");
            sb.Append("<td>Cache View</td>");
            sb.Append("</tr>");
            foreach (DictionaryEntry cacheDictionaryItem in HttpContext.Current.Cache)
            {
                if (HttpContext.Current.Cache[cacheDictionaryItem.Key.ToString()] != null)
                {
                    if (cacheDictionaryItem.Key.ToString().Contains(CourseID.Text))
                    {
                        Object cacheObjectItem = HttpContext.Current.Cache[cacheDictionaryItem.Key.ToString()];
                        System.Type type = cacheObjectItem.GetType();

                        sb.Append("<tr>");
                        sb.Append("<td>" + cacheDictionaryItem.Key.ToString() + "</td>");
                        sb.Append("<td>" + cacheObjectItem.GetType().FullName.ToString() + "</td>");
                        sb.Append("<td><a href='CacheInfoDetail.aspx?cacheitem=" + cacheDictionaryItem.Key.ToString() + "'>View</a></td>");
                        sb.Append("</tr>");
                    }
                }
            }
            sb.Append("</table>");
            ltrlCacheTable.Text = sb.ToString(); 

        }

        private void displayCacheItems(string cachekey) 
        {
            object cacheItem;
            cacheItem = HttpContext.Current.Cache[cachekey];
            if (cacheItem != null)
            {
                StringWriter swTest = XmlSerialize(cacheItem);
                if (swTest == null)
                {
                    ltrlCacheDetails.Text = HttpContext.Current.Server.HtmlEncode(swTest.ToString());
                }
            }
        }

        protected void btnAuthentication_Click(object sender, EventArgs e)
        {
            if (Authentication()) 
            {
                PnlAuthentication.Visible = false;
                PnlCacheInfo.Visible = true;
            }
        }

        private bool Authentication() 
        {
            if (txtUserName.Text.Equals("admin") && txtPassword.Text.Equals("admin#^)"))
            {
                hdnAuthentication.Value = "true"; 
                return true;
            }
            else 
            {
                return false;
            }
            return true;
        }

        private StringWriter XmlSerialize(object objectToSerialize)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(objectToSerialize.GetType());
                System.IO.StringWriter writer = new System.IO.StringWriter();
                XmlQualifiedName[] dummyNamespaceName = new XmlQualifiedName[1];
                dummyNamespaceName[0] = new XmlQualifiedName();
                serializer.Serialize(writer, objectToSerialize, new XmlSerializerNamespaces(dummyNamespaceName));
                return writer;
            }
            
            catch (InvalidOperationException)
            {
                //Ignore This can happen when some objects are just not Serializable using XML serialization
            }
            catch (System.Runtime.Serialization.SerializationException)
            {
                //Ignore. This can happen when storing a set of custom objects in a collection.
                //The XmlSerializer will start to serialize the collection come across the custom objects
                //amd not know what to do. The use of custom serialization attributes will help the serializer.
            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }

            //This will only be hit by a failed serialization execution
            return null;
        }
    }
}
