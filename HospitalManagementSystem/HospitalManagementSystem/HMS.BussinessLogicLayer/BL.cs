using HospitalManagementSystem.HMS.DataAccessLayer;
using HospitalManagementSystem.HMS.EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.HMS.BussinessLogicLayer
{
    class BL
    {
        DataAccess dataAccess = new DataAccess(); 

        public void AddPatient(Patient patient)
        {
            dataAccess.patients.Add(patient);
            Console.WriteLine("New patient record added!\n\n");
        }

        public void AddTreatment(Treatment treatment)
        {

            dataAccess.treatments.Add(treatment);
            Console.WriteLine("New treartment record added!\n\n");
        }

        public void UpdatePatient(Patient patient)
        {
            Patient temp = dataAccess.patients.Find(x => x.patientID.Equals(patient.patientID));

            Console.WriteLine("Original Data");
            Console.Write($"Patient ID : {temp.patientID}\t");
            Console.Write($"Patient Name : {temp.patientName}\n");
            Console.Write($"Gender : {temp.patientGender}\t");
            Console.Write($"Age : {temp.patientAge}\n");
            Console.Write($"Address : {temp.patientAddress}\n");
            Console.Write($"Contact Number : {temp.patientContact}\n");
            Console.Write($"Weight : {temp.patientWeight}\n\n");

            temp = patient;

            Console.WriteLine("Modified Date");
            Console.Write($"Patient ID : {temp.patientID}\t");
            Console.Write($"Patient Name : {temp.patientName}\n");
            Console.Write($"Gender : {temp.patientGender}\t");
            Console.Write($"Age : {temp.patientAge}\n");
            Console.Write($"Address : {temp.patientAddress}\n");
            Console.Write($"Contact Number : {temp.patientContact}\n");
            Console.Write($"Weight : {temp.patientWeight}\n\n");
        }

        public List<Patient> SearchPatient(string pId, string dName, string dOvisit, string admDate, string dischDate)
        {
            List<Treatment> tempTreatment = dataAccess.treatments.FindAll(x => (x.patientId.Equals(pId) || x.doctorName.Equals(dName) ||
                        x.dateOfVisit.ToString().Equals(dOvisit)) || x.dischargeDate.ToString().Equals(dischDate) || x.admissionDate.ToString().Equals(admDate));
            List<Patient> tempPatient = new List<Patient>();
            foreach (var item in tempTreatment)
            {
                tempPatient.Add(dataAccess.patients.Find(x => x.patientID.Equals(item.patientId)));
            }

            foreach (var item in tempPatient)
            {
                Console.Write($"Patient ID : {item.patientID}\t");
                Console.Write($"Patient Name : {item.patientName}\n");
                Console.Write($"Gender : {item.patientGender}\t");
                Console.Write($"Age : {item.patientAge}\n");
                Console.Write($"Address : {item.patientAddress}\n");
                Console.Write($"Contact Number : {item.patientContact}\n");
                Console.Write($"Weight : {item.patientWeight}\n\n");
            }
            return tempPatient;
        }

        public void ViewPatients()
        {
            foreach (var item in dataAccess.patients)
            {
                Console.Write($"Patient ID : {item.patientID}\t");
                Console.Write($"Patient Name : {item.patientName}\n");
                Console.Write($"Gender : {item.patientGender}\t");
                Console.Write($"Age : {item.patientAge}\n");
                Console.Write($"Address : {item.patientAddress}\n");
                Console.Write($"Contact Number : {item.patientContact}\n");
                Console.Write($"Weight : {item.patientWeight}\n\n");
            }
        }

        public void AddReport(LabReport labReport)
        {
            dataAccess.labReports.Add(labReport);
            Console.WriteLine("Report Generated!");

            Console.Write($"Report ID : {labReport.reportID}\t");
            Console.Write($"Patient Name : {labReport.patientName}\n");
            Console.Write($"Test Date : {labReport.testDate}\t");
            Console.Write($"Test Type : {labReport.testType}\n");
            Console.Write($"Doctor Name : {labReport.doctor}\n");
            Console.Write($"Remarks : {labReport.remarks}\n\n");
        }

        public void AddBill(Billing billing)
        {
            dataAccess.bills.Add(billing);
            Console.WriteLine("Bill generated!\n\n");
        }
    }
}