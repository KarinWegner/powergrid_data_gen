using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace powergrid_data_gen.entities
{
    public class ComponentPowerline : ComponentType
    {
       
        public PowerlineSpecification spec;
        public List<PowerlineLogData> loggedData;
        public static readonly List<string> datasetParams =
            new List<string>{
                "power_capacity, powercapacity",
                "line_voltage, linevoltage"};

        public ComponentPowerline(PowerlineSpecification spec, List<PowerlineLogData> datalog) : base (spec,datalog)
        {
            this.spec = spec;
            loggedData=datalog;
        }
    }

    public class PowerlineSpecification :ComponentSpecification
    {

        public string? component_id;
        public double? length_km;
        public double? current_capacity;
        public int? max_operating_temperature;
        public string? conductor_material;
        public string? line_type;
        public string? insulation_type;

    }

    public class PowerlineLogData : ComponentLogData
    {
        public PowerlineLogData(DateTime time, double? powcap, double? linevol) : base(time)
        {
                
        }
        public DateTime timestamp;
        public double? power_capacity;
        public double? line_voltage;
    }
}
