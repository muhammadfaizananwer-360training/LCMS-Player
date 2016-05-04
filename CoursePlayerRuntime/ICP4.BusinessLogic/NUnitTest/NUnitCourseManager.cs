using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using NUnit;
using NUnit.Framework;
using NUnitHelper;


namespace ICP4.BusinessLogic.NUnitTest
{
    [TestFixture]
    class NUnitCourseManager
    {
        ICP4.BusinessLogic.ICPCourseService.CourseService courseService = null;
        ICPCourseService.Asset asset = null;
        int assetID = 14;
        [SetUp]
        public void SetUp()
        {
            courseService = new ICP4.BusinessLogic.ICPCourseService.CourseService();
            courseService.Url = ConfigurationManager.AppSettings["ICPCourseService"].ToString();
            asset = new ICP4.BusinessLogic.ICPCourseService.Asset();
            //asset = courseService.GetAsset(assetID);
            Assert.NotNull(asset);

        }

        public void GetAssetInfo()
        {
            //asset= courseService.GetAsset(assetID);
            Assert.NotNull(asset);
        }

    }
}

