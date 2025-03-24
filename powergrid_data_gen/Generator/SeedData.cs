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

        public static async Task<List<LogObject>> GetLogList()
        {
            List<LogObject> LogList = new List<LogObject>();
            for (int i = 0; i < 3; i++)
            {
            LogObject logObject = await GenerateLogObject();
                LogList.Add(logObject);
            }
            return LogList;
        }
        //public static async Task<ComponentPowerline> GenerateComponent()
        //{
           
        ////Select component type
        ////ToDo: Add transformer

        ////int randNumb = rnd.Next(0, 10);
        ////if (randNumb > 5)
        ////{

        //    //}
        //    //else
        //    //{
        //    //    component = new Powerline(); //ToDo: Change to Transformer
        //    //}
        //    //return component;
        //}

        public static async Task<LogObject> GenerateLogObject()
        {
            DateTime startDate = new DateTime(2025, 3, 15, 0, 0, 0) - TimeSpan.FromDays(5);
            DateTime endDate = startDate + TimeSpan.FromDays(_random.Next(2, 5));


            ComponentPowerline component = await GeneratePowerlineComponent(startDate, endDate);
            return new LogObject(startDate, endDate, component);


        }

        public static async Task<ComponentPowerline> GeneratePowerlineComponent(DateTime startDate, DateTime endDate)
        {
            PowerlineSpecification spec = await GeneratePowerlineSpecification();
            List<PowerlineLogData> log = await GenerateLogData(startDate, endDate, spec.length_km);
            return new ComponentPowerline(spec, log);
        }

        private static async Task<List<PowerlineLogData>> GenerateLogData(DateTime start, DateTime end, double? length_km)
        {
            string length_category = await Categorize_Length(length_km);
            double numberofDays = (end-start).TotalDays;
            Console.WriteLine($"startDate: "+start);
            Console.WriteLine($"endDate: "+end);
            Console.WriteLine($"number of days: "+numberofDays);
                List<PowerlineLogData> logBook= new List<PowerlineLogData>();
            
                DateTime logTime = start;
            for (int i = 0; i < numberofDays; i++)
            {
                for (int j = 0; j < 24; j++)
                {
                    PowerlineLogData hourLog = new PowerlineLogData
                    {
                        timestamp = logTime,
                       line_voltage = (double?)await GetRandomizedComponent(length_category, "voltage"),
                        power_capacity = (double?)await GetRandomizedComponent(length_category, "power")
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
            double? current_capacity = (double?)await GetRandomizedComponent(length_category, "currentcapacity");
            int? max_operating_temperature = Convert.ToInt32(await GetRandomizedComponent(length_category, "maxoperatingtemperature"));
            string? conductor_material = (string)await GetRandomizedComponent(length_category, "conductormaterial");
            string? line_type = (string)await GetRandomizedComponent(length_category, "linetype");
            string? insulation_type = (string)await GetRandomizedComponent(length_category, "insulationtype");

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
        public static async Task<object> GetRandomizedComponent(string category, string param)
        {

            if (ComponentData.powerData.ContainsKey(category) && ComponentData.powerData[category].ContainsKey(param))
            {
                return (object)await SeedData.GetDoubleResult(category, param);

            }

            else if (ComponentData.textbasedData.ContainsKey(param))
            {
                return (object)await SeedData.GetStringResult(param);
            }
            else 
                return null; // Return null if the parameter isn't found
        }

        public static async Task<string> GetStringResult(string param)
        {
            List<string> options = ComponentData.textbasedData[param].ToList();
            int count = options.Count;
            return options[_random.Next(count - 1)];
        }

        public static async Task<double?> GetDoubleResult(string category, string param)
        {
            var range = ComponentData.powerData[category][param];
            double? randResult = await GetRandomDouble(range.Min, range.Max);
            return randResult;
        }

        public static async Task<double> GetRandomDouble(double min, double max)
        {
            Random random = new Random();
            return min + (random.NextDouble() * (max - min));
        }

       
        

    }
    
    
}
