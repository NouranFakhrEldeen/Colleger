using GraduationProject.Data;
using GraduationProject.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace GraduationProject.MVC.Controllers
{
    [RoutePrefix("Dashboard/Courses")]
    [Route("{action=Index}")]
    public class CoursesController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: Courses
        public ActionResult Index()
        {
            return View(db.Courses.ToList());
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Courses courses = db.Courses.Find(id);
            if (courses == null)
            {
                return HttpNotFound();
            }
            return View(courses);
        }

        // GET: Interests/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Interests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,name")] Courses course)
        {
            if (ModelState.IsValid)
            {
                db.Courses.Add(course);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(course);
        }

        // GET: Interests/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Courses course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Interests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,name")] Courses courses)
        {
            if (ModelState.IsValid)
            {
                db.Entry(courses).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(courses);
        }

        // GET: Interests/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Courses courses = db.Courses.Find(id);
            if (courses == null)
            {
                return HttpNotFound();
            }
            return View(courses);
        }

        // POST: Interests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Courses courses = db.Courses.Find(id);
            db.Courses.Remove(courses);
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


        [HttpGet]
        [Route("~/api/courses")]
        public ActionResult getAll()
        {
            var courses = db.Courses.Select(i => new { i.Id, i.Name }).ToList();
            return Json(courses, JsonRequestBehavior.AllowGet);
        }


    }
}