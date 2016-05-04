using _360Training.TrackingServiceBusinessLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _360Training.BusinessEntities;
using _360Training.TrackingServiceBusinessLogic;
using System.Collections.Generic;
using System;

namespace CoursePlayerBusinessLogicUnitTest
{
    
    
    /// <summary>
    ///This is a test class for TrackingManagerTest and is intended
    ///to contain all TrackingManagerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class TrackingManagerTest
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
        ///A test for GetPreviouslyAskedQuestions
        ///</summary>
        [TestMethod()]
        public void GetPreviouslyAskedQuestionsTest()
        {
            TrackingManager target = new TrackingManager(); // TODO: Initialize to an appropriate value
            string learnerSessionGUID = "1330e8de-4f06-4def-8bcf-73212ba5042e"; // TODO: Initialize to an appropriate value
            string assessmentType = LearnerStatisticsType.PostAssessment;// TODO: Initialize to an appropriate value
            int remediationCount = 0; // TODO: Initialize to an appropriate value            
            List<LearnerStatistics> actual;
            actual = target.GetPreviouslyAskedQuestions(learnerSessionGUID, assessmentType, remediationCount,0);
            Assert.AreEqual(true, actual.Count > 0);
        }
        /// <summary>
        ///A test for GetPostAssessmentAssessmentItems
        ///</summary>
        [TestMethod()]
        public void GetQuizResultTest()
        {
            TrackingManager target = new TrackingManager();
            string learningSessionGUID = "2e20722b-5800-48fb-b65c-7e5ce9716b93";
            string contentObjectGUID = "275c6c4a-d692-408d-b6f3-c433f223a85b";
            Assert.IsNotNull(target.GetQuizResult(learningSessionGUID, contentObjectGUID));

        }
        /// <summary>
        ///A test for GetPostAssessmentAssessmentItems
        ///</summary>
        [TestMethod()]
        public void SaveLearnerStatistics()
        {
            TrackingManager target = new TrackingManager();
            LearnerStatistics learnerStatistics = new LearnerStatistics();
            learnerStatistics.RemediationCount = 1;
            learnerStatistics.Item_GUID = "test";
            learnerStatistics.AssessmentAttemptNumber = 1;
            Assert.AreEqual(true, target.SaveLearnerStatistics(learnerStatistics));

        }
        [TestMethod()]
        public void GetCourseCompletionStatus()
        {
            bool courseCompletionStatus = false;
            int courseID = 92751;
            int learnerID = 917796;
            int enrollmentID = 2141353;
            int source = 0;
            TrackingManager trackingManager = new TrackingManager();
            CourseCompletionPolicy policy = new CourseCompletionPolicy();
            policy.PreAssessmentMasteryAchived = true;
            policy.PreAssessmentMasteryScore = 80;

            _360Training.CourseServiceBusinessLogic.CourseManager target = new _360Training.CourseServiceBusinessLogic.CourseManager(); // TODO: Initialize to an appropriate value
            CourseConfiguration courseConfiguration = target.GetCourseConfiguration(123);
            

            ICP4.BusinessLogic.CourseManager.CourseManager cManager = new ICP4.BusinessLogic.CourseManager.CourseManager();

            _360Training.BusinessEntities.CourseCompletionPolicy courseCompletionPolicy = new CourseCompletionPolicy();
            //CourseCompletionPolicyEntity2BizTranslator
            courseCompletionPolicy.CompleteAfterNumberOfUniqueVisits = courseConfiguration.CompletionCompleteAfterNOUniqueCourseVisit;
            courseCompletionPolicy.EnableEmbeddedAknowledgement = courseConfiguration.EmbeddedAcknowledgmentEnabled;
            courseCompletionPolicy.MustCompleteWithInSpecifiedAmountOfDayAfterRegistration = courseConfiguration.CompletionMustCompleteWithinSpecifiedAmountOfTimeDay;
            courseCompletionPolicy.MustCompleteWithInSpecifiedAmountOfTime = courseConfiguration.CompletionMustCompleteWithinSpecifiedAmountOfTimeMinute;
            courseCompletionPolicy.MustCompleteWithInSpecifiedAmountOfTimeUnit = courseConfiguration.CompletionUnitOfMustCompleteWithInSpecifiedAmountOfTime;
            courseCompletionPolicy.PostAssessmentAttempted = courseConfiguration.CompletionPostAssessmentAttempted;
            courseCompletionPolicy.PostAssessmentMasteryAchived = courseConfiguration.CompletionPostAssessmentMastery;
            courseCompletionPolicy.PostAssessmentMasteryScore = courseConfiguration.PostAssessmentConfiguration.MasteryScore;
            courseCompletionPolicy.PostAssessmentScoreType = courseConfiguration.PostAssessmentConfiguration.ScoreType;
            courseCompletionPolicy.PreAssessmentMasteryScore = courseConfiguration.PreAssessmentConfiguration.MasteryScore;
            courseCompletionPolicy.PreAssessmentMasteryAchived = courseConfiguration.CompletionPreAssessmentMastery;
            courseCompletionPolicy.PreAssessmentScoreType = courseConfiguration.PreAssessmentConfiguration.ScoreType;
            courseCompletionPolicy.QuizMasteryScore = courseConfiguration.QuizConfiguration.MasteryScore;
            courseCompletionPolicy.QuizMasteryAchived = courseConfiguration.CompletionQuizMastery;
            courseCompletionPolicy.QuizScoreType = courseConfiguration.QuizConfiguration.ScoreType;
            courseCompletionPolicy.RespondToCourseEvaluation = courseConfiguration.CompletionRespondToCourseEvaluation;
            courseCompletionPolicy.ViewEverySceneInCourse = courseConfiguration.CompletionViewEverySceneInCourse;



            trackingManager.GetCourseCompletionStatus(courseID, courseCompletionPolicy, learnerID, enrollmentID, 1, DateTime.Now, 100.00,0,0);
            //Assert.IsTrue(courseCompletionStatus);
        }
        [TestMethod()]
        public void SaveCourseEvaluationResult()
        {
            TrackingManager trackingManager = new TrackingManager();
            CourseEvaluationResult courseEvaluationResult=new CourseEvaluationResult();
            List<CourseEvaluationResultAnswer> courseEvaluationResultAnswers=new List<CourseEvaluationResultAnswer>();
            CourseEvaluationResultAnswer courseEvaluationResultAnswer=new CourseEvaluationResultAnswer();
            courseEvaluationResultAnswer.CourseEvaluationAnswerID=197;
            courseEvaluationResultAnswer.CourseEvaluationAnswerType="my type";
            courseEvaluationResultAnswer.CourseEvaluationQuestionID=1654;
            courseEvaluationResultAnswer.CourseEvaluationResultAnswerText="this is answere text";
            courseEvaluationResultAnswers.Add(courseEvaluationResultAnswer);
            courseEvaluationResultAnswer=new CourseEvaluationResultAnswer();
            courseEvaluationResultAnswer.CourseEvaluationAnswerID=196;
            courseEvaluationResultAnswer.CourseEvaluationAnswerType="my type2";
            courseEvaluationResultAnswer.CourseEvaluationQuestionID=1653;
            courseEvaluationResultAnswer.CourseEvaluationResultAnswerText="this is answere text2";
            courseEvaluationResultAnswers.Add(courseEvaluationResultAnswer);
            courseEvaluationResultAnswer=new CourseEvaluationResultAnswer();
            courseEvaluationResultAnswer.CourseEvaluationAnswerID=0;
            courseEvaluationResultAnswer.CourseEvaluationAnswerType="";
            courseEvaluationResultAnswer.CourseEvaluationQuestionID=0;
            courseEvaluationResultAnswer.CourseEvaluationResultAnswerText="this is answere text2";
            courseEvaluationResultAnswers.Add(courseEvaluationResultAnswer);
            courseEvaluationResult.CourseID=29679;
            courseEvaluationResult.LearnerID=21408;
            courseEvaluationResult.SurveyID=5983;
            courseEvaluationResult.LearningSessionID = 71382;
            courseEvaluationResult.CourseEvaluationResultAnswers = courseEvaluationResultAnswers;
            bool returnValue = false;
            returnValue= trackingManager.SaveCourseEvaluationResult(courseEvaluationResult);
            Assert.IsTrue(returnValue);
        }

        [TestMethod()]
        public void IsCourseEvaluationAttempted()
        {
            TrackingManager trackingManager = new TrackingManager();
            
            DateTime startTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            int decs =  trackingManager.GetLearnerTimeSpentByTime(2137304,startTime, DateTime.Now,"ddd");
        }
        [TestMethod()]
        public void GetCourseCompletion()
        {
            TrackingManager trackingManager = new TrackingManager();

            //LearnerCourseCompletionStatus bool1 = trackingManager.GetCourseCompletionStatus(91738, 119371, 183092, 1, DateTime.Now);
            //Assert.AreEqual(bool1.IsCourseCompleted, true);
        }



    }
}
