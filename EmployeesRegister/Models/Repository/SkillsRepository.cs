using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EmployeesRegister.Models.Database;

namespace EmployeesRegister.Models.Repository
{
    public class SkillsRepository : Repository
    {
        public SkillsRepository(EmployeeRegisterDb db) : base(db)
        {
        }
        
        public void Create(Skills entity)
        {
            this.db.Entry(entity).State = System.Data.Entity.EntityState.Added;
        }

        public IEnumerable<Skills> EmployeeSkills(int employeeId)
        {
            return this.db.Skills.Where(e => e.EmployeeId == employeeId).ToList();
        }

        public Skills GetById(int id)
        {
            return this.db.Skills.Find(id);
        }

        public void Update(Skills entity)
        {
            this.db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public void Delete(Skills entity)
        {
            this.db.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
        }
    }
}