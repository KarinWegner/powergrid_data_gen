﻿using powergrid_data_gen.Generator;

namespace powergrid_data_gen
{
    internal class Program
    {
        static async Task Main(string[] args)
        {

            await DocumentWriter.Run();

            Console.WriteLine("Press any key to exit:");
            Console.ReadKey();
        }
    }
}
