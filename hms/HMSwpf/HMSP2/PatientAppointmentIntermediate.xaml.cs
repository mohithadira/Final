using HMS.BusinessLayer;
using HMS.Entities;
using HMSPresentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HMSP2
{
    /// <summary>
    /// Interaction logic for PatientAppointmentIntermediate.xaml
    /// </summary>
    public partial class PatientAppointmentIntermediate : Window
    {
        AddAppointmentData appointmentData = new AddAppointmentData();
        PatientAppointmentData appointment = new PatientAppointmentData();
        public PatientAppointmentIntermediate()
        {
            InitializeComponent();
        }

        public PatientAppointmentIntermediate(PatientAppointmentData data)
        {           
            InitializeComponent();
            try
            {
                txtPatientId.Text = data.PatientData.PatientID.ToString();
                txtAppointmentId.Text = data.AppointmentId.ToString();
                txtDoctorName.Text = data.DoctorName;                    
                if (data.PatientType == "In")
                {
                    RdBtnInPatient.IsChecked = true;
                    PickAdmissionDate.SelectedDate = data.AdmissionDate;
                    PickDischargeDate.SelectedDate = data.DischargeDate;
                    txtRoomNo.Text = data.RoomNo.ToString();
                }                    
                else if (data.PatientType == "Out")
                {
                    RdBtnOutPatient.IsChecked = true;
                    PickDateOfVisit.SelectedDate = data.DateOfVisit;
                }
                    
                appointment = data;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            
        }

        public PatientAppointmentIntermediate(int Id)
        {
            InitializeComponent();
            try
            {
                txtPatientId.Text = Id.ToString();
                txtPatientId.IsReadOnly = true;
                txtAppointmentId.Text = (AppointmentBL.GetPatientTreatmentIdBL() + 1).ToString();
                txtAppointmentId.IsReadOnly = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        private void Btn_BackToMenu_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            this.Close();
        }

        private void Btn_Next_Click(object sender, RoutedEventArgs e)
        {
            try
            {                
                appointment.AppointmentId = Convert.ToInt32(txtAppointmentId.Text);                                
                appointment.DoctorName = txtDoctorName.Text;               
                appointment.PatientData.PatientID = Convert.ToInt32(txtPatientId.Text);

                if (RdBtnInPatient.IsChecked == true)
                {
                    appointment.PatientType = RdBtnInPatient.Content.ToString();
                    appointment.AdmissionDate = Convert.ToDateTime(PickAdmissionDate.SelectedDate);
                    appointment.DischargeDate = Convert.ToDateTime(PickDischargeDate.SelectedDate);
                    appointment.RoomNo = txtRoomNo.Text;
                }
                    
                else if (RdBtnOutPatient.IsChecked == true)
                {                   
                    appointment.DateOfVisit = Convert.ToDateTime(PickDateOfVisit.SelectedDate);
                    appointment.PatientType = RdBtnOutPatient.Content.ToString();
                }
                    

                LabReport report = new LabReport(appointment);
                report.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void RdBtnInPatient_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                PickDateOfVisit.IsEnabled = false;
                PickAdmissionDate.IsEnabled = true;
                PickDischargeDate.IsEnabled = true;
                txtRoomNo.IsEnabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void RdBtnOutPatient_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                PickAdmissionDate.IsEnabled = false;
                PickDischargeDate.IsEnabled = false;
                txtRoomNo.IsEnabled = false;
                PickDateOfVisit.IsEnabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }
    }
}
