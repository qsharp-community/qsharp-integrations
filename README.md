# Q# Community Integration #

These samples demonstrate how to integrate with the Q# language infrastructure and/or drivers. 
There are multiple types of intgration with the existing Q# landscape.
Currently present are the following methods;

## 0. Qasm (Quantum Assembler Language) ##

- **[OpenQasmReader](./src/OpenQasmReader)**:
  This sample shows that one can convert OpenQasm 2.0 specifications to Q# methods. This allows one to import algorithms written in OpenQasm 2.0 to be used on the Microsoft Q# Simulator. Apart of the barrier gate (which has no meaning in Q#) all gates are converted to Q# constructions.
