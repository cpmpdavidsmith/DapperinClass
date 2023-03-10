using System;
using System.Data;
using System.Collections.Generic;
using Dapper;

namespace DapperInClass
{
	public class DepartmentRepository : IDepartmentRepository
	{
        private readonly IDbConnection _conn;
		public DepartmentRepository(IDbConnection conn)
		{
            _conn = conn;
		}

        public void CreateDepartment(string name)
        {
            _conn.Execute("INSERT INTO departments Name Values(@name);", new { name = name });
        }

        public IEnumerable<Department> GetDepartments()
        {
            return _conn.Query<Department>("select * FROM departments;");
        }
        public void UpdateDepartment(int id, string newName)
        {
            _conn.Execute("UPDATE departments SET Name = @newName WHERE DepartmentID = @id;", new { newName = newName, id = id });
        }
    }
}

