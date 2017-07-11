using System;
using System.Diagnostics;
using System.IO;
using System.Text;
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
    public class ZAppData
    {
        ZUnits _Units;
        MP4Parser _parser;
        string _GoProFileName;
        string _XmlFileName;
        string _VideoFileName;
        StringBuilder _LogBuffer;
        bool _LogBufferChanged;
        string _LocationText;
        bool _ShowGauge = true;
        bool _ShowAlt = true;
        bool _ShowMaxSpeed = true;
        bool _ShowSpeed = true;

        string _Version;





        public ZAppData()
        {

            _Units = new ZUnits(true);
            _parser = new MP4Parser(this);
            _LogBuffer = new StringBuilder();

            Log("please select a GoPro Mp4 File and press parse.");

            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            _Version = fvi.FileVersion +" Beta";

        }

        public ZUnits Units { get => _Units; }
        public MP4Parser Parser { get => _parser; }
        public String GoProFileName
        {
            get { return _GoProFileName; }
            set
            {
                if (File.Exists(value))
                {
                    _GoProFileName = value;
                    string file = Path.GetFileNameWithoutExtension(_GoProFileName);

                    string path = Path.GetDirectoryName(_GoProFileName);
                    _XmlFileName = Path.Combine(path, file + "_ZGCam.xml");
                    _VideoFileName = Path.Combine(path, file + "_ZGCam.avi");
                    _parser.Clear();
                }

            }
        }
        public String XmlFileName { get => _XmlFileName; }
        public String OverlayVideoFileName { get => _VideoFileName; }
        public bool IsValidParsed
        {
            get
            {
                bool valid = _GoProFileName != null;
                valid = valid && _parser != null &&_parser.HasData;
                return valid;
            }
        }

        

        public void KeepMainWindowSize(Form pMainWindow)
        {

            // Copy window size to app settings
            if (pMainWindow.WindowState == FormWindowState.Normal)
            {
                Properties.Settings.Default.MainSize = pMainWindow.Size;
                Properties.Settings.Default.MainLocation = pMainWindow.Location;
            }
            else
            {
                Properties.Settings.Default.MainSize = pMainWindow.RestoreBounds.Size;
                Properties.Settings.Default.MainLocation = pMainWindow.RestoreBounds.Location;
            }




        }
        public void RestoreMainWindowSize(Form pMainWindow)
        {


            if (Properties.Settings.Default.MainLocation != null && !Properties.Settings.Default.MainLocation.IsEmpty)
            {
                pMainWindow.Location = Properties.Settings.Default.MainLocation;
            }

            // Set window size
            if (Properties.Settings.Default.MainSize != null && !Properties.Settings.Default.MainSize.IsEmpty)
            {
                pMainWindow.Size = Properties.Settings.Default.MainSize;
            }

        }

        public void LoadState()
        {
            _Units.MetricUnits = Properties.Settings.Default.Metric;
            _ShowAlt = Properties.Settings.Default.ShowAltitude;
            _ShowGauge = Properties.Settings.Default.ShowGauge;
            _ShowMaxSpeed = Properties.Settings.Default.ShowMaxSpeed;
            _ShowSpeed = Properties.Settings.Default.ShowSpeed;


            GoProFileName = Properties.Settings.Default.LastFile;


        }


        public void StoreState()
        {
            Properties.Settings.Default.Metric = _Units.MetricUnits;
            Properties.Settings.Default.LastFile = GoProFileName;
            Properties.Settings.Default.ShowAltitude = _ShowAlt;
            Properties.Settings.Default.ShowGauge = _ShowGauge ;
            Properties.Settings.Default.ShowMaxSpeed = _ShowMaxSpeed;
            Properties.Settings.Default.ShowSpeed = _ShowSpeed;
            Properties.Settings.Default.Save();

        }


        public void ClearLog()
        {
            _LogBuffer.Length = 0;
        }
        public void Log(String pText)
        {
            _LogBuffer.Append(DateTime.Now.ToString("T"));
            _LogBuffer.Append(": ");
            _LogBuffer.AppendLine(pText);
            _LogBufferChanged = true;
        }
        public void Log(String pFormat, params object[] pParams)
        {
            Log(string.Format(pFormat, pParams));
        }

        public bool LogTextChanged
        {
            get { return _LogBufferChanged; }
        }
        public string LogText
        {
            get
            {
               
                _LogBufferChanged = false;
                return _LogBuffer.ToString();
            }
         }

        public string LocationText
        {
            get
            {
                return _LocationText;
            }

            set
            {
                _LocationText = value;
            }
        }


        public bool ShowAlt
        {
            get => _ShowAlt;
            set => _ShowAlt = value;
        }
        public bool ShowGauge
        {
            get => _ShowGauge;
            set => _ShowGauge = value;
        }
        public bool ShowSpeed
        {
            get => _ShowSpeed;
            set => _ShowSpeed = value;
        }

        public bool ShowMaxSpeed
        {
            get => _ShowMaxSpeed;
            set => _ShowMaxSpeed = value;
        }
        public string Version
        {
            get => _Version;
        }
    }
}
