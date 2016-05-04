using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _360Training.TrackingServiceBusinessLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _360Training.BusinessEntities;
using _360Training.TrackingServiceBusinessLogic;
using System.Collections.Generic;
using System;


namespace CoursePlayerBusinessLogicUnitTest
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class UnitTest1
    {
        public UnitTest1()
        {
            //
            // TODO: Add constructor logic here
            //
        }

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
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestMethod1()
        {
            //
            // TODO: Add test logic	here
            //
            TrackingManager trackingManager = new TrackingManager();

            DateTime startTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            int decs = trackingManager.GetLearnerTimeSpentByTime(2137304, startTime, DateTime.Now,"tttt");
        }

        [TestMethod]
        public void TestMethod_GetCourseCompletionData_Optimized()
        {
            //
            // TODO: Add test logic	here
            //

            //_360Training.TrackingServiceDataLogic.StudentTrackingDA.StudentTrackingDA trackingDA = new _360Training.TrackingServiceDataLogic.StudentTrackingDA.StudentTrackingDA();
            
            TrackingManager trackingManager = new TrackingManager();

          //  bool bool1 = trackingManager.GetCourseCompletionStatus(79987, 917796, 2137415, 2);
           // Assert.AreEqual(bool1, true);
            //Completion after number of Unique Visits of Course = 1 
            
            //PostAssessmentAttempted (Not Attempted)
           // bool bool1 = trackingManager.GetCourseCompletionStatus(79987, 917796, 2137416, 2);
          //  Assert.AreEqual(bool1, false);
            //PostAssessmentAttempted 

            //PostAssessmentAttempted (Attempted but Failed)
           // bool bool1 = trackingManager.GetCourseCompletionStatus(79987, 917796, 2137425, 1,DateTime.Now);
            ///Assert.AreEqual(bool1, true);
            //PostAssessmentAttempted             

            //PostAssessmentAttempted (Attempted and Passed)
            //bool bool1 = trackingManager.GetCourseCompletionStatus(79987, 917796, 2137322);
            //Assert.AreEqual(bool1, true);
            //PostAssessmentAttempted 


            //PostAssessmentMatery (Not Attempted)
            //bool bool1 = trackingManager.GetCourseCompletionStatus(79987, 917796, 2137326);
            //Assert.AreEqual(bool1, false);
            //PostAssessmentAttempted 

            //PostAssessmentMatery (Attempted and failed)
            //bool bool1 = trackingManager.GetCourseCompletionStatus(79987, 917796, 2137326);
            //Assert.AreEqual(bool1, false);
            //PostAssessmentAttempted 

            //PostAssessmentMatery (Attempted and Passed)
            //bool bool1 = trackingManager.GetCourseCompletionStatus(79987, 917796, 2137326);
            //Assert.AreEqual(bool1, true);
            //PostAssessmentAttempted 

            //PreAssessmentMatery (Attempted and not Attempted)
            //bool bool1 = trackingManager.GetCourseCompletionStatus(79987, 917796, 2137327);
            //Assert.AreEqual(bool1, false);
            //PreAssessmentMatery

            //PreAssessmentMatery (Attempted and failed)
            //bool bool1 = trackingManager.GetCourseCompletionStatus(79987, 917796, 2137327);
            //Assert.AreEqual(bool1, false);
            //PreAssessmentMatery

            //PreAssessmentMatery (Attempted and Passed)
            //bool bool1 = trackingManager.GetCourseCompletionStatus(79987, 917796, 2137327);
            //Assert.AreEqual(bool1, true);
            //PreAssessmentMatery

            
            //QuizMatery (Not Attempted)
            //bool bool1 = trackingManager.GetCourseCompletionStatus(79987, 917796, 2137328);
            //Assert.AreEqual(bool1, false);
            //QuizAssessmentMatery 

            //Original issue
            //QuizMatery (1 Quiz Passed other not attempted, Total 2 Quiz)
            //bool bool1 = trackingManager.GetCourseCompletionStatus(79987, 917796, 2137328);
            //Assert.AreEqual(bool1, false);
            //QuizAssessmentMatery 

            //QuizMatery (1 Quiz Passed and 1 Quiz Failed, Total 2 Quiz)
            //bool bool1 = trackingManager.GetCourseCompletionStatus(79987, 917796, 2137328);
            //Assert.AreEqual(bool1, false);
            //QuizAssessmentMatery 

            //QuizMatery (2 Quiz Passed, Total 2 Quiz)
            //bool bool1 = trackingManager.GetCourseCompletionStatus(79987, 917796, 2137328);
            //Assert.AreEqual(bool1, true);
            //QuizAssessmentMatery 

            //View Every Scene in Course (0%)
            //bool bool1 = trackingManager.GetCourseCompletionStatus(79987, 917796, 2137329);
            //Assert.AreEqual(bool1, false);
            //View Every Scene in Course

            //View Every Scene in Course (50%)
            //bool bool1 = trackingManager.GetCourseCompletionStatus(79987, 917796, 2137329);
            //Assert.AreEqual(bool1, false);
            //View Every Scene in Course

            //View Every Scene in Course (100%)
            //bool bool1 = trackingManager.GetCourseCompletionStatus(79987, 917796, 2137329);
            //Assert.AreEqual(bool1, true);
            //View Every Scene in Course

            //# of unique launches (1 launch,value 2 is set)
            //bool bool1 = trackingManager.GetCourseCompletionStatus(79987, 917796, 2137330);
            //Assert.AreEqual(bool1, false);
            //# of unique launches 

            //# of unique launches (2 launch,value 2 is set)
            //bool bool1 = trackingManager.GetCourseCompletionStatus(79987, 917796, 2137330);
            //Assert.AreEqual(bool1, true);
            //# of unique launches 

            //# of unique launches & Course Evaluation (1 Launch and Course Eval req,1 launch and course eval not attempted)
            //bool bool1 = trackingManager.GetCourseCompletionStatus(79987, 917796, 2137331);
            //Assert.AreEqual(bool1, false);
            //# of unique launches & Course Evaluation

            //# of unique launches & Course Evaluation (1 Launch and Course Eval req,1 launch and course eval attempted)
            //bool bool1 = trackingManager.GetCourseCompletionStatus(79987, 917796, 2137331);
            //Assert.AreEqual(bool1, true);
            //# of unique launches & Course Evaluation

            //# of unique launches & Must completed with specified amount of time (1 Launch and within 5 min)
            //bool bool1 = trackingManager.GetCourseCompletionStatus(79987, 917796, 2137332);
            //Assert.AreEqual(bool1, true);
            //# of unique launches & Course Evaluation

            //# of unique launches & Must completed with specified amount of time (1 Launch and within 1 min,user excceds time)
            //bool bool1 = trackingManager.GetCourseCompletionStatus(79987, 917796, 2137333);
            //Assert.AreEqual(bool1, false);
            //# of unique launches & Course Evaluation

            //# of unique launches & Must completed with specified amount of time (1 Launch and within 1 min,within Time)
           // bool bool1 = trackingManager.GetCourseCompletionStatus(79987, 917796, 2137336,0);
           /// Assert.AreEqual(bool1, false);
            //# of unique launches & Course Evaluation
        }

    }
}

