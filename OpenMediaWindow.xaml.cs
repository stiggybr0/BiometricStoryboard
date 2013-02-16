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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        public String LeftPath
        {
            get
            {
                return this.LeftVideoPath.Text;
            }
        }

        public String RightPath
        {
            get
            {
                return this.RightVideoPath.Text;
            }
        }

        public String DataPath
        {
            get
            {
                return this.BiometricDataPath.Text;
            }
        }
        
 


        private void LeftVideoBrowse_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension
            dlg.DefaultExt = ".txt";
            dlg.Filter = "All Supported File Types(*.mp3,*.wav,*.mpeg,*.wmv,*.avi,*.mp4)|*.mp3;*.wav;*.mpeg;*.wmv;*.avi;*.mp4";
            // Display OpenFileDialog by calling ShowDialog method
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox
            if (result == true)
            {
                // Open document
                string filename = dlg.FileName;
                LeftVideoPath.Text = filename;
            }
        }

        private void RightVideoBrowse_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension
            dlg.DefaultExt = ".txt";

            dlg.Filter = "All Supported File Types(*.mp3,*.wav,*.mpeg,*.wmv,*.avi, *.mp4)|*.mp3;*.wav;*.mpeg;*.wmv;*.avi;*.mp4";

            // Display OpenFileDialog by calling ShowDialog method
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox
            if (result == true)
            {
                // Open document
                string filename = dlg.FileName;
                RightVideoPath.Text = filename;
            }
        }

        private void BiometricDataBrowse_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension
            dlg.DefaultExt = ".dat";
            dlg.Filter = "All Supported File Types (.dat)|*.dat";

            // Display OpenFileDialog by calling ShowDialog method
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox
            if (result == true)
            {
                // Open document
                string filename = dlg.FileName;
                BiometricDataPath.Text = filename;
            }
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }
    }
}
