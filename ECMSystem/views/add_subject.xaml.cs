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
    /// Interaction logic for add_subject.xaml
    /// </summary>
    public partial class add_subject : Window
    {
        DataGrid table;
        model.Subject subject = new model.Subject();
        public add_subject(DataGrid dataGrid)
        {
            InitializeComponent();
            table = dataGrid;
        }

        private void save_btn_Click(object sender, RoutedEventArgs e)
        {
            if(sub_name.Text != "")
            {
                subject.name = sub_name.Text;
                subject.save();
                table.ItemsSource = subject.filter().DefaultView;
                this.Close();
            }
            else
            {
                sub_name_v.Text = "Please enter subject Name";
            }
        }

        private void close_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
