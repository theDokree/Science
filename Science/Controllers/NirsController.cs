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
    public class NirsController : Controller
    {
        private ScienceContext db = new ScienceContext();

        // GET: Nirs
        public ActionResult Index()
        {
            return View(db.Nirs.ToList());
        }

        // GET: Nirs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nir nir = db.Nirs.Find(id);
            if (nir == null)
            {
                return HttpNotFound();
            }
            return View(nir);
        }

        // GET: Nirs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Nirs/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Annotation,Index,Base,Finance,Kind")] Nir nir)
        {
            if (ModelState.IsValid)
            {
                db.Nirs.Add(nir);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nir);
        }

        // GET: Nirs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nir nir = db.Nirs.Find(id);
            if (nir == null)
            {
                return HttpNotFound();
            }
            return View(nir);
        }

        // POST: Nirs/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Annotation,Index,Base,Finance,Kind")] Nir nir)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nir).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nir);
        }

        // GET: Nirs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nir nir = db.Nirs.Find(id);
            if (nir == null)
            {
                return HttpNotFound();
            }
            return View(nir);
        }

        // POST: Nirs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Nir nir = db.Nirs.Find(id);
            db.Nirs.Remove(nir);
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
