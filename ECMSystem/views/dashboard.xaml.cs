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
    /// Interaction logic for dashboard.xaml
    /// </summary>
    public partial class dashboard : Window
    {
        public void permission_manager(string utype)
        {
            ListViewItem[] maneger = { home_m, stu_m, class_m, fina_m, subject_m, user_m,teacher_m };
            ListViewItem[] cashier = { add_pay, add_stu,barcode_report,stu_m };
            ListViewItem[] attend_maker = { atten_mark };
            ListViewItem[] all = { stu_m, class_m, fina_m, subject_m, user_m, teacher_m, add_pay, add_stu, barcode_report, stu_m, atten_mark,home_m };

            foreach (ListViewItem item in all.ToArray())
            {
                item.Visibility = Visibility.Collapsed;
            }

            if (utype == "Manager")
            {
                foreach (ListViewItem item in maneger)
                {

                    item.Visibility = Visibility.Visible;


                }
                main_frame.Content = new views.pages.manager_welcome();
            }
            else if (utype == "Attendence marker")
            {
                main_frame.Content = new views.pages.attendance_marking_am();
                foreach (ListViewItem item in attend_maker)
                {

                    item.Visibility = Visibility.Visible;

                }

            }
            else if (utype == "Cashier")
            {
                main_frame.Content = new views.pages.add_payment_c();
                foreach (ListViewItem item in cashier)
                {

                    item.Visibility = Visibility.Visible;

                }

            }


        }



        public dashboard()
        {
            InitializeComponent();

            who.Text = "Hi " + includes.globles.f_name;
            permission_manager(includes.globles.user_type);
        }



        private void win_min_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void max_win_Click(object sender, RoutedEventArgs e)
        {

            if (this.WindowState == WindowState.Normal)
            {
                this.WindowState = WindowState.Maximized;
                win_max_nor.Kind = MaterialDesignThemes.Wpf.PackIconKind.WindowRestore;

            }
            else
            {
                this.WindowState = WindowState.Normal;
                win_max_nor.Kind = MaterialDesignThemes.Wpf.PackIconKind.WindowMaximize;
            }
        }

        private void close_win_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void class_m_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            main_frame.Content = new views.pages.class_m();
        }

        private void stu_m_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            main_frame.Content = new views.pages.student_m();
        }



        private void subject_m_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            main_frame.Content = new views.pages.subject_m();
        }

        private void fina_m_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            main_frame.Content = new views.pages.reports();
        }

        private void add_pay_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            main_frame.Content = new views.pages.add_payment_c();

        }

        private void add_stu_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            main_frame.Content = new views.pages.Add_student_c(main_frame);
        }

        private void atten_mark_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            main_frame.Content = new views.pages.attendance_marking_am();
        }

        private void profile_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            main_frame.Content = new views.pages.pro_m();
        }

        private void logout_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            
            login login = new login();
            this.Close();
            login.ShowDialog();
            
        }

        private void user_m_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            main_frame.Content = new views.pages.user_m();
        }

        private void teacher_m_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            main_frame.Content = new views.pages.teacher_m();
        }

        private void barcode_report_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            main_frame.Content = new views.pages.barcodes_m_c();
        }

        private void home_m_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            main_frame.Content = new views.pages.manager_welcome();
        }
    }
}
