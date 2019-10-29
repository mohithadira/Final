using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMS.Entities;
using HMS.DataAccessLayer;
using HMS.Exceptions;
using System.Data;
using System.Text.RegularExpressions;

namespace HMS.BusinessLayer
{
    public class AppointmentBL
    {
        public static DataTable SearchPatientInfoBL(int? pId, string doctorName, DateTime? dateOfVisit, DateTime? dateOfAdmission)
        {
            DataTable patients;
            try
            {
                AppointmentDAL appointmentDAL = new AppointmentDAL();
                patients = appointmentDAL.SearchPatientInfoDAL(pId, doctorName, dateOfVisit, dateOfAdmission);
            }
            catch (PatientException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return patients;
        }



        public static bool AddPatientTreatmentInfoBL(PatientAppointmentData patientTreatmentData)
        {
            bool dataAdded = false;
            try
            {
                if (Validate(patientTreatmentData))
                {
                    AppointmentDAL appointmentDAL = new AppointmentDAL();
                    dataAdded = appointmentDAL.AddPatientTreatmentInfoDAL(patientTreatmentData);
                }
            }
            catch (PatientException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dataAdded;
        }

        private static bool Validate(PatientAppointmentData patientTreatmentData)
        {
            bool validatePatientData = true;
            StringBuilder sb = new StringBuilder();
            if (patientTreatmentData.DoctorName == null)
            {
                validatePatientData = false;
                sb.Append("Doctor Name should not be null" + Environment.NewLine);
            }
            if (patientTreatmentData.LabData.TestType == null)
            {
                validatePatientData = false;
                sb.Append("Test type should not be null" + Environment.NewLine);
            }
            if (patientTreatmentData.Bill.DoctorFees < 0)
            {
                validatePatientData = false;
                sb.Append("Doctor fees should not be less than zero" + Environment.NewLine);
            }
            if (patientTreatmentData.Bill.LabFees < 0)
            {
                validatePatientData = false;
                sb.Append("Lab fees should not be less than zero" + Environment.NewLine);
            }
            if (patientTreatmentData.Bill.MedicineFees < 0)
            {
                validatePatientData = false;
                sb.Append("Medicine fees should not be less than zero" + Environment.NewLine);
            }
            if (!Isvalid(patientTreatmentData.LabData.TestDate.ToString().Split(' ')[0]) && patientTreatmentData.LabData.TestDate!=null)
            {
                validatePatientData = false;
                sb.Append("Test date is not in the correct format");
            }
            if (patientTreatmentData.PatientType.ToLower() == "in")
            {
                if (!Isvalid(patientTreatmentData.DischargeDate.ToString().Split(' ')[0]) && patientTreatmentData.DischargeDate != null)
                {
                    validatePatientData = false;
                    sb.Append("Discharge date is not in the correct format" + Environment.NewLine);
                }
                if (!Isvalid(patientTreatmentData.AdmissionDate.ToString().Split(' ')[0]) && patientTreatmentData.AdmissionDate != null)
                {
                    validatePatientData = false;
                    sb.Append("Admission date is not in the correct format" + Environment.NewLine);
                }

                if (patientTreatmentData.RoomNo == null)
                {
                    validatePatientData = false;
                    sb.Append("Room No should not be null" + Environment.NewLine);
                }
                if (patientTreatmentData.Bill.OperationCharge < 0)
                {
                    validatePatientData = false;
                    sb.Append("Operation charges should not be null" + Environment.NewLine);
                }
                if (patientTreatmentData.Bill.RoomCharge < 0)
                {
                    validatePatientData = false;
                    sb.Append("Room charges should not be null" + Environment.NewLine);
                }
                if (patientTreatmentData.Bill.TotalDays <= 0)
                {
                    validatePatientData = false;
                    sb.Append("Total days should not be zero or less than zero" + Environment.NewLine);
                }
                if (patientTreatmentData.AdmissionDate >= patientTreatmentData.DischargeDate)
                {
                    validatePatientData = false;
                    sb.Append("Discharge date should be greater than or equal to admission date" + Environment.NewLine);
                }
                if (patientTreatmentData.AdmissionDate <= patientTreatmentData.LabData.TestDate && patientTreatmentData.DischargeDate >= patientTreatmentData.LabData.TestDate)
                {
                    validatePatientData = false;
                    sb.Append("Test date should be between admission date & discharge date" + Environment.NewLine);
                }
            }
            else
            {
                if (!Isvalid(patientTreatmentData.DateOfVisit.ToString().Split(' ')[0]) && patientTreatmentData.DateOfVisit != null)
                {
                    validatePatientData = false;
                    sb.Append("Date of visit is not in the correct format" + Environment.NewLine);
                }
                if (patientTreatmentData.DateOfVisit >= patientTreatmentData.LabData.TestDate)
                {
                    validatePatientData = false;
                    sb.Append("Test date should be greater than or equal to date of visit" + Environment.NewLine);
                }
            }
            if (validatePatientData == false)
                throw new PatientException(sb.ToString());
            return validatePatientData;
        }

        private static bool Isvalid(string value)
        {
            return Regex.IsMatch(value, "^([0]?[0-9]|[12][0-9]|[3][01])[-]([0]?[1-9]|[1][0-2])[-]([0-9]{4}|[0-9]{2})$");
        }



        public static DataTable GetAllPatientInfoBL()
        {
            try
            {
                AppointmentDAL appointmentDAL = new AppointmentDAL();
                DataTable patients = appointmentDAL.GetAllPatientInfoDAL();
                return patients;
            }
            catch (PatientException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable GetLabReportBL(int patientId)
        {
            DataTable labData = null;
            try
            {
                AppointmentDAL appointmentDAL = new AppointmentDAL();
                labData = appointmentDAL.GetLabReportDAL(patientId);
            }
            catch (PatientException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return labData;
        }


        public static DataTable GetBillDataBL(int patientId)
        {
            DataTable billData = null;
            try
            {
                AppointmentDAL appointmentDAL = new AppointmentDAL();
                billData = appointmentDAL.GetBillDataDAL(patientId);
            }
            catch (PatientException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return billData;
        }

        public static int? GetLabIdBL()
        {
            int? lab = null;
            try
            {
                AppointmentDAL appointmentDAL = new AppointmentDAL();
                lab = appointmentDAL.GetLabIdDAL();
            }
            catch (PatientException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lab;
        }


        public static int? GetBillDataIdBL()
        {
            int? billData = null;
            try
            {
                AppointmentDAL appointmentDAL = new AppointmentDAL();
                billData = appointmentDAL.GetBillDataIdDAL();
            }
            catch (PatientException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return billData;
        }


        public static int? GetPatientTreatmentIdBL()
        {
            int? patientTreatment = null;
            try
            {
                AppointmentDAL appointmentDAL = new AppointmentDAL();
                patientTreatment = appointmentDAL.GetPatientTreatmentIdDAL();
            }
            catch (PatientException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return patientTreatment;
        }

        //public static List<PatientAppointmentData> SearchPatientByDoctorBL(string doctorName)
        //{
        //    List<PatientAppointmentData> patients = null;
        //    try
        //    {
        //        AppointmentDAL appointmentDAL = new AppointmentDAL();
        //        patients = appointmentDAL.SearchPatientByDoctorDAL(doctorName);
        //    }
        //    catch (PatientException ex)
        //    {
        //        throw ex;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return patients;
        //}

        //public static List<PatientAppointmentData> SearchPatientByVisitDateBL(DateTime dateOfVisit)
        //{
        //    List<PatientAppointmentData> patients = null;
        //    try
        //    {
        //        AppointmentDAL appointmentDAL = new AppointmentDAL();
        //        patients = appointmentDAL.SearchPatientByVisitDateDAL(dateOfVisit);
        //    }
        //    catch (PatientException ex)
        //    {
        //        throw ex;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return patients;
        //}

        //public static List<PatientAppointmentData> SearchPatientByAdmissionDateBL(DateTime dateOfAdmission)
        //{
        //    List<PatientAppointmentData> patients = null;
        //    try
        //    {
        //        AppointmentDAL appointmentDAL = new AppointmentDAL();
        //        patients = appointmentDAL.SearchPatientByAdmissionDateDAL(dateOfAdmission);
        //    }
        //    catch (PatientException ex)
        //    {
        //        throw ex;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return patients;
        //}
        //public static PatientAppointmentData SearchPatientByIdBL(int pId)
        //{
        //    PatientAppointmentData patient = null;
        //    try
        //    {
        //        AppointmentDAL appointmentDAL = new AppointmentDAL();
        //        patient = appointmentDAL.SearchPatientByIdDAL(pId);
        //    }
        //    catch (PatientException ex)
        //    {
        //        throw ex;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return patient;
        //}


    }
}
