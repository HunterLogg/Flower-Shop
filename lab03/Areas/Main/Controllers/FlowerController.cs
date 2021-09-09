using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace lab03.Areas.Main.Controllers
{
    public class FlowerController : Controller
    {
        CompanyDBDataContext _context = new CompanyDBDataContext();

        // GET: Main/Flower
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListFlowers()
        {
            var flowers = _context.Flowers.ToList();
            return Json(flowers, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Create([Bind(Exclude = "Id")] Deal deal)
        {
            if (ModelState.IsValid)
            {
                _context.Deals.InsertOnSubmit(deal);
                _context.SubmitChanges();
            }
            return Json(deal, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Update(Deal deal)
        {
            if (ModelState.IsValid)
            {
                Deal de = (from p in _context.Deals
                           where p.Id == deal.Id
                           select p).SingleOrDefault();

                de.CustomerName = deal.CustomerName;
                de.CustomerEmail = deal.CustomerEmail;
                de.DealDate = deal.DealDate;
                de.FlowerName = deal.FlowerName;
                de.Price = deal.Price;
                de.Quantity = deal.Quantity;
                _context.SubmitChanges();
            }
            return Json(deal, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Get(int id)
        {
            var deal = _context.Flowers.ToList().Find(m => m.Id == id);
            return Json(deal, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SearchUser()
        {
            string email = (string)Session["email"];
            var databyid = _context.Accounts.Where(x => x.Email == email);
            return Json(databyid, JsonRequestBehavior.AllowGet);
        }
    }
}