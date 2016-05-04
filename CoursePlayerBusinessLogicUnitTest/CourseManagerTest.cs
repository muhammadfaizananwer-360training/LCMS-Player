using _360Training.CourseServiceBusinessLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _360Training.BusinessEntities;
using ICP4.BusinessLogic.CourseManager;
using System.Configuration;


namespace CoursePlayerBusinessLogicUnitTest
{
    
    
    /// <summary>
    ///This is a test class for CourseManagerTest and is intended
    ///to contain all CourseManagerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CourseManagerTest
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
        ///A test for GetSequence
        ///</summary>
        [TestMethod()]
        public void GetSequenceTest()
        {
            _360Training.CourseServiceBusinessLogic.CourseManager target = new _360Training.CourseServiceBusinessLogic.CourseManager(); // TODO: Initialize to an appropriate value
            int courseID = 92567; // TODO: Initialize to an appropriate value
            CourseConfiguration courseConfiguration = target.GetCourseConfiguration(123); // TODO: Initialize to an appropriate value            
            Sequence actual;
            actual = target.GetSequence(courseID, courseConfiguration);

            Assert.AreEqual(true, actual.SequenceItems.Count > 0);
            
        }

        /// <summary>
        ///A test for GetSequence
        ///</summary>
        [TestMethod()]
        public void GetCourseConfigurationTest()
        {
            _360Training.CourseServiceBusinessLogic.CourseManager target = new _360Training.CourseServiceBusinessLogic.CourseManager();
            int courseID = 93724; // TODO: Initialize to an appropriate value
            
            CourseConfiguration courseConfiguration = target.GetCourseConfiguration(123); // TODO: Initialize to an appropriate value            
            
            Assert.IsNotNull(courseConfiguration);
            Assert.IsTrue(courseConfiguration.PlayerEnableOrientaionScenes);
            Assert.IsFalse(courseConfiguration.PlayerEnableEndOfCourseScene);
            Assert.IsTrue(courseConfiguration.PlayerEnableContent);
            Assert.IsTrue(courseConfiguration.PlayerEnforceTimedOutline);
            Assert.IsTrue(courseConfiguration.PlayerIdleUserTimeout < 1800);
            Assert.IsTrue(courseConfiguration.ActionToTakeUponIdleTimeOut == _360Training.BusinessEntities.IdleTimeOut.CloseCourse);
            Assert.IsTrue(courseConfiguration.PlayerCourseFlow == _360Training.BusinessEntities.CourseFlow.FirstTimeLinear);
            Assert.IsFalse(courseConfiguration.PlayerAllowUserToReviewCourseAfterCompletion);
            Assert.IsTrue(courseConfiguration.CertificateEnabled);
            Assert.IsTrue(courseConfiguration.PlayerCourseEvaluation);
            Assert.IsTrue(courseConfiguration.PlayerDisplayCourseEvaluation == _360Training.BusinessEntities.DisplayCourseEvaluation.BeforeAndAfterPostAssessment);
            Assert.IsTrue(courseConfiguration.PlayerMustCompleteCourseEvaluatio);
            Assert.IsTrue(courseConfiguration.CompletionPostAssessmentAttempted);
            Assert.IsTrue(courseConfiguration.CompletionPostAssessmentMastery);
            Assert.IsTrue(courseConfiguration.CompletionPreAssessmentMastery);
            Assert.IsTrue(courseConfiguration.CompletionQuizMastery);
            Assert.IsTrue(courseConfiguration.CompletionSurvey);
            Assert.IsTrue(courseConfiguration.CompletionViewEverySceneInCourse);
            Assert.IsTrue(courseConfiguration.CompletionCompleteAfterNOUniqueCourseVisit == 5);
            Assert.IsTrue(courseConfiguration.CompletionMustCompleteWithinSpecifiedAmountOfTimeMinute == 1);
            Assert.IsTrue(courseConfiguration.CompletionMustCompleteWithinSpecifiedAmountOfTimeDay == 1);
            Assert.IsTrue(courseConfiguration.CompletionRespondToCourseEvaluation);

        }

