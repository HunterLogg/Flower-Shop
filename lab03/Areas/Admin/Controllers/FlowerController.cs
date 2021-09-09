using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace lab03.Areas.Admin.Controllers
{
    public class FlowerController : Controller
    {
        CompanyDBDataContext _context = new CompanyDBDataContext();

        // GET: Admin/Flower

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            if (file != null)
            {
                string rootPath = Server.MapPath("~/");
                string fileName = System.IO.Path.GetFileName(file.FileName);
                string destFile = System.IO.Path.Combine(rootPath, "Assets\\Flower\\images\\" + fileName);
                file.SaveAs(destFile);
            }
            return View();
        }

        public ActionResult ListFlowers()
        {
            var flowers = _context.Flowers.ToList();
            return Json(flowers, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Create([Bind(Exclude = "Id")] Flower flower)
        {
            if (ModelState.IsValid)
            {
                string rootPath = Server.MapPath("~/");
                string fileName = System.IO.Path.GetFileName(flower.ImagePath);

                flower.ImagePath = "images/" + fileName;
                _context.Flowers.InsertOnSubmit(flower);
                _context.SubmitChanges();
            }
            return Json(flower, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var flower = _context.Flowers.ToList().Find(m => m.Id == id);
            if (flower != null)
            {
                _context.Flowers.DeleteOnSubmit(flower);
                _context.SubmitChanges();
            }
            return Json(flower, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Get(int id)
        {
            var flower = _context.Flowers.ToList().Find(m => m.Id == id);
            string rootPath = Server.MapPath("~/");
            string fileName = System.IO.Path.GetFileName(flower.ImagePath);
            string destFile = System.IO.Path.Combine(rootPath, "Assets\\Flower\\images\\" + fileName);
            flower.ImagePath = destFile;

            return Json(flower, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Update(Flower flower)
        {
            if (ModelState.IsValid)
            {
                string rootPath = Server.MapPath("~/");
                string fileName = System.IO.Path.GetFileName(flower.ImagePath);

                flower.ImagePath = "images/" + fileName;

                Flower fl = (from p in _context.Flowers
                             where p.Id == flower.Id
                             select p).SingleOrDefault();

                fl.FlowerName = flower.FlowerName;
                fl.ImagePath = flower.ImagePath;
                fl.Description = flower.Description;
                fl.Price = flower.Price;
                fl.Type = flower.Type;
                _context.SubmitChanges();
            }
            return Json(flower, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Search(Flower flower)
        {
            var list = (from p in _context.Flowers select p);

            if (flower.FlowerName != null)
            {
                list = list.Where(q => q.FlowerName == flower.FlowerName);
            }
            if (flower.Price != null)
            {
                list = list.Where(q => q.Price == flower.Price);
            }
            List<Flower> ls = list.ToList();

            return Json(ls, JsonRequestBehavior.AllowGet);
        }
    }
}
