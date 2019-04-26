using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GraduationProject.Data;
using GraduationProject.Data.Entities;

namespace GraduationProject.MVC.Controllers
{
    public class TansiqsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: Tansiqs
        public ActionResult Index()
        {
            var tansiq = db.Tansiq.Include(t => t.Division).Include(t => t.Faculty).Include(t => t.Specialization);
            return View(tansiq.ToList());
        }

        // GET: Tansiqs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tansiq tansiq = db.Tansiq.Find(id);
            if (tansiq == null)
            {
                return HttpNotFound();
            }
            return View(tansiq);
        }

        // GET: Tansiqs/Create
        public ActionResult Create()
        {
            ViewBag.DivisionId = new SelectList(db.Division, "Id", "Name");
            ViewBag.FacultyId = new SelectList(db.Faculties, "Id", "Name");
            ViewBag.SpecializationId = new SelectList(db.Specializations, "Id", "Name");
            return View();
        }

        // POST: Tansiqs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Startgrade,Endgrade,Actual,SpecializationId,FacultyId,DivisionId")] Tansiq tansiq)
        {
            if (ModelState.IsValid)
            {
                db.Tansiq.Add(tansiq);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DivisionId = new SelectList(db.Division, "Id", "Name", tansiq.DivisionId);
            ViewBag.FacultyId = new SelectList(db.Faculties, "Id", "Name", tansiq.FacultyId);
            ViewBag.SpecializationId = new SelectList(db.Specializations, "Id", "Name", tansiq.SpecializationId);
            return View(tansiq);
        }

        // GET: Tansiqs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tansiq tansiq = db.Tansiq.Find(id);
            if (tansiq == null)
            {
                return HttpNotFound();
            }
            ViewBag.DivisionId = new SelectList(db.Division, "Id", "Name", tansiq.DivisionId);
            ViewBag.FacultyId = new SelectList(db.Faculties, "Id", "Name", tansiq.FacultyId);
            ViewBag.SpecializationId = new SelectList(db.Specializations, "Id", "Name", tansiq.SpecializationId);
            return View(tansiq);
        }

        // POST: Tansiqs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Startgrade,Endgrade,Actual,SpecializationId,FacultyId,DivisionId")] Tansiq tansiq)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tansiq).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DivisionId = new SelectList(db.Division, "Id", "Name", tansiq.DivisionId);
            ViewBag.FacultyId = new SelectList(db.Faculties, "Id", "Name", tansiq.FacultyId);
            ViewBag.SpecializationId = new SelectList(db.Specializations, "Id", "Name", tansiq.SpecializationId);
            return View(tansiq);
        }

        // GET: Tansiqs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tansiq tansiq = db.Tansiq.Find(id);
            if (tansiq == null)
            {
                return HttpNotFound();
            }
            return View(tansiq);
        }

        // POST: Tansiqs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tansiq tansiq = db.Tansiq.Find(id);
            db.Tansiq.Remove(tansiq);
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
