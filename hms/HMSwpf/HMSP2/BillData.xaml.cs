using HMS.BusinessLayer;
using HMS.Entities;
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
    /// Interaction logic for BillData.xaml
    /// </summary>
    public partial class BillData : Window
    {
        PatientAppointmentData appointment = null;
        public BillData()
        {            
            
        }
        public BillData(PatientAppointmentData appointmentData)
        {
            InitializeComponent();
            try
            {
                txtPatientId.Text = appointmentData.PatientData.PatientID.ToString();
                txtPatientId.IsReadOnly = true;
                txtBillNo.Text = (AppointmentBL.GetBillDataIdBL() + 1).ToString();
                txtBillNo.IsReadOnly = true;
                appointment = appointmentData;
                if (appointment.PatientType == "Out")
                {
                    appointment.Bill.OperationCharge = 0;
                    appointment.Bill.RoomCharge = 0;
                    appointment.Bill.TotalDays = 0;
                    txtOperationCharge.Text = appointment.Bill.OperationCharge.ToString();
                    txtRoomCharge.Text = appointment.Bill.RoomCharge.ToString();
                    txtTotalDays.Text = appointment.Bill.RoomCharge.ToString();
                    txtOperationCharge.IsReadOnly = true;
                    txtRoomCharge.IsReadOnly = true;
                    txtTotalDays.IsReadOnly = true;
                }
                else
                {
                    txtOperationCharge.Text = appointment.Bill.OperationCharge.ToString();
                    txtRoomCharge.Text = appointment.Bill.RoomCharge.ToString();
                    txtTotalDays.Text = appointment.Bill.RoomCharge.ToString();
                }
                txtDoctorFees.Text = appointment.Bill.DoctorFees.ToString();
                txtLabFees.Text = appointment.Bill.LabFees.ToString();
                txtMedicineFees.Text = appointment.Bill.MedicineFees.ToString();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void InsertBillData()
        {
            appointment.Bill.BillNo = Convert.ToInt32(txtBillNo.Text);
            appointment.Bill.DoctorFees = Convert.ToInt32(txtDoctorFees.Text);
            appointment.Bill.LabFees = Convert.ToInt32(txtLabFees.Text);
            appointment.Bill.MedicineFees = Convert.ToInt32(txtMedicineFees.Text);

            if (appointment.PatientType == "In")
            {
                appointment.Bill.OperationCharge = Convert.ToInt32(txtOperationCharge.Text);
                appointment.Bill.RoomCharge = Convert.ToInt32(txtRoomCharge.Text);
                appointment.Bill.TotalDays = Convert.ToInt32(txtTotalDays.Text);

            }

            appointment.Bill.TotalAmount = appointment.Bill.DoctorFees + appointment.Bill.LabFees +
                                           appointment.Bill.MedicineFees + (appointment.Bill.OperationCharge +
                                           appointment.Bill.RoomCharge)*appointment.Bill.TotalDays;
        }
        private void Btn_Back_Click(object sender, RoutedEventArgs e)
        {            
            LabReport labReport = new LabReport(appointment);
            labReport.Show();
            this.Close();
        }

        private void Btn_Submit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                InsertBillData();
                if (AppointmentBL.AddPatientTreatmentInfoBL(appointment))
                    MessageBox.Show("Details added successfully");
                else
                    MessageBox.Show("Failed");
                MainWindow window = new MainWindow();
                window.Show();
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
