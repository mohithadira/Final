using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMS.DataAccessLayer;
using HMS.Entities;
using HMS.Exceptions;
using System.Data;
namespace HMS.BusinessLayer
{
    public class PatientBL
    {
        public static int? GetPatientIdBL()
        {
            int? patient = null;
            try
            {
                PatientDAL patientDAL = new PatientDAL();
                patient = patientDAL.GetPatientIdDAL();
            }
            catch (PatientException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return patient;
        }


        public static bool AddPatientInfoBL(Patient newPatient)
        {
            bool patientAdded = false;
            try
            {
                if (ValidatePatient(newPatient))
                {
                    PatientDAL patientDAL = new PatientDAL();
                    patientAdded = patientDAL.AddPatientDAL(newPatient);
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
            return patientAdded;
        }

        private static bool ValidatePatient(Patient newPatient)
        {
            bool validPatient = true;
            StringBuilder sb = new StringBuilder();
            if (newPatient.Name == string.Empty)
            {
                validPatient = false;
                sb.Append("Patient Name should not be empty\n");
            }
            if (newPatient.PhoneNo.ToString().Length < 10)
            {
                validPatient = false;
                sb.Append("Required 10 digit number\n");
            }
            if (newPatient.Gender == string.Empty)
            {
                validPatient = false;
                sb.Append("Gender should not be empty\n");
            }

            if (newPatient.Age <= 0)
            {
                validPatient = false;
                sb.Append("Age cannot be less than zero\n");
            }
            if (newPatient.Weight <= 0)
            {
                validPatient = false;
                sb.Append("Weight cannot be less than zero\n");
            }
            if (newPatient.Address == string.Empty)
            {
                validPatient = false;
                sb.Append("Address can not be empty\n");
            }
            //if (newPatient.Gender.ToLower() != "m" || newPatient.Gender.ToLower() != "f")
            //{
            //    validPatient = false;
            //    sb.Append("Gender can only be M or F");
            //}
            if (validPatient == false)
                throw new PatientException(sb.ToString());
            return validPatient;
        }

        public static DataTable SearchPatient(int searchPatientID)
        {
            DataTable searchPatient = null;
            try
            {
                PatientDAL patientDAL = new PatientDAL();
                searchPatient = patientDAL.SearchPatientDAL(searchPatientID);
            }
            catch (PatientException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return searchPatient;
        }

        public static bool ModifyPatientInfoBL(Patient updatedPatient)
        {
            bool patientModified = false;
            try
            {
                if (ValidatePatient(updatedPatient))
                {
                    PatientDAL patientDAL = new PatientDAL();
                    patientModified = patientDAL.ModifyPatientInfoDAL(updatedPatient);
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
            return patientModified;
        }
    }
}
