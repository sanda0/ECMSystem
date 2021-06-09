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
    /// Interaction logic for delete_user.xaml
    /// </summary>
    public partial class delete_user : Window
    {

        DataGrid table;
        public delete_user(DataGrid dataGrid)
        {
            InitializeComponent();
            table = dataGrid;
        }

        private void close_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void del_btn_Click(object sender, RoutedEventArgs e)
        {
            model.user user = new model.user();
            if (user.delete(s_box.Text) == 1)
            {
                msg.Foreground = Brushes.Green;
                table.ItemsSource = user.filter().DefaultView;
                this.Close();
                msg.Text = "User Deleted";
               
            }
            else
            {
                msg.Foreground = Brushes.Red;
                msg.Text = "user not delete!";
             
            }
            
        }

        private void s_box_TextChanged(object sender, TextChangedEventArgs e)
        {
            model.user user = new model.user();
            int r = user.find(s_box.Text);
            name.Text = user.f_name;
            nic.Text = user.nic;
            if(r == 0)
            {
                msg.Text = "This ID can not found";
            }
            
        }
    }
}
