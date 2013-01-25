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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;
using System.Windows.Threading;

namespace BiometricStoryboard
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool LeftIsDragging = false;
        bool RightIsDragging = false;

        DispatcherTimer LeftTimer;
        public delegate void LeftTimerTick();
        LeftTimerTick LeftTick;

        DispatcherTimer RightTimer;
        public delegate void RightTimerTick();
        RightTimerTick RightTick;


        public MainWindow()
        {
            InitializeComponent();

            LeftTimer = new DispatcherTimer();
            LeftTimer.Interval = TimeSpan.FromSeconds(1);
            LeftTimer.Tick += new EventHandler(LeftTimer_Tick);
            LeftTick = new LeftTimerTick(LeftChangeStatus);

            RightTimer = new DispatcherTimer();
            RightTimer.Interval = TimeSpan.FromSeconds(1);
            RightTimer.Tick += new EventHandler(RightTimer_Tick);
            RightTick = new RightTimerTick(RightChangeStatus);
        }
        bool LeftIsPlaying;
        bool RightIsPlaying;
        private void playButtonClick(object sender, RoutedEventArgs e)
        {
            LeftVideo.Play();
            RightVideo.Play();
            LeftIsPlaying = true;
            RightIsPlaying = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
       

        //turn volume up-down
        private void LeftVolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            LeftVideo.Volume = LeftVolumeSlider.Value;
        }

        private void RightVolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            RightVideo.Volume = RightVolumeSlider.Value;
        }

        private void LeftBrowse_Click(object sender, RoutedEventArgs e)
        {
            Stream checkStream = null;
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "All Supported File Types(*.mp3,*.wav,*.mpeg,*.wmv,*.avi)|*.mp3;*.wav;*.mpeg;*.wmv;*.avi";
            // Show open file dialog box 
            if ((bool)dlg.ShowDialog())
            {
                try
                {
                    // check if something is selected
                    if ((checkStream = dlg.OpenFile()) != null)
                    {
                        LeftVideo.Source = new Uri(dlg.FileName);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }
        
        private void RightBrowse_Click(object sender, RoutedEventArgs e)
        {
            Stream checkStream = null;
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "All Supported File Types(*.mp3,*.wav,*.mpeg,*.wmv,*.avi)|*.mp3;*.wav;*.mpeg;*.wmv;*.avi";
            // Show open file dialog box 
            if ((bool)dlg.ShowDialog())
            {
                try
                {
                    // check if something is selected
                    if ((checkStream = dlg.OpenFile()) != null)
                    {
                        RightVideo.Source = new Uri(dlg.FileName);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        void LeftChangeStatus()
        {
            if (LeftIsPlaying)
            {
                string sec, min, hours;

                #region customizeTime
                if (LeftVideo.Position.Seconds < 10)
                    sec = "0" + LeftVideo.Position.Seconds.ToString();
                else
                    sec = LeftVideo.Position.Seconds.ToString();


                if (LeftVideo.Position.Minutes < 10)
                    min = "0" + LeftVideo.Position.Minutes.ToString();
                else
                    min = LeftVideo.Position.Minutes.ToString();

                if (LeftVideo.Position.Hours < 10)
                    hours = "0" + LeftVideo.Position.Hours.ToString();
                else
                    hours = LeftVideo.Position.Hours.ToString();

                #endregion customizeTime

                LeftSeekSlider.Value = LeftVideo.Position.TotalMilliseconds;
                LeftProgressBar.Value = LeftVideo.Position.TotalMilliseconds;

                if (LeftVideo.Position.Hours == 0)
                {
                    LeftCurrentTimeTextBlock.Text = min + ":" + sec;
                }
                else
                {
                    LeftCurrentTimeTextBlock.Text = hours + ":" + min + ":" + sec;
                }
            }
        }

        void RightChangeStatus()
        {
            if (LeftIsPlaying)
            {
                string sec, min, hours;

                #region customizeTime
                if (LeftVideo.Position.Seconds < 10)
                    sec = "0" + LeftVideo.Position.Seconds.ToString();
                else
                    sec = LeftVideo.Position.Seconds.ToString();


                if (LeftVideo.Position.Minutes < 10)
                    min = "0" + LeftVideo.Position.Minutes.ToString();
                else
                    min = LeftVideo.Position.Minutes.ToString();

                if (LeftVideo.Position.Hours < 10)
                    hours = "0" + LeftVideo.Position.Hours.ToString();
                else
                    hours = LeftVideo.Position.Hours.ToString();

                #endregion customizeTime

                RightSeekSlider.Value = LeftVideo.Position.TotalMilliseconds;
                RightProgressBar.Value = LeftVideo.Position.TotalMilliseconds;

                if (LeftVideo.Position.Hours == 0)
                {
                    RightCurrentTimeTextBlock.Text = min + ":" + sec;
                }
                else
                {
                    RightCurrentTimeTextBlock.Text = hours + ":" + min + ":" + sec;
                }
            }
        }

        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            
            
            if (LeftIsPlaying)
            {
                try
                {
                    LeftVideo.Pause();
                    LeftIsPlaying = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Left Video Pause Error" + ex.Message);
                }
            }
            if (RightIsPlaying)
            {
                try
                {
                    RightVideo.Pause();
                    RightIsPlaying = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Right Video Pause Error" + ex.Message);
                }

            }
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            if (LeftIsPlaying == true)
            {
                try
                {

                    LeftVideo.Stop();
                    LeftIsPlaying = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Left Video Stop Error" + ex.Message);
                }
            }
            if (RightIsPlaying == true)
            {
                try
                {
                    RightVideo.Stop();
                    RightIsPlaying = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Right Video Stop Error" + ex.Message);
                }
            }
        }

        private void OpenMediaButton_Click(object sender, RoutedEventArgs e)
        {
            var OpenMedia = new OpenMediaWindow();
            OpenMedia.ShowDialog();
        }

        public void LeftVideo_MediaOpened(object sender, RoutedEventArgs e)
        {
            LeftTimer.Start();
            LeftIsPlaying = true;
            LeftOpenMedia();
        }
        public void RightVideo_MediaOpened(object sender, RoutedEventArgs e)
        {
            RightTimer.Start();
            RightIsPlaying = true;
            RightOpenMedia();
        }
        void LeftTimer_Tick(object sender, EventArgs e)
        {
            Dispatcher.Invoke(LeftTick);
        }
        void RightTimer_Tick(object sender, EventArgs e)
        {
            Dispatcher.Invoke(RightTick);
        }
        private void LeftVideo_MediaEnded(object sender, RoutedEventArgs e)
        {
            LeftVideo.Stop();
            LeftVolumeSlider.ValueChanged -= new RoutedPropertyChangedEventHandler<double>(LeftVolumeSlider_ValueChanged);
        }
        private void RightVideo_MediaEnded(object sender, RoutedEventArgs e)
        {
            RightVideo.Stop();
            RightVolumeSlider.ValueChanged -= new RoutedPropertyChangedEventHandler<double>(LeftVolumeSlider_ValueChanged);
        }

        public void LeftOpenMedia()
        {
 
        }
        public void RightOpenMedia()
        {
 
        }
        public MediaElement getLeftVideo
        {
            get
            {
                return LeftVideo;
            }
        }
        public MediaElement getRightVideo
        {
            get
            {
                return RightVideo;
            }
        }



        //left
        private void LeftSeekSlider_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            TimeSpan ts = new TimeSpan(0, 0, 0, 0, (int)LeftSeekSlider.Value);

            LeftChangePosition(ts);
        }

        //mouse down on slide bar in order to seek
        private void LeftSeekSlider_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            LeftIsDragging = true;
        }

        private void LeftSeekSlider_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (LeftIsDragging)
            {
                TimeSpan ts = new TimeSpan(0, 0, 0, 0, (int)LeftSeekSlider.Value);
                LeftChangePosition(ts);
            }
            LeftIsDragging = false;
        }

        //change position of the file
        void LeftChangePosition(TimeSpan ts)
        {
            LeftVideo.Position = ts;
        }

        //right
        private void RightSeekSlider_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            TimeSpan ts = new TimeSpan(0, 0, 0, 0, (int)RightSeekSlider.Value);

            RightChangePosition(ts);
        }

        //mouse down on slide bar in order to seek
        private void RightSeekSlider_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            RightIsDragging = true;
        }

        private void RightSeekSlider_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (RightIsDragging)
            {
                TimeSpan ts = new TimeSpan(0, 0, 0, 0, (int)RightSeekSlider.Value);
                RightChangePosition(ts);
            }
            RightIsDragging = false;
        }

        //change position of the file
        void RightChangePosition(TimeSpan ts)
        {
            RightVideo.Position = ts;
        }
    }
}
