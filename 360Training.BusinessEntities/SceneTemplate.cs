using System;
using System.Collections.Generic;
using System.Text;

namespace _360Training.BusinessEntities
{
    public class SceneTemplate
    {
        private string templateHTML;

        public string TemplateHTML
        {
            get { return templateHTML; }
            set { templateHTML = value; }
        }
        private string sceneTemplateType;

        public string SceneTemplateType
        {
            get { return sceneTemplateType; }
            set { sceneTemplateType = value; }
        }
        private int sceneTemplateID;

        public int SceneTemplateID
        {
            get { return sceneTemplateID; }
            set { sceneTemplateID = value; }
        }
        private string templateHTMLURL;

        public string TemplateHTMLURL
        {
            get { return templateHTMLURL; }
            set { templateHTMLURL = value; }
        }
        public SceneTemplate()
        {
            this.templateHTML = string.Empty;
            this.sceneTemplateType = string.Empty;
            this.sceneTemplateID = 0;
            this.templateHTMLURL = string.Empty;
        }
    }
}
