using Bogus;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NerdStore.WebApp.Tests.Config
{
    public class IntegrationTestsFixture : IDisposable
    {
        public string AntiForgeryFieldName = "__RequestVerificationToken";

        public string UsuarioEmail;
        public string UsuarioSenha;

        public readonly LojaAppFactory Factory;
        public HttpClient Client;

        public IntegrationTestsFixture()
        {
            var clientOptions = new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = true,
                BaseAddress = new Uri("http://localhost"),
                HandleCookies = true,
                MaxAutomaticRedirections = 7
            };

            Factory = new LojaAppFactory();
            Client = Factory.CreateClient(clientOptions);
        }

        public void GerarUserSenha()
        {
            var faker = new Faker("pt_BR");
            UsuarioEmail = faker.Internet.Email().ToLower();
            UsuarioSenha = faker.Internet.Password(8, false, "", "@1Ab_");
        }

        public async Task RealizarLoginWeb()
        {
            var initialResponse = await Client.GetAsync("/Identity/Account/Login");
            initialResponse.EnsureSuccessStatusCode();

            var antiForgeryToken = ObterAntiForgeryToken(await initialResponse.Content.ReadAsStringAsync());

            var formData = new Dictionary<string, string>
            {
                {AntiForgeryFieldName, antiForgeryToken},
                {"Input.Email", "teste@teste.com" },
                {"Input.Password", "Teste@123" }
            };
        }

        public string ObterAntiForgeryToken(string htmlBody)
        {
            var requestVerificationTokenMatch = Regex.Match(htmlBody, $@"\<input name=""{AntiForgeryFieldName}"" type=""hidden"" value=""([^""]+)"" \/\>");

            if(requestVerificationTokenMatch.Success) return requestVerificationTokenMatch.Groups[1].Captures[0].Value;

            throw new ArgumentException($"Anti forgery token '{AntiForgeryFieldName}' não encontrado no HTML", nameof(htmlBody));
        }

        public void Dispose() 
        {
            Client.Dispose();
            Factory.Dispose();
        }
    }
}
