﻿using System;
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
    [RoutePrefix("Dashboard/Divisions")]
    [Route("{action=Index}")]
    public class DivisionController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: Division
        public ActionResult Index()
        {
            var Division = db.Division.Include(d => d.Faculty);
            return View(Division.ToList());
        }

        // GET: Division/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Division division = db.Division.Find(id);
            if (division == null)
            {
                return HttpNotFound();
            }
            return View(division);
        }

        // GET: Division/Create
        public ActionResult Create()
        {
            ViewBag.UniversityId = new SelectList(db.Universities, "Id", "Name");
            return View();
        }

        // POST: Division/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Division division , string Interests, string courses)
        {
            // adding interests from a string of ids
            var interest = Interests.Split(',');
            var InterestsL = new List<int>();
            foreach (var b in interest)
            {
                InterestsL.Add(Int32.Parse(b));
            }
            var lIntenerts = db.Interests.Where(i => InterestsL.Contains(i.Id));

            // adding courses from a string of ids
            var course = courses.Split(',');
            var coursesL = new List<int>();
            foreach(var b in course)
            {
                coursesL.Add(Int32.Parse(b));
            }
            var lCourses = db.Courses.Where(i => coursesL.Contains(i.Id));

            // saving the division to database
            db.Division.Add(division);
            db.SaveChanges();

            // add each interest to division
            foreach (var i in lIntenerts)
            {
                division.Interests.Add(i);
            }

            // add each course into division
            foreach(var i in lCourses)
            {
                division.Courses.Add(i);
            }

            // presist db changes
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        // GET: Division/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Division division = db.Division.Find(id);
            if (division == null)
            {
                return HttpNotFound();
            }
            ViewBag.FacultyId = new SelectList(db.Faculties, "Id", "Name", division.FacultyId);
            return View(division);
        }

        // POST: Division/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,ExtraInfo,Views,FacultyId")] Division division)
        {
            if (ModelState.IsValid)
            {
                db.Entry(division).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FacultyId = new SelectList(db.Faculties, "Id", "Name", division.FacultyId);
            return View(division);
        }

        // GET: Division/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Division division = db.Division.Find(id);
            if (division == null)
            {
                return HttpNotFound();
            }
            return View(division);
        }

        // POST: Division/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Division division = db.Division.Find(id);
            db.Division.Remove(division);
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
