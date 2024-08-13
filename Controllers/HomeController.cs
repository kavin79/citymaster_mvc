using citymaster.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace citymaster.Controllers
{
    public class HomeController : Controller
    {

        private readonly CityContext db;

        public HomeController(CityContext context)
        {
            db = context;
        }

        public HomeController()
        {
            db = new CityContext();
        }
        // GET: Home
        public ActionResult Index(string searchby, string search)
        {
            if (searchby == "cityname")
            {
                var data = db.cities.Where(m => m.cityname.StartsWith(search)).ToList();
                return View(data);
            }

            else if (searchby == "intstateid")
            {
                var data = db.cities.Where(m => m.state.strstatename == search).ToList();
                return View(data);
            }

            else
            {

                if (Session["username"] == null)
                {
                    return RedirectToAction("Index", "Login");
                }

                var items = db.cities.OrderBy(c => c.cityid).ToList();
                return View(items);

            }



        }

        public ActionResult Create()
        {
            // Fetch states from the database
            var states = db.states.ToList();

            // Create a SelectList from states
            var stateList = new SelectList(states, "intstateid", "strstatename");

            // Pass the SelectList to the view
            ViewBag.StateList = stateList;
            return View();

        }

        [HttpPost]

        public ActionResult Create(citymaster.Models.citymaster city)
        {
            if (ModelState.IsValid)
            {
                db.cities.Add(city);
                int a = db.SaveChanges();
                if (a > 0)
                {
                    TempData["InsertMessage"] = "Data Inserted";
                }
                else
                {
                    TempData["InsertMessage"] = "Error : Data Not Inserted";
                }

                return RedirectToAction("Index");
            }

            // Fetch states from the database
            var states = db.states.ToList();

            // Create a SelectList from states
            var stateList = new SelectList(states, "intstateid", "strstatename");

            // Pass the SelectList to the view
            ViewBag.StateList = stateList;

            return View(city);
        }


        public ActionResult Delete(int id)
        {
            var city = db.cities.FirstOrDefault(c => c.cityid == id);
            if (city == null)
            {
                TempData["DeleteMessage"] = "Error: City not found";
                return RedirectToAction("Index");
            }
            return View(city);
        }

        [HttpPost, ActionName("Delete")]

        public ActionResult DeleteConfirm(int id)
        {
            try
            {
                var existingCity = db.cities.FirstOrDefault(c => c.cityid == id);
                if (existingCity == null)
                {
                    TempData["DeleteMessage"] = "Error: City not found";
                    return RedirectToAction("Index");
                }

                db.Entry(existingCity).State = EntityState.Deleted;
                int a = db.SaveChanges();
                if (a > 0)
                {
                    TempData["DeleteMessage"] = "Data Deleted";
                }
                else
                {
                    TempData["DeleteMessage"] = "Error : Data Not Deleted";
                }

                return RedirectToAction("Index");
            }
            catch (DbUpdateConcurrencyException ex)
            {
                TempData["DeleteMessage"] = "Concurrency Exception: " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(int id)
        {

            var states = db.states.ToList();

            // Create a SelectList from states
            var stateList = new SelectList(states, "intstateid", "strstatename");

            // Pass the SelectList to the view
            ViewBag.StateList = stateList;

            var row = db.cities.Where(model => model.cityid == id).FirstOrDefault();
            return View(row);
        }

        [HttpPost]
        public ActionResult Edit(citymaster.Models.citymaster c)
        {
            if (ModelState.IsValid)
            {
                db.Entry(c).State = EntityState.Modified;
                int a = db.SaveChanges();
                if (a > 0)
                {
                    TempData["UpdateMessage"] = "Data updated";
                }
                else
                {
                    TempData["UpdateMessage"] = "Error : Data Not updated";
                }

                return RedirectToAction("Index");
            }

            // Fetch states from the database
            var states = db.states.ToList();

            // Create a SelectList from states
            var stateList = new SelectList(states, "intstateid", "strstatename");

            // Pass the SelectList to the view
            ViewBag.StateList = stateList;

            return View(c);
        }

        public ActionResult Details(int id)
        {
            var details = db.cities.Where(model => model.cityid == id).FirstOrDefault(); return View(details);
        }
    }

}