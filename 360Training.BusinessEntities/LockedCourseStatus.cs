using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _360Training.BusinessEntities
{
    public class LockedCourseStatus
    {
        private string enrollmentID;

        public string EnrollmentID
        {
            get { return enrollmentID; }
            set { enrollmentID = value; }
        }

        private bool lockedStatus;

        public bool LockedStatus
        {
            get { return lockedStatus; }
            set { lockedStatus = value; }
        }

        private string finalMessage;

        public string FinalMessage
        {
            get { return finalMessage; }
            set { finalMessage = value; }
        }

        //private string lockedType;

        //public string LockedType
        //{
        //    get { return lockedType; }
        //    set { lockedType = value; }
        //}

     
            
    }
}
