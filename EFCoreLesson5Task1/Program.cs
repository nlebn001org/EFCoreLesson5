using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreLesson5Task1
{
    internal class Program
    {
        static int prodID;


        static async Task Main(string[] args)
        {
            //Task1
            List<Product> products = await GetAllProductsSql(new ShopDBContext());
            foreach (Product p in products)
            {
                Console.WriteLine($"{p.ProdId}, {(p.Description).Trim()}, {p.UnitPrice}, {p.Weight}");
            }

            //Task2
            //Console.WriteLine("Choose ID of your product");
            //bool boo = int.TryParse(Console.ReadLine(), out prodID);

            //Product prod = await GetProductAtIDAsync(new ShopDBContext(), prodID);
            //Console.WriteLine($"{prod.ProdId}, {(prod.Description).Trim()}, {prod.Weight}");
        }

        static async Task<Product> GetProductAtIDAsync(ShopDBContext db, int prodId)
        {
            using (db)
            {
                return await Task<Product>.Run(() =>
                {
                    Product prod = db.Products.FromSqlRaw("exec ShowProductsAtID @p0", new SqlParameter("@p0", prodId))
                .AsEnumerable().FirstOrDefault<Product>();
                    return prod;
                });
            }
        }

        static async Task<List<Product>> GetAllProductsSql(ShopDBContext db)
        {
            return await Task<Product>.Run(() =>
            {
                List<Product> products = db.Products.FromSqlRaw("select*from Products").ToList();
                return products;
            });
        }


    }
}
