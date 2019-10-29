using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMS.Entities;
using System.Data;
using HMS.Exceptions;
using System.Data.SqlClient;
using System.Configuration;
namespace HMS.DataAccessLayer
{
    public class AppointmentDAL
    {

        string connectionString;
        SqlConnection connection;
        SqlCommand cmd;
        public AppointmentDAL()
        {
            connectionString = ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString;
            connection = new SqlConnection(connectionString);
        }
        public DataTable SearchPatientInfoDAL(int? pId, string doctorName, DateTime? dateOfVisit, DateTime? dateOfAdmission)
        {
            DataTable searchPatients = null;
            try
            {
                if (pId != null && doctorName == null && dateOfVisit == null && dateOfAdmission == null)
                {
                    cmd = new SqlCommand("usp_GetPatientsById", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@PatientID", pId);
                    searchPatients = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(searchPatients);
                }
                else if (pId == null && doctorName != null && dateOfVisit == null && dateOfAdmission == null)
                {
                    cmd = new SqlCommand("usp_GetPatientsByDoctorName", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@DoctorName", doctorName);
                    searchPatients = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(searchPatients);
                }    
                else if (pId == null && doctorName == null && dateOfVisit != null && dateOfAdmission == null)
                {
                    cmd = new SqlCommand("usp_GetPatientsByDateOfVisit", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@DateOfVisit", dateOfVisit);
                    searchPatients = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(searchPatients);
                }   
                else if (pId == null && doctorName == null && dateOfVisit == null && dateOfAdmission != null)
                {
                    cmd = new SqlCommand("usp_GetPatientsByDateOfAdmission", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@AdmissionDate", dateOfAdmission);
                    searchPatients = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(searchPatients);
                }    
            }
            catch (SystemException ex)
            {
                throw new PatientException(ex.Message);
            }
            return searchPatients;
        }

        public bool AddPatientTreatmentInfoDAL(PatientAppointmentData patientTreatmentData)
        {
            bool patientDataAdded = false;
            try
            {
                cmd = new SqlCommand("usp_insertPatientData", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };                              
                Console.WriteLine(patientTreatmentData.DoctorName);
                cmd.Parameters.AddWithValue("@DoctorName", patientTreatmentData.DoctorName);
                if (patientTreatmentData.PatientType == "Out")
                {
                    cmd.Parameters.AddWithValue("@DateOfVisit", SqlDbType.DateTime2).Value = patientTreatmentData.DateOfVisit;
                    cmd.Parameters.AddWithValue("@RoomNo", DBNull.Value);
                    cmd.Parameters.AddWithValue("@AdmissionDate", DBNull.Value);
                    cmd.Parameters.AddWithValue("@DischargeDate", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@DateOfVisit", DBNull.Value);
                    cmd.Parameters.AddWithValue("@RoomNo", patientTreatmentData.RoomNo);
                    cmd.Parameters.AddWithValue("@AdmissionDate", SqlDbType.DateTime2).Value = patientTreatmentData.AdmissionDate;
                    cmd.Parameters.AddWithValue("@DischargeDate", SqlDbType.DateTime2).Value = patientTreatmentData.DischargeDate;
                }
                    
                
                
                
                //cmd.Parameters.AddWithValue("@DateOfVisit", patientTreatmentData.DateOfVisit);
                //cmd.Parameters.AddWithValue("@AdmissionDate", patientTreatmentData.AdmissionDate);
                //cmd.Parameters.AddWithValue("@DischargeDate", patientTreatmentData.DischargeDate);
                cmd.Parameters.AddWithValue("@PatientType", patientTreatmentData.PatientType);
                cmd.Parameters.AddWithValue("@PatientID", patientTreatmentData.PatientData.PatientID);
                cmd.Parameters.AddWithValue("@TestDate", SqlDbType.DateTime2).Value = patientTreatmentData.LabData.TestDate;
                //cmd.Parameters.AddWithValue("@TestDate", patientTreatmentData.LabData.TestDate);
                cmd.Parameters.AddWithValue("@Remarks", patientTreatmentData.LabData.Remarks);
                cmd.Parameters.AddWithValue("@TestType", patientTreatmentData.LabData.TestType);
                
                cmd.Parameters.AddWithValue("@DoctorFees", patientTreatmentData.Bill.DoctorFees);
                cmd.Parameters.AddWithValue("@RoomCharge", patientTreatmentData.Bill.RoomCharge);
                cmd.Parameters.AddWithValue("@OperationCharge", patientTreatmentData.Bill.OperationCharge);
                cmd.Parameters.AddWithValue("@MedicineFees", patientTreatmentData.Bill.MedicineFees);
                cmd.Parameters.AddWithValue("@TotalDays", patientTreatmentData.Bill.TotalDays);
                cmd.Parameters.AddWithValue("@LabFees", patientTreatmentData.Bill.LabFees);
                cmd.Parameters.AddWithValue("@TotalAmount", patientTreatmentData.Bill.TotalAmount);
                cmd.Parameters.AddWithValue("@ReportId", patientTreatmentData.LabData.ReportId);
                cmd.Parameters.AddWithValue("@BillNo", patientTreatmentData.Bill.BillNo);
               
                if (!(connection.State == ConnectionState.Open))
                    connection.Open();
                int count = cmd.ExecuteNonQuery();
                if (count == 3)
                    patientDataAdded = true;
            }
            catch(SystemException ex)
            {
                throw new PatientException(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return patientDataAdded;
        }

        public DataTable GetAllPatientInfoDAL()
        {
            DataTable patientInfo;
            try
            {
                cmd = new SqlCommand("usp_GetAllPatientTreatmentInfo", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                patientInfo = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(patientInfo);
            }
            catch (SystemException ex)
            {
                throw new PatientException(ex.Message);
            }
            return patientInfo;
        }

        public DataTable GetLabReportDAL(int patientID)
        {
            DataTable labData;
            try
            {
                cmd = new SqlCommand("usp_GetLabReport", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@PatientID", patientID);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                labData = new DataTable();
                adapter.Fill(labData);
            }
            catch (SystemException ex)
            {
                throw new PatientException(ex.Message);
            }
            return labData;
        }

        public DataTable GetBillDataDAL(int patientID)
        {
            DataTable billData;
            try
            {
                cmd = new SqlCommand("usp_GetBillData", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@PatientID", patientID);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                billData = new DataTable();
                adapter.Fill(billData);
            }
            catch (SystemException ex)
            {
                throw new PatientException(ex.Message);
            }
            return billData;
        }

        public int? GetPatientTreatmentIdDAL()
        {
            int? patientTreatmentId = null;
            try
            {
                cmd = new SqlCommand("usp_GetPatientTreatmentId", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                if (connection.State != ConnectionState.Open)
                    connection.Open();
                patientTreatmentId = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (SystemException ex)
            {
                throw new PatientException(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return patientTreatmentId;

        }


        public int? GetLabIdDAL()
        {
            int? labId = null;
            try
            {
                cmd = new SqlCommand("usp_GetLabId", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                if (connection.State != ConnectionState.Open)
                    connection.Open();
                labId = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (SystemException ex)
            {
                throw new PatientException(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return labId;

        }



        public int? GetBillDataIdDAL()
        {
            int? billDataId = null;
            try
            {
                cmd = new SqlCommand("usp_GetBillDataId", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                if (connection.State != ConnectionState.Open)
                    connection.Open();
                billDataId = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (SystemException ex)
            {
                throw new PatientException(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return billDataId;

        }



    }
}
