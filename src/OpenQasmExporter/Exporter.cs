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
        class ConsoleToFileWriter : TextWriter, IDisposable
        {
            public override Encoding Encoding => m_PreviousConsoleOutput.Encoding;

            TextWriter m_PreviousConsoleOutput;
            FileStream m_OutputFile;
            StreamWriter m_StreamWriter;

            public ConsoleToFileWriter(TextWriter consoleTextWriter, string outputFileName)
            {
                m_PreviousConsoleOutput = consoleTextWriter;

                try
                {
                    if (!Path.IsPathRooted(outputFileName))
                    {
                        // TODO: Figure out how to get output to the same directory that `dotnet run` occurs (e.g. the main project directory)
                        // Console.WriteLine($"{Directory.GetCurrentDirectory()}");
                        // outputFileName = $"{Directory.GetCurrentDirectory()}{outputFileName}";
                    }

                    m_OutputFile = new FileStream(outputFileName, FileMode.OpenOrCreate, FileAccess.Write);
                    m_StreamWriter = new StreamWriter(m_OutputFile);

                    Console.SetOut(this);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Cannot open {outputFileName} for writing");
                    Console.WriteLine(e.Message);
                }
            }

            public override void Write(string message)
            {
                m_PreviousConsoleOutput.Write(message);
                m_StreamWriter.Write(message);
            }

            public override void WriteLine(string message)
            {
                m_PreviousConsoleOutput.WriteLine(message);
                m_StreamWriter.WriteLine(message);
            }

            public new void Dispose()
            {
                Console.SetOut(m_PreviousConsoleOutput);
                m_StreamWriter?.Close();
                m_OutputFile?.Close();
            }
        }
        public override string Name => nameof(Exporter);
        public string Circuit => writer.ToString();
        private StringWriter writer = new StringWriter();

        const string k_DefaultOutputFileName = "output.qasm";
        const int k_MaxQubits = 32;

        ConsoleToFileWriter m_ConsoleToFileWriter;
        
        public Exporter(string outputFileName, TextWriter outputTextWriter)
            : base(new QubitManager(k_MaxQubits, true))
        {
            m_ConsoleToFileWriter = new ConsoleToFileWriter(outputTextWriter, outputFileName);

            Console.WriteLine("OPENQASM 2.0;");
            Console.WriteLine("include \"qelib1.inc\";");
            Console.WriteLine($"qreg q[{k_MaxQubits}];");
            Console.WriteLine($"creg c[{k_MaxQubits}];");
        }

        public Exporter(TextWriter outputTextWriter)
            : this(k_DefaultOutputFileName, outputTextWriter)
        {
        }

        public Exporter(string outputFileName)
            : this(outputFileName, Console.Out)
        {
        }

        public Exporter()
            : this(Console.Out)
        {
        }

        public void Dispose()
        {
            m_ConsoleToFileWriter.Dispose();
        }

        internal void WriteOpenQasm (string line)
        {
            writer.WriteLine(line);
        }
    }
}
