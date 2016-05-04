using System;
using System.Collections.Generic;
using System.Text;

namespace _360Training.BusinessEntities
{
    public class Sequence
    {
        private List<SequenceItem> sequenceItems;
        private DateTime lastPublishedDateTime;
        public List<SequenceItem> SequenceItems
        {
            get { return sequenceItems; }
            set { sequenceItems = value; }
        }

        public DateTime LastPublishedDateTime
        {
            get { return lastPublishedDateTime; }
            set { lastPublishedDateTime = value; }
        }

        public Sequence()
        {
            sequenceItems = new List<SequenceItem>();
            lastPublishedDateTime = new DateTime(1900, 1, 1);
        }


    }
}
