using _360Training.TrackingServiceBusinessLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _360Training.BusinessEntities;
using System.Collections.Generic;

namespace TrackingManagerTestTest
{
    
    /// <summary>
    ///This is a test class for TrackingManagerTestTest and is intended
    ///to contain all TrackingManagerTestTest Unit Tests
    ///</summary>
    [TestClass()]
    public class TrackingManagerTestTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for GET_LCMS_CONNECTOR_STATS_FORSCORM
        ///</summary>
        [TestMethod()]        
        public void GET_LCMS_CONNECTOR_STATS_FORSCORMTest()
        {
            TrackingManager trackingManager = new TrackingManager();
            LegacyLearnerStatistics legacyLearnerStatistics=trackingManager.GET_LCMS_CONNECTOR_STATS_FORSCORM(22,123,1);

 

        }

        /// <summary>
        ///A test for GET_LCMS_CONNECTOR_STATS_FORICP3Test
        ///</summary>
        [TestMethod()]
        public void GET_LCMS_CONNECTOR_STATS_FORICP3Test()
        {
            TrackingManager trackingManager = new TrackingManager();
            LegacyLearnerStatistics legacyLearnerStatistics = trackingManager.GET_LCMS_CONNECTOR_STATS_FORICP3(176808001, 982369, 1); 
        }

        /// <summary>
        ///A test for GET_LCMS_CONNECTOR_STATS_FORICP2Test
        ///</summary>
        [TestMethod()]
        public void GET_LCMS_CONNECTOR_STATS_FORICP2Test()
        {
            TrackingManager trackingManager = new TrackingManager();
            LegacyLearnerStatistics legacyLearnerStatistics = trackingManager.GET_LCMS_CONNECTOR_STATS_FORICP2(176808001, 982369, 1);
        }


        /// <summary>
        ///A test for GET_LCMS_CONNECTOR_STATS_FORICP2Test
        ///</summary>
        [TestMethod()]
        public void GET_LCMS_CONNECTOR_STATS_FORCERTIFICATE()
        {
            TrackingManager trackingManager = new TrackingManager();
            LegacyCertificateInfo legacyCertificateInfo = trackingManager.GET_LCMS_CONNECTOR_STATS_CERTIFICATE(176808001, 982369, 1);
        }
    }
}
