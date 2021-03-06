﻿
using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;


/**
 * Copyright (c) 2017 Matthias Zartmann
 * Email: Matthias.Zartmann@gmail.com
 * Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
 * The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */
namespace ZGCam
{
    public partial class MainForm : Form
    {

      
        ZAppData _AppData = new ZAppData();
        FrameRender _FrameRender;
        XmlGpsEntry _PreviewGpsEntry = null;

        public MainForm()
        {
            InitializeComponent();
        }


        private void UpdateGui()
        {
            if (_AppData.LogTextChanged)
            {
                TBLog.Text = String.Empty;
                TBLog.AppendText(_AppData.LogText);
            }
            TbOverlayFile.Text = _AppData.OverlayVideoFileName;
            TbGpsFile.Text = _AppData.XmlFileName;
            TbFilename.Text = _AppData.GoProFileName;

            BtnParse.Enabled = !String.IsNullOrEmpty(TbFilename.Text);


            CbMetric.Checked = _AppData.Units.MetricUnits;
            cbShowAlt.Checked = _AppData.ShowAlt ;
            CbShowGauge.Checked = _AppData.ShowGauge;
            CbShowMaxSpeed.Checked = _AppData.ShowMaxSpeed ;
            CbShowSpeed.Checked = _AppData.ShowSpeed ;

            BtnGenerateVideo.Enabled = _AppData.IsValidParsed;
            btnQueryLocation.Enabled = _AppData.IsValidParsed;

            if (_AppData.IsValidParsed)
            {
                pnlNoLock.Visible = !_AppData.Parser.HasGPS;
                LbTimeline.Visible = _AppData.Parser.HasGPS;
                TrackBarTimeLine.Visible = _AppData.Parser.HasGPS;
            }
            else
            {
                pnlNoLock.Visible = false;
                
                LbTimeline.Visible = true;
                TrackBarTimeLine.Visible = true;
            }



            if (! _AppData.IsValidParsed)
            {
                TrackBarTimeLine.Enabled = false;
                tb_pos.Text= "-- sec";
                tb_latitude.Text = "--,-- deg";
                tb_longitude.Text = "--,-- deg";
                tb_altitude.Text = "--,-- "+ _AppData.Units.AltUnit;
                tb_ground_speed.Text = "--,-- "+ _AppData.Units.SpeedUnit;
                tb_pdop.Text = "----";
                cb_2dlock.Checked = false;
                cb_3dlock.Checked = false;
                cb_nolock.Checked = false;


            }
            else
            {
                TrackBarTimeLine.Enabled = true;
                TrackBarTimeLine.Minimum = 0;
                TrackBarTimeLine.Maximum = _AppData.Parser.GpsCount() -1;
                _PreviewGpsEntry = _AppData.Parser.GpsEntry(TrackBarTimeLine.Value);
                tb_pos.Text = _PreviewGpsEntry.Time;
                tb_latitude.Text = string.Format("{0} deg", _PreviewGpsEntry.gpsLatitude);
                tb_longitude.Text =  string.Format("{0} deg", _PreviewGpsEntry.gpsLongitude);
                tb_altitude.Text = string.Format("{0}", _AppData.Units.AltString(_PreviewGpsEntry.gpsAltitude));
                tb_ground_speed.Text = string.Format("{0}", _AppData.Units.SpeedString(_PreviewGpsEntry.gpsGroundSpeed3D)); 
                tb_pdop.Text = string.Format("{0}", _PreviewGpsEntry.gpsPdop);
                cb_2dlock.Checked = false;
                cb_3dlock.Checked = false;
                cb_nolock.Checked = false;
                switch (_PreviewGpsEntry.gpsLock)
                {
                    case 0:
                        cb_nolock.Checked = true;
                        break;
                    case 2:
                        cb_2dlock.Checked = true;
                        break;
                    case 3:
                        cb_3dlock.Checked = true;
                        break;
                }


                pb_preview.Image =  _FrameRender.GenerateFrame( Brushes.White, _AppData, TrackBarTimeLine.Value);

            }


        }
     
        

        private void Form1_Load(object sender, EventArgs e)
        {
            _AppData.RestoreMainWindowSize(this);
            _AppData.LoadState();
            _FrameRender = new FrameRender(_AppData);
            this.Text = "ZGCam by Matthias Zartmann Version: " + _AppData.Version;
            UpdateGui();

        }

