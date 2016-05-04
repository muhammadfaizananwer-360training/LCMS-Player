using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _360Training.BusinessEntities
{
    public class CourseCompletionPolicy
    {

        private bool postAssessmentAttempted;
        private bool postAssessmentMasteryAchived;
        private bool preAssessmentMasteryAchived;
        private bool quizMasteryAchived;
        private bool viewEverySceneInCourse;
        private int completeAfterNumberOfUniqueVisits;
        private int postAssessmentMasteryScore;
        private int preAssessmentMasteryScore;
        private int quizMasteryScore;
        private int mustCompleteWithInSpecifiedAmountOfTime;
        private String mustCompleteWithInSpecifiedAmountOfTimeUnit;
        private bool respondToCourseEvaluation;
        private int mustCompleteWithInSpecifiedAmountOfDayAfterRegistration;
        private bool enableEmbeddedAknowledgement;
        private String postAssessmentScoreType;
        private String preAssessmentScoreType;
        private String quizScoreType;



        public bool PostAssessmentAttempted
        {
            get { return postAssessmentAttempted; }
            set { postAssessmentAttempted = value; }
        }

        public bool PostAssessmentMasteryAchived
        {
            get { return postAssessmentMasteryAchived; }
            set { postAssessmentMasteryAchived = value; }
        }

        public bool PreAssessmentMasteryAchived
        {
            get { return preAssessmentMasteryAchived; }
            set { preAssessmentMasteryAchived = value; }
        }

        public bool QuizMasteryAchived
        {
            get { return quizMasteryAchived; }
            set { quizMasteryAchived = value; }
        }

        public bool ViewEverySceneInCourse
        {
            get { return viewEverySceneInCourse; }
            set { viewEverySceneInCourse = value; }
        }

        public int CompleteAfterNumberOfUniqueVisits
        {
            get { return completeAfterNumberOfUniqueVisits; }
            set { completeAfterNumberOfUniqueVisits = value; }
        }

        public int PostAssessmentMasteryScore
        {
            get { return postAssessmentMasteryScore; }
            set { postAssessmentMasteryScore = value; }
        }

        public int PreAssessmentMasteryScore
        {
            get { return preAssessmentMasteryScore; }
            set { preAssessmentMasteryScore = value; }
        }

        public int QuizMasteryScore
        {
            get { return quizMasteryScore; }
            set { quizMasteryScore = value; }
        }

        public int MustCompleteWithInSpecifiedAmountOfTime
        {
            get { return mustCompleteWithInSpecifiedAmountOfTime; }
            set { mustCompleteWithInSpecifiedAmountOfTime = value; }
        }

        public String MustCompleteWithInSpecifiedAmountOfTimeUnit
        {
            get { return mustCompleteWithInSpecifiedAmountOfTimeUnit; }
            set { mustCompleteWithInSpecifiedAmountOfTimeUnit = value; }
        }

        public bool RespondToCourseEvaluation
        {
            get { return respondToCourseEvaluation; }
            set { respondToCourseEvaluation = value; }
        }

        public int MustCompleteWithInSpecifiedAmountOfDayAfterRegistration
        {
            get { return mustCompleteWithInSpecifiedAmountOfDayAfterRegistration; }
            set { mustCompleteWithInSpecifiedAmountOfDayAfterRegistration = value; }
        }

        public bool EnableEmbeddedAknowledgement
        {
            get { return enableEmbeddedAknowledgement; }
            set { enableEmbeddedAknowledgement = value; }
        }

        public String PostAssessmentScoreType
        {
            get { return postAssessmentScoreType; }
            set { postAssessmentScoreType = value; }
        }

        public String PreAssessmentScoreType
        {
            get { return preAssessmentScoreType; }
            set { preAssessmentScoreType = value; }
        }

        public String QuizScoreType
        {
            get { return quizScoreType; }
            set { quizScoreType = value; }
        }


        
         
    }
}
