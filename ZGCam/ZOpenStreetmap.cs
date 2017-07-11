using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.XPath;

/**
 * Copyright (c) 2017 Matthias Zartmann
 * Email: Matthias.Zartmann@gmail.com
 * Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
 * The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */



namespace ZGCam
{
    public class ZOpenStreetmap
    {
        string _Street;
        string _country;
        string _state;
        string _town;
        string _postcode;
        static readonly Random _SRANDOM = new System.Random();
        static string SUSERAGENT = string.Format("Mozilla/5.0 (Windows NT {1}.0; {2}rv:{0}.0) Gecko/20100101 Firefox/{0}.0",
         _SRANDOM.Next(DateTime.Today.Year - 1969 - 5, DateTime.Today.Year - 1969),
         _SRANDOM.Next(0, 10) % 2 == 0 ? 10 : 6,
         _SRANDOM.Next(0, 10) % 2 == 1 ? string.Empty : "WOW64; ");

        public static string _REFERERURL = "http://zgeo.ma-za.de/";
        private Exception _LastError;

        public string ReadAdressPart(XPathNavigator pNav, params string[] pChoices)
        {

            foreach (string choice in pChoices)
            {
                string xPath = String.Format("/reversegeocode/addressparts/{0}/text()", choice);
                XPathNavigator element = pNav.SelectSingleNode(xPath);
                if (element != null && !string.IsNullOrEmpty(element.Value))
                {
                    return element.Value;
                }
            }


            return string.Empty;
        }


        public string BuildAdress()
        {

            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrEmpty(_Street))
                sb.Append(_Street).Append(", ");
            if (!string.IsNullOrEmpty(_postcode))
                sb.Append(_postcode).Append(" ");
            if (!string.IsNullOrEmpty(_town))
                sb.Append(_town).Append(", ");
            if (!string.IsNullOrEmpty(_country))
                sb.Append(_country).Append(" ");

            return sb.ToString();


        }

        public bool FindAddress(double lat, double lng)
        {
            _LastError = null;
            try
            {
                string url = string.Format("http://nominatim.openstreetmap.org/reverse?format=xml&lat={0}&lon={1}&zoom=18&addressdetails=1", lat.ToString(CultureInfo.InvariantCulture), lng.ToString(CultureInfo.InvariantCulture));
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Referer = _REFERERURL;
                request.UserAgent = SUSERAGENT;
                request.AutomaticDecompression = DecompressionMethods.GZip;
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {

                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(reader.ReadToEnd());
                    var nav = doc.CreateNavigator();
                    _Street = ReadAdressPart(nav, "path", "road");
                    _postcode = ReadAdressPart(nav, "postcode");
                    _town = ReadAdressPart(nav, "village", "town", "city");
                    _state = ReadAdressPart(nav, "state");
                    _country = ReadAdressPart(nav, "country");
                }
            }
            catch (Exception Ex)
            {
                _LastError = Ex;
                return false;

            }
            return true;
        }


        public Exception LastErrror
        {
            get { return _LastError; }
        }
    }
}
