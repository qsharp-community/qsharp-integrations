# Q# Community Integration #

These samples demonstrate the use of the Quantum Development Kit for a variety of different quantum computing tasks.
Most samples are provided as a Visual Studio 2017 C# or F# project under the [`QsharpSamples.sln`](./Samples/QsharpSamples.sln) solution.

Each sample is self-contained in a folder. Most of the samples consist of a Q# source file with detailed comments explaining the sample and a short classical program (either `Program.cs` (C#), `Program.fs` (F#), or `host.py` (Python)) that calls into the Q# operations and functions. 
There are some samples that are written as an interactive Jupyter notebook thus require no classical host.

A small number of the samples have additional installation requirements beyond those for the rest of the Quantum Development Kit.
These are noted in the README.md files for each sample, along with complete installation instructions.

You can find instructions on how to install the Quantum Development Kit in [our online documentation](https://docs.microsoft.com/en-us/quantum/install-guide/), which also includes
an introduction to [quantum programming concepts](https://docs.microsoft.com/en-us/quantum/concepts/). A [Docker](https://docs.docker.com/install/) image definition is also provided for your convenience, see below
for instructions on how to build and use it.

There are multiple types of intgration with the existing Q# landscape.
Current;ly present are the following methods;

## 0. Qasm (Quantum Assembler Language) ##

- **[OpenQasm](./Samples/src/OpenQasm)**:
  This sample shows that one can output a subset of the quantum operations of a Q# application in OpenQASM.
- **[Qiskit](./Samples/src/Qiskit)**:
  This sample shows that one can run the quantum operations of a Q# application by using the OpenQASM output on the IBMQuantumExperience by changing the driver.
- **[OpenQasmReader](./Samples/src/OpenQasmReader)**:
  This sample shows that one can convert OpenQasm 2.0 specifications to Q# methods. This allows one to import algorithms written in OpenQasm 2.0 to be used on the Microsoft Q# Simulator. Apart of the barrier gate (which has no meaning in Q#) all gates are converted to Q# constructions.
