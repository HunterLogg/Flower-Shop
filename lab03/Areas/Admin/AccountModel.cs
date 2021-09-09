using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace lab03.Areas.Admin
{
    public class AccountModel
    {
        private CompanyDBDataContext context = null;

        public AccountModel()
        {
            context = new CompanyDBDataContext();
        }
        public bool Login(string UserName, string Password)
        {
            bool? res = false;
            context.sp_Account_Login_Check(UserName, Password, ref res);

            return (res ?? false);
        }
        public bool Register(string UserName, string Email)
        {
            bool? res = false;
            context.sp_Check_Account(UserName, Email, ref res);
            
            return (res ?? false);
        }
    }
}