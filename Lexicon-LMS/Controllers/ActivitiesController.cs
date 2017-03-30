using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Lexicon_LMS.Models;

namespace Lexicon_LMS.Controllers
{
    [Authorize]
    public class ActivitiesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Activities
        public ActionResult Index()
        {
            var activities = db.Activities.Include(a => a.Module).Include(a => a.Type);
            return View(activities.ToList());
        }

        // GET: Activities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = db.Activities.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        // GET: Activities/Create
        [Authorize(Roles = "teacher")]
        public ActionResult Create(int? courseId)
        {
            if (courseId == null)
                return RedirectToAction("Index", "Courses", null);

            else
            {
                var course = db.Courses.FirstOrDefault(c => c.Id == courseId);
                if (course == null)
                    return RedirectToAction("Index", "Courses", null);

                var model = new CreateActivityViewModel
                {
                    CourseName = course.Name,
                    CourseStart = course.StartDate.ToShortDateString(),
                    CourseEnd = course.EndDate.ToShortDateString(),
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today
                };

                var modules = course.Modules.OrderBy(m => m.Name).ToList();
                ViewBag.ModuleId = new SelectList(modules, "Id", "Name");
                ViewBag.ActivityTypeId = new SelectList(db.ActivityTypes, "Id", "Name");

                return View(model);
            }
        }

        // POST: Activities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "teacher")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateActivityViewModel viewModel)
        {
            if (viewModel.ActivityTypeId == 0)
                ModelState.AddModelError("ActivityTypeId", "Du måste skapa en aktivitetstyp innan du skapar en aktivitet");

            // Check if Activity with this Name already exist    
            if (db.Activities.Any(m => m.Name == viewModel.Name))
            {
                ModelState.AddModelError("Name", "Det finns redan en aktivitet med detta namn");
            }

            if (viewModel.StartDate.CompareTo(viewModel.EndDate) == 1)
            {
                ModelState.AddModelError("EndDate", "Slutdatum kan inte inträffa innan startdatum");
            }

            Module module = db.Modules.Find(viewModel.ModuleId);
            var info = $"Modulen \"{module.Name}\" pågår mellan {module.StartDate.ToShortDateString()} - {module.EndDate.ToShortDateString()}";
            var showInfo = false;

            if (module.StartDate.CompareTo(viewModel.StartDate) == 1)
            {
                ModelState.AddModelError("StartDate", "Aktivitetens Startdatum kan inte inträffa innan modulen startar");
                showInfo = true;
            }

            if (module.StartDate.CompareTo(viewModel.EndDate) == 1)
            {
                ModelState.AddModelError("EndDate", "Aktivitetens Slutdatum kan inte inträffa innan modulen startar");
                showInfo = true;
            }

            if (viewModel.EndDate.CompareTo(module.EndDate) == 1)
            {
                ModelState.AddModelError("EndDate", "Aktivitetens Slutdatum kan inte inträffa efter att modulen har slutat");
                showInfo = true;
            }

            if (viewModel.StartDate.CompareTo(module.EndDate) == 1)
            {
                ModelState.AddModelError("StartDate", "Aktivitetens Startdatum kan inte inträffa efter att modulen har slutat");
                showInfo = true;
            }

            if (ModelState.IsValid)
            {
                Activity activity = viewModel;
                db.Activities.Add(activity);
                db.SaveChanges();
                return RedirectToAction("Details", "Courses", new { id = viewModel.CourseId });
            }

            if (showInfo)
                ModelState.AddModelError("", info);

            var course = db.Courses.FirstOrDefault(c => c.Id == viewModel.CourseId);
            var modules = course.Modules.OrderBy(m => m.Name).ToList();
            ViewBag.ModuleId = new SelectList(modules, "Id", "Name");
            ViewBag.ActivityTypeId = new SelectList(db.ActivityTypes, "Id", "Name", viewModel.ActivityTypeId);

            return View(viewModel);
        }

        // GET: Activities/Edit/5
        [Authorize(Roles = "teacher")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = db.Activities.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            ViewBag.ModuleId = new SelectList(db.Modules, "Id", "Name", activity.ModuleId);
            ViewBag.ActivityTypeId = new SelectList(db.ActivityTypes, "Id", "Name", activity.ActivityTypeId);
            return View(activity);
        }

        // POST: Activities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "teacher")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,StartDate,EndDate,ActivityTypeId,ModuleId")] Activity activity)
        {
            // Check if Activity with this Name already exist            
            if (db.Activities.Any(m => m.Name == activity.Name))
            {
                ModelState.AddModelError("Name", "Det finns redan en aktivitet med detta namn");
            }

            if (activity.StartDate.CompareTo(activity.EndDate) == 1)
            {
                ModelState.AddModelError("EndDate", "Slutdatum kan inte inträffa innan startdatum");
            }

            Module module = db.Modules.Find(activity.ModuleId);

            if (module.StartDate.CompareTo(activity.StartDate) == 1)
            {
                ModelState.AddModelError("StartDate", "Aktivitetens Startdatum kan inte inträffa innan modulen startar");
            }

            if (module.StartDate.CompareTo(activity.EndDate) == 1)
            {
                ModelState.AddModelError("EndDate", "Aktivitetens Slutdatum kan inte inträffa innan modulen startar");
            }

            if (activity.EndDate.CompareTo(module.EndDate) == 1)
            {
                ModelState.AddModelError("EndDate", "Aktivitetens Slutdatum kan inte inträffa efter att modulen har slutat");
            }

            if (activity.StartDate.CompareTo(module.EndDate) == 1)
            {
                ModelState.AddModelError("StartDate", "Aktivitetens Startdatum kan inte inträffa efter att modulen har slutat");
            }

            if (ModelState.IsValid)
            {
                db.Entry(activity).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Courses", new { id = activity.Module.CourseId });
            }
            ViewBag.ModuleId = new SelectList(db.Modules, "Id", "Name", activity.ModuleId);
            ViewBag.ActivityTypeId = new SelectList(db.ActivityTypes, "Id", "Name", activity.ActivityTypeId);
            return View(activity);
        }

        // GET: Activities/Delete/5
        [Authorize(Roles = "teacher")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = db.Activities.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        // POST: Activities/Delete/5
        [Authorize(Roles = "teacher")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Activity activity = db.Activities.Find(id);
            int courseId = activity.Module.CourseId;
            db.Activities.Remove(activity);
            db.SaveChanges();
            return RedirectToAction("Details", "Courses", new { id = courseId });
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
