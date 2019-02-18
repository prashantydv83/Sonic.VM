using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sonic.VM.Entities;

namespace Sonic.VM.Repository.Interface
{
    public interface IProductRepository
    {
        List<Product> GetProducts();
        bool UpdateProductStock(Product product);
        bool AddNewProductStock(List<Product> newstockproducts);
    }
}
