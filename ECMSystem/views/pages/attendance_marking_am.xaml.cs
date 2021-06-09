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
    /// Interaction logic for attendance_marking_am.xaml
    /// </summary>
    public partial class attendance_marking_am : Page
    {

        FilterInfoCollection filterInfoCollection;
        private IVideoSource _videoSource;

        includes.Util util = new includes.Util();
        //model.Class_ class_ = new model.Class_();
        model.Stu_class stu_Class = new model.Stu_class();
        model.Student student = new model.Student();
        model.Payment payment = new model.Payment();
        model.Attendance attendance = new model.Attendance();

        public attendance_marking_am()
        {
            InitializeComponent();
            mark_btn.IsEnabled = false;
            stop_cam.IsEnabled = false;

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
                    if (result != null)
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



        private void stat_cam_Click(object sender, RoutedEventArgs e)
        {
            startcam();
        }

        private void stop_cam_Click(object sender, RoutedEventArgs e)
        {
            stopcam();
        }

        private void class_select_DropDownClosed(object sender, EventArgs e)
        {
            if (class_select.SelectedIndex != -1)
            {
                mark_btn.IsEnabled = true;
                string sid = util.DTto2D(student.filter(by: "nic", key: nic_p.Text))[0, 0];
                string cid = util.DTto2D(stu_Class.filter(by: "stu_class.student_id", key: sid))[class_select.SelectedIndex, 1];
                if (payment.checkPayment(sid: sid, cid: cid))
                {
                    //MessageBox.Show("pay");
                    msg.Foreground = System.Windows.Media.Brushes.Green;
                    msg.Text = "Paid";
                }
                else
                {
                    msg.Foreground = System.Windows.Media.Brushes.Red;
                    msg.Text = "Not Paid";
                   
                }

            }

        }

        private void nic_p_TextChanged(object sender, TextChangedEventArgs e)
        {
            msg.Text = "";
            if (nic_p.Text != null)
            {

                try
                {
                    string sid = util.DTto2D(student.filter(by: "nic", key: nic_p.Text))[0, 0];
                    class_select.Items.Clear();
                    util.fillComboBox(class_select, util.DTto2D(stu_Class.filter(by: "stu_class.student_id", key: sid)), 2);
                    mark_btn.IsEnabled = false;
                }
                catch (Exception)
                {
                    class_select.Items.Clear();
                    nic_p.Text = "Unregisterd Student";
                    mark_btn.IsEnabled = false;

                }
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            stat_cam.IsEnabled = true;
            stop_cam.IsEnabled = true;
            filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo device in filterInfoCollection)
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

        private void mark_btn_Click(object sender, RoutedEventArgs e)
        {
            string sid = util.DTto2D(student.filter(by: "nic", key: nic_p.Text))[0, 0];
            string cid = util.DTto2D(stu_Class.filter(by: "stu_class.student_id", key: sid))[class_select.SelectedIndex, 1];
            if (attendance.save(sid: sid, cid: cid) == 1)
            {
                //msg.Text = "";
                nic_p.Clear();
                class_select.Items.Clear();

            }

        }

        private void view_today_Click(object sender, RoutedEventArgs e)
        {
            views.today_atted today_Atted = new views.today_atted();
            today_Atted.ShowDialog();
        }
    }
}
