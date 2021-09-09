using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lab03.Areas.Main
{
    public class AccountModel
    {
        private CompanyDBDataContext context = null;

        public AccountModel()
        {
            context = new CompanyDBDataContext();
        }

        public bool Login(string Email, string Password)
        {
            bool? res = false;
            context.sp_Account_Loginuser_Check(Email, Password, ref res);

            return (res ?? false);
        }
        public bool Create(string Username, string Email)
        {
            bool? res = false;
            context.sp_Check_Account(Username, Email, ref res);

            return (res ?? false);
        }
    }
}