        /// <summary>
        ///A test for GetSequence
        ///</summary>
        [TestMethod()]
        public void GetCourseConfigurationLCMSHightPrecedenseTest()
        {
             _360Training.CourseServiceBusinessLogic.CourseManager target = new _360Training.CourseServiceBusinessLogic.CourseManager();
            int courseID = 93039; // TODO: Initialize to an appropriate value

            CourseConfiguration courseConfiguration = target.GetCourseConfiguration(123); // TODO: Initialize to an appropriate value            

            Assert.IsNotNull(courseConfiguration);
            Assert.IsTrue(courseConfiguration.PlayerEnableOrientaionScenes);
            Assert.IsTrue(courseConfiguration.PlayerEnableEndOfCourseScene);
            Assert.IsTrue(courseConfiguration.PlayerEnableContent);
            Assert.IsTrue(courseConfiguration.PlayerEnforceTimedOutline);
            Assert.IsTrue(courseConfiguration.PlayerIdleUserTimeout < 1800);
            Assert.IsTrue(courseConfiguration.ActionToTakeUponIdleTimeOut == _360Training.BusinessEntities.IdleTimeOut.CloseCourse);
            Assert.IsTrue(courseConfiguration.PlayerCourseFlow == _360Training.BusinessEntities.CourseFlow.FirstTimeLinear);
            Assert.IsFalse(courseConfiguration.PlayerAllowUserToReviewCourseAfterCompletion);
            Assert.IsTrue(courseConfiguration.CertificateEnabled);
            Assert.IsTrue(courseConfiguration.PlayerCourseEvaluation);
            Assert.IsTrue(courseConfiguration.PlayerDisplayCourseEvaluation == _360Training.BusinessEntities.DisplayCourseEvaluation.BeforeAndAfterPostAssessment);
            //Assert.IsTrue(courseConfiguration.PlayerMustCompleteCourseEvaluatio);
            Assert.IsTrue(courseConfiguration.CompletionPostAssessmentAttempted);
            Assert.IsTrue(courseConfiguration.CompletionPostAssessmentMastery);
            Assert.IsTrue(courseConfiguration.CompletionPreAssessmentMastery);
            Assert.IsTrue(courseConfiguration.CompletionQuizMastery);
            
            //Assert.IsTrue(courseConfiguration.CompletionSurvey);
            //Assert.IsTrue(courseConfiguration.CompletionViewEverySceneInCourse);
            //Assert.IsTrue(courseConfiguration.CompletionCompleteAfterNOUniqueCourseVisit == 5);
            Assert.IsTrue(courseConfiguration.CompletionMustCompleteWithinSpecifiedAmountOfTimeMinute == 10);
            Assert.IsTrue(courseConfiguration.CompletionUnitOfMustCompleteWithInSpecifiedAmountOfTime == TimeUnit.Days);
            //Assert.IsTrue(courseConfiguration.CompletionMustCompleteWithinSpecifiedAmountOfTimeDay == 1);
            //Assert.IsTrue(courseConfiguration.CompletionRespondToCourseEvaluation);
            Assert.IsTrue(courseConfiguration.PlayerEndOfCourseInstructions.Contains("LMS")); //From LMS Side

            Assert.IsTrue(courseConfiguration.PreAssessmentConfiguration.Enabled); //From LCMS Side
            Assert.IsTrue(courseConfiguration.PreAssessmentConfiguration.NOQuestion < 20);
            Assert.IsTrue(courseConfiguration.QuizConfiguration.Enabled); //From LMS Side
            Assert.IsTrue(courseConfiguration.QuizConfiguration.NOQuestion < 20); //From LMS Side
            Assert.IsTrue(courseConfiguration.PostAssessmentConfiguration.Enabled); //From LCMS Side
            Assert.IsTrue(courseConfiguration.PostAssessmentConfiguration.NOQuestion < 20); //From LCMS Side

            Assert.IsTrue(courseConfiguration.ValidationRequireIdentityValidation); //From LCMS Side
            Assert.IsTrue(courseConfiguration.ValidationNOMissedQuestionsAllowed < 3); //From LMS Side
            Assert.IsTrue(courseConfiguration.ValidationTimeBetweenQuestion < 600); //From LMS Side
            Assert.IsTrue(courseConfiguration.ValidationTimeToAnswerQuestion < 30); //From LMS Side

            Assert.IsTrue(courseConfiguration.EmbeddedAcknowledgmentEnabled); //From LCMS Side
            Assert.IsTrue(courseConfiguration.EmbeddedAcknowledgmentText.Contains("LCMS")); //From LCMS Side

            Assert.IsTrue(courseConfiguration.SeatTimeEnabled); //From LCMS Side
            Assert.IsTrue(courseConfiguration.SeatTimeInHour > 5); //From LCMS Side
            Assert.IsTrue(courseConfiguration.SeatTimeInMin < 30); //From LCMS Side
            Assert.IsTrue(courseConfiguration.MessageSeatTimeCourseLaunch.Contains("LMS")); //From LMS Side
            Assert.IsTrue(courseConfiguration.MessageSeatTimeExceeds.Contains("LCMS")); //From LCMS Side

        }


