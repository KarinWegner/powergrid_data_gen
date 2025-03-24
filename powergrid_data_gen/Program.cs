using powergrid_data_gen.Generator;

namespace powergrid_data_gen
{
    internal class Program
    {

        static async Task Main(string[] args)
        {
            DocWriter docWriter = new DocWriter();
            await DocumentWriter.GenerateFiles(docWriter);
            
            Console.WriteLine("press any key to exit");
            Console.ReadKey();
        }
    }
}
