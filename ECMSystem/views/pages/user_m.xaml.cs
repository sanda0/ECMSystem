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
    /// Interaction logic for user_m.xaml
    /// </summary>
    public partial class user_m : Page
    {
        model.user user = new model.user();
        public user_m()
        {
            InitializeComponent();
            //delete_btn.IsEnabled = false;
            //update_btn.IsEnabled = false;
            user_table.ItemsSource = user.filter().DefaultView;
            
             
        }

        private void add_btn_Click(object sender, RoutedEventArgs e)
        {
            views.register register = new views.register(user_table);
            register.ShowDialog();
        }

        private void update_btn_Click(object sender, RoutedEventArgs e)
        {
            views.update_user update_User = new views.update_user(user_table);
            update_User.ShowDialog();
        }

        private void delete_btn_Click(object sender, RoutedEventArgs e)
        {
            views.delete_user delete_User = new views.delete_user(user_table);
            delete_User.ShowDialog();
        }

        private void search_box_TextChanged(object sender, TextChangedEventArgs e)
        {
            user_table.ItemsSource = user.filter("f_name",search_box.Text).DefaultView;
        }

 
    }
}
