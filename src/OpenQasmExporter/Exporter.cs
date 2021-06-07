using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Quantum.Intrinsic;
using Microsoft.Quantum.Simulation.Common;
using Microsoft.Quantum.Simulation.Core;

namespace QSharpCommunity.Simulators.OpenQasmExporter
{
    public partial class Exporter : SimulatorBase, IDisposable
    {
        const int k_MaxQubits = 32;
        public override string Name => nameof(Exporter);
        public string Circuit => writer.ToString();

        private StringWriter writer = new StringWriter();
        
        public Exporter(StringWriter writer)
            : base(new QubitManager(k_MaxQubits, true))
        {
            // Todo: defer to end of program
            this.WriteOpenQasmLine("OPENQASM 2.0;");
            this.WriteOpenQasmLine("include \"qelib1.inc\";");
            this.WriteOpenQasmLine($"qreg q[{k_MaxQubits}];");
            this.WriteOpenQasmLine($"creg c[{k_MaxQubits}];");
        }

        public void Dispose()
        {
            writer.Dispose();
        }

        internal void WriteOpenQasmLine (string line)
        {
            writer.WriteLine(line);
        }

        //public Exporter(TextWriter outputTextWriter)
        //   : this(k_DefaultOutputFileName, outputTextWriter)
        // {
        // }

        //public Exporter(string outputFileName)
        //     : this(outputFileName, Console.Out)
        // {
        // }

        // public Exporter()
        //     : this(Console.Out)
        // {
        // }


    }
}
