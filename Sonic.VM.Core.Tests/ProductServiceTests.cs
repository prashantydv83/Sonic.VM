using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sonic.VM.Repository.Interface;
using Sonic.VM.Entities;
using Sonic.VM.Service;
using System.Collections.Generic;
using Moq;

namespace Sonic.VM.Core.Tests
{
    [TestClass]
    public class ProductServiceTests
    {
        IProductRepository productRepository;

        [TestInitialize]
        public void Setup()
        {
            List<Product> products = new List<Product> {
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
            productRepository = _productrepoMock.Object;
        }

        [TestMethod]
        public void Products_OnGetProducts_IsPopulated()
        {
            // Act
            var obj = productRepository.GetProducts();

            // Assert
            Assert.IsNotNull(obj);
            Assert.AreEqual(3, obj.Count);
        }

        [TestMethod]
        public void IsValid_OnIsProductInStock_OfValidQty()
        {
            //Arrange
            var obj = new ProductService();
            
            //Act
            var result = obj.IsProductInStock(1);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsNotValid_OnIsProductInStock_OfInvalidProduct()
        {
            //Arrange
            var obj = new ProductService();

            //Act
            var result = obj.IsProductInStock(11);

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void StockUpdated_OnUpdateProductStock()
        {
            //Arrange
            var obj = new ProductService();
            Product prd = new Product { ProdId = 2, ProdName = "Orange", ProdPrice = 2.5, ProdQty = 3 };
            // Act
            var result = productRepository.UpdateProductStock(prd);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void StockNotUpdated_OnUpdateProductStock_WhereNoStock()
        {
            //
            Product prd = new Product { ProdId = 2, ProdName = "Orange", ProdPrice = 2.5, ProdQty = 0 };
            // Act
            var obj = productRepository.UpdateProductStock(prd);

            // Assert
            Assert.IsTrue(obj);
        }

        [TestMethod]
        public void StockUpdated_OnAddNewProductStock()
        {

            // Act
            //var obj = productRepository.AddNewProductStock(products);

            //// Assert
            //Assert.IsTrue(obj);
        }


    }
}
