using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ProductCatalog.Application.DTOs;
using ProductCatalog.Application.Interfaces;
using ProductCatalog.WebAPI.Controllers;

namespace ProductCatalog.WebAPI.Tests.Controllers
{
    public class CategoryControllerTests
    {

        List<CategoryDTO> listCategories = new List<CategoryDTO>
        {
            new CategoryDTO { Id = 1, Name = "Material Escolar" },
            new CategoryDTO { Id = 2, Name = "Eletrônicos" },
            new CategoryDTO { Id = 3, Name = "Acessórios" },
        };

        [Fact(DisplayName = "GetCategories - Return All Categories")]
        public async Task CategoryController_GetCategories_ShouldReturnAllCategories()
        {
            var mockCategoryService = new Mock<ICategoryService>();

            mockCategoryService.Setup(service => service.GetCategories()).ReturnsAsync(listCategories);

            var controller = new CategoryController(mockCategoryService.Object);

            var result = await controller.GetCategories();

            var resultOk = Assert.IsType<OkObjectResult>(result);
            var categories = Assert.IsAssignableFrom<IEnumerable<CategoryDTO>>(resultOk.Value);

            Assert.Equal(listCategories, categories);
        }

        [Fact(DisplayName = "GetCategories - Return Empty List When No Categories Exist")]
        public async Task CategoryController_GetCategories_ShouldReturnEmptyListWhenNoCategoriesExist()
        {
            var mockCategoryService = new Mock<ICategoryService>();

            mockCategoryService.Setup(service => service.GetCategories()).ReturnsAsync(new List<CategoryDTO>());

            var controller = new CategoryController(mockCategoryService.Object);

            var result = await controller.GetCategories();

            var resultOk = Assert.IsType<OkObjectResult>(result);
            var categories = Assert.IsAssignableFrom<IEnumerable<CategoryDTO>>(resultOk.Value);

            Assert.Empty(categories);
        }

        [Fact(DisplayName = "GetCategory - Returns Existing Category By Id")]
        public async Task CategoryController_GetCategory_ShouldReturnCategoryById()
        {
            var categoryId = 1;
            var mockCategoryService = new Mock<ICategoryService>();

            var category = new CategoryDTO { Id = categoryId, Name = "Material Escolar" };

            mockCategoryService.Setup(service => service.GetById(categoryId)).ReturnsAsync(category);

            var controller = new CategoryController(mockCategoryService.Object);

            var result = await controller.GetCategory(categoryId);

            var resultOk = Assert.IsType<OkObjectResult>(result);
            var resultCategory = Assert.IsType<CategoryDTO>(resultOk.Value);

            Assert.Equal(category, resultCategory);
        }

        [Fact(DisplayName = "GetCategory - Returns Nonexistent Category By Id")]
        public async Task CategoryController_GetCategory_ShouldReturnNotFoundForNonexistentCategory()
        {
            var categoryId = 0;
            var mockCategoryService = new Mock<ICategoryService>();

            mockCategoryService.Setup(service => service.GetById(categoryId)).ReturnsAsync(null as CategoryDTO);

            var controller = new CategoryController(mockCategoryService.Object);

            var result = await controller.GetCategory(categoryId);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact(DisplayName = "CreateCategory - Return To Created Category")]
        public async Task CategoryController_CreateCategory_ShouldReturnCreatedCategory()
        {
            var mockCategoryService = new Mock<ICategoryService>();

            var newCategoryDto = new CategoryDTO { Name = "Nova Categoria" };

            mockCategoryService.Setup(service => service.Add(newCategoryDto)).Returns(Task.CompletedTask);

            var controller = new CategoryController(mockCategoryService.Object);

            var result = await controller.CreateCategory(newCategoryDto);

            var createdResult = Assert.IsType<CreatedAtActionResult>(result);

            Assert.Equal(nameof(CategoryController.GetCategory), createdResult.ActionName);

            var resultCategory = Assert.IsType<CategoryDTO>(createdResult.Value);
            Assert.Equal(newCategoryDto, resultCategory);
        }

        [Fact(DisplayName = "UpdateCategory - Return To Update Category")]
        public async Task CategoryController_UpdateCategory_ShouldReturnUpdatedCategory()
        {
            var mockCategoryService = new Mock<ICategoryService>();
            var categoryId = 1;
            var updatedCategoryDto = new CategoryDTO { Id = categoryId, Name = "Categoria Atualizada" };

            mockCategoryService.Setup(service => service.Update(updatedCategoryDto)).Returns(Task.CompletedTask);

            var controller = new CategoryController(mockCategoryService.Object);

            var result = await controller.UpdateCategory(categoryId, updatedCategoryDto);

            var noContentResult = Assert.IsType<NoContentResult>(result);
            Assert.Equal(StatusCodes.Status204NoContent, noContentResult.StatusCode);

            mockCategoryService.Verify(service => service.Update(updatedCategoryDto), Times.Once);
        }

        [Fact(DisplayName = "UpdateCategory - Return Failed To Update Category")]
        public async Task CategoryController_UpdateCategory_ShouldReturnFailedUpdatedCategory()
        {
            var mockCategoryService = new Mock<ICategoryService>();
            var categoryId = 1;
            var categoryIdIncompatible = 9999;
            var categoryDto = new CategoryDTO { Id = categoryIdIncompatible, Name = "Categoria Atualizada" };

            var controller = new CategoryController(mockCategoryService.Object);

            var result = await controller.UpdateCategory(categoryId, categoryDto);

            var badRequestResult = Assert.IsType<BadRequestResult>(result);
            Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);

            mockCategoryService.Verify(service => service.Update(It.IsAny<CategoryDTO>()), Times.Never);
        }

        [Fact(DisplayName = "DeleteCategory - Return To Deleted Category")]
        public async Task CategoryController_DeleteCategory_ShouldReturnDeletedCategory()
        {
            var mockCategoryService = new Mock<ICategoryService>();
            var categoryId = 1;

            mockCategoryService.Setup(service => service.GetById(categoryId)).ReturnsAsync(new CategoryDTO { Id = categoryId });
            mockCategoryService.Setup(service => service.Remove(categoryId)).Returns(Task.CompletedTask);

            var controller = new CategoryController(mockCategoryService.Object);

            var result = await controller.DeleteCategory(categoryId);

            var noContentResult = Assert.IsType<NoContentResult>(result);
            Assert.Equal(StatusCodes.Status204NoContent, noContentResult.StatusCode);

            mockCategoryService.Verify(service => service.Remove(categoryId), Times.Once);
        }

        [Fact(DisplayName = "DeleteCategory - Return Null To Deleted Category")]
        public async Task CategoryController_DeleteCategory_ShouldReturnNullDeletedCategory()
        {
            var mockCategoryService = new Mock<ICategoryService>();
            var categoryId = 1;

            mockCategoryService.Setup(service => service.GetById(categoryId)).ReturnsAsync((CategoryDTO)null);

            var controller = new CategoryController(mockCategoryService.Object);

            var result = await controller.DeleteCategory(categoryId);

            var notFoundResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);

            mockCategoryService.Verify(service => service.Remove(It.IsAny<int>()), Times.Never);
        }

    }
}
