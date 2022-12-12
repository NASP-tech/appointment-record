using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using medicalAppointments.DAL;
using medicalAppointments.Models;

namespace medicalAppointments.Controllers
{
    public class CitasController : Controller
    {
        CitasDAL _citasDal = new CitasDAL();

        // GET: Citas
        public ActionResult Index()
        {
            var citasList = _citasDal.GetAllCitas();

            if (citasList.Count == 0)
            {
                TempData["InfoMessage"] = "Currently appointments not available in the Database.";
            }

            return View(citasList);
        }

        // GET: Citas/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Citas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Citas/Create
        [HttpPost]
        public ActionResult Create(Citas citas)
        {
            bool IsInserted = false;

            try
            {
                if (ModelState.IsValid)
                {
                    IsInserted = _citasDal.InsertCitas(citas);

                    if (IsInserted)
                    {
                        TempData["SuccessMessage"] = "Appointment details saved successfully...!";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Appointmet is already registered/Unable to save the pacient details.";
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                TempData["ErrorMessage"] = ex.Message;
                return View();

            }
        }

        // GET: Citas/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Citas/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Citas/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Citas/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
