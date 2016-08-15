using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using ASCOM.Utilities;
using System.Globalization;
using System.Diagnostics;
using System.Reflection;
using System.Threading;

namespace ASCOM.IP9212_rolloffroof3
{
    [ComVisible(false)]					// Form not registered for COM!
    public partial class SetupDialogForm : Form
    {
        IP9212_switch_class HardwareLnk;
        Dome DomeDriverLnk;


        public SetupDialogForm(Dome Dome_ext, IP9212_switch_class Hardware_ext)
        {
            //Link to parent Dome and Hardware classes
            DomeDriverLnk = Dome_ext;
            HardwareLnk = Hardware_ext;

            InitializeComponent();
            // Initialise current values of user settings from the ASCOM Profile 


            // Initialise current values of user settings from the ASCOM Profile 
            ipaddr.Text = IP9212_switch_class.ip_addr;
            port.Text = IP9212_switch_class.ip_port;
            login.Text = IP9212_switch_class.ip_login;
            pass.Text = IP9212_switch_class.ip_pass;

            chkTrace.Checked = IP9212_switch_class.traceState;
            txtNetworkTimeout.Text = Convert.ToInt32(MyWebClient.NETWORK_TIMEOUT / 1000).ToString();
            txtCacheConnect.Text = IP9212_switch_class.CACHE_CHECKCONNECTED_INTERVAL.ToString();
            txtCacheRead.Text = IP9212_switch_class.CACHE_SHUTTERSTATUS_INTERVAL_NORMAL.ToString();
            txtCacheReadReduced.Text = IP9212_switch_class.CACHE_SHUTTERSTATUS_INTERVAL_REDUCED.ToString();


            //Write driver version
            Version ver = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;

            string driverVersion = String.Format(CultureInfo.InvariantCulture, "{0}.{1} build {2}.{3}", ver.Major, ver.Minor, ver.Build, ver.Revision);
            //MessageBox.Show("Application " + assemName.Name + ", Version " + ver.ToString());
            string fileVersion = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion;

            lblDriverInfo.Text = DomeDriverLnk.DriverInfo;
            lblVersion.Text = "Driver: " + fileVersion;
            lblVersion.Text += Environment.NewLine + "Assembly: " + driverVersion;
            lblVersion.Text += Environment.NewLine + "Compile time: " + RetrieveLinkerTimestamp().ToString("yyyy-MM-dd HH:mm");

            //Language
            cmbLang.DataSource = new CultureInfo[]{
                CultureInfo.GetCultureInfo("en-US"),
                CultureInfo.GetCultureInfo("ru-RU")
            };
            cmbLang.DisplayMember = "NativeName";
            cmbLang.ValueMember = "Name";
            cmbLang.SelectedValue = IP9212_switch_class.currentLang;

            

            opened_port.Text = IP9212_switch_class.opened_sensor_port.ToString();
            opened_port_state_type.Checked = IP9212_switch_class.opened_port_state_type;

            closedstateport.Text = IP9212_switch_class.closed_sensor_port.ToString();
            closed_port_state_type.Checked = IP9212_switch_class.closed_port_state_type;

            switchport.Text = IP9212_switch_class.switch_roof_port.ToString();
            switch_port_type.Checked = IP9212_switch_class.switch_port_state_type;

            chkTrace.Checked = Dome.traceState;
        }



