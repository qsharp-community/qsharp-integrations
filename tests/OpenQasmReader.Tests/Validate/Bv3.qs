// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
namespace Microsoft.Quantum.Samples.OpenQasmReader.Tests.Validate {
    open Microsoft.Quantum.Intrinsic;
    open Microsoft.Quantum.Canon;
    open Microsoft.Quantum.Math;
    
    operation Bv3 () : Result[] {
        
        mutable _out = new Result[4];
        mutable c = new Result[4];
        
        use q = Qubit[5];
        X(q[2]);
        H(q[0]);
        H(q[1]);
        H(q[2]);
        H(q[3]);
        H(q[4]);
        CNOT(q[0], q[2]);
        CNOT(q[1], q[2]);
        H(q[0]);
        H(q[1]);
        H(q[3]);
        H(q[4]);
        set _out w/= 0 <- M(q[0]);
        set _out w/= 1 <- M(q[1]);
        set _out w/= 2 <- M(q[3]);
        set _out w/= 3 <- M(q[4]);
        ResetAll(q);

        return [_out[0], _out[1], _out[2], _out[3]];
    }
    
}


