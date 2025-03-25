using Bogus;
using powergrid_data_gen.entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace powergrid_data_gen.Generator
{
    public static class SeedData
    {
        private static readonly Random _random = new Random();
        private static readonly int[] frequency_level = new int[2] {50, 60};

        public static async Task<List<LogObject>> GetLogList()
        {
            List<LogObject> LogList = new List<LogObject>();
            for (int i = 0; i < 3; i++)
            {
                if (_random.Next(0, 10) < 5)
                {
                    TransformerLogObject? logObject = (TransformerLogObject?)await GenerateLogObject("transformer");
                    if (logObject == null)
                        continue;
                    LogList.Add(logObject);
                }

                else
                {
                    PowerlineLogObject? logObject = (PowerlineLogObject?)await GenerateLogObject("powerline");
                    if (logObject == null)
                        continue;
                    LogList.Add(logObject);
                }
                             
            }
            return LogList;
        }

        public static async Task<LogObject?> GenerateLogObject(string componentType)
        {
            DateTime startDate = new DateTime(2025, 3, 15, 0, 0, 0);
            DateTime endDate = startDate + TimeSpan.FromDays(_random.Next(5, 10));

            if (componentType == "transformer")
            {
            ComponentTransformer component = await GenerateTransformerComponent(startDate, endDate);
            return new TransformerLogObject(startDate, endDate, component);

            }
            else if (componentType == "powerline")
            {
                ComponentPowerline component = await GeneratePowerlineComponent(startDate, endDate);
                return new PowerlineLogObject(startDate, endDate, component);

            }
            else
            {
                return null;
            }


        }

        public static async Task<ComponentPowerline> GeneratePowerlineComponent(DateTime startDate, DateTime endDate)
        {
            PowerlineSpecification spec = await GeneratePowerlineSpecification();
            List<PowerlineLogData> log = await GeneratePowerlineLogData(startDate, endDate, spec.length_km);
            return new ComponentPowerline(spec, log);
        }
        public static async Task<ComponentTransformer> GenerateTransformerComponent(DateTime startDate, DateTime endDate)
        {
            TransformerSpecification spec = await GenerateTransformerSpecification();
            List<TransformerLogData> log = await GenerateTransformerLogData(startDate, endDate, spec.power_rating);
            return new ComponentTransformer(spec, log);
        }

        private static async Task<List<TransformerLogData>> GenerateTransformerLogData(DateTime startDate, DateTime endDate, double? power_rating)
        {
            string voltage_category = Categorize_Voltage(power_rating);
            double numberofDays = (endDate - startDate).TotalDays;
            Console.WriteLine($"startDate: " + startDate);
            Console.WriteLine($"endDate: " + endDate);
            Console.WriteLine($"number of days: " + numberofDays);

            List<TransformerLogData> logBook = new List<TransformerLogData>();
            DateTime logTime = startDate;

            for (int i = 0; i < numberofDays; i++)
            {
                for (int j = 0; j < 24; j++)
                {
                    TransformerLogData hourLog = new TransformerLogData
                    {
                        timestamp = logTime,
                        load_current = (double?)await GetRandomizedComponent("transformer", voltage_category, "loadcurrent"),
                        temperature = (double?)await GetRandomizedComponent("transformer", voltage_category, "currenttemperature"),
                        power_factor = (double?)await GetRandomizedComponent("transformer", voltage_category, "powerfactor"),
                        active_power = (double?)await GetRandomizedComponent("transformer", voltage_category, "activepower"),
                        reactive_power = (double?)await GetRandomizedComponent("transformer", voltage_category, "reactivepower"),
                    };
                    logTime += TimeSpan.FromHours(1);
                    logBook.Add(hourLog);
                }
            }
            return logBook;

        }

        private static async Task<List<PowerlineLogData>> GeneratePowerlineLogData(DateTime startDate, DateTime endDate, double? length_km)
        {
            string length_category = await Categorize_Length(length_km);
            double numberofDays = (endDate-startDate).TotalDays;
            Console.WriteLine($"startDate: "+startDate);
            Console.WriteLine($"endDate: "+endDate);
            Console.WriteLine($"number of days: "+numberofDays);
                List<PowerlineLogData> logBook= new List<PowerlineLogData>();
            
                DateTime logTime = startDate;
            for (int i = 0; i < numberofDays; i++)
            {
                for (int j = 0; j < 24; j++)
                {
                    PowerlineLogData hourLog = new PowerlineLogData
                    {
                        timestamp = logTime,
                       line_voltage = (double?)await GetRandomizedComponent("powerline", length_category, "voltage"),
                        power_capacity = (double?)await GetRandomizedComponent("powerline", length_category, "power")
                    };
                    logTime += TimeSpan.FromHours(1);
                    logBook.Add(hourLog);
                }
            }
            return logBook;

           
        }

        public static async Task<PowerlineSpecification> GeneratePowerlineSpecification() 
        {
            string? serial_number = await GenerateSerialNumber();
            double? length_km = _random.Next(40, 1000);
            string length_category = await Categorize_Length(length_km);
            double? current_capacity = (double?)await GetRandomizedComponent("powerline",length_category, "currentcapacity");
            int? max_operating_temperature = Convert.ToInt32(await GetRandomizedComponent("powerline", length_category, "maxoperatingtemperature"));
            string? conductor_material = (string)await GetRandomizedComponent("powerline", length_category, "conductormaterial");
            string? line_type = (string)await GetRandomizedComponent("powerline",length_category, "linetype");
            string? insulation_type = (string)await GetRandomizedComponent("powerline", length_category, "insulationtype");

            PowerlineSpecification specData = new PowerlineSpecification
            {
                component_id = serial_number,
                length_km = length_km,
                current_capacity = current_capacity,
                max_operating_temperature = max_operating_temperature,
                conductor_material = conductor_material,
                line_type = line_type,
                insulation_type = insulation_type,
            };

            return specData;
        }
        private static async Task<TransformerSpecification> GenerateTransformerSpecification()
        {

            string? serial_number = await GenerateSerialNumber();
            double? power_rating = _random.Next(100, 1800);
            string voltage_category = Categorize_Voltage(power_rating);
            int ? primary_voltage = Convert.ToInt32(await GetRandomizedComponent("transformer", voltage_category, "primaryvoltage"));
            int? secondary_voltage = await GetSecondaryVoltage(primary_voltage, voltage_category);
            int frequency = frequency_level[_random.Next(0, 1)];
            string? cooling_type = (string?)await GetRandomizedComponent("transformer", voltage_category, "coolingtype");
            int? temperature_rise = Convert.ToInt32(await GetRandomizedComponent("transformer", voltage_category, "temperaturerise"));

            TransformerSpecification specdata = new TransformerSpecification
            {
                component_id = serial_number,
                power_rating = power_rating,
                primary_voltage = primary_voltage,
                secondary_voltage = secondary_voltage,
                frequency = frequency,
                cooling_type = cooling_type,
                temperature_rise = temperature_rise
            };
            return specdata;
        }

        private static async Task<int?> GetSecondaryVoltage(int? primary_voltage, string voltage_category)
        {
            int? secondary_voltage = Convert.ToInt32(await GetRandomizedComponent("transformer", voltage_category, "secondaryvoltage"));
            if (secondary_voltage >= primary_voltage) //ensures that secondary voltage is always below primary voltafe
            {
                secondary_voltage = primary_voltage-10;
            }
            return secondary_voltage;
        }

        private static string? Categorize_Voltage(double? power_rating)
        {
            if (power_rating <= 10)
                return "low_voltage";
            else if (power_rating <= 100)
                return "medium_voltage";
            else if (power_rating <= 500)
                return "high_voltage";
            else if (power_rating <= 1000)
                return "extra_high_voltage";
            else
                return "ultra_ high_voltage";
        }

        public static async Task<string> Categorize_Length(double? lineLength)
        {
            //Categorize power line based on length.
            if (lineLength <= 10)
                return "low_voltage";
            else if (lineLength <= 100)
                return "medium_voltage";
            else
                return "high_voltage";
        }
        public static async Task<string> GenerateSerialNumber()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ01234567890123456789";
            var stringChars = new char[_random.Next(8, 12)];

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[_random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);
        
            return finalString;
        }
        public static async Task<object> GetRandomizedComponent(string componentType, string category, string param)
        {
            if (componentType == "transformer")
            {

                if (ComponentData.transformerData.ContainsKey(category) && ComponentData.transformerData[category].ContainsKey(param))
                {
                    return (object)await SeedData.GetDoubleResult(category, param, componentType);

                }

                else if (ComponentData.textbasedTransformerData.ContainsKey(category) && ComponentData.textbasedTransformerData[category].ContainsKey(param))
                {
                    return (object)await SeedData.GetStringResult(category, param, componentType);
                }
                else
                    return null; // Return null if the parameter isn't found
            }
            else if (componentType == "powerline")
            {

                if (ComponentData.powerlineData.ContainsKey(category) && ComponentData.powerlineData[category].ContainsKey(param))
                {
                    return (object)await SeedData.GetDoubleResult(category, param, componentType);

                }

                else if (ComponentData.textbasedPowerlineData.ContainsKey(param))
                {
                    return (object)await SeedData.GetStringResult("", param, componentType);
                }
                else
                    return null; // Return null if the parameter isn't found
            }
            else
                return null; // Return null if the componenttype isn't found
        }

        public static async Task<string> GetStringResult(string category,string param, string componentType)
        {
            List<string> options = new List<string>();
            if (componentType == "transformer")
            {
                options = ComponentData.textbasedTransformerData[category][param].ToList();

            }
            else if (componentType == "powerline")
            {
                options = ComponentData.textbasedPowerlineData[param].ToList();

            }
            int count = options.Count;
            return options[_random.Next(count - 1)];
        }

        public static async Task<double?> GetDoubleResult(string category, string param, string componentType)
        {
            double? randResult;
            if (componentType == "transformer")
            {
               var range = ComponentData.transformerData[category][param];
                randResult = await GetRandomDouble(range.Min, range.Max);
            }
            else if (componentType == "powerline")
            {
               var range = ComponentData.powerlineData[category][param];
               randResult = await GetRandomDouble(range.Min, range.Max);
            }
            else
                throw new ArgumentOutOfRangeException(nameof(componentType));
           

            return randResult;
        }

        public static async Task<double> GetRandomDouble(double min, double max)
        {
            Random random = new Random();
            return min + (random.NextDouble() * (max - min));
        }

       
        

    }
    
    
}
