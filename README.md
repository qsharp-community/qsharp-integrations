# Q# Community Integration #

These samples demonstrate how to integrate with the Q# language infrastructure and/or drivers. 
Most samples are provided as a Visual Studio 2017 C# or F# project under the [`QsharpIntegrations.sln`](./QsharpIntegrations.sln) solution.

There are multiple types of intgration with the existing Q# landscape.
Currently present are the following methods;

## 0. Qasm (Quantum Assembler Language) ##

- **[OpenQasm](./Samples/src/OpenQasm)**:
  This sample shows that one can output a subset of the quantum operations of a Q# application in OpenQASM.
- **[Qiskit](./Samples/src/Qiskit)**:
  This sample shows that one can run the quantum operations of a Q# application by using the OpenQASM output on the IBMQuantumExperience by changing the driver.
- **[OpenQasmReader](./Samples/src/OpenQasmReader)**:
  This sample shows that one can convert OpenQasm 2.0 specifications to Q# methods. This allows one to import algorithms written in OpenQasm 2.0 to be used on the Microsoft Q# Simulator. Apart of the barrier gate (which has no meaning in Q#) all gates are converted to Q# constructions.
