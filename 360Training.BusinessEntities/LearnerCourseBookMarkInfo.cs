using System;
using System.Collections.Generic;
using System.Text;

namespace _360Training.BusinessEntities
{
    public class LearnerCourseBookMarkInfo
    {
        private int bookMarkInfoID;
        public int BookMarkInfoID
        {
            get {return bookMarkInfoID; }
            set { bookMarkInfoID = value; }
        }
        private string item_GUID;
        public string Item_GUID
        {
            get { return item_GUID; }
            set { item_GUID = value; }
        }

        private string scene_GUID;
        public string Scene_GUID
        {
            get { return scene_GUID; }
            set { scene_GUID = value; }
        }
        private int learnerID;
        public int LearnerID
        {
            get { return learnerID; }
            set { learnerID = value; }
        }
        private int courseID;
        public int CourseID
        {
            get { return courseID; }
            set { courseID = value; }
        }
        private string flashSceneNo;
        public string FlashSceneNo
        {
            get { return flashSceneNo; }
            set { flashSceneNo = value; }
        }
        private string bookMarkTitle;
        public string BookMarkTitle
        {
            get { return bookMarkTitle; }
            set { bookMarkTitle = value; }
        }

        private string lastScene;

        public string LastScene
        {
            get { return lastScene; }
            set { lastScene = value; }
        }
        private bool isMovieEnded;

        public bool IsMovieEnded
        {
            get { return isMovieEnded; }
            set { isMovieEnded = value; }
        }
        private bool nextButtonState;

        public bool NextButtonState
        {
            get { return nextButtonState; }
            set { nextButtonState = value; }
        }

        private string firstSceneName;

        public string FirstSceneName
        {
            get { return firstSceneName; }
            set { firstSceneName = value; }
        }

        private DateTime createdDate;

        public DateTime CreatedDate
        {
            get { return createdDate; }
            set { createdDate = value; }
        }



        public LearnerCourseBookMarkInfo()
        {
            this.bookMarkInfoID = 0;
            this.bookMarkTitle = string.Empty;
            this.courseID = 0;
            this.flashSceneNo = string.Empty;
            this.item_GUID = string.Empty;
            this.learnerID = 0;
            this.lastScene = string.Empty; ;
            this.isMovieEnded = false;
            this.nextButtonState = true;
            this.firstSceneName = string.Empty;
            this.createdDate = DateTime.MinValue;
        }
    }
}
