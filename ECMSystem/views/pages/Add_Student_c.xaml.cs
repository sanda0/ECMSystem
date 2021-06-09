using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace ECMSystem.views.pages
{
    /// <summary>
    /// Interaction logic for Add_student_c.xaml
    /// </summary>
    public partial class Add_student_c : Page
    {
        model.Class_ class_ = new model.Class_();
        includes.Util util = new includes.Util();
       
        includes.validation validation = new includes.validation();
        

        Frame mainwin;
        public Add_student_c(Frame frame)
        {
            InitializeComponent();
            util.fillComboBox(clz_1, util.DTto2D(class_.filter()), 1);
            util.fillComboBox(clz_2, util.DTto2D(class_.filter()), 1);
            util.fillComboBox(clz_3, util.DTto2D(class_.filter()), 1);
            mainwin = frame;
           


        }

        private void cls()
        {
            f_name.Clear();
            l_name.Clear();
            nic.Clear();
            tel.Clear();
            addr.Clear();
            clz_1.SelectedIndex = -1;
            clz_2.SelectedIndex = -1;
            clz_3.SelectedIndex = -1;
            dob.SelectedDate = null;

        }

        private void save_btn_Click(object sender, RoutedEventArgs e)
        {
            int c = 0;
            TextBox[] textBoxes = { f_name, l_name, nic, tel, addr };
            TextBlock[] textBlocks = { f_name_v, l_name_v, nic_v, tele_v, addr_v };
            string[,] clz_list = util.DTto2D(class_.filter());

            string g="";
            if ((bool)g_m.IsChecked)
            {
                g = g_m.Content.ToString();
            }else if ((bool)g_f.IsChecked)
            {
                g = g_f.Content.ToString();
            }


            if(validation.number_of_fill(textBoxes,textBlocks) == 5)
            {
                c++;
            }
            if (validation.is_setDate(dob))
            {
                c++;
            }
            else
            {
                dob_v.Text = "Please select date";
            }

            if(c == 2)
            {
                model.Student student = new model.Student();
                model.Stu_class stu_Class = new model.Stu_class();


                /////////////////////////////////////////////////////////////////////////////////
                student.f_name = f_name.Text;student.l_name = l_name.Text;student.dob = dob.Text;
                student.nic = nic.Text; student.tp = tel.Text; student.addr = addr.Text;
                student.gender = g;

                student.clz1 = clz_1.Text; 
                student.clz2 = clz_2.Text;
                student.clz3 = clz_3.Text;

      

                string folderpath = System.IO.Path.Combine(@"c:\", "bcodes");
                try
                {
                    System.IO.Directory.CreateDirectory(folderpath);
                }
                catch (Exception)
                {
                    ///pass
                }
                student.barcode_path = folderpath +"\\"+ student.nic + ".png";
            
                if (student.save() == 1)
                {
                    util.SaveToPng(barcode, student.barcode_path);

                    stu_Class.student_id = util.DTto2D(student.filter("nic", student.nic))[0, 0];

                    if (clz_1.SelectedIndex != -1)
                    {

                        stu_Class.class_id = clz_list[clz_1.SelectedIndex, 0];
                        stu_Class.save("c1");
                    }

                    if (clz_2.SelectedIndex != -1)
                    {

                        stu_Class.class_id = clz_list[clz_2.SelectedIndex, 0];
                        stu_Class.save("c2");
                    }

                    if (clz_3.SelectedIndex != -1)
                    {

                        stu_Class.class_id = clz_list[clz_3.SelectedIndex, 0];
                        stu_Class.save("c3");
                    }

                    cls();
                            string[] btns = { "ok" };
                            custom_controlles.msgbox msgbox = new custom_controlles.msgbox(MaterialDesignThemes.Wpf.PackIconKind.CheckCircle, "Student saved", "", Brushes.Green, btns);
                            msgbox.ShowDialog();
                    ////////////////////////////////
                    ///
                    mainwin.Content = new views.pages.Add_student_c(mainwin);
                    c = 0;

                }
                else
                {
                            string[] btns = { "ok" };
                            custom_controlles.msgbox msgbox = new custom_controlles.msgbox(MaterialDesignThemes.Wpf.PackIconKind.Error, "Error", "", Brushes.Red, btns);
                            msgbox.ShowDialog();
                } 
            }

                

            
            
            
        }

        private void nic_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(nic.Text != "")
            {
                util.createBarcode(nic, barcode);
            }
           
        }
    }
}
