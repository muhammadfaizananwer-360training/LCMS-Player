using _360Training.AssessmentServiceBusinessLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _360Training.BusinessEntities;
using System.Collections.Generic;

namespace CoursePlayerBusinessLogicUnitTest
{
    
    
    /// <summary>
    ///This is a test class for AssessmentManagerTest and is intended
    ///to contain all AssessmentManagerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class AssessmentManagerTest
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
        ///A test for GetPreAssessmentAssessmentItems
        ///</summary>
        [TestMethod()]
        public void GetPreAssessmentAssessmentItemsTest()
        {
            string error = "";
            //for (int j = 0; j < 10; j++)
            {
                _360Training.CourseServiceBusinessLogic.CourseManager courseManager = new _360Training.CourseServiceBusinessLogic.CourseManager();
                
                    
                AssessmentManager target = new AssessmentManager(); // TODO: Initialize to an appropriate value
                int courseID = 90140; // TODO: Initialize to an appropriate value
                CourseConfiguration courseConfiguration = courseManager.GetCourseConfiguration(1223);
                //courseConfiguration.PreAssessmentConfiguration.NOQuestion = 5;
                List<string> previouslyAskedQuestionsGUIDs = new List<string>(); // TODO: Initialize to an appropriate value
                bool expected = true; // TODO: Initialize to an appropriate value
                List<AssessmentItem> actual;
                actual = target.GetPreAssessmentAssessmentItems(courseID, courseConfiguration, previouslyAskedQuestionsGUIDs,0);
                //actual = target.GetPostAssessmentAssessmentItems(courseID, courseConfiguration, previouslyAskedQuestionsGUIDs);
                //actual = target.GetQuizAssessmentAssessmentItems(courseID, 43519, courseConfiguration, previouslyAskedQuestionsGUIDs);
                
            //    for (int i = 0; i < 10; i++)
            //    {
            //        //target.RandomizeAnswers(ref actual);


            //        int index = 0;
            //        foreach (AssessmentItem assessmentItem in actual)
            //        {
            //            index++;
            //            if (assessmentItem.AssessmentAnswers[0].DisplayOrder == 1 && assessmentItem.AssessmentAnswers[1].DisplayOrder == 2 && assessmentItem.AssessmentAnswers[2].DisplayOrder == 3 && assessmentItem.AssessmentAnswers[3].DisplayOrder == 4)
            //            {
            //                error = error + index + "," + assessmentItem.AssessmentAnswers[0].Value + ";";
            //            }
            //        }
            //    }
            }

            Assert.AreEqual(error, "");    
             
            
        }

        /// <summary>
        ///A test for GetQuizAssessmentAssessmentItems
        ///</summary>
        [TestMethod()]
        public void GetQuizAssessmentAssessmentItemsTest()
        {
            AssessmentManager target = new AssessmentManager(); // TODO: Initialize to an appropriate value
            int contentObjectID = 14667; // TODO: Initialize to an appropriate value
            CourseConfiguration courseConfiguration =  new CourseConfiguration(); // TODO: Initialize to an appropriate value
            List<string> previouslyAskedQuestionsGUIDs = new List<string>(); // TODO: Initialize to an appropriate value
            bool expected = true; // TODO: Initialize to an appropriate value
            List<AssessmentItem> actual;
            actual = target.GetQuizAssessmentAssessmentItems(0, contentObjectID, courseConfiguration, previouslyAskedQuestionsGUIDs, 0);
            Assert.AreEqual(expected, actual.Count > 0);           
        }

        [TestMethod()]
        public void GetExamAssessmentItemsTest_Pre()
        {
            _360Training.CourseServiceBusinessLogic.CourseManager courseManager = new _360Training.CourseServiceBusinessLogic.CourseManager();


            AssessmentManager target = new AssessmentManager(); // TODO: Initialize to an appropriate value
            int courseID = 92294; // TODO: Initialize to an appropriate value
            CourseConfiguration courseConfiguration = courseManager.GetCourseConfiguration(123);
            //courseConfiguration.PreAssessmentConfiguration.NOQuestion = 5;
            List<string> previouslyAskedQuestionsGUIDs = new List<string>(); // TODO: Initialize to an appropriate value
            bool expected = true; // TODO: Initialize to an appropriate value
            List<AssessmentItem> actual;
            actual = target.GetPreAssessmentAssessmentItems(courseID, courseConfiguration, previouslyAskedQuestionsGUIDs, 171);

        }


        [TestMethod()]
        public void GetExamAssessmentItemsTest_Post()
        {
            _360Training.CourseServiceBusinessLogic.CourseManager courseManager = new _360Training.CourseServiceBusinessLogic.CourseManager();


            AssessmentManager target = new AssessmentManager(); // TODO: Initialize to an appropriate value
            int courseID = 90144; // TODO: Initialize to an appropriate value
            CourseConfiguration courseConfiguration = courseManager.GetCourseConfiguration(123);
            //courseConfiguration.PreAssessmentConfiguration.NOQuestion = 5;
            List<string> previouslyAskedQuestionsGUIDs = new List<string>(); // TODO: Initialize to an appropriate value
            bool expected = true; // TODO: Initialize to an appropriate value
            List<AssessmentItem> actual;
            actual = target.GetPostAssessmentAssessmentItems(courseID, courseConfiguration, previouslyAskedQuestionsGUIDs, 5);

        }

