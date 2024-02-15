using FluentAssertions;
using ProductCatalog.Domain.Validation;

namespace ProductCatalog.Domain.Tests.Validation
{
    public class DomainExceptionValidationTests
    {
        [Fact(DisplayName = "When - No Errors No Exception Thrown")]
        public void DomainExceptionValidation_When_NoErrorsNoExceptionThrown()
        {

            Action action = () => DomainExceptionValidation.When(false, "An error occurred.");

            action.Should().NotThrow();
        }

        [Fact(DisplayName = "When - Errors Domain Exception Thrown")]
        public void DomainExceptionValidation_When_ErrorsDomainExceptionThrown()
        {

            Action action = () => DomainExceptionValidation.When(true, "An error occurred.");

            action.Should().Throw<DomainExceptionValidation>().WithMessage("An error occurred.");
        }
    }
}
