using FluentAssertions;
using ProductCatalog.Application.DTOs;
using System.ComponentModel.DataAnnotations;

namespace ProductCatalog.Application.Tests.DTOs
{
    public class CategoryDTOTest
    {
        [Fact(DisplayName = "Create - CategoryDTO With All Data Valid")]
        public void CategoryDTO_Create_ShouldReturnCategoryWithValidParameters()
        {
            var categoryDTO = new CategoryDTO
            {
                Id = 1,
                Name = "Material Escolar"
            };

            var context = new ValidationContext(categoryDTO, null, null);
            var validationResult = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(categoryDTO, context, validationResult, true);

            isValid.Should().BeTrue();
            validationResult.Should().BeEmpty();
        }

        [Fact(DisplayName = "Create - CategoryDTO With Id Negative")]
        public void CategoryDTO_Create_ShouldReturnCategoryDTOWithIdNegative()
        {
            var categoryDTO = new CategoryDTO
            {
                Id = -1
            };

            var context = new ValidationContext(categoryDTO, null, null);
            var validationResult = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(categoryDTO, context, validationResult, true);

            isValid.Should().BeFalse();
            validationResult.Should().ContainSingle(e => e.ErrorMessage == "Invalid Id value");
        }

        [Fact(DisplayName = "Create - CategoryDTO With Empty Name")]
        public void CategoryDTO_Create_ShouldReturnCategoryDTOWithEmptyName()
        {
            var categoryDTO = new CategoryDTO
            {
                Name = string.Empty
            };

            var context = new ValidationContext(categoryDTO, null, null);
            var validationResult = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(categoryDTO, context, validationResult, true);

            isValid.Should().BeFalse();
            validationResult.Should().ContainSingle(e => e.ErrorMessage == "The Name is Required");
        }

        [Fact(DisplayName = "Create - CategoryDTO With Short Name")]
        public void CategoryDTO_Create_ShouldReturnCategoryDTOWithShortName()
        {
            var categoryDTO = new CategoryDTO
            {
                Name = "Ma"
            };

            var context = new ValidationContext(categoryDTO, null, null);
            var validationResult = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(categoryDTO, context, validationResult, true);

            isValid.Should().BeFalse();
            validationResult.Should().ContainSingle(e => e.ErrorMessage == "The field Name must be a string or array type with a minimum length of '3'.");
        }

        [Fact(DisplayName = "Create - CategoryDTO With Null Name")]
        public void CategoryDTO_Create_ShouldReturnCategoryDTOWithNullName()
        {
            var categoryDTO = new CategoryDTO
            {
                Name = null
            };

            var context = new ValidationContext(categoryDTO, null, null);
            var validationResult = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(categoryDTO, context, validationResult, true);

            isValid.Should().BeFalse();
            validationResult.Should().ContainSingle(e => e.ErrorMessage == "The Name is Required");
        }

        [Fact(DisplayName = "Create - CategoryDTO With Long Name")]
        public void CategoryDTO_Create_ShouldReturnCategoryDTOWithLongName()
        {
            var categoryDTO = new CategoryDTO
            {
                Name = new string('M', 101)
            };

            var context = new ValidationContext(categoryDTO, null, null);
            var validationResult = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(categoryDTO, context, validationResult, true);

            isValid.Should().BeFalse();
            validationResult.Should().ContainSingle(e => e.ErrorMessage == "The field Name must be a string or array type with a maximum length of '100'.");
        }
    }
}
