using System;
using System.Collections.Generic;
using System.Text;

namespace ICP4.BusinessEntities
{
    public class PlayerServer
    {
        private String localIPAddress;
        public String LocalIPAddress
        {
            get { return localIPAddress; }
            set { localIPAddress = value; }
        }

        private String name;
        public String Name
        {
            get { return name; }
            set { name = value; }
        }

        private String description;
        public String Description
        {
            get { return description; }
            set { description = value; }
        }

        private String hostName;
        public String HostName
        {
            get { return hostName; }
            set { hostName = value; }
        }

        private String playerWebServiceURL;
        public String PlayerWebServiceURL
        {
            get { return playerWebServiceURL; }
            set { playerWebServiceURL = value; }
        }

    }
}
