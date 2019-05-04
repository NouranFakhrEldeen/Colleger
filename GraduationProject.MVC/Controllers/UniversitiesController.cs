using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GraduationProject.Data;
using GraduationProject.Data.Entities;

namespace GraduationProject.MVC.Controllers
{
    [RoutePrefix("Dashboard/Universities")]
    [Route("{action=Index}")]
    public class UniversitiesController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: Universities
        public ActionResult Index()
        {
            return View(db.Universities.ToList());
        }

        [Route("RetrieveImage/{id}")]
        public ActionResult RetrieveImage(int id)
        {
            byte[] cover = GetImageFromDataBase(id);
            if (cover != null)
            {
                return File(cover, "image/jpg");
            }
            else
            {
                return null;
            }
        }

        public byte[] GetImageFromDataBase(int Id)
        {
            var q = from temp in db.Universities where temp.Id == Id select temp.Logo;
            byte[] cover = q.First();
            return cover;
        }

        // GET: Universities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            University university = db.Universities.Find(id);
            if (university == null)
            {
                return HttpNotFound();
            } else
            {
                university.Views = university.Views + 1;
                db.SaveChanges();
            }
            return View(university);
        }

        // GET: Universities/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Universities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(University university)
        {
            if (ModelState.IsValid)
            {
                HttpPostedFileBase file = Request.Files["ImageData"];

                if(file.ContentLength > 1)
                {
                    university.Logo = ConvertToBytes(file);
                }
                db.Universities.Add(university);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(university);
        }
       
        public byte[] ConvertToBytes(HttpPostedFileBase Logo)
        {
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(Logo.InputStream);
            imageBytes = reader.ReadBytes((int)Logo.ContentLength);
            return imageBytes;
        }

        // GET: Universities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            University university = db.Universities.Find(id);
            if (university == null)
            {
                return HttpNotFound();
            }
            return View(university);
        }

        // POST: Universities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Zipcode,Street,City,Governorate,Logo,Description,Views")] University university)
        {
            if (ModelState.IsValid)
            {
                HttpPostedFileBase file = Request.Files["LogoIncoming"];

                if (file.ContentLength > 1)
                {
                    university.Logo = ConvertToBytes(file);
                }
                db.Entry(university).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(university);
        }

        // GET: Universities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            University university = db.Universities.Find(id);
            if (university == null)
            {
                return HttpNotFound();
            }
            return View(university);
        }

        // POST: Universities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            University university = db.Universities.Find(id);
            db.Universities.Remove(university);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
