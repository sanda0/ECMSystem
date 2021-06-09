using MaterialDesignThemes.Wpf;
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
    /// Interaction logic for add_class.xaml
    /// </summary>
    public partial class add_class : Window
    {
        model.Class_ class_ = new model.Class_();
        model.Subject subject = new model.Subject();
        model.teacher teacher = new model.teacher();
        includes.Util util = new includes.Util();
        includes.validation validation = new includes.validation();
        DataGrid table;
        public add_class(DataGrid dataGrid)
        {
            InitializeComponent();
            table = dataGrid;
            util.fillComboBox(subject_select, util.DTto2D(subject.filter()), 1);
            util.fillComboBox(teacher_select, util.DTto2D(teacher.filter()), 1);

        }



        private void add_cls_btn_Click_1(object sender, RoutedEventArgs e)
        {
            int condi = 0;
            TextBox[] textBoxes = { cls_name, fee };
            TextBlock[] textboxes_v = { cls_name_v, fee_v };
            ComboBox[] combos = { teacher_select, subject_select, select_weekday };
            TextBlock[] combos_v = { teacher_v, subject_v, weekday_v };
            TimePicker[] timePickers = { start_time, end_time };
            TextBlock[] timepic_v = { start_time_v, end_time_v };

            if (validation.number_of_fill(textBoxes, textboxes_v) == 2)
            {
                condi++;
            }
            if (validation.check_comboBoxes(combos, combos_v) == 3)
            {
                
                condi++;
            }
            if (validation.check_timePickers(timePickers, timepic_v) == 2)
            {
                condi++;
            }

            //
            if(condi == 3)
            {
                class_.name = cls_name.Text;
                class_.fee = fee.Text;
                class_.day = select_weekday.Text;
                class_.teacher_id = util.DTto2D(teacher.filter())[teacher_select.SelectedIndex, 0];
                class_.subject_id = util.DTto2D(subject.filter())[subject_select.SelectedIndex, 0];
                class_.start_time = start_time.Text;
                class_.end_time = end_time.Text;

                if(class_.save() == 1)
                {
                    table.ItemsSource = class_.filter().DefaultView;
                    this.Close();
                }
                else
                {
                    string[] btns = { "ok" };
                    custom_controlles.msgbox msgbox = new custom_controlles.msgbox(MaterialDesignThemes.Wpf.PackIconKind.Error, "Error", "", Brushes.Red, btns);
                    msgbox.ShowDialog();
                }
            }
        }

        private void close_btn_cls_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            
        }

        private void start_time_SelectedTimeChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
        {

        }

        private void end_time_SelectedTimeChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
        {

        }
    }
}
