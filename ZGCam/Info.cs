using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
    public partial class Info : Form
    {
        ZAppData _Appdata;

        public Info()
        {
            InitializeComponent();
        }

        public static void Show(ZAppData pAppdata)
        {
            Info info = new Info();
            info._Appdata = pAppdata;
            info.CenterToScreen();
            info.ShowDialog();

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }



        private void Info_Load(object sender, EventArgs e)
        {

            LbVersion.Text = _Appdata.Version;



            StringBuilder sb = new StringBuilder();

            sb.AppendLine("ZGCam is under mit-License, see: https://tldrlegal.com/license/mit-license ");
            sb.AppendLine("ZGCam Homepage: https://sites.google.com/view/zgcam");
            sb.AppendLine("ZGCam uses the follow components and libraries:");
            sb.AppendLine("");
            sb.AppendLine("• GoPro Gpmf Parser (Apache License 2.0)");
            sb.AppendLine("  https://github.com/gopro/gpmf-parser");
            sb.AppendLine("  GoPro are registered tradmark of GoPro Inc.");
            sb.AppendLine("");
            sb.AppendLine("• AForge.NET (LGPL v3 license +  GPL v3 license)");
            sb.AppendLine("  http://www.aforgenet.com");
            sb.AppendLine("");
            sb.AppendLine("");
            sb.AppendLine("Copyright(c) 2017 Matthias Zartmann");
            sb.AppendLine("Email : mailto:Matthias.Zartmann@gmail.com");
            sb.Append("person obtaining a copy of this software and associated documentation files(the \"Software\"), ");
            sb.Append("to deal in the Software without restriction, including without limitation the rights ");
            sb.Append("to use, copy, modify, merge, publish, distribute, sublicense, and/ or sell copies ");
            sb.Append("of the Software, and to permit persons to whom the Software is furnished to do so, ");
            sb.AppendLine("subject to the following conditions: ");
            sb.Append("Permission is hereby granted, free of charge, to any ");
            sb.Append("The above copyright notice and this permission notice shall be included in all copies ");
            sb.AppendLine("or substantial portions of the Software.");
            sb.Append("THE SOFTWARE IS PROVIDED \"AS IS\", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, ");
            sb.Append("INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. ");
            sb.Append("IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, ");
            sb.Append("WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE ");
            sb.Append("SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.");



            RtbInfo.Text = sb.ToString();
            LinkLabel link = new LinkLabel();
            link.Text = "Mail";












        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void RtbInfo_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.LinkText);
            
        }
    }
}
