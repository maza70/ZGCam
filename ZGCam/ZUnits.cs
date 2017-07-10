using System;
using System.Collections.Generic;
using System.Globalization;
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
    public class ZUnits
    {
        
        private bool _MetricUnits= true;
        
        public ZUnits(bool pUseMetric)
        {
            _MetricUnits = pUseMetric;
            
        }


        public string AltUnit
        {
            get
            {
                if (!_MetricUnits)
                    return "ft";
                else
                    return "m ";
            }
        }

        public string AltString(double pAlt_meter)
        {
            return string.Format("{0:0.0} {1}", Alt(pAlt_meter), AltUnit);
        }

        public double Alt(double pAlt_meter)
        {
            if (!_MetricUnits)
                return pAlt_meter * 3.28084;
            else
                return pAlt_meter;
        }



        public string SpeedUnit
        {
            get
            {
                if (!_MetricUnits)
                    return "mph";
                else
                    return "kmh";
            }
        }

        

        public double Speed(double pKmh)
        {
            if (!_MetricUnits)
                return pKmh * 0.621371;
            else
                return pKmh;
        }

        public string SpeedString(double pKmh)
        {
            return string.Format("{0:0.0} {1}", Speed(pKmh), SpeedUnit);
        }


        public bool MetricUnits
        {
            get => _MetricUnits; set => _MetricUnits= value;
        }
    }
}
