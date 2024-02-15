using Moq;
using ProductCatalog.Domain.Entities;
using ProductCatalog.Domain.Interfaces;

namespace ProductCatalog.Domain.Tests.Interfaces
{
    public class ProductRepositoryTests
    {
        List<Product> listProducts = new List<Product>
        {
            new Product(1, "Caderno Espiral", "Caderno espiral 100 fôlhas", 7.99m, 50, "caderno-1.png"),
            new Product(2, "Chaveiro Chip", "Chaveiro personalizado em formato chip", 30.90m, 30, "chaveiro.png"),
            new Product(3, "Borracha Escolar", "Borracha branca pequena", 3.25m, 80, "borracha.png")
        };

        [Fact(DisplayName = "GetProductsAsync - Return All Products")]
        public async Task ProductRepository_GetProductsAsync_ShouldReturnAllProducts()
        {
            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(repository => repository.GetProducts()).ReturnsAsync(listProducts);

            IProductRepository productRepository = productRepositoryMock.Object;

            var result = await productRepository.GetProducts();

            Assert.NotNull(result);
            Assert.Equal(listProducts.Count, result.Count());
            Assert.True(listProducts.SequenceEqual(result));
        }

        [Fact(DisplayName = "GetProductsAsync - Return Empty List When No Products Exist")]
        public async Task ProductRepository_GetProductsAsync_ShouldReturnEmptyListWhenNoProductsExist()
        {
            var products = new List<Product>();
            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(repository => repository.GetProducts()).ReturnsAsync(products);

            IProductRepository productRepository = productRepositoryMock.Object;

            var result = await productRepository.GetProducts();

            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact(DisplayName = "GetByIdAsync - Returns Product When Id exists")]
        public async Task ProductRepository_GetByIdAsync_ShouldReturnProductWhenIdExists()
        {
            var productId = 2;
            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(repository => repository.GetById(productId))
                .ReturnsAsync(listProducts.FirstOrDefault(p => p.Id == productId));

            IProductRepository productRepository = productRepositoryMock.Object;

            var result = await productRepository.GetById(productId);

            Assert.NotNull(result);
            Assert.Equal(listProducts[1], result);
        }

        [Fact(DisplayName = "GetByIdAsync - Returns Null When Product Id Does Not Exist")]
        public async Task ProductRepository_GetByIdAsync_ShouldReturnNullWhenProductIdDoesNotExist()
        {
            var productId = 4;
            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(repository => repository.GetById(productId)).ReturnsAsync((Product)null);

            IProductRepository productRepository = productRepositoryMock.Object;

            var result = await productRepository.GetById(productId);

            Assert.Null(result);
        }

        [Fact(DisplayName = "CreateAsync - Add And Return The Created Product")]
        public async Task ProductRepository_CreateAsync_ShouldInsertProductAndReturnCreatedProduct()
        {
            var newProduct = new Product("Calculadora Escolar", "Calculadora escolar simples", 40.0m, 400, "calculadora.png");
            var simulatedId = 4;

            var productRepositoryMock = new Mock<IProductRepository>();

            productRepositoryMock.Setup(repository => repository.Create(It.IsAny<Product>()))
            .ReturnsAsync((Product product) =>
            {
                product.GetType().GetProperty("Id").SetValue(product, simulatedId);
                simulatedId++;
                return product;
            });

            IProductRepository productRepository = productRepositoryMock.Object;

            var createdProduct = await productRepository.Create(newProduct);

            Assert.NotNull(createdProduct);
            Assert.Equal("Calculadora Escolar", createdProduct.Name);
            Assert.Equal(4, createdProduct.Id);
        }

        [Fact(DisplayName = "CreateAsync - Returns Null When Insert Fails")]
        public async Task ProductRepository_CreateAsync_Should_ShouldReturnNullWhenInsertProductFails()
        {
            var newProduct = new Product("Calculadora Escolar", "Calculadora escolar simples", 40.0m, 400, "calculadora.png");
            var productRepositoryMock = new Mock<IProductRepository>();

            productRepositoryMock.Setup(repository => repository.Create(It.IsAny<Product>())).ReturnsAsync((Product)null);

            IProductRepository productRepository = productRepositoryMock.Object;

            var createdProduct = await productRepository.Create(newProduct);

            Assert.Null(createdProduct);
        }

