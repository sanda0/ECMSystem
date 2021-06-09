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

namespace ECMSystem.testing
{
    /// <summary>
    /// Interaction logic for test1.xaml
    /// </summary>
    public partial class test1 : Window
    {
        public test1()
        {
            InitializeComponent();
        }

        private void start(object sender, RoutedEventArgs e)
        {
            model.teacher teacher = new model.teacher();
            includes.Util conv = new includes.Util();
            string[,] data = conv.DTto2D(teacher.filter());

            le.Text = data.GetLength(0).ToString() ;

            for(int i = 0;i< data.GetLength(0); i++)
            {
                urs.Items.Add(data[i, 1]);
            }
        }
    }
}
