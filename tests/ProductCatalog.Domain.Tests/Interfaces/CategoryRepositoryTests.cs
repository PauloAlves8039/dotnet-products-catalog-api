using Moq;
using ProductCatalog.Domain.Entities;
using ProductCatalog.Domain.Interfaces;

namespace ProductCatalog.Domain.Tests.Interfaces
{
    public class CategoryRepositoryTests
    {
        List<Category> listCategories = new List<Category>
        {
            new Category(1, "Material Escola"),
            new Category(2, "Eletrônicos"),
            new Category(3, "Acessórios")
        };

        [Fact(DisplayName = "GetCategories - Return All Categories")]
        public async Task CategoryRepository_GetCategories_ShouldReturnAllCategories()
        {
            var categoryRepositoryMock = new Mock<ICategoryRepository>();
            categoryRepositoryMock.Setup(repository => repository.GetCategories()).ReturnsAsync(listCategories);

            ICategoryRepository categoryRepository = categoryRepositoryMock.Object;

            var result = await categoryRepository.GetCategories();

            Assert.NotNull(result);
            Assert.Equal(listCategories.Count, result.Count());
            Assert.True(listCategories.SequenceEqual(result));
        }

        [Fact(DisplayName = "GetCategories - Return Empty List When No Categories Exist")]
        public async Task CategoryRepository_GetCategories_ShouldReturnEmptyListWhenNoCategoriesExist()
        {
            var emptyCategoriesList = new List<Category>();
            var categoryRepositoryMock = new Mock<ICategoryRepository>();

            categoryRepositoryMock.Setup(repository => repository.GetCategories()).ReturnsAsync(emptyCategoriesList);

            ICategoryRepository categoryRepository = categoryRepositoryMock.Object;

            var result = await categoryRepository.GetCategories();

            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact(DisplayName = "GetById - Returns Category When Id exists")]
        public async Task CategoryRepository_GetById_ShouldReturnCategoryWhenIdExists()
        {
            var categoryRepositoryMock = new Mock<ICategoryRepository>();
            categoryRepositoryMock.Setup(repository => repository.GetById(It.IsAny<int?>()))
                                  .ReturnsAsync((int? id) => listCategories.Find(c => c.Id == id));

            ICategoryRepository categoryRepository = categoryRepositoryMock.Object;

            var result = await categoryRepository.GetById(2);

            Assert.NotNull(result);
            Assert.Equal("Eletrônicos", result.Name);
        }

        [Fact(DisplayName = "GetById - Returns null when id does not exist")]
        public async Task CategoryRepository_GetById_ShouldReturnNullWhenCategoryIdDoesNotExist()
        {
            var categoryRepositoryMock = new Mock<ICategoryRepository>();

            categoryRepositoryMock.Setup(repository => repository.GetById(It.IsAny<int?>()))
                                  .ReturnsAsync((int? id) => listCategories.Find(c => c.Id == id));

            ICategoryRepository categoryRepository = categoryRepositoryMock.Object;

            var result = await categoryRepository.GetById(4);

            Assert.Null(result);
        }

        [Fact(DisplayName = "Create - Add And Return The Created Category")]
        public async Task CategoryRepository_Create_ShouldInsertCategoryAndReturnCreatedCategory()
        {
            var newCategoryName = "Livros";
            var newCategory = new Category(newCategoryName);
            var simulatedId = 1;

            var categoryRepositoryMock = new Mock<ICategoryRepository>();
            categoryRepositoryMock.Setup(repository => repository.Create(It.IsAny<Category>()))
            .ReturnsAsync((Category category) =>
            {
                category.GetType().GetProperty("Id").SetValue(category, simulatedId);
                simulatedId++;
                return category;
            });

            ICategoryRepository categoryRepository = categoryRepositoryMock.Object;

            var createdCategory = await categoryRepository.Create(newCategory);

            Assert.NotNull(createdCategory);
            Assert.Equal(newCategoryName, createdCategory.Name);
            Assert.Equal(1, createdCategory.Id);
        }

        [Fact(DisplayName = "Create - Returns Null When Insert Fails")]
        public async Task CategoryRepository_Create_ShouldReturnNullWhenInsertCategoryFails()
        {

            var newCategoryName = "Livros";
            var newCategory = new Category(newCategoryName);
            var categoryRepositoryMock = new Mock<ICategoryRepository>();

            categoryRepositoryMock.Setup(repository => repository.Create(It.IsAny<Category>())).ReturnsAsync((Category)null);

            ICategoryRepository categoryRepository = categoryRepositoryMock.Object;

            var createdCategory = await categoryRepository.Create(newCategory);

            Assert.Null(createdCategory);
        }

        [Fact(DisplayName = "Update - Return Updated Category")]
        public async Task CategoryRepository_Update_ShouldUpdateReturnUpdatedCategory()
        {
            var existingCategory = new Category(1, "Material Escola");
            var updatedCategory = new Category(1, "Materiais Escolares");
            var categoryRepositoryMock = new Mock<ICategoryRepository>();

            categoryRepositoryMock.Setup(repository => repository.Update(It.IsAny<Category>()))
                .ReturnsAsync((Category category) => { return category; });

            ICategoryRepository categoryRepository = categoryRepositoryMock.Object;

            var updatedCategoryResult = await categoryRepository.Update(updatedCategory);

            Assert.NotNull(updatedCategoryResult);
            Assert.Equal("Materiais Escolares", updatedCategoryResult.Name);
        }

        [Fact(DisplayName = "Update - Returns Null When Category Not Found")]
        public async Task CategoryRepository_Update_ShouldReturnNullWhenCategoryNotFound()
        {
            var noExistingCategory = new Category(100, "No Existing Category");

            var categoryRepositoryMock = new Mock<ICategoryRepository>();
            categoryRepositoryMock.Setup(repository => repository.Update(It.IsAny<Category>())).ReturnsAsync((Category)null);

            ICategoryRepository categoryRepository = categoryRepositoryMock.Object;

            var updatedCategoryResult = await categoryRepository.Update(noExistingCategory);

            Assert.Null(updatedCategoryResult);
        }

        [Fact(DisplayName = "Remove - Returns The Existing Category Delete")]
        public async Task CategoryRepository_Remove_ShouldRemoveCategoryAndReturnRemovedCategory()
        {
            var existingCategory = new Category(1, "Material Escola");
            var categoryRepositoryMock = new Mock<ICategoryRepository>();

            categoryRepositoryMock.Setup(repository => repository.Remove(It.IsAny<Category>()))
                .ReturnsAsync((Category category) => { return category; });

            ICategoryRepository categoryRepository = categoryRepositoryMock.Object;

            var removedCategoryResult = await categoryRepository.Remove(existingCategory);

            Assert.NotNull(removedCategoryResult);
            Assert.Equal("Material Escola", removedCategoryResult.Name);
            Assert.Equal(1, removedCategoryResult.Id);
        }


        [Fact(DisplayName = "Remove - Returns Null When Category Not Found")]
        public async Task CategoryRepository_Remove_ShouldReturnNullWhenCategoryNotFound()
        {
            var noExistingCategory = new Category(100, "No Existing Category");
            var categoryRepositoryMock = new Mock<ICategoryRepository>();

            categoryRepositoryMock.Setup(repository => repository.Remove(It.IsAny<Category>())).ReturnsAsync((Category)null);

            ICategoryRepository categoryRepository = categoryRepositoryMock.Object;

            var removedCategoryResult = await categoryRepository.Remove(noExistingCategory);

            Assert.Null(removedCategoryResult);
        }
    }
}
