using FluentAssertions;
using UsersAndAlbumsWebServiceTest.Fixtures;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Threading.Tasks;
using Xunit;
using UsersAndAlbumsWebService.Data;
using Newtonsoft.Json;
using System.Collections.Generic;
using UsersAndAlbumsWebService.Models;
using System.Linq;

namespace UsersAndAlbumsWebServiceTest.Tests
{
    public class UsersControllerTest : IntegrationTest
    {
        public UsersControllerTest(ApiWebApplicationFactory fixture) : base(fixture)
        { }

        [Fact]
        public async Task GetUsersShouldReturnOK()
        {
            // Arrange
            
            // Act
            var response = await _client.GetAsync("/users");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var users = JsonConvert.DeserializeObject<IEnumerable<User>>(
              await response.Content.ReadAsStringAsync()
            );
            Assert.Equal(2, users.Count());
        }

        [Fact]
        public async Task GetUsersShouldReturnError()
        {
            // Arrange
            var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddTransient<IDataProvider, InvalidDataProviderStub>();
                });
            })
           .CreateClient(new WebApplicationFactoryClientOptions());

            // Act
            var response = await client.GetAsync("/users");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetUserByIdShouldReturnOk()
        {
            // Arrange

            // Act
            var response = await _client.GetAsync("/users/1");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var user = JsonConvert.DeserializeObject<User>(
              await response.Content.ReadAsStringAsync()
            );
            Assert.Equal(1, user.Id);
        }

        [Fact]
        public async Task GetUserByIdShouldReturnError()
        {
            // Arrange
            var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddTransient<IDataProvider, InvalidDataProviderStub>();
                });
            })
           .CreateClient(new WebApplicationFactoryClientOptions());

            // Act
            var response = await client.GetAsync("/users/-1");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
