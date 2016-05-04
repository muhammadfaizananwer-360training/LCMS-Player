using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using _360Training.BusinessEntities;
using NUnitHelper;
using System.Configuration;

namespace _360Training.CourseServiceBusinessLogic.NUnitTest

{
    [TestFixture]
    class CourseManagerTest
    {
        private CourseManager courseManager;
        private NUnitTestDataManager nUnitTestDataManager;       
        [SetUp]
        protected void SetUp()
        {            
            courseManager = new CourseManager();
            nUnitTestDataManager = NUnitTestDataManager.GetInstance();
            string testDataFilePath = ConfigurationManager.AppSettings["NUnitTestDataPath"];
            Assert.IsTrue(nUnitTestDataManager.LoadTestData(testDataFilePath), "Could Not Load Test Data file");                        
        }

        #region Course Configuration Test
        [Test]
        public void GetConfigurationTest()
        {
            //Get Test Data from List
            int courseIDForConfiguration = nUnitTestDataManager.GetIntValue("courseID-CourseConfiguration");
            int nonExistingCourseIDForConfiguration = 0;
            //Configuration Test
            //Case: Existing configuration with PostAssessmentMinimumTimeBeforeStart
            {
                CourseConfiguration courseConfiguration = courseManager.GetCourseConfiguration(courseIDForConfiguration);
                Assert.IsNotNull(courseConfiguration);
                Assert.Greater(courseConfiguration.CourseConfigurationID, 0);
                Assert.AreEqual(111, courseConfiguration.PostAssessmentMinimumTimeBeforeStart);
                Assert.AreEqual(TimeUnit.Days, courseConfiguration.PostAssessmentMinimumTimeBeforeStartUnit);
            }
            //Case: Existing configuration with Embedded ACK 
            {
                CourseConfiguration courseConfiguration = courseManager.GetCourseConfiguration(courseIDForConfiguration);
                Assert.IsNotNull(courseConfiguration);
                Assert.Greater(courseConfiguration.CourseConfigurationID, 0);
                Assert.AreEqual(courseConfiguration.EmbeddedAcknowledgmentEnabled, false);
                Assert.IsTrue(courseConfiguration.EmbeddedAcknowledgmentText.Contains("this is test Embedded ACK!!!!"));
            }
            //Case: Non Existing configuration (wrong entry)
            {
                CourseConfiguration courseConfiguration = courseManager.GetCourseConfiguration(nonExistingCourseIDForConfiguration);
                Assert.IsNotNull(courseConfiguration);
                Assert.AreEqual(courseConfiguration.CourseConfigurationID, 0);                               
            }
        }
        #endregion

