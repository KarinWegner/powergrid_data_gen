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
        public static async Task Run() {

            var Data = await SeedData.GetLogList();
           

            string folderPath = "C:\\Users\\karin\\source\\repos\\powergrid_data_gen\\powergrid_data_gen\\Generated_Data\\"; // Replace with the folder path you want
            string filePath = Path.Combine(folderPath, "jsondata.txt");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            List<string> errors = new List<string>();
    

            string jsonData = JsonConvert.SerializeObject(Data, Formatting.Indented);
            // Write the JSON string to a file
             File.WriteAllText(filePath, jsonData);

            string csvfilePath = "C:\\Users\\karin\\source\\repos\\powergrid_data_gen\\powergrid_data_gen\\documents\\";
            CsvWriter.WriteLogObjectToCsv(Data, csvfilePath);

            Console.WriteLine("Generated date created at: " + filePath);
           
        }

    }

public class CsvWriter
    {
        public static void WriteLogObjectToCsv(List<LogObject> logList, string filePath)
        {
            if (logList == null || logList.Count == 0)
                throw new ArgumentException("Log list is empty or null."); 


            int logCount = 0;
            // CSV Header
            foreach (var logObject in logList)
            {
            var sb = new StringBuilder();
                if (logObject is TransformerLogObject)
                {
                    TransformerLogObject transobj = logObject as TransformerLogObject;
                    if (transobj.component?.loggedData == null)
                    continue; // Skip if there's no log data
                    sb.AppendLine("Log start, Log end, Component Id, Component type, Power rating, Primary voltage, Secondary voltage, Frequency, Cooling type, Temperature rise, Time stamp, Load current, Temperature, Power factor, Active power, Reactive power");
                    foreach (var log in transobj.component.loggedData)
                        { sb.Append($"{transobj.LogStart:yyyy-MM-dd HH:mm:ss},{transobj.LogEnd:yyyy-MM-dd HH:mm:ss}," +
                                    $"{transobj.component.spec?.component_id}, {transobj.component.spec?.component_type}, {transobj.component.spec?.power_rating}, {transobj.component.spec?.primary_voltage}, " +
                                    $"{transobj.component.spec?.secondary_voltage}, {transobj.component.spec?.frequency}, {transobj.component.spec?.cooling_type}" +
                                    $"{transobj.component.spec?.temperature_rise}, {log.timestamp:yyyy-MM-dd HH:mm:ss}, {log.load_current}, {log.temperature}" +
                                    $"{log.power_factor}, {log.active_power}, {log.reactive_power}"  ); 
                    }

                }
                if (logObject is PowerlineLogObject)
                {
                        PowerlineLogObject powlobj = logObject as PowerlineLogObject;
                        if (powlobj.component?.loggedData == null)
                            continue; // Skip if there's no log data
                    sb.AppendLine( "Log start, Log end, Component Id, Component type, Length (km), current capacity, max operating temperature, conductor material, line type, insulation type, Time stamp, power capacity, line voltage");
                   foreach (var log in powlobj.component.loggedData)
                    {
                        sb.AppendLine($"{powlobj.LogStart:yyyy-MM-dd HH:mm:ss},{powlobj.LogEnd:yyyy-MM-dd HH:mm:ss}," +
                                      $"{powlobj.component.spec?.component_id}, {powlobj.component.spec?.component_type}, {powlobj.component.spec?.length_km},{powlobj.component.spec?.current_capacity},{powlobj.component.spec?.max_operating_temperature}," +
                                      $"{powlobj.component.spec?.conductor_material},{powlobj.component.spec?.line_type},{powlobj.component.spec?.insulation_type}," +
                                      $"{log.timestamp:yyyy-MM-dd HH:mm:ss},{log.power_capacity},{log.line_voltage}");
                    }
                }
                string csvfilename = $"logdata{logCount++}.csv";
                string fullFilePath = filePath + csvfilename;
                File.WriteAllText(fullFilePath, sb.ToString());
            }

            // Write to CSV file

            Console.WriteLine("CSV file successfully written!");
        }
    }
}
