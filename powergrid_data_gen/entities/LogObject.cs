using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace powergrid_data_gen.entities
{
    public class LogObject
    {
        public DateTime LogStart;
        public DateTime LogEnd;

        public ComponentPowerline component;

        public LogObject(DateTime start, DateTime end, ComponentPowerline cpow)
        {
                component= cpow;
            LogStart = start;
            LogEnd =end;
        }
    }
}
