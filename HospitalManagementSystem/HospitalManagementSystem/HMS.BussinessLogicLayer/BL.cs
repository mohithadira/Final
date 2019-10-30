using HospitalManagementSystem.HMS.DataAccessLayer;
using HospitalManagementSystem.HMS.EntityLayer;
using HospitalManagementSystem.HMS.ExceptionLayer;
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
            if (dataAccess.patients.Exists(x => x.patientID.Equals(patient.patientID)))
            {
                throw new UniquenessException();
            }
            if (patient.patientAge <= 0 || patient.patientAge >= 110)
            {
                throw new AgeException();
            } 
            dataAccess.patients.Add(patient);
            Console.WriteLine("New patient record added!\n\n");
        }

        public void AddTreatment(Treatment treatment)
        {
            int Id = treatment.appointmentId;
            if (dataAccess.treatments.Exists(x => x.appointmentId.Equals(Id)))
            {
                throw new UniquenessException();
            }
            var tempList = dataAccess.patients.FindAll(x => x.patientID.Equals(treatment.patientId));
            if (tempList.Count == 0)
            {
                throw new NoPatientException();
            }
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
            List<Treatment> tempTreatment = dataAccess.treatments;
            if(pId != "")
            {
                tempTreatment = tempTreatment.FindAll(s => s.patientId.ToString().Equals(pId));
            }
            if (dName != "")
            {
                tempTreatment = tempTreatment.FindAll(s => s.doctorName.Equals(dName));
            }
            if (dOvisit != "")
            {
                tempTreatment = tempTreatment.FindAll(s => s.dateOfVisit.Equals(DateTime.Parse(dOvisit)));
            }
            if (admDate != "")
            {
                tempTreatment = tempTreatment.FindAll(s => s.dateOfVisit.Equals(DateTime.Parse(admDate)));
            }
            if (dischDate != "")
            {
                tempTreatment = tempTreatment.FindAll(s => s.dateOfVisit.Equals(DateTime.Parse(dischDate)));
            }
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
            int Id = billing.BillId;
            if (dataAccess.bills.Exists(x => x.BillId.Equals(Id)))
            {
                throw new UniquenessException();
            }
            var tempList = dataAccess.treatments.FindAll(x => x.appointmentId.Equals(billing.treatmentId));
            if (tempList.Count == 0)
            {
                throw new NoPatientException();
            }
            if (billing.doctorFees < 0)
            {
                throw new AmountException();
            }
            if(billing.labFees < 0)
            {
                throw new AmountException();
            }
            if (billing.medicineFees < 0)
            {
                throw new AmountException();
            }
            if(billing.operationCharges < 0)
            {
                throw new AmountException();
            }
            if (billing.roomCharges < 0)
            {
                throw new AmountException();
            }
            if(billing.totalDays < 0)
            {
                throw new DaysException();

            }
            dataAccess.bills.Add(billing);
            Console.WriteLine("Bill generated!\n\n");
        }

        public List<Billing> SearchBill(int bId)
        {
            List<Billing> tempBill = new List<Billing>();
            tempBill = dataAccess.bills.FindAll(x => x.BillId.Equals(bId));
            foreach (var item in tempBill)
            {
                Console.WriteLine($"Bill ID : {item.BillId}");
                Console.WriteLine($"Treatment ID : {item.treatmentId}");
                Console.WriteLine($"Doctor Fees : {item.doctorFees}");
                Console.WriteLine($"Room Charges : {item.roomCharges}");
                Console.WriteLine($"Total Days : {item.totalDays}");
                Console.WriteLine($"Operation Charges : {item.operationCharges}");
                Console.WriteLine($"Medicine Fees : {item.medicineFees}");
                Console.WriteLine($"Lab Fees : {item.labFees}");
                Console.WriteLine($"Total Amount : {item.totalAmount}");
            }
            return tempBill;
        }
    }
}