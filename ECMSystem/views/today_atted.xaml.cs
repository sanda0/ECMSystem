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

namespace ECMSystem.views
{
    /// <summary>
    /// Interaction logic for today_atted.xaml
    /// </summary>
    public partial class today_atted : Window
    {
        model.Attendance attendance = new model.Attendance();
        public today_atted()
        {
            InitializeComponent();
            date.Text = DateTime.Now.ToShortDateString();
            
            att_rep.ItemsSource = attendance.getToday().DefaultView;
        }

        private void cls_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void expt_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                expt_btn.IsEnabled = false;
                
                PrintDialog printDialog = new PrintDialog();
                if (printDialog.ShowDialog() == true)
                {
                    printDialog.PrintVisual(today_att_r, "Today Attendance report");
                }
            }
            finally
            {
                
                expt_btn.IsEnabled = true;
            }
        }
    }
}
