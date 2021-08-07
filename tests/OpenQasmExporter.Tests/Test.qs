namespace Tests {
    open Microsoft.Quantum.Intrinsic;
    open Microsoft.Quantum.Diagnostics;
    open Microsoft.Quantum.Measurement;
    
    @EntryPoint()
    @Test("QSharpCommunity.Simulators.OpenQasmExporter.Exporter")
    operation SampleTest() : Unit {
        use q = Qubit();
        H(q);
        let result = MResetZ(q);
        Message($"Result of Hadamard: {result}");

        // TODO: See if we can actually use test methods; Example below from qRAM project
        // let expectedValue = ConstantArray(2^addressSize, [false]);
        // let data = EmptyQRAM(addressSize);
        // let result = CreateQueryMeasureAllQRAM(data);
        // let pairs = Zip(result, expectedValue);
        // Ignore(Mapped(
        // AllEqualityFactB(_, _, $"Expecting memory contents {expectedValue}, got {result}."), 
        // pairs
        //));
    }
}