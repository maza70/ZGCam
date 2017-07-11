using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
/**
 *Copyright (c) 2017 Matthias Zartmann
 *Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
 *The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
 *THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 * 
 */


namespace ZGCam
{
    public class FrameRender
    {
        [DllImport("gdi32.dll", ExactSpelling = true)]
        private static extern IntPtr AddFontMemResourceEx(byte[] pbFont, int cbFont, IntPtr pdv, out uint pcFonts);
        [DllImport("gdi32.dll", ExactSpelling = true)]
        internal static extern bool RemoveFontMemResourceEx(IntPtr fh);

        static private IntPtr _fh = IntPtr.Zero;
        static private PrivateFontCollection _pfc = null;

        private Rectangle _VideoSize = new Rectangle(0, 0, 960, 540);
        private Gauge _Gauge;
        //private static PrivateFontCollection _pfc;
        private Bitmap _Frame = null;
        private ZAppData _AppData;

        private Font _SpeedFont;
        private Font _UnitFont;
        private Font _MaxSpeedFont;


        public FrameRender(ZAppData pAppData)
        {

            _AppData = pAppData;
            _Gauge = new Gauge(this, ZGCam.Properties.Resources.gauge, 6, 4);
            _Frame = new Bitmap(_VideoSize.Width, _VideoSize.Height);
            _SpeedFont = GetFont(50);
            _UnitFont = GetFont(15);
            _MaxSpeedFont = GetFont(12);
        }


     


        public Bitmap GenerateFrame(Brush pBackground, ZAppData pAppData, int pFrame)
        {
      
            Graphics graph = Graphics.FromImage(_Frame);

            graph.FillRectangle(pBackground, _VideoSize);
            graph.Save();
            graph.TranslateTransform(0, _VideoSize.Height - _Gauge.TileHeight);
            if (pAppData.ShowGauge)
            {

                _Gauge.DrawGauge(graph, pAppData, pFrame);
            }

            RectangleF textRect, textRect1;
            XmlGpsEntry entry = pAppData.Parser.GpsEntry(pFrame);

            if (pAppData.ShowSpeed && pAppData.Parser.HasGPS)
            {
                string speedtext = String.Format("{0:0.0}", pAppData.Units.Speed(entry.gpsGroundSpeed3D));
                if (!entry.HasLock)
                {

                    speedtext = String.Format("{0:0.0}", pAppData.Units.Speed(0));
                }
        
                textRect = MasureDrawOutlineText(graph, speedtext, _SpeedFont, 2);
                textRect1 = MasureDrawOutlineText(graph, pAppData.Units.SpeedUnit, _MaxSpeedFont, 2);

                DrawOutlineText(graph, speedtext, _SpeedFont, Brushes.White, Color.Black, 2,
                                                300 - ((int)(textRect.Width + textRect1.Width) + 20), 33);
                DrawOutlineText(graph, pAppData.Units.SpeedUnit, _MaxSpeedFont, Brushes.White, Color.Black, 2,
                                          300 - (int)textRect1.Width,
                                          90);
            }

            if (pAppData.ShowSpeed &&  !pAppData.Parser.HasGPS)
            {
                string speedtext = "No GPS";
                textRect = MasureDrawOutlineText(graph, speedtext, _SpeedFont, 2);
                DrawOutlineText(graph, speedtext, _SpeedFont, Brushes.White, Color.Black, 2,
                                                350 - ((int)(textRect.Width) + 20), 40);
            }

            if (pAppData.ShowMaxSpeed && pAppData.Parser.HasGPS)
            {
                //--Max Speed
                string maxSpeedText = string.Format("maximum speed: {0}", pAppData.Units.SpeedString(pAppData.Parser.MaxGroundSpeed));
                textRect = MasureDrawOutlineText(graph, maxSpeedText, _MaxSpeedFont, 2);
                DrawOutlineText(graph, maxSpeedText, _MaxSpeedFont, Brushes.White, Color.Black, 2, 300 - (int)textRect.Width, 123);
            }

            if (pAppData.ShowAlt && pAppData.Parser.HasGPS)
            {
                string maxAltText = string.Format("alt: {0}", pAppData.Units.AltString(entry.gpsAltitude));
                if (!entry.HasLock)
                {

                    maxAltText = String.Format("alt: {0}", pAppData.Units.AltString(0));
                }

                //--Altitude
              
                textRect = MasureDrawOutlineText(graph, maxAltText, _MaxSpeedFont, 2);
                DrawOutlineText(graph, maxAltText, _MaxSpeedFont, Brushes.White, Color.Black, 2, 300 - (int)textRect.Width, 30);
            }

            //--Location text
            if (!string.IsNullOrEmpty(pAppData.LocationText))
            {
                DrawOutlineText(graph, pAppData.LocationText, _MaxSpeedFont, Brushes.White, Color.Black, 2, 320, 123);
            }





            graph.Save();
            


            //result.Save(@"s:\source\c#\ZGoPro\images\test.png", ImageFormat.Png);
            return _Frame;
        }


