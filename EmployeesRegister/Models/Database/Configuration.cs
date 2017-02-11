using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace EmployeesRegister.Models.Database
{
    public class Configuration : DbMigrationsConfiguration<EmployeeRegisterDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = false;

        }

        protected override void Seed(EmployeeRegisterDb db)
        {
            db.ProgLanguages.AddOrUpdate(e => e.Id, new[] {
                new ProgLanguage { Id = 1, Name = "C#" },
                new ProgLanguage { Id = 2, Name = "T-SQL" },
                new ProgLanguage { Id = 3, Name = "C++" } ,
                new ProgLanguage { Id = 4, Name = "PHP" } ,
                new ProgLanguage { Id = 5, Name = "JAVA" } });

            db.Departments.AddOrUpdate(e => e.Id, new[] {
                new Department { Id = 1, Name = "Big devs dpt.", Floor = 2 },
                new Department { Id = 2, Name = "Small devs dpt.", Floor = 3 },
                new Department { Id = 3, Name = "Big and small devs dpt.", Floor = 2 } ,
                new Department { Id = 4, Name = "Forcasting dpt.", Floor = 4 } ,
                new Department { Id = 5, Name = "Administartion", Floor = 5 } });

            db.Users.AddOrUpdate(e => e.Login, new User { Login = "Administrator"
                , PasswordMD5 = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(new UTF8Encoding().GetBytes("123qwe!"))});

            //db.Database.ExecuteSqlCommand(@"if (object_id('DF_Employee_CreatedUtc') is null)
            //alter table Employee add constraint DF_Employee_CreatedUtc  default(getutcdate()) for CreatedUtc");

            //           db.Database.ExecuteSqlCommand(@"if (object_id('UQ_Account_UserName') is null)
            //alter table Account add constraint UQ_Account_UserName  unique(UserName)");

            //           db.Database.ExecuteSqlCommand(@"if (object_id('UQ_Account_UserLogin') is null)
            //alter table Account add constraint UQ_Account_UserLogin  unique(UserLogin)");

            db.Database.ExecuteSqlCommand(@"if (object_id('DF_User_CreatedUtc') is null)
            alter table [User] add constraint DF_User_CreatedUtc  default(getutcdate()) for CreatedUtc");

            db.Database.ExecuteSqlCommand(@"if (object_id('CK_Employee_Gender') is null)
            alter table Employee add constraint CK_Employee_Gender  check(Gender in ('M', 'F'))");

            db.Database.ExecuteSqlCommand(@"if (object_id('CK_Employee_Age') is null)
            alter table Employee add constraint CK_Employee_Age  check(Age > 0)");

            //           db.Database.ExecuteSqlCommand(@"if (object_id('CK_Payment_Ammount') is null)
            //alter table Payment add constraint CK_Payment_Ammount  check(Ammount > 0)");

            //           db.Database.ExecuteSqlCommand(@"if (object_id('CK_Account_Balance') is null)
            //alter table Account add constraint CK_Account_Balance  check(Balance >= 0)");
        }
    }
}