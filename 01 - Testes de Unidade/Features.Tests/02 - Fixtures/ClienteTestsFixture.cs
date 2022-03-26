using Features.Clientes;
using System;

namespace Features.Tests
{
    public class ClienteTestsFixture : IDisposable
    {
        public Cliente GerarClienteValido()
        {
            var cliente = new Cliente(
                Guid.NewGuid(),
                "Yuri",
                "Siman",
                DateTime.Now.AddYears(-30),
                "yuri@gmail.com",
                true,
                DateTime.Now);

            return cliente;
        }

        public Cliente GerarClienteInvalido()
        {
            var cliente = new Cliente(
                Guid.NewGuid(),
                "",
                "",
                DateTime.Now,
                "yuri1gmail.com",
                true,
                DateTime.Now);

            return cliente;
        }

        public void Dispose() { }
    }
}
