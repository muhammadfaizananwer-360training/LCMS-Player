using System;
using System.Collections.Generic;
using System.Text;

namespace ICP4.CommunicationLogic.CommunicationCommand.ShowQuestion
{
    [Serializable]
    public class ImageTargetCoordinate
    {
        private int xPos;

        public int XPos
        {
            get { return xPos; }
            set { xPos = value; }
        }

        private int yPos;

        public int YPos
        {
            get { return yPos; }
            set { yPos = value; }
        }

        private int width;

        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        private int height;

        public int Height
        {
            get { return height; }
            set { height = value; }
        }

        private int coordinateOrder;

        public int CoordinateOrder
        {
            get { return coordinateOrder; }
            set { coordinateOrder = value; }
        }

        public ImageTargetCoordinate()
        {
            this.xPos = 0;
            this.yPos = 0;
            this.width = 0;
            this.height = 0;
            this.coordinateOrder = 0;
        }

 
    }
}
