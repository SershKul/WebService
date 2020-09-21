using UsersAndAlbumsWebService.Data;
using UsersAndAlbumsWebService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace UsersAndAlbumsWebServiceTest.Fixtures
{
    public class ApiWebApplicationFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration(config =>
            {
                var integrationConfig = new ConfigurationBuilder()
                  .Build();

                config.AddConfiguration(integrationConfig);
            });

            builder.ConfigureTestServices(services =>
            {
                services.AddTransient<IDataProvider, DataProviderStub>();
            });
        }
    }
}
