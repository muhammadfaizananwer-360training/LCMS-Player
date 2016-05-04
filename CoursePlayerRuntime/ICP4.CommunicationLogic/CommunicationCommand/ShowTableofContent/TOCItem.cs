using System;
using System.Collections.Generic;
using System.Text;

namespace ICP4.CommunicationLogic.CommunicationCommand.ShowTableofContent
{
    public class TOCItem
    {
        private int id;
        public int ID
        {
            get { return id; }
            set { id = value;}
        }

        private string title;

        public string Title
        {
            get { return title; }
            set { title = value; }
        }



        private bool isDisabled;


        public bool IsDisabled
        {
            get { return isDisabled; }
            set { isDisabled = value; }
        }

        public TOCItem()
        {
            this.ID = 0;
            this.Title = String.Empty;
        }


        private string breadCrumb;

        public string BreadCrumb
        {
            get { return breadCrumb; }
            set { breadCrumb = value; }
        }

        private string type;

        public string Type
        {
            get { return type; }
            set { type = value; }
        }


        private List<TOCItem> tocItems;

        public List<TOCItem> TOCItems
        {
            get { return tocItems; }
            set { tocItems = value; }
        }
    }
}
