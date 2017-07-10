using AForge.Video.FFMPEG;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
/**
 *Copyright (c) 2017 Matthias Zartmann
 *Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
 *The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
 *THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */


namespace ZGCam
{
    public class VideoGen
    {
        private Brush _Background = Brushes.LimeGreen;
        private ZAppData _AppData;
    
       

        public VideoGen(ZAppData pAppData)
        {
            _AppData = pAppData;
            
        }

        public void DoWork(object sender, DoWorkEventArgs e)
        {  try
            {
                BackgroundWorker worker = sender as BackgroundWorker;

                FrameRender frameRederer = new FrameRender(_AppData);

                _AppData.Log("Write video -> {0} ", _AppData.OverlayVideoFileName);

                VideoFileWriter writer = new VideoFileWriter();
                writer.Open(_AppData.OverlayVideoFileName, frameRederer.Width, frameRederer.Height, 18, VideoCodec.WMV2, 1000000);


                int count = _AppData.Parser.GpsCount();
                int TotalSec = count / 18;
                for (int i = 0; i < count; i++)
                {

                    if ((worker.CancellationPending == true))
                    {
                        e.Cancel = true;
                        break;
                    }
                    Bitmap frame = frameRederer.GenerateFrame(_Background, _AppData, i);
                    writer.WriteVideoFrame(frame);
                    worker.ReportProgress((100 * (i + 1)) / count, string.Format("Write Video {0}/{1} sec Frame:{2}", i / 18, TotalSec, i));
                }
                writer.Close();

                if (e.Cancel == true)
                {
                    _AppData.Log("User cancel videogenerating.");
                    File.Delete(_AppData.OverlayVideoFileName);
                } else
                {
                    _AppData.Log("Video write successfully. ");
                }
            } catch (Exception Ex)
            {
                _AppData.Log(Ex.ToString());


            }

            
        }



   
        public Brush Background { get => _Background; set => _Background = value; }
        public ZAppData AppData { get => _AppData;  }
       


    }
}
