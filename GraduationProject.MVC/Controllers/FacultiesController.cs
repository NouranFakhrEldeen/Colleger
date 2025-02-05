﻿using System;
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
    [RoutePrefix("Dashboard/Faculties")]
    [Route("{action=Index}")]
    public class FacultiesController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: Faculties
        public ActionResult Index()
        {
            var faculties = db.Faculties.Include(f => f.University);
            return View(faculties.ToList());
        }
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
            var q = from temp in db.Faculties where temp.Id == Id select temp.logo;
            byte[] cover = q.First();
            return cover;
        }


        // GET: Faculties/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Faculty faculty = db.Faculties.Find(id);
            if (faculty == null)
            {
                return HttpNotFound();
            }
            return View(faculty);
        }

        // GET: Faculties/Create
        public ActionResult Create()
        {
            ViewBag.UniversityId = new SelectList(db.Universities, "Id", "Name");
            return View();
        }

        // POST: Faculties/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Faculty faculty)
        {
            if (ModelState.IsValid)
            {
                HttpPostedFileBase file = Request.Files["ImageData"];
                faculty.logo = ConvertToBytes(file);
                db.Faculties.Add(faculty);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UniversityId = new SelectList(db.Universities, "Id", "Name", faculty.UniversityId);
            return View(faculty);
        }
        public byte[] ConvertToBytes(HttpPostedFileBase Logo)
        {
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(Logo.InputStream);
            imageBytes = reader.ReadBytes((int)Logo.ContentLength);
            return imageBytes;
        }

        // GET: Faculties/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Faculty faculty = db.Faculties.Find(id);
            if (faculty == null)
            {
                return HttpNotFound();
            }
            ViewBag.UniversityId = new SelectList(db.Universities, "Id", "Name", faculty.UniversityId);
            return View(faculty);
        }

        // POST: Faculties/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,logo,searchAppearancesCount,UniversityId")] Faculty faculty)
        {
            if (ModelState.IsValid)
            {
                db.Entry(faculty).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UniversityId = new SelectList(db.Universities, "Id", "Name", faculty.UniversityId);
            return View(faculty);
        }

        // GET: Faculties/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Faculty faculty = db.Faculties.Find(id);
            if (faculty == null)
            {
                return HttpNotFound();
            }
            return View(faculty);
        }

        // POST: Faculties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Faculty faculty = db.Faculties.Find(id);
            db.Faculties.Remove(faculty);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("~/api/faculties/{id}")]
        public ActionResult getAllByUniversityId(int id)
        {
            var faculties = db.Faculties.Where(i => i.UniversityId == id).Select(i => new { i.Id, i.Name });
            return Json(faculties, JsonRequestBehavior.AllowGet);
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
