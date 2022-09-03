using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;



namespace BestBuyBestPractices
{
    public class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string connString = config.GetConnectionString("DefaultConnection");
            
            IDbConnection conn = new MySqlConnection(connString);
            var repo = new DapperDepartmentRepository(conn);

            Console.WriteLine("Type a new Department name");

            var newDepartment = Console.ReadLine();

            repo.InsertDepartment(newDepartment);

            var departments = repo.GetAllDepartments();

            foreach(var department in departments) 
            {
                Console.WriteLine(department.Name);
            }

            var prepo = new DapperProductRepository(conn);

            Console.WriteLine("Enter new Prodcut Name");
            string newProductName = Console.ReadLine();

            Console.WriteLine("Enter new Prodcut Price");
            double newProductPrice = double.Parse(Console.ReadLine());

            Console.WriteLine("Enter new Prodcut Category ID");
            int newProductCategory = int.Parse(Console.ReadLine());
            
            prepo.CreateProduct(newProductName, newProductPrice, newProductCategory);

            var products = prepo.GetAllProducts();

            foreach(Product product in products)
            {
                Console.WriteLine(product.Name);
                Console.WriteLine(product.Price);
                Console.WriteLine(product.CategoryID);
            }

        }
    }
}
