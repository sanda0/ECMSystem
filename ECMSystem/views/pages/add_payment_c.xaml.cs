using AForge.Video;
using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
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
using ZXing;


namespace ECMSystem.views.pages
{
    /// <summary>
    /// Interaction logic for add_payment_c.xaml
    /// </summary>
    public partial class add_payment_c : Page
    {
        FilterInfoCollection filterInfoCollection;    
        private IVideoSource _videoSource;
        includes.Util util = new includes.Util();
        //model.Class_ class_ = new model.Class_();
        model.Stu_class stu_Class = new model.Stu_class();
        model.Student student = new model.Student();
        model.Payment payment = new model.Payment();

        public add_payment_c()
        {
            InitializeComponent();
            add_pay.IsEnabled = false;
            stop_cam.IsEnabled = false;
            
        }

        private void add_pay_Click(object sender, RoutedEventArgs e)
        {
            string sid = util.DTto2D(student.filter(by: "nic", key: nic_p.Text))[0, 0];
            string cid = util.DTto2D(stu_Class.filter(by: "stu_class.student_id", key: sid))[class_select.SelectedIndex, 1];
            if(payment.save(sid: sid, cid: cid) == 1)
            {
                msg.Foreground = System.Windows.Media.Brushes.Green;
                msg.Text = "Payment added";
            }
            
            nic_p.Clear();
            class_select.Items.Clear();
        }

        private void page_load(object sender, RoutedEventArgs e)
        {
            stat_cam.IsEnabled = true;
            stop_cam.IsEnabled = true;
            filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach(FilterInfo device in filterInfoCollection)
            {
                sel_cam.Items.Add(device.Name);

            }
            try
            {
                sel_cam.SelectedIndex = 0;
                stat_cam.IsEnabled = true;

            }
            catch (Exception)
            {

            }
        }
        private void startcam()
        {
            if (filterInfoCollection[0] != null)
            {
                stat_cam.IsEnabled = false;
                stop_cam.IsEnabled = true;

                _videoSource = new VideoCaptureDevice(filterInfoCollection[sel_cam.SelectedIndex].MonikerString);
                _videoSource.NewFrame += VideoCaptureDevice_newFrame;
                _videoSource.Start();
            }
        }

        private void stopcam()
        {
            if (_videoSource != null && _videoSource.IsRunning)
            {
                stat_cam.IsEnabled = true;
                stop_cam.IsEnabled = false;
                _videoSource.SignalToStop();
                _videoSource.NewFrame -= new NewFrameEventHandler(VideoCaptureDevice_newFrame);
            }
        }

        private void stat_cam_Click(object sender, RoutedEventArgs e)
        {
            startcam();

        }

        private void VideoCaptureDevice_newFrame(object sender, NewFrameEventArgs eventArgs)
        {
            try
            {
                BitmapImage bi;
                using (var bitmap = (Bitmap)eventArgs.Frame.Clone())
                {
                    bi = util.ToBitmapImage(bitmap);
                    BarcodeReader reader = new BarcodeReader();
                    var result = reader.Decode(bi);
                    if(result != null)
                    {
                        //nic_p.Text = result.Text;
                        Dispatcher.BeginInvoke(new ThreadStart(delegate { nic_p.Text = result.Text; }));
                        //MessageBox.Show(result.Text);
                    }
                }
                bi.Freeze(); // avoid cross thread operations and prevents leaks
                Dispatcher.BeginInvoke(new ThreadStart(delegate { barcode_img.Source = bi; }));
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error on _videoSource_NewFrame:\n" + exc.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                stopcam();
            }
        }

        private void stop_cam_Click(object sender, RoutedEventArgs e)
        {
            stopcam();
        }

        private void nic_p_TextChanged(object sender, TextChangedEventArgs e)
        {
            msg.Text = "";
            if ( nic_p.Text != null)
            {
                
                try
                {
                    string sid = util.DTto2D(student.filter(by: "nic", key: nic_p.Text))[0, 0];
                    class_select.Items.Clear();
                    util.fillComboBox(class_select, util.DTto2D(stu_Class.filter(by: "stu_class.student_id", key: sid)),2);
                    add_pay.IsEnabled = false;
                }
                catch (Exception)
                {
                    class_select.Items.Clear();
                    nic_p.Text = "Unregisterd Student";
                    add_pay.IsEnabled = false;

                }
            }

           
        }

        private void class_select_DropDownClosed(object sender, EventArgs e)
        {
            if(class_select.SelectedIndex != -1)
            {
                string sid = util.DTto2D(student.filter(by: "nic", key: nic_p.Text))[0, 0];
                string cid = util.DTto2D(stu_Class.filter(by: "stu_class.student_id", key: sid))[class_select.SelectedIndex, 1];
                if (payment.checkPayment(sid: sid, cid: cid))
                {
                    //MessageBox.Show("pay");
                    msg.Foreground = System.Windows.Media.Brushes.Green;
                    msg.Text = "Paid";
                    add_pay.IsEnabled = false;


                }
                else
                {
                    msg.Foreground = System.Windows.Media.Brushes.Red;
                    msg.Text = "Not Paid";
                    add_pay.IsEnabled = true;

                }

            }
        }
    }
}
