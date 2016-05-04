using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using _360Training.BusinessEntities;
using _360Training.CourseServiceBusinessLogic;

namespace CoursePlayerBusinessLogicUnitTest
{
    class TestCourse
    {
        [Test]
        public void GetSequenceTest()
        {
            CourseManager target = new CourseManager(); // TODO: Initialize to an appropriate value
            //int courseID = 7385;
            int courseID = 54959;// TODO: Initialize to an appropriate value
            CourseConfiguration courseConfiguration = target.GetCourseConfiguration(courseID); // TODO: Initialize to an appropriate value            
            Sequence actual;
            
            System.Console.WriteLine("START TIME: " + DateTime.Now.ToString("HH:mm ss tt"));
            actual = target.GetSequence(courseID, courseConfiguration);
            System.Console.WriteLine("END TIME:"+DateTime.Now.ToString("HH:mm ss tt"));

            Assert.AreEqual(true, actual.SequenceItems.Count > 0);

        }
    }
}