        [TestMethod()]
        public void GetExamAssessmentItemsTest_Quiz()
        {
            _360Training.CourseServiceBusinessLogic.CourseManager courseManager = new _360Training.CourseServiceBusinessLogic.CourseManager();


            AssessmentManager target = new AssessmentManager(); // TODO: Initialize to an appropriate value
            int courseID = 90144; // TODO: Initialize to an appropriate value
            CourseConfiguration courseConfiguration = courseManager.GetCourseConfiguration(123);
            //courseConfiguration.PreAssessmentConfiguration.NOQuestion = 5;
            List<string> previouslyAskedQuestionsGUIDs = new List<string>(); // TODO: Initialize to an appropriate value
            bool expected = true; // TODO: Initialize to an appropriate value
            List<AssessmentItem> actual;
            actual = target.GetQuizAssessmentAssessmentItems(courseID, 43670, courseConfiguration, previouslyAskedQuestionsGUIDs, 5);

        }

        /// <summary>
        ///A test for GetPostAssessmentAssessmentItems
        ///</summary>
        [TestMethod()]
        public void GetPostAssessmentAssessmentItemsTest()
        {
            AssessmentManager target = new AssessmentManager(); // TODO: Initialize to an appropriate value
            int courseID = 11935; // TODO: Initialize to an appropriate value
            CourseConfiguration courseConfiguration = new CourseConfiguration(); // TODO: Initialize to an appropriate value
            List<string> previouslyAskedQuestionsGUIDs = new List<string>(); // TODO: Initialize to an appropriate value
            bool expected = true; // TODO: Initialize to an appropriate value
            List<AssessmentItem> actual;
            actual = target.GetPostAssessmentAssessmentItems(courseID, courseConfiguration, previouslyAskedQuestionsGUIDs, 0);
            Assert.AreEqual(expected, actual.Count > 0);            
        }
        [TestMethod()]
        public void GetKnowledgeCheckAssessmentItems()
        {
            AssessmentManager target = new AssessmentManager(); // TODO: Initialize to an appropriate value
            List<AssessmentItem> actual=null;
            int sceneID = 46014; // TODO: Initialize to an appropriate value
            actual = target.GetKnowledgeCheckAssessmentItems(sceneID);
            Assert.IsNotNull(actual);
            
        }

        [TestMethod()]
        public void TestGetPostAssessmentAssessmentItemsForAdvanceQuestionSelection()
        {
            List<string> strList = new List<string>();
            //strList.Add("f0969ced-d192-4286-90d7-fe59e33b1a6b");
            //strList.Add("ffc9b934-cc3c-4f33-93e8-7a45ab4c0edb");
            //strList = null;
            AssessmentManager assessmentManager = new AssessmentManager();
            List<AssessmentItem> listAssessmentItem = assessmentManager.GetAssessmentAssessmentItemsForAdvanceQuestionSelection(90144, false, false, 20, strList, AssessmentConfiguration.ADVANCEQUESTIONSELECTIONTYPE_MINMAX, AssessmentConfiguration.ASSESSMENTYPE_PREASSESSMENT, 0, 4);
        }

        [TestMethod()]
        public void TestGetQuizAssessmentAssessmentItemsForAdvanceQuestionSelection()
        {
            List<string> strList = new List<string>();
            //strList.Add("c7b019af-a8f3-4538-a2a5-cc03acf1307c");
            //strList.Add("b43e4e62-b102-45ff-b6f4-ed906fb82b60");
            AssessmentManager assessmentManager = new AssessmentManager();
            List<AssessmentItem> listAssessmentItem = assessmentManager.GetAssessmentAssessmentItemsForAdvanceQuestionSelection(86941, false, false, 3, strList, AssessmentConfiguration.ADVANCEQUESTIONSELECTIONTYPE_MINMAX, AssessmentConfiguration.ASSESSMENTYPE_QUIZ, 43266,0);
        }

        [TestMethod()]
        public void TestConvertToCommaSeperatedStr()
        {
            List<string> strList = new List<string>();
            strList.Add("1");
            strList.Add("33241");
            strList.Add("324343243243232_234231");
            AssessmentManager assessmentManager = new AssessmentManager();
            string str = assessmentManager.ConvertToCommaSeperatedStr(strList);

        }

        [TestMethod()]
        public void TestExcludeoCommaSeperatedStr()
        {
            AssessmentManager assessmentManager = new AssessmentManager();
            assessmentManager.ExcludeCommaSeperatedStr("1,22,33", 2);
        }

     
    }
}
