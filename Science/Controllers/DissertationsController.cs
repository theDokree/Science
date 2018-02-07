using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Science.Models;

namespace Science.Controllers
{
    public class DissertationsController : Controller
    {
        private ScienceContext db = new ScienceContext();

        // GET: Dissertations
        public ActionResult Index()
        {
            return View(db.Dissertations.ToList());
        }

        // GET: Dissertations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dissertation dissertation = db.Dissertations.Find(id);
            if (dissertation == null)
            {
                return HttpNotFound();
            }
            return View(dissertation);
        }

        // GET: Dissertations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Dissertations/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Author,Type,Rank,Index,Number,City,Date")] Dissertation dissertation)
        {
            if (ModelState.IsValid)
            {
                db.Dissertations.Add(dissertation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dissertation);
        }

        // GET: Dissertations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dissertation dissertation = db.Dissertations.Find(id);
            if (dissertation == null)
            {
                return HttpNotFound();
            }
            return View(dissertation);
        }

        // POST: Dissertations/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Author,Type,Rank,Index,Number,City,Date")] Dissertation dissertation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dissertation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dissertation);
        }

        // GET: Dissertations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dissertation dissertation = db.Dissertations.Find(id);
            if (dissertation == null)
            {
                return HttpNotFound();
            }
            return View(dissertation);
        }

        // POST: Dissertations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Dissertation dissertation = db.Dissertations.Find(id);
            db.Dissertations.Remove(dissertation);
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
