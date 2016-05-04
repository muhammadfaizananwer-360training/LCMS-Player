using System;
using System.Collections.Generic;
using System.Text;

namespace ICP4.CommunicationLogic.CommunicationCommand.ShowTopicScoreSummary
{
    public class TopicScoreSummary : IDisposable
    {

        private int topicID;
        public int TopicID
        {
            get { return topicID; }
            set { topicID = value; }
        }

        private int topicNumber;
        public int TopicNumber
        {
            get { return topicNumber; }
            set { topicNumber = value; }
        }

        private string topicName;
        public string TopicName
        {
            get { return topicName; }
            set { topicName = value; }
        }

        private int numberOfQuestionsAsked;
        public int NumberOfQuestionsAsked
        {
            get { return numberOfQuestionsAsked; }
            set { numberOfQuestionsAsked = value; }
        }

        private int correctAnswersCount;
        public int CorrectAnswersCount
        {
            get { return correctAnswersCount; }
            set { correctAnswersCount = value; }
        }

        private int inCorrectAnswersCount;
        public int InCorrectAnswersCount
        {
            get { return inCorrectAnswersCount; }
            set { inCorrectAnswersCount = value; }
        }

        private double weightedScore;
        public double WeightedScore
        {
            get { return weightedScore; }
            set { weightedScore = value; }
        }

        private double achievedScore;
        public double AchievedScore
        {
            get { return achievedScore; }
            set { achievedScore = value; }
        }

        private ICP4.CommunicationLogic.CommunicationCommand.ShowIndividualQuestionScore.ShowIndividualQuestionScore showIndividualQuestionScore;
        public ICP4.CommunicationLogic.CommunicationCommand.ShowIndividualQuestionScore.ShowIndividualQuestionScore ShowIndividualQuestionScore
        {
            get { return showIndividualQuestionScore; }
            set { showIndividualQuestionScore = value; }
        }


        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
