using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace DemoSpecFlow.SpecFlowTests
{
    using LogBuffer = List<string>;

    [Binding]
    internal static class ScenarioLog
    {
        public static void Write(string format, params object[] args)
        {
            if (ScenarioContext.Current != null)
            {
                var message = String.Format(format, args);

                if (!ScenarioContext.Current.ContainsKey(ScenarioLogKey))
                    ScenarioContext.Current[ScenarioLogKey] = new LogBuffer();

                var log = (LogBuffer)ScenarioContext.Current[ScenarioLogKey];

                log.Add(message);
            }
        }

        [AfterScenario]
        public static void HandleScenarioFailure()
        {
            if (ScenarioContext.Current.TestError != null)
                DumpLog();
        }

        private static void DumpLog()
        {
            if (ScenarioContext.Current.ContainsKey(ScenarioLogKey))
            {
                Console.WriteLine("\nScenario: {0}", ScenarioContext.Current.ScenarioInfo.Title);

                var log = (LogBuffer)ScenarioContext.Current[ScenarioLogKey];

                foreach (var message in log)
                    Console.WriteLine(message);
            }
        }

        private const string ScenarioLogKey = "ScenarioLog";
    }
}
