using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using _360Training.BusinessEntities;
using NUnitHelper;
using System.Configuration;
using _360Training.AssessmentServiceDataLogic.AssessmentDA;

namespace _360Training.CourseServiceBusinessLogic.NUnitTest

{
    [TestFixture]
    class AssessmentManagerTest
    {
        //private AssessmentServiceBusinessLogic.AssessmentManager assessmentManager;
        //private NUnitTestDataManager nUnitTestDataManager;       
        
        [Test]
        public void GetPostAssessment()
        {
            using (AssesmentDA assesmentDA = new AssesmentDA())
            {
                //List<AssessmentItem> PreAssessmentItems = new List<AssessmentItem>();
                //PreAssessmentItems = assesmentDA.GetPreAssessmentAssessmentItems(78137);

                //List<AssessmentItem> QuizAssessmentItems = new List<AssessmentItem>();
                //QuizAssessmentItems = assesmentDA.GetQuizAssessmentItems(40628);
                CourseConfiguration config = new CourseConfiguration();
                
                AssessmentServiceBusinessLogic.AssessmentManager AssessmentManager = new _360Training.AssessmentServiceBusinessLogic.AssessmentManager();
                List<AssessmentItem> assessmentList = AssessmentManager.GetPreAssessmentAssessmentItems(17775, config, null);

                //Console.WriteLine(deletedAssessmentItems[0].Disablerandomizeanswerchoicetf);
            }
        }
        
       
    }
}