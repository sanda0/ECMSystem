using LiveCharts;
using LiveCharts.Wpf;
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
    /// Interaction logic for reports.xaml
    /// </summary>
    public partial class reports : Page
    {
        model.Reports repo = new model.Reports();
        includes.Util util = new includes.Util();

        public reports()
        {
            InitializeComponent();
            pr_c.Visibility = Visibility.Collapsed;
            ar_c.Visibility = Visibility.Collapsed;
            ir_c.Visibility = Visibility.Collapsed;
            for_pdf.Visibility = Visibility.Collapsed;
        }



        private void report_type_DropDownClosed(object sender, EventArgs e)
        {
            if(report_type.SelectedIndex == 0)
            {
                //pr
                pr_clz.Items.Clear();
                pr_y.Items.Clear();
                pr_m.Items.Clear();

                pr_c.Visibility = Visibility.Visible;
                ar_c.Visibility = Visibility.Collapsed;
                ir_c.Visibility = Visibility.Collapsed;

                util.fillComboBox(pr_clz, util.DTto2D(repo.getClass(table: "payment")), 1);
                util.fillComboBox(pr_y, util.DTto2D(repo.getYearOrMonthList(what: "year", table: "payment")), 0);
                util.fillComboBox(pr_m, util.DTto2D(repo.getYearOrMonthList(what: "month", table: "payment")), 0);

                //
                //report



            }else if(report_type.SelectedIndex == 1)
            {
                //ar
                ar_clz.Items.Clear();
                ar_y.Items.Clear();
                ar_m.Items.Clear();

                pr_c.Visibility = Visibility.Collapsed;
                ar_c.Visibility = Visibility.Visible;
                ir_c.Visibility = Visibility.Collapsed;

                util.fillComboBox(ar_clz, util.DTto2D(repo.getClass(table: "attendance")), 1);
                util.fillComboBox(ar_y, util.DTto2D(repo.getYearOrMonthList(what: "year", table: "attendance")), 0);
                util.fillComboBox(ar_m, util.DTto2D(repo.getYearOrMonthList(what: "month", table: "attendance")), 0);

            }else if(report_type.SelectedIndex == 2)
            {
                //ir
                ir_y.Items.Clear();
                ir_m.Items.Clear();

                pr_c.Visibility = Visibility.Collapsed;
                ar_c.Visibility = Visibility.Collapsed;
                ir_c.Visibility = Visibility.Visible;

                util.fillComboBox(ir_y, util.DTto2D(repo.getYearOrMonthList(what: "year", table: "payment")), 0);
                util.fillComboBox(ir_m, util.DTto2D(repo.getYearOrMonthList(what: "month", table: "payment")), 0);
            }
            else
            {
                pr_c.Visibility = Visibility.Collapsed;
                ar_c.Visibility = Visibility.Collapsed;
                ir_c.Visibility = Visibility.Collapsed;
            }
        }

        private void clearReport()
        {
            my_pie.Series.Clear();
            r_hedding.Text = "";
            clz_l.Text = "";
            m_l.Text = "";
            t1_name.Text = "";
            t2_name.Text = "";
            data_table.ItemsSource = null;
            data_table2.ItemsSource = null;
            data_table2.Visibility = Visibility.Visible;
            my_pie.Height = 220;
            my_pie.LegendLocation = LegendLocation.Bottom;

        }


        private void pr_btn_Click(object sender, RoutedEventArgs e)
        {
            clearReport();
            r_hedding.Text = "Monthly Payment Report - Students";
            clz_l.Text = "Class : " + pr_clz.Text;
            m_l.Text = "Month : " + pr_y.Text + " - " + pr_m.Text;
            for_pdf.Visibility = Visibility.Visible;
            

            if (pr_clz.SelectedIndex != -1 && pr_y.SelectedIndex != -1 && pr_m.SelectedIndex != -1)
            {
               try
               {

                    double[] chartData = repo.getTotalPayment(y: pr_y.Text, m: pr_m.Text, cid: util.DTto2D(repo.getClass(table: "payment"))[pr_clz.SelectedIndex, 0]);
                    my_pie.Series.Clear();
                    my_pie.Series.Add(new PieSeries { DataLabels = true, Title = "Amount due(LKR)", Fill = Brushes.Orange, StrokeThickness = 0, Values = new ChartValues<double> { chartData[0] - chartData[1] } });
                    my_pie.Series.Add(new PieSeries { DataLabels = true, Title = "Amount received(LKR)", Fill = Brushes.LightGreen, StrokeThickness = 0, Values = new ChartValues<double> { chartData[1] } });
                    t1_name.Text = "Paid students";
                    t2_name.Text = "Unpaid students";

                    data_table.ItemsSource = repo.getPaiedOrUnpaiedList(paied: true, y: pr_y.Text, m: pr_m.Text, cid: util.DTto2D(repo.getClass(table: "payment"))[pr_clz.SelectedIndex, 0]).DefaultView;

                    if(chartData[0]-chartData[1] != 0)
                    {
                        data_table2.ItemsSource = repo.getPaiedOrUnpaiedList(paied: false, y: pr_y.Text, m: pr_m.Text, cid: util.DTto2D(repo.getClass(table: "payment"))[pr_clz.SelectedIndex, 0]).DefaultView;
                    }
                    


               }
               catch (Exception )
               {
                   //MessageBox.Show(ee.ToString());
               }

            }

        }

        private void ar_btn_Click(object sender, RoutedEventArgs e)
        {
            clearReport();
            r_hedding.Text = "Monthly Attendance Report - Students";
            clz_l.Text = "Class : " + ar_clz.Text;
            m_l.Text = "Month : " + ar_y.Text + " - " + ar_m.Text;
            for_pdf.Visibility = Visibility.Visible;
            


            if (ar_clz.SelectedIndex != -1 && ar_y.SelectedIndex != -1 && ar_m.SelectedIndex != -1)
            {
                try
                {

                    int[] chartData = repo.getTotalAttendance(y: ar_y.Text, m: ar_m.Text, cid: util.DTto2D(repo.getClass(table: "attendance"))[ar_clz.SelectedIndex, 0]);
                    my_pie.Series.Clear();
                    my_pie.Series.Add(new PieSeries { DataLabels = true, Title = "Total number of students", Fill = Brushes.Orange, StrokeThickness = 0, Values = new ChartValues<double> { chartData[0] - chartData[1] } });
                    my_pie.Series.Add(new PieSeries { DataLabels = true, Title = "Number of students who attended", Fill = Brushes.LightGreen, StrokeThickness = 0, Values = new ChartValues<double> { chartData[1] } });
                    t1_name.Text = "Students who attended";
                    t2_name.Text = "Students who did not attend";



                    data_table.ItemsSource = repo.getAttendOrNotList (attend: true, y: ar_y.Text, m: ar_m.Text, cid: util.DTto2D(repo.getClass(table: "attendance"))[ar_clz.SelectedIndex, 0]).DefaultView;
                    data_table2.ItemsSource = repo.getAttendOrNotList(attend: false, y: ar_y.Text, m: ar_m.Text, cid: util.DTto2D(repo.getClass(table: "attendance"))[ar_clz.SelectedIndex, 0]).DefaultView;




                }
                catch (Exception)
                {
                    //MessageBox.Show(ee.ToString());
                }

            }


        }

        private void ir_btn_Click(object sender, RoutedEventArgs e)
        {
            clearReport();
            r_hedding.Text = "Monthly All Classes Imcome Report";           
            m_l.Text = "Month : " + ir_y.Text + " - " + ir_m.Text;
            for_pdf.Visibility = Visibility.Visible;
            data_table2.Visibility = Visibility.Collapsed;
            my_pie.Height = 400;
            my_pie.LegendLocation = LegendLocation.Right;

            double tot = 0;

            string[,] chartData = util.DTto2D(repo.getClassesIncome(y: ir_y.Text, m: ir_m.Text));

            for(int i=0; i < chartData.GetLength(0);i++)
            {
                tot = tot + double.Parse(chartData[i, 2]);
                my_pie.Series.Add(new PieSeries { DataLabels = true, Title = chartData[i,1]+" ("+chartData[i,2]+"LKR)", StrokeThickness = 0, Values = new ChartValues<double> { double.Parse(chartData[i,2]) } });
            }




            t1_name.Text = "All Classes Imcome";
            data_table.ItemsSource = repo.getClassesIncome(y: ir_y.Text, m: ir_m.Text).DefaultView;
            t2_name.Text = "Total Income : " + tot.ToString();



        }

        private void expt_btn_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                expt_btn.IsEnabled = false;

                PrintDialog printDialog = new PrintDialog();
                if (printDialog.ShowDialog() == true)
                {
                    printDialog.PrintVisual(for_pdf, "Report");
                }
            }
            finally
            {

                expt_btn.IsEnabled = true;
            }
           
        }
    }
}
