using System;
using System.Collections.Generic;
using System.Text;

namespace _360Training.BusinessEntities
{
    public class LocaleResource
    {
        private string resourceKey;

        public string ResourceKey
        {
            get { return resourceKey; }
            set { resourceKey = value; }
        }
        private string resourceValue;

        public string ResourceValue
        {
            get { return resourceValue; }
            set { resourceValue = value; }
        }
    }
}
