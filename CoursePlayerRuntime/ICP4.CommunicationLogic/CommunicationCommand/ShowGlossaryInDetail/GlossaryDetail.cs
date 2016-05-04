using System;
using System.Collections.Generic;
using System.Text;

namespace ICP4.CommunicationLogic.CommunicationCommand.ShowGlossaryInDetail
{
    public class GlossaryDetail
    {
        private int glossaryID;
        public int GlossaryID
        {
            get { return glossaryID; }
            set { glossaryID = value; }
        }

        private string headingGlossaryTerm;
        public string HeadingGlossaryTerm
        {
            get { return headingGlossaryTerm; }
            set { headingGlossaryTerm = value; }
        }
        
        private string headingGlossaryDefinition;
        public string HeadingGlossaryDefinition
        {
            get { return headingGlossaryDefinition; }
            set { headingGlossaryDefinition = value; }
        }
        
        private string headingGlossaryBoxTitle;
        public string HeadingGlossaryBoxTitle
        {
            get { return headingGlossaryBoxTitle; }
            set { headingGlossaryBoxTitle = value; }
        }


        private string glossaryDefinition;
        public string GlossaryDefinition
        {
            get { return glossaryDefinition; }
            set { glossaryDefinition = value; }
        }

        private string buttonText;
        public string ButtonText
        {
            get { return buttonText; }
            set { buttonText = value; }
        }
    }
}
