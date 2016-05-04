using System;
using System.Collections.Generic;
using System.Text;

namespace ICP4.CommunicationLogic.CommunicationCommand.ShowQuestion
{
    public class QuestionType
    {
        public const string TrueFalse = "True False";
        public const string SingleSelectMCQ = "Single Select MCQ";
        public const string MultipleSelectMCQ = "Multiple Select MCQ";
        public const string Matching = "Matching";
        public const string TextInputFITB = "Fill in the Blanks";
        public const string NumericInputFITB = "Numeric Input FITB";
        public const string Ordering = "Ordering";
        public const string ImageTarget = "Image Selection";
        public const string Rating = "Rating";
    }
}
