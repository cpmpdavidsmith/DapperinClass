using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;


namespace DapperInClass
{
    class Program
    {
        //these 4 lines (14,15,16) will find our app settings file 

        //_________________________________________________________________
        static IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

        static string connString = config.GetConnectionString("DefaultConnection");
        //^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

        static IDbConnection conn = new MySqlConnection(connString);                            //create out IDbConnection that uses MySQL, so Dapper can extend it 

        static void Main(string[] args)
        {
            ListProducts();

            DeleteProduct();

            ListProducts();
        }
        public static void DeleteProduct()                                                           //we can use these methods that add user interaction with our Dapper Methods 
        {
            var prodRepo = new DapperProductRepository(conn);
            Console.WriteLine($"What is the productID of th product you would like to delete:");
            var productID = Convert.ToInt32(Console.ReadLine());

            prodRepo.DeleteProduct(productID);

        }
        public static void UpdateProductName()
        {
            var prodRepo = new DapperProductRepository(conn);

            Console.WriteLine($"What is the productID of the product you would like to update?");
            var productID = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine($"What is the new name you ould like for the product with an id of {productID}?");
            var updatedName = Console.ReadLine();

            prodRepo.UpdateProductName(productID, updatedName);
        }
        public static void CreateAndListProducts()
        {
            var prodRepo = new DapperProductRepository(conn);

            Console.WriteLine($"What is the new product name?");
            var prodName = Console.ReadLine();

            Console.WriteLine($"What is the new product's price?");
            var price = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine($"What is th new products category id?");
            var categoryID = Convert.ToInt32(Console.ReadLine());

            prodRepo.CreateProduct(prodName, price, categoryID);

            var products = prodRepo.GetAllProducts();

            foreach (var product in products)
            {
                Console.WriteLine($"{product.ProductID} {product.Name}");
            }

        }
        
        public static void ListDepartments()
        {
            var repo = new DepartmentRepository(conn);
            var departments = repo.GetDepartments();
            foreach (var item in departments)
            {
                Console.WriteLine($"{item.DepartmentID} {item.Name}");
            }

        }
        public static void DepartmentUpdate()
        {
            var repo = new DepartmentRepository(conn);
            Console.WriteLine($"Would you like to update a departmen? yes or no");
            if (Console.ReadLine().ToUpper() == "Yes")
            {
                Console.WriteLine($"What is the ID of the Department you would like to update?");
                var id = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine($"What would you like to change the name of the department to?");
                var nameName = Console.ReadLine();
                repo.UpdateDepartment(id, newName);
                var depts = repo.GetDepartments();
                foreach (var item in depts)
                {
                    Console.WriteLine($"{item.DepartmentID} {item.Name}");
                }
            }
        }
    }
}


