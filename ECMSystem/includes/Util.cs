using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ZXing;


namespace ECMSystem.includes
{
    class Util
    {
        //by sandakelum priyamantha
        public string[,] DTto2D(DataTable dataTable)
        {
            string[,] str2d = new string[dataTable.Rows.Count,dataTable.Columns.Count];
            for(int i=0;i< dataTable.Rows.Count; i++)
            {
                for(int j=0; j < dataTable.Columns.Count; j++)
                {
                    str2d[i, j] = dataTable.Rows[i][j].ToString();
                }
            }

            return str2d;
        }

        public void fillComboBox(ComboBox comboBox,string[,] data,int column)
        {
            for (int i = 0; i < data.GetLength(0); i++)
            {
               comboBox.Items.Add(data[i, column]);
            }
        }

        public void createBarcode(TextBox textBox, System.Windows.Controls.Image imgui)
        {
            try
            {
                System.Drawing.Image img = null;
                BitmapImage bimg = new BitmapImage();
                using (var ms = new MemoryStream())
                {
                    BarcodeWriter writer;
                    writer = new ZXing.BarcodeWriter() { Format = BarcodeFormat.CODE_128 };
                    writer.Options.Height = 180;
                    writer.Options.Width = 380;
                    writer.Options.PureBarcode = false;
                    img = writer.Write(textBox.Text);
                    img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    ms.Position = 0;
                    bimg.BeginInit();
                    bimg.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                    bimg.CacheOption = BitmapCacheOption.OnLoad;
                    bimg.UriSource = null;
                    bimg.StreamSource = ms;
                    bimg.EndInit();
                    imgui.Source = bimg;// return File(ms.ToArray(), "image/jpeg");  
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void SaveToPng(FrameworkElement visual, string fileName)
        {
            var encoder = new PngBitmapEncoder();
            SaveUsingEncoder(visual, fileName, encoder);
        }

        // and so on for other encoders (if you want)


        public void SaveUsingEncoder(FrameworkElement visual, string fileName, BitmapEncoder encoder)
        {
            RenderTargetBitmap bitmap = new RenderTargetBitmap((int)visual.ActualWidth, (int)visual.ActualHeight, 96, 96, PixelFormats.Pbgra32);
            bitmap.Render(visual);
            BitmapFrame frame = BitmapFrame.Create(bitmap);
            encoder.Frames.Add(frame);

            using (var stream = File.Create(fileName))
            {
                encoder.Save(stream);
            }
        }


        public  BitmapImage ToBitmapImage(Bitmap bitmap)
        {
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            MemoryStream ms = new MemoryStream();
            bitmap.Save(ms, ImageFormat.Bmp);
            ms.Seek(0, SeekOrigin.Begin);
            bi.StreamSource = ms;
            bi.EndInit();
            return bi;
        }


        public string[] getOneColumn( string[,] data, int column)
        {
            string[] col = new string[data.GetLength(0)];
            for (int i = 0; i < data.GetLength(0); i++)
            {
                col[i] = data[column, i];
            }
            return col;
        }



    }
}
