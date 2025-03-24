using Bogus;
using CsvHelper;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using powergrid_data_gen.entities;
using powergrid_data_gen.Generator;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices.Marshalling;

namespace powergrid_data_gen
{

    public static class DocumentWriter
    {
        public static async Task GenerateFiles() {

            var Data = await SeedData.GetLogList();
           

            string folderPath = "C:\\Users\\karin\\source\\repos\\powergrid_data_gen\\powergrid_data_gen\\documents\\"; // Replace with the folder path you want
            string filePath = Path.Combine(folderPath, "jsondata.txt");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            List<string> errors = new List<string>();



            string jsonData = JsonConvert.SerializeObject(Data, Formatting.Indented);
            // Write the JSON string to a file
            //  File.WriteAllText(filePath, jsonData);

            string csvfilePath = "C:\\Users\\karin\\source\\repos\\powergrid_data_gen\\powergrid_data_gen\\documents\\log_output.csv";
            CsvWriter.WriteLogObjectToCsv(Data, csvfilePath);

            Console.WriteLine("JSON file created at: " + filePath);
            Console.WriteLine(jsonData);
        }

    }

public class DocWriter
    {
        public  string JsonWriter(List<LogObject> Data)
    {

            var options = new JsonSerializerOptions();
            options.Converters.Add(new ObjectConverter<ComponentType>());
            options.Converters.Add(new ObjectConverter<ComponentSpecification>());
            options.Converters.Add(new ObjectConverter<ComponentLogData>());
            string json = System.Text.Json.JsonSerializer.Serialize(Data, options);
            return json;
        }
        public static void WriteLogObjectToCsv(List<LogObject> logList, string filePath)
        {
            if (logList == null || logList.Count == 0)
                throw new ArgumentException("Log list is empty or null."); 


            var sb = new StringBuilder();

            // CSV Header
            foreach (var logObject in logList)
            {
                if (logObject.component?.loggedData == null)
                    continue; // Skip if there's no log data

                foreach (var log in logObject.component.loggedData)
                {
                    sb.AppendLine($"{logObject.LogStart:yyyy-MM-dd HH:mm:ss},{logObject.LogEnd:yyyy-MM-dd HH:mm:ss}," +
                                  $"{logObject.component.spec?.component_id}, {logObject.component.spec?.length_km},{logObject.component.spec?.current_capacity},{logObject.component.spec?.max_operating_temperature}," +
                                  $"{logObject.component.spec?.conductor_material},{logObject.component.spec?.line_type},{logObject.component.spec?.insulation_type}," +
                                  $"{log.timestamp:yyyy-MM-dd HH:mm:ss},{log.power_capacity},{log.line_voltage}");
                }
            }

            // Write to CSV file
            File.WriteAllText(filePath, sb.ToString());

            Console.WriteLine("CSV file successfully written!");
        }

    }
}
