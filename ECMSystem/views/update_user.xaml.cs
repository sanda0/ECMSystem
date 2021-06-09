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
    /// Interaction logic for update_user.xaml
    /// </summary>
    public partial class update_user : Window
    {
        model.user user = new model.user();
        DataGrid table;
        public update_user(DataGrid dataGrid)
        {
            InitializeComponent();
            table = dataGrid;
            if(u_id_txt.Text == "")
            {
                desable();
                cls();
            }
        }

        private void cls()
        {
            f_name.Text = "";
            l_name.Text = "";
            dob.SelectedDate = null;
            addess.Text = "";
            tel.Text = "";
            user_type.Text = "";
            msg.Text = "";
        }

        private void desable()
        {
            f_name.IsEnabled = false;
            l_name.IsEnabled = false;
            dob.IsEnabled = false;
            addess.IsEnabled = false;
            tel.IsEnabled = false;
            user_type.IsEnabled = false;
            up_user_d_btn.IsEnabled = false;
        }
        private void enable()
        {
            f_name.IsEnabled = true;
            l_name.IsEnabled = true;
            dob.IsEnabled = true;
            addess.IsEnabled = true;
            tel.IsEnabled = true;
            user_type.IsEnabled = true;
            up_user_d_btn.IsEnabled = true;
        }

        private void close_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void up_user_d_btn_Click(object sender, RoutedEventArgs e)
        {
            user.f_name = f_name.Text;
            user.l_name = l_name.Text;
            user.dob = dob.SelectedDate.ToString();
            user.address = addess.Text;
            user.tp = tel.Text;
            user.user_type = user_type.Text;
            if(user.update() == 1)
            {
                msg.Text = "User updated";
                table.ItemsSource = user.filter().DefaultView;
                this.Close();
            }
            else
            {
                msg.Foreground = Brushes.Red;
                msg.Text = "User update faild!";
            }

        }

        private void u_id_txt_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            if(u_id_txt.Text != "")
            {
                if(user.find(u_id_txt.Text) == 1)
                {
                    if(user.user_id == u_id_txt.Text)
                    {
                        enable();
                        f_name.Text = user.f_name;
                        l_name.Text = user.l_name;
                        dob.SelectedDate = Convert.ToDateTime(user.dob);
                        addess.Text = user.address;
                        tel.Text = user.tp;
                        user_type.Text = user.user_type;
                        msg.Text = "Now you can edit user details";
                    }
                    else
                    {
                        cls();
                        desable();
                    }
                   
                }
            }
            else
            {
                cls();
                desable();
            }
        }
    }
}
