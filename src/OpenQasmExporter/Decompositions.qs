namespace QSharpCommunity.Simulators.OpenQasmExporter.Decompositions {

    open Microsoft.Quantum.Intrinsic;
    open Microsoft.Quantum.Canon;
    open Microsoft.Quantum.Measurement;
    open QSharpCommunity.Simulators.OpenQasmExporter.OpenQasmIntrinsics as OpenQasm;

    // This file takes the original simulator calls and turns them into
    // calls to operations in the OpenQasmIntrinsics namespace, which defines
    // all of the operations supported in OpenQASM 2.

    operation Measure(bases : Pauli[], target : Qubit[]) : Result {
        // Take a measurement with lots of bases/joint measurements and decompose
        // it into the only measure in the OpenQasm namespace.

        //TODO: Generalize to IZII etc. and handle single I
        if Length(bases)==1 {
            within {
                if bases[0] == PauliZ {
                }
                elif bases[0] == PauliY {
                    HY(target[0]);
                }
                elif bases[0] == PauliX {
                    H(target[0]);                    
                }
            }
            apply {
                OpenQasm.Measure(target[0]);
            }
            return Zero;
        }
        else {
            return MeasureWithScratch(bases, target);
        }
    }

    operation X(target : Qubit) : Unit is Adj + Ctl {
        OpenQasm.X(target);
    }


    operation Y(target : Qubit) : Unit is Adj + Ctl {
        OpenQasm.Y(target);
    }


    operation Z(target : Qubit) : Unit is Adj + Ctl {
        OpenQasm.Z(target);
    }

}
