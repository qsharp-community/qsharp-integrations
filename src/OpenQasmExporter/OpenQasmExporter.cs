using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Quantum.Intrinsic;
using Microsoft.Quantum.Samples.OpenQasmExporter.Circuits;
using Microsoft.Quantum.Simulation.Common;
using Microsoft.Quantum.Simulation.Core;

namespace Microsoft.Quantum.Samples.OpenQasmExporter
{
    public class OpenQasmExporter : SimulatorBase, IDisposable
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
        public override string Name => nameof(OpenQasmExporter);

        const int k_MaxQubits = 32;

        ConsoleToFileWriter m_ConsoleToFileWriter;
        
        public OpenQasmExporter(string outputFilename)
            : base(new QubitManagerTrackingScope(k_MaxQubits, true))
        {
            m_ConsoleToFileWriter = new ConsoleToFileWriter(Console.Out, outputFilename);

            RegisterPrimitiveOperationsGivenAsCircuits();

            Console.WriteLine("OPENQASM 2.0;");
            Console.WriteLine("include \"qelib1.inc\";");
            Console.WriteLine($"qreg q[{k_MaxQubits}];");
            Console.WriteLine($"creg c[{k_MaxQubits}];");
        }

        public OpenQasmExporter()
            : this("output.qasm")
        {
        }

        public void Dispose()
        {
            m_ConsoleToFileWriter.Dispose();
        }
        
        void RegisterPrimitiveOperationsGivenAsCircuits()
        {
            var primitiveOperationTypes =
                from op in typeof(X).Assembly.GetExportedTypes()
                where op.IsSubclassOf(typeof(AbstractCallable))
                select op;

            var primitiveOperationAsCircuits =
                from op in typeof(SingleQubitOp<>).Assembly.GetExportedTypes()
                where op.IsSubclassOf(typeof(AbstractCallable))
                    && op.Namespace == typeof(SingleQubitOp<>).Namespace
                select op;

            foreach (var operationType in primitiveOperationTypes)
            {
                var matchingCircuitTypes =
                    from op in primitiveOperationAsCircuits
                    where op.Name == operationType.Name
                    select op;

                var numberOfMatchesFound = matchingCircuitTypes.Count();
                if (numberOfMatchesFound == 0)
                {
                    // Use a default
                    if (typeof(Unitary<Qubit>).IsAssignableFrom(operationType))
                    {
                        var genericType = typeof(SingleQubitOp<>).MakeGenericType(operationType);
                        Register(operationType, genericType, typeof(ICallable));
                        continue;
                    }
                }

                Debug.Assert(
                    numberOfMatchesFound <= 1,
                    "There should be at most one matching operation.");
                
                if (numberOfMatchesFound == 1)
                    Register(operationType, matchingCircuitTypes.First(), typeof(ICallable));
            }
        }
    }
}
