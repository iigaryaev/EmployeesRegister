using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EmployeesRegister.Models.Database;
using System.Text;
using System.Security.Cryptography;

namespace EmployeesRegister.Models.Repository
{
    public class UserRepository : Repository
    {
        public UserRepository(EmployeeRegisterDb db) : base(db)
        {
        }

        public void Create(User entity)
        {
            this.db.Entry(entity).State = System.Data.Entity.EntityState.Added;
        }

        public User GetById(int id)
        {
            return this.db.Users.Find(id);
        }

        public User GetByLogin(string login)
        {
            return this.db.Users.FirstOrDefault(e => e.Login == login);
        }

        public void Update(User entity)
        {
            this.db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public RegistrationResult Register(string login, string password)
        {
            var existingLogin = this.GetByLogin(login);
            if (existingLogin != null)
            {
                return new RegistrationResult(false, "Login already exists");
            }

            try
            {
                byte[] encodedPassword = new UTF8Encoding().GetBytes(password);
                byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);

                this.Create(new User { Login = login, PasswordMD5 = hash });
            }
            catch (Exception ex)
            {
                return new RegistrationResult(false, "Registration error");
            }


            return new RegistrationResult(true);
        }
    }
}