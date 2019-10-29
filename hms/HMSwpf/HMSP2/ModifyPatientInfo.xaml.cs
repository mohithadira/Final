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
    /// Interaction logic for ModifyPatientInfo.xaml
    /// </summary>
    public partial class ModifyPatientInfo : Window
    {
        public ModifyPatientInfo()
        {
            InitializeComponent();
            try
            {
                LblAddress.Visibility = Visibility.Hidden;
                LblAge.Visibility = Visibility.Hidden;
                LblDisease.Visibility = Visibility.Hidden;
                LblGender.Visibility = Visibility.Hidden;
                LblPatientName.Visibility = Visibility.Hidden;
                LblPhoneNo.Visibility = Visibility.Hidden;
                LblWeight.Visibility = Visibility.Hidden;

                txtAddress.Visibility = Visibility.Hidden;
                txtAge.Visibility = Visibility.Hidden;
                txtDisease.Visibility = Visibility.Hidden;
                txtPatientName.Visibility = Visibility.Hidden;
                txtPhoneNo.Visibility = Visibility.Hidden;
                txtWeight.Visibility = Visibility.Hidden;
                RdBtnFemale.Visibility = Visibility.Hidden;
                RdBtnMale.Visibility = Visibility.Hidden;

                Btn_BackToMenu.Visibility = Visibility.Hidden;
                Btn_Modify.Visibility = Visibility.Hidden;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
         
        }

        private void Btn_Modify_Click(object sender, RoutedEventArgs e)
        {
            Patient patient = new Patient();
            try
            {
                patient.PatientID = Convert.ToInt32(txtPatientId.Text);
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

                if (PatientBL.ModifyPatientInfoBL(patient))
                    MessageBox.Show("Patient Info modified successfully");
                else
                    MessageBox.Show("Patient Info modification failed!!!");

                MainWindow window = new MainWindow();
                window.Show();
                this.Close();
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

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataTable patient = PatientBL.SearchPatient(Convert.ToInt32(txtPatientId.Text));
                if (patient.Rows.Count == 0)
                    MessageBox.Show("No data found");
                else
                {
                    DataRow dataRow = patient.Rows[0];

                    txtPatientId.Text = dataRow["PatientId"].ToString();
                    txtAddress.Text = dataRow["Address"].ToString();
                    txtAge.Text = dataRow["Age"].ToString();
                    txtDisease.Text = dataRow["Disease"].ToString();
                    txtPatientName.Text = dataRow["Name"].ToString();
                    txtPhoneNo.Text = dataRow["PhoneNo"].ToString();
                    txtWeight.Text = dataRow["Weight"].ToString();

                    if (dataRow["Gender"].ToString() == "M")
                        RdBtnMale.IsChecked = true;
                    else if (dataRow["Gender"].ToString() == "F")
                        RdBtnFemale.IsChecked = true;

                    txtPatientId.IsReadOnly = true;

                    LblAddress.Visibility = Visibility.Visible;
                    LblAge.Visibility = Visibility.Visible;
                    LblDisease.Visibility = Visibility.Visible;
                    LblGender.Visibility = Visibility.Visible;
                    LblPatientName.Visibility = Visibility.Visible;
                    LblPhoneNo.Visibility = Visibility.Visible;
                    LblWeight.Visibility = Visibility.Visible;

                    txtAddress.Visibility = Visibility.Visible;
                    txtAge.Visibility = Visibility.Visible;
                    txtDisease.Visibility = Visibility.Visible;
                    txtPatientName.Visibility = Visibility.Visible;
                    txtPhoneNo.Visibility = Visibility.Visible;
                    txtWeight.Visibility = Visibility.Visible;
                    RdBtnMale.Visibility = Visibility.Visible;
                    RdBtnFemale.Visibility = Visibility.Visible;

                    Btn_BackToMenu.Visibility = Visibility.Visible;
                    Btn_Modify.Visibility = Visibility.Visible;

                    btnSubmit.Visibility = Visibility.Hidden;
                    btnBack.Visibility = Visibility.Hidden;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            this.Close();
        }
    }
}
