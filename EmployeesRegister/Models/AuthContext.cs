using EmployeesRegister.Models.Database;
using EmployeesRegister.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace EmployeesRegister.Models
{
    public class AuthContext
    {
        protected readonly UserRepository database;
        public AuthContext(EmployeeRegisterDb db)
        {
            this.database = new UserRepository(db);
        }

        public User Authorize(string login, string password)
        {
            byte[] encodedPassword = new UTF8Encoding().GetBytes(password);
            byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);

            var account = this.database.GetByLogin(login);

            if (account == null)
            {
                return null;
            }

            var passwordsEqual = true;

            for (var i = 0; i < Math.Max(hash.Length, account.PasswordMD5.Length); i++)
            {
                if (hash[i] != account.PasswordMD5[i])
                {
                    passwordsEqual = false;
                }
            }

            return passwordsEqual ? account : null;
        }
    }
}