using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using NUnitHelper;
using ICP4.CommunicationLogic.CommunicationCommand.ShowResourceInfo;
using System.Configuration;
namespace ICP4.BusinessLogic.NUnitTest
{
    [TestFixture]
    class BrandManagerTest
    {
        private BrandManager.BrandManager brandManager;
        private NUnitTestDataManager nUnitTestDataManager;       
        /// <summary>
        /// 
        /// </summary>
        /// 
        [SetUp]
        protected void SetUp()
        {
            brandManager = new ICP4.BusinessLogic.BrandManager.BrandManager();
            nUnitTestDataManager = NUnitTestDataManager.GetInstance();
            string testDataFilePath = ConfigurationManager.AppSettings["NUnitTestDataPath"];
            Assert.IsTrue(nUnitTestDataManager.LoadTestData(testDataFilePath), "Could Not Load Test Data file");                        
        }        

        #region Local Resource Test
        [Test]
        /// <summary>
        /// This method creates ShowResourceInfo command by calling Branding Service and getting Resourceinfo
        /// </summary>
        /// <param name="variant">Variant string value</param>
        /// <param name="brandCode">BrandCode  string value</param>
        /// <returns>ShowResourceInfo command</returns>
        public void GetLocalResourceTest()
        {           
            //Load Test Data 
            string variant = nUnitTestDataManager.GetStringValue("variant");
            string brandCode = nUnitTestDataManager.GetStringValue("brandCode");
            string embeddedAcknowledgmentHeadingValue = nUnitTestDataManager.GetStringValue("embeddedAcknowledgmentHeadingValue");
            string embeddedAcknowledgmentHelpTextValue = nUnitTestDataManager.GetStringValue("embeddedAcknowledgmentHelpTextValue");
            string embeddedAcknowledgmentIAgreeButtonValue = nUnitTestDataManager.GetStringValue("embeddedAcknowledgmentIAgreeButtonValue");

            //Test Case: Embedded Acknowledgment Resource Key values
            //Note: Cannot be tested with Cache = 'Yes'
            {
            ShowResourceInfo showResourceInfo = brandManager.GetLocalResource(variant, brandCode);
            Assert.IsNotNull(showResourceInfo);
            Assert.IsNotNull(showResourceInfo.ResourceInfo);
            //Embedded Acknowledgment Heading Value
            string resourceKeyToCheck = BrandManager.ResourceKeyNames.AcknowledgmentHeading;
            string resourceKeyValueToCheck = embeddedAcknowledgmentHeadingValue;
            Assert.IsTrue(showResourceInfo.ResourceInfo.Exists(delegate(ResourceInfo resourceInfo) { return resourceInfo.ResourceKey == resourceKeyToCheck && resourceInfo.ResourceValue == resourceKeyValueToCheck; }));
            //Embedded Acknowledgment Help Text Value   
            resourceKeyToCheck = BrandManager.ResourceKeyNames.AcknowledgmentHelpText;
            resourceKeyValueToCheck = embeddedAcknowledgmentHelpTextValue;
            Assert.IsTrue(showResourceInfo.ResourceInfo.Exists(delegate(ResourceInfo resourceInfo) { return resourceInfo.ResourceKey == resourceKeyToCheck && resourceInfo.ResourceValue == resourceKeyValueToCheck; }));
            // Embedded Acknowledgment I Agree Button Value
            resourceKeyToCheck = BrandManager.ResourceKeyNames.AcknowledgmentIAgreeButton;
            resourceKeyValueToCheck = embeddedAcknowledgmentIAgreeButtonValue;
            Assert.IsTrue(showResourceInfo.ResourceInfo.Exists(delegate(ResourceInfo resourceInfo) { return resourceInfo.ResourceKey == resourceKeyToCheck && resourceInfo.ResourceValue == resourceKeyValueToCheck; }));                
            }

        }
        #endregion     
    }
}
