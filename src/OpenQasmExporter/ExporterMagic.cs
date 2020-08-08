// Adapted from CHP magic command in the CHP-Sim project here:
// https://github.com/qsharp-community/chp-sim/blob/master/src/jupyter/ChpMagic.cs

namespace QSharpCommunity.Simulators.OpenQasmExporter
{
    using System;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Jupyter.Core;
    using Microsoft.Quantum.IQSharp;
    using Microsoft.Quantum.IQSharp.Jupyter;

    /// <summary>
    /// Runs a given function or operation on the OpenQasmExporter target machine.
    /// </summary>
    public class ExporterMagic : AbstractMagic
    {
        class ChannelTextWriter : TextWriter
        {
            public override Encoding Encoding => Console.OutputEncoding;

            IChannel m_Channel;

            public ChannelTextWriter(IChannel channel)
            {
                m_Channel = channel;
            }

            public override void Write(string message) => m_Channel.Stdout(message);

            public override void WriteLine(string message) => m_Channel.Stdout(message);
        }

        const string ParameterNameOperationName = "__operationName__";
        readonly IConfigurationSource configurationSource;

        /// <summary>
        /// Initializes a new instance of the <see cref="Exporter"/> class.
        /// Default constructor.
        /// </summary>
        /// <param name="resolver">Symbol resolver.</param>
        /// <param name="configurationSource">Source for confirgarion settings.</param>
        public ExporterMagic(ISymbolResolver resolver, IConfigurationSource configurationSource)
            : base(
            "qasmexport",
            new Documentation
            {
                Summary = "Runs a given function or operation using the OpenQasmExporter.",
                Description = @"
                    This magic command allows executing a given function or operation on the OpenQasmExporter, 
                    which performs a simulation of the given function or operation and translates it to the OpenQasm language
                    
                    #### Required parameters

                    - Q# operation or function name. This must be the first parameter, and must be a valid Q# operation
                    or function name that has been defined either in the notebook or in a Q# file in the same folder.
                    - Arguments for the Q# operation or function must also be specified as `key=value` pairs.
                ",
                Examples = new[]
                {
                    @"
                        Use the OpenQasmExporter to simulate a Q# operation
                        defined as `operation MyOperation() : Result`:
                        ```
                        In []: %qasmexport MyOperation
                        Out[]: <OpenQasm translation of the operation>
                        ```
                    ",
                    @"
                        Use the OpenQasmExporter to simulate a Q# operation
                        defined as `operation MyOperation(a : Int, b : Int) : Result`:
                        ```
                        In []: %qasmexporter MyOperation a=5 b=10
                        Out[]: <OpenQasm value of the operation>
                        ```
                    ",
                },
            })
        {
            this.SymbolResolver = resolver;
            this.configurationSource = configurationSource;
        }

        /// <summary>
        /// Gets the ISymbolResolver used to find the function/operation to simulate.
        /// </summary>
        public ISymbolResolver SymbolResolver { get; }

        /// <inheritdoc />
        public override ExecutionResult Run(string input, IChannel channel) =>
            this.RunAsync(input, channel).Result;

        /// <summary>
        /// Simulates a function/operation using the OpenQasmExporter as target machine.
        /// It expects a single input: the name of the function/operation to simulate.
        /// </summary>
        /// <param name="input">current parameters for the function/operation.</param>
        /// <param name="channel">channel connecting up with jupiter.</param>
        /// <returns>function result.</returns>
        public async Task<ExecutionResult> RunAsync(string input, IChannel channel)
        {
            var inputParameters = ParseInputParameters(input, firstParameterInferredName: ParameterNameOperationName);

            var name = inputParameters.DecodeParameter<string>(ParameterNameOperationName);
            var symbol = this.SymbolResolver.Resolve(name) as dynamic; // FIXME: should be as IQSharpSymbol.
            if (symbol == null)
            {
                throw new InvalidOperationException($"Invalid operation name: {name}");
            }

            // TODO: File bug for the following to be public:
            // https://github.com/microsoft/iqsharp/blob/9fa7d4da4ec0401bf5803e40fce5b37e716c3574/src/Jupyter/ConfigurationSource.cs#L35
            var outputFileName =
                this.configurationSource.Configuration.TryGetValue("qasmexport.outputFileName", out var token)
                ? token.ToObject<string>()
                : $"{name}.qasm";

            channel.Display($"// Exporting to {outputFileName} (use %config qasmexport.outputFileName to change)");

            using (var qsim = new Exporter(outputFileName, new ChannelTextWriter(channel)))
            {
                var operationInfo = (OperationInfo)symbol.Operation;
                var value = await operationInfo.RunAsync(qsim, inputParameters);

                return value.ToExecutionResult();
            }
        }
    }
}