using System;
using System.Collections.Generic;
using System.Drawing;
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
    public class Gauge
    {
        private int _TileWidth;
        private int _TileHeight;
        private int _TileMax;

        

        public Bitmap TileImage { get; set; }
        public int TileRows { get; set; }
        public int TileColumns { get; set; }

       

        public int X { get; set; }
        public int Y { get; set; }
        public int TileWidth { get { return _TileWidth;   } }
        public int TileHeight { get { return _TileHeight; } }

        private FrameRender _FrameRenderer;

        public Gauge(FrameRender pFrameRenderer, Bitmap pTile,int pTileRows,int pTileColumns)
        {
            _FrameRenderer = pFrameRenderer;
            TileRows = pTileRows;
            TileColumns = pTileColumns;
            TileImage = pTile;
            _TileWidth = TileImage.Width / pTileColumns;
            _TileHeight = TileImage.Height / pTileRows;
            _TileMax = pTileRows * pTileColumns;
          

        }

        public Rectangle getTileSourcePos(int pNumber)
        {
            if (pNumber > _TileMax)
            {
                pNumber = _TileMax;
            }
            if (pNumber < 0)
            {
                pNumber = 0;
            }
                Rectangle RSource = new Rectangle();
            int row = pNumber / TileColumns;
            int col = pNumber % TileColumns;
            RSource.Y = row * _TileHeight;
            RSource.X = col * _TileWidth;
            RSource.Width = _TileWidth;
            RSource.Height = _TileHeight;
            return RSource;
        }

        public void  DrawTile(Graphics pGraphic, int pNumber)
        {
            Rectangle targetRec = new Rectangle(0, 0, _TileWidth, _TileHeight);
            pGraphic.DrawImage(TileImage, targetRec, getTileSourcePos(pNumber),GraphicsUnit.Pixel);
        }

        public void DrawGauge(Graphics pGraphic, ZAppData pAppData, int pFrame)
        {
          

            XmlGpsEntry entry = pAppData.Parser.GpsEntry(pFrame);

            int tilenum  = (int)Math.Round((22.0f * entry.gpsGroundSpeed3D) / pAppData.Parser.MaxGroundSpeed, 0) ;
            DrawTile(pGraphic, tilenum);
          
        }

        



    }
}

