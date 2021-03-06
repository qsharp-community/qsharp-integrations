# OpenQasm Exporter Sample
This sample shows you how you can convert  Q# methods to the OpenQasm 2.0 specifications. 
The exporter is designed as a custom Simulator similar to the QCTraceSimulator. To make use of it in your project you can either call it [directly from your host application](../../tests/OpenQasmExporter.Sample/Program.cs):

```csharp
using (var exporter = new namespace QSharpCommunity.Simulators.OpenQasmExporter.Exporter("Test.qasm"))
{
    await Tests.SampleTest.Run(exporter);
}
```

or you can use it as a drop-in simulator replacement by adding a reference to the project (assuming the qsharp-integrations repo is side-by-side):

```
dotnet add reference ../qsharp-integrations/src/OpenQasmExporter/OpenQasmExporter.csproj
```

 and adding the following `DefaultSimulator` line to your `.csproj` file:

```xml
<PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>netcoreapp3.1</TargetFramework>
    <DefaultSimulator>QSharpCommunity.Simulators.OpenQasmExporter.Exporter</DefaultSimulator>
</PropertyGroup>
```

## Using with Jupyter Notebooks

There is a `%qasmexport` command that you can use in the IQ# kernel. Take a look at [SampleExporterMagic.ipynb](../../tests/OpenQasmExporter.Sample/SampleExporterMagic.ipynb) for an example.