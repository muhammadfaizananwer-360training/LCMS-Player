using System;
using System.Collections.Generic;
using System.Text;

namespace ICP4.BusinessLogic.CourseManager
{
    public class LockingReason
    {
        public const string ValidationFailed = "ValidationFailed";
        public const string MaxAttemptReach = "MaxAttemptReach";
        public const string NothingEnable = "NothingEnable";
        public const string GeneralCase = "GeneralCase";
        public const string MaxAttemptReachPostAssessment = "MaxAttemptReachPostAssessment";
        public const string MaxAttemptReachLessonAssessment = "MaxAttemptReachLessonAssessment";
        public const string MaxAttemptReachPreAssessment = "MaxAttemptReachPreAssessment";
        public const string MaxAttemptReachPracticeExam = "MaxAttemptReachPracticeExam";
        public const string FailedCompletionMustCompleteWithinSpecificAmountOfTimeMinute = "FailedCompletionMustCompleteWithinSpecificAmountOfTimeMinute";
        public const string FailedCompletionMustCompleteWithinSpecificAmountOfTimeAfterRegistration = "FailedCompletionMustCompleteWithinSpecificAmountOfTimeAfterRegistration";
        public const string MustStartCourseWithinSpecificAmountOfTimeAfterRegistration = "MustStartCourseWithinSpecificAmountOfTimeAfterRegistration";
        public const string IdleUserTimeElapsed = "IdleUserTimeElapsed";
        public const string CoursePublishedScene = "CoursePublishedScene";
        public const string CoursePublishedAssessment = "CoursePublishedAssessment";
        public const string ProctorLoginFailed = "ProctorLoginFailed";
        public const string ProctorAccountNotActive = "ProctorAccountNotActive";
        public const string ProctorNotPartOfCredential = "ProctorNotPartOfCredential";
        public const string ReportingFieldNotAttachedWithCourseApproval = "ReportingFieldNotAttachedWithCourseApproval";
        public const string CourseApprovalNotAttachedWithCourse = "CourseApprovalNotAttachedWithCourse";
        public const string ReportingFieldMisMatch = "ReportingFieldMisMatch";
        public const string MonitorFieldMisMatch = "MonitorFieldMisMatch";
        public const string ClickingAwayFromActiveWindow = "ClickingAwayFromActiveWindow";
    }
}