        [Fact(DisplayName = "UpdateAsync - Return Updated Product")]
        public async Task ProductRepository_UpdateAsync_ShouldUpdateReturnUpdatedProduct()
        {
            var existingProduct = new Product(2, "Chaveiro Chip", "Chaveiro personalizado em formato chip", 30.90m, 30, "chaveiro.png");
            var updatedProduct = new Product(2, "Chaveiro Chip 2", "Chaveiro personalizado formato chip", 32.90m, 35, "chaveiro.png");

            var productRepositoryMock = new Mock<IProductRepository>();

            productRepositoryMock.Setup(repository => repository.Update(It.IsAny<Product>()))
                                  .ReturnsAsync((Product product) => { return product; });

            IProductRepository productRepository = productRepositoryMock.Object;

            var updatedProductResult = await productRepository.Update(updatedProduct);

            Assert.NotNull(updatedProductResult);
            Assert.Equal("Chaveiro Chip 2", updatedProductResult.Name);
            Assert.Equal("Chaveiro personalizado formato chip", updatedProductResult.Description);
            Assert.Equal(32.90m, updatedProductResult.Price);
            Assert.Equal(35, updatedProductResult.Stock);
            Assert.Equal("chaveiro.png", updatedProductResult.Image);
        }

        [Fact(DisplayName = "UpdateAsync - Returns Null When Product Not Found")]
        public async Task ProductRepository_Update_ShouldReturnNullWhenProductNotFound()
        {
            var nonExistingProduct = new Product(100, "No Existing Product", "Description", 10.0m, 100, "image.jpg");
            var productRepositoryMock = new Mock<IProductRepository>();

            productRepositoryMock.Setup(repository => repository.Update(It.IsAny<Product>())).ReturnsAsync((Product)null);

            IProductRepository productRepository = productRepositoryMock.Object;

            var updatedProductResult = await productRepository.Update(nonExistingProduct);

            Assert.Null(updatedProductResult);
        }

        [Fact(DisplayName = "RemoveAsync - Returns The Existing Product Delete")]
        public async Task ProductRepository_RemoveAsync_ShouldRemoveProductAndReturnRemovedProduct()
        {
            var existingProduct = new Product(3, "Borracha Escolar", "Borracha branca pequena", 3.25m, 80, "borracha.png");
            var productRepositoryMock = new Mock<IProductRepository>();

            productRepositoryMock.Setup(repository => repository.Remove(It.IsAny<Product>()))
                                .ReturnsAsync((Product product) => { return product; }).Verifiable();

            IProductRepository productRepository = productRepositoryMock.Object;

            var removedProductResult = await productRepository.Remove(existingProduct);

            Assert.NotNull(removedProductResult);
            Assert.Equal("Borracha Escolar", removedProductResult.Name);
            Assert.Equal(3, removedProductResult.Id);

            productRepositoryMock.Verify(repository => repository.Remove(It.IsAny<Product>()), Times.Once());
        }

        [Fact(DisplayName = "RemoveAsync - Returns Null When Product Not Found")]
        public async Task ProductRepository_RemoveAsync_ShouldReturnNullWhenProductNotFound()
        {
            var nonExistingProduct = new Product(100, "No Existing Product", "Description", 10.0m, 100, "image.jpg");
            var productRepositoryMock = new Mock<IProductRepository>();

            productRepositoryMock.Setup(repository => repository.Remove(It.IsAny<Product>())).ReturnsAsync((Product)null);

            IProductRepository productRepository = productRepositoryMock.Object;

            var removedProductResult = await productRepository.Remove(nonExistingProduct);

            Assert.Null(removedProductResult);
        }
    }
}
