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
    /// Interaction logic for Add_teacher.xaml
    /// </summary>
    public partial class Add_teacher : Window
    {
        DataGrid table;
        public Add_teacher(DataGrid dataGrid)
        {
            InitializeComponent();
            table = dataGrid;

        }

        private void save_btn_Click(object sender, RoutedEventArgs e)
        {
            TextBox[] textBoxes = { t_name, tele };
            TextBlock[] textBlocks = { t_name_v, tele_v };
            includes.validation validation = new includes.validation();
            model.teacher teacher = new model.teacher();
            if(validation.number_of_fill(textBoxes,textBlocks) == 2)
            {
                teacher.name = t_name.Text;
                teacher.tp = tele.Text;
                if(teacher.save() == 1)
                {
                    table.ItemsSource = teacher.filter().DefaultView;
                    this.Close();
                    string[] btns = { "ok" };
                    custom_controlles.msgbox msgbox = new custom_controlles.msgbox(MaterialDesignThemes.Wpf.PackIconKind.CheckCircle, "Teacher saved", "", Brushes.Green, btns);
                    msgbox.ShowDialog();
                }
                else
                {
                    string[] btns = { "ok" };
                    custom_controlles.msgbox msgbox = new custom_controlles.msgbox(MaterialDesignThemes.Wpf.PackIconKind.Error, "Error", "", Brushes.Red, btns);
                    msgbox.ShowDialog();
                }

            }
        }

        private void close_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