        /// <summary>
        ///A test for GetSequence
        ///</summary>
        [TestMethod()]
        public void GetCourseConfigurationLMSHightPrecedeneTest()
        {
            _360Training.CourseServiceBusinessLogic.CourseManager target = new _360Training.CourseServiceBusinessLogic.CourseManager();
            int courseID = 93041; // TODO: Initialize to an appropriate value

            CourseConfiguration courseConfiguration = target.GetCourseConfiguration(123); // TODO: Initialize to an appropriate value            

            Assert.IsNotNull(courseConfiguration);
            Assert.IsTrue(courseConfiguration.PlayerEnableOrientaionScenes);
            Assert.IsTrue(courseConfiguration.PlayerEnableEndOfCourseScene);
            Assert.IsTrue(courseConfiguration.PlayerEnableContent);
            Assert.IsTrue(courseConfiguration.PlayerEnforceTimedOutline);
            Assert.IsTrue(courseConfiguration.PlayerIdleUserTimeout < 1800);
            Assert.IsTrue(courseConfiguration.ActionToTakeUponIdleTimeOut == _360Training.BusinessEntities.IdleTimeOut.CloseCourse);
            Assert.IsTrue(courseConfiguration.PlayerCourseFlow == _360Training.BusinessEntities.CourseFlow.FirstTimeLinear);
            Assert.IsFalse(courseConfiguration.PlayerAllowUserToReviewCourseAfterCompletion);
            Assert.IsTrue(courseConfiguration.CertificateEnabled);
            Assert.IsTrue(courseConfiguration.PlayerCourseEvaluation);
            Assert.IsTrue(courseConfiguration.PlayerDisplayCourseEvaluation == _360Training.BusinessEntities.DisplayCourseEvaluation.BeforeAndAfterPostAssessment);
            //Assert.IsTrue(courseConfiguration.PlayerMustCompleteCourseEvaluatio);
            Assert.IsTrue(courseConfiguration.CompletionPostAssessmentAttempted);
            Assert.IsTrue(courseConfiguration.CompletionPostAssessmentMastery);
            Assert.IsTrue(courseConfiguration.CompletionPreAssessmentMastery);
            Assert.IsTrue(courseConfiguration.CompletionQuizMastery);

            //Assert.IsTrue(courseConfiguration.CompletionSurvey);
            //Assert.IsTrue(courseConfiguration.CompletionViewEverySceneInCourse);
            //Assert.IsTrue(courseConfiguration.CompletionCompleteAfterNOUniqueCourseVisit == 5);
            Assert.IsTrue(courseConfiguration.CompletionMustCompleteWithinSpecifiedAmountOfTimeMinute == 10);
            Assert.IsTrue(courseConfiguration.CompletionUnitOfMustCompleteWithInSpecifiedAmountOfTime == TimeUnit.Days);
            //Assert.IsTrue(courseConfiguration.CompletionRespondToCourseEvaluation);
            Assert.IsTrue(courseConfiguration.PlayerEndOfCourseInstructions.Contains("LMS")); //From LMS Side

            Assert.IsTrue(courseConfiguration.PreAssessmentConfiguration.Enabled); //From LCMS Side
            Assert.IsTrue(courseConfiguration.PreAssessmentConfiguration.NOQuestion < 20);
            Assert.IsTrue(courseConfiguration.QuizConfiguration.Enabled); //From LMS Side
            Assert.IsTrue(courseConfiguration.QuizConfiguration.NOQuestion < 20); //From LMS Side
            Assert.IsTrue(courseConfiguration.PostAssessmentConfiguration.Enabled); //From LCMS Side
            Assert.IsTrue(courseConfiguration.PostAssessmentConfiguration.NOQuestion < 20); //From LCMS Side

            Assert.IsTrue(courseConfiguration.ValidationRequireIdentityValidation); //From LCMS Side
            Assert.IsTrue(courseConfiguration.ValidationNOMissedQuestionsAllowed < 3); //From LMS Side
            Assert.IsTrue(courseConfiguration.ValidationTimeBetweenQuestion < 600); //From LMS Side
            Assert.IsTrue(courseConfiguration.ValidationTimeToAnswerQuestion < 30); //From LMS Side

            Assert.IsTrue(courseConfiguration.EmbeddedAcknowledgmentEnabled); //From LCMS Side
            Assert.IsTrue(courseConfiguration.EmbeddedAcknowledgmentText.Contains("LCMS")); //From LCMS Side

            Assert.IsTrue(courseConfiguration.SeatTimeEnabled); //From LCMS Side
            Assert.IsTrue(courseConfiguration.SeatTimeInHour > 5); //From LCMS Side
            Assert.IsTrue(courseConfiguration.SeatTimeInMin < 30); //From LCMS Side
            Assert.IsTrue(courseConfiguration.MessageSeatTimeCourseLaunch.Contains("LMS")); //From LMS Side
            Assert.IsTrue(courseConfiguration.MessageSeatTimeExceeds.Contains("LCMS")); //From LCMS Side

        }


