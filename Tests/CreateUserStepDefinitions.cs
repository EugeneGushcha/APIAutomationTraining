using API.Automation;
using API.Automation.Models.Request;
using API.Automation.Models.Response;
using API.Automation.Utility;
using Newtonsoft.Json.Linq;
using NUnit.Framework.Legacy;
using RestSharp;
using System;
using System.Net;
using TechTalk.SpecFlow;


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

        [Given(@"User payload ""([^""]*)""")]
        public void GivenUserPayload(string filename)
        {
            string file = HandleContent.GetFilePath(filename);
            var payload = HandleContent.ParseJson<CreateUserReq>(file);
            scenarioContext.Add("createUser_payload", payload);
        }

        
        [Given(@"I get all users")]
        public void GivenIGetAllAvailableUsers()
        {
            var response = apiRead.GetListofUsers();
        }

        
        [When(@"Send request to create user")]
        public void WhenSendRequestToCreateUser()
        {
            createUserReq = scenarioContext.Get<CreateUserReq>("createUser_payload");
            response = apiWrite.CreateUser(createUserReq);
        }

        [Then(@"Validate user is created")]
        public void ThenValidateUserIsCreated()
        {
            statusCode = response.StatusCode;
            var code = (int)statusCode;
            Assert.That(201, Is.EqualTo(code), "User is created");
            Assert.That(400, Is.Not.EqualTo(code), "Users must be unique for complex key name+sex");
            Assert.That(409, Is.Not.EqualTo(code), "Some required fields are missed");
            Assert.That(424, Is.Not.EqualTo(code), "Specified zip code is not available");

            var content = HandleContent.GetContent<CreateUserRes>(response);
            //Assert.That(content.name, Is.EqualTo(createUserReq.name));
            //Assert.That(content.age, Is.EqualTo(createUserReq.age));
            //Assert.That(content.sex, Is.EqualTo(createUserReq.sex));
        }


    }
}
