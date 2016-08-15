namespace ASCOM.IP9212_rolloffroof3
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.closed_port_state_type = new System.Windows.Forms.CheckBox();
            this.opened_port_state_type = new System.Windows.Forms.CheckBox();
            this.switch_port_type = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.closedstateport = new System.Windows.Forms.TextBox();
            this.opened_port = new System.Windows.Forms.TextBox();
            this.switchport = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.lblVersion = new System.Windows.Forms.Label();
            this.linkAviosys = new System.Windows.Forms.LinkLabel();
            this.linkAstromania = new System.Windows.Forms.LinkLabel();
            this.picAstromania = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbLang = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.txtNetworkTimeout = new System.Windows.Forms.TextBox();
            this.txtCacheRead = new System.Windows.Forms.TextBox();
            this.txtCacheConnect = new System.Windows.Forms.TextBox();
            this.chkTrace = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCacheReadReduced = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.lblDriverInfo = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
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
            this.cmdOK.Location = new System.Drawing.Point(262, 190);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(83, 24);
            this.cmdOK.TabIndex = 4;
            this.cmdOK.Text = "OK";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(354, 190);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(80, 25);
            this.cmdCancel.TabIndex = 5;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(143, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(10, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = ":";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(1, 76);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Pass";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Login";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "IP addr";
            // 
            // port
            // 
            this.port.Location = new System.Drawing.Point(156, 21);
            this.port.Name = "port";
            this.port.Size = new System.Drawing.Size(38, 20);
            this.port.TabIndex = 6;
            // 
            // pass
            // 
            this.pass.Location = new System.Drawing.Point(48, 73);
            this.pass.MaxLength = 14;
            this.pass.Multiline = true;
            this.pass.Name = "pass";
            this.pass.PasswordChar = '*';
            this.pass.Size = new System.Drawing.Size(92, 20);
            this.pass.TabIndex = 3;
            // 
            // login
            // 
            this.login.Location = new System.Drawing.Point(48, 47);
            this.login.Name = "login";
            this.login.Size = new System.Drawing.Size(92, 20);
            this.login.TabIndex = 2;
            // 
            // ipaddr
            // 
            this.ipaddr.Location = new System.Drawing.Point(48, 21);
            this.ipaddr.Name = "ipaddr";
            this.ipaddr.Size = new System.Drawing.Size(92, 20);
            this.ipaddr.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.closed_port_state_type);
            this.groupBox2.Controls.Add(this.opened_port_state_type);
            this.groupBox2.Controls.Add(this.switch_port_type);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.closedstateport);
            this.groupBox2.Controls.Add(this.opened_port);
            this.groupBox2.Controls.Add(this.switchport);
            this.groupBox2.Location = new System.Drawing.Point(11, 117);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(208, 107);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "IP9212 settings";
            // 
            // closed_port_state_type
            // 
            this.closed_port_state_type.AutoSize = true;
            this.closed_port_state_type.Location = new System.Drawing.Point(147, 79);
            this.closed_port_state_type.Name = "closed_port_state_type";
            this.closed_port_state_type.Size = new System.Drawing.Size(48, 17);
            this.closed_port_state_type.TabIndex = 5;
            this.closed_port_state_type.Text = "NOff";
            this.closed_port_state_type.UseVisualStyleBackColor = true;
            // 
            // opened_port_state_type
            // 
            this.opened_port_state_type.AutoSize = true;
            this.opened_port_state_type.Location = new System.Drawing.Point(147, 53);
            this.opened_port_state_type.Name = "opened_port_state_type";
            this.opened_port_state_type.Size = new System.Drawing.Size(48, 17);
            this.opened_port_state_type.TabIndex = 3;
            this.opened_port_state_type.Text = "NOff";
            this.opened_port_state_type.UseVisualStyleBackColor = true;
            // 
            // switch_port_type
            // 
            this.switch_port_type.AutoSize = true;
            this.switch_port_type.Checked = true;
            this.switch_port_type.CheckState = System.Windows.Forms.CheckState.Checked;
            this.switch_port_type.Location = new System.Drawing.Point(147, 28);
            this.switch_port_type.Name = "switch_port_type";
            this.switch_port_type.Size = new System.Drawing.Size(48, 17);
            this.switch_port_type.TabIndex = 1;
            this.switch_port_type.Text = "NOff";
            this.switch_port_type.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 79);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(86, 13);
            this.label8.TabIndex = 5;
            this.label8.Text = "Closed state port";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 54);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(92, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "Opened state port";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Press button port";
            // 
            // closedstateport
            // 
            this.closedstateport.Location = new System.Drawing.Point(101, 76);
            this.closedstateport.Name = "closedstateport";
            this.closedstateport.Size = new System.Drawing.Size(38, 20);
            this.closedstateport.TabIndex = 4;
            // 
            // opened_port
            // 
            this.opened_port.Location = new System.Drawing.Point(102, 51);
            this.opened_port.Name = "opened_port";
            this.opened_port.Size = new System.Drawing.Size(38, 20);
            this.opened_port.TabIndex = 2;
            // 
            // switchport
            // 
            this.switchport.Location = new System.Drawing.Point(102, 25);
            this.switchport.Name = "switchport";
            this.switchport.Size = new System.Drawing.Size(38, 20);
            this.switchport.TabIndex = 0;
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
            this.groupBox1.Location = new System.Drawing.Point(11, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(208, 103);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "IP9212 device";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.lblDriverInfo);
            this.groupBox6.Controls.Add(this.lblVersion);
            this.groupBox6.Controls.Add(this.linkAviosys);
            this.groupBox6.Controls.Add(this.linkAstromania);
            this.groupBox6.Controls.Add(this.picAstromania);
            this.groupBox6.Controls.Add(this.pictureBox1);
            this.groupBox6.Location = new System.Drawing.Point(231, 9);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(226, 169);
            this.groupBox6.TabIndex = 3;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "About";
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblVersion.Location = new System.Drawing.Point(60, 72);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(72, 13);
            this.lblVersion.TabIndex = 2;
            this.lblVersion.Text = "Driver version";
            // 
            // linkAviosys
            // 
            this.linkAviosys.AutoSize = true;
            this.linkAviosys.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.linkAviosys.Location = new System.Drawing.Point(120, 137);
            this.linkAviosys.Name = "linkAviosys";
            this.linkAviosys.Size = new System.Drawing.Size(94, 13);
            this.linkAviosys.TabIndex = 2;
            this.linkAviosys.TabStop = true;
            this.linkAviosys.Text = "Device by Aviosys";
            this.linkAviosys.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkAviosys_LinkClicked);
            // 
            // linkAstromania
            // 
            this.linkAstromania.AutoSize = true;
            this.linkAstromania.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.linkAstromania.Location = new System.Drawing.Point(10, 136);
            this.linkAstromania.Name = "linkAstromania";
            this.linkAstromania.Size = new System.Drawing.Size(104, 13);
            this.linkAstromania.TabIndex = 1;
            this.linkAstromania.TabStop = true;
            this.linkAstromania.Text = "Driver by Astromania";
            this.linkAstromania.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkAstromania_LinkClicked);
            // 
            // picAstromania
            // 
            this.picAstromania.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picAstromania.Image = global::ASCOM.IP9212_rolloffroof3.Properties.Resources.logo_48_blue;
            this.picAstromania.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.picAstromania.Location = new System.Drawing.Point(9, 14);
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
            this.pictureBox1.Image = global::ASCOM.IP9212_rolloffroof3.Properties.Resources.ASCOM;
            this.pictureBox1.Location = new System.Drawing.Point(9, 72);
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
            this.groupBox4.Controls.Add(this.chkTrace);
            this.groupBox4.Location = new System.Drawing.Point(11, 230);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(446, 146);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Advanced";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label9.Location = new System.Drawing.Point(109, 19);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(55, 13);
            this.label9.TabIndex = 1;
            this.label9.Text = "Language";
            // 
            // cmbLang
            // 
            this.cmbLang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLang.FormattingEnabled = true;
            this.cmbLang.Location = new System.Drawing.Point(170, 15);
            this.cmbLang.Name = "cmbLang";
            this.cmbLang.Size = new System.Drawing.Size(132, 21);
            this.cmbLang.TabIndex = 1;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label10.Location = new System.Drawing.Point(310, 93);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(24, 13);
            this.label10.TabIndex = 11;
            this.label10.Text = "sec";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label11.Location = new System.Drawing.Point(310, 47);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(24, 13);
            this.label11.TabIndex = 5;
            this.label11.Text = "sec";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label12.Location = new System.Drawing.Point(310, 70);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(24, 13);
            this.label12.TabIndex = 8;
            this.label12.Text = "sec";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label13.Location = new System.Drawing.Point(7, 93);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(169, 13);
            this.label13.TabIndex = 9;
            this.label13.Text = "Caching switch ouput port data for";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label14.Location = new System.Drawing.Point(6, 47);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(84, 13);
            this.label14.TabIndex = 3;
            this.label14.Text = "Network timeout";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label15.Location = new System.Drawing.Point(6, 70);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(155, 13);
            this.label15.TabIndex = 6;
            this.label15.Text = "Caching check connections for";
            // 
            // txtNetworkTimeout
            // 
            this.txtNetworkTimeout.Location = new System.Drawing.Point(271, 44);
            this.txtNetworkTimeout.Name = "txtNetworkTimeout";
            this.txtNetworkTimeout.Size = new System.Drawing.Size(33, 20);
            this.txtNetworkTimeout.TabIndex = 2;
            // 
            // txtCacheRead
            // 
            this.txtCacheRead.Location = new System.Drawing.Point(271, 90);
            this.txtCacheRead.Name = "txtCacheRead";
            this.txtCacheRead.Size = new System.Drawing.Size(33, 20);
            this.txtCacheRead.TabIndex = 4;
            // 
            // txtCacheConnect
            // 
            this.txtCacheConnect.Location = new System.Drawing.Point(271, 67);
            this.txtCacheConnect.Name = "txtCacheConnect";
            this.txtCacheConnect.Size = new System.Drawing.Size(33, 20);
            this.txtCacheConnect.TabIndex = 3;
            // 
            // chkTrace
            // 
            this.chkTrace.AutoSize = true;
            this.chkTrace.Checked = true;
            this.chkTrace.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTrace.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chkTrace.Location = new System.Drawing.Point(8, 19);
            this.chkTrace.Name = "chkTrace";
            this.chkTrace.Size = new System.Drawing.Size(69, 17);
            this.chkTrace.TabIndex = 0;
            this.chkTrace.Text = "Trace on";
            this.chkTrace.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(7, 118);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(258, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Caching switch ouput port data during open/close for";
            // 
            // txtCacheReadReduced
            // 
            this.txtCacheReadReduced.Location = new System.Drawing.Point(271, 115);
            this.txtCacheReadReduced.Name = "txtCacheReadReduced";
            this.txtCacheReadReduced.Size = new System.Drawing.Size(33, 20);
            this.txtCacheReadReduced.TabIndex = 5;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label16.Location = new System.Drawing.Point(310, 118);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(24, 13);
            this.label16.TabIndex = 11;
            this.label16.Text = "sec";
            // 
            // lblDriverInfo
            // 
            this.lblDriverInfo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblDriverInfo.Location = new System.Drawing.Point(60, 14);
            this.lblDriverInfo.Name = "lblDriverInfo";
            this.lblDriverInfo.Size = new System.Drawing.Size(154, 48);
            this.lblDriverInfo.TabIndex = 11;
            this.lblDriverInfo.Text = "Driver name";
            // 
            // SetupDialogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(465, 378);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SetupDialogForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "IP9212_rolloffroof2 Setup";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
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
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox closed_port_state_type;
        private System.Windows.Forms.CheckBox opened_port_state_type;
        private System.Windows.Forms.CheckBox switch_port_type;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox closedstateport;
        private System.Windows.Forms.TextBox opened_port;
        private System.Windows.Forms.TextBox switchport;
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
    }
}