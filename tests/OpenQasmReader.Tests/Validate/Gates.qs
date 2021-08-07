// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
namespace Microsoft.Quantum.Samples.OpenQasmReader.Tests.Validate {
    
    open Microsoft.Quantum.Intrinsic;
    open Microsoft.Quantum.Canon;
    open Microsoft.Quantum.Math;
    
    
    operation Majority (a : Qubit, b : Qubit, c : Qubit) : Unit {
        CNOT(c, b);
        CNOT(c, a);
        CCNOT(a, b, c);
    }
    
    operation Gates () : Unit {
        use q = Qubit[3];
        Majority(q[0], q[1], q[2]);
        ResetAll(q);
    }
    
}


