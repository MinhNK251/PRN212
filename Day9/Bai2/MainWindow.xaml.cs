using AForge.Video;
using AForge.Video.DirectShow;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Bai2
{
    public partial class MainWindow : Window
    {
        private FilterInfoCollection videoDevices;
        private VideoCaptureDevice videoSource;

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            Closing += MainWindow_Closing;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (videoDevices.Count == 0)
            {
                MessageBox.Show("No video sources found");
                return;
            }

            videoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);
            videoSource.NewFrame += VideoSource_NewFrame;
            videoSource.Start();
        }

        private void VideoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            BitmapImage bitmapImage;
            using (var bitmap = (Bitmap)eventArgs.Frame.Clone())
            {
                bitmapImage = BitmapToBitmapImage(bitmap);
                bitmapImage.Freeze(); // To prevent cross-thread operations
            }

            Dispatcher.Invoke(() =>
            {
                cameraFeed.Source = bitmapImage;
            });
        }

        private BitmapImage BitmapToBitmapImage(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                return bitmapImage;
            }
        }

        private void TakePhoto_Click(object sender, RoutedEventArgs e)
        {
            if (videoSource != null && videoSource.IsRunning)
            {
                videoSource.NewFrame += CapturePhoto;
            }
        }

        private void CapturePhoto(object sender, NewFrameEventArgs eventArgs)
        {
            videoSource.NewFrame -= CapturePhoto;
            using (var bitmap = (Bitmap)eventArgs.Frame.Clone())
            {
                // Determine the project root directory
                string executablePath = AppDomain.CurrentDomain.BaseDirectory;
                string projectRoot = Directory.GetParent(executablePath).Parent.Parent.Parent.FullName;
                string directory = Path.Combine(projectRoot, "Images");
                Directory.CreateDirectory(directory);
                string filePath = Path.Combine(directory, $"photo_{DateTime.Now:yyyyMMdd_HHmmssfff}.jpg");
                bitmap.Save(filePath, ImageFormat.Jpeg);
                MessageBox.Show($"Photo saved to {filePath}");
            }
        }

        private async void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            if (videoSource != null)
            {
                videoSource.NewFrame -= VideoSource_NewFrame;
                if (videoSource.IsRunning)
                {
                    await Task.Run(() =>
                    {
                        videoSource.SignalToStop();
                        videoSource.WaitForStop();
                    });
                }
                videoSource = null;
            }
        }
    }
}
