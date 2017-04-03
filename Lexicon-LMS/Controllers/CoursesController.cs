using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Lexicon_LMS.Models;
using System;
using System.Collections.Generic;

namespace Lexicon_LMS.Controllers
{
    [Authorize]
    public class CoursesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Courses
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListCourses()
        {
            return PartialView("_ListAllPartialView", db.Courses.ToList());
        }

        // GET: Courses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }

            List<Activity> ongoingActivities = new List<Activity>();

            foreach (var module in course.Modules)
            {
                var start = module.StartDate;
                var end = module.EndDate;
               
                if (start.CompareTo(DateTime.Today) <= 0 && end.CompareTo(DateTime.Today) >= 0)
                {
                    ViewBag.ShowModuleContent = module.Name;


                    foreach (var activity in module.Actvities)
                    {
                        var astart = activity.StartDate;
                        var aend = activity.EndDate;
                        if (astart.CompareTo(DateTime.Today) <= 0 && aend.CompareTo(DateTime.Today) >= 0)
                        {
                            ongoingActivities.Add(activity);                           
                        }
                    }                  
                }       
                 
            }
            if (ongoingActivities != null) {
                ViewBag.ShowActivityContent = ongoingActivities;
            }  
                 
            course.Modules = course.Modules.OrderBy(m => m.StartDate).ToList();
            return View(course);
        }

        // GET: Courses/Create
        [Authorize(Roles = "teacher")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "teacher")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,StartDate,EndDate")] Course course)
        {       
            // Check if course with this Name already exist            
            if (db.Courses.Any(c => c.Name == course.Name))
            {
                ModelState.AddModelError("Name", "Det finns redan en kurs med detta Kursnamn");
            }

            var start = course.StartDate;
            var end = course.EndDate;

            if (start.CompareTo(end) == 1)
                ModelState.AddModelError("EndDate", "Slutdatum kan inte inträffa innan startdatum");
            else
            {
                if (start.Year.CompareTo(DateTime.Today.Year - 1) == -1)
                    ModelState.AddModelError("StartDate", "Startdatum får inte vara äldre än 365 dagar");

                if (end.Year.CompareTo(DateTime.Today.Year + 1) == 1)
                    ModelState.AddModelError("EndDate", "Slutdatum får inte vara senare än 365 dagar");
            }

            if (ModelState.IsValid)
            {
                db.Courses.Add(course);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ShowContent = "in";
            return View("Index", course);
        }

        // GET: Courses/Edit/5
        [Authorize(Roles = "teacher")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "teacher")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,StartDate,EndDate")] Course course)
        {
            // Check if course with this Name and different Id, already exist    
            if (db.Courses.Any(c => ((c.Id != course.Id) && (c.Name == course.Name))))
            {
                ModelState.AddModelError("Name", "Det finns redan en kurs med detta Kursnamn");               
            }

            if (course.StartDate.CompareTo(course.EndDate) == 1)
            {
                ModelState.AddModelError("EndDate", "Slutdatum kan inte inträffa innan startdatum");
            }

            if (ModelState.IsValid)
            {
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new {  id = course.Id});
            }
            return View(course);
        }

        // GET: Courses/Delete/5
        [Authorize(Roles = "teacher")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Courses/Delete/5
        [Authorize(Roles = "teacher")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Course course = db.Courses.Find(id);
            db.Courses.Remove(course);
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
