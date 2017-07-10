using AForge.Video.FFMPEG;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml.Serialization;
/**
 *Copyright (c) 2017 Matthias Zartmann
 *Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
 *The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
 *THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */


namespace ZGCam
{
    public class MP4Parser
    {

        private float _MaxGroundSpeed = 0;
        private float _MinGroundSpeed = 0;
        private float _MaxAlt = 0;
        private float _MinAlt = 0;
        private double _VideoLen;
        private int _VideoWidth;
        private int _VideoHeight;
        private double _GpsPerSecond = 18.0;
        private Boolean _ForceReparse;
        private BackgroundWorker _Worker;
        private XmlGoPro _GpsData;
        private long _VideoFrameCount;
        private int _VideoFrameRate;
        private string _VideoCodecName;
        private bool _HasGpsLock;
        private ZAppData _AppData;

        public MP4Parser(ZAppData pAppData, bool pForceReparse)
        {
            _AppData = pAppData;

            _ForceReparse = pForceReparse;

        }
        public MP4Parser(ZAppData pAppData)
        {
            _AppData = pAppData;

            _ForceReparse = true;

        }


        public void DoWork(object sender, DoWorkEventArgs e)
        {
            _Worker = sender as BackgroundWorker;
            _Worker.WorkerSupportsCancellation = true;
            _Worker.WorkerReportsProgress = true;
            Run();
        }


        public void Run()
        {
            try
            {
                Clear();

                _AppData.Log("Parse gps data in {0}", _AppData.GoProFileName);
                ReportProgress("Parse Gps data in Video.");
                RunParser();

                if (IsCanceled())
                    return;

                _AppData.Log("Evaluation gps data.");
                ReportProgress("Read GPS data.");
                Deserialize(_AppData.XmlFileName);

                if (IsCanceled())
                    return;

                ReportProgress("Calculate min/max.");
                CalcMinMax();

                if (IsCanceled())
                    return;

                _AppData.Log("Evaluation source video Information.");
                ReportProgress("Evaluation video information.");
                ParseVideoInfo(_AppData.GoProFileName);

                if (IsCanceled())
                    return;

                ReportProgress("Align Video length.");
                AlignGpsToVideo();

                _AppData.Log("Parsing finish.");
            }
            catch (Exception e)
            {
                _AppData.Log("Errro: {0}", e.ToString());
            }

        }

        public void Clear()
        {
            _MaxGroundSpeed = 0;
            _MinGroundSpeed = 0;
            _MaxAlt = 0;
            _MinAlt = 0;
            _VideoLen = 0;
            _VideoWidth = 0;
            _VideoHeight = 0;
            _GpsData = null;
            _VideoFrameCount = 0;
            _VideoFrameRate = 0;
            _VideoCodecName = string.Empty;
            _HasGpsLock = false;

        }

        private bool IsCanceled()
        {
            if (_Worker.CancellationPending)
            {
                _AppData.Log("User cancel parsing!");
                return true;
            }
            return false;
        }

        private void ReportProgress(String pProgressText)
        {
            if (_Worker != null)
            {
                _Worker.ReportProgress(0, pProgressText);
            }
        }

        private void RunParser()
        {
            if (File.Exists(_AppData.XmlFileName) && _ForceReparse)
            {
                _AppData.Log("Xml File {0} alrady exists.", _AppData.XmlFileName);
                return; //Xml exists
            }

            using (Process p = new Process())
            {
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.FileName = "GPMF_Parser.exe";
                p.StartInfo.Arguments = String.Format("\"{0}\"", _AppData.GoProFileName);
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.CreateNoWindow = true;
                p.Start();
                using (var writer = new StreamWriter(_AppData.XmlFileName, append: false))
                {
                    writer.Write(p.StandardOutput.ReadToEnd());
                };

                p.WaitForExit();
            }
        }


        private void Deserialize(string pFilename)
        {


            
                XmlSerializer serializer = new XmlSerializer(typeof(XmlGoPro));
                using (StreamReader reader = new StreamReader(pFilename))
                {
                    _GpsData = (XmlGoPro)serializer.Deserialize(reader);
                }

                _HasGpsLock = _GpsData.GpsEntries.Find(x => (x.HasLock == true)) != null;

                if (_HasGpsLock == false)
                {
                    _AppData.Log("The Mp4 has no Gps data included, please ensure that Gps is enabled in the GoPro!!");
                    _AppData.Log("More info under: https://de.gopro.com/telemetry?utm_source=qda&utm_medium=app&utm_content=gauges_modal");
                }
          

        }

