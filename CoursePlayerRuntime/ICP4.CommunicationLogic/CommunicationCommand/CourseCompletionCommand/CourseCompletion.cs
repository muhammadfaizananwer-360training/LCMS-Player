using System;
using System.Collections.Generic;
using System.Text;

namespace ICP4.CommunicationLogic.CommunicationCommand.CourseCompletionCommand
{
    public class CourseCompletion : IDisposable
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

        #region IsCompletionEnabled
        private bool isPostAssessmentEnabled;

        public bool IsPostAssessmentEnabled
        {
            get { return isPostAssessmentEnabled; }
            set { isPostAssessmentEnabled = value; }
        }
        private bool isPostAssessmentMasteryEnabled;

        public bool IsPostAssessmentMasteryEnabled
        {
            get { return isPostAssessmentMasteryEnabled; }
            set { isPostAssessmentMasteryEnabled = value; }
        }


        private bool isPreAssessmentMasteryEnabled;

        public bool IsPreAssessmentMasteryEnabled
        {
            get { return isPreAssessmentMasteryEnabled; }
            set { isPreAssessmentMasteryEnabled = value; }
        }
        private bool isQuizMasteryEnabled;

        public bool IsQuizMasteryEnabled
        {
            get { return isQuizMasteryEnabled; }
            set { isQuizMasteryEnabled = value; }
        }

        private bool isViewEverySceneInCourseEnabled;

        public bool IsViewEverySceneInCourseEnabled
        {
            get { return isViewEverySceneInCourseEnabled; }
            set { isViewEverySceneInCourseEnabled = value; }
        }
        private bool isCompleteAfterNOUniqueCourseVisitEnabled;

        public bool IsCompleteAfterNOUniqueCourseVisitEnabled
        {
            get { return isCompleteAfterNOUniqueCourseVisitEnabled; }
            set { isCompleteAfterNOUniqueCourseVisitEnabled = value; }
        }
        private bool isMustCompleteWithinSpecifiedAmountOfTimeMinuteEnabled;

        public bool IsMustCompleteWithinSpecifiedAmountOfTimeaAfterFirstAccessEnabled
        {
            get { return isMustCompleteWithinSpecifiedAmountOfTimeMinuteEnabled; }
            set { isMustCompleteWithinSpecifiedAmountOfTimeMinuteEnabled = value; }
        }
        private bool isMustCompleteWithinSpecifiedAmountOfTimeDayEnabled;

        public bool IsMustCompleteWithinSpecifiedAmountOfTimeAfterRegDateEnabled
        {
            get { return isMustCompleteWithinSpecifiedAmountOfTimeDayEnabled; }
            set { isMustCompleteWithinSpecifiedAmountOfTimeDayEnabled = value; }
        }

        private bool isembeddedAcknowledgmentEnabled;

        public bool IsembeddedAcknowledgmentEnabled
        {
            get { return isembeddedAcknowledgmentEnabled; }
            set { isembeddedAcknowledgmentEnabled = value; }
        }

        private bool isRespondToCourseEvaluationEnabled;

        public bool IsRespondToCourseEvaluationEnabled
        {
            get { return isRespondToCourseEvaluationEnabled; }
            set { isRespondToCourseEvaluationEnabled = value; }
        }

        private int postAssessmentMasteryValue;

        public int PostAssessmentMasteryValue
        {
            get { return postAssessmentMasteryValue; }
            set { postAssessmentMasteryValue = value; }
        }
        private int quizAssessmentMasteryValue;

        public int QuizAssessmentMasteryValue
        {
            get { return quizAssessmentMasteryValue; }
            set { quizAssessmentMasteryValue = value; }
        }
        private int preAssessmentMasteryValue;

        public int PreAssessmentMasteryValue
        {
            get { return preAssessmentMasteryValue; }
            set { preAssessmentMasteryValue = value; }
        }
        private int completeAfterNOUniqueCourseVisitValue;

        public int CompleteAfterNOUniqueCourseVisitValue
        {
            get { return completeAfterNOUniqueCourseVisitValue; }
            set { completeAfterNOUniqueCourseVisitValue = value; }
        }
        private int isMustCompleteWithinSpecifiedAmountOfTimeAfterRegDateValue;

        public int IsMustCompleteWithinSpecifiedAmountOfTimeAfterRegDateValue
        {
            get { return isMustCompleteWithinSpecifiedAmountOfTimeAfterRegDateValue; }
            set { isMustCompleteWithinSpecifiedAmountOfTimeAfterRegDateValue = value; }
        }
        private int isMustCompleteWithinSpecifiedAmountOfTimeaAfterFirstAccessEnabledValue;

        public int IsMustCompleteWithinSpecifiedAmountOfTimeaAfterFirstAccessEnabledValue
        {
            get { return isMustCompleteWithinSpecifiedAmountOfTimeaAfterFirstAccessEnabledValue; }
            set { isMustCompleteWithinSpecifiedAmountOfTimeaAfterFirstAccessEnabledValue = value; }
        }

