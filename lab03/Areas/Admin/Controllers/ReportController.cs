using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using lab03.Models;
using Microsoft.Reporting.WebForms;

namespace lab03.Areas.Admin.Controllers
{
    public class ReportController : Controller
    {
        CompanyDBDataContext context = new CompanyDBDataContext();
        ProjectMVCEntities1 pro = new ProjectMVCEntities1();
        // GET: Admin/Report
        public ActionResult Index()
        {

            return View(context.Deals.ToList());
        }
        public ActionResult Reports(string ReportType)
        {
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Reported/Report1.rdlc");
            var reportDataSource = new ReportDataSource();
            reportDataSource.Name = "DealData";
            reportDataSource.Value = context.Deals.ToList();
            localReport.DataSources.Add(reportDataSource);
            string reportType = ReportType;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string fileNameExtension = string.Empty;
            if(reportType == "Excel")
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

            string[] streams ;
            Warning[] warnings;
            byte[] renderedByte;
            renderedByte = localReport.Render(reportType, "", out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
            HttpResponseBase response = Response;
            response.AddHeader("content-disposition", "attachment;filename= deal_report." + fileNameExtension);

            return File(renderedByte, fileNameExtension);
        }
    }
    
}