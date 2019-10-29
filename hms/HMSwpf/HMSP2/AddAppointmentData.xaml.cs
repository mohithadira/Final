using HMS.BusinessLayer;
using HMSP2;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HMSPresentation
{
   
    /// <summary>
    /// Interaction logic for AddAppointmentData.xaml
    /// </summary>
    public partial class AddAppointmentData : Window
    {
        public AddAppointmentData()
        {
            InitializeComponent();  
        }
       

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataTable patient = PatientBL.SearchPatient(Convert.ToInt32(txtPatientId.Text));
                if (patient.Rows.Count == 0)
                    MessageBox.Show("This Id does not exist!!!");
                else
                {
                    PatientAppointmentIntermediate appointmentIntermediate = new PatientAppointmentIntermediate(Convert.ToInt32(txtPatientId.Text));
                    appointmentIntermediate.Show();
                    this.Close();
                }
            }

            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            this.Close();
        }

       
    }
}
