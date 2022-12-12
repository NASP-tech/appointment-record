using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using medicalAppointments.DAL;
using medicalAppointments.Models;

namespace medicalAppointments.Controllers
{
    public class PacienteController : Controller
    {

        PacienteDAL _pacienteDAL = new PacienteDAL();
        // GET: Paciente
        public ActionResult Index()
        {
            var pacienteList = _pacienteDAL.GetAllPacientes();

            if(pacienteList.Count == 0)
            {
                TempData["InfoMessage"] = "Currently products not available in the Database.";
            }

            return View(pacienteList);
        }

        // GET: Paciente/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Paciente/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Paciente/Create
        [HttpPost]
        public ActionResult Create(Paciente paciente)
        {
            bool IsInserted = false;

            try
            {
                if (ModelState.IsValid)
                {
                    IsInserted = _pacienteDAL.InsertPaciente(paciente);

                    if (IsInserted)
                    {
                        TempData["SuccessMessage"] = "Pacient details saved successfully...!";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Pacient is already registered/Unable to save the pacient details.";
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

        // GET: Paciente/Edit/5
        public ActionResult Edit(int id)
        {
            var pacientes = _pacienteDAL.GetPacientesByID(id).FirstOrDefault();

            if (pacientes == null)
            {
                TempData["InfoMessage"] = "Pacient not available with ID" + id.ToString();
                return RedirectToAction("Index");
            }

            return View(pacientes);
        }

        // POST: Paciente/Edit/5
        [HttpPost, ActionName("Edit")]
        public ActionResult UpdatePacient(Paciente paciente)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool IsUpdated = _pacienteDAL.UpdatePaciente(paciente);

                    if (IsUpdated)
                    {
                        TempData["SuccessMessage"] = "Pacient details updated successfully...!";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Pacient is already registered/Unable to update the pacient details.";
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

        // GET: Paciente/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Paciente/Delete/5
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
