using HMS.BusinessLayer;
using HMS.Entities;
using HMSP2;
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

namespace HMSPresentation
{
    /// <summary>
    /// Interaction logic for LabReport.xaml
    /// </summary>
    public partial class LabReport : Window
    {
        PatientAppointmentData appointment = null;
   
        public LabReport()
        {

        }
        public LabReport(PatientAppointmentData appointmentData)
        {
            InitializeComponent();
            try
            {
                txtPatientId.Text = appointmentData.PatientData.PatientID.ToString();
                txtPatientId.IsReadOnly = true;
                txtReportId.Text = (AppointmentBL.GetLabIdBL() + 1).ToString();
                txtReportId.IsReadOnly = true;
                txtTestType.Text = appointmentData.LabData.TestType;
                txtRemarks.Text = appointmentData.LabData.Remarks;
                if(appointmentData.LabData.TestDate.ToShortDateString()!="01-01-0001")
                    PickTestDate.SelectedDate = appointmentData.LabData.TestDate;
                appointment = appointmentData;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
               
        }

        private void Btn_Next_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                appointment.LabData.ReportId = Convert.ToInt32(txtReportId.Text);                
                appointment.LabData.Remarks = txtRemarks.Text;
                appointment.PatientData.PatientID = Convert.ToInt32(txtPatientId.Text);
                appointment.LabData.TestDate = Convert.ToDateTime(PickTestDate.SelectedDate);

                appointment.LabData.TestType = txtTestType.Text;

                BillData bill = new BillData(appointment);
                bill.Show();
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void Btn_Back_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PatientAppointmentIntermediate appointmentIntermediate = new PatientAppointmentIntermediate(appointment);
                appointmentIntermediate.Show();
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
