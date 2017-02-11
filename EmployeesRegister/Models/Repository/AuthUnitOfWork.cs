using EmployeesRegister.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeesRegister.Models.Repository
{
    public class AuthUnitOfWork
    {
        private readonly EmployeeRegisterDb db;
        
        private DepartmentRepository departmentRepository;

        private EmployeeRepository employeeRepository;

        private UserRepository userRepository;

        private ProgLanguageRepository progLanguageRepository;

        private SkillsRepository skillsRepository;

        public AuthUnitOfWork(EmployeeRegisterDb db)
        {
            this.db = db;
        }
        
        public DepartmentRepository DepartmentRepository
        {
            get
            {
                if (this.departmentRepository == null)
                {
                    this.departmentRepository = new DepartmentRepository(this.db);
                }

                return this.departmentRepository;
            }
        }

        public EmployeeRepository EmployeeRepository
        {
            get
            {
                if (this.employeeRepository == null)
                {
                    this.employeeRepository = new EmployeeRepository(this.db);
                }

                return this.employeeRepository;
            }
        }

        public UserRepository UserRepository
        {
            get
            {
                if (this.userRepository == null)
                {
                    this.userRepository = new UserRepository(this.db);
                }

                return this.userRepository;
            }
        }

        public ProgLanguageRepository ProgLanguageRepository
        {
            get
            {
                if (this.progLanguageRepository == null)
                {
                    this.progLanguageRepository = new ProgLanguageRepository(this.db);
                }

                return this.progLanguageRepository;
            }
        }

        public SkillsRepository SkillsRepository
        {
            get
            {
                if (this.skillsRepository == null)
                {
                    this.skillsRepository = new SkillsRepository(this.db);
                }

                return this.skillsRepository;
            }
        }
        

        public void Save(User user)
        {
            if(user == null)
            {
                throw new Exception("Not authorized");
            }

            user.LastActionUtc = DateTime.UtcNow;
            this.UserRepository.Update(user);

            this.db.SaveChanges();
        }
    }
}