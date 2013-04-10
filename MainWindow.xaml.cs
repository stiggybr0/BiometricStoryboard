﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Drawing;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;
using System.Windows.Threading;
using System.Xml;
using System.Collections;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization;
using System.Windows.Forms.DataVisualization.Charting;
using Microsoft.Research.DynamicDataDisplay.DataSources;
using Microsoft.Research.DynamicDataDisplay;
using Microsoft.Research.DynamicDataDisplay.PointMarkers;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using Microsoft.Research.DynamicDataDisplay.Charts.Navigation;



namespace BiometricStoryboard
{


   
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

        string IomData, NoteString, OutputNotePath;
        ArrayList NoteList = new ArrayList();

        ArrayList TSList = new ArrayList();
        ArrayList EDRList = new ArrayList();
        ArrayList DETList = new ArrayList();
        ArrayList RateList = new ArrayList();
        ArrayList LabList = new ArrayList();
        ArrayList COHList = new ArrayList();
        Form button1Form = new Form();
        CursorCoordinateGraph mouseTrack = new CursorCoordinateGraph();
    
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
                    System.Windows.MessageBox.Show("Error: Left Video Pause Error" + ex.Message);
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
                    System.Windows.MessageBox.Show("Error: Right Video Pause Error" + ex.Message);
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
                    System.Windows.MessageBox.Show("Error: Left Video Stop Error" + ex.Message);
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
                    System.Windows.MessageBox.Show("Error: Right Video Stop Error" + ex.Message);
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
                IomData = System.IO.File.ReadAllText(DataPath);
                char[] delimiterChars = { ' ', ',', '[', ']', '\n', '\t', '\r' };

                string[] words = IomData.Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries);

                int count = 0;
                for (int i = 5; i < words.Length; i++)
                {
                    String s = words[i];
                    if (!(s.Equals("#TS:") || s.Equals("#EDR:") || s.Equals("#DETECT:") || s.Equals("#rate:") || s.Equals("#Lable:") || s.Equals("#coh:")))
                    {
                        if (count == 0)
                        {
                            float fval = System.Convert.ToSingle(s);
                            float seconds = fval / (float)1000.00;
                            TSList.Add(fval);
                        }
                        else if (count == 1)
                        {
                            float fval = System.Convert.ToSingle(s);
                            EDRList.Add(fval);
                        }
                        else if (count == 2)
                        {
                            float fval = System.Convert.ToSingle(s);
                            DETList.Add(fval);
                        }
                        else if (count == 3)
                        {
                            float fval = System.Convert.ToSingle(s);
                            RateList.Add(fval);
                        }
                        else if (count == 4)
                        {
                            LabList.Add(s);
                        }
                        else if (count == 5)
                        {
                            float fval = System.Convert.ToSingle(s);
                            COHList.Add(fval);
                            count = -1;
                        }
                        count++;
                    }
                }
                UpdateGraphData();
            }
        }

        private void UpdateGraphData()
        {

            var TimeStampSource = new EnumerableDataSource<float>(TSList);
            var EDRDataSource = new EnumerableDataSource<float>(EDRList);
            
            TimeStampSource.SetXMapping(x => x);
            EDRDataSource.SetYMapping(y => y);
            CompositeDataSource TimeDataSource = new CompositeDataSource(TimeStampSource, EDRDataSource);
            Plotter.AddLineGraph(TimeDataSource, new System.Windows.Media.Pen(System.Windows.Media.Brushes.Green, 2), new TrianglePointMarker { Size = 10.0, Pen = new System.Windows.Media.Pen(System.Windows.Media.Brushes.Black, 2.0), Fill = System.Windows.Media.Brushes.GreenYellow },
            new Microsoft.Research.DynamicDataDisplay.PenDescription("GSR data"));
            Plotter.Viewport.FitToView();
            Plotter.Children.Add(mouseTrack);
            mouseTrack.ShowHorizontalLine = false;
            mouseTrack.ShowVerticalLine = false;
        }



        private void MakeNoteButton_Click(object sender, RoutedEventArgs e)
        {
            var MakeNote = new NoteWindow();
            MakeNote.ShowDialog();
            if ((bool)MakeNote.DialogResult)
            {
                NoteString = MakeNote.NoteData;
                NoteList.Add(NoteString);
            }
        }

        private void SaveMenu_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "Note";
            dlg.DefaultExt = ".xml";
            dlg.Filter = "XML files (.xml)|*.xml";
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                OutputNotePath = dlg.FileName;
            }
            using (XmlWriter writer = XmlWriter.Create(OutputNotePath))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Notes");
                foreach (String s in NoteList)
                {
                    writer.WriteStartElement("Note");
                    writer.WriteElementString("Content", s);
                    writer.WriteEndElement();

                }
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }



        private void Chart_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            var transform = Plotter.Viewport.Transform;
            System.Windows.Point mousePos = mouseTrack.Position;
            var mouseScreenPosition = Mouse.GetPosition(Plotter.CentralGrid);
            var mousePositionInData = mouseScreenPosition.ScreenToData(transform);
            double xValue = mousePositionInData.X;
            System.Diagnostics.Debug.WriteLine(mousePositionInData);
        }



        private void DrawButton_Click(object sender, RoutedEventArgs e)
        {

            /*
            button1Form.FormBorderStyle = FormBorderStyle.None;
            button1Form.StartPosition = new FormStartPosition();
            button1Form.Width = 60;
            button1Form.Height = 25;    
            button1Form.BackColor = System.Drawing.Color.Red;
            button1Form.TransparencyKey = button1Form.BackColor;
            System.Drawing.Pen myPen;
            myPen = new System.Drawing.Pen(System.Drawing.Color.Red);
            System.Drawing.Graphics formGraphics = button1Form.CreateGraphics();
            formGraphics.DrawRectangle(myPen, 0, 0, 60, 20);
            myPen.Dispose();
            formGraphics.Dispose();
            button1Form.ShowDialog();
             * */
            // get desktop window
            /*
            IntPtr desktopHwnd = GetDesktopWindow();
            // Create a new pen.
            Pen skyBluePen = new Pen(System.Drawing.Brushes.DeepSkyBlue);
            // Set the pen's width.
            skyBluePen.Width = 8.0F;
            // Set the LineJoin property.
            skyBluePen.LineJoin = System.Drawing.Drawing2D.LineJoin.Bevel;
            Graphics.FromHwnd(desktopHwnd).DrawRectangle(skyBluePen, x, y, width, height);
            skyBluePen.Dispose();
             * */


        }

    }

}
