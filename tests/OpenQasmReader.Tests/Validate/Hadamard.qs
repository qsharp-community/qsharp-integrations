// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
namespace Microsoft.Quantum.Samples.OpenQasmReader.Tests.Validate {
    
    open Microsoft.Quantum.Intrinsic;
    open Microsoft.Quantum.Canon;
    open Microsoft.Quantum.Math;
    
    operation Hadamard () : Result[] {
        
        mutable c = new Result[1];
        
        use q = Qubit[1];
        H(q[0]);
        set c w/= 0 <- M(q[0]);
        ResetAll(q);

        return [c[0]];
    }
    
}


