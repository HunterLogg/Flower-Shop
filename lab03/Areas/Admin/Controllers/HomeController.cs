using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace lab03.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        // GET: Admin/Home

        private CompanyDBDataContext context = new CompanyDBDataContext();

        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginModel model)
        {
            if (model.Email == null && model.UserName == null && model.Password == null && model.User == null)
            {
                ModelState.AddModelError("", "Please in put your Info!");
            } else
            if (model.User == null)
            {
                var result = new AccountModel().Login(model.UserName, model.Password);
                if (result || ModelState.IsValid)
                {
                    return RedirectToAction("Index", "Flower", new { Area = "Admin" });
                }
                else if (model.Password == null)
                {
                    ModelState.AddModelError("", "Please input your Password");
                }
                else
                    ModelState.AddModelError("", "Username or Password is incorrent");
            }
            else
            {
                var resultt = new AccountModel().Register(model.User, model.Email);
                if (resultt && ModelState.IsValid)
                {
                    ModelState.AddModelError("", "Username or Password is Exist");
                }
                else if (model.Password == null)
                {
                    ModelState.AddModelError("", "Please input your Password");
                }
                else if (model.User == null)
                {
                    ModelState.AddModelError("", "Please input your User");
                }
                else if (model.Email == null)
                {
                    ModelState.AddModelError("", "Please input your Email");
                }
                else
                {
                    context.sp_Create_Account_Admin(model.User, model.Password, model.Email);
                    context.SubmitChanges();
                    return RedirectToAction("Index", "Flower", new { Area = "Admin" });
                }

            }
            return View(model);
        }
        public ActionResult ListAccount()
        {
            CompanyDBDataContext context = new CompanyDBDataContext();
            var AccountInfos = context.Accounts.ToList();
            return View(AccountInfos);
        }
        public ActionResult Details(int id)
        {
            var databyid = context.Accounts.Single(x => x.Id == id);

            return View(databyid);
        }

        public ActionResult Edit(int id)
        {
            var databyid = context.Accounts.Single(x => x.Id == id);
            return View(databyid);
        }

        [HttpPost]
        public ActionResult Edit(int id, LoginModel account)
        {
            try
            {
                var data = context.Accounts.Single(x => x.Id == id);
                data.Email = account.Email;
                data.Password = account.Password;
                data.UserName = account.UserName;
                context.SubmitChanges();
                return RedirectToAction("ListAccount");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            var databyid = context.Accounts.Single(x => x.Id == id);
            return View(databyid);
        }

        [HttpPost]
        public ActionResult Delete(int id, LoginModel account)
        {
            try
            {
                var data = context.Accounts.Single(x => x.Id == id);
                context.Accounts.DeleteOnSubmit(data);
                context.SubmitChanges();
                return RedirectToAction("ListAccount");
            }
            catch
            {
                return View();
            }
        }
    }
}