using Moq;
using ProductCatalog.Application.DTOs;
using ProductCatalog.Application.Interfaces;

namespace ProductCatalog.Application.Tests.Interfaces
{
    public class ProductServiceTest
    {
        List<ProductDTO> listProducts = new List<ProductDTO>
        {
            new ProductDTO
            {
                Id = 1,
                Name = "Caderno Espiral",
                Description = "Caderno espiral com 100 fôlhas",
                Price = 7.99m,
                Stock = 50,
                Image = "caderno-1.png",
                CategoryId = 1
            },
            new ProductDTO
            {
                Id = 2,
                Name = "Calculadora escolar",
                Description = "Calculadora simples",
                Price = 15.39m,
                Stock = 20,
                Image = "calculadora.png",
                CategoryId = 2
            },
            new ProductDTO
            {
                Id = 3,
                Name = "Chaveiro Chip",
                Description = "Chaveiro personalizado em formato chip",
                Price = 30.90M,
                Stock = 30,
                Image = "chaveiro.png",
                CategoryId = 3
            },
        };

        [Fact(DisplayName = "GetProducts - Return All Products")]
        public async Task ProductService_GetProducts_ShouldReturnAllProducts()
        {
            var mockProductService = new Mock<IProductService>();

            mockProductService.Setup(service => service.GetProducts()).ReturnsAsync(listProducts);

            var productService = mockProductService.Object;

            var result = await productService.GetProducts();

            Assert.NotNull(result);
            Assert.Equal(listProducts.Count, result.Count());
        }

        [Fact(DisplayName = "GetProducts - Return Empty List When No Products Exist")]
        public async Task ProductService_GetProducts_ShouldReturnEmptyListWhenNoProductsExist()
        {
            var mockProductService = new Mock<IProductService>();
            var emptyProducts = new List<ProductDTO>();

            mockProductService.Setup(service => service.GetProducts()).ReturnsAsync(emptyProducts);

            var productService = mockProductService.Object;

            var result = await productService.GetProducts();

            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact(DisplayName = "GetById - Returns ProductDTO When Id exists")]
        public async Task ProductService_GetById_ShouldReturnProductDTOWhenIdExists()
        {
            var mockProductService = new Mock<IProductService>();
            var productId = 1;
            var productDto = new ProductDTO
            {
                Id = 1,
                Name = "Caderno Espiral",
                Description = "Caderno espiral com 100 fôlhas",
                Price = 7.99m,
                Stock = 50,
                Image = "caderno-1.png",
                CategoryId = 1
            };

            mockProductService.Setup(service => service.GetById(productId)).ReturnsAsync(productDto);

            var productService = mockProductService.Object;

            var result = await productService.GetById(productId);

            Assert.NotNull(result);
            Assert.Equal(productId, result.Id);
        }

        [Fact(DisplayName = "GetById - Returns null when id does not exist")]
        public async Task ProductService_GetById_ShouldReturnNullWhenProductDTOIdDoesNotExist()
        {
            var mockProductService = new Mock<IProductService>();
            var invalidProductId = 9999;

            mockProductService.Setup(service => service.GetById(invalidProductId)).ReturnsAsync((ProductDTO)null);

            var productService = mockProductService.Object;

            var result = await productService.GetById(invalidProductId);

            Assert.Null(result);
        }

        [Fact(DisplayName = "Add - Return The Added ProductDTO")]
        public async Task ProductService_Add_ShouldInsertProductAndReturnAddedProductDTO()
        {
            var mockProductService = new Mock<IProductService>();
            var productDto = new ProductDTO
            {
                Name = "Agenda",
                Description = "Agenda escolar",
                Price = 10.00M,
                Stock = 15,
                CategoryId = 1
            };

            mockProductService.Setup(service => service.Add(productDto)).Verifiable();

            var productService = mockProductService.Object;

            await productService.Add(productDto);

            mockProductService.Verify();
        }

        [Fact(DisplayName = "Add - Returns Null When Add Fails")]
        public async Task ProductService_Add_ShouldReturnNullWhenAddedProductDTOFails()
        {
            var mockProductService = new Mock<IProductService>();
            var invalidProductDto = new ProductDTO
            {
                Description = "Descrição invalida",
                Price = 0M,
                Stock = 50,
                CategoryId = 2
            };

            mockProductService.Setup(service => service.Add(invalidProductDto)).ThrowsAsync(new ArgumentException("Invalid product"));

            var productService = mockProductService.Object;

            Func<Task> act = async () => await productService.Add(invalidProductDto);

            await Assert.ThrowsAsync<ArgumentException>(act);
            mockProductService.Verify(service => service.Add(invalidProductDto), Times.Once);
        }

        [Fact(DisplayName = "Update - Return Updated CategoryDTO")]
        public async Task ProductService_Update_ShouldUpdateReturnUpdatedProductDTO()
        {
            var mockProductService = new Mock<IProductService>();
            var updatedProductDto = new ProductDTO
            {
                Id = 1,
                Name = "Caderno Espiral",
                Description = "Caderno espiral com 100 fôlhas",
                Price = 8.99m,
                Stock = 70,
                Image = "caderno-1.png",
                CategoryId = 1
            };

            mockProductService.Setup(service => service.Update(updatedProductDto)).Verifiable();

            var productService = mockProductService.Object;

            await productService.Update(updatedProductDto);

            mockProductService.Verify();
        }

        [Fact(DisplayName = "Update - Returns Null When ProductDTO Not Found")]
        public async Task ProductService_Update_ShouldReturnNullWhenProductDTONotFound()
        {
            var mockProductService = new Mock<IProductService>();
            var invalidProductDto = new ProductDTO
            {
                Id = 1,
                Name = null,
                Description = "Caderno espiral com 100 fôlhas",
                Price = 8.99m,
                Stock = 70,
                Image = "caderno-1.png",
                CategoryId = 1
            };

            mockProductService.Setup(service => service.Update(invalidProductDto))
                .ThrowsAsync(new ArgumentException("Invalid product update"));

            var productService = mockProductService.Object;

            Func<Task> act = async () => await productService.Update(invalidProductDto);

            await Assert.ThrowsAsync<ArgumentException>(act);
            mockProductService.Verify(service => service.Update(invalidProductDto), Times.Once);
        }


        [Fact(DisplayName = "Remove - Returns The Existing ProductDTO Delete")]
        public async Task ProductService_Remove_ShouldRemoveProductAndReturnRemovedProductDTO()
        {
            var mockProductService = new Mock<IProductService>();
            var productId = 1;

            mockProductService.Setup(service => service.Remove(productId)).Verifiable();

            var productService = mockProductService.Object;

            await productService.Remove(productId);

            mockProductService.Verify();
        }

        [Fact(DisplayName = "Remove - Returns Null When ProductDTO Not Found")]
        public async Task ProductService_Remove_ShouldReturnNullWhenProductDTONotFound()
        {
            var mockProductService = new Mock<IProductService>();
            var invalidProductId = 9999;

            mockProductService.Setup(service => service.Remove(invalidProductId))
                .ThrowsAsync(new InvalidOperationException("Invalid product removal"));

            var productService = mockProductService.Object;

            Func<Task> act = async () => await productService.Remove(invalidProductId);

            await Assert.ThrowsAsync<InvalidOperationException>(act);
        }
    }
}
