namespace ASCOM.Vedrus_rolloffroof
{
    partial class TestASCOM2
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
            this.buttonChoose = new System.Windows.Forms.Button();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtShutterStatus = new System.Windows.Forms.TextBox();
            this.buttonCloseShutter = new System.Windows.Forms.Button();
            this.buttonOpenShutter = new System.Windows.Forms.Button();
            this.txtDriverId = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtLog = new System.Windows.Forms.RichTextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonChoose
            // 
            this.buttonChoose.Location = new System.Drawing.Point(6, 57);
            this.buttonChoose.Name = "buttonChoose";
            this.buttonChoose.Size = new System.Drawing.Size(81, 23);
            this.buttonChoose.TabIndex = 1;
            this.buttonChoose.Text = "Choose";
            this.buttonChoose.UseVisualStyleBackColor = true;
            this.buttonChoose.Click += new System.EventHandler(this.buttonChoose_Click);
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point(192, 29);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(72, 51);
            this.buttonConnect.TabIndex = 2;
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtShutterStatus);
            this.groupBox1.Controls.Add(this.buttonCloseShutter);
            this.groupBox1.Controls.Add(this.buttonOpenShutter);
            this.groupBox1.Location = new System.Drawing.Point(2, 115);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(272, 96);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Shutter";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Status:";
            // 
            // txtShutterStatus
            // 
            this.txtShutterStatus.Enabled = false;
            this.txtShutterStatus.Location = new System.Drawing.Point(74, 54);
            this.txtShutterStatus.Name = "txtShutterStatus";
            this.txtShutterStatus.Size = new System.Drawing.Size(111, 20);
            this.txtShutterStatus.TabIndex = 4;
            // 
            // buttonCloseShutter
            // 
            this.buttonCloseShutter.Enabled = false;
            this.buttonCloseShutter.Location = new System.Drawing.Point(110, 19);
            this.buttonCloseShutter.Name = "buttonCloseShutter";
            this.buttonCloseShutter.Size = new System.Drawing.Size(75, 23);
            this.buttonCloseShutter.TabIndex = 3;
            this.buttonCloseShutter.Text = "Close";
            this.buttonCloseShutter.UseVisualStyleBackColor = true;
            this.buttonCloseShutter.Click += new System.EventHandler(this.buttonCloseShutter_Click);
            // 
            // buttonOpenShutter
            // 
            this.buttonOpenShutter.Enabled = false;
            this.buttonOpenShutter.Location = new System.Drawing.Point(6, 19);
            this.buttonOpenShutter.Name = "buttonOpenShutter";
            this.buttonOpenShutter.Size = new System.Drawing.Size(75, 23);
            this.buttonOpenShutter.TabIndex = 3;
            this.buttonOpenShutter.Text = "Open";
            this.buttonOpenShutter.UseVisualStyleBackColor = true;
            this.buttonOpenShutter.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtDriverId
            // 
            this.txtDriverId.Enabled = false;
            this.txtDriverId.Location = new System.Drawing.Point(6, 31);
            this.txtDriverId.Name = "txtDriverId";
            this.txtDriverId.Size = new System.Drawing.Size(179, 20);
            this.txtDriverId.TabIndex = 8;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(106, 57);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(79, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Setup";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtDriverId);
            this.groupBox2.Controls.Add(this.buttonChoose);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.buttonConnect);
            this.groupBox2.Location = new System.Drawing.Point(2, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(272, 97);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Device";
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(296, 19);
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(465, 371);
            this.txtLog.TabIndex = 10;
            this.txtLog.Text = "";
            // 
            // TestASCOM2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(775, 406);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "TestASCOM2";
            this.Text = "TestASCOM2";
            this.Load += new System.EventHandler(this.TestASCOM2_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonChoose;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtShutterStatus;
        private System.Windows.Forms.Button buttonCloseShutter;
        private System.Windows.Forms.Button buttonOpenShutter;
        private System.Windows.Forms.TextBox txtDriverId;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RichTextBox txtLog;
    }
}