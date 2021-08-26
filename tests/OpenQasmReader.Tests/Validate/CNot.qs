// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
namespace Microsoft.Quantum.Samples.OpenQasmReader.Tests.Validate {
    
    open Microsoft.Quantum.Intrinsic;
    open Microsoft.Quantum.Canon;
    open Microsoft.Quantum.Math;
    
    operation CNot () : Result[] {
        
        mutable c = new Result[2];
        
        use q = Qubit[2];
        H(q[0]);
        CNOT(q[0], q[1]);
            
        for _idx in 0 .. Length(c) - 1 {
            set c w/= _idx <- M(q[_idx]);
        }
            
        ResetAll(q);
        
        return [c[0], c[1]];
    }
    
}


