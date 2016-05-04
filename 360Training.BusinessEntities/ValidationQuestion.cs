using System;
using System.Collections.Generic;
using System.Text;

namespace _360Training.BusinessEntities
{
    public class ValidationQuestion
    {
        private string questionStem;

        public string QuestionStem
        {
            get { return questionStem; }
            set { questionStem = value; }
        }

        private int languageID;

        public int LanguageID
        {
            get { return languageID; }
            set { languageID = value; }
        }

        private string answer;

        public string Answer
        {
            get { return answer; }
            set { answer = value; }
        }
        private int validitionQuestionId;

        public int ValiditionQuestionId
        {
            get { return validitionQuestionId; }
            set { validitionQuestionId = value; }
        }

        private string questionType;

        public string QuestionType
        {
            get { return questionType; }
            set { questionType = value; }
        }

        private List<ValidationQuestionOption> validationQuestionOption;

        public List<ValidationQuestionOption> ValidationQuestionOption
        {
            get { return validationQuestionOption; }
            set { validationQuestionOption = value; }
        }

    }
}
