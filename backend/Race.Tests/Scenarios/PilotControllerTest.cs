using FluentAssertions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Race.Model.Models;
using Race.Repo.Dtos.Pilots;
using System.Collections.Generic;
using Xunit;

namespace Race.Tests.Scenarios
{
    public class PilotControllerTest : IClassFixture<RaceWebApplicationFactory>
    {
        private readonly RaceWebApplicationFactory factory;
        private readonly JsonSerializerSettings settings;

        public PilotControllerTest(RaceWebApplicationFactory factory)
        {
            this.factory = factory;
            this.settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore
            };
        }

        [Fact]
        public async void Get_Pilot_By_Id_Works()
        {
            //Arrange
            var client = factory.CreateClient();
            Pilot pilot = CreatePilot();

            var expectedResult = new List<PilotDetailsDto> { new PilotDetailsDto(pilot) };

            //Act
            var response = await client.GetAsync($"api/pilot/{pilot.Id}");


            //Assert
            settings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
            var stringResponse = await response.Content.ReadAsStringAsync();
            var responseContent =
                JsonConvert.DeserializeObject<List<PilotDetailsDto>>(stringResponse, settings);

            responseContent.Should().BeEquivalentTo(expectedResult);
        }

        private static Pilot CreatePilot()
        {
            return new Pilot
            {
                Id = 1000,
                Code = "Test",
                Name = "Test Pilot",
                Nationality = "HUN",
                Number = "33",
                TeamId = 1000,
            };
        }
    }
}
