
using System;
using System.Collections.Generic;

/**
 * Copyright (c) 2017 Matthias Zartmann
 * Email: Matthias.Zartmann@gmail.com
 * Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
 * The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */


namespace ZGCam
{


    [Serializable()]
    [System.Xml.Serialization.XmlType("GOPRO5")]
    public class XmlGoPro
    {
        [System.Xml.Serialization.XmlElement("GPS")]
        public List<XmlGpsEntry> GpsEntries = new List<XmlGpsEntry>();
    }


    

    [Serializable()]
    public class XmlGpsEntry
    {

        [System.Xml.Serialization.XmlAttribute("n")]
        public int Number { get; set; }
        public string Time
        { get
            {
                return TimeSpan.FromSeconds((float)Number/18.0f).ToString();
            }

        }

        [System.Xml.Serialization.XmlAttribute("lk")]
        public int gpsLock { get; set; }

        public bool HasLock {
            get { return (gpsLock == 3 || gpsLock == 2); }
        }

        [System.Xml.Serialization.XmlAttribute("pdop")]
        public int gpsPdop { get; set; }
        [System.Xml.Serialization.XmlAttribute("lat")]
        public float gpsLatitude { get; set; }
        [System.Xml.Serialization.XmlAttribute("lon")]
        public float gpsLongitude { get; set; }
        [System.Xml.Serialization.XmlAttribute("alt")]
        public float gpsAltitude{ get; set; }
        [System.Xml.Serialization.XmlAttribute("gs")]
        public float gpsGroundSpeed{ get; set; }
        [System.Xml.Serialization.XmlAttribute("gs3d")]
        public float gpsGroundSpeed3D { get; set; }
    }

}
