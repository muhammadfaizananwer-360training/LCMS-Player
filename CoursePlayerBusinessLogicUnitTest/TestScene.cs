using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _360Training.CourseServiceBusinessLogic;
using _360Training.BusinessEntities;
using NUnit.Framework;

namespace CoursePlayerBusinessLogicUnitTest
{
    class TestScene
    {

        [Test]
        public void TestGetSceneAssets()
        {
            int sceneID = 24539;
            CourseManager courseManager = new CourseManager();
            List<Asset> assetList = courseManager.GetSceneAssets(sceneID);
            Assert.IsNotNull(assetList);
            

        }
    }
}
