using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace powergrid_data_gen.entities
{


    public abstract class LogObject 
    {
        public DateTime LogStart;
        public DateTime LogEnd;

        protected LogObject(DateTime logStart, DateTime logEnd)
        {
            LogStart = logStart;
            LogEnd = logEnd;
        }
    }
    public class TransformerLogObject: LogObject
    {
        public ComponentTransformer component;


        public TransformerLogObject(DateTime start, DateTime end, ComponentTransformer cpow) : base(start, end)
        {
            component = cpow;
        }
    }
    public class PowerlineLogObject : LogObject
    {

        public ComponentPowerline component;

        public PowerlineLogObject(DateTime start, DateTime end,ComponentPowerline cpow) :base(start,end)
        {
            component = cpow;
        }
    }

}
