using Moq;
using ProductCatalog.Application.DTOs;
using ProductCatalog.Application.Interfaces;

namespace ProductCatalog.Application.Tests.Interfaces
{
    public class CategoryServiceTest
    {
        List<CategoryDTO> listCategories = new List<CategoryDTO>
        {
            new CategoryDTO { Id = 1, Name = "Material Escolar" },
            new CategoryDTO { Id = 2, Name = "Eletrônicos" },
            new CategoryDTO { Id = 3, Name = "Acessórios" },
        };

        [Fact(DisplayName = "GetCategories - Return All Categories")]
        public async Task CategoryService_GetCategories_ShouldReturnAllCategories()
        {
            var mockCategoryService = new Mock<ICategoryService>();

            mockCategoryService.Setup(service => service.GetCategories()).ReturnsAsync(listCategories);

            var categoryService = mockCategoryService.Object;

            var result = await categoryService.GetCategories();


            Assert.NotNull(result);
            Assert.Equal(listCategories.Count, result.Count());
        }

        [Fact(DisplayName = "GetCategories - Return Empty List When No Categories Exist")]
        public async Task CategoryService_GetCategories_ShouldReturnEmptyListWhenNoCategoriesExist()
        {
            var mockCategoryService = new Mock<ICategoryService>();
            var emptyCategories = new List<CategoryDTO>();

            mockCategoryService.Setup(service => service.GetCategories()).ReturnsAsync(emptyCategories);

            var categoryService = mockCategoryService.Object;

            var result = await categoryService.GetCategories();

            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact(DisplayName = "GetById - Returns CategoryDTO When Id exists")]
        public async Task CategoryService_GetById_ShouldReturnCategoryDTOWhenIdExists()
        {
            var mockCategoryService = new Mock<ICategoryService>();
            var categoryId = 1;
            var categoryDto = new CategoryDTO { Id = categoryId, Name = "Material Escolar" };

            mockCategoryService.Setup(service => service.GetById(categoryId)).ReturnsAsync(categoryDto);

            var categoryService = mockCategoryService.Object;

            var result = await categoryService.GetById(categoryId);

            Assert.NotNull(result);
            Assert.Equal(categoryId, result.Id);
        }

        [Fact(DisplayName = "GetById - Returns null when id does not exist")]
        public async Task CategoryService_GetById_ShouldReturnNullWhenCategoryDTOIdDoesNotExist()
        {
            var mockCategoryService = new Mock<ICategoryService>();
            var invalidCategoryId = 9999;

            mockCategoryService.Setup(service => service.GetById(invalidCategoryId)).ReturnsAsync((CategoryDTO)null);

            var categoryService = mockCategoryService.Object;

            var result = await categoryService.GetById(invalidCategoryId);

            Assert.Null(result);
        }

        [Fact(DisplayName = "Add - Return The Added CategoryDTO")]
        public async Task CategoryService_Add_ShouldInsertCategoryAndReturnAddedCategoryDTO()
        {
            var mockCategoryService = new Mock<ICategoryService>();
            var categoryDto = new CategoryDTO { Name = "Livros" };

            mockCategoryService.Setup(service => service.Add(categoryDto)).Verifiable();

            var categoryService = mockCategoryService.Object;

            await categoryService.Add(categoryDto);

            mockCategoryService.Verify();
        }

        [Fact(DisplayName = "Add - Returns Null When Add Fails")]
        public async Task CategoryService_Add_ShouldReturnNullWhenAddedCategoryDTOFails()
        {
            var mockCategoryService = new Mock<ICategoryService>();
            var invalidCategoryDto = new CategoryDTO { Name = null };

            mockCategoryService.Setup(service => service.Add(invalidCategoryDto))
                .ThrowsAsync(new InvalidOperationException("Livros"));

            var categoryService = mockCategoryService.Object;

            Func<Task> act = async () => await categoryService.Add(invalidCategoryDto);

            await Assert.ThrowsAsync<InvalidOperationException>(act);
            mockCategoryService.Verify(service => service.Add(invalidCategoryDto), Times.Once);
        }

        [Fact(DisplayName = "Update - Return Updated CategoryDTO")]
        public async Task CategoryService_Update_ShouldUpdateReturnUpdatedCategoryDTO()
        {
            var mockCategoryService = new Mock<ICategoryService>();
            var updatedCategoryDto = new CategoryDTO { Id = 1, Name = "Materiais Escolares" };

            mockCategoryService.Setup(service => service.Update(updatedCategoryDto)).Verifiable();

            var categoryService = mockCategoryService.Object;

            await categoryService.Update(updatedCategoryDto);

            mockCategoryService.Verify();
        }

        [Fact(DisplayName = "Update - Returns Null When CategoryDTO Not Found")]
        public async Task CategoryService_Update_ShouldReturnNullWhenCategoryDTONotFound()
        {
            var mockCategoryService = new Mock<ICategoryService>();
            var invalidCategoryDto = new CategoryDTO { Id = 1, Name = null };

            mockCategoryService.Setup(service => service.Update(invalidCategoryDto))
                .ThrowsAsync(new InvalidOperationException("Invalid category update"));

            var categoryService = mockCategoryService.Object;

            Func<Task> act = async () => await categoryService.Update(invalidCategoryDto);

            await Assert.ThrowsAsync<InvalidOperationException>(act);
        }

        [Fact(DisplayName = "Remove - Returns The Existing CategoryDTO Delete")]
        public async Task CategoryService_Remove_ShouldRemoveCategoryAndReturnRemovedCategoryDTO()
        {
            var mockCategoryService = new Mock<ICategoryService>();
            var categoryId = 1;

            mockCategoryService.Setup(service => service.Remove(categoryId)).Verifiable();

            var categoryService = mockCategoryService.Object;

            await categoryService.Remove(categoryId);

            mockCategoryService.Verify();
        }

        [Fact(DisplayName = "Remove - Returns Null When CategoryDTO Not Found")]
        public async Task CategoryService_Remove_ShouldReturnNullWhenCategoryDTONotFound()
        {
            var mockCategoryService = new Mock<ICategoryService>();
            var invalidCategoryId = 9999;

            mockCategoryService.Setup(service => service.Remove(invalidCategoryId))
                .ThrowsAsync(new InvalidOperationException("Invalid category removal"));

            var categoryService = mockCategoryService.Object;

            Func<Task> act = async () => await categoryService.Remove(invalidCategoryId);

            await Assert.ThrowsAsync<InvalidOperationException>(act);
        }
    }
}
