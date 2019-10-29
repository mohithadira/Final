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
using HMS.BusinessLayer;

namespace HMSPresentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddPatientInfo_Click(object sender, RoutedEventArgs e)
        {

            AddPatientInfo addPatient = new AddPatientInfo();
            addPatient.Show();
            this.Close();
        }

        private void Btn_AddAppointmentData_Click(object sender, RoutedEventArgs e)
        {
            AddAppointmentData addAppointment = new AddAppointmentData();
            addAppointment.Show();
            this.Close();
        }

        private void Btn_ModifyPatientInfo_Click(object sender, RoutedEventArgs e)
        {
            ModifyPatientInfo modifyPatient = new ModifyPatientInfo();
            modifyPatient.Show();
            this.Close();
        }

        private void Btn_FindLabReport_Click(object sender, RoutedEventArgs e)
        {
            FindLabReport labReport = new FindLabReport();
            labReport.Show();
            this.Close();
        }

        private void Btn_FindBillData_Click(object sender, RoutedEventArgs e)
        {
            FindBillData billData = new FindBillData();
            billData.Show();
            this.Close();
        }

        private void Btn_SearchPatientInfo_Click(object sender, RoutedEventArgs e)
        {
            SearchPatientInfo patientInfo = new SearchPatientInfo();
            patientInfo.Show();
            this.Close();
        }

        private void Btn_ViewPatientHistory_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataTable dataTable = AppointmentBL.GetAllPatientInfoBL();
                if (dataTable.Rows.Count == 0)
                    MessageBox.Show("No data found!!!");
                else
                    PatientDataGrid.DataContext = dataTable;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }     
    }
}
