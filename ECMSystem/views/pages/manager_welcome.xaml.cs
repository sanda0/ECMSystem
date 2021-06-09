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
    /// Interaction logic for manager_welcome.xaml
    /// </summary>
    public partial class manager_welcome : Page
    {
        model.Reports repo = new model.Reports();
        includes.Util util = new includes.Util();

        public manager_welcome()
        {
            InitializeComponent();
            welcome.Text = "Welcome " + includes.globles.f_name;
            today.Text = DateTime.Now.ToLongDateString();


            string y = DateTime.Now.Year.ToString();
            string m = DateTime.Now.Month.ToString();

            util.fillComboBox(clz_select, util.DTto2D(repo.getClassToday(table: "attendance")), 1);


            double tot = 0;

            string[,] chartData = util.DTto2D(repo.getClassesIncome(y: y, m: m));

            for (int i = 0; i < chartData.GetLength(0); i++)
            {
                tot = tot + double.Parse(chartData[i, 2]);
                income_pi.Series.Add(new PieSeries { DataLabels = true, Title = chartData[i, 1] + " (" + chartData[i, 2] + "LKR)", StrokeThickness = 0, Values = new ChartValues<double> { double.Parse(chartData[i, 2]) } });
            }

            income.Text = tot.ToString() + " LKR";



        }

        private void clz_select_DropDownClosed(object sender, EventArgs e)
        {
            if(clz_select.SelectedIndex != -1)
            {
                int[] chartData = repo.getTotalAttendance(y: DateTime.Now.Year.ToString(), m: DateTime.Now.Month.ToString(), cid: util.DTto2D(repo.getClassToday(table: "attendance"))[clz_select.SelectedIndex, 0]);
                attend.Series.Clear();
                attend.Series.Add(new PieSeries { DataLabels = true, Title = "Total number of students", Fill = Brushes.Orange, StrokeThickness = 0, Values = new ChartValues<double> { chartData[0] - chartData[1] } });
                attend.Series.Add(new PieSeries { DataLabels = true, Title = "Number of students who attended", Fill = Brushes.LightGreen, StrokeThickness = 0, Values = new ChartValues<double> { chartData[1] } });

            }

        }
    }
}
