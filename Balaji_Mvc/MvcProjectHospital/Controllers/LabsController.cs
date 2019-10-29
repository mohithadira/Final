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
    public class LabsController : Controller
    {
        private HospitalContext db = new HospitalContext();

        // GET: Labs
        public ActionResult Index()
        {
            var labs = db.Labs.Include(l => l.PatientTreatment).Include(l => l.Patient);
            return View(labs.ToList());
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
                return RedirectToAction("Details", "Labs", new { id = patientId });
            }
        }

        //[HttpPost]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            List<Lab> lab = db.Labs.Where(s => s.PatientID == id).ToList();
            string doctorName = db.PatientTreatments.Where(s => s.PatientID == id).ToList()[0].DoctorName;
            string patientType = db.PatientTreatments.Where(s => s.PatientID == id).ToList()[0].PatientType;


            ViewData["doctorName"] = doctorName;
            ViewData["patientType"] = patientType;

            if (lab == null)
            {
                return HttpNotFound();
            }
            return View(lab);
        }

        // GET: Labs/Create
        public ActionResult Create(int appointmentId, int patientId)
        {
            ViewBag.AppointmentId = new SelectList(db.PatientTreatments, "AppointmentId", "DoctorName");
            ViewBag.PatientID = new SelectList(db.Patients, "PatientID", "Name");
            ViewData["appointmentId"] = appointmentId;
            ViewData["patientId"] = patientId;
            return View();
        }

        // POST: Labs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReportId,TestDate,Remarks,TestType,PatientID,AppointmentId")] Lab lab)
        {
            if (ModelState.IsValid)
            {
                db.Labs.Add(lab);
                db.SaveChanges();
                return RedirectToAction("Create","BillDatas",new { appointmentId = lab.AppointmentId, patientId = lab.PatientID});
            }

            ViewBag.AppointmentId = new SelectList(db.PatientTreatments, "AppointmentId", "DoctorName", lab.AppointmentId);
            ViewBag.PatientID = new SelectList(db.Patients, "PatientID", "Name", lab.PatientID);
            return View(lab);
        }

        // GET: Labs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lab lab = db.Labs.Find(id);
            if (lab == null)
            {
                return HttpNotFound();
            }
            ViewBag.AppointmentId = new SelectList(db.PatientTreatments, "AppointmentId", "DoctorName", lab.AppointmentId);
            ViewBag.PatientID = new SelectList(db.Patients, "PatientID", "Name", lab.PatientID);
            return View(lab);
        }

        // POST: Labs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReportId,TestDate,Remarks,TestType,PatientID,AppointmentId")] Lab lab)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lab).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AppointmentId = new SelectList(db.PatientTreatments, "AppointmentId", "DoctorName", lab.AppointmentId);
            ViewBag.PatientID = new SelectList(db.Patients, "PatientID", "Name", lab.PatientID);
            return View(lab);
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
