using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sonic.VM.Entities;
using Sonic.VM.Contracts;
using Sonic.VM.Logging;
using System.Reflection;
using Sonic.VM.Repository.Data;
using Sonic.VM.Repository.Interface;

namespace Sonic.VM.Service
{
    public class ProductService : IProductService
    {
        private readonly ILogger _logger = Logger.MyLogger;
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository?? new ProductDataRepository();
        }

        public List<Product> GetProducts()
        {
            _logger.LogInfoMessage("{0}()", MethodBase.GetCurrentMethod().Name);

            try
            {
                return _productRepository.GetProducts();
            }
            catch (Exception ex)
            {
                _logger.LogInfoMessage("Exception: {0}", ex.Message);
                if (ex.InnerException != null) _logger.LogInfoMessage("Inner Exception: {0}", ex.InnerException.Message);
                throw new Exception();
            }
        }

        public bool IsProductInStock(int prodId)
        {
            _logger.LogInfoMessage("{0}()", MethodBase.GetCurrentMethod().Name);

            var products = new List<Product>();

            try
            {
                products = _productRepository.GetProducts();
                var product = products.FirstOrDefault(i => i.ProdId == prodId);
                if (product.ProdQty > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                _logger.LogInfoMessage("Exception: {0}", ex.Message);
                if (ex.InnerException != null) _logger.LogInfoMessage("Inner Exception: {0}", ex.InnerException.Message);
                return false;
            }
        }

        public bool UpdateProductStock(int productId)
        {
            _logger.LogInfoMessage("{0}()", MethodBase.GetCurrentMethod().Name);

            var products = new List<Product>();
            try
            {
                products = _productRepository.GetProducts();
                var prd = products.First(i => i.ProdId == productId);
                prd.ProdQty = prd.ProdQty - 1;
                return _productRepository.UpdateProductStock(prd);
            }
            catch (Exception ex)
            {
                _logger.LogInfoMessage("Exception: {0}", ex.Message);
                if (ex.InnerException != null) _logger.LogInfoMessage("Inner Exception: {0}", ex.InnerException.Message);
                return false;
            }
        }

        public bool AddNewProductStock(List<Product> newstockproducts)
        {
            _logger.LogInfoMessage("{0}()", MethodBase.GetCurrentMethod().Name);

            try
            {
                return _productRepository.AddNewProductStock(newstockproducts);
            }
            catch (Exception ex)
            {
                _logger.LogInfoMessage("Exception: {0}", ex.Message);
                if (ex.InnerException != null) _logger.LogInfoMessage("Inner Exception: {0}", ex.InnerException.Message);
                return false;
            }
        }
    }
}
