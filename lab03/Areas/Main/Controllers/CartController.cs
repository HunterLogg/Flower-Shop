using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace lab03.Areas.Main.Controllers
{
    public class CartController : Controller
    {
        CompanyDBDataContext _context = new CompanyDBDataContext();

        // GET: Main/Cart
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListCarts()
        {
            var deal = _context.Deals.ToList();
            string email = (string)Session["email"];
            var databyid = _context.Deals.Where(x => x.CustomerEmail == email);
            return Json(databyid, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SearchUser()
        {
            string email = (string)Session["email"];
            var databyid = _context.Accounts.Where(x => x.Email == email);
            return Json(databyid, JsonRequestBehavior.AllowGet);
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

        public ActionResult Reports(string ReportType)
        {
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Reported/Report1.rdlc");
            var reportDataSource = new ReportDataSource();
            reportDataSource.Name = "DealData";
            string email = (string)Session["email"];
            var databyid = _context.Deals.Where(x => x.CustomerEmail == email);
            reportDataSource.Value = databyid;
            localReport.DataSources.Add(reportDataSource);
            string reportType = ReportType;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string fileNameExtension = string.Empty;
            if (reportType == "Excel")
            {
                fileNameExtension = "xlsx";
            }
            else if (reportType == "PDF")
            {
                fileNameExtension = "pdf";
            }
            else if (reportType == "Word")
            {
                fileNameExtension = "docx";
            }
            else
            {
                fileNameExtension = "jpg";
            }

            string[] streams;
            Warning[] warnings;
            byte[] renderedByte;
            renderedByte = localReport.Render(reportType, "", out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
            HttpResponseBase response = Response;
            response.AddHeader("content-disposition", "attachment;filename= deal_report." + fileNameExtension);

            return File(renderedByte, fileNameExtension);
        }
    }
}