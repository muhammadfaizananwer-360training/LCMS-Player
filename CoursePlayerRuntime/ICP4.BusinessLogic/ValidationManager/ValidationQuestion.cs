using System;
using System.Collections.Generic;
using System.Text;

namespace ICP4.BusinessLogic.ValidationManager
{
    public class ValidationQuestionData
    {
        private int validationQuestionID;

        public int ValidationQuestionID
        {
            get { return validationQuestionID; }
            set { validationQuestionID = value; }
        }

        private string questionType;

        public string QuestionType
        {
            get { return questionType; }
            set { questionType = value; }
        }

        private string questionText;

        public string QuestionText
        {
            get { return questionText; }
            set { questionText = value; }
        }

        private string answerText;

        public string AnswerText
        {
            get { return answerText; }
            set { answerText = value; }
        }
        private string studentAnswerText;

        public string StudentAnswerText
        {
            get { return studentAnswerText; }
            set { studentAnswerText = value; }
        }
        private bool isAnswered;

        public bool IsAnswered
        {
            get { return isAnswered; }
            set { isAnswered = value; }
        }
        private bool isCorrect;

        public bool IsCorrect
        {
            get { return isCorrect; }
            set { isCorrect = value; }
        }

        private List<ValidationQuestionOption> validationQuestionOption;

        public List<ValidationQuestionOption> ValidationQuestionOption
        {
            get { return validationQuestionOption; }
            set { validationQuestionOption = value; }
        }
    }
}
