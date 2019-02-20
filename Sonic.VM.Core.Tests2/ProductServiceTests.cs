using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sonic.VM.Repository.Interface;
using Sonic.VM.Entities;
using Sonic.VM.Service;
using System.Collections.Generic;
using Moq;

namespace Sonic.VM.Core.Tests2
{
    [TestClass]
    public class ProductServiceTests
    {
        IProductRepository productRepository;
        ProductService productService;
        List<Product> products;

        [TestInitialize]
        public void Setup()
        {
            products = new List<Product> {
            new Product{ ProdId = 1, ProdName= "Mango", ProdPrice= 2.5,ProdQty = 3 },
            new Product{ ProdId = 2, ProdName= "Orange", ProdPrice= 2.5 ,ProdQty = 3},
            new Product{ ProdId = 3, ProdName= "PineApple", ProdPrice= 2.5,ProdQty = 3 }
            };

            Product product = new Product { ProdId = 2, ProdName = "Orange", ProdPrice = 2.5, ProdQty = 3 };

            var _productrepoMock = new Mock<IProductRepository>();
            _productrepoMock.Setup(r => r.GetProducts())
                .Returns(products);            
            _productrepoMock.Setup(r => r.UpdateProductStock(It.IsAny<Product>()))
               .Returns(true);
            _productrepoMock.Setup(r => r.AddNewProductStock(It.IsAny<List<Product>>()))
             .Returns(true);
            productRepository = _productrepoMock.Object;
            productService = new ProductService(_productrepoMock.Object);
        }

        [TestMethod]
        public void Products_OnGetProducts_IsPopulated()
        {
            // Act
            var result = productService.GetProducts();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
        }

        [TestMethod]
        public void IsValid_OnIsProductInStock_OfValidQty()
        {
            //Arrange
            
            //Act
            var result = productService.IsProductInStock(3);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsNotValid_OnIsProductInStock_OfInvalidProduct()
        {
            //Act
            var result = productService.IsProductInStock(11);

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void StockUpdated_OnUpdateProductStock()
        {
            //Arrange
            // Act
            var result = productService.UpdateProductStock(2);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void StockNotUpdated_OnUpdateProductStock_WhereNoStock()
        {
           // Act
            var obj = productService.UpdateProductStock(5);

            // Assert
            Assert.IsFalse(obj);
        }

        [TestMethod]
        public void StockUpdated_OnAddNewProductStock()
        {

            // Act
            var obj = productService.AddNewProductStock(products);

            //// Assert
            Assert.IsTrue(obj);
        }
    }
}
