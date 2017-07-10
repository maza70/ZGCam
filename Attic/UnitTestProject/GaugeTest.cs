using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZGoPro;

namespace UnitTestProject
{
    [TestClass]
    public class GaugeTest
    {

        [TestMethod]
        public void TestTile()
        {
            Gauge g = new Gauge(ZGoPro.Properties.Resources.gauge,6,4);
            Rectangle r = g.getTileSourcePos(0);
            for (int i = 0; i < (6 * 4); i++)
            {
                r = g.getTileSourcePos(i);
             
            }


        }

    }
}
