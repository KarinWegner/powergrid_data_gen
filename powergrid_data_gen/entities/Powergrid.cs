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
        public double? length_km;
        public double? current_capacity;
        public double? max_operating_temperature;
        public string? conductor_material;
        public string? line_type;
        public string? insulation_type;

    }

    public class PowerlineLogData
    {
        public DateTime timestamp;
        public double? power_capacity;
        public double? line_voltage;
    }
}
