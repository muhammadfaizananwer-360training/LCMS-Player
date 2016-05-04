using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _360Training.BusinessEntities
{
    public class VisitedScene
    {
        
        private int sceneId;
        public int SceneId
        {
            get { return sceneId; }
            set { sceneId = value; }
        }

        private bool isCompleted;
        public bool IsCompleted
        {
            get { return isCompleted; }
            set { isCompleted = value; }
        }

        private String sceneGUID;
        public string SceneGUID
        {
            get { return sceneGUID; }
            set { sceneGUID = value; }
        }

        /*LCMS-8972 - Start*/
        private int timeSpent;
        public int TimeSpent
        {
            get { return timeSpent; }
            set { timeSpent = value; }
        }

        private int sceneDurationInSeconds;
        public int SceneDurationInSeconds
        {
            get { return sceneDurationInSeconds; }
            set { sceneDurationInSeconds = value; }
        }

        public int GetRemainingSceneDurationTimeInSeconds()
        {
            if (TimeSpent >= SceneDurationInSeconds)
            {
                return 0;
            }
            else
            {
                return SceneDurationInSeconds;
            }

        }
        /*LCMS-8972 - Start*/

    }
}
