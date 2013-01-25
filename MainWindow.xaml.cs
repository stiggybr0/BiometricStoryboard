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

namespace BiometricStoryboard
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
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

        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            
            
            if (LeftIsPlaying == true)
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
            if (RightIsPlaying == true)
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
    
        
    }
}