/*
  
Test cases (Unit testing):
-PostAssessmentAttempted (Not Attempted)

-PostAssessmentAttempted (Attempted but Failed)

-PostAssessmentAttempted (Attempted and Passed)

-PostAssessmentMatery (Not Attempted)

-PostAssessmentMatery (Attempted and failed)

-PostAssessmentMatery (Attempted and Passed)

-PreAssessmentMatery (Attempted and not Attempted)

-PreAssessmentMatery (Attempted and failed)

-PreAssessmentMatery (Attempted and Passed)


-QuizMatery (Not Attempted)

-Original issue
-QuizMatery (1 Quiz Passed other not attempted, Total 2 Quiz)

-QuizMatery (1 Quiz Passed and 1 Quiz Failed, Total 2 Quiz)

-QuizMatery (2 Quiz Passed, Total 2 Quiz)

-View Every Scene in Course (0%)

-View Every Scene in Course (50%)

-View Every Scene in Course (100%)

-# of unique launches (1 launch,value 2 is set)

-# of unique launches (2 launch,value 2 is set)

-# of unique launches & Course Evaluation (1 Launch and Course Eval req,1 launch and course eval not attempted)

-# of unique launches & Course Evaluation (1 Launch and Course Eval req,1 launch and course eval attempted)

-# of unique launches & Must completed with specified amount of time (1 Launch and within 5 min)

-# of unique launches & Must completed with specified amount of time (1 Launch and within 1 min,user excceds time)

-# of unique launches & Must completed with specified amount of time (1 Launch and within 1 min,within Time)

*/