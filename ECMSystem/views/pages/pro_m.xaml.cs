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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ECMSystem.views.pages
{
    /// <summary>
    /// Interaction logic for pro_m.xaml
    /// </summary>
    public partial class pro_m : Page
    {
        //includes.globles glo = new includes.globles();
        model.user user = new model.user();

        int ok = 0;

        public pro_m()
        {
            InitializeComponent();
            ch_pw.Visibility = Visibility.Collapsed;
        }

        private void chg_pwd_btn_Click(object sender, RoutedEventArgs e)
        {
            ch_pw.Visibility = Visibility.Visible;
        }

        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            if(pwd_old.Password == user.pw)
            {
                if(pwd_new.Password == pwd_cfm.Password)
                {
                    //user.pw = pwd_new.Password;
                    ok = 1;
                    pw_v.Foreground = Brushes.Green;
                    pw_v.Text = "password ready to change click update button";

                }
                else
                {
                    pw_v.Foreground = Brushes.Green;
                    pw_v.Text = "Invalide password";
                }
            }
            else
            {
                pw_v.Foreground = Brushes.Green;
                //MessageBox.Show(pwd_old.Password + "---" + user.pw);
                pw_v.Text = "Invalide old password";                
            }
        }

        private void up_save_btn_Click(object sender, RoutedEventArgs e)
        {
            model.user usernew = new model.user();
            usernew.find(includes.globles.user_id);
            usernew.f_name = f_name.Text;
            usernew.l_name = l_name.Text;
            usernew.tp = tel.Text;
            if (ok == 1)
            {
                usernew.pw = pwd_new.Password;
            }
            //MessageBox.Show(usernew.dob);
            usernew.update();
            up_msg.Foreground = Brushes.Green;
            up_msg.Text = "User update succesful";
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            show_nic.Text = includes.globles.nic;
            user.find(includes.globles.user_id);
            f_name.Text = user.f_name;
            l_name.Text = user.l_name;
            tel.Text = user.tp;
        }
    }
}
