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

    public class PowerlineSpecification :ComponentSpecification
    {

        public string? component_id;
        public double? length_km; //"The length of the powerline in kilometres"
        public double? current_capacity; //Defines max current capacity
        public int? max_operating_temperature; //"The heat tolerance in Celsius before degredation occurs"
        public string? conductor_material; //"The material the line is made of. Impacts resistance"
        public string? line_type; //'Overhead' or 'Underground'
        public string? insulation_type; //"Defines durability and breakdown voltage"

    }
    public class PowerlineLogData : LoggedData
    {
        public DateTime timestamp;
        public double? power_capacity; //Defines total power the line can transmit"
        public double? line_voltage; //"Defines the transmission level"
    }
    public class ComponentTransformer
    {
        public TransformerSpecification spec;
        public List<TransformerLogData> loggedData;
        public static readonly List<string> datasetParams =
            new List<string>{
                "power_capacity, powercapacity",
                "line_voltage, linevoltage"};

        public ComponentTransformer(TransformerSpecification spec, List<TransformerLogData> datalog)
        {
            this.spec = spec;
            loggedData = datalog;
        }
    }
    public class TransformerSpecification
    {
        double? power_rating; //"Defines maximum load capacity"
        int? primary_voltage; // "Input voltage level")
        int? secondary_voltage; //"Output voltage level")
        int? frequency;         //"Hz")
        string? coolingtype; //"The cooling method the transformer uses. Decides temperaturerise"
        int? temperaturerise; //"Determines safe operating temperature")
    }

    public class TransformerLogData 
    {
        public DateTime timestamp;
        public double? load_current;
        public double? power_factor;
        public double? active_power;
        public double? reactive_power;

    }
}
