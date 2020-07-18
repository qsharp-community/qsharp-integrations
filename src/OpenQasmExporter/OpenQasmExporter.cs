using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Microsoft.Quantum.Intrinsic;
using Microsoft.Quantum.Samples.OpenQasmExporter.Circuits;
using Microsoft.Quantum.Simulation.Common;
using Microsoft.Quantum.Simulation.Core;

namespace Microsoft.Quantum.Samples.OpenQasmExporter
{
    public class OpenQasmExporter : SimulatorBase, IDisposable
    {
        const int k_MaxQubits = 32;

        string m_OutputFilename = "output.qasm";
        FileStream m_OutputFile;
        TextWriter m_PreviousConsoleOutput = Console.Out;
        StreamWriter m_StreamWriter;

        public OpenQasmExporter(string outputFilename)
        {
            m_OutputFilename = outputFilename;
        }

        public OpenQasmExporter()
            : base(new QubitManagerTrackingScope(k_MaxQubits, true))
        {
            try
            {
                m_OutputFile = new FileStream(m_OutputFilename, FileMode.OpenOrCreate, FileAccess.Write);
                m_StreamWriter = new StreamWriter(m_OutputFile);
                Console.SetOut(m_StreamWriter);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Cannot open {m_OutputFilename} for writing");
                Console.WriteLine(e.Message);
            }

            RegisterPrimitiveOperationsGivenAsCircuits();

            Console.WriteLine("OPENQASM 2.0;");
            Console.WriteLine("include \"qelib1.inc\";");
            Console.WriteLine($"qreg q[{k_MaxQubits}];");
            Console.WriteLine($"creg c[{k_MaxQubits}];");
        }

        public override string Name => nameof(OpenQasmExporter);

        public void Dispose()
        {
            Console.SetOut(m_PreviousConsoleOutput);
            m_StreamWriter.Close();
            m_OutputFile.Close();
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