        public Font GetFont(float pSize)
        {
            //"Arial Black";"Impact"
            return new Font("Arial Black", pSize,FontStyle.Bold);
           
        }

        public Font GetSpecialFont(float size)
        {

            Font fnt = null;

            if (null == _pfc)
            {


                byte[] ttf_font = ZGCam.Properties.Resources.Transformers_Movie_TTF;

                uint cFonts;
                AddFontMemResourceEx(ttf_font, ttf_font.Length, IntPtr.Zero, out cFonts);


                IntPtr pbyt = Marshal.AllocCoTaskMem(ttf_font.Length);
                if (null != pbyt)
                {
                    Marshal.Copy(ttf_font, 0, pbyt, ttf_font.Length);
                    _pfc = new PrivateFontCollection();
                    _pfc.AddMemoryFont(pbyt, ttf_font.Length);
                    Marshal.FreeCoTaskMem(pbyt);
                }

            }

            if (_pfc.Families.Length > 0)
            {

                fnt = new Font(_pfc.Families[0], size);
            }

            return fnt;
        }


        /*
        public Font GetFont(int pEmSize, FontStyle pStyle)
        {
            if (_pfc == null)
            {
                _pfc = new PrivateFontCollection();


                byte[] ttf_font = ZGCam.Properties.Resources.Transformers_Movie_TTF;
                System.IntPtr data = Marshal.AllocCoTaskMem((int)ttf_font.Length);
                // copy the bytes to the unsafe memory block
                Marshal.Copy(ttf_font, 0, data, (int)ttf_font.Length);
                _pfc.AddMemoryFont(data, (int)ttf_font.Length);


            }
            var fontFamily= _pfc.Families[0];
            if (fontFamily.IsStyleAvailable(FontStyle.Regular))
            {
                 //jow
            }
            if (fontFamily.IsStyleAvailable(FontStyle.Bold))
            {
                //jow
            }

            return new Font(_pfc.Families[0], pEmSize, pStyle);
        }
        */

        public RectangleF MasureDrawOutlineText(Graphics pGraphic, string Text, Font pFont, int pOutlineWidth)
        {
            using (GraphicsPath gp = new GraphicsPath())
            {
                using (StringFormat sf = new StringFormat())
                {
                    using (Pen outline = new Pen(Color.Black, pOutlineWidth))
                    {
                        outline.LineJoin = LineJoin.Round;
                        gp.AddString(Text, pFont.FontFamily, (int)pFont.Style, pFont.Size * 1.4f, new Point(0,0), sf);
                        return gp.GetBounds();
                    }
                }
            }
         }

        public RectangleF  DrawOutlineText(Graphics pGraphic, string Text, Font pFont,Brush pBrush, Color pOutlineColor,int pOutlineWidth, int x, int y)
        {
            using (GraphicsPath gp = new GraphicsPath())
            {
                using (StringFormat sf = new StringFormat())
                {
                    using (Pen outline = new Pen(pOutlineColor, pOutlineWidth))
                    {
                        outline.LineJoin = LineJoin.Round;

                        gp.AddString(Text, pFont.FontFamily, (int)pFont.Style, pFont.Size *1.4f, new Point(x,y ), sf);
                        pGraphic.SmoothingMode = SmoothingMode.HighQuality;
                        pGraphic.DrawPath(outline, gp);
                        pGraphic.FillPath(pBrush, gp);
                        return gp.GetBounds();
                    }
                }
            }

        }

        public int Width{
            get { return _VideoSize.Width; }
            }
        public int Height
        {
            get { return _VideoSize.Height; }
        }
       
    }
}
