using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net.Http;

namespace NerdStore.WebApp.Tests.Config
{
    public class IntegrationTestsFixture : IDisposable
    {
        public readonly LojaAppFactory Factory;
        public HttpClient Client;

        public IntegrationTestsFixture()
        {
            var clientOptions = new WebApplicationFactoryClientOptions
            {

            };

            Factory = new LojaAppFactory();
            Client = Factory.CreateClient(clientOptions);
        }

        public void Dispose() 
        {
            Client.Dispose();
            Factory.Dispose();
        }
    }
}
