namespace ZGCam
{
    partial class MainForm
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.TBLog = new System.Windows.Forms.TextBox();
            this.PanelMain = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.LbTimeline = new System.Windows.Forms.Label();
            this.LbNoGps = new System.Windows.Forms.Label();
            this.TrackBarTimeLine = new System.Windows.Forms.TrackBar();
            this.CbShowSpeed = new System.Windows.Forms.CheckBox();
            this.pb_preview = new System.Windows.Forms.PictureBox();
            this.cbShowAlt = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.CbShowMaxSpeed = new System.Windows.Forms.CheckBox();
            this.CbShowGauge = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.CbMetric = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnQueryLocation = new System.Windows.Forms.Button();
            this.tb_latitude = new System.Windows.Forms.TextBox();
            this.TbLocation = new System.Windows.Forms.TextBox();
            this.tb_longitude = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tb_altitude = new System.Windows.Forms.TextBox();
            this.tb_pdop = new System.Windows.Forms.TextBox();
            this.tb_ground_speed = new System.Windows.Forms.TextBox();
            this.cb_3dlock = new System.Windows.Forms.CheckBox();
            this.tb_pos = new System.Windows.Forms.TextBox();
            this.cb_2dlock = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cb_nolock = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.TbOverlayFile = new System.Windows.Forms.TextBox();
            this.TbGpsFile = new System.Windows.Forms.TextBox();
            this.BtnParse = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.TbFilename = new System.Windows.Forms.TextBox();
            this.BtnSelect = new System.Windows.Forms.Button();
            this.BtnGenerateVideo = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.infoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.LlbLatitude = new System.Windows.Forms.LinkLabel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.PanelMain.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBarTimeLine)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_preview)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // TBLog
            // 
            this.TBLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TBLog.Location = new System.Drawing.Point(0, 0);
            this.TBLog.Multiline = true;
            this.TBLog.Name = "TBLog";
            this.TBLog.ReadOnly = true;
            this.TBLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TBLog.Size = new System.Drawing.Size(634, 131);
            this.TBLog.TabIndex = 30;
            this.TBLog.WordWrap = false;
            // 
            // PanelMain
            // 
            this.PanelMain.Controls.Add(this.panel1);
            this.PanelMain.Controls.Add(this.label11);
            this.PanelMain.Controls.Add(this.label9);
            this.PanelMain.Controls.Add(this.TbOverlayFile);
            this.PanelMain.Controls.Add(this.TbGpsFile);
            this.PanelMain.Controls.Add(this.BtnParse);
            this.PanelMain.Controls.Add(this.label1);
            this.PanelMain.Controls.Add(this.TbFilename);
            this.PanelMain.Controls.Add(this.BtnSelect);
            this.PanelMain.Controls.Add(this.BtnGenerateVideo);
            this.PanelMain.Controls.Add(this.menuStrip1);
            this.PanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelMain.Location = new System.Drawing.Point(0, 0);
            this.PanelMain.Name = "PanelMain";
            this.PanelMain.Size = new System.Drawing.Size(634, 470);
            this.PanelMain.TabIndex = 31;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.LlbLatitude);
            this.panel1.Controls.Add(this.LbTimeline);
            this.panel1.Controls.Add(this.LbNoGps);
            this.panel1.Controls.Add(this.TrackBarTimeLine);
            this.panel1.Controls.Add(this.CbShowSpeed);
            this.panel1.Controls.Add(this.pb_preview);
            this.panel1.Controls.Add(this.cbShowAlt);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.CbShowMaxSpeed);
            this.panel1.Controls.Add(this.CbShowGauge);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.CbMetric);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.btnQueryLocation);
            this.panel1.Controls.Add(this.tb_latitude);
            this.panel1.Controls.Add(this.TbLocation);
            this.panel1.Controls.Add(this.tb_longitude);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.tb_altitude);
            this.panel1.Controls.Add(this.tb_pdop);
            this.panel1.Controls.Add(this.tb_ground_speed);
            this.panel1.Controls.Add(this.cb_3dlock);
            this.panel1.Controls.Add(this.tb_pos);
            this.panel1.Controls.Add(this.cb_2dlock);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.cb_nolock);
            this.panel1.Location = new System.Drawing.Point(3, 150);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(627, 317);
            this.panel1.TabIndex = 40;
            // 
            // LbTimeline
            // 
            this.LbTimeline.AutoSize = true;
            this.LbTimeline.Location = new System.Drawing.Point(5, 0);
            this.LbTimeline.Name = "LbTimeline";
            this.LbTimeline.Size = new System.Drawing.Size(53, 13);
            this.LbTimeline.TabIndex = 30;
            this.LbTimeline.Text = "Time-Line";
            // 
            // LbNoGps
            // 
            this.LbNoGps.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LbNoGps.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LbNoGps.ForeColor = System.Drawing.Color.Red;
            this.LbNoGps.Location = new System.Drawing.Point(100, 16);
            this.LbNoGps.Name = "LbNoGps";
            this.LbNoGps.Size = new System.Drawing.Size(414, 33);
            this.LbNoGps.TabIndex = 55;
            this.LbNoGps.Text = "No Gps informations in Video";
            this.LbNoGps.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TrackBarTimeLine
            // 
            this.TrackBarTimeLine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TrackBarTimeLine.BackColor = System.Drawing.SystemColors.Control;
            this.TrackBarTimeLine.LargeChange = 18;
            this.TrackBarTimeLine.Location = new System.Drawing.Point(7, 16);
            this.TrackBarTimeLine.Maximum = 500;
            this.TrackBarTimeLine.Name = "TrackBarTimeLine";
            this.TrackBarTimeLine.Size = new System.Drawing.Size(612, 45);
            this.TrackBarTimeLine.SmallChange = 18;
            this.TrackBarTimeLine.TabIndex = 29;
            this.TrackBarTimeLine.TickFrequency = 18;
            this.TrackBarTimeLine.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.TrackBarTimeLine.ValueChanged += new System.EventHandler(this.TrackBarTimeLine_ValueChanged);
            // 
            // CbShowSpeed
            // 
            this.CbShowSpeed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CbShowSpeed.AutoSize = true;
            this.CbShowSpeed.Location = new System.Drawing.Point(498, 250);
            this.CbShowSpeed.Name = "CbShowSpeed";
            this.CbShowSpeed.Size = new System.Drawing.Size(87, 17);
            this.CbShowSpeed.TabIndex = 54;
            this.CbShowSpeed.Text = "Show Speed";
            this.CbShowSpeed.UseVisualStyleBackColor = true;
            this.CbShowSpeed.Click += new System.EventHandler(this.Checkbox_Clicked);
            // 
            // pb_preview
            // 
            this.pb_preview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pb_preview.BackColor = System.Drawing.Color.DarkGray;
            this.pb_preview.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pb_preview.Image = ((System.Drawing.Image)(resources.GetObject("pb_preview.Image")));
            this.pb_preview.Location = new System.Drawing.Point(60, 146);
            this.pb_preview.Name = "pb_preview";
            this.pb_preview.Size = new System.Drawing.Size(432, 168);
            this.pb_preview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pb_preview.TabIndex = 46;
            this.pb_preview.TabStop = false;
            // 
            // cbShowAlt
            // 
            this.cbShowAlt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbShowAlt.AutoSize = true;
            this.cbShowAlt.Location = new System.Drawing.Point(498, 181);
            this.cbShowAlt.Name = "cbShowAlt";
            this.cbShowAlt.Size = new System.Drawing.Size(90, 17);
            this.cbShowAlt.TabIndex = 53;
            this.cbShowAlt.Text = "Show altitude";
            this.cbShowAlt.UseVisualStyleBackColor = true;
            this.cbShowAlt.Click += new System.EventHandler(this.Checkbox_Clicked);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 31;
            this.label3.Text = "position";
            // 
            // CbShowMaxSpeed
            // 
            this.CbShowMaxSpeed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CbShowMaxSpeed.AutoSize = true;
            this.CbShowMaxSpeed.Location = new System.Drawing.Point(498, 227);
            this.CbShowMaxSpeed.Name = "CbShowMaxSpeed";
            this.CbShowMaxSpeed.Size = new System.Drawing.Size(110, 17);
            this.CbShowMaxSpeed.TabIndex = 52;
            this.CbShowMaxSpeed.Text = "Show Max-Speed";
            this.CbShowMaxSpeed.UseVisualStyleBackColor = true;
            this.CbShowMaxSpeed.Click += new System.EventHandler(this.Checkbox_Clicked);
            // 
            // CbShowGauge
            // 
            this.CbShowGauge.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CbShowGauge.AutoSize = true;
            this.CbShowGauge.Location = new System.Drawing.Point(498, 204);
            this.CbShowGauge.Name = "CbShowGauge";
            this.CbShowGauge.Size = new System.Drawing.Size(86, 17);
            this.CbShowGauge.TabIndex = 51;
            this.CbShowGauge.Text = "Show gauge";
            this.CbShowGauge.UseVisualStyleBackColor = true;
            this.CbShowGauge.Click += new System.EventHandler(this.Checkbox_Clicked);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(175, 97);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 13);
            this.label5.TabIndex = 33;
            this.label5.Text = "longitude";
            // 
            // CbMetric
            // 
            this.CbMetric.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CbMetric.AutoSize = true;
            this.CbMetric.Location = new System.Drawing.Point(498, 158);
            this.CbMetric.Name = "CbMetric";
            this.CbMetric.Size = new System.Drawing.Size(80, 17);
            this.CbMetric.TabIndex = 50;
            this.CbMetric.Text = "Metric units";
            this.CbMetric.UseVisualStyleBackColor = true;
            this.CbMetric.Click += new System.EventHandler(this.Checkbox_Clicked);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(314, 97);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 13);
            this.label6.TabIndex = 34;
            this.label6.Text = "altitude";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(10, 123);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(44, 13);
            this.label12.TabIndex = 49;
            this.label12.Text = "location";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(454, 97);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 13);
            this.label7.TabIndex = 35;
            this.label7.Text = "ground speed";
            // 
            // btnQueryLocation
            // 
            this.btnQueryLocation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQueryLocation.Location = new System.Drawing.Point(504, 120);
            this.btnQueryLocation.Name = "btnQueryLocation";
            this.btnQueryLocation.Size = new System.Drawing.Size(105, 20);
            this.btnQueryLocation.TabIndex = 30;
            this.btnQueryLocation.Text = "Query location";
            this.btnQueryLocation.UseVisualStyleBackColor = true;
            this.btnQueryLocation.Click += new System.EventHandler(this.btnQueryLocation_Click);
            // 
            // tb_latitude
            // 
            this.tb_latitude.BackColor = System.Drawing.SystemColors.Control;
            this.tb_latitude.Location = new System.Drawing.Point(60, 94);
            this.tb_latitude.Name = "tb_latitude";
            this.tb_latitude.ReadOnly = true;
            this.tb_latitude.Size = new System.Drawing.Size(107, 20);
            this.tb_latitude.TabIndex = 36;
            // 
            // TbLocation
            // 
            this.TbLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TbLocation.Location = new System.Drawing.Point(60, 120);
            this.TbLocation.Name = "TbLocation";
            this.TbLocation.Size = new System.Drawing.Size(438, 20);
            this.TbLocation.TabIndex = 48;
            this.TbLocation.TextChanged += new System.EventHandler(this.TbLocation_TextChanged);
            // 
            // tb_longitude
            // 
            this.tb_longitude.BackColor = System.Drawing.SystemColors.Control;
            this.tb_longitude.Location = new System.Drawing.Point(234, 94);
            this.tb_longitude.Name = "tb_longitude";
            this.tb_longitude.ReadOnly = true;
            this.tb_longitude.Size = new System.Drawing.Size(74, 20);
            this.tb_longitude.TabIndex = 37;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(9, 186);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(45, 13);
            this.label10.TabIndex = 47;
            this.label10.Text = "Preview";
            // 
            // tb_altitude
            // 
            this.tb_altitude.BackColor = System.Drawing.SystemColors.Control;
            this.tb_altitude.Location = new System.Drawing.Point(364, 94);
            this.tb_altitude.Name = "tb_altitude";
            this.tb_altitude.ReadOnly = true;
            this.tb_altitude.Size = new System.Drawing.Size(74, 20);
            this.tb_altitude.TabIndex = 38;
            // 
            // tb_pdop
            // 
            this.tb_pdop.BackColor = System.Drawing.SystemColors.Control;
            this.tb_pdop.Location = new System.Drawing.Point(535, 66);
            this.tb_pdop.Name = "tb_pdop";
            this.tb_pdop.ReadOnly = true;
            this.tb_pdop.Size = new System.Drawing.Size(74, 20);
            this.tb_pdop.TabIndex = 45;
            // 
            // tb_ground_speed
            // 
            this.tb_ground_speed.BackColor = System.Drawing.SystemColors.Control;
            this.tb_ground_speed.Location = new System.Drawing.Point(535, 94);
            this.tb_ground_speed.Name = "tb_ground_speed";
            this.tb_ground_speed.ReadOnly = true;
            this.tb_ground_speed.Size = new System.Drawing.Size(74, 20);
            this.tb_ground_speed.TabIndex = 39;
            // 
            // cb_3dlock
            // 
            this.cb_3dlock.AutoSize = true;
            this.cb_3dlock.Enabled = false;
            this.cb_3dlock.Location = new System.Drawing.Point(338, 69);
            this.cb_3dlock.Name = "cb_3dlock";
            this.cb_3dlock.Size = new System.Drawing.Size(67, 17);
            this.cb_3dlock.TabIndex = 44;
            this.cb_3dlock.Text = "3D Lock";
            this.cb_3dlock.UseVisualStyleBackColor = true;
            // 
            // tb_pos
            // 
            this.tb_pos.BackColor = System.Drawing.SystemColors.Control;
            this.tb_pos.Location = new System.Drawing.Point(60, 68);
            this.tb_pos.Name = "tb_pos";
            this.tb_pos.ReadOnly = true;
            this.tb_pos.Size = new System.Drawing.Size(107, 20);
            this.tb_pos.TabIndex = 40;
            // 
            // cb_2dlock
            // 
            this.cb_2dlock.AutoSize = true;
            this.cb_2dlock.Enabled = false;
            this.cb_2dlock.Location = new System.Drawing.Point(265, 69);
            this.cb_2dlock.Name = "cb_2dlock";
            this.cb_2dlock.Size = new System.Drawing.Size(67, 17);
            this.cb_2dlock.TabIndex = 43;
            this.cb_2dlock.Text = "2D Lock";
            this.cb_2dlock.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(435, 71);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(91, 13);
            this.label8.TabIndex = 41;
            this.label8.Text = "precision ( PDOP)";
            this.toolTip1.SetToolTip(this.label8, "The precision of the GPS signal < 300 = good");
            // 
            // cb_nolock
            // 
            this.cb_nolock.AutoSize = true;
            this.cb_nolock.Enabled = false;
            this.cb_nolock.Location = new System.Drawing.Point(199, 69);
            this.cb_nolock.Name = "cb_nolock";
            this.cb_nolock.Size = new System.Drawing.Size(67, 17);
            this.cb_nolock.TabIndex = 42;
            this.cb_nolock.Text = "No Lock";
            this.cb_nolock.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(7, 89);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(85, 13);
            this.label11.TabIndex = 38;
            this.label11.Text = "Overlay videofile";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(7, 57);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(66, 13);
            this.label9.TabIndex = 37;
            this.label9.Text = "Gps data file";
            // 
            // TbOverlayFile
            // 
            this.TbOverlayFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TbOverlayFile.BackColor = System.Drawing.SystemColors.Control;
            this.TbOverlayFile.Location = new System.Drawing.Point(98, 86);
            this.TbOverlayFile.Name = "TbOverlayFile";
            this.TbOverlayFile.ReadOnly = true;
            this.TbOverlayFile.Size = new System.Drawing.Size(532, 20);
            this.TbOverlayFile.TabIndex = 36;
            // 
            // TbGpsFile
            // 
            this.TbGpsFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TbGpsFile.BackColor = System.Drawing.SystemColors.Control;
            this.TbGpsFile.Location = new System.Drawing.Point(98, 60);
            this.TbGpsFile.Name = "TbGpsFile";
            this.TbGpsFile.ReadOnly = true;
            this.TbGpsFile.Size = new System.Drawing.Size(532, 20);
            this.TbGpsFile.TabIndex = 35;
            // 
            // BtnParse
            // 
            this.BtnParse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnParse.Location = new System.Drawing.Point(297, 114);
            this.BtnParse.Name = "BtnParse";
            this.BtnParse.Size = new System.Drawing.Size(155, 23);
            this.BtnParse.TabIndex = 34;
            this.BtnParse.Text = "Reparse Gps";
            this.BtnParse.UseVisualStyleBackColor = true;
            this.BtnParse.Click += new System.EventHandler(this.BtnParse_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 33;
            this.label1.Text = "Source mp4";
            // 
            // TbFilename
            // 
            this.TbFilename.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TbFilename.BackColor = System.Drawing.SystemColors.Control;
            this.TbFilename.Location = new System.Drawing.Point(98, 27);
            this.TbFilename.Name = "TbFilename";
            this.TbFilename.ReadOnly = true;
            this.TbFilename.Size = new System.Drawing.Size(470, 20);
            this.TbFilename.TabIndex = 32;
            // 
            // BtnSelect
            // 
            this.BtnSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSelect.Location = new System.Drawing.Point(575, 27);
            this.BtnSelect.Name = "BtnSelect";
            this.BtnSelect.Size = new System.Drawing.Size(56, 20);
            this.BtnSelect.TabIndex = 31;
            this.BtnSelect.Text = "select";
            this.BtnSelect.UseVisualStyleBackColor = true;
            this.BtnSelect.Click += new System.EventHandler(this.BtnSelect_Click);
            // 
            // BtnGenerateVideo
            // 
            this.BtnGenerateVideo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnGenerateVideo.Location = new System.Drawing.Point(475, 114);
            this.BtnGenerateVideo.Name = "BtnGenerateVideo";
            this.BtnGenerateVideo.Size = new System.Drawing.Size(155, 23);
            this.BtnGenerateVideo.TabIndex = 30;
            this.BtnGenerateVideo.Text = "Genarate overlay video";
            this.BtnGenerateVideo.UseVisualStyleBackColor = true;
            this.BtnGenerateVideo.Click += new System.EventHandler(this.btn_generate_video_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(634, 24);
            this.menuStrip1.TabIndex = 42;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.infoToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // infoToolStripMenuItem
            // 
            this.infoToolStripMenuItem.Name = "infoToolStripMenuItem";
            this.infoToolStripMenuItem.Size = new System.Drawing.Size(95, 22);
            this.infoToolStripMenuItem.Text = "Info";
            this.infoToolStripMenuItem.Click += new System.EventHandler(this.infoToolStripMenuItem_Click);
            // 
            // splitContainer
            // 
            this.splitContainer.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.PanelMain);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.TBLog);
            this.splitContainer.Size = new System.Drawing.Size(634, 611);
            this.splitContainer.SplitterDistance = 470;
            this.splitContainer.SplitterWidth = 10;
            this.splitContainer.TabIndex = 33;
            // 
            // LlbLatitude
            // 
            this.LlbLatitude.AutoSize = true;
            this.LlbLatitude.Location = new System.Drawing.Point(13, 97);
            this.LlbLatitude.Name = "LlbLatitude";
            this.LlbLatitude.Size = new System.Drawing.Size(41, 13);
            this.LlbLatitude.TabIndex = 56;
            this.LlbLatitude.TabStop = true;
            this.LlbLatitude.Text = "latitude";
            this.toolTip1.SetToolTip(this.LlbLatitude, "Shows the location in OpenStreetMap");
            this.LlbLatitude.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LlbLatitude_LinkClicked);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 611);
            this.Controls.Add(this.splitContainer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(640, 600);
            this.Name = "MainForm";
            this.Text = "ZGCam by Matthias Zartmann";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.PanelMain.ResumeLayout(false);
            this.PanelMain.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBarTimeLine)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_preview)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.Panel2.PerformLayout();
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox TBLog;
        private System.Windows.Forms.Panel PanelMain;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btnQueryLocation;
        private System.Windows.Forms.TextBox TbLocation;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tb_pdop;
        private System.Windows.Forms.CheckBox cb_3dlock;
        private System.Windows.Forms.CheckBox cb_2dlock;
        private System.Windows.Forms.CheckBox cb_nolock;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tb_pos;
        private System.Windows.Forms.TextBox tb_ground_speed;
        private System.Windows.Forms.TextBox tb_altitude;
        private System.Windows.Forms.TextBox tb_longitude;
        private System.Windows.Forms.TextBox tb_latitude;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label LbTimeline;
        private System.Windows.Forms.TrackBar TrackBarTimeLine;
        private System.Windows.Forms.PictureBox pb_preview;
        
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox TbOverlayFile;
        private System.Windows.Forms.TextBox TbGpsFile;
        private System.Windows.Forms.Button BtnParse;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TbFilename;
        private System.Windows.Forms.Button BtnSelect;
        private System.Windows.Forms.Button BtnGenerateVideo;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.CheckBox CbMetric;
        private System.Windows.Forms.CheckBox cbShowAlt;
        private System.Windows.Forms.CheckBox CbShowMaxSpeed;
        private System.Windows.Forms.CheckBox CbShowGauge;
        private System.Windows.Forms.CheckBox CbShowSpeed;
        public System.Windows.Forms.Label LbNoGps;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem infoToolStripMenuItem;
        private System.Windows.Forms.LinkLabel LlbLatitude;
        public System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}

