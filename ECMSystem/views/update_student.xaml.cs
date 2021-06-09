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
    /// Interaction logic for update_student.xaml
    /// </summary>
    public partial class update_student : Window
    {
        model.Student student = new model.Student();
        //model.Class_ class_ = new model.Class_();
       // includes.Util util = new includes.Util();

        DataGrid table;
        public update_student(string sid,DataGrid dataGrid)
        {
            InitializeComponent();
            table = dataGrid;
            student.find(sid: sid);

            nic_txt.Text ="NIC : "+student.nic;
            f_name.Text = student.f_name;
            l_name.Text = student.l_name;
            tel.Text = student.tp;
            addr.Text = student.addr;
            dob.Text = student.dob;

        }



        private void upd_st_btn_Click(object sender, RoutedEventArgs e)
        {

            student.f_name = f_name.Text;
            student.l_name = l_name.Text;
            student.tp = tel.Text;
            student.addr = addr.Text;
            student.dob = dob.Text;

            if (student.update() ==1)
            {
                table.ItemsSource = student.filter().DefaultView;
                this.Close();
            }
            else
            {
                string[] btns = { "ok" };
                custom_controlles.msgbox msgbox = new custom_controlles.msgbox(MaterialDesignThemes.Wpf.PackIconKind.Error, "Error", "", Brushes.Red, btns);
                msgbox.ShowDialog();
            }


        }

        private void close_btn_cls_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
