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
using System.Windows.Shapes;
using HMS.BusinessLayer;
using HMS.Entities;

namespace HMSPresentation
{
    /// <summary>
    /// Interaction logic for AddPatientInfo.xaml
    /// </summary>
    public partial class AddPatientInfo : Window
    {
        Patient patient = new Patient();

        public AddPatientInfo()
        {
            InitializeComponent();
            try
            {
                txtPatientId.Text = (PatientBL.GetPatientIdBL() + 1).ToString();
                txtPatientId.IsReadOnly = true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            
        }

        private void Btn_Submit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                patient.Name = txtPatientName.Text;
                if (RdBtnMale.IsChecked == true)
                    patient.Gender = RdBtnMale.Content.ToString();
                else if (RdBtnFemale.IsChecked == true)
                    patient.Gender = RdBtnFemale.Content.ToString();
                patient.Address = txtAddress.Text;
                patient.Age = Convert.ToInt32(txtAge.Text);
                patient.Disease = txtDisease.Text;
                patient.PhoneNo = Convert.ToInt64(txtPhoneNo.Text);
                patient.Weight = Convert.ToInt32(txtWeight.Text);

                if (PatientBL.AddPatientInfoBL(patient))
                    MessageBox.Show("Patient added successfully");
                else
                    MessageBox.Show("Adding patient failed!!!");

            }

            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            MainWindow window = new MainWindow();
            window.Show();
            this.Close();
        }

        private void Btn_BackToMenu_Click(object sender, RoutedEventArgs e)
        {

            MainWindow window = new MainWindow();
            window.Show();
            this.Close();
        }
    }
}
