using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
/**
 *Copyright (c) 2017 Matthias Zartmann
 *Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
 *The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
 *THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */


namespace ZGCam
{
    public partial class WaitForm : Form
    {
        private BackgroundWorker _BwWorker;

        public WaitForm()
        {
            InitializeComponent();
        }

        public static void RunInBackground(Form pOwner,BackgroundWorker pBwWorker, string _Titel)
        {
            WaitForm wForm = new WaitForm();
            wForm.Run(pOwner,pBwWorker, _Titel);


        }

        public void Run(Form pOwner,BackgroundWorker pBwWorker, string _Titel)
        {
            this.Text = _Titel;
            //Method 2. The manual way
            this.StartPosition = FormStartPosition.Manual;
            this.Top = ((pOwner.Bounds.Height - this.Height) / 2) + pOwner.Top;
            this.Left = ((pOwner.Bounds.Width - this.Width) / 2) + pOwner.Left;

            pOwner.BeginInvoke(new Action(() => this.ShowDialog()));

            
            



            PGBar.Maximum = 100;
            _BwWorker = pBwWorker;
            _BwWorker.RunWorkerCompleted += RunWorkerCompleted;
            
            _BwWorker.ProgressChanged += _BwWorker_ProgressChanged;

            if (_BwWorker.IsBusy != true)
            {
                _BwWorker.RunWorkerAsync();
            }

            BtnCancel.Enabled = _BwWorker.WorkerSupportsCancellation;
        }

        private void _BwWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState != null)
            {
                tb_info.Text = (string)e.UserState;
            }
            if (e.ProgressPercentage == 0)
            {
                PGBar.Style = ProgressBarStyle.Marquee;
                PGBar.Value = 100;
            }
            else
            {
                PGBar.Style = ProgressBarStyle.Continuous;
                PGBar.Value = e.ProgressPercentage;
            }

        }

        private void RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

           
            this.Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            _BwWorker.CancelAsync();
        }

        private void WaitForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                if (_BwWorker.WorkerSupportsCancellation && !_BwWorker.CancellationPending)
                {
                    _BwWorker.CancelAsync();
                    
                }
               
            }
        }
    }
}
