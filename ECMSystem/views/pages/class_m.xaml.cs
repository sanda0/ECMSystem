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
    /// Interaction logic for class_m.xaml
    /// </summary>
    public partial class class_m : Page
    {
        model.Class_ class_ = new model.Class_();
        public class_m()
        {
            InitializeComponent();
            search_by_combo.SelectedIndex = 0;
            class_table.ItemsSource = class_.filter().DefaultView;
        }

        private void add_new_cls_btn_Click(object sender, RoutedEventArgs e)
        {
            views.add_class add_Class = new views.add_class(class_table);
            add_Class.ShowDialog();
        }

        private void btn_update_Click(object sender, RoutedEventArgs e)
        {
            views.update_class update_Class = new views.update_class(class_table);
            update_Class.ShowDialog();
        }

        private void btn_delete_Click(object sender, RoutedEventArgs e)
        {
            views.delete_class delete_Class = new views.delete_class(class_table);
            delete_Class.ShowDialog();
        }

        private void search_box_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(search_by_combo.SelectedIndex == 0)
            {
                class_table.ItemsSource = class_.filter("class.name", search_box.Text).DefaultView;
            }else if(search_by_combo.SelectedIndex == 1)
            {
                class_table.ItemsSource = class_.filter("subject.name", search_box.Text).DefaultView;
            }else if (search_by_combo.SelectedIndex == 2)
            {
                class_table.ItemsSource = class_.filter("teacher.name", search_box.Text).DefaultView;
            }
        }
    }
}
