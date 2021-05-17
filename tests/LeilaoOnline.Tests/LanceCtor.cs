using LeilaoOnline.Core;
using System;
using Xunit;

namespace LeilaoOnline.Tests
{
    public class LanceCtor
    {
        [Fact]
        public void LancaArgumentExceptionDadoValorNegativo()
        {
            // Arrange - Given
            var valorNegativo = -100;

            // Assert - When
            Assert.Throws<ArgumentException>(
                // Act - Then
                () => new Lance(null, valorNegativo)
            );
        }
    }
}
