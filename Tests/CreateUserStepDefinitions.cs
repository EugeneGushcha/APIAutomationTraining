using API.Automation;
using API.Automation.Models.Request;
using API.Automation.Models.Response;
using API.Automation.Utility;
using Gherkin.Ast;
using Newtonsoft.Json.Linq;
using NUnit.Framework.Legacy;
using RestSharp;
using System;
using System.Net;
using TechTalk.SpecFlow;
using NUnit.Framework;
using System.Text.Json;
using System.Xml.Linq;
using System.Reflection.Emit;


namespace Tests
{
    [Binding]
    public class CreateUserStepDefinitions
    {
        private CreateUserReq createUserReq;
        private RestResponse response;
        private ScenarioContext scenarioContext;
        private HttpStatusCode statusCode;
        private APIClientRead apiRead;
        private APIClientWrite apiWrite;

        public CreateUserStepDefinitions(CreateUserReq createUserReq, ScenarioContext scenarioContext)
        {
            this.createUserReq = createUserReq;
            this.scenarioContext = scenarioContext;
            apiRead = new APIClientRead();
            apiWrite = new APIClientWrite();
        }

        [Given(@"CreateUser payload ""([^""]*)"" created")]
        public void GivenCreateUserPayloadCreated(string filename)
        {
            string file = HandleContent.GetFilePath(filename);
            var payload = HandleContent.ParseJson<CreateUserReq>(file);
            if (scenarioContext.ContainsKey("createUser_payload"))
            {
                // Update the existing payload
                scenarioContext["createUser_payload"] = payload;
            }
            else
            {
                // Add the new payload
                scenarioContext.Add("createUser_payload", payload);
            }
        }



        [Given(@"I get all users")]
        public void GivenIGetAllAvailableUsers()
        {
            response = apiRead.GetListofUsers();
            //var content = response.Content;
            //string name = "Eugene";
            //List<GetAllUsersRes> people = HandleContent.GetContent<List<GetAllUsersRes>>(response);

            //foreach (GetAllUsersRes man in people)
            //{
            //    if (man.name == name)
            //        Console.WriteLine("Hello world!");

            //}


            Console.WriteLine("Available Users: ");
            string? content = response.Content;
            Console.WriteLine(content);
        }

        
        [When(@"Send request to create user")]
        public void WhenSendRequestToCreateUser()
        {
            createUserReq = scenarioContext.Get<CreateUserReq>("createUser_payload");
            response = apiWrite.CreateUser(createUserReq);
            if (scenarioContext.ContainsKey("createUser_response"))
            {
                // Update the existing payload
                scenarioContext["createUser_response"] = response;
            }
            else
            {
                // Add the new payload
                scenarioContext.Add("createUser_response", response);
            }
        }

        [When(@"Send request to create user ""([^""]*)"" times")]
        public void WhenSendRequestToCreateUserTimes(int times)
        {
            createUserReq = scenarioContext.Get<CreateUserReq>("createUser_payload");
            for (int i = 0; i < times; i++) 
                {
                    response = apiWrite.CreateUser(createUserReq);
                }
            scenarioContext.Add("createUser_response", response);
        }



        [Then(@"I get ""([^""]*)"" response code after CreateUser payload")]
        public void ThenIGetResponseCodeAfterCreateUserPayload(string responseCode)
        {
            var response = scenarioContext.Get<RestResponse>("createUser_response");

            Assert.That((int)response.StatusCode, Is.EqualTo(Int32.Parse(responseCode)));
        }


        [Then(@"Validate user is created")]
        public void ThenValidateUserIsCreated()
        {
            var response = scenarioContext.Get<RestResponse>("createUser_response");
            var code = (int)response.StatusCode;
            Assert.That(201, Is.EqualTo(code), "User is created");
            Assert.That(400, Is.Not.EqualTo(code), "Users must be unique for complex key name+sex");
            Assert.That(409, Is.Not.EqualTo(code), "Some required fields are missed");
            Assert.That(424, Is.Not.EqualTo(code), "Specified zip code is not available");

            var content = HandleContent.GetContent<CreateUserRes>(response);
            //Assert.That(content.name, Is.EqualTo(createUserReq.name));
            //Assert.That(content.age, Is.EqualTo(createUserReq.age));
            //Assert.That(content.sex, Is.EqualTo(createUserReq.sex));
        }

        [Then(@"Validate user with name ""([^""]*)"" age ""([^""]*)"" sex ""([^""]*)"" and zip code ""([^""]*)"" is created")]
        public void ThenValidateUserWithNameAgeSexAndZipCodeIsCreated(string name, int age, string sex, string zipCode)
        {
            var response = apiRead.GetListofUsers();
            bool isCreated = HandleContent.IsUserCreated(response, name, age, sex, zipCode);

            Assert.That(isCreated, Is.True, $"The user with name: {name}, age: {age}, sex: {sex} and zip code: {zipCode} does not present in created users list.");
        
        }

        [Then(@"Validate user with name ""([^""]*)"" age ""([^""]*)"" sex ""([^""]*)"" and zip code ""([^""]*)"" is NOT created")]
        public void ThenValidateUserWithNameAgeSexAndZipCodeIsNOTCreated(string name, int age, string sex, string zipCode)
        {
            var response = apiRead.GetListofUsers();
            bool isCreated = HandleContent.IsUserCreated(response, name, age, sex, zipCode);

            Assert.That(isCreated, Is.False, $"The user with name: {name}, age: {age}, sex: {sex} and zip code: {zipCode} presents in created users list.");
        }

        [Then(@"Validate user with name ""([^""]*)"" age ""([^""]*)"" sex ""([^""]*)"" and zip code ""([^""]*)"" is NOT dublicated")]
        public void ThenValidateUserWithNameAgeSexAndZipCodeIsNOTDublicated(string name, int age, string sex, string zipCode)
        {
            var response = apiRead.GetListofUsers();
            bool isDuplicated = HandleContent.IsUserDublicateCreated(response, name, age, sex, zipCode);

            Assert.That(isDuplicated, Is.False, $"The user with name: {name}, age: {age}, sex: {sex} and zip code: {zipCode} is duplicated in created users list.");
        }


    }
}
