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
    /// Interaction logic for barcodes_m_c.xaml
    /// </summary>
    public partial class barcodes_m_c : Page
    {
        model.Student student = new model.Student();
        includes.Util util = new includes.Util();
        model.Class_ class_ = new model.Class_();
        public barcodes_m_c()
        {
            InitializeComponent();
            barcode_r.ItemsSource = student.filter().DefaultView;
            progrss.Visibility = Visibility.Hidden;
            clz.IsEnabled = false;
            stD.IsEnabled = false;
            endD.IsEnabled = false;
            date_l.Visibility = Visibility.Collapsed;
            clzn_l.Visibility = Visibility.Collapsed;

            util.fillComboBox(clz, util.DTto2D(class_.filter()), 1);
        }

        private void filter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(filter.SelectedIndex == 1)
            {
                clz.IsEnabled = true;
                stD.IsEnabled = false;
                endD.IsEnabled = false;
                dfilter.IsEnabled = false;
                date_l.Visibility = Visibility.Collapsed;
                clzn_l.Visibility = Visibility.Visible;
            }
            else if(filter.SelectedIndex == 2)
            {
                clz.IsEnabled = false;
                stD.IsEnabled = true;
                endD.IsEnabled = true;
                dfilter.IsEnabled = true;
                date_l.Visibility = Visibility.Visible;
                clzn_l.Visibility = Visibility.Collapsed;
            }
            else
            {
                clz.IsEnabled = false;
                stD.IsEnabled = false;
                endD.IsEnabled = false;
                dfilter.IsEnabled = false;
                date_l.Visibility = Visibility.Collapsed;
                clzn_l.Visibility = Visibility.Collapsed;
                barcode_r.ItemsSource = student.filter().DefaultView;
            }
        }



        private void clz_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            barcode_r.ItemsSource = student.filterByclass(util.DTto2D(class_.filter())[clz.SelectedIndex, 0]).DefaultView;
            cn_l.Text = clz.Text;
        }



        private void expt_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                expt_btn.IsEnabled = false;
                progrss.Visibility = Visibility.Visible;
                PrintDialog printDialog = new PrintDialog();
                if(printDialog.ShowDialog() == true)
                {
                    printDialog.PrintVisual(r_for_print, "Student Barcodes");
                }
            }
            finally
            {
                progrss.Visibility = Visibility.Hidden;
                expt_btn.IsEnabled = true;
            }
        }

        private void dfilter_Click(object sender, RoutedEventArgs e)
        {
            if (stD.SelectedDate != null && endD.SelectedDate != null)
            {
                barcode_r.ItemsSource = student.filter(sd: stD.Text, ed: endD.Text).DefaultView;
            }
        }

        private void stD_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            sdl.Text = stD.Text;
        }

        private void endD_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            edl.Text = endD.Text;
        }

        private void clz_DropDownClosed(object sender, EventArgs e)
        {
            if(clz.SelectedIndex != -1)
            {
                barcode_r.ItemsSource = student.filterByclass(util.DTto2D(class_.filter())[clz.SelectedIndex, 0]).DefaultView;
                cn_l.Text = clz.Text;
            }

        }
    }
}
