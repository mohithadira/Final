using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;


namespace MvcProjectHospital.Controllers
{
    public class PatientTreatmentsController : Controller
    {
        private HospitalContext db = new HospitalContext();

        // GET: PatientTreatments
        public ActionResult Index()
        {
            var patientTreatments = db.PatientTreatments.Include(p => p.Patient);
            return View(patientTreatments.ToList());
        }

        [HttpPost]
        public ActionResult Index(string searchByID, string searchByDoctor, string searchByVisitDate, string searchByAdmissionDate)
        {
            var tempList = db.PatientTreatments.ToList();
            if(searchByID != "")
            {
                int pID = Convert.ToInt32(searchByID);
                tempList = tempList.Where(s => s.PatientID == pID).ToList();
            }
            if (searchByDoctor != "")
            {
                tempList = tempList.Where(s => s.DoctorName == searchByDoctor).ToList();
            }
            if (searchByVisitDate !="")
            {
                var date = Convert.ToDateTime(searchByVisitDate);
                tempList = tempList.Where(s => s.DateOfVisit == date).ToList();

            }
            if (searchByAdmissionDate != "")
            {
                var date = Convert.ToDateTime(searchByAdmissionDate);
                tempList = tempList.Where(s => s.AdmissionDate == date).ToList();
            } 
            return View(tempList);
        }

        // GET: PatientTreatments/Create
        public ActionResult Create()
        {
            ViewBag.PatientID = new SelectList(db.Patients, "PatientID", "Name");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AppointmentId,PatientID,DoctorName,RoomNo,DateOfVisit,AdmissionDate,DischargeDate,PatientType")] PatientTreatment patientTreatment)
        {
            if (ModelState.IsValid)
            {
                db.PatientTreatments.Add(patientTreatment);
                db.SaveChanges();           
                return RedirectToAction("Create","Labs", new { appointmentId = patientTreatment.AppointmentId, patientId = patientTreatment.PatientID});
            }
            ViewBag.PatientID = new SelectList(db.Patients, "PatientID", "Name", patientTreatment.PatientID);
            return View(patientTreatment);
        }

        // GET: PatientTreatments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PatientTreatment patientTreatment = db.PatientTreatments.Find(id);
            if (patientTreatment == null)
            {
                return HttpNotFound();
            }
            ViewBag.PatientID = new SelectList(db.Patients, "PatientID", "Name", patientTreatment.PatientID);
            return View(patientTreatment);
        }

        // POST: PatientTreatments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AppointmentId,PatientID,DoctorName,RoomNo,DateOfVisit,AdmissionDate,DischargeDate,PatientType")] PatientTreatment patientTreatment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(patientTreatment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PatientID = new SelectList(db.Patients, "PatientID", "Name", patientTreatment.PatientID);
            return View(patientTreatment);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult CheckID()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PatientHistory(string history)
        {
            if (history == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int id = Convert.ToInt32(history);
            List<PatientTreatment> patientData = db.PatientTreatments.Where(s => s.PatientID == id).ToList();
            string testType = db.Labs.Where(s => s.PatientID == id).ToList()[0].TestType;
            int reportID = db.Labs.Where(s => s.PatientID == id).ToList()[0].ReportId;
            string remarks = db.Labs.Where(s => s.PatientID == id).ToList()[0].Remarks;

            ViewData["testType"] = testType;
            ViewData["remarks"] = remarks;
            ViewData["reportID"] = reportID;

            return View(patientData);
        }
    }
}
