using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ProductCatalog.Application.DTOs;
using ProductCatalog.Application.Interfaces;
using ProductCatalog.WebAPI.Controllers;

namespace ProductCatalog.WebAPI.Tests.Controllers
{
    public class ProductControllerTests
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
        public async Task ProductController_GetProducts_ShouldReturnAllProducts()
        {
            var mockProductService = new Mock<IProductService>();

            mockProductService.Setup(service => service.GetProducts()).ReturnsAsync(listProducts);

            var controller = new ProductController(mockProductService.Object);

            var result = await controller.GetProducts();

            var resultOk = Assert.IsType<OkObjectResult>(result);
            var products = Assert.IsAssignableFrom<IEnumerable<ProductDTO>>(resultOk.Value);

            Assert.Equal(listProducts, products);
        }

        [Fact(DisplayName = "GetProducts - Return Empty List When No Products Exist")]
        public async Task ProductController_GetProducts_ShouldReturnEmptyListWhenNoProductsExist()
        {
            var mockProductService = new Mock<IProductService>();

            mockProductService.Setup(service => service.GetProducts()).ReturnsAsync(new List<ProductDTO>());

            var controller = new ProductController(mockProductService.Object);

            var result = await controller.GetProducts();

            var resultOk = Assert.IsType<OkObjectResult>(result);
            var products = Assert.IsAssignableFrom<IEnumerable<ProductDTO>>(resultOk.Value);

            Assert.Empty(products);
        }

        [Fact(DisplayName = "GetProduct - Returns Existing Product By Id")]
        public async Task ProductController_GetProduct_ShouldReturnProductById()
        {
            var productId = 1;
            var mockProductService = new Mock<IProductService>();

            var product = new ProductDTO 
            { 
                Id = productId,
                Name = "Caderno Espiral",
                Description = "Caderno espiral com 100 fôlhas",
                Price = 7.99m,
                Stock = 50,
                Image = "caderno-1.png",
                CategoryId = 1
            };

            mockProductService.Setup(service => service.GetById(productId)).ReturnsAsync(product);

            var controller = new ProductController(mockProductService.Object);

            var result = await controller.GetProduct(productId);

            var resultOk = Assert.IsType<OkObjectResult>(result);
            var resultProduct = Assert.IsType<ProductDTO>(resultOk.Value);

            Assert.Equal(product, resultProduct);
        }

        [Fact(DisplayName = "GetProduct - Returns Nonexistent Product By Id")]
        public async Task ProductController_GetProduct_ShouldReturnNotFoundForNonexistentProduct()
        {
            var productId = 0;
            var mockProductService = new Mock<IProductService>();

            var product = new ProductDTO
            {
                Id = productId,
                Name = "Caderno Espiral",
                Description = "Caderno espiral com 100 fôlhas",
                Price = 7.99m,
                Stock = 50,
                Image = "caderno-1.png",
                CategoryId = 1
            };

            mockProductService.Setup(service => service.GetById(productId)).ReturnsAsync(null as ProductDTO);

            var controller = new ProductController(mockProductService.Object);

            var result = await controller.GetProduct(productId);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact(DisplayName = "CreateProduct - Return To Created Product")]
        public async Task ProductController_GetProduct_ShouldReturnCreatedProduct()
        {
            var mockProductService = new Mock<IProductService>();

            var newProductDto = new ProductDTO
            {
                Name = "Agenda",
                Description = "Agenda escolar",
                Price = 10.00M,
                Stock = 15,
                Image = "agenda.png",
                CategoryId = 1
            };

            mockProductService.Setup(service => service.Add(newProductDto)).Returns(Task.CompletedTask);

            var controller = new ProductController(mockProductService.Object);

            var result = await controller.CreateProduct(newProductDto);

            var createdResult = Assert.IsType<CreatedAtActionResult>(result);

            Assert.Equal(nameof(ProductController.GetProduct), createdResult.ActionName);

            var resultProduct = Assert.IsType<ProductDTO>(createdResult.Value);
            Assert.Equal(newProductDto, resultProduct);
        }

        [Fact(DisplayName = "UpdateProduct - Return To Update Product")]
        public async Task ProductController_UpdateProduct_ShouldReturnUpdatedProduct()
        {
            var mockProductService = new Mock<IProductService>();

            var productId = 1;
            var updatedProductDto = new ProductDTO 
            { 
                Id = productId, 
                Name = "Agenda Atualizada",
                Description = "Agenda escolar",
                Price = 12.00M,
                Stock = 18,
                Image = "agenda.png",
                CategoryId = 1
            };

            mockProductService.Setup(service => service.Update(updatedProductDto)).Returns(Task.CompletedTask);

            var controller = new ProductController(mockProductService.Object);

            var result = await controller.UpdateProduct(productId, updatedProductDto);

            var noContentResult = Assert.IsType<NoContentResult>(result);
            Assert.Equal(StatusCodes.Status204NoContent, noContentResult.StatusCode);

            mockProductService.Verify(service => service.Update(updatedProductDto), Times.Once);
        }

        [Fact(DisplayName = "UpdateProduct - Return Failed To Update Product")]
        public async Task ProductController_UpdateProduct_ShouldReturnFailedUpdatedProduct()
        {
            var mockProductService = new Mock<IProductService>();

            var productId = 1;
            var productIdIncompatible = 9999;
            var productDto = new ProductDTO 
            { 
                Id = productIdIncompatible,
                Name = "Agenda Atualizada",
                Description = "Agenda escolar",
                Price = 12.00M,
                Stock = 18,
                Image = "agenda.png",
                CategoryId = 1
            };

            var controller = new ProductController(mockProductService.Object);

            var result = await controller.UpdateProduct(productId, productDto);

            var badRequestResult = Assert.IsType<BadRequestResult>(result);
            Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);

            mockProductService.Verify(service => service.Update(It.IsAny<ProductDTO>()), Times.Never);
        }

        [Fact(DisplayName = "DeleteProduct - Return To Deleted Product")]
        public async Task ProductController_DeleteProduct_ShouldReturnDeletedProduct()
        {
            var mockProductService = new Mock<IProductService>();

            var productId = 1;
            mockProductService.Setup(service => service.GetById(productId)).ReturnsAsync(new ProductDTO 
            { 
                Id = productId,
                Description = "Agenda escolar",
                Price = 10.00M,
                Stock = 15,
                Image = "agenda.png",
                CategoryId = 1
            });

            mockProductService.Setup(service => service.Remove(productId)).Returns(Task.CompletedTask);

            var controller = new ProductController(mockProductService.Object);

            var result = await controller.DeleteProduct(productId);

            var noContentResult = Assert.IsType<NoContentResult>(result);
            Assert.Equal(StatusCodes.Status204NoContent, noContentResult.StatusCode);

            mockProductService.Verify(service => service.Remove(productId), Times.Once);
        }

        [Fact(DisplayName = "DeleteProduct - Return Null To Deleted Product")]
        public async Task ProductController_DeleteProduct_ShouldReturnNullDeletedProduct()
        {
            var mockProductService = new Mock<IProductService>();
            var productId = 1;

            mockProductService.Setup(service => service.GetById(productId)).ReturnsAsync((ProductDTO)null);

            var controller = new ProductController(mockProductService.Object);

            var result = await controller.DeleteProduct(productId);

            var notFoundResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);

            mockProductService.Verify(service => service.Remove(It.IsAny<int>()), Times.Never);
        }
    }
}
