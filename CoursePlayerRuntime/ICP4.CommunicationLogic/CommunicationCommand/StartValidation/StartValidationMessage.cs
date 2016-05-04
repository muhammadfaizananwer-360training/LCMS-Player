using System;
using System.Collections.Generic;
using System.Text;

namespace ICP4.CommunicationLogic.CommunicationCommand.StartValidation
{
    public class StartValidationMessage
    {
        private bool isOrientationScene;

        public bool IsOrientationScene
        {
            get { return isOrientationScene; }
            set { isOrientationScene = value; }
        }
    }
}