        /// <summary>
        ///A test for GetSequence
        ///</summary>
        [TestMethod()]
        public void GetCourseConfigurationLMSHightPrecedenseMultipleApprovalTest()
        {
            _360Training.CourseServiceBusinessLogic.CourseManager target = new _360Training.CourseServiceBusinessLogic.CourseManager();
            int courseID = 92634; // TODO: Initialize to an appropriate value

            CourseConfiguration courseConfiguration = target.GetCourseConfiguration(123); // TODO: Initialize to an appropriate value            

            Assert.IsNotNull(courseConfiguration);
            Assert.IsTrue(courseConfiguration.PlayerEnableOrientaionScenes);
            Assert.IsTrue(courseConfiguration.PlayerEnableEndOfCourseScene);
            Assert.IsTrue(courseConfiguration.PlayerEnableContent);
            Assert.IsTrue(courseConfiguration.PlayerEnforceTimedOutline);
            Assert.IsTrue(courseConfiguration.PlayerIdleUserTimeout < 1800);
            Assert.IsTrue(courseConfiguration.ActionToTakeUponIdleTimeOut == _360Training.BusinessEntities.IdleTimeOut.CloseCourse);
            Assert.IsTrue(courseConfiguration.PlayerCourseFlow == _360Training.BusinessEntities.CourseFlow.FirstTimeLinear);
            Assert.IsFalse(courseConfiguration.PlayerAllowUserToReviewCourseAfterCompletion);
            Assert.IsTrue(courseConfiguration.CertificateEnabled);
            Assert.IsTrue(courseConfiguration.PlayerCourseEvaluation);
            Assert.IsTrue(courseConfiguration.PlayerDisplayCourseEvaluation == _360Training.BusinessEntities.DisplayCourseEvaluation.BeforeAndAfterPostAssessment);
            //Assert.IsTrue(courseConfiguration.PlayerMustCompleteCourseEvaluatio);
            Assert.IsTrue(courseConfiguration.CompletionPostAssessmentAttempted);
            Assert.IsTrue(courseConfiguration.CompletionPostAssessmentMastery);
            Assert.IsTrue(courseConfiguration.CompletionPreAssessmentMastery);
            Assert.IsTrue(courseConfiguration.CompletionQuizMastery);

            //Assert.IsTrue(courseConfiguration.CompletionSurvey);
            //Assert.IsTrue(courseConfiguration.CompletionViewEverySceneInCourse);
            //Assert.IsTrue(courseConfiguration.CompletionCompleteAfterNOUniqueCourseVisit == 5);
            Assert.IsTrue(courseConfiguration.CompletionMustCompleteWithinSpecifiedAmountOfTimeMinute == 10);
            Assert.IsTrue(courseConfiguration.CompletionUnitOfMustCompleteWithInSpecifiedAmountOfTime == TimeUnit.Days);
            //Assert.IsTrue(courseConfiguration.CompletionRespondToCourseEvaluation);
            Assert.IsTrue(courseConfiguration.PlayerEndOfCourseInstructions.Contains("LMS")); //From LMS Side

            Assert.IsTrue(courseConfiguration.PreAssessmentConfiguration.Enabled); //From LCMS Side
            Assert.IsTrue(courseConfiguration.PreAssessmentConfiguration.NOQuestion < 20);
            Assert.IsTrue(courseConfiguration.QuizConfiguration.Enabled); //From LMS Side
            Assert.IsTrue(courseConfiguration.QuizConfiguration.NOQuestion < 20); //From LMS Side
            Assert.IsTrue(courseConfiguration.PostAssessmentConfiguration.Enabled); //From LCMS Side
            Assert.IsTrue(courseConfiguration.PostAssessmentConfiguration.NOQuestion < 20); //From LCMS Side

            Assert.IsTrue(courseConfiguration.ValidationRequireIdentityValidation); //From LCMS Side
            Assert.IsTrue(courseConfiguration.ValidationNOMissedQuestionsAllowed < 3); //From LMS Side
            Assert.IsTrue(courseConfiguration.ValidationTimeBetweenQuestion < 600); //From LMS Side
            Assert.IsTrue(courseConfiguration.ValidationTimeToAnswerQuestion < 30); //From LMS Side

            Assert.IsTrue(courseConfiguration.EmbeddedAcknowledgmentEnabled); //From LCMS Side
            Assert.IsTrue(courseConfiguration.EmbeddedAcknowledgmentText.Contains("LCMS")); //From LCMS Side

            Assert.IsTrue(courseConfiguration.SeatTimeEnabled); //From LCMS Side
            Assert.IsTrue(courseConfiguration.SeatTimeInHour > 5); //From LCMS Side
            Assert.IsTrue(courseConfiguration.SeatTimeInMin < 30); //From LCMS Side
            Assert.IsTrue(courseConfiguration.MessageSeatTimeCourseLaunch.Contains("LMS")); //From LMS Side
            Assert.IsTrue(courseConfiguration.MessageSeatTimeExceeds.Contains("LCMS")); //From LCMS Side

        }

