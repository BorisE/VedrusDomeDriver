namespace IP9212_switch
{
    partial class SetupDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetupDialog));
            this.port = new System.Windows.Forms.TextBox();
            this.pass = new System.Windows.Forms.TextBox();
            this.login = new System.Windows.Forms.TextBox();
            this.ipaddr = new System.Windows.Forms.TextBox();
            this.closed_port_state_type = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.opened_port_state_type = new System.Windows.Forms.CheckBox();
            this.switch_port_type = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.closedstateport = new System.Windows.Forms.TextBox();
            this.opened_port = new System.Windows.Forms.TextBox();
            this.switchport = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdOK = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkRoofPowerPortStateType = new System.Windows.Forms.CheckBox();
            this.chkHeatingSwitchPortStateType = new System.Windows.Forms.CheckBox();
            this.chkFocuserPowerPortStateType = new System.Windows.Forms.CheckBox();
            this.chkTelescopePowerPortStateType = new System.Windows.Forms.CheckBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtRoofPowerPort = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtHeatingSwitchPort = new System.Windows.Forms.TextBox();
            this.txtFocuserPowerSwitchPort = new System.Windows.Forms.TextBox();
            this.txtTelescopePowerSwitchPort = new System.Windows.Forms.TextBox();
            this.picASCOM = new System.Windows.Forms.PictureBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.lblVersion = new System.Windows.Forms.Label();
            this.linkAviosys = new System.Windows.Forms.LinkLabel();
            this.linkAstromania = new System.Windows.Forms.LinkLabel();
            this.picAstromania = new System.Windows.Forms.PictureBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbLang = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.txtNetworkTimeout = new System.Windows.Forms.TextBox();
            this.txtCacheRead = new System.Windows.Forms.TextBox();
            this.txtCacheConnect = new System.Windows.Forms.TextBox();
            this.chkTrace = new System.Windows.Forms.CheckBox();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picASCOM)).BeginInit();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picAstromania)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // port
            // 
            this.port.Location = new System.Drawing.Point(177, 27);
            this.port.Name = "port";
            this.port.Size = new System.Drawing.Size(38, 20);
            this.port.TabIndex = 11;
            // 
            // pass
            // 
            this.pass.Location = new System.Drawing.Point(63, 79);
            this.pass.MaxLength = 14;
            this.pass.Multiline = true;
            this.pass.Name = "pass";
            this.pass.PasswordChar = '*';
            this.pass.Size = new System.Drawing.Size(92, 20);
            this.pass.TabIndex = 12;
            // 
            // login
            // 
            this.login.Location = new System.Drawing.Point(63, 53);
            this.login.Name = "login";
            this.login.Size = new System.Drawing.Size(92, 20);
            this.login.TabIndex = 13;
            // 
            // ipaddr
            // 
            this.ipaddr.Location = new System.Drawing.Point(63, 27);
            this.ipaddr.Name = "ipaddr";
            this.ipaddr.Size = new System.Drawing.Size(92, 20);
            this.ipaddr.TabIndex = 14;
            // 
            // closed_port_state_type
            // 
            this.closed_port_state_type.AutoSize = true;
            this.closed_port_state_type.Location = new System.Drawing.Point(180, 80);
            this.closed_port_state_type.Name = "closed_port_state_type";
            this.closed_port_state_type.Size = new System.Drawing.Size(48, 17);
            this.closed_port_state_type.TabIndex = 6;
            this.closed_port_state_type.Text = "NOff";
            this.closed_port_state_type.UseVisualStyleBackColor = true;
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
            this.groupBox2.Location = new System.Drawing.Point(8, 112);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(262, 110);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Roll-off settings";
            // 
            // opened_port_state_type
            // 
            this.opened_port_state_type.AutoSize = true;
            this.opened_port_state_type.Location = new System.Drawing.Point(180, 54);
            this.opened_port_state_type.Name = "opened_port_state_type";
            this.opened_port_state_type.Size = new System.Drawing.Size(48, 17);
            this.opened_port_state_type.TabIndex = 6;
            this.opened_port_state_type.Text = "NOff";
            this.opened_port_state_type.UseVisualStyleBackColor = true;
            // 
            // switch_port_type
            // 
            this.switch_port_type.AutoSize = true;
            this.switch_port_type.Checked = true;
            this.switch_port_type.CheckState = System.Windows.Forms.CheckState.Checked;
            this.switch_port_type.Location = new System.Drawing.Point(180, 29);
            this.switch_port_type.Name = "switch_port_type";
            this.switch_port_type.Size = new System.Drawing.Size(48, 17);
            this.switch_port_type.TabIndex = 6;
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
            this.closedstateport.Location = new System.Drawing.Point(124, 77);
            this.closedstateport.Name = "closedstateport";
            this.closedstateport.Size = new System.Drawing.Size(38, 20);
            this.closedstateport.TabIndex = 4;
            // 
            // opened_port
            // 
            this.opened_port.Location = new System.Drawing.Point(124, 53);
            this.opened_port.Name = "opened_port";
            this.opened_port.Size = new System.Drawing.Size(38, 20);
            this.opened_port.TabIndex = 4;
            // 
            // switchport
            // 
            this.switchport.Location = new System.Drawing.Point(124, 27);
            this.switchport.Name = "switchport";
            this.switchport.Size = new System.Drawing.Size(38, 20);
            this.switchport.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(7, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(262, 100);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "IP9212 address";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(161, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(10, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = ":";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Login";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "IP addr";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 82);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "Pass";
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(336, 192);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(112, 25);
            this.cmdCancel.TabIndex = 8;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdOK
            // 
            this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cmdOK.Location = new System.Drawing.Point(336, 159);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(112, 24);
            this.cmdOK.TabIndex = 7;
            this.cmdOK.Text = "OK";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chkRoofPowerPortStateType);
            this.groupBox3.Controls.Add(this.chkHeatingSwitchPortStateType);
            this.groupBox3.Controls.Add(this.chkFocuserPowerPortStateType);
            this.groupBox3.Controls.Add(this.chkTelescopePowerPortStateType);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.txtRoofPowerPort);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.txtHeatingSwitchPort);
            this.groupBox3.Controls.Add(this.txtFocuserPowerSwitchPort);
            this.groupBox3.Controls.Add(this.txtTelescopePowerSwitchPort);
            this.groupBox3.Location = new System.Drawing.Point(8, 348);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(217, 148);
            this.groupBox3.TabIndex = 19;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Power switch settings";
            this.groupBox3.Visible = false;
            // 
            // chkRoofPowerPortStateType
            // 
            this.chkRoofPowerPortStateType.AutoSize = true;
            this.chkRoofPowerPortStateType.Location = new System.Drawing.Point(145, 117);
            this.chkRoofPowerPortStateType.Name = "chkRoofPowerPortStateType";
            this.chkRoofPowerPortStateType.Size = new System.Drawing.Size(48, 17);
            this.chkRoofPowerPortStateType.TabIndex = 6;
            this.chkRoofPowerPortStateType.Text = "NOff";
            this.chkRoofPowerPortStateType.UseVisualStyleBackColor = true;
            // 
            // chkHeatingSwitchPortStateType
            // 
            this.chkHeatingSwitchPortStateType.AutoSize = true;
            this.chkHeatingSwitchPortStateType.Location = new System.Drawing.Point(145, 91);
            this.chkHeatingSwitchPortStateType.Name = "chkHeatingSwitchPortStateType";
            this.chkHeatingSwitchPortStateType.Size = new System.Drawing.Size(48, 17);
            this.chkHeatingSwitchPortStateType.TabIndex = 6;
            this.chkHeatingSwitchPortStateType.Text = "NOff";
            this.chkHeatingSwitchPortStateType.UseVisualStyleBackColor = true;
            // 
            // chkFocuserPowerPortStateType
            // 
            this.chkFocuserPowerPortStateType.AutoSize = true;
            this.chkFocuserPowerPortStateType.Location = new System.Drawing.Point(147, 53);
            this.chkFocuserPowerPortStateType.Name = "chkFocuserPowerPortStateType";
            this.chkFocuserPowerPortStateType.Size = new System.Drawing.Size(48, 17);
            this.chkFocuserPowerPortStateType.TabIndex = 6;
            this.chkFocuserPowerPortStateType.Text = "NOff";
            this.chkFocuserPowerPortStateType.UseVisualStyleBackColor = true;
            // 
            // chkTelescopePowerPortStateType
            // 
            this.chkTelescopePowerPortStateType.AutoSize = true;
            this.chkTelescopePowerPortStateType.Checked = true;
            this.chkTelescopePowerPortStateType.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTelescopePowerPortStateType.Location = new System.Drawing.Point(147, 28);
            this.chkTelescopePowerPortStateType.Name = "chkTelescopePowerPortStateType";
            this.chkTelescopePowerPortStateType.Size = new System.Drawing.Size(48, 17);
            this.chkTelescopePowerPortStateType.TabIndex = 6;
            this.chkTelescopePowerPortStateType.Text = "NOff";
            this.chkTelescopePowerPortStateType.UseVisualStyleBackColor = true;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(9, 118);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(62, 13);
            this.label12.TabIndex = 5;
            this.label12.Text = "Roof power";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 92);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(76, 13);
            this.label9.TabIndex = 5;
            this.label9.Text = "Heating power";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(9, 54);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(77, 13);
            this.label10.TabIndex = 5;
            this.label10.Text = "Focuser power";
            // 
            // txtRoofPowerPort
            // 
            this.txtRoofPowerPort.Location = new System.Drawing.Point(101, 115);
            this.txtRoofPowerPort.Name = "txtRoofPowerPort";
            this.txtRoofPowerPort.Size = new System.Drawing.Size(38, 20);
            this.txtRoofPowerPort.TabIndex = 4;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(9, 28);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(89, 13);
            this.label11.TabIndex = 5;
            this.label11.Text = "Telescope power";
            // 
            // txtHeatingSwitchPort
            // 
            this.txtHeatingSwitchPort.Location = new System.Drawing.Point(101, 89);
            this.txtHeatingSwitchPort.Name = "txtHeatingSwitchPort";
            this.txtHeatingSwitchPort.Size = new System.Drawing.Size(38, 20);
            this.txtHeatingSwitchPort.TabIndex = 4;
            // 
            // txtFocuserPowerSwitchPort
            // 
            this.txtFocuserPowerSwitchPort.Location = new System.Drawing.Point(102, 51);
            this.txtFocuserPowerSwitchPort.Name = "txtFocuserPowerSwitchPort";
            this.txtFocuserPowerSwitchPort.Size = new System.Drawing.Size(38, 20);
            this.txtFocuserPowerSwitchPort.TabIndex = 4;
            // 
            // txtTelescopePowerSwitchPort
            // 
            this.txtTelescopePowerSwitchPort.Location = new System.Drawing.Point(102, 25);
            this.txtTelescopePowerSwitchPort.Name = "txtTelescopePowerSwitchPort";
            this.txtTelescopePowerSwitchPort.Size = new System.Drawing.Size(38, 20);
            this.txtTelescopePowerSwitchPort.TabIndex = 4;
            // 
            // picASCOM
            // 
            this.picASCOM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picASCOM.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picASCOM.Image = global::IP9212_switch.Properties.Resources.ASCOM;
            this.picASCOM.Location = new System.Drawing.Point(5, 75);
            this.picASCOM.Name = "picASCOM";
            this.picASCOM.Size = new System.Drawing.Size(48, 56);
            this.picASCOM.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picASCOM.TabIndex = 10;
            this.picASCOM.TabStop = false;
            this.picASCOM.Click += new System.EventHandler(this.BrowseToAscom);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.lblVersion);
            this.groupBox6.Controls.Add(this.linkAviosys);
            this.groupBox6.Controls.Add(this.linkAstromania);
            this.groupBox6.Controls.Add(this.picAstromania);
            this.groupBox6.Controls.Add(this.picASCOM);
            this.groupBox6.Location = new System.Drawing.Point(275, 6);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(210, 138);
            this.groupBox6.TabIndex = 24;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "About";
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblVersion.Location = new System.Drawing.Point(59, 73);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(72, 13);
            this.lblVersion.TabIndex = 2;
            this.lblVersion.Text = "Driver version";
            // 
            // linkAviosys
            // 
            this.linkAviosys.AutoSize = true;
            this.linkAviosys.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.linkAviosys.Location = new System.Drawing.Point(48, 39);
            this.linkAviosys.Name = "linkAviosys";
            this.linkAviosys.Size = new System.Drawing.Size(94, 13);
            this.linkAviosys.TabIndex = 1;
            this.linkAviosys.TabStop = true;
            this.linkAviosys.Text = "Device by Aviosys";
            // 
            // linkAstromania
            // 
            this.linkAstromania.AutoSize = true;
            this.linkAstromania.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.linkAstromania.Location = new System.Drawing.Point(48, 20);
            this.linkAstromania.Name = "linkAstromania";
            this.linkAstromania.Size = new System.Drawing.Size(104, 13);
            this.linkAstromania.TabIndex = 0;
            this.linkAstromania.TabStop = true;
            this.linkAstromania.Text = "Driver by Astromania";
            // 
            // picAstromania
            // 
            this.picAstromania.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picAstromania.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picAstromania.Image = global::IP9212_switch.Properties.Resources.logo_48_blue;
            this.picAstromania.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.picAstromania.Location = new System.Drawing.Point(5, 16);
            this.picAstromania.Name = "picAstromania";
            this.picAstromania.Size = new System.Drawing.Size(48, 48);
            this.picAstromania.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picAstromania.TabIndex = 3;
            this.picAstromania.TabStop = false;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label1);
            this.groupBox5.Controls.Add(this.cmbLang);
            this.groupBox5.Controls.Add(this.label13);
            this.groupBox5.Controls.Add(this.label14);
            this.groupBox5.Controls.Add(this.label15);
            this.groupBox5.Controls.Add(this.label16);
            this.groupBox5.Controls.Add(this.label17);
            this.groupBox5.Controls.Add(this.label18);
            this.groupBox5.Controls.Add(this.txtNetworkTimeout);
            this.groupBox5.Controls.Add(this.txtCacheRead);
            this.groupBox5.Controls.Add(this.txtCacheConnect);
            this.groupBox5.Controls.Add(this.chkTrace);
            this.groupBox5.Location = new System.Drawing.Point(7, 229);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(262, 113);
            this.groupBox5.TabIndex = 25;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Advanced";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(86, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Language";
            // 
            // cmbLang
            // 
            this.cmbLang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLang.FormattingEnabled = true;
            this.cmbLang.Location = new System.Drawing.Point(148, 15);
            this.cmbLang.Name = "cmbLang";
            this.cmbLang.Size = new System.Drawing.Size(110, 21);
            this.cmbLang.TabIndex = 2;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label13.Location = new System.Drawing.Point(221, 93);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(24, 13);
            this.label13.TabIndex = 11;
            this.label13.Text = "sec";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label14.Location = new System.Drawing.Point(221, 47);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(24, 13);
            this.label14.TabIndex = 5;
            this.label14.Text = "sec";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label15.Location = new System.Drawing.Point(221, 70);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(24, 13);
            this.label15.TabIndex = 8;
            this.label15.Text = "sec";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label16.Location = new System.Drawing.Point(7, 93);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(118, 13);
            this.label16.TabIndex = 9;
            this.label16.Text = "Caching switch data for";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label17.Location = new System.Drawing.Point(6, 47);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(84, 13);
            this.label17.TabIndex = 3;
            this.label17.Text = "Network timeout";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label18.Location = new System.Drawing.Point(6, 70);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(155, 13);
            this.label18.TabIndex = 6;
            this.label18.Text = "Caching check connections for";
            // 
            // txtNetworkTimeout
            // 
            this.txtNetworkTimeout.Location = new System.Drawing.Point(182, 44);
            this.txtNetworkTimeout.Name = "txtNetworkTimeout";
            this.txtNetworkTimeout.Size = new System.Drawing.Size(33, 20);
            this.txtNetworkTimeout.TabIndex = 4;
            // 
            // txtCacheRead
            // 
            this.txtCacheRead.Location = new System.Drawing.Point(182, 90);
            this.txtCacheRead.Name = "txtCacheRead";
            this.txtCacheRead.Size = new System.Drawing.Size(33, 20);
            this.txtCacheRead.TabIndex = 10;
            // 
            // txtCacheConnect
            // 
            this.txtCacheConnect.Location = new System.Drawing.Point(182, 67);
            this.txtCacheConnect.Name = "txtCacheConnect";
            this.txtCacheConnect.Size = new System.Drawing.Size(33, 20);
            this.txtCacheConnect.TabIndex = 7;
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
            // SetupDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(492, 498);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.port);
            this.Controls.Add(this.pass);
            this.Controls.Add(this.login);
            this.Controls.Add(this.ipaddr);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SetupDialog";
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.SetupDialog_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picASCOM)).EndInit();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picAstromania)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox port;
        private System.Windows.Forms.TextBox pass;
        private System.Windows.Forms.TextBox login;
        private System.Windows.Forms.TextBox ipaddr;
        private System.Windows.Forms.CheckBox closed_port_state_type;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox opened_port_state_type;
        private System.Windows.Forms.CheckBox switch_port_type;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox closedstateport;
        private System.Windows.Forms.TextBox opened_port;
        private System.Windows.Forms.TextBox switchport;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox chkHeatingSwitchPortStateType;
        private System.Windows.Forms.CheckBox chkFocuserPowerPortStateType;
        private System.Windows.Forms.CheckBox chkTelescopePowerPortStateType;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtHeatingSwitchPort;
        private System.Windows.Forms.TextBox txtFocuserPowerSwitchPort;
        private System.Windows.Forms.TextBox txtTelescopePowerSwitchPort;
        private System.Windows.Forms.CheckBox chkRoofPowerPortStateType;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtRoofPowerPort;
        private System.Windows.Forms.PictureBox picASCOM;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.LinkLabel linkAviosys;
        private System.Windows.Forms.LinkLabel linkAstromania;
        private System.Windows.Forms.PictureBox picAstromania;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbLang;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtNetworkTimeout;
        private System.Windows.Forms.TextBox txtCacheRead;
        private System.Windows.Forms.TextBox txtCacheConnect;
        private System.Windows.Forms.CheckBox chkTrace;
    }
}