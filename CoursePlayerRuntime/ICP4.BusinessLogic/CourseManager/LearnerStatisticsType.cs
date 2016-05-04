using System;
using System.Collections.Generic;
using System.Text;

namespace ICP4.BusinessLogic.CourseManager
{
    public class LearnerStatisticsType
    {
        public const string PreAssessmentEnd = "PreAssessmentResultStatistic";
        public const string FlashAsset = "FlashAsset";
        public const string ContentAsset = "ContentAsset";
        public const string Question = "Question";
        public const string QuizEnd = "QuizAssessmentResultStatistic";
        public const string PracticeExamEnd = "PracticeAssessmentResultStatistic";
        public const string PostAssessmentEnd = "PostAssessmentResultStatistic";
        public const string IntroPage = "IntroPage";
        public const string EndOfCourseScene = "EndOfCourseScene";
        public const string LessonIntroductionScene = "LessonIntroductionScene";
        public const string CourseIntroduction = "CourseIntroduction";

        public const string PreAssessment = "PreAssessmentResultStatistic";
        public const string PostAssessment = "PostAssessmentResultStatistic";
        public const string Quiz = "QuizAssessmentResultStatistic";
        public const string PracticeExam = "PracticeAssessmentResultStatistic";

        /*
           Changes Start 
         * Add 3 New LearnerStatistics Type
         * Change made by Waqas Zakai 25-Feb-2010 11:48 
         * LCMS-3508
         
         */
        public const string EOCInstructions = "EOCInstructions";
        public const string CourseCertificate = "CourseCertificateScene";        
        public const string EmbeddedAcknowledgmentScene = "EmbeddedAcknowledgmentScene";
        public const string EmbeddedAcknowledgmentAgreedScene = "EmbeddedAcknowledgmentAgreedScene";

        /*
            Change End
         */

        public const string CourseEvaluation = "CourseEvaluation";
        public const string KnowledgeCheck = "KnowledgeCheck";

        public const string SpecialQuestionnaire = "SpecialQuestionnaire";

        // LCSM-11877
        public const string CourseRating = "CourseRatingScene";

    }
}
