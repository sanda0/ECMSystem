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
    /// Interaction logic for student_m.xaml
    /// </summary>
    public partial class student_m : Page
    {
        model.Student student = new model.Student();
        model.Class_ class_ = new model.Class_();
        includes.Util util = new includes.Util();

        public student_m()
        {
            InitializeComponent();
            if ((bool)class_select_on_off.IsChecked == false)
            {
                class_select.IsEnabled = false;
            }
            stu_table.ItemsSource = student.filter().DefaultView;
            util.fillComboBox(class_select, util.DTto2D(class_.filter()), 1);
        }

        private void class_select_on_off_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)class_select_on_off.IsChecked == false)
            {
                class_select.IsEnabled = false;
                stu_table.ItemsSource = student.filter().DefaultView;
            }
            else
            {
                class_select.IsEnabled = true;
            }
        }

 

        private void del_stu_Click(object sender, RoutedEventArgs e)
        {
            Button btn  = sender as Button;
            if(student.delete(btn.CommandParameter.ToString())== 1)
            {
                stu_table.ItemsSource = student.filter().DefaultView;
            }

        }

        private void update_stu_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            views.update_student update_Student = new views.update_student(btn.CommandParameter.ToString(),stu_table);
            update_Student.ShowDialog();
        }

        private void view_clz_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            views.Stu_clz stu_Clz = new views.Stu_clz(btn.CommandParameter.ToString());
            stu_Clz.ShowDialog();
        }

        private void class_select_DropDownClosed(object sender, EventArgs e)
        {
            if(class_select.SelectedIndex != -1)
            {
                stu_table.ItemsSource = student.filterByclass(util.DTto2D(class_.filter())[class_select.SelectedIndex, 0]).DefaultView;
            }
           
        }

        private void search_box_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(search_by.SelectedIndex == 0)
            {
                stu_table.ItemsSource = student.filter(by: "f_name", key: search_box.Text).DefaultView;
            }else if(search_by.SelectedIndex == 1)
            {
                stu_table.ItemsSource = student.filter(by: "nic", key: search_box.Text).DefaultView;
            }
        }
    }
}
