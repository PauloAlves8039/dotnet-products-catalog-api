using FluentAssertions;
using ProductCatalog.Domain.Entities;
using ProductCatalog.Domain.Validation;

namespace ProductCatalog.Domain.Tests.Entities
{
    public class ProductUpdateTest
    {
        [Fact(DisplayName = "Update - Product With Valid Parameters")]
        public void Product_Update_ShouldReturnProductWithValidParametersAndNotThrowException()
        {
            var product = new Product("Caderno Espiral", "Caderno espiral 100 fôlhas", 7.99m, 50, "");
            int categoryId = 1;

            Action action = () =>
            {
                product.Update("Caderno Espiral", "Caderno espiral com 100 fôlhas", 8.99m, 60, "caderno-1.png", categoryId);
            };

            action.Should().NotThrow<DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Update - Product With Invalid Name")]
        public void Product_Update_ShouldReturnProductWithShortNameAndThrowException()
        {
            var product = new Product("Caderno Espiral", "Caderno espiral 100 fôlhas", 7.99m, 50, "");

            Action action = () =>
            {
                product.Update("C", "Caderno espiral com 100 fôlhas", 8.99m, 60, "caderno-1.png", 1);
            };

            action.Should().Throw<DomainExceptionValidation>().WithMessage("Invalid name, too short, minimum 3 characters");
        }

        [Fact(DisplayName = "Update - Product With Null Name")]
        public void Product_Update_ShouldReturnProductWithNullNameAndExceptionsThrown()
        {
            var product = new Product("Caderno Espiral", "Caderno espiral 100 fôlhas", 7.99m, 50, "");

            Action action = () =>
            {
                product.Update(null, "Caderno espiral com 100 fôlhas", 8.99m, 60, "caderno-1.png", 1);
            };

            action.Should().Throw<DomainExceptionValidation>().WithMessage("Invalid name, Name is required");
        }


        [Fact(DisplayName = "Update - Product With Invalid Description")]
        public void Product_Update_ShouldReturnProductWithShortDescriptionAndThrowException()
        {
            var product = new Product("Caderno Espiral", "Caderno espiral 100 fôlhas", 7.99m, 50, "");

            Action action = () =>
            {
                product.Update("Caderno Espiral", "Cade", 8.99m, 60, "caderno-1.png", 1);
            };

            action.Should().Throw<DomainExceptionValidation>().WithMessage("Invalid description, too short, minimum 5 characters");
        }

        [Fact(DisplayName = "Update - Product With Null Description")]
        public void Product_Update_ShouldReturnProductWithNullDescriptionAndExceptionsThrown()
        {
            var product = new Product("Caderno Espiral", "Caderno espiral 100 fôlhas", 7.99m, 50, "");

            Action action = () =>
            {
                product.Update("Caderno Espiral", null, 8.99m, 60, "caderno-1.png", 1);
            };

            action.Should().Throw<DomainExceptionValidation>().WithMessage("Invalid description. Description is required");
        }

        [Theory(DisplayName = "Update - Product With Invalid Price")]
        [InlineData(-8.99)]
        [InlineData(-9.99)]
        [InlineData(-10.99)]
        public void Product_Update_ShouldReturnProductWithNegativePriceAndThrowException(decimal price)
        {
            var product = new Product("Caderno Espiral", "Caderno espiral 100 fôlhas", 7.99m, 50, "");

            Action action = () =>
            {
                product.Update("Caderno Espira", "Caderno espiral com 100 fôlhas", price, 560, "caderno-1.png", 1);
            };

            action.Should().Throw<DomainExceptionValidation>().WithMessage("Invalid price value");
        }

        [Theory(DisplayName = "Update - Product With Negative Stock Value")]
        [InlineData(-1)]
        [InlineData(-2)]
        [InlineData(-32)]
        public void Product_Update_ShouldReturnProductWithNegativeStockAndThrowException(int stock)
        {
            var product = new Product("Caderno Espiral", "Caderno espiral 100 fôlhas", 7.99m, 50, "");

            Action action = () =>
            {
                product.Update("Valid Name", "Caderno espiral com 100 fôlhas", 8.99m, stock, "caderno-1.png", 1);
            };

            action.Should().Throw<DomainExceptionValidation>().WithMessage("Invalid stock value");
        }

        [Fact(DisplayName = "Update - Product With Null Image Name")]
        public void Product_Update_ShouldReturnProductWithNullImageAndExceptionsThrown()
        {
            var product = new Product("Caderno Espiral", "Caderno espiral 100 fôlhas", 7.99m, 50, "");

            Action action = () =>
            {
                product.Update("Caderno Espiral", "Caderno espiral com 100 fôlhas", 8.99m, 60, null, 1);
            };

            action.Should().NotThrow<DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Update - Product With Empty Image Name")]
        public void Product_Update_ShouldReturnProductWithEmptymageAndExceptionsThrown()
        {
            var product = new Product("Caderno Espiral", "Caderno espiral 100 fôlhas", 7.99m, 50, "");

            Action action = () =>
            {
                product.Update("Caderno Espiral", "Caderno espiral com 100 fôlhas", 8.99m, 60, "", 1);
            };

            action.Should().NotThrow<DomainExceptionValidation>();
        }
    }
}
