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
    /// Interaction logic for update_teacher.xaml
    /// </summary>
    public partial class update_teacher : Window
    {
        model.teacher teacher = new model.teacher();
        DataGrid table;
        public update_teacher(DataGrid dataGrid)
        {
            InitializeComponent();
            table = dataGrid;
            if(t_id_txt.Text == "")
            {
                cls();
                desable();
            }
        }

        public void enable()
        {
            t_f_name.IsEnabled = true;
            t_tel.IsEnabled = true;
            update_btn.IsEnabled = true;

        }
         public void desable()
        {
            t_f_name.IsEnabled = false;
            t_tel.IsEnabled = false;
            update_btn.IsEnabled = false;
        }

        public void cls()
        {
            t_f_name.Clear();
            t_tel.Clear();
        }

        private void close_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void t_id_txt_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (t_id_txt.Text != "")
            {
                if (teacher.find(t_id_txt.Text) == 1)
                {
                    if (teacher.id == t_id_txt.Text)
                    {
                        enable();
                        t_f_name.Text = teacher.name;
                        t_tel.Text = teacher.tp;

                        msg.Text = "Now you can edit teacher details";
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


        private void update_btn_Click(object sender, RoutedEventArgs e)
        {
            teacher.name = t_f_name.Text;
            teacher.tp = t_tel.Text;
            if (teacher.update() == 1)
            {
                msg.Text = "Teacher updated";
                table.ItemsSource = teacher.filter().DefaultView;
                this.Close();
            }
            else
            {
                msg.Foreground = Brushes.Red;
                msg.Text = "Teacher update faild!";
            }
        }
    }
}
