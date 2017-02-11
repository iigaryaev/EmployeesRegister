using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EmployeesRegister.Models.Database;

namespace EmployeesRegister.Models.Repository
{
    public class EmployeeRepository : Repository
    {
        public EmployeeRepository(EmployeeRegisterDb db) : base(db)
        {
        }

        public void Create(Employee entity)
        {
            this.db.Entry(entity).State = System.Data.Entity.EntityState.Added;
        }

        public IEnumerable<Employee> GetAll(bool showDelete = false)
        {
            return this.db.Employees.Include("Skills").Where(e => e.IsActive || showDelete).ToList();
        }

        public Employee GetById(int id)
        {
            return this.db.Employees.Include("Skills").First(e => e.Id == id);
        }
        
        public void Update(Employee entity)
        {
            this.db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public IEnumerable<Employee> Search(string filter, bool showDelete = false)
        {
            return this.db.Employees.Include("Skills").Where(e => (e.IsActive || showDelete) 
                && ( e.FirstName.Contains(filter) 
                    || e.LastName.Contains(filter) 
                    || e.Skills.Any(s => s.ProgLanguage.Name.Contains(filter)) 
                    || e.Department.Name.Contains(filter))).ToList();
        }
    }
}