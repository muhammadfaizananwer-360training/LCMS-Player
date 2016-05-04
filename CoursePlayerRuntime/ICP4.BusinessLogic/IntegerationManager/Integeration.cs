using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICP4.BusinessLogic.IntegerationManager
{
    public interface Integeration
    {
        void SynchStatsToExternalSystem(IntegerationStatistics integerationStatistics);
    }
}