        #region Sequence Methods
        [Test]
        public void GetSequenceTest()
        {
            //Test Data initialization
            int sceneTemplateIDForEmbeddedACK = nUnitTestDataManager.GetIntValue("sceneTemplateID-ACK");
            int selfPacedExistingDraftedCourseID = nUnitTestDataManager.GetIntValue("selfpaced-CourseID-Drafted");
            int selfPacedExistingCourseDraftedWithACKOFF = nUnitTestDataManager.GetIntValue("courseID-Drafted-ACK-OFF");
            int selfPacedExistingPublishedCourseID = nUnitTestDataManager.GetIntValue("selfpaced-CourseID-Published");
            int selfPacedExistingCoursePublishedWithACKOFF = nUnitTestDataManager.GetIntValue("courseID-Published-ACK-OFF");

            //Drafted Course Test for Sequence
            //Case: Drafted Course Embedded ACK test with ACK ON
            {
                //first get the course configuration
                CourseConfiguration courseConfiguration = courseManager.GetCourseConfiguration(selfPacedExistingDraftedCourseID);
                Sequence courseSequence = courseManager.GetSequence(selfPacedExistingDraftedCourseID, courseConfiguration);
                Assert.IsNotNull(courseSequence);
                Assert.IsNotNull(courseSequence.SequenceItems);
                Assert.IsTrue(courseSequence.SequenceItems.Exists(delegate(SequenceItem sequenceItem) { return sequenceItem.SequenceItemType == SequenceItemType.EmbeddedAcknowledgmentScene && sequenceItem.SceneTemplateID == sceneTemplateIDForEmbeddedACK; }));
                //Get the index of Post Assessment 
                int postAssessmentIndex = courseSequence.SequenceItems.FindIndex(delegate(SequenceItem sequenceItem) { return sequenceItem.SequenceItemType == SequenceItemType.PostAssessment; });
                //Get the index of ACK scene
                int ackIndex = courseSequence.SequenceItems.FindIndex(delegate(SequenceItem sequenceItem) { return sequenceItem.SequenceItemType == SequenceItemType.EmbeddedAcknowledgmentScene; });
                // ACK scene should be before post assessment
                Assert.Greater(postAssessmentIndex, ackIndex);
            }
            //Case: Drafted Course Embedded ACK test with ACK OFF
            {
                //first get the course configuration
                CourseConfiguration courseConfiguration = courseManager.GetCourseConfiguration(selfPacedExistingCourseDraftedWithACKOFF);
                Sequence courseSequence = courseManager.GetSequence(selfPacedExistingCourseDraftedWithACKOFF, courseConfiguration);
                Assert.IsNotNull(courseSequence);
                Assert.IsNotNull(courseSequence.SequenceItems);
                //Sequence should not contain ACK Scene
                Assert.IsFalse(courseSequence.SequenceItems.Exists(delegate(SequenceItem sequenceItem) { return sequenceItem.SequenceItemType == SequenceItemType.EmbeddedAcknowledgmentScene && sequenceItem.SceneTemplateID == sceneTemplateIDForEmbeddedACK; }));
            }
            //Case: Published Course Embedded ACK test with ACK ON
            {
                //first get the course configuration
                CourseConfiguration courseConfiguration = courseManager.GetCourseConfiguration(selfPacedExistingPublishedCourseID);
                Sequence courseSequence = courseManager.GetSequence(selfPacedExistingPublishedCourseID, courseConfiguration);
                Assert.IsNotNull(courseSequence);
                Assert.IsNotNull(courseSequence.SequenceItems);
                Assert.IsTrue(courseSequence.SequenceItems.Exists(delegate(SequenceItem sequenceItem) { return sequenceItem.SequenceItemType == SequenceItemType.EmbeddedAcknowledgmentScene && sequenceItem.SceneTemplateID == sceneTemplateIDForEmbeddedACK; }));
                //Get the index of Post Assessment 
                int postAssessmentIndex = courseSequence.SequenceItems.FindIndex(delegate(SequenceItem sequenceItem) { return sequenceItem.SequenceItemType == SequenceItemType.PostAssessment; });
                //Get the index of ACK scene
                int ackIndex = courseSequence.SequenceItems.FindIndex(delegate(SequenceItem sequenceItem) { return sequenceItem.SequenceItemType == SequenceItemType.EmbeddedAcknowledgmentScene; });
                // ACK scene should be before post assessment
                Assert.Greater(postAssessmentIndex, ackIndex);
            }
            //Case: Published Course Embedded ACK test with ACK OFF
            {
                //first get the course configuration
                CourseConfiguration courseConfiguration = courseManager.GetCourseConfiguration(selfPacedExistingCoursePublishedWithACKOFF);
                Sequence courseSequence = courseManager.GetSequence(selfPacedExistingCoursePublishedWithACKOFF, courseConfiguration);
                Assert.IsNotNull(courseSequence);
                Assert.IsNotNull(courseSequence.SequenceItems);
                //Sequence should not contain ACK Scene
                Assert.IsFalse(courseSequence.SequenceItems.Exists(delegate(SequenceItem sequenceItem) { return sequenceItem.SequenceItemType == SequenceItemType.EmbeddedAcknowledgmentScene && sequenceItem.SceneTemplateID == sceneTemplateIDForEmbeddedACK; }));
            }
            //Testing using List values with Test Case Descriptions
            {
                //Load Test Cases / Values
                List<int> courseIDsToTest = nUnitTestDataManager.GetIntListValues("courseID-Configuration-TestCases");
                List<string> courseIDsTestCases = nUnitTestDataManager.GetVariableListDescriptions("courseID-Configuration-TestCases");
                for (int count = 0; count < courseIDsToTest.Count; count++)
                {
                    int courseIDToTest = courseIDsToTest[count];
                    string testCase = courseIDsTestCases[count];
                    string failureMessage = "Test Case Failed on: '" + testCase + "' - CourseID: " + courseIDToTest.ToString();
                    //first get the course configuration
                    CourseConfiguration courseConfiguration = courseManager.GetCourseConfiguration(courseIDToTest);
                    Sequence courseSequence = courseManager.GetSequence(courseIDToTest, courseConfiguration);
                    Assert.IsNotNull(courseSequence, failureMessage); 
                    Assert.IsNotNull(courseSequence.SequenceItems,failureMessage);                                        
                }                
            }
        }        
        #endregion
        #region Scene Template Tests        
        [Test]
        public void GetSceneTemplateHTMLTest()
        {
            //Data Initialization
            int sceneTemplateIDForEmbeddedACK = nUnitTestDataManager.GetIntValue("sceneTemplateID-ACK");
             //Test Case: Acknowledgment Html SceneTemplate
            {   
                SceneTemplate templateToGet = courseManager.GetSceneTemplateHTML(sceneTemplateIDForEmbeddedACK);
                Assert.IsNotNull(templateToGet);                
                Assert.IsNotNullOrEmpty(templateToGet.TemplateHTML);                
            }
           }
        #endregion 
        
        #region Course Evaluation Test
        [Test]
        public void GetCourseEvaluationByCourseIDTest()
        {
            int courseID = nUnitTestDataManager.GetIntValue("courseID-CourseEvaluation-TestCase");
            {
                CourseEvaluation courseEval = courseManager.GetCourseEvaluationByCourseID(courseID);
                Assert.IsNotNull(courseEval);
                Assert.AreEqual(2856, courseEval.ID);
                Assert.AreEqual("My New Survey", courseEval.Name);
                Assert.AreEqual(1, courseEval.ShowAllTF);
                Assert.IsNotNull(courseEval.CourseEvaluationQuestions);
                Assert.AreEqual(1, courseEval.CourseEvaluationQuestions.Count);
            }
        }
        [Test]
        public void GetCourseEvaluationQuestionsTest()
        {
            int courseEvaluationID = nUnitTestDataManager.GetIntValue("CourseEvaluationID-CourseEvaluationQuestion-TestCase");
            {
                List<CourseEvaluationQuestion> courseEvalQuestions = courseManager.GetCourseEvaluationQuestions(courseEvaluationID);
                Assert.IsNotNull(courseEvalQuestions);
                Assert.AreEqual(1,courseEvalQuestions.Count);
                Assert.AreEqual(5,courseEvalQuestions[0].CourseEvaluationAnswers.Count);                
            }
        }
        #endregion
    }
}