using Xunit;

namespace Demo.Tests
{
    public class AssertStringsTests
    {
        [Fact]
        public void StringsTools_UnirNomes_RetornarNomeCompleto()
        {
            // Arrange
            var sut = new StringsTools();

            // Act
            var nomeCompleto = sut.Unir("Yuri", "Siman");

            // Assert
            Assert.Equal("Yuri Siman", nomeCompleto);
        }

        [Fact]
        public void StringsTools_UnirNomes_DeveIgnorarCase()
        {
            // Arrange
            var sut = new StringsTools();

            // Act
            var nomeCompleto = sut.Unir("Yuri", "Siman");

            // Assert
            Assert.Equal("YURI SIMAN", nomeCompleto, true);
        }

        [Fact]
        public void StringsTools_UnirNomes_DeveConterTrecho()
        {
            // Arrange
            var sut = new StringsTools();

            // Act
            var nomeCompleto = sut.Unir("Yuri", "Siman");

            // Assert
            Assert.Contains("uri", nomeCompleto);
        }

        [Fact]
        public void StringsTools_UnirNomes_DeveComecarCom()
        {
            // Arrange
            var sut = new StringsTools();

            // Act
            var nomeCompleto = sut.Unir("Yuri", "Siman");

            // Assert
            Assert.StartsWith("Yur", nomeCompleto);
        }

        [Fact]
        public void StringsTools_UnirNomes_DeveAcabarCom()
        {
            // Arrange
            var sut = new StringsTools();

            // Act
            var nomeCompleto = sut.Unir("Yuri", "Siman");

            // Assert
            Assert.EndsWith("man", nomeCompleto);
        }

        [Fact]
        public void StringsTools_UnirNomes_ValidarExpressaoRegular()
        {
            // Arrange
            var sut = new StringsTools();

            // Act
            var nomeCompleto = sut.Unir("Yuri", "Siman");

            // Assert
            Assert.Matches("[A-Z]{1}[a-z]+ [A-Z]{1}[a-z]+", nomeCompleto);
        }
    }
}
