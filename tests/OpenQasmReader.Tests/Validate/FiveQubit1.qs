// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
namespace Microsoft.Quantum.Samples.OpenQasmReader.Tests.Validate {
    
    open Microsoft.Quantum.Intrinsic;
    open Microsoft.Quantum.Canon;
    open Microsoft.Quantum.Math;
    
    operation FiveQubit1 () : Result[] {
        
        mutable _out = new Result[2];
        mutable c = new Result[3];
        
        use q = Qubit[5];
        X(q[4]);
        H(q[3]);
        CNOT(q[4], q[2]);
        CNOT(q[1], q[2]);
        H(q[4]);
        H(q[2]);
        H(q[1]);
        CNOT(q[1], q[2]);
        H(q[2]);
        H(q[1]);
        CNOT(q[1], q[2]);
        CNOT(q[4], q[2]);
        CNOT(q[4], q[2]);
        H(q[2]);
        H(q[4]);
        CNOT(q[4], q[2]);
        H(q[2]);
        H(q[4]);
        CNOT(q[4], q[2]);
        CNOT(q[3], q[2]);
        CNOT(q[1], q[2]);
        H(q[2]);
        H(q[1]);
        CNOT(q[1], q[2]);
        H(q[2]);
        H(q[1]);
        CNOT(q[1], q[2]);
        CNOT(q[1], q[2]);
        CNOT(q[0], q[2]);
        H(q[1]);
        H(q[2]);
        H(q[0]);
        CNOT(q[0], q[2]);
        H(q[2]);
        H(q[0]);
        CNOT(q[0], q[2]);
        CNOT(q[3], q[2]);
        set _out w/= 0 <- M(q[0]);
        CNOT(q[4], q[2]);
        H(q[3]);
        H(q[2]);
        H(q[4]);
        CNOT(q[4], q[2]);
        H(q[2]);
        H(q[4]);
        CNOT(q[4], q[2]);
        CNOT(q[3], q[2]);
        CNOT(q[4], q[2]);
        set _out w/= 1 <- M(q[3]);
        H(q[2]);
        H(q[4]);
        CNOT(q[4], q[2]);
        H(q[2]);
        H(q[4]);
        CNOT(q[4], q[2]);
        CNOT(q[1], q[2]);
        set c w/= 0 <- M(q[4]);
        set c w/= 1 <- M(q[1]);
        set c w/= 2 <- M(q[2]);
        ResetAll(q);
        
        return [_out[0], _out[1], c[0], c[1], c[2]];
    }
    
}


