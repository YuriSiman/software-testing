using Features.Core;
using FluentAssertions;
using Xunit;

namespace Features.Tests
{
    public class CpfValidationTests
    {
        [Theory(DisplayName = "CPF Válidos")]
        [Trait("01 - Testes de Unidade", "CPF Validation Tests")]
        [InlineData("15231766607")]
        [InlineData("78246847333")]
        [InlineData("64184957307")]
        [InlineData("21681764423")]
        [InlineData("13830803800")]
        public void Cpf_ValidarMultiplosNumeros_TodosDevemSerValidos(string cpf)
        {
            // Arrange
            var cpfValidation = new CpfValidation();

            // Act & Assert
            //Assert.True(cpfValidation.EhValido(cpf));

            // Act & Assert - Fluent Assertion
            cpfValidation.EhValido(cpf).Should().BeTrue();
        }

        [Theory(DisplayName = "CPF Inválidos")]
        [Trait("01 - Testes de Unidade", "CPF Validation Tests")]
        [InlineData("15231766607123")]
        [InlineData("12345678945")]
        [InlineData("12358549874")]
        [InlineData("00025401870")]
        public void Cpf_ValidarMultiplosNumeros_TodosDevemSerInvalidos(string cpf)
        {
            // Arrange
            var cpfValidation = new CpfValidation();

            // Act & Assert
            //Assert.False(cpfValidation.EhValido(cpf));

            // Act & Assert - Fluent Assertion
            cpfValidation.EhValido(cpf).Should().BeFalse();
        }
    }
}
