using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvcProjectHospital;

namespace MvcProjectHospital.Controllers
{
    public class PatientsController : Controller
    {
        private HospitalContext db = new HospitalContext();

        // GET: Patients
        public ActionResult Index()
        {
            return View(db.Patients.ToList());
        }

        [HttpPost]
        public ActionResult Index(int patientId)
        {
            var searchedPatient = db.Patients.Where(p => p.PatientID == patientId).ToList();
            if (searchedPatient.Count == 0)
            {
                ViewBag.Message = "Patient with such patient was not found";
                return View(searchedPatient);
            }
            else
            {
                return RedirectToAction("Edit","Patients",new { id= patientId});
            }    
        }
        

        public ActionResult CheckExistingId(int PatientID)
        {
            bool idExist = false;
            var patient = db.Patients.FirstOrDefault(p => p.PatientID == PatientID);
            if (patient != null)
                idExist = true;
            return Json(idExist, JsonRequestBehavior.AllowGet);
        }

        // GET: Patients/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Patients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PatientID,Name,Gender,Age,Address,PhoneNo,Weight,Disease")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                db.Patients.Add(patient);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(patient);
        }

        // GET: Patients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // POST: Patients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PatientID,Name,Gender,Age,Address,PhoneNo,Weight,Disease")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                db.Entry(patient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(patient);
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
