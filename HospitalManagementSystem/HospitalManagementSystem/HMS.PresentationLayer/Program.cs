using HospitalManagementSystem.HMS.BussinessLogicLayer;
using HospitalManagementSystem.HMS.EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.HMS.PresentationLayer
{
    class Program
    {
        static void Main(string[] args)
        {
            BL newObj = new BL();
        start:
            Console.WriteLine("******************************************************");
            Console.WriteLine("\t\tHospital Management System");
            Console.WriteLine("******************************************************");
            Console.Write("1 Add New Patient\t");
            Console.Write("2 Add New Treatment\n");
            Console.Write("3 Search Patients\t");
            Console.Write("4 Modify Patient\n");
            Console.Write("5 View All Patients\t");
            Console.Write("6 Lab Report Generation\n");
            Console.Write("7 Bill Generation\t");
            Console.Write("8 Exit\n\n");
            Console.Write("Enter your choice: ");
            char choice = char.Parse(Console.ReadLine());

            switch (choice)
            {
                default:
                    Console.WriteLine("Not Matched with any option");
                    Console.WriteLine("********************************************");
                    Console.WriteLine("Enter R for retry!");
                    if (Console.ReadLine() == "r")
                    {
                        Console.Clear();
                        goto start;
                    }
                    break;
                case ('1'):
                    try
                    {
                        Console.Clear();
                        Console.WriteLine("You chose 'Add Patient'");
                        Console.WriteLine("Please enter the following details");

                        Patient patient = new Patient();
                        Console.Write("Patient ID: ");
                        patient.patientID = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Patient Name: ");
                        patient.patientName = Console.ReadLine();
                        Console.Write("Age: ");
                        patient.patientAge = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Gender: ");
                        patient.patientGender = char.Parse(Console.ReadLine());
                        Console.Write("Address: ");
                        patient.patientAddress = Console.ReadLine();
                        Console.Write("Contact No.: ");
                        patient.patientContact = Console.ReadLine();
                        Console.Write("Weight: ");
                        patient.patientWeight = float.Parse(Console.ReadLine());

                        newObj.AddPatient(patient);
                    }
                    catch (Exception exception)
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.WriteLine(exception.Message);
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    Console.WriteLine("Enter N for Next Operation!");
                    if (Console.ReadLine() == "n")
                    {
                        Console.Clear();
                        goto start;
                    }
                    break;
                case ('2'):
                    try
                    {
                        Console.Clear();
                        Console.WriteLine("You chose 'Add Treatment'");
                        Console.WriteLine("Please enter the following details");

                        Treatment treatment = new Treatment();
                        Console.Write("Appointment ID: ");
                        treatment.appointmentId = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Patient ID: ");
                        treatment.patientId = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Doctor's Name: ");
                        treatment.doctorName = Console.ReadLine();
                        Console.Write("Room No.: ");
                        treatment.roomNo = Console.ReadLine();
                        Console.Write("Patient Type(in/out): ");
                        treatment.patientType = Console.ReadLine();

                        if (treatment.patientType == "in")
                        {
                            Console.Write("Date Of Visit: ");
                            treatment.dateOfVisit = DateTime.Parse(Console.ReadLine());
                            treatment.admissionDate = null;
                            treatment.dischargeDate = null;
                        }
                        else if (treatment.patientType == "out")
                        {
                            treatment.dateOfVisit = null;
                            Console.Write("Admission Date: ");
                            treatment.admissionDate = DateTime.Parse(Console.ReadLine());
                            Console.Write("Discharge Date: ");
                            treatment.dischargeDate = DateTime.Parse(Console.ReadLine());
                        }

                        newObj.AddTreatment(treatment);
                    }
                    catch (Exception exception)
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.WriteLine(exception.Message);
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    Console.WriteLine("Enter N for Next Operation!");
                    if (Console.ReadLine() == "n")
                    {
                        Console.Clear();
                        goto start;
                    }
                    break;
                case ('3'):
                    try
                    {
                        Console.Clear();
                        Console.WriteLine("You chose 'Search Patients'");
                        Console.WriteLine("Please enter the following details");
                        
                        Console.Write("Patient ID: ");
                        string pid = Console.ReadLine();
                        Console.Write("Doctor's Name: ");
                        string docName = Console.ReadLine();
                        Console.Write("Date Of Visit(dd/mm/yyyy): ");
                        string dOvisit = Console.ReadLine();
                        Console.Write("Admission Date(dd/mm/yyyy): ");
                        string admDate = Console.ReadLine();
                        Console.Write("Discharge Date(dd/mm/yyyy): ");
                        string dischDate = Console.ReadLine();
                        
                        newObj.SearchPatient(pid, docName, dOvisit, admDate, dischDate);
                    }
                    catch (Exception exception)
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.WriteLine(exception.Message);
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    Console.WriteLine("Enter N for Next Operation!");
                    if (Console.ReadLine() == "n")
                    {
                        Console.Clear();
                        goto start;
                    }
                    break;
                case ('4'):
                    try
                    {
                        Console.Clear();
                        Console.WriteLine("You chose 'Modify Patient'");
                        Console.WriteLine("Please enter the following details");

                        Patient patient = new Patient();
                        Console.Write("Patient ID: ");
                        patient.patientID = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Patient Name: ");
                        patient.patientName = Console.ReadLine();
                        Console.Write("Age: ");
                        patient.patientAge = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Gender: ");
                        patient.patientGender = char.Parse(Console.ReadLine());
                        Console.Write("Address: ");
                        patient.patientAddress = Console.ReadLine();
                        Console.Write("Contact No.: ");
                        patient.patientContact = Console.ReadLine();
                        Console.Write("Weight: ");
                        patient.patientWeight = float.Parse(Console.ReadLine());

                        newObj.UpdatePatient(patient);
                    }
                    catch (Exception exception)
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.WriteLine(exception.Message);
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    Console.WriteLine("Enter N for Next Operation!");
                    if (Console.ReadLine() == "n")
                    {
                        Console.Clear();
                        goto start;
                    }
                    break;
                case ('5'):
                    try
                    {
                        Console.Clear();
                        Console.WriteLine("You chose 'Add Treatment'");
                        Console.WriteLine("Please enter the following details");

                        newObj.ViewPatients();
                    }
                    catch (Exception exception)
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.WriteLine(exception.Message);
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    Console.WriteLine("Enter N for Next Operation!");
                    if (Console.ReadLine() == "n")
                    {
                        Console.Clear();
                        goto start;
                    }
                    break;
                case ('6'):
                    try
                    {
                        Console.Clear();
                        Console.WriteLine("You chose 'Lab Report Generation'");
                        Console.WriteLine("Please enter the following details");

                        LabReport labReport = new LabReport();
                        Console.Write($"Report ID: ");
                        labReport.reportID = Convert.ToInt32(Console.ReadLine());
                        Console.Write($"Patient Name: ");
                        labReport.patientName = Console.ReadLine();
                        Console.Write($"Test Date: ");
                        labReport.testDate = DateTime.Parse(Console.ReadLine());
                        Console.Write($"Test Type: ");
                        labReport.testType = Console.ReadLine();
                        Console.Write($"Doctor Name: ");
                        labReport.doctor = Console.ReadLine();
                        Console.Write($"Remarks: ");
                        labReport.remarks = Console.ReadLine();

                        newObj.AddReport(labReport);
                    }
                    catch (Exception exception)
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.WriteLine(exception.Message);
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    Console.WriteLine("Enter N for Next Operation!");
                    if (Console.ReadLine() == "n")
                    {
                        Console.Clear();
                        goto start;
                    }
                    break;
                case ('7'):
                    try
                    {
                        Console.Clear();
                        Console.WriteLine("You chose 'Bill Generation'");
                        Console.WriteLine("Please enter the following details");

                        Billing bill = new Billing();
                        Console.Write($"Bill ID: ");
                        bill.BillId = Convert.ToInt32(Console.ReadLine());
                        Console.Write($"Patient Name: ");
                        bill.treatmentId = Convert.ToInt32(Console.ReadLine());
                        Console.Write($"Test Date: ");
                        bill.doctorFees = Convert.ToInt32(Console.ReadLine());
                        Console.Write($"Test Type: ");
                        bill.roomCharges = Convert.ToInt32(Console.ReadLine());
                        Console.Write($"Doctor Name: ");
                        bill.totalDays = Convert.ToInt32(Console.ReadLine());
                        Console.Write($"Remarks: ");
                        bill.operationCharges = Convert.ToInt32(Console.ReadLine());
                        Console.Write($"Remarks: ");
                        bill.medicineFees = Convert.ToInt32(Console.ReadLine());
                        Console.Write($"Remarks: ");
                        bill.labFees = Convert.ToInt32(Console.ReadLine());
                        Console.Write($"Remarks: ");
                        bill.totalAmount = bill.doctorFees + (bill.roomCharges * bill.totalDays) + 
                                            bill.operationCharges + bill.medicineFees + bill.labFees;

                        newObj.AddBill(bill);
                    }
                    catch (Exception exception)
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.WriteLine(exception.Message);
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    Console.WriteLine("Enter N for Next Operation!");
                    if (Console.ReadLine() == "n")
                    {
                        Console.Clear();
                        goto start;
                    }
                    break;
                case '8':
                    {
                        Console.WriteLine("Thank You!!");
                        Environment.Exit(0);
                    }
                    break;
            }
        }
    }
}
