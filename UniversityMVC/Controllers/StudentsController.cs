using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UniversityMVC.Data;
using UniversityMVC.Models;
using UniversityMVC.ViewModels;
using PagedList;

namespace UniversityMVC.Controllers
{
    public class StudentsController : Controller
    {
        private UniversityContext db = new UniversityContext();

        // GET: Students
        public ActionResult Index(string campus, string search, string sortBy, int? page)
        {
            //instantiate a new view model
            StudentIndexViewModel viewModel = new StudentIndexViewModel();
            var students = db.Students.Include(s => s.Campus);

            //perform the search and save the search string to the viewModel
            if (!String.IsNullOrEmpty(search))
            {
                students = students.Where(s => s.Name.Contains(search) ||
                                               s.Address.Contains(search) ||
                                               s.Campus.Name.Contains(search));
                viewModel.Search = search;
            }

            //group search results into campuses and count how many items in each campus
            viewModel.CampusesWithCount = from matchingStudents in students
                                          where matchingStudents.CampusID != null
                                          group matchingStudents by matchingStudents.Campus.Name into campusGroup
                                          select new CampusWithCount()
                                          {
                                              CampusName = campusGroup.Key,
                                              StudentCount = campusGroup.Count()
                                          };

            if (!String.IsNullOrEmpty(campus))
            {
                students = students.Where(s => s.Campus.Name == campus);
                viewModel.Campus = campus;
            }

            //sort the results
            switch (sortBy)
            {
                case "name_lowest":
                    students = students.OrderBy(s => s.Name);
                    break;
                case "name_highest":
                    students = students.OrderByDescending(s => s.Name);
                    break;
                case "address_lowest":
                    students = students.OrderBy(s => s.Address);
                    break;
                case "address_highest":
                    students = students.OrderByDescending(s => s.Address);
                    break;
                default:
                    students = students.OrderBy(s => s.Name);
                    break;
            }

            viewModel.SortBy = sortBy;
            viewModel.Sorts = new Dictionary<string, string>
            {
                {"Name A to Z", "name_lowest" },
                {"Name Z to A", "name_highest" },
                {"Address A to Z", "address_lowest" },
                {"Address Z to A", "address_highest" }
            };

            //paging: 3 items per page
            const int PageItems = 3;
            int currentPage = (page ?? 1);
            viewModel.Students = students.ToPagedList(currentPage, PageItems);

            return View(viewModel);
        }

        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            ViewBag.CampusID = new SelectList(db.Campuses, "ID", "Name");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Address,CampusID")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CampusID = new SelectList(db.Campuses, "ID", "Name", student.CampusID);
            return View(student);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.CampusID = new SelectList(db.Campuses, "ID", "Name", student.CampusID);
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Address,CampusID")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CampusID = new SelectList(db.Campuses, "ID", "Name", student.CampusID);
            return View(student);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
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
