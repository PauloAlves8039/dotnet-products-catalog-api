using FluentAssertions;
using ProductCatalog.Domain.Entities;
using ProductCatalog.Domain.Validation;

namespace ProductCatalog.Domain.Tests.Entities
{
    public class CategoryUpdateTest
    {
        [Fact(DisplayName = "Update - Category Name With Valid Value")]
        public void Category_Update_ShouldReturnCategoryWithValidParameterAndNotThrowException()
        {
            var category = new Category("Material Escola");

            Action action = () =>
            {
                category.Update("Material Escolar");
            };

            action.Should().NotThrow<DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Update - Category Name With Short Value")]
        public void Category_Update_ShouldReturnCategoryWithShortNameAndThrowException()
        {
            var category = new Category("Material Escola");

            Action action = () =>
            {
                category.Update("Ma");
            };

            action.Should().Throw<DomainExceptionValidation>().WithMessage("Invalid name, too short, minimum 3 characters");
        }

        [Fact(DisplayName = "Update - Category Name With Null Value")]
        public void Category_Update_ShouldReturnCategoryWithNullNameAndThrowException()
        {
            var category = new Category("Material Escola");

            Action action = () =>
            {
                category.Update(null);
            };

            action.Should().Throw<DomainExceptionValidation>().WithMessage("Invalid name, Name is required");
        }
    }
}
