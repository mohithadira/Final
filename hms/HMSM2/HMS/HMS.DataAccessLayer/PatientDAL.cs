using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMS.Entities;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using HMS.Exceptions;
namespace HMS.DataAccessLayer
{
    public class PatientDAL
    {
        string connectionString;
        SqlConnection connection;
        SqlCommand cmd;
        public PatientDAL()
        {
            connectionString = ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString;
            connection = new SqlConnection(connectionString);
        }

        public int? GetPatientIdDAL()
        {
            int? patientId=null ;
            try
            {
                cmd = new SqlCommand("usp_GetPatientId", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                if (connection.State != ConnectionState.Open)
                    connection.Open();
                patientId = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (SystemException ex)
            {
                throw new PatientException(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return patientId;

        }

        public bool AddPatientDAL(Patient newPatient)
        {
            bool patientAdded = false;
            try
            {
                cmd = new SqlCommand("usp_insertPatient", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Name",newPatient.Name);
                cmd.Parameters.AddWithValue("@Gender", newPatient.Gender);
                cmd.Parameters.AddWithValue("@Age", newPatient.Age);
                cmd.Parameters.AddWithValue("@Disease", newPatient.Disease);
                cmd.Parameters.AddWithValue("@Weight", newPatient.Weight);
                cmd.Parameters.AddWithValue("@Address", newPatient.Address);
                cmd.Parameters.AddWithValue("@PhoneNo", newPatient.PhoneNo);
                if(connection.State!=ConnectionState.Open)
                    connection.Open();
                int count = cmd.ExecuteNonQuery();
                if (count > 0)
                    patientAdded = true;
            }
            catch(SystemException ex)
            {
                throw new PatientException(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return patientAdded;
        }

        public DataTable SearchPatientDAL(int searchPatientID)
        {
            DataTable searchPatient;
            try
            {
                cmd = new SqlCommand("usp_GetPatientInfo", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@PatientID",searchPatientID);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                searchPatient= new DataTable("Patient");
                adapter.Fill(searchPatient);               
            }
            catch(SystemException ex)
            {
                throw new PatientException(ex.Message);
            }            
            return searchPatient;
        }

        public bool ModifyPatientInfoDAL(Patient updatedPatient)
        {
            bool patientUpdated = false;
            try
            {
                cmd = new SqlCommand("usp_updatePatient", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@PatientID", updatedPatient.PatientID);
                cmd.Parameters.AddWithValue("@Name", updatedPatient.Name);
                cmd.Parameters.AddWithValue("@Gender", updatedPatient.Gender);
                cmd.Parameters.AddWithValue("@Age", updatedPatient.Age);
                cmd.Parameters.AddWithValue("@Disease", updatedPatient.Disease);
                cmd.Parameters.AddWithValue("@Weight", updatedPatient.Weight);
                cmd.Parameters.AddWithValue("@Address", updatedPatient.Address);
                cmd.Parameters.AddWithValue("@PhoneNo", updatedPatient.PhoneNo);
                if (connection.State != ConnectionState.Open)
                    connection.Open();
                int count = cmd.ExecuteNonQuery();
                if (count > 0)
                    patientUpdated = true;
            }
            catch(SystemException ex)
            {
                throw new PatientException(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return patientUpdated;
        }
    }
}