        private void cmdOK_Click(object sender, EventArgs ev) // OK button event handler
        {
            //Device settings
            IP9212_switch_class.ip_addr = ipaddr.Text;
            IP9212_switch_class.ip_port = port.Text;
            IP9212_switch_class.ip_pass = pass.Text;
            IP9212_switch_class.ip_login = login.Text;

            //Base settings
            #region Base settings
            try
            {
                IP9212_switch_class.opened_sensor_port = Convert.ToInt16(opened_port.Text);
            }
            catch (Exception e)
            {
                IP9212_switch_class.opened_sensor_port = Convert.ToInt16(IP9212_switch_class.opened_port_default);
                IP9212_switch_class.tl.LogMessage("SetupDialog_cmdOK", "Input string [opened_sensor_port] is not a sequence of digits [" + e.Message + "]");
            }

            try
            {
                IP9212_switch_class.closed_sensor_port = Convert.ToInt16(closedstateport.Text);
            }
            catch (Exception e)
            {
                IP9212_switch_class.closed_sensor_port = Convert.ToInt16(IP9212_switch_class.closed_port_default);
                IP9212_switch_class.tl.LogMessage("SetupDialog_cmdOK", "Input string [closed_sensor_port] is not a sequence of digits [" + e.Message + "]");
            }

            try
            {
                IP9212_switch_class.switch_roof_port = Convert.ToInt16(switchport.Text);
            }
            catch (Exception e)
            {
                IP9212_switch_class.switch_roof_port = Convert.ToInt16(IP9212_switch_class.switch_port_default);
                IP9212_switch_class.tl.LogMessage("SetupDialog_cmdOK", "Input string [switch_roof_port] is not a sequence of digits [" + e.Message + "]");
            }

            IP9212_switch_class.opened_port_state_type = opened_port_state_type.Checked;
            IP9212_switch_class.closed_port_state_type = closed_port_state_type.Checked;
            IP9212_switch_class.switch_port_state_type = switch_port_type.Checked;
            #endregion

            //Advanced settings
            #region Advanced settings

            IP9212_switch_class.traceState = chkTrace.Checked;

            try
            {
                MyWebClient.NETWORK_TIMEOUT = Convert.ToInt32(txtNetworkTimeout.Text) * 1000;
            }
            catch (Exception e)
            {
                MyWebClient.NETWORK_TIMEOUT = MyWebClient.NETWORK_TIMEOUT_default;
                IP9212_switch_class.tl.LogMessage("SetupDialog_cmdOK", "Input string [NetworkTimeout] is not a sequence of digits [" + e.Message + "]");
            }
            IP9212_switch_class.Semaphore_timeout = MyWebClient.NETWORK_TIMEOUT+ IP9212_switch_class.Semaphore_timeout_extratime;

            try
            {
                IP9212_switch_class.CACHE_CHECKCONNECTED_INTERVAL = Convert.ToUInt32(txtCacheConnect.Text);
            }
            catch (Exception e)
            {
                IP9212_switch_class.CACHE_CHECKCONNECTED_INTERVAL = IP9212_switch_class.CACHE_CHECKCONNECTED_INTERVAL_default;
                IP9212_switch_class.tl.LogMessage("SetupDialog_cmdOK", "Input string [CACHE_CHECKCONNECTED_INTERVAL] is not a sequence of digits [" + e.Message + "]");
            }

            try
            {
                IP9212_switch_class.CACHE_SHUTTERSTATUS_INTERVAL_NORMAL = Convert.ToUInt32(txtCacheRead.Text);
            }
            catch (Exception e)
            {
                IP9212_switch_class.CACHE_SHUTTERSTATUS_INTERVAL_NORMAL = IP9212_switch_class.CACHE_SHUTTERSTATUS_INTERVAL_NORMAL_default;
                IP9212_switch_class.tl.LogMessage("SetupDialog_cmdOK", "Input string [CACHE_SHUTTERSTATUS_INTERVAL_NORMAL] is not a sequence of digits [" + e.Message + "]");
            }

            try
            {
                IP9212_switch_class.CACHE_SHUTTERSTATUS_INTERVAL_REDUCED = Convert.ToUInt32(txtCacheReadReduced.Text);
            }
            catch (Exception e)
            {
                IP9212_switch_class.CACHE_SHUTTERSTATUS_INTERVAL_REDUCED = IP9212_switch_class.CACHE_SHUTTERSTATUS_INTERVAL_REDUCED_default;
                IP9212_switch_class.tl.LogMessage("SetupDialog_cmdOK", "Input string [CACHE_SHUTTERSTATUS_INTERVAL_REDUCED] is not a sequence of digits [" + e.Message + "]");
            }
            #endregion

            DomeDriverLnk.WriteProfile();
        }

        private void cmdCancel_Click(object sender, EventArgs e) // Cancel button event handler
        {
            Close();
        }

        private void linkAviosys_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (Thread.CurrentThread.CurrentUICulture.Name == "ru-RU")
                {
                    System.Diagnostics.Process.Start("http://www.aviosys.ru/ipp9212d.htm");
                }
                else
                {
                    System.Diagnostics.Process.Start("http://www.aviosys.com/9212delux.html");
                }
            }
            catch (System.ComponentModel.Win32Exception noBrowser)
            {
                if (noBrowser.ErrorCode == -2147467259)
                    MessageBox.Show(noBrowser.Message);
            }
            catch (System.Exception other)
            {
                MessageBox.Show(other.Message);
            }
        }

        private void BrowseToAstromania(object sender, EventArgs e) // Click on ASCOM logo event handler
        {
            try
            {
                if (Thread.CurrentThread.CurrentUICulture.Name == "ru-RU")
                {
                    System.Diagnostics.Process.Start("http://astromania.info/");
                }
                else
                {
                    System.Diagnostics.Process.Start("http://astromania.info/");
                }
            }
            catch (System.ComponentModel.Win32Exception noBrowser)
            {
                if (noBrowser.ErrorCode == -2147467259)
                    MessageBox.Show(noBrowser.Message);
            }
            catch (System.Exception other)
            {
                MessageBox.Show(other.Message);
            }
        }


        private void linkAstromania_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (Thread.CurrentThread.CurrentUICulture.Name == "ru-RU")
                {
                    System.Diagnostics.Process.Start("http://astromania.info/articles/ip9212driver");
                }
                else
                {
                    System.Diagnostics.Process.Start("http://astromania.info/articles/ip9212driver");
                }
            }
            catch (System.ComponentModel.Win32Exception noBrowser)
            {
                if (noBrowser.ErrorCode == -2147467259)
                    MessageBox.Show(noBrowser.Message);
            }
            catch (System.Exception other)
            {
                MessageBox.Show(other.Message);
            }

        }


        private DateTime RetrieveLinkerTimestamp()
        {
            string filePath = System.Reflection.Assembly.GetCallingAssembly().Location;
            const int c_PeHeaderOffset = 60;
            const int c_LinkerTimestampOffset = 8;
            byte[] b = new byte[2048];
            System.IO.Stream s = null;

            try
            {
                s = new System.IO.FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                s.Read(b, 0, 2048);
            }
            finally
            {
                if (s != null)
                {
                    s.Close();
                }
            }

            int i = System.BitConverter.ToInt32(b, c_PeHeaderOffset);
            int secondsSince1970 = System.BitConverter.ToInt32(b, i + c_LinkerTimestampOffset);
            DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0);
            dt = dt.AddSeconds(secondsSince1970);
            dt = dt.AddHours(TimeZone.CurrentTimeZone.GetUtcOffset(dt).Hours);
            return dt;
        }
    }
}