using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Science.Models;

namespace Science.Controllers
{
    public class HomeController : Controller
    {
        Repository _repo = new Repository();

        public ActionResult Index()
        {
            ViewBag.Dissertations = _repo.getListDissertations();
            ViewBag.Types = _repo.getListTypes();
            return View();
        }

        [HttpGet]
        public ActionResult CreateDis()
        {
            SelectList types = new SelectList(_repo.getListTypes(), "Id", "Name");
            ViewBag.Types = types;
            return View();
        }

        [HttpPost]
        public ActionResult CreateDis(Dissertation dissertation)
        {
            _repo.addDissertation(dissertation);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult CreateType()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateType(Models.Type type)
        {
            _repo.addType(type);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Dissertation(int id)
        {
            Dissertation b = _repo.getDissertationFromId(id);
            return View(b);
        }

        [HttpGet]
        public ActionResult DeleteDis(int id)
        {
            Dissertation b = _repo.getDissertationFromId(id);
            return View(b);
        }

        [HttpPost, ActionName("DeleteDis")]
        public ActionResult DeleteDisConfirmed(int id)
        {
            _repo.dellDissertation(id);
            return RedirectToAction("Index");
        }
    }
}