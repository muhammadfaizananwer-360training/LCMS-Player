using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICP4.BusinessLogic.IntegerationManager
{
    public class IntegerationFactory
    {
        public static Integeration GetObject(int source)
        {
            Integeration integeration = null;

            switch (source)
            {
                case 1:
                    integeration = new IntegerationVU();
                    break;
                case 0:
                    integeration = new IntegrationDirectDB();
                    break;
            }

            return integeration;
        }
    }
}
