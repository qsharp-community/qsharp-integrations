using System;
using System.Linq;
using Microsoft.Quantum.Simulation.Core;
using Intrinsic = Microsoft.Quantum.Intrinsic;
using Decomp = QSharpCommunity.Simulators.OpenQasmExporter.Decompositions;

namespace QSharpCommunity.Simulators.OpenQasmExporter
{
    public partial class Exporter 
    {
        public class Measure : Intrinsic.Measure
        {
            public Measure(IOperationFactory m)
                : base(m) { }

            public override Func<(IQArray<Pauli>, IQArray<Qubit>), Result> __Body__ => 
                args => this.__Factory__.Get<Decomp.Measure, Decomp.Measure>().__Body__(args);
        }

        // It's necessary to override this operation, so it doesn't end up in the OpenQASM output
        public class MessageSim : Intrinsic.Message
        {
            public MessageSim(IOperationFactory m)
                : base(m) { }

            public override Func<string, QVoid> __Body__ =>
                msg =>
                {
                    // Ignore
                    return QVoid.Instance;
                };
        }

        // It's necessary to override this operation, so it doesn't end up in the OpenQASM output
        public class Reset : Intrinsic.Reset
        {
            public Reset(IOperationFactory m)
                : base(m) { }

            public override Func<Qubit, QVoid> __Body__ => qubit =>
                {
                    return QVoid.Instance;
                };
        }

        // It's necessary to override this operation, so it doesn't end up in the OpenQASM output
        public class ResetAll : Intrinsic.ResetAll
        {
            public ResetAll(IOperationFactory m)
                : base(m) { }

            public override Func<IQArray<Qubit>, QVoid> __Body__ =>
                args =>
                {
                    return QVoid.Instance;
                };
        }

        /* public class SingleQubitOp<T> : Intrinsic.I, ICallable
        {
            string ICallable.Name => typeof(T).Name;

            public override Func<Qubit, QVoid> __Body__ =>
                qubit =>
                {
                    var opName = ((ICallable)this).Name.ToLower();
                    Console.WriteLine($"{opName} q[{qubit.Id}];");
                    return QVoid.Instance;
                };

            public override Func<(IQArray<Qubit>, Qubit), QVoid> __ControlledBody__ =>
                args =>
                {
                    var (controls, qubit) = args;
                    var controlCount = controls.Count;
                    var controlPrefix = string.Concat(Enumerable.Repeat("c", controlCount));
                    var opName = ((ICallable)this).Name.ToLower();
                    Console.WriteLine($"{controlPrefix}{opName} {string.Join(",", controls.Select(c => $"q[{c.Id}]"))},q[{qubit.Id}];");
                    return QVoid.Instance;
                };

            public override Func<Qubit, QVoid> __AdjointBody__ =>
                qubit =>
                {
                    var opName = ((ICallable)this).Name;
                    switch (opName)
                    {
                        case nameof(Intrinsic.T):
                        case nameof(Intrinsic.S):
                            opName = opName.ToLower() +"dg";
                            break;

                        default:
                            opName = opName.ToLower();
                            break;
                    }

                    Console.WriteLine($"{opName} q[{qubit.Id}];");
                    return QVoid.Instance;
                };

            public SingleQubitOp(IOperationFactory m) 
                : base(m) {}
        } */
    }
}