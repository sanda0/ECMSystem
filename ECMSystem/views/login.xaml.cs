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
    /// Interaction logic for login.xaml
    /// </summary>
    public partial class login : Window
    {
        public login()
        {
            InitializeComponent();
        }

        private void close_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }



        private void login_btn_Click(object sender, RoutedEventArgs e)
        {
            model.user user = new model.user();
            int r = user.check_login(nic.Text, pw_box.Password);
            if (r == 1)
            {
                dashboard dashboard = new dashboard();
                dashboard.Show();
                dashboard.WindowState = WindowState.Maximized;
                this.Close();
            }
            else if (r == 0)
            {
                nic_v.Text = "invalid login";
                pw_v.Text = "invalid login";
                nic.Focus();
            }
        }


    }
}
