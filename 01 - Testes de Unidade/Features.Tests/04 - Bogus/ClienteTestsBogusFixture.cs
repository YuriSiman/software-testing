using Bogus;
using Bogus.DataSets;
using Features.Clientes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Features.Tests
{
    public class ClienteTestsBogusFixture : IDisposable
    {
        public Cliente GerarClienteValido()
        {
            return GerarClientes(1, true, DateTime.Now).FirstOrDefault();
        }

        public Cliente GerarClienteValidoEspecial()
        {
            return GerarClientes(1, true, DateTime.Now.AddYears(-3)).FirstOrDefault();
        }

        public IEnumerable<Cliente> ObterClientesVariados()
        {
            var clientes = new List<Cliente>();

            clientes.AddRange(GerarClientes(50, true, DateTime.Now).ToList());
            clientes.AddRange(GerarClientes(50, false, DateTime.Now).ToList());

            return clientes;
        }

        public IEnumerable<Cliente> GerarClientes(int quantidade, bool ativo, DateTime date)
        {
            var genero = new Faker().PickRandom<Name.Gender>();

            var clientes = new Faker<Cliente>("pt_BR")
                .CustomInstantiator(f => new Cliente(
                    Guid.NewGuid(),
                    f.Name.FirstName(genero),
                    f.Name.LastName(genero),
                    f.Date.Past(80, DateTime.Now.AddYears(-18)),
                    "",
                    ativo,
                    date))
                .RuleFor(c => c.Email, (f,c) => 
                    f.Internet.Email(c.Nome.ToLower(), c.Sobrenome.ToLower()));

            return clientes.Generate(quantidade);
        }

        public Cliente GerarClienteInvalido()
        {
            var genero = new Faker().PickRandom<Name.Gender>();

            var cliente = new Faker<Cliente>("pt_BR")
                .CustomInstantiator(f => new Cliente(
                    Guid.NewGuid(),
                    f.Name.FirstName(genero),
                    "",
                    f.Date.Past(1, DateTime.Now.AddYears(1)),
                    "",
                    false,
                    DateTime.Now));

            return cliente;
        }

        public void Dispose() { }
    }
}
