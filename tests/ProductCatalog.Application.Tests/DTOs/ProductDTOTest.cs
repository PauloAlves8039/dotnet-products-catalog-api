using FluentAssertions;
using ProductCatalog.Application.DTOs;
using ProductCatalog.Domain.Validation;
using System.ComponentModel.DataAnnotations;

namespace ProductCatalog.Application.Tests.DTOs
{
    public class ProductDTOTest
    {
        [Fact(DisplayName = "Create - Valid ProductDTO")]
        public void ProductDTO_Create_ShouldReturnValidProductDTO()
        {
            var productDTO = new ProductDTO
            {
                Id = 1,
                Name = "Caderno Espiral",
                Description = "Caderno espiral com 100 fôlhas",
                Price = 7.99m,
                Stock = 50,
                Image = "caderno-1.png",
                CategoryId = 1
            };

            var context = new ValidationContext(productDTO, null, null);
            var validationResult = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(productDTO, context, validationResult, true);

            isValid.Should().BeTrue();
            validationResult.Should().BeEmpty();
        }

        [Fact(DisplayName = "Create - ProductDTO With Id Negative")]
        public void ProductDTO_Create_ShouldReturnProductDTOWithIdNegative()
        {
            var productDTO = new ProductDTO
            {
                Id = -1,
                Name = "Caderno Espiral",
                Description = "Caderno espiral com 100 fôlhas",
                Price = 7.99m,
                Stock = 50,
                Image = "caderno-1.png",
                CategoryId = 1
            };

            var context = new ValidationContext(productDTO, null, null);
            var validationResult = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(productDTO, context, validationResult, true);

            isValid.Should().BeFalse();
            validationResult.Should().ContainSingle(e => e.ErrorMessage == "Invalid Id value");
        }

        [Fact(DisplayName = "Create - ProductDTO With Empty Name")]
        public void ProductDTO_Create_ShouldReturnProductDTOWithEmptyName()
        {
            var productDTO = new ProductDTO
            {
                Id = 1,
                Name = string.Empty,
                Description = "Caderno espiral com 100 fôlhas",
                Price = 7.99m,
                Stock = 50,
                Image = "caderno-1.png",
                CategoryId = 1
            };

            var context = new ValidationContext(productDTO, null, null);
            var validationResult = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(productDTO, context, validationResult, true);

            isValid.Should().BeFalse();
            validationResult.Should().ContainSingle(e => e.ErrorMessage == "The Name is Required");
        }

        [Fact(DisplayName = "Create - ProductDTO With Null Name")]
        public void ProductDTO_Create_ShouldReturnProductDTOWithNullName()
        {
            var productDTO = new ProductDTO
            {
                Id = 1,
                Name = null,
                Description = "Caderno espiral com 100 fôlhas",
                Price = 7.99m,
                Stock = 50,
                Image = "caderno-1.png",
                CategoryId = 1
            };

            var context = new ValidationContext(productDTO, null, null);
            var validationResult = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(productDTO, context, validationResult, true);

            isValid.Should().BeFalse();
            validationResult.Should().ContainSingle(e => e.ErrorMessage == "The Name is Required");
        }

        [Fact(DisplayName = "Create - ProductDTO With Short Name")]
        public void ProductDTO_Create_ShouldReturnProductDTOWithShortName()
        {
            var productDTO = new ProductDTO
            {
                Id = 1,
                Name = "Ca",
                Description = "Caderno espiral com 100 fôlhas",
                Price = 7.99m,
                Stock = 50,
                Image = "caderno-1.png",
                CategoryId = 1
            };

            var context = new ValidationContext(productDTO, null, null);
            var validationResult = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(productDTO, context, validationResult, true);

            isValid.Should().BeFalse();
            validationResult.Should().ContainSingle(e => e.ErrorMessage == "The field Name must be a string or array type with a minimum length of '3'.");
        }

        [Fact(DisplayName = "Create - ProductDTO With Long Name")]
        public void ProductDTO_Create_ShouldReturnProductDTOWithLongName()
        {
            var productDTO = new ProductDTO
            {
                Id = 1,
                Name = new string('C', 101),
                Description = "Caderno espiral com 100 fôlhas",
                Price = 7.99m,
                Stock = 50,
                Image = "caderno-1.png",
                CategoryId = 1
            };

            var context = new ValidationContext(productDTO, null, null);
            var validationResult = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(productDTO, context, validationResult, true);

            isValid.Should().BeFalse();
            validationResult.Should().ContainSingle(e => e.ErrorMessage == "The field Name must be a string or array type with a maximum length of '100'.");
        }

