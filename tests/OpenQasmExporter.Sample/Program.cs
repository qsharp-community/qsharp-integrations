using System;
using System.Threading.Tasks;

namespace OpenQasmExporter.Sample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Exporting Test.qs");
            using (var exporter = new Microsoft.Quantum.Samples.OpenQasmExporter.OpenQasmExporter())
            {
                await Tests.SampleTest.Run(exporter);
            }
        }
    }
}
