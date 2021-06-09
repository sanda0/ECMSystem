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
    /// Interaction logic for register.xaml
    /// </summary>
    public partial class register : Window
    {
        includes.DBhelper db = new includes.DBhelper();
        includes.validation validation = new includes.validation();
        DataGrid table;
        public register(DataGrid dataGrid)
        {
            InitializeComponent();
            table = dataGrid;
        }

        private void cls_v()
        {
            f_name_v.Text = "";
            l_name_v.Text = "";
            dob_v.Text = "";
            nic_v.Text = "";
            tele_v.Text = "";
            addr_v.Text = "";
            user_type_v.Text = "";
            g_v.Text = "";
            pw_v.Text = "";
            cpw_v.Text = "";

        }

        private void cls_inputs()
        {
            f_name.Clear();
            l_name.Clear();
            nic.Clear();
            dob.SelectedDate = null;
            tele.Clear();
            addr.Clear();
            user_type.SelectedItem = -1;
            pw_box.Clear();
            cpw_box.Clear();

        }

        private void close_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void save_btn_Click(object sender, RoutedEventArgs e)
        {
            cls_v();
            bool is_ok = true;

            //gender make
            string gender = "";
            if (g_f.IsChecked == true)
            {
                gender = "Female";
            }
            else if (g_m.IsChecked == true)
            {
                gender = "Male";
            }
            ///

            


            //password validaion
            string pw = "";
            if (pw_box.Password == "")
            {
                is_ok = is_ok && false;
                pw_v.Text = "This field is required";
            }
            else
            {

                if (pw_box.Password == cpw_box.Password)
                {
                    pw = pw_box.Password;
                    is_ok = is_ok && true;

                }
                else
                {
                    cpw_box.Focus();
                    cpw_v.Text = "password mismach";
                    is_ok = is_ok && false;
                }

            }
            //

            //dob validation
            if (!validation.is_setDate(dob))
            {
                dob_v.Text = "Please select date";
                is_ok = is_ok && false;
            }
            //
            //user type validation
            string user_type_str = null; 
            if (!validation.is_selectItem(user_type))
            {
                user_type_v.Text = "please select user type";
                is_ok = is_ok && false;
            }
            else
            {
                user_type_str = user_type.Text;
            }
            //


            TextBox[] textBoxes = { f_name, l_name, nic, tele, addr };
            TextBlock[] textBlocks = { f_name_v, l_name_v, nic_v, tele_v, addr_v };
            if (validation.number_of_fill(textBoxes, textBlocks) == textBoxes.Length)
            {
                is_ok = is_ok && true;
            }
            else
            {
                is_ok = is_ok && false;
            }

            if (is_ok)
            {
                model.user user = new model.user();
                user.f_name = f_name.Text; user.l_name = l_name.Text; user.dob = dob.SelectedDate.Value.ToShortDateString(); user.nic = nic.Text;
                user.tp = tele.Text; user.address = addr.Text; user.user_type = user_type_str;
                user.gender = gender; user.pw = pw;


                int r = user.save();

                if (r == 1)
                {
                    cls_v();
                    cls_inputs();
                    table.ItemsSource = user.filter().DefaultView;
                    this.Close();
                    string[] btns = { "ok" };
                    custom_controlles.msgbox msgbox = new custom_controlles.msgbox(MaterialDesignThemes.Wpf.PackIconKind.CheckCircle, "User saved", "", Brushes.Green, btns);
                    msgbox.ShowDialog();

                }
                else if (r == 2)
                {

                    nic.Text = "";
                    nic.Focus();
                    nic_v.Text = "this nic number all ready exist";

                }else if(r == 0)
                {
                    string[] btns = { "ok" };
                    custom_controlles.msgbox msgbox = new custom_controlles.msgbox(MaterialDesignThemes.Wpf.PackIconKind.Error, "Error", "", Brushes.Red, btns);
                    msgbox.ShowDialog();
                }
            }



        }
    }
}
