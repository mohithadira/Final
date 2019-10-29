using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using MvcProjectHospital;

namespace MvcProjectHospital.Controllers
{
    public class BillDatasController : Controller
    {
        private HospitalContext db = new HospitalContext();

        // GET: BillDatas
        public ActionResult Index()
        {
            var billDatas = db.BillDatas.Include(b => b.PatientTreatment).Include(b => b.Patient);
            return View(billDatas.ToList());
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
                return RedirectToAction("Details", "BillDatas", new { id = patientId });
            }
        }

        public ActionResult Details(int? id)
        {
            BillData bill = new BillData();
            PatientTreatment objTreat = new PatientTreatment();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            

            if (objTreat.PatientType == "Inpatient")
            {
                bill.TotalAmount = (bill.DoctorFees + (int)bill.LabFees + bill.MedicineFees + (int)bill.OperationCharge + (int)bill.RoomCharge);
            }
            if (objTreat.PatientType == "Outpatient")
            {
                bill.TotalAmount = (bill.DoctorFees + bill.LabFees + bill.MedicineFees);
            }
            List<BillData> billData = db.BillDatas.Where(s => s.PatientID == id).ToList();

            string doctorName = db.PatientTreatments.Where(s => s.PatientID == id).ToList()[0].DoctorName;
            ViewData["doctorName"] = doctorName;

            if (billData == null)
            {
                return HttpNotFound();
            }
            return View(billData);
        }

        // GET: BillDatas/Create
        public ActionResult Create(int appointmentId, int patientId)
        {
            ViewData["appointmentId"] = appointmentId;
            ViewData["patientId"] = patientId;
            ViewBag.patientType = db.PatientTreatments.ToList().Find(p=>p.AppointmentId == appointmentId).PatientType;
            return View();
        }

        // POST: BillDatas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BillNo,DoctorFees,RoomCharge,OperationCharge,MedicineFees,TotalDays,LabFees,TotalAmount,PatientID,AppointmentId")] BillData billData)
        {
            if (ModelState.IsValid)
            {
                int ? total = billData.MedicineFees + billData.LabFees + billData.DoctorFees;
                //billData.TotalAmount = Convert.ToInt32(total.ToString());
                billData.TotalAmount = total.GetValueOrDefault();
                if(db.PatientTreatments.Where(s=>s.AppointmentId == billData.AppointmentId).ToList()[0].PatientType == "Inpatient")
                {
                    total = total + billData.OperationCharge + (billData.RoomCharge * billData.TotalDays);
                    billData.TotalAmount = total.GetValueOrDefault();
                }
                db.BillDatas.Add(billData);
                db.SaveChanges();
                //ScriptManager.RegisterStartupScript(,this.GetType(),"patientData","alert('Patient Treatment data added')",true);
                return RedirectToAction("Index","Home");
            }

            ViewBag.AppointmentId = new SelectList(db.PatientTreatments, "AppointmentId", "DoctorName", billData.AppointmentId);
            ViewBag.PatientID = new SelectList(db.Patients, "PatientID", "Name", billData.PatientID);
            return View(billData);
        }

        // GET: BillDatas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BillData billData = db.BillDatas.Find(id);
            if (billData == null)
            {
                return HttpNotFound();
            }
            ViewBag.AppointmentId = new SelectList(db.PatientTreatments, "AppointmentId", "DoctorName", billData.AppointmentId);
            ViewBag.PatientID = new SelectList(db.Patients, "PatientID", "Name", billData.PatientID);
            return View(billData);
        }

        // POST: BillDatas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BillNo,DoctorFees,RoomCharge,OperationCharge,MedicineFees,TotalDays,LabFees,TotalAmount,PatientID,AppointmentId")] BillData billData)
        {
            if (ModelState.IsValid)
            {
                db.Entry(billData).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AppointmentId = new SelectList(db.PatientTreatments, "AppointmentId", "DoctorName", billData.AppointmentId);
            ViewBag.PatientID = new SelectList(db.Patients, "PatientID", "Name", billData.PatientID);
            return View(billData);
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
