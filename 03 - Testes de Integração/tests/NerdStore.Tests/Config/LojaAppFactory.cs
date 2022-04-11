using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;

namespace NerdStore.WebApp.Tests.Config
{
    public class LojaAppFactory : WebApplicationFactory<Program>
    {
        private readonly string _environment;

        public LojaAppFactory(string environment = "Testing")
        {
            _environment = environment;
        }

        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.UseEnvironment(_environment);

            // Adicionando serviços de mock/test
            //builder.ConfigureServices(services =>
            //{
            //    services.AddScoped(sp =>
            //    {
            //        // Replace SQLite with in-memory database for tests
            //        return new DbContextOptionsBuilder<CatalogoContext>()
            //        .UseInMemoryDatabase("Tests")
            //        .UseApplicationServiceProvider(sp)
            //        .Options;
            //    });
            //});

            return base.CreateHost(builder);
        }
    }
}