        [Fact(DisplayName = "Create - ProductDTO With Empty Description")]
        public void ProductDTO_Create_ShouldReturnProductDTOWithEmptyDescription()
        {
            var productDTO = new ProductDTO
            {
                Id = 1,
                Name = "Caderno Espiral",
                Description = string.Empty,
                Price = 7.99m,
                Stock = 50,
                Image = "caderno-1.png",
                CategoryId = 1
            };

            var context = new ValidationContext(productDTO, null, null);
            var validationResult = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(productDTO, context, validationResult, true);

            isValid.Should().BeFalse();
            validationResult.Should().ContainSingle(e => e.ErrorMessage == "The Description is Required");
        }

        [Fact(DisplayName = "Create - ProductDTO With Null Description")]
        public void ProductDTO_Create_ShouldReturnProductDTOWithNullDescription()
        {
            var productDTO = new ProductDTO
            {
                Id = 1,
                Name = "Caderno Espiral",
                Description = null,
                Price = 7.99m,
                Stock = 50,
                Image = "caderno-1.png",
                CategoryId = 1
            };

            var context = new ValidationContext(productDTO, null, null);
            var validationResult = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(productDTO, context, validationResult, true);

            isValid.Should().BeFalse();
            validationResult.Should().ContainSingle(e => e.ErrorMessage == "The Description is Required");
        }

        [Fact(DisplayName = "Create - ProductDTO With Short Description")]
        public void ProductDTO_Create_ShouldReturnProductDTOWithShortDescription()
        {
            var productDTO = new ProductDTO
            {
                Id = 1,
                Name = "Caderno Espiral",
                Description = "Cad",
                Price = 7.99m,
                Stock = 50,
                Image = "caderno-1.png",
                CategoryId = 1
            };

            var context = new ValidationContext(productDTO, null, null);
            var validationResult = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(productDTO, context, validationResult, true);

            isValid.Should().BeFalse();
            validationResult.Should().ContainSingle(e => e.ErrorMessage == "The field Description must be a string or array type with a minimum length of '5'.");
        }

        [Fact(DisplayName = "Create - ProductDTO With Long Description")]
        public void ProductDTO_Create_ShouldReturnProductDTOWithLongDescription()
        {
            var productDTO = new ProductDTO
            {
                Id = 1,
                Name = "Caderno Espiral",
                Description = new string('C', 201),
                Price = 7.99m,
                Stock = 50,
                Image = "caderno-1.png",
                CategoryId = 1
            };

            var context = new ValidationContext(productDTO, null, null);
            var validationResult = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(productDTO, context, validationResult, true);

            isValid.Should().BeFalse();
            validationResult.Should().ContainSingle(e => e.ErrorMessage == "The field Description must be a string or array type with a maximum length of '200'.");
        }

        [Theory(DisplayName = "Create - ProductDTO With Negative Price Value")]
        [InlineData(-1.00)]
        [InlineData(-2.00)]
        public void ProductDTO_Create_ShouldReturnProductDTOWithNegativePrice(decimal price)
        {
            var productDTO = new ProductDTO
            {
                Id = 1,
                Name = "Caderno Espiral",
                Description = "Caderno espiral com 100 fôlhas",
                Price = price,
                Stock = 50,
                Image = "caderno-1.png",
                CategoryId = 1
            };

            Action action = () => DomainExceptionValidation.When(productDTO.Price < 0, "The field Preço must be greater than or equal to 0.");

            action.Should().Throw<DomainExceptionValidation>().WithMessage("The field Preço must be greater than or equal to 0.");
        }

        [Theory(DisplayName = "Create - ProductDTO With Negative Stock Value")]
        [InlineData(-1)]
        [InlineData(-2)]
        public void ProductDTO_Create_ShouldReturnProductDTOWithNegativeStock(int stock)
        {
            var productDTO = new ProductDTO
            {
                Id = 1,
                Name = "Caderno Espiral",
                Description = "Caderno espiral com 100 fôlhas",
                Price = 7.99m,
                Stock = stock,
                Image = "caderno-1.png",
                CategoryId = 1
            };

            Action action = () => DomainExceptionValidation.When(productDTO.Stock < 1 || productDTO.Stock > 9999, "The field Estoque must be between 1 and 9999.");

            action.Should().Throw<DomainExceptionValidation>().WithMessage("The field Estoque must be between 1 and 9999.");
        }

        [Fact(DisplayName = "Create - ProductDTO With Long Image Name")]
        public void ProductDTO_Create_ShouldReturnProductDTOWithLongImageName()
        {
            var productDTO = new ProductDTO
            {
                Id = 1,
                Name = "Caderno Espiral",
                Description = "Caderno espiral com 100 fôlhas",
                Price = 7.99m,
                Stock = 50,
                Image = new string('a', 251),
                CategoryId = 1
            };

            Action action = () => DomainExceptionValidation.When(productDTO.Image?.Length > 250, "Invalid image name, too long, maximum 250 characters.");

            action.Should().Throw<DomainExceptionValidation>().WithMessage("Invalid image name, too long, maximum 250 characters.");
        }
    }
}