        //LCMS-11285
        private bool isAcceptAffidavitAcknowledgmentEnabled;

        public bool IsAcceptAffidavitAcknowledgmentEnabled
        {
            get { return isAcceptAffidavitAcknowledgmentEnabled; }
            set { isAcceptAffidavitAcknowledgmentEnabled = value; }
        }

        private bool isAcceptAffidavitAcknowledgment;

        public bool IsAcceptAffidavitAcknowledgment
        {
            get { return isAcceptAffidavitAcknowledgment; }
            set { isAcceptAffidavitAcknowledgment = value; }
        }

        private bool isSubmitSignedAffidavitEnabled;

        public bool IsSubmitSignedAffidavitEnabled
        {
            get { return isSubmitSignedAffidavitEnabled; }
            set { isSubmitSignedAffidavitEnabled = value; }
        }

        private bool isSubmitSignedAffidavit;
        public bool IsSubmitSignedAffidavit
        {
            get { return isSubmitSignedAffidavit; }
            set { isSubmitSignedAffidavit = value; }
        }
       
        //End


        #endregion

        #region Course Completion Text
        
        private string isPostAssessmentAttemptedText;

        public string IsPostAssessmentAttemptedText
        {
            get { return isPostAssessmentAttemptedText; }
            set { isPostAssessmentAttemptedText = value; }
        }
        private string isPostAssessmentMasteryAchievedText;

        public string IsPostAssessmentMasteryAchievedText
        {
            get { return isPostAssessmentMasteryAchievedText; }
            set { isPostAssessmentMasteryAchievedText = value; }
        }
        private string isPreAssessmentMasteryAchievedText;

        public string IsPreAssessmentMasteryAchievedText
        {
            get { return isPreAssessmentMasteryAchievedText; }
            set { isPreAssessmentMasteryAchievedText = value; }
        }
        private string isQuizMasteryAchievedText;

        public string IsQuizMasteryAchievedText
        {
            get { return isQuizMasteryAchievedText; }
            set { isQuizMasteryAchievedText = value; }
        }

        private string isViewEverySceneInCourseAchievedText;

        public string IsViewEverySceneInCourseAchievedText
        {
            get { return isViewEverySceneInCourseAchievedText; }
            set { isViewEverySceneInCourseAchievedText = value; }
        }
        private string isCompleteAfterNOUniqueCourseVisitAchievedText;

        public string IsCompleteAfterNOUniqueCourseVisitAchievedText
        {
            get { return isCompleteAfterNOUniqueCourseVisitAchievedText; }
            set { isCompleteAfterNOUniqueCourseVisitAchievedText = value; }
        }
        private string isMustCompleteWithinSpecifiedAmountOfTimeMinuteAchievedText;

        public string IsMustCompleteWithinSpecifiedAmountOfTimeaAfterFirstAccessText
        {
            get { return isMustCompleteWithinSpecifiedAmountOfTimeMinuteAchievedText; }
            set { isMustCompleteWithinSpecifiedAmountOfTimeMinuteAchievedText = value; }
        }
        private string isMustCompleteWithinSpecifiedAmountOfTimeDayAchievedText;

        public string IsMustCompleteWithinSpecifiedAmountOfTimeAfterRegDateText
        {
            get { return isMustCompleteWithinSpecifiedAmountOfTimeDayAchievedText; }
            set { isMustCompleteWithinSpecifiedAmountOfTimeDayAchievedText = value; }
        }

        private string isembeddedAcknowledgmentAchievedText;

        public string IsembeddedAcknowledgmentAchievedText
        {
            get { return isembeddedAcknowledgmentAchievedText; }
            set { isembeddedAcknowledgmentAchievedText = value; }
        }

        private string isRespondToCourseEvaluationAchievedText;

        public string IsRespondToCourseEvaluationAchievedText
        {
            get { return isRespondToCourseEvaluationAchievedText; }
            set { isRespondToCourseEvaluationAchievedText = value; }
        }
        
        #endregion

        private string courseCompletionReportText;

        public string CourseCompletionReportText
        {
            get { return courseCompletionReportText; }
            set { courseCompletionReportText = value; }
        }

        private string courseCompletionReportSummaryText;

        public string CourseCompletionReportSummaryText
        {
            get { return courseCompletionReportSummaryText; }
            set { courseCompletionReportSummaryText = value; }
        }

        private string courseCompletionReportInfo1;

        public string CourseCompletionReportInfo1
        {
            get { return courseCompletionReportInfo1; }
            set { courseCompletionReportInfo1 = value; }
        }
        private string courseCompletionReportInfo2;

        public string CourseCompletionReportInfo2
        {
            get { return courseCompletionReportInfo2; }
            set { courseCompletionReportInfo2 = value; }
        }
        private string courseCompletionReportInfo3;

        public string CourseCompletionReportInfo3
        {
            get { return courseCompletionReportInfo3; }
            set { courseCompletionReportInfo3 = value; }
        }
        

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
