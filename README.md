# Q# Community Integration #

These samples demonstrate integration between the Q# language infrastructure/drivers and other languages. 
Currently present are the following integrations:

![Build](https://github.com/qsharp-community/qsharp-integrations/workflows/Build/badge.svg)

## 0. Qasm (Quantum Assembler Language) ##

- **[OpenQasmReader](./src/OpenQasmReader)**:
  This sample shows that one can convert OpenQasm 2.0 specifications to Q# methods. This allows one to import algorithms written in OpenQasm 2.0 to be used on the Microsoft Q# Simulator. Apart of the barrier gate (which has no meaning in Q#) all gates are converted to Q# constructions.
- **[OpenQasmExporter](./src/OpenQasmExporter)**:
  This sample shows that one can convert Q# methods to OpenQasm 2.0 specifications. This allows one to export algorithms written in Q# to OpenQasm 2.0.
