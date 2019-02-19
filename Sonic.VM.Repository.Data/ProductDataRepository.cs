using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sonic.VM.Entities;
using Sonic.VM.Repository.Interface;

namespace Sonic.VM.Repository.Data
{
    public class ProductDataRepository : IProductRepository
    {
        List<Product> products = new List<Product> {
            new Product{ ProdId = 1, ProdName= "Mango", ProdPrice= 2.5,ProdQty = 5 },
            new Product{ ProdId = 2, ProdName= "Orange", ProdPrice= 2.5,ProdQty = 5 },
            new Product{ ProdId = 3, ProdName= "PineApple", ProdPrice= 2.5,ProdQty = 5 },
            new Product{ ProdId = 4, ProdName= "Cranberry", ProdPrice= 2.5 ,ProdQty = 5},
            new Product{ ProdId = 5, ProdName= "Raspberry", ProdPrice= 2.5,ProdQty = 5 },
            new Product{ ProdId = 6, ProdName= "Cola", ProdPrice= 2.5,ProdQty = 5 },
            new Product{ ProdId = 7, ProdName= "Mixed", ProdPrice= 2.5,ProdQty = 5 },
            new Product{ ProdId = 8, ProdName= "Strawberry", ProdPrice= 2.5 ,ProdQty = 5},
            new Product{ ProdId = 9, ProdName= "Banana", ProdPrice= 2.5 ,ProdQty = 5},
            new Product{ ProdId = 10, ProdName= "Apple", ProdPrice= 2.5,ProdQty = 0 }
        };

        public bool AddNewProductStock(List<Product> newstockproducts)
        {
            for (int i = 0; i < newstockproducts.Count; i++)
            {
                foreach (var z in products.FindAll(x => x.ProdId == newstockproducts[i].ProdId))
                {
                    z.ProdQty = Convert.ToInt32(newstockproducts[i].ProdQty);
                }
            }
            return true;
        }

        public List<Product> GetProducts()
        {
            return products;
        }

        public bool UpdateProductStock(Product product)
        {
            var prd = products.First(i => i.ProdId == product.ProdId);
            prd.ProdQty = product.ProdQty;
            return true;
        }
    }
}
