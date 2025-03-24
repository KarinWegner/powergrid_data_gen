using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace powergrid_data_gen.Generator
{
    public static class ComponentData
    {

        public static readonly Dictionary<string, List<string>> textbasedData =
            new Dictionary<string, List<string>>()
            {
                {
                    "conductormaterial", new List<string>
                    {
                        "aluminium",
                        "copper"
                    }
                },
                {
                    "linetype", new List<string>
                    {
                        "underground",
                        "overhead"
                    }
                },
                {
                    "insulationtype", new List<string>
                    {
                        "XLPE",
                        "composite",
                        "EPR",
                        "PVC"
                    }
                }
            };

        public static readonly Dictionary<string, Dictionary<string, 
            (double Min, double Max)>> powerData =
       new Dictionary<string, Dictionary<string, (double, double)>>()
       {
            { "low_voltage", new Dictionary<string, (double, double)>
                {
                    { "voltage", (0.1, 1)},
                    { "currentcapacity", (50, 500) },
                    { "power", (0.1, 5) },
                    { "maxoperatingtemperature", (75, 90) },
                    { "loadcurrent", (10, 500) }
                }
            },
            { "medium_voltage", new Dictionary<string, (double, double)>
                {
                    { "voltage", (1, 69) },
                    { "currentcapacity", (200, 2000) },
                    { "power", (5, 500) },
                    { "maxoperatingtemperature", (90, 120) },
                    { "loadcurrent", (100, 2000) }
                }
            },
            { "high_voltage", new Dictionary<string, (double, double)>
                {
                    { "voltage", (69, 765) },
                    { "currentcapacity", (500, 5000) },
                    { "power", (500, 10000) },
                    { "maxoperatingtemperature", (100, 250) },
                    { "loadcurrent", (1000, 5000) }
                }
            }
       };
        
    }
}
