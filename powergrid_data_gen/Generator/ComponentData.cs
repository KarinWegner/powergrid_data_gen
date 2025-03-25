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
            (double Min, double Max)>> powerlineData =
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
        public static readonly Dictionary<string, Dictionary<string,
            (double Min, double Max)>> transformerData =
       new Dictionary<string, Dictionary<string, (double, double)>>()
       {
            { "low_voltage", new Dictionary<string, (double, double)>
                {
                    {"primaryvoltage", (11, 69) },
                    {"secondaryvoltage", (1, 35) },
                    {"temperaturerise", (40, 65) },
                    {"loadcurrent", (100, 1000) },
                    {"currenttemperature", (-20, 130) },
                    {"powerfactor", (0.8, 1.0) },
                    {"activepower", (5, 10) },
                    {"reactivepower", (1, 5) }
                }
            },
            { "medium_voltage", new Dictionary<string, (double, double)>
                {
                    {"primaryvoltage", (33, 230) },
                    {"secondaryvoltage", (11, 69) },
                    {"temperaturerise", (30, 55) },
                    {"loadcurrent", (500, 5000) },
                    {"currenttemperature", (-30, 150) },
                    {"powerfactor", (0.8, 1.0) },
                    {"activepower", (10, 100) },
                    {"reactivepower", (5, 50) }
                }
            },
            { "high_voltage", new Dictionary<string, (double, double)>
                {
                    {"primaryvoltage", (110, 400) },
                    {"secondaryvoltage", (33, 230) },
                    {"temperaturerise", (25, 50) },
                    {"loadcurrent", (2000, 15000) },
                    {"currenttemperature", (-40, 160) },
                    {"powerfactor", (0.85, 1.0) },
                    {"activepower", (100, 500) },
                    {"reactivepower", (50, 200) }
                }
            },
            { "extra_high_voltage", new Dictionary<string, (double, double)>
                {
                    {"primaryvoltage", (220, 765) },
                    {"secondaryvoltage", (69, 400) },
                    {"temperaturerise", (25, 45) },
                    {"loadcurrent", (5000, 30000) },
                    {"currenttemperature", (-50, 170) },
                    {"powerfactor", (0.9, 1.0) },
                    {"activepower", (500, 1000) },
                    {"reactivepower", (200, 600) }
                }
            },
            { "ultra_high_voltage", new Dictionary<string, (double, double)>
                {
                    {"primaryvoltage", (400, 1100) },
                    {"secondaryvoltage", (110, 765) },
                    {"temperaturerise", (20, 40) },
                    {"loadcurrent", (10000, 50000) },
                    {"currenttemperature", (-50, 180) },
                    {"powerfactor", (0.95,1.0) },
                    {"activepower", (1000, 5000) },
                    {"reactivepower", (600, 2000) }
                }
           }
       };

        public static readonly Dictionary<string, Dictionary<string,
            List<string>>> textbasedPowerData =
       new Dictionary<string, Dictionary<string, List<string>>>()
       {
           { "low_voltage", new Dictionary<string, List<string>>
                {
                    {"coolingtype", new List<string>{"ONAN", "ONAF"} },
                }
           },
           { "medium_voltage", new Dictionary<string, List<string>>
                {
                    {"coolingtype", new List<string>{"ONAN", "ONAF"} },

                }
           },
           { "high_voltage", new Dictionary<string, List<string>>
                {
                    {"coolingtype", new List<string>{"ONAN", "OFAF", "OFWF"} },

                }
           },
           { "extra_high_voltage", new Dictionary<string, List<string>>
                {
                    {"coolingtype", new List<string>{"OFAF", "OFWF"} },

                }
           },
           { "ultra_high_voltage", new Dictionary<string, List<string>>
                {
                    {"coolingtype", new List<string>{"OFWF", "Hydrogen Cooling", "GIS"} },

                }
           },
       };
    }
}
