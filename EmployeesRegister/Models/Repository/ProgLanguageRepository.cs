using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EmployeesRegister.Models.Database;

namespace EmployeesRegister.Models.Repository
{
    public class ProgLanguageRepository : Repository
    {
        public ProgLanguageRepository(EmployeeRegisterDb db) : base(db)
        {
        }

        public ProgLanguage GetById(int id)
        {
            return this.db.ProgLanguages.Find(id);
        }

        public IEnumerable<ProgLanguage> GetAll()
        {
            return this.db.ProgLanguages.ToList();
        }
    }
}