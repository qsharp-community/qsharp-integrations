// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
namespace Microsoft.Quantum.Samples.OpenQasm.Measurement {
    open Microsoft.Quantum.Intrinsic;
    open Microsoft.Quantum.Canon;
    open Microsoft.Quantum.Measurement;
    
    /// # Summary
    /// Measurement example: create a state $1/\sqrt(2)(|0\rangle+|1\rangle)$ and
    /// measure it in the Pauli-Z basis.
    ///
    /// # Remarks
    /// It is asserted that upon measurement in the Pauli-Z basis a perfect coin toss
    /// of a 50-50 coin results.
    operation MeasureOneQubit() : Result {
        // The following using block creates a fresh qubit and initializes it in |0〉.
        using (qubit = Qubit()) {

            // Apply a Hadamard operation H to the state, thereby creating
            // the state 1/sqrt(2)(|0〉+|1〉).
            H(qubit);
            AssertProb([PauliZ], [qubit], Zero, 0.5, "Error: Outcomes of the measurement must be equally likely", 1E-05);

            // Now we measure the qubit in Z-basis
            let result = M(qubit);

            // As the qubit is now in an eigenstate of the measurement operator,
            // i.e., either in |0⟩ or in |1⟩, and qubits need to be in |0⟩ when they
            // are released, we have to manually reset the qubit before releasing it.
            // Note that this is how the Microsoft.Quantum.Measurement.MResetZ
            // operation works.
            if (result == One) {
                X(qubit);
            }

            // Finally, we return the result of the measurement.
            return result;
        }
    }
    
    /// # Summary
    /// Measurement example: create a state $1/\sqrt(2)(|00\rangle+|11\rangle)$ and measure
    /// it in the Pauli-Z basis.
    ///
    /// # Remarks
    /// It is asserted that upon measurement in the Pauli-Z basis a perfect coin toss of a
    /// 50-50 coin results with outcomes "00" and "11".
    operation MeasureInBellBasis() : (Result, Result) {
        // The following using block creates a fresh qubit and initializes it in |0〉.
        using ((left, right) = (Qubit(), Qubit())) {
            // By applying the Hadamard operator and a CNOT, we create the cat state
            // 1/sqrt(2)(|00〉+|11〉).
            H(left);
            CNOT(left, right);

            // The following two assertions ascertain that the created state is indeed
            // invariant under both, the XX and the ZZ operations, i.e., it projects
            // into the +1 eigenstate of these two Pauli operators.
            Assert([PauliZ, PauliZ], [left, right], Zero, "Error: EPR state must be eigenstate of ZZ");
            Assert([PauliX, PauliX], [left, right], Zero, "Error: EPR state must be eigenstate of XX");
            AssertProb([PauliZ, PauliZ], [left, right], One, 0.0, "Error: 01 or 10 should never occur as an outcome", 1E-05);

            // Finally, we measure each qubit in Z-basis and construct a tuple from the results.
            return (MResetZ(left), MResetZ(right));
        }
    }

}