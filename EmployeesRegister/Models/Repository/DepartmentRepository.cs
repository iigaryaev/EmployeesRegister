using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EmployeesRegister.Models.Database;

namespace EmployeesRegister.Models.Repository
{
    public class DepartmentRepository : Repository
    {
        public DepartmentRepository(EmployeeRegisterDb db) : base(db)
        {
        }

        public Department GetById(int id)
        {
            return this.db.Departments.Find(id);
        }

        public IEnumerable<Department> GetAll()
        {
            return this.db.Departments.ToList();
        }
    }
}