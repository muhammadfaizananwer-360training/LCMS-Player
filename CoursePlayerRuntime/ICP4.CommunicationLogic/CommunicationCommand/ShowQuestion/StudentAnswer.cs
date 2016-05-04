using System;
using System.Collections.Generic;
using System.Text;

namespace ICP4.CommunicationLogic.CommunicationCommand.ShowQuestion
{
    public class StudentAnswer
    {
        public StudentAnswer()
        {

            isCorrectlyAnswered = false;
            answerIDs = new List<int>();
            answerTexts = new List<string>();
            questionFeedBack = "";

        }

        private bool isCorrectlyAnswered;
        public bool IsCorrectlyAnswered
        {
            get { return isCorrectlyAnswered; }
            set { isCorrectlyAnswered = value; }
        }
        
        private string questionFeedBack;
        public string QuestionFeedBack
        {
            get { return questionFeedBack; }
            set { questionFeedBack = value; }
        }
        
        private List<int> answerIDs;
        public List<int> AnswerIDs
        {
            get { return answerIDs; }
            set { answerIDs = value; }
        }
        
        private List<string> answerTexts;
        public List<string> AnswerTexts
        {
            get { return answerTexts; }
            set { answerTexts = value; }
        }

        //Abdus Samad
        //LCMS-12105
        //Start
        bool toogleFlag;

        public bool ToogleFlag
        {
            get { return toogleFlag; }
            set { toogleFlag = value; }
        }
        //Stop
    }
}
