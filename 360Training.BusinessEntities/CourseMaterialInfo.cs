using System;
using System.Collections.Generic;
using System.Text;

namespace _360Training.BusinessEntities
{
    public class CourseMaterialInfo
    {
        private string courseMaterialAssetName;

        public string CourseMaterialAssetName
        {
            get { return courseMaterialAssetName; }
            set { courseMaterialAssetName = value; }
        }
        private string courseMaterialAssetLocation;

        public string CourseMaterialAssetLocation
        {
            get { return courseMaterialAssetLocation; }
            set { courseMaterialAssetLocation = value; }
        }
        public CourseMaterialInfo()
        {
            this.courseMaterialAssetLocation = string.Empty;
            this.courseMaterialAssetName = string.Empty;
        }

    }
}
