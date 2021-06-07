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

        public class X : Intrinsic.X
        {
            public X(IOperationFactory m)
                : base(m) { }

            public override Func<Qubit, QVoid> __Body__ => 
                args => this.__Factory__.Get<Decomp.X, Decomp.X>().__Body__(args);
        }

        public class Y : Intrinsic.Y
        {
            public Y(IOperationFactory m)
                : base(m) { }

            public override Func<Qubit, QVoid> __Body__ => 
                args => this.__Factory__.Get<Decomp.Y, Decomp.Y>().__Body__(args);
        }

        public class Z : Intrinsic.Z
        {
            public Z(IOperationFactory m)
                : base(m) { }

            public override Func<Qubit, QVoid> __Body__ => 
                args => this.__Factory__.Get<Decomp.Z, Decomp.Z>().__Body__(args);
        }

        public class H : Intrinsic.H
        {
            public H(IOperationFactory m)
                : base(m) { }

            public override Func<Qubit, QVoid> __Body__ => 
                args => this.__Factory__.Get<Decomp.H, Decomp.H>().__Body__(args);
        }

        public class S : Intrinsic.S
        {
            public S(IOperationFactory m)
                : base(m) { }

            public override Func<Qubit, QVoid> __Body__ => 
                args => this.__Factory__.Get<Decomp.S, Decomp.S>().__Body__(args);
        }

        public class T : Intrinsic.T
        {
            public T(IOperationFactory m)
                : base(m) { }

            public override Func<Qubit, QVoid> __Body__ => 
                args => this.__Factory__.Get<Decomp.T, Decomp.T>().__Body__(args);
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