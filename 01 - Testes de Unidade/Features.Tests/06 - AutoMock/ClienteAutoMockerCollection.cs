using Xunit;

namespace Features.Tests._06___AutoMock
{
    [CollectionDefinition(nameof(ClienteAutoMockerCollection))]
    public class ClienteAutoMockerCollection : ICollectionFixture<ClienteTestsAutoMockerFixture> { }
}
