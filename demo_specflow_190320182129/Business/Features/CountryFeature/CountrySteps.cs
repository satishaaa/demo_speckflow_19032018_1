using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TechTalk.SpecFlow;

namespace DemoSpecFlow
{
    /// <summary>
    /// Country API Steps for the feature
    /// </summary>
    [Binding]
    public class CountrySteps
    {
        private bool result = false;
        private int borders = 0;
        private Country _countryObj = new Country();
        private SpecFlowTests.SpecFlowTestListener _testListener = new SpecFlowTests.SpecFlowTestListener();

        [Given(@"Calling a API and I entered Capital : (.*) and Code : (.*)")]
        public void GetCountryDetailsWithCapitalAndCode(string capital, int code)
        {
            _testListener.WriteTestOutput(" Getting Details from - api:{/rest/v2/capital/{capital} ");
            _countryObj = ScenarioUtils.GetCountryDetails(capital);
            if (_countryObj.callingCodes.Contains(code.ToString()))
            {
                result = true;
            }
            _testListener.WriteTestOutput($"result : {result}");
        }

        [Given(@"Calling a API and I entered region : (.*)")]
        public void GetCountriesInRegion(string region)
        {
            _testListener.WriteTestOutput(" Getting Details from - api:{/rest/v2/region/{region} ");
            var _listCountries = ScenarioUtils.GetCountriesInTheRegion(region);

            if (_listCountries != null)
            {
                foreach (var item in _listCountries)
                {
                    borders = Convert.ToInt16(item.borders.Count);
                    if (borders > 3)
                    {
                        result = true;
                        break;
                    }
                }
            }
            _testListener.WriteTestOutput($"result : {result}");
        }

        [Given(@"Calling a API and I entered name (.*)")]
        public void GetCountryByName(string name)
        {
            _testListener.WriteTestOutput(" Getting Details from - api:{/rest/v2/name/{name} ");
            _countryObj = ScenarioUtils.GetCountryByName(name);
            if (_countryObj != null && _countryObj.name != null)
            {
                result = false;
            }
            _testListener.WriteTestOutput($"result : {result}");
        }

        [Then(@"the case should success")]
        public void ThenTheCaseShouldSuccess()
        {
            Assert.AreEqual(true, result);
        }

        [Then(@"the case should failure")]
        public void ThenTheCaseShouldFailure()
        {
            Assert.AreEqual(false, result);
        }

        [Then(@"the system should return bordering countries more than (.*)")]
        public void GetBorderingCountryMoreThanGivenInput(int countryCount)
        {
            Assert.AreEqual(true, borders > countryCount);
        }
    }
}
