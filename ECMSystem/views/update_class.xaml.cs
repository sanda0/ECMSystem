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
    /// Interaction logic for update_class.xaml
    /// </summary>
    public partial class update_class : Window

    {
        model.Class_ class_ = new model.Class_();
        model.Subject subject = new model.Subject();
        model.teacher teacher = new model.teacher();
        includes.Util util = new includes.Util();
        DataGrid table;
        public update_class(DataGrid dataGrid)
        {
            InitializeComponent();
            table = dataGrid;
            util.fillComboBox(subject_select, util.DTto2D(subject.filter()), 1);
            util.fillComboBox(teacher_select, util.DTto2D(teacher.filter()), 1);
            desable();
        }

        public void cls()
        {
            cls_name.Clear();
            fees.Clear();
            select_weekday.SelectedIndex = -1;
            start_time.Text = "";
            end_time.Text = "";
            subject_select.SelectedIndex = -1;
            teacher_select.SelectedIndex = -1;
        }

        public void desable()
        {
            cls_name.IsEnabled = false;
            fees.IsEnabled = false;
            select_weekday.IsEnabled = false;
            start_time.IsEnabled = false;
            end_time.IsEnabled = false;
            subject_select.IsEnabled = false;
            teacher_select.IsEnabled = false;
            up_clz_btn.IsEnabled = false;
        }
        public void enable()
        {
            cls_name.IsEnabled = true;
            fees.IsEnabled = true;
            select_weekday.IsEnabled = true;
            start_time.IsEnabled = true;
            end_time.IsEnabled = true;
            subject_select.IsEnabled = true;
            teacher_select.IsEnabled = true;
            up_clz_btn.IsEnabled = true;
        }

        private void clz_id_txt_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (clz_id_txt.Text != "")
            {
                if (class_.find(clz_id_txt.Text) == 1)
                {
                    if (class_.id == clz_id_txt.Text)
                    {
                        enable();
                        cls_name.Text = class_.name;
                        fees.Text = class_.fee;
                        select_weekday.Text = class_.day;
                        start_time.Text = class_.start_time;
                        end_time.Text = class_.end_time;
                        subject_select.Text = class_.subject_id;
                        teacher_select.Text = class_.teacher_id;

                       
                        
                    }
                    else
                    {
                        cls();
                        desable();
                    }

                }
            }
            else
            {
                cls();
                desable();
            }


        }

        private void close_btn_cls_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }



        private void up_clz_btn_Click(object sender, RoutedEventArgs e)
        {
            class_.name = cls_name.Text;
            class_.fee = fees.Text;
            class_.day = select_weekday.Text;
            class_.start_time = start_time.Text;
            class_.end_time = end_time.Text;
            class_.subject_id = util.DTto2D(subject.filter())[subject_select.SelectedIndex, 0];
            class_.teacher_id = util.DTto2D(teacher.filter())[teacher_select.SelectedIndex, 0];


            if (class_.update() == 1)
            {
               
                table.ItemsSource = class_.filter().DefaultView;
                this.Close();
            }
            /*
            else
            {
                msg.Foreground = Brushes.Red;
                msg.Text = "Teacher update faild!";
            }*/
        }
    }
}
