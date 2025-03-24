using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace powergrid_data_gen.entities
{
    public class ComponentPowerline : ComponentType
    {
       
        public static readonly List<string> datasetParams =
            new List<string>{
                "power_capacity, powercapacity",
                "line_voltage, linevoltage"};

        public ComponentPowerline(PowerlineSpecification spec, List<ComponentLogData> datalog) : base (spec,datalog)
        {
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
        public double? power_capacity;
        public double? line_voltage;
        public PowerlineLogData(DateTime timestamp, double? powcap, double? linevol) : base(timestamp)
        {
            power_capacity = powcap;
            line_voltage = linevol;
        }
    }
}
