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
    public partial class CacheInfoDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["cacheitem"] != null && !Request.QueryString["cacheitem"].Equals(""))
                {                    
                    displayCacheItems(Request.QueryString["cacheitem"].ToString());
                }
            }
        }

        private void displayCacheItems(string cachekey)
        {
            object cacheItem;
            cacheItem = HttpContext.Current.Cache[cachekey];
            if (cacheItem != null)
            {
                StringWriter swTest = XmlSerialize(cacheItem);
                if (swTest != null)
                {
                    ltrlCacheDetails.Text = HttpContext.Current.Server.HtmlEncode(swTest.ToString());
                }
            }
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
