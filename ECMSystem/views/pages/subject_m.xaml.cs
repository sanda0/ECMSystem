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
    /// Interaction logic for subject_m.xaml
    /// </summary>
    public partial class subject_m : Page
    {
        model.Subject subject = new model.Subject();
        public subject_m()
        {
            InitializeComponent();
            subject_table.ItemsSource = subject.filter().DefaultView;
        }

        private void add_new_hall_btn_Click(object sender, RoutedEventArgs e)
        {
            views.add_subject add_Subject = new views.add_subject(subject_table);
            add_Subject.ShowDialog();
        }

        private void btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            views.delete_subject delete_Subject = new views.delete_subject(subject_table);
            delete_Subject.ShowDialog();
        }
    }
}
