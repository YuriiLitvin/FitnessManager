using Microsoft.AspNetCore.Hosting;
using System.Net.Http;
using Microsoft.AspNetCore.TestHost;
using FitnessManager.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Xunit;
using System.Net;
using Newtonsoft.Json;
using System.Linq;
using System.Text;
using FitnessManager.Web.Models;
using System.Collections.Generic;
using FitnessManager.Data.Entity;

namespace FitnessManager.Web.Tests
{
    public class CoachControllerTest
    {
        private readonly HttpClient _client;
        
        private FitnessDbContext _context;

        public CoachControllerTest()
        {
            var webHostBuilder = new WebHostBuilder().ConfigureAppConfiguration(builder => builder
                    .AddJsonFile("appsettings.json"))
                    .UseStartup<Startup>();

            var server = new TestServer(webHostBuilder);
            
            _client = server.CreateClient();

            _context = server.Services.GetService<FitnessDbContext>();
        }
        
        #region GET tests
        [Fact(DisplayName ="[Get] get all data from db")]
        public async void GetCoaches()
        {
            // Arrange.

            // Act.
            var response = await _client.GetAsync("/api/Coach");

            // Assert.
            var responseJson = await response.Content.ReadAsStringAsync();
            var responseCoachModels = JsonConvert.DeserializeObject<List<CoachModel>>(responseJson);

            var responseDb = _context.Coaches.ToList();

            Assert.True(responseDb.Count == responseCoachModels.Count);
            Assert.True(response.StatusCode == HttpStatusCode.OK);


            foreach (var coachModel in responseCoachModels)
            {
                responseDb.Any(_ => _.Equals(coachModel));
            }

        }
        
        [Fact(DisplayName = "[Get] check if db is empty")] // DOESN'T WORK
        public async void GetCoachOrNull()
        {
            // Arrange.

            // Act.
            var response = await _client.GetStringAsync("/api/Coach");

            // Assert.

            Assert.False(response == null);
        }

        [Fact(DisplayName = "[Get] get Coach by Id")] 
        public async void GetCoachById()
        {
            // Arrange.
            int coachId = 3;
            // Act.
            var response = await _client.GetAsync("/api/Coach/3");
            
            // Assert.
            var responseJson = await response.Content.ReadAsStringAsync();
            var responseCoachModel = JsonConvert.DeserializeObject<CoachModel>(responseJson);
            var responseCoach = Map(responseCoachModel);

            var responseDb = _context.Coaches.FirstOrDefault(_ => _.Id == coachId);

            Assert.True(response.StatusCode == HttpStatusCode.OK);
            Assert.True(responseDb.Equals(responseCoach));

        }

        [Fact(DisplayName = "[Get] get Coach by invalid Id")]
        public async void GetCoachByInvalidId()
        {
            // Arrange.

            // Act.
            var response = await _client.GetAsync("/api/Coach/40");

            // Assert.
            Assert.True(response.StatusCode == HttpStatusCode.NotFound);
        }
        #endregion


        #region POST tests
        [Fact(DisplayName = "[Post] create Coach")]
        public async void CreateCoach()
        {
            // Arrange.
            var coachModel = new CoachModel
            {
                FirstName = "Ivan",
                LastName  = "Susanin",
                Email = "ivansus@gmail.com",
                MobileNumber = "07773342",
                TypeOfTraining = (TypeOfTraining)3
            };

            
            var requestJson = JsonConvert.SerializeObject(coachModel);

            // Act.
            var response = await _client.PostAsync(
                "/api/Coach", new StringContent(
                    requestJson, Encoding.UTF8, mediaType: "application/json"));

            // Assert.
            
            var responseJson = await response.Content.ReadAsStringAsync();
            var responseCoach = JsonConvert.DeserializeObject<Coach>(responseJson);
            
            var responseDb = _context.Coaches.FirstOrDefault(_ => _.LastName.Contains("Sus"));
            
            
            Assert.True(response.StatusCode == HttpStatusCode.Created);
            Assert.True(responseDb.Equals(responseCoach)); 
            
        }

        [Fact(DisplayName = "[Post] create Coach with notValid data")]
        public async void CreateCoachWithNotValidData()
        {
            // Arrange.
            var coachModel = new CoachModel
            {
                FirstName = "Khan",
                LastName = "Sirofimovich",
                Email = "ks@gmail.com",
                MobileNumber = "0000000",
                TypeOfTraining = (TypeOfTraining)10
            };

            var requestJson = JsonConvert.SerializeObject(coachModel);

            // Act.
            var response = await _client.PostAsync(
                "/api/Coach", new StringContent(requestJson, Encoding.UTF8, mediaType: "application/json"));

            // Assert.
            Assert.True(response.StatusCode == HttpStatusCode.BadRequest);
            
        }
        #endregion


        #region PUT tests
        [Fact(DisplayName = "[Put] update Coach")]
        public async void UpdateCoach()
        {
            // Arrange.
            var coachModel = new CoachModel
            {
                FirstName = "Bezhamin",
                LastName = "Suleimanin",
                Email = "besu@gmail.com",
                MobileNumber = "06663342",
                TypeOfTraining = (TypeOfTraining)2
            };

            var coach = Map(coachModel);
            var requestJson = JsonConvert.SerializeObject(coach);

            // Act.
            var response = await _client.PutAsync(
                "/api/Coach/36", new StringContent(requestJson, Encoding.UTF8, mediaType: "application/json"));

            // Assert.
            var responseDb = _context.Coaches.FirstOrDefault(_ => _.LastName.Contains("Sulei"));

            Assert.True(response.StatusCode == HttpStatusCode.OK);
            Assert.True(responseDb.Equals(coach));

        }

        [Fact(DisplayName = "[Put] update Coach with invalid data")]
        public async void UpdateCoachWithInvalidData()
        {
            // Arrange.
            var coachModel = new CoachModel
            {
                FirstName = "Bezhamin",
                LastName = "Suleimanin",
                Email = "besu@gmail.com",
                MobileNumber = "06663342",
                TypeOfTraining = (TypeOfTraining)10
            };

            var coach = Map(coachModel);
            var requestJson = JsonConvert.SerializeObject(coach);

            // Act.
            var response = await _client.PutAsync(
                "/api/Coach/36", new StringContent(requestJson, Encoding.UTF8, mediaType: "application/json"));

            // Assert.
            Assert.True(response.StatusCode == HttpStatusCode.BadRequest);

        }
        #endregion


        #region DELETE tests
        [Fact(DisplayName = "[Delete] delete Coach by Id")] 
        public async void DeleteCoachByInvalidId()
        {
            // Arrange.

            // Act.
            var response = await _client.DeleteAsync("/api/Coach/28");

            // Assert.
            Assert.True(response.StatusCode == HttpStatusCode.OK);


        }
        [Fact(DisplayName = "[Delete] delete Coach by invalid Id")]
        public async void DeleteCoachById()
        {
            // Arrange.

            // Act.
            var response = await _client.DeleteAsync("/api/Coach/44");

            // Assert.
            Assert.True(response.StatusCode == HttpStatusCode.NotFound);
        }


        #endregion

        
        private Coach Map(CoachModel coachModel)
        {
            var coach = new Coach
            {
                FirstName = coachModel.FirstName,
                LastName = coachModel.LastName,
                Email = coachModel.Email,
                MobileNumber = coachModel.MobileNumber,
                TypeOfTraining = coachModel.TypeOfTraining
            };
            return coach;
        }

    }
}
