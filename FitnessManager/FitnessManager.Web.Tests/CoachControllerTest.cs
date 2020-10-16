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
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore;

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
            var coach1 = new Coach
            {
                FirstName = "Ioan",
                LastName = "Susanin",
                Email = "ivansus@gmail.com",
                MobileNumber = "07773342",
                TypeOfTraining = (TypeOfTraining)3
            };
            var coach2 = new Coach
            {
                FirstName = "Atem",
                LastName = "Busanin",
                Email = "artbus@gmail.com",
                MobileNumber = "0443456",
                TypeOfTraining = (TypeOfTraining)1
            };

            var list = new List<Coach>
            {
                coach1,
                coach2
            };

            _context.Coaches.AddRange(list);
            _context.SaveChanges();
            var responseDb = _context.Coaches.ToList();

            // Act.
            var response = await _client.GetAsync("/api/Coach");

            // Assert.
            var responseJson = await response.Content.ReadAsStringAsync();
            var responseCoachModels = JsonConvert.DeserializeObject<List<CoachModel>>(responseJson);
            
            Assert.Equal(responseDb.Count,responseCoachModels.Count);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);


            foreach (var coachModel in responseCoachModels)
            {
                Assert.All(responseDb, _ => _.Equals(coachModel));
            }

        }
        
        [Fact(DisplayName = "[Get] check if no content")] 
        public async void GetNoContent()
        {
            var list = _context.Coaches.ToList();
            _context.Coaches.RemoveRange(list);
            _context.SaveChanges();

            // Act.
            var response = await _client.GetAsync("/api/Coach");

            // Assert.
            Assert.Equal(HttpStatusCode.NoContent,response.StatusCode);
        }

        [Fact(DisplayName = "[Get] get Coach by Id")] 
        public async void GetCoachById()
        {
            // Arrange.
            var coach = new Coach
            {
                FirstName = "Rob",
                LastName = "Murrey",
                Email = "bmarl@gmail.com",
                MobileNumber = "0342895834",
                TypeOfTraining = (TypeOfTraining)2
            };

            _context.Coaches.Add(coach);
            _context.SaveChanges();

            var coachId = coach.Id;

            // Act.
            var response = await _client.GetAsync($"/api/Coach/{coachId}");
            
            // Assert.
            var responseJson = await response.Content.ReadAsStringAsync();
            var responseCoachModel = JsonConvert.DeserializeObject<CoachModel>(responseJson);
            var responseCoach = Map(responseCoachModel);


            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True(coach.Equals(responseCoach));

        }

        [Fact(DisplayName = "[Get] get Coach by invalid Id")]
        public async void GetCoachByInvalidId()
        {
            // Arrange.
            int invalidCoachId = 60;
            // Act.
            var response = await _client.GetAsync($"/api/Coach/{invalidCoachId}");

            // Assert.
            Assert.Equal(HttpStatusCode.NotFound,response.StatusCode);
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
            var coach = Map(coachModel);

            // Act.
            var response = await _client.PostAsync(
                "/api/Coach", new StringContent(
                    requestJson, Encoding.UTF8, mediaType: "application/json"));

            // Assert.
            var responseJson = await response.Content.ReadAsStringAsync();
            var responseCoach = JsonConvert.DeserializeObject<Coach>(responseJson);


            Assert.Equal(HttpStatusCode.Created,response.StatusCode);
            Assert.Equal(coach,responseCoach); 
            
        }

        [Fact(DisplayName = "[Post] create Coach with invalid data")]
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
            Assert.Equal(HttpStatusCode.BadRequest,response.StatusCode);
            
        }
        #endregion


        #region PUT tests
        [Fact(DisplayName = "[Put] update Coach")]
        public async void UpdateCoach()
        {
            // Arrange.
            var coachModel = new CoachModel
            {
                FirstName = "Bezh",
                LastName = "Sulenin",
                Email = "besu@gmail.com",
                MobileNumber = "06663342",
                TypeOfTraining = (TypeOfTraining)2
            };

            var requestCoach = Map(coachModel);
            var requestJson = JsonConvert.SerializeObject(requestCoach);
            
            var coach = _context.Coaches.FirstOrDefault(_ => _ != null);
            var coachId = coach.Id;
            
            // Act.
            var response = await _client.PutAsync($"/api/Coach/{coachId}",
                new StringContent(requestJson, Encoding.UTF8, mediaType: "application/json"));

            // Assert.
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

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

            var requestCoach = Map(coachModel);
            var requestJson = JsonConvert.SerializeObject(requestCoach);
            
            var coach = _context.Coaches.FirstOrDefault(_ => _ != null);
            var coachId = coach.Id;
            // Act.
            var response = await _client.PutAsync($"/api/Coach/{coachId}",
                new StringContent(requestJson, Encoding.UTF8, mediaType: "application/json"));

            // Assert.
            Assert.Equal(HttpStatusCode.BadRequest,response.StatusCode);

        }
        #endregion


        #region DELETE tests
        [Fact(DisplayName = "[Delete] delete Coach by Id")]
        public async void DeleteCoachById()
        {
            // Arrange.
            var coach = _context.Coaches.FirstOrDefault(_ => _ != null);
            var coachId = coach.Id;
            // Act.
            var response = await _client.DeleteAsync($"/api/Coach/{coachId}");

            // Assert.
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }


        [Fact(DisplayName = "[Delete] delete Coach by invalid Id")]
        public async void DeleteCoachByInvalidId()
        {
            // Arrange.
            var coachId = _context.Coaches.Max(_=>_.Id);
            var invalidCoachId = coachId + 100;
            // Act.
            var response = await _client.DeleteAsync($"/api/Coach/{invalidCoachId}");

            // Assert.
            
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

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
