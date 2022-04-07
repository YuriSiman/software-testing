using Microsoft.AspNetCore.Mvc.Testing;
using NerdStore.WebApp.Tests.Config;
using System;
using System.Net.Http;

namespace NerdStore.WebApp.Tests.Config
{
    public class IntegrationTestsFixture<TStartup> : IDisposable where TStartup : class
    {
        public readonly LojaAppFactory<TStartup> Factory;
        public HttpClient Client;

        public IntegrationTestsFixture()
        {
            var clientOptions = new WebApplicationFactoryClientOptions
            {

            };

            Factory = new LojaAppFactory<TStartup>();
            Client = Factory.CreateClient(clientOptions);
        }

        public void Dispose() 
        {
            Client.Dispose();
            Factory.Dispose();
        }
    }
}
