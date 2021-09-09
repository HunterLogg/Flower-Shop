using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace lab03.Areas.Admin.Controllers
{
    public class DealController : Controller
    {
        CompanyDBDataContext _context = new CompanyDBDataContext();

        // GET: Admin/Deal
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListDeals()
        {
            var deal = _context.Deals.ToList();
            return Json(deal, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var deal = _context.Deals.ToList().Find(m => m.Id == id);
            if (deal != null)
            {
                _context.Deals.DeleteOnSubmit(deal);
                _context.SubmitChanges();
            }
            return Json(deal, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Get(int id)
        {
            var deal = _context.Deals.ToList().Find(m => m.Id == id);
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
                de.Quantity = deal.Quantity;
                de.Total = deal.Total;
                _context.SubmitChanges();
            }
            return Json(deal, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Search(Deal deal)
        {
            var list = (from p in _context.Deals select p);
            if (deal.CustomerName != null)
            {
                list = list.Where(q => q.CustomerName == deal.CustomerName);
            }
            if (deal.CustomerEmail != null)
            {
                list = list.Where(q => q.CustomerEmail == deal.CustomerEmail);
            }
            if (deal.DealDate != null)
            {
                list = list.Where(q => q.DealDate == deal.DealDate);
            }
            if (deal.FlowerName != null)
            {
                list = list.Where(q => q.FlowerName == deal.FlowerName);
            }
            List<Deal> ls = list.ToList();

            return Json(ls, JsonRequestBehavior.AllowGet);
        }
    }
}