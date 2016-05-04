using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP4.BusinessLogic.CourseManager;

namespace ICP4.BusinessLogic.CourseManager
{
    public class LearnerStatisticsTypeTranslator
    {
        public static string ConvertLearnerStatisticsTypeToSequenceType(string LearnerStatisticsType)
        {
            string SequenceType = LearnerStatisticsType;
            switch(LearnerStatisticsType)
            {
                case ICP4.BusinessLogic.CourseManager.LearnerStatisticsType.Quiz:
                    SequenceType = "Quiz";
                    break;

                case ICP4.BusinessLogic.CourseManager.LearnerStatisticsType.PostAssessment:
                    SequenceType = "PostAssessment";
                    break;
                case ICP4.BusinessLogic.CourseManager.LearnerStatisticsType.PreAssessment:
                    SequenceType = "PreAssessment";
                    break;
                
            }

            return SequenceType;
        }

        public static string ConvertLearnerSequenceTypeToAssessmentType(string LearnerStatisticsType)
        {
            string SequenceType = LearnerStatisticsType;
            switch (LearnerStatisticsType)
            {
                case "Quiz":
                    SequenceType = ICP4.BusinessLogic.CourseManager.LearnerStatisticsType.Quiz;
                    break;

                case "PostAssessment":
                    SequenceType = ICP4.BusinessLogic.CourseManager.LearnerStatisticsType.PostAssessment;
                    break;
                case "PreAssessment":
                    SequenceType = ICP4.BusinessLogic.CourseManager.LearnerStatisticsType.PreAssessment;
                    break;

            }

            return SequenceType;
        }
    }
}
