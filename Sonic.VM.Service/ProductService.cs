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
        private readonly IProductRepository productRepository;

        public ProductService()
        {
            productRepository = new ProductDataRepository();
        }

        public List<Product> GetProducts()
        {
            _logger.LogInfoMessage("{0}()", MethodBase.GetCurrentMethod().Name);

            try
            {
                return productRepository.GetProducts();
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
                products = productRepository.GetProducts();
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

        public bool UpdateProductStock(Product product)
        {
            _logger.LogInfoMessage("{0}()", MethodBase.GetCurrentMethod().Name);

            var products = new List<Product>();
            try
            {
                products = productRepository.GetProducts();
                var prd = products.First(i => i.ProdId == product.ProdId);
                prd.ProdQty = prd.ProdQty - 1;
                return productRepository.UpdateProductStock(prd);
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

            var products = new List<Product>();
            try
            {
                products = productRepository.GetProducts();
                for (int i = 0; i < newstockproducts.Count; i++)
                {
                    foreach (var z in products.FindAll(x => x.ProdId == newstockproducts[i].ProdId))
                    {
                        z.ProdQty = z.ProdQty + Convert.ToInt32(newstockproducts[i].ProdQty);
                    }
                }
                return productRepository.AddNewProductStock(newstockproducts);
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
