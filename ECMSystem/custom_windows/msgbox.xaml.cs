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

namespace ECMSystem.custom_controlles
{
    /// <summary>
    /// Interaction logic for msgbox.xaml
    /// </summary>
    public partial class msgbox : Window
    {
        public msgbox(MaterialDesignThemes.Wpf.PackIconKind i, string h , string sh ,Brush color,string[] btns)
        {
            
            InitializeComponent();

            icon.Kind = i;
            icon.Foreground = color;
            heading.Text = h;
            subhead.Text = sh;
            ok_btn.Background = color;
            //cancle_btn.Background = color;
            cancle_btn.Visibility = Visibility.Collapsed;
            if(btns.Contains("ok")){
                ok_btn.Visibility = Visibility.Visible;
            }
            else
            {
                ok_btn.Visibility = Visibility.Collapsed;
            }

            


        }

        private void ok_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ok_cancle_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cancle_btn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
