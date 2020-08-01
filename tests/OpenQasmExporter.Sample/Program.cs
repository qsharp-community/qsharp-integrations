using System;
using System.Threading.Tasks;

namespace OpenQasmExporter.Sample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Exporting Test.qs");
            using (var exporter = new QSharpCommunity.Simulators.OpenQasmExporter.Exporter("Test.qasm"))
            {
                await Tests.SampleTest.Run(exporter);
            }
        }
    }
}
