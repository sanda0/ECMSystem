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
    /// Interaction logic for delete_teacher.xaml
    /// </summary>
    public partial class delete_teacher : Window
    {
        DataGrid table;
        public delete_teacher(DataGrid dataGrid)
        {
            InitializeComponent();
            table = dataGrid;
        }

        private void close_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void s_box_TextChanged(object sender, TextChangedEventArgs e)
        {
            model.teacher teacher = new model.teacher();
            int r = teacher.find(s_box.Text);
            name.Text = teacher.name;
            
            if (r == 0)
            {
                msg.Text = "This ID can not found";
            }
        }

        private void del_btn_Click(object sender, RoutedEventArgs e)
        {
            model.teacher teacher = new model.teacher();
          
            if (teacher.delete(s_box.Text) == 1)
            {
                msg.Foreground = Brushes.Green;
                msg.Text = "Teacher Deleted";
                table.ItemsSource = teacher.filter().DefaultView;
                this.Close();

            }
            else
            {
                msg.Foreground = Brushes.Red;
                msg.Text = "Teacher not delete!";

            }
        }
    }
}
