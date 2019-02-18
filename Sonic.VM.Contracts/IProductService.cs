using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sonic.VM.Entities;

namespace Sonic.VM.Contracts
{
    public interface IProductService
    {
        /// <summary>
        /// Get all products available in Inventory.
        /// </summary>
        /// <returns>List of All Products</returns>
        List<Product> GetProducts();

        /// <summary>
        /// Get product stock by id.
        /// </summary>
        /// <param name="prodId">prodId</param>
        /// <returns>whether selected product is in stock or not</returns>
        bool IsProductInStock(int prodId);

        /// <summary>
        /// Update stock by reducing one qty of selected product
        /// </summary>
        /// <param name="prodId">prodId</param>
        /// <returns>whether selected product stock is updated or not</returns>
        bool UpdateProductStock(Product product);

        /// <summary>
        /// Add new stock.
        /// </summary>
        /// <param name="newstockproducts">newstockproducts</param>
        /// <returns>whether product stock is updated or not</returns>
        bool AddNewProductStock(List<Product> newstockproducts);
    }
}
