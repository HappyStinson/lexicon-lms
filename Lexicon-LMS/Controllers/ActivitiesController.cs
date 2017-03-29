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
        public ActionResult Create()
        {
            ViewBag.ModuleId = new SelectList(db.Modules, "Id", "Name");
            ViewBag.ActivityTypeId = new SelectList(db.ActivityTypes, "Id", "Name");
            return View();
        }

        // POST: Activities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "teacher")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,StartDate,EndDate,ActivityTypeId,ModuleId")] Activity activity)
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
                db.Activities.Add(activity);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ModuleId = new SelectList(db.Modules, "Id", "Name", activity.ModuleId);
            ViewBag.ActivityTypeId = new SelectList(db.ActivityTypes, "Id", "Name", activity.ActivityTypeId);
            return View(activity);
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
            bool activitySameName = false;
            var activities = db.Activities.Where(a => a.Id != activity.Id).ToList();         

            foreach (var item in activities)
            {
                if (item.Name.Equals(activity.Name))
                {
                    activitySameName = true;
                }
            }
            if (activitySameName == true)
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
                return RedirectToAction("Index");
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
            db.Activities.Remove(activity);
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
