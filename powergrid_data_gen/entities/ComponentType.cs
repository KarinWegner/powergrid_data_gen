using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace powergrid_data_gen.entities
{
    [JsonDerivedType(typeof(ComponentPowerline))]
    public class ComponentType
    {
        ComponentSpecification specData;
        List<string> loggedData; //List of the data that is logged hourly
        public ComponentType(ComponentSpecification spec, List<string> log)
        {
            specData = spec;
            loggedData = log;
        }
    }
    [JsonDerivedType(typeof(PowerlineSpecification))]
    public class ComponentSpecification
    {

    }
    public class LoggedData
    {
        
    }
}