        private void Parse(bool pForce )
        {



            _AppData.ClearLog();
            _AppData.Parser.ForceReparse = pForce;

            if (!File.Exists(_AppData.GoProFileName))
            {
                MessageBox.Show("Mp4 Source-File not exists!", "File not exists", MessageBoxButtons.OK, MessageBoxIcon.Error);
              return;
            }


            BackgroundWorker bw = new BackgroundWorker();
            bw.WorkerSupportsCancellation = true;
            bw.WorkerReportsProgress = true;
            bw.DoWork += _AppData.Parser.DoWork;
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(
                                      delegate (object _Sender, RunWorkerCompletedEventArgs _E)
                                      {
                                          if (!_E.Cancelled)
                                          {
                                              UpdateGui();
                                          }

                                      });




            WaitForm.RunInBackground(this, bw, "Parse Mp4, please wait..");
        }

        private void BtnParse_Click(object sender, EventArgs e)
        {
            Parse(true);
        }


        private void trb_timeline_ValueChanged(object sender, EventArgs e)
        {
            UpdateGui();
        }
        private void VideoGen()
        {
            VideoGen videoGen = new VideoGen(_AppData);
            BackgroundWorker bw = new BackgroundWorker();
            bw.WorkerSupportsCancellation = true;
            bw.WorkerReportsProgress = true;
            bw.DoWork += videoGen.DoWork;
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(
                                  delegate (object _Sender, RunWorkerCompletedEventArgs _E)
                                  {

                                      UpdateGui();

                                  });

            WaitForm.RunInBackground(this, bw, "Generate Overlay-Video, please wait..");

        }

        private void btn_generate_video_Click(object sender, EventArgs e)
        {
            VideoGen();
        }
    

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            _AppData.KeepMainWindowSize(this);
            _AppData.StoreState();
        }

        private void BtnSelect_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            TbLocation.Text = "";

            if (!string.IsNullOrEmpty(_AppData.GoProFileName))
            {
                openFileDialog1.InitialDirectory = Path.GetDirectoryName(_AppData.GoProFileName);
            }
            openFileDialog1.Filter = "GoPro5 MP4 (*.mp4)|*.mp4|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                _AppData.GoProFileName = openFileDialog1.FileName;
                _PreviewGpsEntry = null;
                TrackBarTimeLine.Value = 0;
                Parse(false);
            }
        }

        private void Checkbox_Clicked(object sender, EventArgs e)
        {
           
            _AppData.Units.MetricUnits = CbMetric.Checked;
            _AppData.ShowAlt = cbShowAlt.Checked ;
            _AppData.ShowGauge = CbShowGauge.Checked;
            _AppData.ShowMaxSpeed = CbShowMaxSpeed.Checked;
            _AppData.ShowSpeed = CbShowSpeed.Checked;
            UpdateGui();
        }

        private void btnQueryLocation_Click(object sender, EventArgs e)
        {
            ZOpenStreetmap osm = new ZOpenStreetmap();
            if (osm.FindAddress(_PreviewGpsEntry.gpsLatitude, _PreviewGpsEntry.gpsLongitude))
            {
                TbLocation.Text = osm.BuildAdress();
            }
            else
            {
                MessageBox.Show("Error on Query Adress", "Query OpenStreetmap fail");
                if (osm.LastErrror != null) {
                    _AppData.Log(osm.LastErrror.ToString());
                 }
            }

            UpdateGui();
        }   

        private void TrackBarTimeLine_ValueChanged(object sender, EventArgs e)
        {
            UpdateGui();
        }

        

        private void TbLocation_TextChanged(object sender, EventArgs e)
        {
            _AppData.LocationText = TbLocation.Text;
            UpdateGui();
        }

       
        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Info.Show(_AppData);
        }

        private void LlbLatitude_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_PreviewGpsEntry != null)
            {
                string url = string.Format("www.openstreetmap.org?mlat={0}&mlon={1}&zoom=12&layers=M",
                                      _PreviewGpsEntry.gpsLatitude.ToString(CultureInfo.InvariantCulture)
                                      ,_PreviewGpsEntry.gpsLongitude.ToString(CultureInfo.InvariantCulture)
                                      );

                System.Diagnostics.Process.Start(url);
            }


        }

        private void LlGopro_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(Properties.Settings.Default.GoProHelpLink);

        }
    }
}
