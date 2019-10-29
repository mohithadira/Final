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
using HMS.BusinessLayer;
using System.Data;

namespace HMSPresentation
{
    /// <summary>
    /// Interaction logic for SearchPatientInfo.xaml
    /// </summary>
    public partial class SearchPatientInfo : Window
    {
        public SearchPatientInfo()
        {
            InitializeComponent();
            try
            {
                lblSearch.Visibility = Visibility.Hidden;
                txtSearch.Visibility = Visibility.Hidden;
                DateSearch.Visibility = Visibility.Hidden;

                Btn_DisplayDetails.IsEnabled = false;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        private void DisplayDetails_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (RdBtnPatientId.IsChecked == true)
                {
                    DataTable dataTable = AppointmentBL.SearchPatientInfoBL(Convert.ToInt32(txtSearch.Text), null, null, null);

                    if (dataTable.Rows.Count == 0)
                        MessageBox.Show("No data found!!!");
                    else
                        DataGridPatientInfo.DataContext = dataTable;
                }

                if (RdBtnDoctorName.IsChecked == true)
                {
                    DataTable dataTable = AppointmentBL.SearchPatientInfoBL(null, txtSearch.Text, null, null);

                    if (dataTable.Rows.Count == 0)
                        MessageBox.Show("No data found!!!");
                    else
                        DataGridPatientInfo.DataContext = dataTable;
                }

                if (RdBtnDateOfAdmission.IsChecked == true)
                {
                    DataTable dataTable = AppointmentBL.SearchPatientInfoBL(null, null, null, Convert.ToDateTime(DateSearch.Text));

                    if (dataTable.Rows.Count == 0)
                        MessageBox.Show("No data found!!!");
                    else
                        DataGridPatientInfo.DataContext = dataTable;
                }

                if (RdBtnDateOfVisit.IsChecked == true)
                {
                    DataTable dataTable = AppointmentBL.SearchPatientInfoBL(null, null, Convert.ToDateTime(DateSearch.Text), null);

                    if (dataTable.Rows.Count == 0)
                        MessageBox.Show("No data found!!!");
                    else
                        DataGridPatientInfo.DataContext = dataTable;
                }
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

        private void RdBtnPatientId_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                lblSearch.Visibility = Visibility.Visible;
                txtSearch.Visibility = Visibility.Visible;
                lblSearch.Content = "Enter Id:";
                DateSearch.Visibility = Visibility.Hidden;
                Btn_DisplayDetails.IsEnabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void RdBtnDateOfVisit_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                lblSearch.Visibility = Visibility.Visible;
                txtSearch.Visibility = Visibility.Hidden;
                lblSearch.Content = "Enter Date of visit:";
                DateSearch.Visibility = Visibility.Visible;
                Btn_DisplayDetails.IsEnabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void RdBtnDoctorName_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                lblSearch.Visibility = Visibility.Visible;
                txtSearch.Visibility = Visibility.Visible;
                lblSearch.Content = "Enter Doctor name:";
                DateSearch.Visibility = Visibility.Hidden;
                Btn_DisplayDetails.IsEnabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void RdBtnDateOfAdmission_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                lblSearch.Visibility = Visibility.Visible;
                txtSearch.Visibility = Visibility.Hidden;
                lblSearch.Content = "Enter admission date:";
                DateSearch.Visibility = Visibility.Visible;
                Btn_DisplayDetails.IsEnabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }
    }
}