        private void ParseVideoInfo(String pMp4VideoFile)
        {
            VideoFileReader reader = new VideoFileReader();
            // open video file
            reader.Open(pMp4VideoFile);
            try
            {

                // check some of its attributes
                _VideoWidth = reader.Width;
                _VideoHeight = reader.Height;
                _VideoLen = (double)reader.FrameCount / (double)reader.FrameRate;
                _VideoFrameCount = reader.FrameCount;
                _VideoFrameRate = reader.FrameRate;
                _VideoCodecName = reader.CodecName;

                _AppData.Log("Source Width:  {0} pixel", _VideoWidth);
                _AppData.Log("Source Height: {0} pixel", _VideoHeight);
                _AppData.Log("Source Length: {0} ", TimeSpan.FromSeconds(_VideoLen).ToString());
                _AppData.Log("Source Frames: {0} ", _VideoFrameCount);
                _AppData.Log("Source FPS:    {0} ", _VideoFrameRate);
                _AppData.Log("Source Codec:  {0} ", _VideoCodecName);


            }
            finally
            {
                reader.Close();
            }

        }

        private void CalcMinMax()
        {


            _MinAlt = _GpsData.GpsEntries.Min(GpsEntry => GpsEntry.gpsAltitude);
            _MaxAlt = _GpsData.GpsEntries.Max(GpsEntry => GpsEntry.gpsAltitude);
            _MinGroundSpeed = _GpsData.GpsEntries.Min(GpsEntry => GpsEntry.gpsGroundSpeed3D);
            _MaxGroundSpeed = _GpsData.GpsEntries.Max(GpsEntry => GpsEntry.gpsGroundSpeed3D);

        }

        private void AlignGpsToVideo()
        {

            double gpsDateDuration = (double)_GpsData.GpsEntries.Count / _GpsPerSecond;
            XmlGpsEntry entry = _GpsData.GpsEntries.Last();

            TimeSpan ts = TimeSpan.FromSeconds(_VideoLen - ((double)_GpsData.GpsEntries.Count / _GpsPerSecond));
            _AppData.Log("Align Gps data length about {0} .", ts.ToString());

            if (((double)_GpsData.GpsEntries.Count / _GpsPerSecond) < _VideoLen)
            {





                //neue Einträge hinzufügen bis die länge stimmt
                while (((double)_GpsData.GpsEntries.Count / _GpsPerSecond) < _VideoLen)
                {
                    _GpsData.GpsEntries.Add(entry); //Letzter eintrag so oft verdoppeln bis wir rankommen
                }
            }
            else
            {
                //Kommt eigentlich nicht vor aber wir haben anscheinend mehr
                while (((double)_GpsData.GpsEntries.Count / _GpsPerSecond) > _VideoLen)
                {
                    _GpsData.GpsEntries.RemoveAt(_GpsData.GpsEntries.Count - 1);
                }

            }
            _AppData.Log("New Gps length => {0} .", TimeSpan.FromSeconds((double)_GpsData.GpsEntries.Count / _GpsPerSecond));


        }


        public float MaxGroundSpeed { get => _MaxGroundSpeed; }
        public float MinGroundSpeed { get => _MinGroundSpeed; }
        public float MaxAlt { get => _MaxAlt; }
        public float MinAlt { get => _MinAlt; }
        public double VideoLen { get => _VideoLen; }
        public int VideoWidth { get => _VideoWidth; }
        public int VideoHeight { get => _VideoHeight; }
        public string VideoCodecName { get => _VideoCodecName; }
        public long VideoFrameCount { get => _VideoFrameCount; }
        public int VideoFrameRate { get => _VideoFrameRate; }
        public XmlGoPro GpsData { get => _GpsData; }



        public bool ForceReparse { get => _ForceReparse; set => _ForceReparse = value; }
        public bool HasData
        {
            get => (_GpsData != null && _GpsData.GpsEntries.Count > 0);
        }

        public bool HasGPS
        {
            get => (_HasGpsLock);
        }

        public XmlGpsEntry GpsEntry(int pIndex)
        {
            return _GpsData.GpsEntries[pIndex];

        }
        public int GpsCount()
        {
            return _GpsData.GpsEntries.Count;

        }


    }
}
