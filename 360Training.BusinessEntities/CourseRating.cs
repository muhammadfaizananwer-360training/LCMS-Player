using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _360Training.BusinessEntities
{
    public class CourseRating
    {
        private int _CourseID;

        public int CourseID
        {
            get { return _CourseID; }
            set { _CourseID = value; }
        }
        private int _Rating;

        public int Rating
        {
            get { return _Rating; }
            set { _Rating = value; }
        }
        private int _EnrollmentID;

        public int EnrollmentID
        {
            get { return _EnrollmentID; }
            set { _EnrollmentID = value; }
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

        private string _CourseGuid;

        public string CourseGuid
        {
            get { return _CourseGuid; }
            set { _CourseGuid = value; }
        }
        private double _AvgRating;

        public double AvgRating
        {
            get { return _AvgRating; }
            set { _AvgRating = value; }
        }
        private int _TotalRating;

        public int TotalRating
        {
            get { return _TotalRating; }
            set { _TotalRating = value; }
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

    }
}
