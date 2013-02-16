using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;
using System.Windows.Forms;

namespace BiometricStoryboard
{
    /// <summary>
    /// Interaction logic for OpenMediaWindow.xaml
    /// </summary>
    public partial class OpenMediaWindow : Window
    {
        public OpenMediaWindow()
        {
            InitializeComponent();
        }
        //Uri RightPath;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
        /*
        private void LeftVideoBrowse_Click(object sender, RoutedEventArgs e)
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
                        RightPath = new Uri(dlg.FileName);
                        
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }
        */
        /*
        public Uri ReturnRightPath()
        {
            return RightPath;
        }
         * */
        /*
        private void LeftVideoBrowse_Click(object sender, RoutedEventArgs e)
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

        private void RightVideoBrowse_Click(object sender, RoutedEventArgs e)
        {
            Stream checkStream = null;
            OpenFileDialog dlg = new OpenFileDialog();
            Window MainWin = (Window)VisualTreeHelper.GetParent(this);
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
        
        public static Window GetWindowRef_ByName(string strWindowName)
        {
            foreach (Window win in Application.Current.Windows)
                if (win.Name == strWindowName) return win;

            //Got here? Then the window hasn't been loaded.
            return null;
        }
         * */
    }
}
