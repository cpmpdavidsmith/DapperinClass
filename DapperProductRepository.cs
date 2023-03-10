using System;
using System.Data;
using System.Collections.Generic;
using Dapper;

namespace DapperInClass
{
	public class DapperProductRepository : IProductRepository
	{
        //field called _connection of type IDbConnection
        //the private message is an example oof "encapsulation" where every time an instance is made of line 7, a connection(with password!!) called 'IDbConnection' is run in 'constructor' line 11 and stored in field (which is unaccessable by the world) and is 'private" line 10
        private readonly IDbConnection _connection;                                                          
		public DapperProductRepository(IDbConnection connection)                                            
		{
            _connection = connection;
		}

        public void CreateProduct(string name, double price, int categoryID)
        {
            _connection.Execute("INSERT INTO products (Name, Price, CategoryID)" +
                "VALUES (@name, @price, @categoryID);"
                , new { name = name, price = price, categoryID = categoryID});
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _connection.Query<Product>("SELECT * FROM products;");
        }

        public void UpdateProductName(int  productID, string updatedName)
        {
            _connection.Execute("UPDATE products SET Name = @updatedName WHERE ProductID = @productID;",   
                new { updatedName = updatedName, productID = productID });
        }

        public void DeleteProduct(int productID)
        {
            _connection.Execute("DELETE FROM reviews WHERE ProductID = @productID;",
                new { productID = productID });

            _connection.Execute("DELETS FROM sale WHERE ProductID = @productID;",
                new { productID = productID });

            _connection.Execute("DELETE FROM products WHERE ProductID = @productID;",
                new { productID = productID });
        }

    }
}

