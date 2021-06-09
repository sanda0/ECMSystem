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
    /// Interaction logic for teacher_m.xaml
    /// </summary>
    public partial class teacher_m : Page
    {

        model.teacher teacher = new model.teacher();

        public teacher_m()
        {
            InitializeComponent();
    
            data_table.ItemsSource = teacher.filter().DefaultView;

        }





        private void add_btn_Click(object sender, RoutedEventArgs e)
        {
            Add_teacher add_Teacher = new Add_teacher(data_table);
            add_Teacher.ShowDialog();
        }

        private void update_btn_Click(object sender, RoutedEventArgs e)
        {
            views.update_teacher update_Teacher = new views.update_teacher(data_table);
            update_Teacher.ShowDialog();
        }

        private void delete_btn_Click(object sender, RoutedEventArgs e)
        {
            views.delete_teacher delete_Teacher = new views.delete_teacher(data_table);
            delete_Teacher.ShowDialog();
        }

        private void search_box_TextChanged(object sender, TextChangedEventArgs e)
        {
            data_table.ItemsSource = teacher.filter("name",search_box.Text).DefaultView;
        }
    }
}
