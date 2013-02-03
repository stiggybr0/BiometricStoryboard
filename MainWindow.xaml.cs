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

        bool LeftIsPlaying = false;
        bool RightIsPlaying = false;

        string LeftSec, LeftMin, LeftHours;
        string RightSec, RightMin, RightHours;
        string LeftPath, RightPath, DataPath;


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

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

        void LeftTimer_Tick(object sender, EventArgs e)
        {
            Dispatcher.Invoke(LeftTick);
        }

        void RightTimer_Tick(object sender, EventArgs e)
        {
            Dispatcher.Invoke(RightTick);
        }

        void LeftChangeStatus()
        {
            if (LeftIsPlaying)
            {

                #region customizeTime
                if (LeftVideo.Position.Seconds < 10)
                    LeftSec = "0" + LeftVideo.Position.Seconds.ToString();
                else
                    LeftSec = LeftVideo.Position.Seconds.ToString();


                if (LeftVideo.Position.Minutes < 10)
                    LeftMin = "0" + LeftVideo.Position.Minutes.ToString();
                else
                    LeftMin = LeftVideo.Position.Minutes.ToString();

                if (LeftVideo.Position.Hours < 10)
                    LeftHours = "0" + LeftVideo.Position.Hours.ToString();
                else
                    LeftHours = LeftVideo.Position.Hours.ToString();

                #endregion customizeTime

                LeftSeekSlider.Value = LeftVideo.Position.TotalMilliseconds;
                LeftProgressBar.Value = LeftVideo.Position.TotalMilliseconds;

                if (LeftVideo.Position.Hours == 0)
                {
                    LeftCurrentTimeTextBlock.Text = LeftMin + ":" + LeftSec;
                }
                else
                {
                    LeftCurrentTimeTextBlock.Text = LeftHours + ":" + LeftMin + ":" + LeftSec;
                }
            }
        }

        void RightChangeStatus()
        {
            if (RightIsPlaying)
            {
                #region customizeTime
                if (RightVideo.Position.Seconds < 10)
                    RightSec = "0" + RightVideo.Position.Seconds.ToString();
                else
                    RightSec = RightVideo.Position.Seconds.ToString();
                if (RightVideo.Position.Minutes < 10)
                    RightMin = "0" + RightVideo.Position.Minutes.ToString();
                else
                    RightMin = RightVideo.Position.Minutes.ToString();

                if (RightVideo.Position.Hours < 10)
                    RightHours = "0" + RightVideo.Position.Hours.ToString();
                else
                    RightHours = RightVideo.Position.Hours.ToString();

                #endregion customizeTime

                RightSeekSlider.Value = RightVideo.Position.TotalMilliseconds;
                RightProgressBar.Value = RightVideo.Position.TotalMilliseconds;

                if (RightVideo.Position.Hours == 0)
                {
                    RightCurrentTimeTextBlock.Text = RightMin + ":" + RightSec;
                }
                else
                {
                    RightCurrentTimeTextBlock.Text = RightHours + ":" + RightMin + ":" + RightSec;
                }
            }
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

        public void LeftOpenMedia()
        {
            LeftInitializePropertyValues();
            try
            {
                #region customizeTime
                if (LeftVideo.NaturalDuration.TimeSpan.Seconds < 10)
                    LeftSec = "0" + LeftVideo.Position.Seconds.ToString();
                else
                    LeftSec = LeftVideo.NaturalDuration.TimeSpan.Seconds.ToString();

                if (LeftVideo.NaturalDuration.TimeSpan.Minutes < 10)
                    LeftMin = "0" + LeftVideo.NaturalDuration.TimeSpan.Minutes.ToString();
                else
                    LeftMin = LeftVideo.NaturalDuration.TimeSpan.Minutes.ToString();

                if (LeftVideo.NaturalDuration.TimeSpan.Hours < 10)
                    LeftHours = "0" + LeftVideo.NaturalDuration.TimeSpan.Hours.ToString();
                else
                    LeftHours = LeftVideo.NaturalDuration.TimeSpan.Hours.ToString();

                if (LeftVideo.NaturalDuration.TimeSpan.Hours == 0)
                {

                    LeftEndTimeTextBlock.Text = LeftMin + ":" + LeftSec;
                }
                else
                {
                    LeftEndTimeTextBlock.Text = LeftHours + ":" + LeftMin + ":" + LeftSec;
                }

                #endregion customizeTime
            }
            catch { }
            string path = LeftVideo.Source.LocalPath.ToString();

            double duration = LeftVideo.NaturalDuration.TimeSpan.TotalMilliseconds;
            LeftSeekSlider.Maximum = duration;
            LeftProgressBar.Maximum = duration;

            LeftVideo.Volume = LeftVolumeSlider.Value;
            //LeftVideo.SpeedRatio = LeftSpeedRatioSlider.Value;
            LeftVideo.ScrubbingEnabled = true;

            LeftVolumeSlider.ValueChanged += new RoutedPropertyChangedEventHandler<double>(LeftVolumeSlider_ValueChanged);
            //speedRatioSlider.ValueChanged += new RoutedPropertyChangedEventHandler<double>(speedRatioSlider_ValueChanged);
        }
        public void RightOpenMedia()
        {
            RightInitializePropertyValues();
            try
            {
                #region customizeTime
                if (RightVideo.NaturalDuration.TimeSpan.Seconds < 10)
                    RightSec = "0" + RightVideo.Position.Seconds.ToString();
                else
                    RightSec = RightVideo.NaturalDuration.TimeSpan.Seconds.ToString();

                if (RightVideo.NaturalDuration.TimeSpan.Minutes < 10)
                    RightMin = "0" + RightVideo.NaturalDuration.TimeSpan.Minutes.ToString();
                else
                    RightMin = RightVideo.NaturalDuration.TimeSpan.Minutes.ToString();

                if (RightVideo.NaturalDuration.TimeSpan.Hours < 10)
                    RightHours = "0" + RightVideo.NaturalDuration.TimeSpan.Hours.ToString();
                else
                    RightHours = RightVideo.NaturalDuration.TimeSpan.Hours.ToString();

                if (RightVideo.NaturalDuration.TimeSpan.Hours == 0)
                {

                    RightEndTimeTextBlock.Text = RightMin + ":" + RightSec;
                }
                else
                {
                    RightEndTimeTextBlock.Text = RightHours + ":" + RightMin + ":" + RightSec;
                }

                #endregion customizeTime
            }
            catch { }
            string path = RightVideo.Source.LocalPath.ToString();

            double duration = RightVideo.NaturalDuration.TimeSpan.TotalMilliseconds;
            RightSeekSlider.Maximum = duration;
            RightProgressBar.Maximum = duration;

            RightVideo.Volume = RightVolumeSlider.Value;
            //RightVideo.SpeedRatio = speedRatioSlider.Value;
            RightVideo.ScrubbingEnabled = true;

            RightVolumeSlider.ValueChanged += new RoutedPropertyChangedEventHandler<double>(RightVolumeSlider_ValueChanged);
            //speedRatioSlider.ValueChanged += new RoutedPropertyChangedEventHandler<double>(speedRatioSlider_ValueChanged);
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

        void LeftInitializePropertyValues()
        {
            LeftVideo.Volume = (double)LeftVolumeSlider.Value;
            //LeftVideo.SpeedRatio = (double)speedRatioSlider.Value;
        }

        void RightInitializePropertyValues()
        {
            RightVideo.Volume = (double)RightVolumeSlider.Value;
            //mediaElement.SpeedRatio = (double)speedRatioSlider.Value;
        }

        //left
        private void LeftSeekSlider_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            TimeSpan ts = new TimeSpan(0, 0, 0, 0, (int)LeftSeekSlider.Value);
            LeftChangePosition(ts);
        }

        //right
        private void RightSeekSlider_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            TimeSpan ts = new TimeSpan(0, 0, 0, 0, (int)RightSeekSlider.Value);
            RightChangePosition(ts);
        }

        //mouse down on slide bar in order to seek
        private void LeftSeekSlider_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            LeftIsDragging = true;
            LeftIsPlaying = true;
        }

        //mouse down on slide bar in order to seek
        private void RightSeekSlider_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            RightIsDragging = true;
            RightIsPlaying = true;
        }


        private void LeftSeekSlider_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (LeftIsDragging)
            {
                TimeSpan ts = new TimeSpan(0, 0, 0, 0, (int)LeftSeekSlider.Value);
                LeftChangePosition(ts);
                LeftIsPlaying = true;
            }
            LeftIsDragging = false;
        }

        private void RightSeekSlider_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (RightIsDragging)
            {
                TimeSpan ts = new TimeSpan(0, 0, 0, 0, (int)RightSeekSlider.Value);
                RightChangePosition(ts);
                RightIsPlaying = true;
            }
            RightIsDragging = false;
        }


        //change position of the file
        void LeftChangePosition(TimeSpan ts)
        {
            LeftVideo.Position = ts;
        }

        //change position of the file
        void RightChangePosition(TimeSpan ts)
        {
            RightVideo.Position = ts;
        }
        

        private void playButtonClick(object sender, RoutedEventArgs e)
        {
            LeftVideo.Play();
            RightVideo.Play();
            LeftIsPlaying = true;
            RightIsPlaying = true;
            LeftTimer.Start();
            RightTimer.Start();
        }


        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            if (LeftIsPlaying)
            {
                try
                {
                    LeftVideo.Pause();
                    LeftIsPlaying = false;
                    LeftTimer.Stop();
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
                    RightTimer.Stop();
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
                    LeftTimer.Stop();
                    LeftSeekSlider.Value = 0;
                    LeftProgressBar.Value = 0;
                    LeftCurrentTimeTextBlock.Text = "00:00";
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
                    RightTimer.Stop();
                    RightSeekSlider.Value = 0;
                    RightProgressBar.Value = 0;
                    RightCurrentTimeTextBlock.Text = "00:00";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Right Video Stop Error" + ex.Message);
                }
            }
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

        private void OpenMediaButton_Click(object sender, RoutedEventArgs e)
        {
            OpenMediaWindow OpenMedia = new OpenMediaWindow();
            OpenMedia.ShowDialog();
            if ((bool)OpenMedia.DialogResult)
            {
                LeftPath = OpenMedia.LeftPath;
                RightPath = OpenMedia.RightPath;
                DataPath = OpenMedia.DataPath;
                LeftVideo.Source = new Uri(LeftPath);
                RightVideo.Source = new Uri(RightPath);
            }
        }
        


        private void MakeNoteButton_Click(object sender, RoutedEventArgs e)
        {
            var MakeNote = new NoteWindow();
            MakeNote.ShowDialog();
            
        }        
    }
}
