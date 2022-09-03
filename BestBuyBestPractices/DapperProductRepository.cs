using Dapper;
using System.Collections.Generic;
using System.Data;

namespace BestBuyBestPractices
{
    public class DapperProductRepository : IProductRepository
    {
        private readonly IDbConnection _connection;
        public string name { get; set; }
        public double  price { get; set; }
        public int categoryID { get; set; }
        public DapperProductRepository(IDbConnection connection)
        {
            _connection = connection;
        }
        public void CreateProduct(string name, double price, int categoryID)
        {
            var sqlQuery = ("INSERT INTO PRODUCTS (Name, Price, CategoryID) VALUES (@productName, @productPrice, @productCategoryID);");
            _connection.Execute(sqlQuery,
                new
                {
                    productName = name,
                    productPrice = price,
                    productCategoryID = categoryID
                }
                );
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _connection.Query<Product>("SELECT * FROM Products;");
        }
    }
}
