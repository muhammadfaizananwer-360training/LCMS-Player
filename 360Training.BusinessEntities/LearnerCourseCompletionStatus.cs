using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _360Training.BusinessEntities
{
    public class LearnerCourseCompletionStatus
    {
        #region Completion
        private bool isPostAssessmentAttempted;

        public bool IsPostAssessmentAttempted
        {
            get { return isPostAssessmentAttempted; }
            set { isPostAssessmentAttempted = value; }
        }
        private bool isPostAssessmentMasteryAchieved;

        public bool IsPostAssessmentMasteryAchieved
        {
            get { return isPostAssessmentMasteryAchieved; }
            set { isPostAssessmentMasteryAchieved = value; }
        }
        private bool isPreAssessmentMasteryAchieved;

        public bool IsPreAssessmentMasteryAchieved
        {
            get { return isPreAssessmentMasteryAchieved; }
            set { isPreAssessmentMasteryAchieved = value; }
        }
        private bool isQuizMasteryAchieved;

        public bool IsQuizMasteryAchieved
        {
            get { return isQuizMasteryAchieved; }
            set { isQuizMasteryAchieved = value; }
        }

        private bool isViewEverySceneInCourseAchieved;

        public bool IsViewEverySceneInCourseAchieved
        {
            get { return isViewEverySceneInCourseAchieved; }
            set { isViewEverySceneInCourseAchieved = value; }
        }
        private bool isCompleteAfterNOUniqueCourseVisitAchieved;

        public bool IsCompleteAfterNOUniqueCourseVisitAchieved
        {
            get { return isCompleteAfterNOUniqueCourseVisitAchieved; }
            set { isCompleteAfterNOUniqueCourseVisitAchieved = value; }
        }
        private bool isMustCompleteWithinSpecifiedAmountOfTimeMinuteAchieved;

        public bool IsMustCompleteWithinSpecifiedAmountOfTimeaAfterFirstAccess
        {
            get { return isMustCompleteWithinSpecifiedAmountOfTimeMinuteAchieved; }
            set { isMustCompleteWithinSpecifiedAmountOfTimeMinuteAchieved = value; }
        }
        private bool isMustCompleteWithinSpecifiedAmountOfTimeDayAchieved;

        public bool IsMustCompleteWithinSpecifiedAmountOfTimeAfterRegDate
        {
            get { return isMustCompleteWithinSpecifiedAmountOfTimeDayAchieved; }
            set { isMustCompleteWithinSpecifiedAmountOfTimeDayAchieved = value; }
        }

        private bool isembeddedAcknowledgmentAchieved;

        public bool IsembeddedAcknowledgmentAchieved
        {
            get { return isembeddedAcknowledgmentAchieved; }
            set { isembeddedAcknowledgmentAchieved = value; }
        }

        private bool isRespondToCourseEvaluationAchieved;

        public bool IsRespondToCourseEvaluationAchieved
        {
            get { return isRespondToCourseEvaluationAchieved; }
            set { isRespondToCourseEvaluationAchieved = value; }
        }

        private bool isCourseCompleted;

        public bool IsCourseCompleted
        {
            get { return isCourseCompleted; }
            set { isCourseCompleted = value; }
        }

        #endregion
    }
}
