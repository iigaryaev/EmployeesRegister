using EmployeesRegister.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeesRegister.Models.Repository
{
    public abstract class Repository
    {
        protected readonly EmployeeRegisterDb db;
        public Repository(EmployeeRegisterDb db)
        {
            this.db = db;
        }

        public void Save()
        {
            this.db.SaveChanges();
        }
    }
}