        /// <summary>
        ///A test for GetSequence
        ///</summary>
        [TestMethod()]
        public void GetPracticeExamAssessmentConfigurationTest()
        {
            _360Training.CourseServiceBusinessLogic.CourseManager target = new _360Training.CourseServiceBusinessLogic.CourseManager();
            int ExamID = 8; // TODO: Initialize to an appropriate value

            AssessmentConfiguration assessmentConfiguration = target.GetPraceticeExamAssessmentConfiguration(ExamID);  // TODO: Initialize to an appropriate value                        
            Assert.IsNotNull(assessmentConfiguration);

        }

        /// <summary>
        ///A test for GetEOCInstructions_VU
        ///</summary>
        [TestMethod()]
        public void GetEOCInstructions_VUTest()
        {
            ICP4.BusinessLogic.CourseManager.CourseManager target = new ICP4.BusinessLogic.CourseManager.CourseManager(); // TODO: Initialize to an appropriate value
            {
                //Existing Case Same course Published on Different ASV Test
                {
                    //Both on different ASV
                    //learnerid = 2135789
                    //learnerid 2 = 2135790
                    string resultingEOC = "Congratulations on successfully completing your course! You may now print out your completion certification unless you have been notified otherwise.  Please refer all questions to 360training at 800-442-1149 or email support@360training.com."; //should come 
                    int courseID = 274; // TODO: Initialize to an appropriate value
                    int learnerID = 928891; // TODO: Initialize to an appropriate value
                    string courseGUID = "7EE6265B4B2F4F5F825FE5CA9D6F7A31"; // TODO: Initialize to an appropriate value
                    int enrollmentID = 2135789; // TODO: Initialize to an appropriate value            
                    object actual = target.GetEOCInstructions_VU(courseID, learnerID, courseGUID, enrollmentID);
                    Assert.IsNotNull(actual);
                    Assert.IsInstanceOfType(actual, typeof(ICP4.CommunicationLogic.CommunicationCommand.EOCInstructions.ShowEOCInstructions));
                    Assert.IsTrue((actual as ICP4.CommunicationLogic.CommunicationCommand.EOCInstructions.ShowEOCInstructions).CommandName == ICP4.CommunicationLogic.CommunicationCommand.CommandNames.ShowEOCInstructions);
                    Assert.IsNotNull((actual as ICP4.CommunicationLogic.CommunicationCommand.EOCInstructions.ShowEOCInstructions).EOCInstructions);
                    Assert.IsTrue((actual as ICP4.CommunicationLogic.CommunicationCommand.EOCInstructions.ShowEOCInstructions).EOCInstructions.CourseID == courseID);
                    Assert.IsTrue((actual as ICP4.CommunicationLogic.CommunicationCommand.EOCInstructions.ShowEOCInstructions).EOCInstructions.LMS_VU == 1);
                    Assert.IsTrue((actual as ICP4.CommunicationLogic.CommunicationCommand.EOCInstructions.ShowEOCInstructions).EOCInstructions.CourseEOCInstructions.Contains(resultingEOC));

                }
                //Existing Case
                {
                    //Both on different ASV
                    //learnerid = 2135789
                    //learnerid 2 = 2135790
                    string resultingEOC = "<p><i>Congratulations! You have successfully completed your course. Click on the link to print your certificate.  Keep this certificate for your records as proof that you completed the course.  If you are a Texas licensee please DO NOT mail your certificate to the TDLR, your hours are reported electronically. If you ordered laminated certificate shipping, your certificate will be mailed to you. </p>"; //should come 
                    int courseID = 274; // TODO: Initialize to an appropriate value
                    int learnerID = 928891; // TODO: Initialize to an appropriate value
                    string courseGUID = "7EE6265B4B2F4F5F825FE5CA9D6F7A31"; // TODO: Initialize to an appropriate value
                    int enrollmentID = 2135790; // TODO: Initialize to an appropriate value            
                    object actual = target.GetEOCInstructions_VU(courseID, learnerID, courseGUID, enrollmentID);
                    Assert.IsNotNull(actual);
                    Assert.IsInstanceOfType(actual, typeof(ICP4.CommunicationLogic.CommunicationCommand.EOCInstructions.ShowEOCInstructions));
                    Assert.IsTrue((actual as ICP4.CommunicationLogic.CommunicationCommand.EOCInstructions.ShowEOCInstructions).CommandName == ICP4.CommunicationLogic.CommunicationCommand.CommandNames.ShowEOCInstructions);
                    Assert.IsNotNull((actual as ICP4.CommunicationLogic.CommunicationCommand.EOCInstructions.ShowEOCInstructions).EOCInstructions);
                    Assert.IsTrue((actual as ICP4.CommunicationLogic.CommunicationCommand.EOCInstructions.ShowEOCInstructions).EOCInstructions.CourseID == courseID);
                    Assert.IsTrue((actual as ICP4.CommunicationLogic.CommunicationCommand.EOCInstructions.ShowEOCInstructions).EOCInstructions.LMS_VU == 1);
                    Assert.IsTrue((actual as ICP4.CommunicationLogic.CommunicationCommand.EOCInstructions.ShowEOCInstructions).EOCInstructions.CourseEOCInstructions.Contains(resultingEOC));

                }
            }

            //non Existing Case for zero input
            {
                string resultingEOC = string.Empty;
                int courseID = 0; // TODO: Initialize to an appropriate value
                int learnerID = 0; // TODO: Initialize to an appropriate value
                string courseGUID = ""; // TODO: Initialize to an appropriate value
                int enrollmentID = 0; // TODO: Initialize to an appropriate value            
                object actual = target.GetEOCInstructions_VU(courseID, learnerID, courseGUID, enrollmentID);
                Assert.IsNotNull(actual);
                Assert.IsInstanceOfType(actual, typeof(ICP4.CommunicationLogic.CommunicationCommand.EOCInstructions.ShowEOCInstructions));
                Assert.IsTrue((actual as ICP4.CommunicationLogic.CommunicationCommand.EOCInstructions.ShowEOCInstructions).CommandName == ICP4.CommunicationLogic.CommunicationCommand.CommandNames.ShowEOCInstructions);
                Assert.IsNotNull((actual as ICP4.CommunicationLogic.CommunicationCommand.EOCInstructions.ShowEOCInstructions).EOCInstructions);
                Assert.IsTrue((actual as ICP4.CommunicationLogic.CommunicationCommand.EOCInstructions.ShowEOCInstructions).EOCInstructions.CourseID == courseID);
                Assert.IsTrue((actual as ICP4.CommunicationLogic.CommunicationCommand.EOCInstructions.ShowEOCInstructions).EOCInstructions.LMS_VU == 1);
                Assert.AreEqual(resultingEOC, (actual as ICP4.CommunicationLogic.CommunicationCommand.EOCInstructions.ShowEOCInstructions).EOCInstructions.CourseEOCInstructions);
            }
            //non Existing Case of enrollmentID
            {
                string resultingEOC = string.Empty;
                int courseID = 274; // TODO: Initialize to an appropriate value
                int learnerID = 917752; // TODO: Initialize to an appropriate value
                string courseGUID = "8241a155e5f14dd9842e74734b08eff0"; // TODO: Initialize to an appropriate value
                int enrollmentID = 0; // TODO: Initialize to an appropriate value            
                object actual = target.GetEOCInstructions_VU(courseID, learnerID, courseGUID, enrollmentID);
                Assert.IsNotNull(actual);
                Assert.IsInstanceOfType(actual, typeof(ICP4.CommunicationLogic.CommunicationCommand.EOCInstructions.ShowEOCInstructions));
                Assert.IsTrue((actual as ICP4.CommunicationLogic.CommunicationCommand.EOCInstructions.ShowEOCInstructions).CommandName == ICP4.CommunicationLogic.CommunicationCommand.CommandNames.ShowEOCInstructions);
                Assert.IsNotNull((actual as ICP4.CommunicationLogic.CommunicationCommand.EOCInstructions.ShowEOCInstructions).EOCInstructions);
                Assert.IsTrue((actual as ICP4.CommunicationLogic.CommunicationCommand.EOCInstructions.ShowEOCInstructions).EOCInstructions.CourseID == courseID);
                Assert.IsTrue((actual as ICP4.CommunicationLogic.CommunicationCommand.EOCInstructions.ShowEOCInstructions).EOCInstructions.LMS_VU == 1);
                Assert.AreEqual(resultingEOC, (actual as ICP4.CommunicationLogic.CommunicationCommand.EOCInstructions.ShowEOCInstructions).EOCInstructions.CourseEOCInstructions);
            }
            //non Existing Case of enrollmentID and guid
            {
                string resultingEOC = string.Empty;
                int courseID = 274; // TODO: Initialize to an appropriate value
                int learnerID = 917752; // TODO: Initialize to an appropriate value
                string courseGUID = ""; // TODO: Initialize to an appropriate value
                int enrollmentID = 0; // TODO: Initialize to an appropriate value            
                object actual = target.GetEOCInstructions_VU(courseID, learnerID, courseGUID, enrollmentID);
                Assert.IsNotNull(actual);
                Assert.IsInstanceOfType(actual, typeof(ICP4.CommunicationLogic.CommunicationCommand.EOCInstructions.ShowEOCInstructions));
                Assert.IsTrue((actual as ICP4.CommunicationLogic.CommunicationCommand.EOCInstructions.ShowEOCInstructions).CommandName == ICP4.CommunicationLogic.CommunicationCommand.CommandNames.ShowEOCInstructions);
                Assert.IsNotNull((actual as ICP4.CommunicationLogic.CommunicationCommand.EOCInstructions.ShowEOCInstructions).EOCInstructions);
                Assert.IsTrue((actual as ICP4.CommunicationLogic.CommunicationCommand.EOCInstructions.ShowEOCInstructions).EOCInstructions.CourseID == courseID);
                Assert.IsTrue((actual as ICP4.CommunicationLogic.CommunicationCommand.EOCInstructions.ShowEOCInstructions).EOCInstructions.LMS_VU == 1);
                Assert.AreEqual(resultingEOC, (actual as ICP4.CommunicationLogic.CommunicationCommand.EOCInstructions.ShowEOCInstructions).EOCInstructions.CourseEOCInstructions);
            }
            //non Existing Case of enrollmentID and guid and learner id
            {
                string resultingEOC = string.Empty;
                int courseID = 274; // TODO: Initialize to an appropriate value
                int learnerID = 0; // TODO: Initialize to an appropriate value
                string courseGUID = ""; // TODO: Initialize to an appropriate value
                int enrollmentID = 0; // TODO: Initialize to an appropriate value            
                object actual = target.GetEOCInstructions_VU(courseID, learnerID, courseGUID, enrollmentID);
                Assert.IsNotNull(actual);
                Assert.IsInstanceOfType(actual, typeof(ICP4.CommunicationLogic.CommunicationCommand.EOCInstructions.ShowEOCInstructions));
                Assert.IsTrue((actual as ICP4.CommunicationLogic.CommunicationCommand.EOCInstructions.ShowEOCInstructions).CommandName == ICP4.CommunicationLogic.CommunicationCommand.CommandNames.ShowEOCInstructions);
                Assert.IsNotNull((actual as ICP4.CommunicationLogic.CommunicationCommand.EOCInstructions.ShowEOCInstructions).EOCInstructions);
                Assert.IsTrue((actual as ICP4.CommunicationLogic.CommunicationCommand.EOCInstructions.ShowEOCInstructions).EOCInstructions.CourseID == courseID);
                Assert.IsTrue((actual as ICP4.CommunicationLogic.CommunicationCommand.EOCInstructions.ShowEOCInstructions).EOCInstructions.LMS_VU == 1);
                Assert.AreEqual(resultingEOC, (actual as ICP4.CommunicationLogic.CommunicationCommand.EOCInstructions.ShowEOCInstructions).EOCInstructions.CourseEOCInstructions);
            }

        }

