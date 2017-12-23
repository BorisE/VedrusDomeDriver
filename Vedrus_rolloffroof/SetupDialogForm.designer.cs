namespace ASCOM.Vedrus_rolloffroof
{
    partial class SetupDialogForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.cmdOK = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.port = new System.Windows.Forms.TextBox();
            this.pass = new System.Windows.Forms.TextBox();
            this.login = new System.Windows.Forms.TextBox();
            this.ipaddr = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.lblDriverInfo = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.linkAviosys = new System.Windows.Forms.LinkLabel();
            this.linkAstromania = new System.Windows.Forms.LinkLabel();
            this.picAstromania = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbLang = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.txtNetworkTimeout = new System.Windows.Forms.TextBox();
            this.txtCacheReadReduced = new System.Windows.Forms.TextBox();
            this.txtCacheRead = new System.Windows.Forms.TextBox();
            this.txtCacheConnect = new System.Windows.Forms.TextBox();
            this.chkTrace = new System.Windows.Forms.CheckBox();
            this.chkDebugFlag = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picAstromania)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdOK
            // 
            this.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cmdOK.Location = new System.Drawing.Point(521, 346);
            this.cmdOK.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(159, 49);
            this.cmdOK.TabIndex = 4;
            this.cmdOK.Text = "OK";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(708, 346);
            this.cmdCancel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(152, 48);
            this.cmdCancel.TabIndex = 5;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(214, 37);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(13, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = ":";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(2, 117);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 20);
            this.label5.TabIndex = 13;
            this.label5.Text = "Pass";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(2, 77);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 20);
            this.label4.TabIndex = 14;
            this.label4.Text = "Login";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 37);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 20);
            this.label2.TabIndex = 15;
            this.label2.Text = "IP addr";
            // 
            // port
            // 
            this.port.Location = new System.Drawing.Point(234, 32);
            this.port.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.port.Name = "port";
            this.port.Size = new System.Drawing.Size(55, 26);
            this.port.TabIndex = 6;
            // 
            // pass
            // 
            this.pass.Enabled = false;
            this.pass.Location = new System.Drawing.Point(72, 112);
            this.pass.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pass.MaxLength = 14;
            this.pass.Multiline = true;
            this.pass.Name = "pass";
            this.pass.PasswordChar = '*';
            this.pass.Size = new System.Drawing.Size(136, 29);
            this.pass.TabIndex = 3;
            // 
            // login
            // 
            this.login.Enabled = false;
            this.login.Location = new System.Drawing.Point(72, 72);
            this.login.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.login.Name = "login";
            this.login.Size = new System.Drawing.Size(136, 26);
            this.login.TabIndex = 2;
            // 
            // ipaddr
            // 
            this.ipaddr.Location = new System.Drawing.Point(72, 32);
            this.ipaddr.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ipaddr.Name = "ipaddr";
            this.ipaddr.Size = new System.Drawing.Size(136, 26);
            this.ipaddr.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.ipaddr);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.login);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.pass);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.port);
            this.groupBox1.Location = new System.Drawing.Point(16, 12);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Size = new System.Drawing.Size(477, 158);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Device";
            // 
            // groupBox6
            // 
            this.groupBox6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox6.Controls.Add(this.lblDriverInfo);
            this.groupBox6.Controls.Add(this.lblVersion);
            this.groupBox6.Controls.Add(this.linkAviosys);
            this.groupBox6.Controls.Add(this.linkAstromania);
            this.groupBox6.Controls.Add(this.picAstromania);
            this.groupBox6.Controls.Add(this.pictureBox1);
            this.groupBox6.Location = new System.Drawing.Point(502, 14);
            this.groupBox6.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox6.Size = new System.Drawing.Size(392, 294);
            this.groupBox6.TabIndex = 3;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "About";
            // 
            // lblDriverInfo
            // 
            this.lblDriverInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblDriverInfo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblDriverInfo.Location = new System.Drawing.Point(90, 22);
            this.lblDriverInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDriverInfo.Name = "lblDriverInfo";
            this.lblDriverInfo.Size = new System.Drawing.Size(292, 120);
            this.lblDriverInfo.TabIndex = 11;
            this.lblDriverInfo.Text = "Driver name";
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblVersion.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblVersion.Location = new System.Drawing.Point(90, 143);
            this.lblVersion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(104, 20);
            this.lblVersion.TabIndex = 2;
            this.lblVersion.Text = "Driver version";
            // 
            // linkAviosys
            // 
            this.linkAviosys.AutoSize = true;
            this.linkAviosys.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.linkAviosys.Location = new System.Drawing.Point(180, 246);
            this.linkAviosys.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.linkAviosys.Name = "linkAviosys";
            this.linkAviosys.Size = new System.Drawing.Size(106, 20);
            this.linkAviosys.TabIndex = 2;
            this.linkAviosys.TabStop = true;
            this.linkAviosys.Text = "Device by MO";
            this.linkAviosys.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkAviosys_LinkClicked);
            // 
            // linkAstromania
            // 
            this.linkAstromania.AutoSize = true;
            this.linkAstromania.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.linkAstromania.Location = new System.Drawing.Point(15, 245);
            this.linkAstromania.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.linkAstromania.Name = "linkAstromania";
            this.linkAstromania.Size = new System.Drawing.Size(155, 20);
            this.linkAstromania.TabIndex = 1;
            this.linkAstromania.TabStop = true;
            this.linkAstromania.Text = "Driver by Astromania";
            this.linkAstromania.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkAstromania_LinkClicked);
            // 
            // picAstromania
            // 
            this.picAstromania.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picAstromania.Image = global::ASCOM.Vedrus_rolloffroof.Properties.Resources.logo_48_blue;
            this.picAstromania.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.picAstromania.Location = new System.Drawing.Point(14, 35);
            this.picAstromania.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.picAstromania.Name = "picAstromania";
            this.picAstromania.Size = new System.Drawing.Size(48, 48);
            this.picAstromania.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picAstromania.TabIndex = 3;
            this.picAstromania.TabStop = false;
            this.picAstromania.Click += new System.EventHandler(this.BrowseToAstromania);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Image = global::ASCOM.Vedrus_rolloffroof.Properties.Resources.ASCOM;
            this.pictureBox1.Location = new System.Drawing.Point(14, 143);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(48, 56);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.cmbLang);
            this.groupBox4.Controls.Add(this.label16);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Controls.Add(this.label12);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.label13);
            this.groupBox4.Controls.Add(this.label14);
            this.groupBox4.Controls.Add(this.label15);
            this.groupBox4.Controls.Add(this.txtNetworkTimeout);
            this.groupBox4.Controls.Add(this.txtCacheReadReduced);
            this.groupBox4.Controls.Add(this.txtCacheRead);
            this.groupBox4.Controls.Add(this.txtCacheConnect);
            this.groupBox4.Controls.Add(this.chkDebugFlag);
            this.groupBox4.Controls.Add(this.chkTrace);
            this.groupBox4.Location = new System.Drawing.Point(16, 180);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox4.Size = new System.Drawing.Size(477, 265);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Advanced";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label9.Location = new System.Drawing.Point(164, 29);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(81, 20);
            this.label9.TabIndex = 1;
            this.label9.Text = "Language";
            // 
            // cmbLang
            // 
            this.cmbLang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLang.FormattingEnabled = true;
            this.cmbLang.Location = new System.Drawing.Point(255, 23);
            this.cmbLang.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbLang.Name = "cmbLang";
            this.cmbLang.Size = new System.Drawing.Size(196, 28);
            this.cmbLang.TabIndex = 1;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label16.Location = new System.Drawing.Point(415, 232);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(34, 20);
            this.label16.TabIndex = 11;
            this.label16.Text = "sec";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label10.Location = new System.Drawing.Point(415, 193);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(34, 20);
            this.label10.TabIndex = 11;
            this.label10.Text = "sec";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label11.Location = new System.Drawing.Point(415, 122);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(34, 20);
            this.label11.TabIndex = 5;
            this.label11.Text = "sec";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label12.Location = new System.Drawing.Point(415, 158);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(34, 20);
            this.label12.TabIndex = 8;
            this.label12.Text = "sec";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(9, 231);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(380, 20);
            this.label1.TabIndex = 9;
            this.label1.Text = "Caching switch ouput port data during open/close for";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label13.Location = new System.Drawing.Point(9, 192);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(251, 20);
            this.label13.TabIndex = 9;
            this.label13.Text = "Caching switch ouput port data for";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label14.Location = new System.Drawing.Point(8, 121);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(124, 20);
            this.label14.TabIndex = 3;
            this.label14.Text = "Network timeout";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label15.Location = new System.Drawing.Point(8, 157);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(226, 20);
            this.label15.TabIndex = 6;
            this.label15.Text = "Caching check connections for";
            // 
            // txtNetworkTimeout
            // 
            this.txtNetworkTimeout.Location = new System.Drawing.Point(357, 118);
            this.txtNetworkTimeout.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtNetworkTimeout.Name = "txtNetworkTimeout";
            this.txtNetworkTimeout.Size = new System.Drawing.Size(48, 26);
            this.txtNetworkTimeout.TabIndex = 2;
            // 
            // txtCacheReadReduced
            // 
            this.txtCacheReadReduced.Enabled = false;
            this.txtCacheReadReduced.Location = new System.Drawing.Point(357, 227);
            this.txtCacheReadReduced.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtCacheReadReduced.Name = "txtCacheReadReduced";
            this.txtCacheReadReduced.Size = new System.Drawing.Size(48, 26);
            this.txtCacheReadReduced.TabIndex = 5;
            // 
            // txtCacheRead
            // 
            this.txtCacheRead.Enabled = false;
            this.txtCacheRead.Location = new System.Drawing.Point(357, 188);
            this.txtCacheRead.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtCacheRead.Name = "txtCacheRead";
            this.txtCacheRead.Size = new System.Drawing.Size(48, 26);
            this.txtCacheRead.TabIndex = 4;
            // 
            // txtCacheConnect
            // 
            this.txtCacheConnect.Location = new System.Drawing.Point(357, 153);
            this.txtCacheConnect.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtCacheConnect.Name = "txtCacheConnect";
            this.txtCacheConnect.Size = new System.Drawing.Size(48, 26);
            this.txtCacheConnect.TabIndex = 3;
            // 
            // chkTrace
            // 
            this.chkTrace.AutoSize = true;
            this.chkTrace.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chkTrace.Location = new System.Drawing.Point(12, 29);
            this.chkTrace.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkTrace.Name = "chkTrace";
            this.chkTrace.Size = new System.Drawing.Size(97, 24);
            this.chkTrace.TabIndex = 0;
            this.chkTrace.Text = "Trace on";
            this.toolTip1.SetToolTip(this.chkTrace, "Write log files");
            this.chkTrace.UseVisualStyleBackColor = true;
            // 
            // chkDebugFlag
            // 
            this.chkDebugFlag.AutoSize = true;
            this.chkDebugFlag.Checked = true;
            this.chkDebugFlag.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDebugFlag.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chkDebugFlag.Location = new System.Drawing.Point(12, 63);
            this.chkDebugFlag.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkDebugFlag.Name = "chkDebugFlag";
            this.chkDebugFlag.Size = new System.Drawing.Size(188, 24);
            this.chkDebugFlag.TabIndex = 0;
            this.chkDebugFlag.Text = "Debug (emulation) on";
            this.toolTip1.SetToolTip(this.chkDebugFlag, "Used for debugging purposes, do not enable unless you know what are you doing");
            this.chkDebugFlag.UseVisualStyleBackColor = true;
            // 
            // SetupDialogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(912, 472);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SetupDialogForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dome Driver Setup";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picAstromania)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox port;
        private System.Windows.Forms.TextBox pass;
        private System.Windows.Forms.TextBox login;
        private System.Windows.Forms.TextBox ipaddr;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.LinkLabel linkAviosys;
        private System.Windows.Forms.LinkLabel linkAstromania;
        private System.Windows.Forms.PictureBox picAstromania;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmbLang;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtNetworkTimeout;
        private System.Windows.Forms.TextBox txtCacheRead;
        private System.Windows.Forms.TextBox txtCacheConnect;
        private System.Windows.Forms.CheckBox chkTrace;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCacheReadReduced;
        private System.Windows.Forms.Label lblDriverInfo;
        private System.Windows.Forms.CheckBox chkDebugFlag;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}