using API.Automation;
using API.Automation.Models.Request;
using API.Automation.Models.Response;
using API.Automation.Utility;
using Newtonsoft.Json.Linq;
using NUnit.Framework.Legacy;
using RestSharp;
using System;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
using System.Reflection.PortableExecutable;
using TechTalk.SpecFlow;


namespace Tests
{
    [Binding]
    public class GetZipCodesStepDefinitions
    {
        private ExpandZipCodeReq expandZipCodeReq;
        private RestResponse response;
        private ScenarioContext scenarioContext;
        private HttpStatusCode statusCode;
        private APIClientRead apiRead;
        private APIClientWrite apiWrite;


        public GetZipCodesStepDefinitions(ExpandZipCodeReq expandZipCodeReq, ScenarioContext scenarioContext)
        {
            
            this.expandZipCodeReq = expandZipCodeReq;
            this.scenarioContext = scenarioContext;
            apiRead = new APIClientRead();
            apiWrite = new APIClientWrite();
        }

        [Given(@"zip-code payload ""([^""]*)"" created")]
        public void GivenZip_CodePayloadCreated(string filename)
        {
            string filePath = HandleContent.GetFilePath(filename);
            var payload = File.ReadAllLines(filePath).Skip(4).ToArray();
            scenarioContext.Add("expandZipCodes_payload", payload);
        }


        [When(@"I get all zip codes with ""([^""]*)"" response code")]
        public void WhenIGetAllZipCodesWithResponseCode(string code)
        {
            response = apiRead.GetAvailableZipCodes();
            statusCode = response.StatusCode;
            var statusString = statusCode.ToString();
            var responseCode = (int)statusCode;
            var expCode = Int32.Parse(code);

            Console.WriteLine("Available zip codes: ");
            string? content = response.Content;
            Console.WriteLine(content);

            Assert.That(statusString, Is.EqualTo("OK"), "Unexpected status code.");
            Assert.That(responseCode, Is.EqualTo(expCode), "Unexpected status code.");
        }

        /// <summary>
        /// Expand available zip codes with a single new zip code
        /// </summary>
        /// <param name="zipCode"></param>
        [When(@"Send request to expand zip code with ""([^""]*)""")]
        public void WhenSendRequestToExpandZipCodeWith(string zipCode)
        {
            string[] array = [zipCode];
            response = apiWrite.ExpandZipCode(array);
            scenarioContext.Add("Response", response);
        }

        /// <summary>
        /// Expand available zip codes from a file.
        /// Multiple or single zip codes extending is possible.
        /// </summary>
        [When(@"Send request to expand zip code with payload")]
        public void WhenSendRequestToExpandZipCodeWithPayload()
        {
            var expandZipReq = scenarioContext.Get<string[]>("expandZipCodes_payload");
            response = apiWrite.ExpandZipCode(expandZipReq);
            scenarioContext.Add("Response_from_payload", response);
        }

        [Then(@"I get ""([^""]*)"" response code after payload")]
        public void ThenIGetResponseCodeAfterPayload(string responseCode)
        {
            var response = scenarioContext.Get<RestResponse>("Response_from_payload");

            Assert.That((int)response.StatusCode, Is.EqualTo(Int32.Parse(responseCode)));
        }


        /// <summary>
        /// Method validates if the expected zip code presents in available zip codes
        /// </summary>
        /// <param name="zipcode"></param>
        [Then(@"I should have zip code ""([^""]*)"" available")]
        public void ThenIShouldHaveZipCodeAvailable(string zipcode)
        {
            var response = apiRead.GetAvailableZipCodes();

            Assert.That(response.StatusCode.ToString(), Is.EqualTo("Created"), "Unexpected status code.");
            Assert.That(response.Content, Does.Contain(zipcode), $"Available zip codes do not contain {zipcode} zip code.");
        }


        /// <summary>
        /// Method validates if a list available zip codes contains duplicate values for a certain zip code.
        /// </summary>
        /// <param name="zipCode"></param>
        [Then(@"there are no zip code ""([^""]*)"" dublicates in available")]
        public void ThenThereAreNoZipCodeDublicatesInAvailable(string zipCode)
        {
            var response = scenarioContext.Get<RestResponse>("Response_from_payload");
            var array = response.Content.Split(',');
            string[] trimmedArray = array.Select(s => s.Trim('"', '[', '\\', ']')).ToArray();
            bool isDublicate = HandleContent.DuplicateInArray(trimmedArray, zipCode);

            Assert.That(isDublicate, Is.False, $"There is {zipCode} duplicated in available zip codes.");
        }


        /// <summary>
        /// Method validates if a list available zip codes contains duplicate values
        /// </summary>
        /// <exception cref="PendingStepException"></exception>
        [Then(@"there are no dublicates in available zip codes")]
        public void ThenThereAreNoDublicatesInAvailableZipCodes()
        {
            var response = scenarioContext.Get<RestResponse>("Response_from_payload");
            var array = response.Content.Split(',');
            string[] trimmedArray = array.Select(s => s.Trim('"', '[', '\\', ']')).ToArray();
            bool isDublicate = HandleContent.DuplicateInArray(trimmedArray);

            Assert.That(isDublicate, Is.False, $"There are duplicates in available zip codes.");
        }





    }
}
