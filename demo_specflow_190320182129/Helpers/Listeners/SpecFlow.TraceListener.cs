using System;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Tracing;

// <specFlow>
//     <trace traceSuccessfulSteps="false" listener="MyCompany.SpecFlowTests.SpecFlowTestListener, MyCompany.SpecFlowTests" />
// </specFlow>

namespace DemoSpecFlow.SpecFlowTests
{
    public class SpecFlowTestListener : ITraceListener
    {
        public SpecFlowTestListener()
        {
            var disableTrace = Environment.GetEnvironmentVariable(DisableTraceVariable);

            if (String.IsNullOrWhiteSpace(disableTrace))
                _listener = new DefaultListener();
        }

        public void WriteTestOutput(string message)
        {
            if (_listener != null)
                _listener.WriteTestOutput(message);
        }

        public void WriteToolOutput(string message)
        {
            if (_listener != null)
                _listener.WriteToolOutput(message);
        }

        private readonly ITraceListener _listener;

        private const string DisableTraceVariable = "DISABLE_SPECFLOW_TRACE_OUTPUT";
    }
}
