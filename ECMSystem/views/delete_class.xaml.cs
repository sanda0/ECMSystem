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
    /// Interaction logic for delete_class.xaml
    /// </summary>
    public partial class delete_class : Window
    {
        model.Class_ class_ = new model.Class_();
        DataGrid table;
        public delete_class(DataGrid dataGrid)
        {
            InitializeComponent();
            table = dataGrid;
            clz_del_btn.IsEnabled = false;
        }

        private void close_clz_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void clz_del_box_TextChanged(object sender, TextChangedEventArgs e)
        {
            int r = class_.find(clz_del_box.Text);
            if(r == 1)
            {
                clz_del_btn.IsEnabled = true;
                name.Foreground = Brushes.Black;
                name.Text = class_.name;
            }
            if (r == 0)
            {
                msg_clz.Foreground = Brushes.Red;
                msg_clz.Text = "This ID can not found";
            }
        }

        private void clz_del_btn_Click(object sender, RoutedEventArgs e)
        {
            if (class_.delete(clz_del_box.Text) == 1)
            {             
                table.ItemsSource = class_.filter().DefaultView;
                this.Close();
            }
            else
            {
                msg_clz.Foreground = Brushes.Red;
                msg_clz.Text = "Teacher not delete!";
            }
        }
    }
}
