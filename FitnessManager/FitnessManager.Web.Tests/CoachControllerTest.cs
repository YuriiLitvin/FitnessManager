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
        
        [Fact(DisplayName = "[Post] create Coach")]
        public async void CreateCoach()
        {
            // Arrange
            var coach = new CoachModel
            {
                FirstName = "Ivan",
                LastName  = "Susanin",
                Email = "ivansus@gmail.com",
                MobileNumber = "07773342",
                TypeOfTraining = (TypeOfTraining)3
            };

            var requestJson = JsonConvert.SerializeObject(coach);

            // Act
            var response = await _client.PostAsync(
                "/api/Coach", new StringContent(requestJson, Encoding.UTF8, mediaType: "application/json"));

            // Assert
            Assert.True(response.StatusCode == HttpStatusCode.Created);
            var responseJson = await response.Content.ReadAsStringAsync();
            var responseCoach = JsonConvert.DeserializeObject<CoachModel>(responseJson);
            var coachFromDb = _context.Coaches.FirstOrDefault();

            Assert.Same(responseCoach, coachFromDb);
        }
    } 
}
