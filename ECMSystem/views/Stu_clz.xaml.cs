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
    /// Interaction logic for Stu_clz.xaml
    /// </summary>
    public partial class Stu_clz : Window
    {
        model.Student student = new model.Student();
        model.Stu_class stu_Class = new model.Stu_class();
        model.Class_ class_ = new model.Class_();
        includes.Util util = new includes.Util();

        public Stu_clz(string sid)
        {
            InitializeComponent();
           
            student.find(sid: sid);
            util.fillComboBox(c1, util.DTto2D(class_.filter()), 1);
            util.fillComboBox(c2, util.DTto2D(class_.filter()), 1);
            util.fillComboBox(c3, util.DTto2D(class_.filter()), 1);

            c1.Text = student.clz1;
            c2.Text = student.clz2;
            c3.Text = student.clz3;
            

        }

        private void save_btn_Click(object sender, RoutedEventArgs e)
        {
            string[,] clzData = util.DTto2D(class_.filter());

            //c1
            if(student.clz1 == null && c1.SelectedIndex != -1)
            {
                stu_Class.save("c1", student.id, clzData[c1.SelectedIndex, 0]);
            }
            else if(c1.SelectedIndex != -1)
            {
                stu_Class.update(sid: student.id, cid: clzData[c1.SelectedIndex, 0],c_no:"c1");
            }
            //c2
            if(student.clz2 == null && c2.SelectedIndex != -1)
            {
                stu_Class.save("c2", student.id, clzData[c2.SelectedIndex, 0]);
            }
            else if(c2.SelectedIndex != -1)
            {
                stu_Class.update(sid: student.id, cid: clzData[c2.SelectedIndex, 0],c_no:"c2");
            }
            //c3
            if(student.clz3 == null && c3.SelectedIndex != -1)
            {
                stu_Class.save("c3", student.id, clzData[c3.SelectedIndex, 0]);
            }
            else if(c3.SelectedIndex != -1)
            {
                stu_Class.update(sid: student.id, cid: clzData[c3.SelectedIndex, 0],c_no:"c3");
            }
            this.Close();
        }

        private void c1_del_Click(object sender, RoutedEventArgs e)
        {
            c1.SelectedIndex = -1;
            stu_Class.delete(sid: student.id, c_no: "c1");
        }

        private void c2_del_Click(object sender, RoutedEventArgs e)
        {
            c2.SelectedIndex = -1;
            stu_Class.delete(sid: student.id, c_no: "c2");
        }

        private void c3_del_Click(object sender, RoutedEventArgs e)
        {
            c3.SelectedIndex = -1;
            stu_Class.delete(sid: student.id, c_no: "c3");
        }

        private void close_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
