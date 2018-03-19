using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using Newtonsoft.Json;

namespace demo_speckflow_19032018
{
    /// <summary>
    /// Enum defines the HTTP Verbs
    /// </summary>
    public enum HttpVerb
    {
        GET,
        POST,
        PUT,
        DELETE
    }

    public class HttpRestClientUtility
    {
        public string EndPoint { get; set; }
        public HttpVerb Method { get; set; }
        public string ContentType { get; set; }
        public string PostData { get; set; }

        public HttpRestClientUtility()
        {
            EndPoint = "";
            Method = HttpVerb.GET;
            ContentType = "application/json";
            PostData = "";
        }
        public HttpRestClientUtility(string endpoint)
        {
            EndPoint = endpoint;
            Method = HttpVerb.GET;
            ContentType = "application/json";
            PostData = "";
        }
        public HttpRestClientUtility(string endpoint, HttpVerb method)
        {
            EndPoint = endpoint;
            Method = method;
            ContentType = "application/json";
            PostData = "";
        }

        public HttpRestClientUtility(string endpoint, HttpVerb method, string postData)
        {
            EndPoint = endpoint;
            Method = method;
            ContentType = "application/json";
            PostData = postData;
        }


        /// <summary>
        /// Make empty request
        /// </summary>
        /// <returns></returns>
        public string MakeRequest()
        {
            return MakeRequest("");
        }

        /// <summary>
        /// Make a request with params
        /// </summary>
        /// <param name="parameters">params</param>
        /// <returns>result</returns>
        public string MakeRequest(string parameters)
        {
            var request = (HttpWebRequest)WebRequest.Create(EndPoint + parameters);

            request.Method = Method.ToString();
            request.ContentLength = 0;
            request.ContentType = ContentType;

            if (!string.IsNullOrEmpty(PostData) && Method == HttpVerb.POST)
            {
                var encoding = new UTF8Encoding();
                var bytes = Encoding.GetEncoding("iso-8859-1").GetBytes(PostData);
                request.ContentLength = bytes.Length;

                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(bytes, 0, bytes.Length);
                }
            }

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                var responseValue = string.Empty;

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    var message = String.Format("Request failed. Received HTTP {0}", response.StatusCode);
                    throw new ApplicationException(message);
                }

                // grab the response
                using (var responseStream = response.GetResponseStream())
                {
                    if (responseStream != null)
                        using (var reader = new StreamReader(responseStream))
                        {
                            responseValue = reader.ReadToEnd();
                        }
                }
                return responseValue;
            }
        }

    } // class

    /// <summary>
    /// This class used for scenario utility
    /// </summary>
    public static class ScenarioUtils
    {
        static string endpoint = @"http://restcountries.eu/rest/v2/";
        static string endpointURL = "";
        static Country _countryObj = null;
        static List<Country> _lstCountries = null;

        /// <summary>
        /// This method get the country details 
        /// </summary>
        /// <param name="capital">name of the capital</param>
        /// <param name="code">code of the capital</param>
        /// <returns>Country</returns>
        public static Country GetCountryDetails(string capital)
        {
            endpointURL = endpoint + "capital/" + capital;
            var client = new HttpRestClientUtility(endpointURL);
            var json = client.MakeRequest();
            _countryObj = ReadToObject(json)[0];
            return _countryObj;
        }

        /// <summary>
        /// This method get the country details
        /// </summary>
        /// <param name="name">name of the country</param>
        /// <returns>Country</returns>
        public static Country GetCountryByName(string name)
        {
            endpointURL = endpoint + "name/" + name;
            var client = new HttpRestClientUtility(endpointURL);
            var json = client.MakeRequest();
            _countryObj = ReadToObject(json)[0];
            return _countryObj;
        }

        /// <summary>
        /// Get the countries within the specified region
        /// </summary>
        /// <param name="region"></param>
        /// <returns>List<Country></returns>
        public static List<Country> GetCountriesInTheRegion(string region)
        {
            endpointURL = endpoint + "region/" + region;
            var client = new HttpRestClientUtility(endpointURL);
            var json = client.MakeRequest();
            _lstCountries = ReadToObject(json);
            return _lstCountries;
        }

        /// <summary>
        /// Deserialize a JSON stream to a User object.  
        /// </summary>
        /// <param name="json">validate json object to deserialize</param>
        /// <returns>Basket</returns>
        public static List<Country> ReadToObject(string json)
        {
            return JsonConvert.DeserializeObject<List<Country>>(json);
        }

    }
}
