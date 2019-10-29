using HMS.BusinessLayer;
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

namespace HMSPresentation
{
    /// <summary>
    /// Interaction logic for FindLabReport.xaml
    /// </summary>
    public partial class FindLabReport : Window
    {
        public FindLabReport()
        {
            InitializeComponent();
        }

        private void Btn_Submit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataTable dataTable = AppointmentBL.GetLabReportBL(Convert.ToInt32(txtPatientId.Text));
                if (dataTable.Rows.Count == 0)
                    MessageBox.Show("No data found for this Id!!!");
                else
                    DataGridLabReport.DataContext = dataTable;
            }
            catch(Exception ex)
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
    }
}
