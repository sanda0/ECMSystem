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
    /// Interaction logic for delete_subject.xaml
    /// </summary>
    public partial class delete_subject : Window
    {
        DataGrid table;
        model.Subject subject = new model.Subject();
        public delete_subject(DataGrid dataGrid)
        {
            InitializeComponent();
            table = dataGrid;
        }

        private void close_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }



        private void del_s_TextChanged(object sender, TextChangedEventArgs e)
        {
            int r = subject.find(del_s.Text);
            del_sub_txt.Foreground = Brushes.Black;
            del_sub_txt.Text = subject.name;

            if (r == 0)
            {
                del_sub_txt.Foreground = Brushes.Red;
                del_sub_txt.Text = "This ID can not found";
            }
        }

        private void del_btn_Click(object sender, RoutedEventArgs e)
        {
            if (subject.delete(del_s.Text) == 1)
            {
                del_sub_txt.Foreground = Brushes.Green;
                del_sub_txt.Text = "Teacher Deleted";
                table.ItemsSource = subject.filter().DefaultView;
                this.Close();

            }
            else
            {
                del_sub_txt.Foreground = Brushes.Red;
                del_sub_txt.Text = "Teacher not delete!";

            }
        }
    }
}
