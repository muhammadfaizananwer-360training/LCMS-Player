using System;
using System.Collections.Generic;
using System.Text;
 
namespace ICP4.CommunicationLogic.CommunicationCommand.ShowCourseLevelRating
{
    public class CourseLevelRating : IDisposable
    {
        private string templateHtml;
        /// <summary>
        /// Template HTML to render
        /// </summary>
        /// 
        private int currentUserCourseRating;

        public int CurrentUserCourseRating
        {
            get { return currentUserCourseRating; }
            set { currentUserCourseRating = value; }
        }

        private short _NPS_RATING;

        public short NPS_RATING
        {
            get { return _NPS_RATING; }
            set { _NPS_RATING = value; }
        }

        private string _USER_REVIEW_TEXT;

        public string USER_REVIEW_TEXT
        {
            get { return _USER_REVIEW_TEXT; }
            set { _USER_REVIEW_TEXT = value; }
        }

        private short _RATING_COURSE;

        public short RATING_COURSE
        {
            get { return _RATING_COURSE; }
            set { _RATING_COURSE = value; }
        }

        private short _RATING_SHOPPINGEXP;

        public short RATING_SHOPPINGEXP
        {
            get { return _RATING_SHOPPINGEXP; }
            set { _RATING_SHOPPINGEXP = value; }
        }

        private short _RATING_LEARNINGTECH;

        public short RATING_LEARNINGTECH
        {
            get { return _RATING_LEARNINGTECH; }
            set { _RATING_LEARNINGTECH = value; }
        }

        private short _RATING_CS;

        public short RATING_CS
        {
            get { return _RATING_CS; }
            set { _RATING_CS = value; }
        }

        private string _RATING_COURSE_SECONDARY;

        public string RATING_COURSE_SECONDARY
        {
            get { return _RATING_COURSE_SECONDARY; }
            set { _RATING_COURSE_SECONDARY = value; }
        }

        private string _RATING_SHOPPINGEXP_SECONDARY;

        public string RATING_SHOPPINGEXP_SECONDARY
        {
            get { return _RATING_SHOPPINGEXP_SECONDARY; }
            set { _RATING_SHOPPINGEXP_SECONDARY = value; }
        }

        private string _RATING_LEARNINGTECH_SECONDARY;

        public string RATING_LEARNINGTECH_SECONDARY
        {
            get { return _RATING_LEARNINGTECH_SECONDARY; }
            set { _RATING_LEARNINGTECH_SECONDARY = value; }
        }

        private string _RATING_CS_SECONDARY;

        public string RATING_CS_SECONDARY
        {
            get { return _RATING_CS_SECONDARY; }
            set { _RATING_CS_SECONDARY = value; }
        }


        public string TemplateHtml
        {
            get { return templateHtml; }
            set { templateHtml = value; }
        }

        private string _RATING_COURSE_SECONDARY_Q;

        public string RATING_COURSE_SECONDARY_Q
        {
            get { return _RATING_COURSE_SECONDARY_Q; }
            set { _RATING_COURSE_SECONDARY_Q = value; }
        }

        private string _RATING_SHOPPINGEXP_SECONDARY_Q;

        public string RATING_SHOPPINGEXP_SECONDARY_Q
        {
            get { return _RATING_SHOPPINGEXP_SECONDARY_Q; }
            set { _RATING_SHOPPINGEXP_SECONDARY_Q = value; }
        }

        private string _RATING_LEARNINGTECH_SECONDARY_Q;

        public string RATING_LEARNINGTECH_SECONDARY_Q
        {
            get { return _RATING_LEARNINGTECH_SECONDARY_Q; }
            set { _RATING_LEARNINGTECH_SECONDARY_Q = value; }
        }

        private string _RATING_CS_SECONDARY_Q;

        public string RATING_CS_SECONDARY_Q
        {
            get { return _RATING_CS_SECONDARY_Q; }
            set { _RATING_CS_SECONDARY_Q = value; }
        }


        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion

    }
}
