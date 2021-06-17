using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var brands = new List<Brand>()
            {
                new Brand{ID=1, Name="Cong ty AAA"},
                new Brand{ID=2, Name="Cong ty BBB"},
                new Brand{ID=3, Name="Cong ty CCC"}
            };

            var products = new List<Product>()
            {
                new Product(1,"Ban tra", 400, new string[]{"xam","xanh"}, 2),
                new Product(2,"Tranh treo", 400, new string[]{"vang","xanh"}, 1),
                new Product(3,"Den chum", 500, new string[]{"trang"}, 3),
                new Product(4,"Ban hoc", 200, new string[]{"trang","xanh"}, 1),
                new Product(5,"Tui da", 300, new string[]{"do","den","vang"}, 2),
                new Product(6,"Giuong ngu", 500, new string[]{"trang"}, 2),
                new Product(7,"Tu ao", 600, new string[]{"trang"}, 3),
            };

            //lay sp gia 400
            //
            var query = from p in products 
                        where p.Price == 400 
                        select p; 
                        
        }
    }
}
