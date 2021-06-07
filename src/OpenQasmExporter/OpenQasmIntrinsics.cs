using System;
using System.Linq;
using Microsoft.Quantum.Simulation.Core;
using OpenQasm = QSharpCommunity.Simulators.OpenQasmExporter.OpenQasmIntrinsics;

// This file has the OpenQASM intrinsic defintions for the operations found
// in OpenQasmIntrinsics.qs.

namespace QSharpCommunity.Simulators.OpenQasmExporter
{
    public partial class Exporter 
    {
        public class OpenQasmMeasure : OpenQasm.Measure
        {

            public OpenQasmMeasure(IOperationFactory m)
                : base(m) { }

            public override Func<Qubit, QVoid> __Body__ => 
                qubit => 
                { 
                    //TODO: generalize to qubit reuse?
                    (this.__Factory__ as Exporter)?.WriteOpenQasmLine($"measure q[{qubit.Id}] -> c[{qubit.Id}];");
                    return QVoid.Instance;
                };
        }

        public class OpenQasmX : OpenQasm.X
        {

            public OpenQasmX(IOperationFactory m)
                : base(m) { }

            public override Func<Qubit, QVoid> __Body__ => 
                qubit => 
                { 
                    (this.__Factory__ as Exporter)?.WriteOpenQasmLine($"x q[{qubit.Id}];");
                    return QVoid.Instance;
                };
        }

        public class OpenQasmY : OpenQasm.Y
        {

            public OpenQasmY(IOperationFactory m)
                : base(m) { }

            public override Func<Qubit, QVoid> __Body__ => 
                qubit => 
                { 
                    (this.__Factory__ as Exporter)?.WriteOpenQasmLine($"y q[{qubit.Id}];");
                    return QVoid.Instance;
                };
        }
    }
}