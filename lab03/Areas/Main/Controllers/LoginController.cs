using lab03.Areas.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace lab03.Areas.Main.Controllers
{
    public class LoginController : Controller
    {
        // GET: Main/Home

        CompanyDBDataContext context = new CompanyDBDataContext();

        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginModel model)
        {

            if(model.emailRegister == null && model.FirstName == null && model.LastName == null && model.Password == null && model.Email == null)
            {
                ModelState.AddModelError("", "Please input your Info");
            }
            else if (model.emailRegister == null)
            {
                var result = new AccountModel().Login(model.Email, model.Password);
                if (result && ModelState.IsValid)
                {
                    Session.Add("email", model.Email);
                    return RedirectToAction("Index", "Flower", new { Area = "Main" });
                }
                else if(model.Email == null)
                {
                    ModelState.AddModelError("", "Please input your Email");
                }
                else if(model.Password == null)
                {
                    ModelState.AddModelError("", "Please input your Password");
                }
                else if(model.emailRegister == null && model.Email != null)
                {
                    ModelState.AddModelError("", "Please input your Email");
                }
                else
                {
                    ModelState.AddModelError("", "Email or Password is incorrect");
                }
            }
            else
            {
                var check = new AccountModel().Create(model.FirstName + model.LastName, model.emailRegister);
                if (check || ModelState.IsValid)
                {
                    ModelState.AddModelError("", "Email or User is Exist");
                }
                else if( model.FirstName ==null || model.LastName == null)
                {
                    ModelState.AddModelError("", "Please input your First name or Last name");
                }
                else if (model.Password == null)
                {
                    ModelState.AddModelError("", "Please input your Password");
                }
                else
                {
                    context.sp_Create_Account_Main(model.Password,model.emailRegister,model.FirstName,model.LastName);
                    context.SubmitChanges();
                    Session.Add("email", model.emailRegister);
                    return RedirectToAction("Index", "Flower", new { Area = "Main" });
                }
            }
            return View(model);
        }

        public ActionResult TableResponsive()
        {
            CompanyDBDataContext context = new CompanyDBDataContext();
            var ProductsInfo = context.Products.ToList();
            return View(ProductsInfo);
        }


    }
}