        [TestMethod]
        public void GetSceneTemplate()
        {
            string result=@"\\10.0.1.47\ICPSceneTemplate\visual-left.html";
            ICP4.BusinessLogic.ICPCourseService.CourseService courseService = new ICP4.BusinessLogic.ICPCourseService.CourseService();
            //ICP4.

            courseService.Url = ConfigurationManager.AppSettings["ICPCourseService"];
            SceneTemplate sceneTemplate = new SceneTemplate();
            //(_360Training.BusinessEntities.SceneTemplate)
            sceneTemplate.TemplateHTMLURL = courseService.GetSceneTemplateHTML(1).TemplateHTMLURL;

            bool TF = Assert.Equals(result, sceneTemplate.TemplateHTMLURL);
        }


        [TestMethod]
        public void GetSequenceOnUpdatedCourseConfiguration()
        {
            int courseId = 7477;
            int source = 0;
            string learningSessionGuid = "1ED84D2D-0A9E-8249-FF9934AE3293B180";
            Sequence sequence = null;
            _360Training.CourseServiceBusinessLogic.CourseManager  courseManager = new _360Training.CourseServiceBusinessLogic.CourseManager();
            _360Training.BusinessEntities.CourseConfiguration courseConfiguration = courseManager.GetCourseConfiguration(123);

            ICP4.BusinessLogic.ICPTrackingService.TrackingService trackingService = new ICP4.BusinessLogic.ICPTrackingService.TrackingService();
            trackingService.Url = ConfigurationManager.AppSettings["ICPTrackingService"];
            source = trackingService.GetSource(learningSessionGuid);

            if (source == 1)
            {
                courseConfiguration.CertificateAssetID = 0;
                courseConfiguration.CertificateEnabled = false;
            }
            sequence= courseManager.GetSequence(7477, courseConfiguration);            

        }
        [TestMethod]
        public void GetCourseEvaluationQuestionsCount()
        {
            using (_360Training.CourseServiceBusinessLogic.CourseManager courseManager = new _360Training.CourseServiceBusinessLogic.CourseManager())
            {

                int courseID = 3;
                int count = 0; // courseManager.GetCourseEvaluationQuestionsCount(courseID);
                Assert.IsTrue(count == 2);

            }
        }

        [TestMethod]
        public void GetTOC()
        {
            using (_360Training.CourseServiceBusinessLogic.CourseManager courseManager = new _360Training.CourseServiceBusinessLogic.CourseManager())
            {

                int courseID = 90154;                
                TableOfContent tableOfContent = new TableOfContent();
                tableOfContent = courseManager.GetTableOfContent(courseID);
                Assert.IsNotNull(tableOfContent); 

            }
        }
    }
}
