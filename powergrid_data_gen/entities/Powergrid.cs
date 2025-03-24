using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace powergrid_data_gen.entities
{
    public class ComponentPowerline
    {
       
        public PowerlineSpecification spec;
        public List<PowerlineLogData> loggedData;
        public static readonly List<string> datasetParams =
            new List<string>{
                "power_capacity, powercapacity",
                "line_voltage, linevoltage"};

        public ComponentPowerline(PowerlineSpecification spec, List<PowerlineLogData> datalog) 
        {
            this.spec = spec;
            loggedData=datalog;
        }
    }

    public class PowerlineSpecification 
    {

        public string? component_id;
        public double? length_km; //"The length of the powerline in kilometres"
        public double? current_capacity; //Defines max current capacity
        public int? max_operating_temperature; //"The heat tolerance in Celsius before degredation occurs"
        public string? conductor_material; //"The material the line is made of. Impacts resistance"
        public string? line_type; //'Overhead' or 'Underground'
        public string? insulation_type; //"Defines durability and breakdown voltage"

    }

    public class PowerlineLogData
    {
        public DateTime timestamp;
        public double? power_capacity; //Defines total power the line can transmit"
        public double? line_voltage; //"Defines the transmission level"
    }
}
