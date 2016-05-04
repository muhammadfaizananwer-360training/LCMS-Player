using System;
using System.Collections.Generic;
using System.Text;

namespace _360Training.BusinessEntities
{
    public class GlossaryItem
    {
        private int glossaryItemID;

        public int GlossaryItemID
        {
            get { return glossaryItemID; }
            set { glossaryItemID = value; }
        }
        private string term;

        public string Term
        {
            get { return term; }
            set { term = value; }
        }
        private string definition;

        public string Definition
        {
            get { return definition; }
            set { definition = value; }
        }
        public GlossaryItem()
        {
            this.definition = string.Empty;
            this.glossaryItemID = 0;
            this.term = string.Empty;
        }
    }
}
