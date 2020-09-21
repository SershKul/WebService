using FluentAssertions;
using UsersAndAlbumsWebServiceTest.Fixtures;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Threading.Tasks;
using Xunit;
using UsersAndAlbumsWebService.Data;
using Newtonsoft.Json;
using UsersAndAlbumsWebService.Models;
using System.Collections.Generic;
using System.Linq;

namespace UsersAndAlbumsWebServiceTest.Tests
{
    public class AlbumsControllerTest : IntegrationTest
    {
        public AlbumsControllerTest(ApiWebApplicationFactory fixture)
          : base(fixture) { }

        [Fact]
        public async Task GetAlbumsShouldReturnOK()
        {
            // Arrange

            // Act
            var response = await _client.GetAsync("/albums");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var albums = JsonConvert.DeserializeObject<IEnumerable<Album>>(
                await response.Content.ReadAsStringAsync()
            );
            Assert.Equal(2, albums.Count());
        }

        [Fact]
        public async Task GetAlbumsShouldReturnError()
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
            var response = await client.GetAsync("/albums");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetAlbumByIdShouldReturnOk()
        {
            // Arrange

            // Act
            var response = await _client.GetAsync("/albums/1");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var album = JsonConvert.DeserializeObject<Album>(
                await response.Content.ReadAsStringAsync()
            );
            Assert.Equal(1, album.Id);
        }

        [Fact]
        public async Task GetAlbumByIdShouldReturnError()
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
            var response = await client.GetAsync("/albuma/-1");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetAlbumsByUserIdShouldReturnOk()
        {
            // Arrange

            // Act
            var response = await _client.GetAsync("/albums?userid=1");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var albums = JsonConvert.DeserializeObject<IEnumerable<Album>>(
                await response.Content.ReadAsStringAsync()
            );
            Assert.Equal(2, albums.Count());
        }

        [Fact]
        public async Task GetAlbumsByUserIdShouldReturnError()
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
            var response = await client.GetAsync("/albuma?userid=1");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
