using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Globalization;

using ASCOM;
using ASCOM.Utilities;
using ASCOM.DeviceInterface;

namespace ASCOM.IP9212_rolloffroof3
{
    public class MyWebClient : WebClient
    {
        public const int NETWORK_TIMEOUT_default = 5 * 1000;
        static public int NETWORK_TIMEOUT = NETWORK_TIMEOUT_default;
        internal static string NETWORK_TIMEOUT_profilename = "NetworkTimeout";

        protected override WebRequest GetWebRequest(Uri uri)
        {
            WebRequest w = base.GetWebRequest(uri);
            w.Timeout = NETWORK_TIMEOUT;
            return w;
        }
    }

    /// <summary>
    /// Class for working with ip9212 device
    /// </summary>
    public class IP9212_switch_class
    {
        Dome DomeDriverLnk;

        internal bool debugFlag = false;

        public static string IP9212_switch_id = "IP9212_switch";
        public static string IP9212_switch_description = "Aviosys IP9212 observatory switch driver. Written by Boris Emchenko http://astromania.info";
        public static string IP9212_switch_description_short = "Aviosys IP9212 switch";

        //Settings
        #region Settings variables
        public static string ip_addr, ip_port, ip_login, ip_pass;
        internal static string ip_addr_profilename = "IP address", ip_port_profilename = "Port number", ip_login_profilename = "login", ip_pass_profilename = "password";
        internal static string ip_addr_default = "192.168.1.90", ip_port_default = "80", ip_login_default = "admin", ip_pass_default = "12345678";

        internal static int switch_roof_port, opened_sensor_port, closed_sensor_port;
        internal static string switch_port_profilename = "Roof switch", opened_port_profilename = "Roof opened state port", closed_port_profilename = "Roof closed state port";
        internal static string switch_port_default = "5", opened_port_default = "6", closed_port_default = "5";

        internal static bool switch_port_state_type, opened_port_state_type, closed_port_state_type;
        internal static string switch_port_state_type_profilename = "Roof switch port state type", opened_port_state_type_profilename = "Roof opened state port state type", closed_port_state_type_profilename = "Roof closed state port state type";
        internal static string switch_port_state_type_default = "true", opened_port_state_type_default = "false", closed_port_state_type_default = "false";
        #endregion Settings variables

        #region Advanced settings

        //public static string ip_addr, ip_port, ip_login, ip_pass;

        internal static bool traceState;
        internal static string traceStateProfileName = "Trace Level";
        internal static string traceStateDefault = "true";

        public static string currentLang= "ru-RU";
        internal static string currentLocalizationProfileName = "Current language";
        internal static string currentLangDefault = "ru-RU";

        #endregion

        #region Obsolete parameters
        internal static int telescope_power_port, focuser_power_port, heating_port, roofpower_port;
        internal static string telescope_power_port_profilename = "Telescope power port", focuser_power_port_profilename = "Focuser power port", heating_port_profilename = "Heating port state type", roof_power_port_profilename = "Roof power port";
        internal static string telescope_power_port_default = "6", focuser_power_port_default = "8", heating_port_default = "7", roof_power_port_default = "3";

        internal static bool telescope_power_port_state_type, focuser_power_port_state_type, heating_port_state_type, roofpower_port_state_type;
        internal static string telescope_power_port_state_type_profilename = "Telescope power port state type", focuser_power_port_state_type_profilename = "Focuser power port state type", heating_port_state_type_profilename = "Heating port state type", roof_power_port_state_type_profilename = "Roof power port state type";
        internal static string telescope_power_port_state_type_default = "true", focuser_power_port_state_type_default = "true", heating_port_state_type_default = "true", roof_power_port_state_type_default = "false";
        #endregion

        /// <summary>
        /// input sensors state
        /// </summary>
        private int[] input_state_arr = new int[1] { -1 };
        // [0] - overall read status
        // [1..8] - status of # input

        /// <summary>
        /// connected?
        /// </summary>
        public bool hardware_connected_flag = false;


        /// <summary>
        /// Private variable to hold the trace logger object (creates a diagnostic log file with information that you specify)
        /// </summary>
        public static TraceLogger tl;

        /// <summary>
        /// Semaphor for blocking concurrent requests
        /// </summary>
        public static Semaphore IP9212Semaphore;
        public static Int32 Semaphore_timeout = 2000; //millisec
        internal static readonly int Semaphore_timeout_extratime = 2000; //2 sec to NetworkTimeout

        /// <summary>
        /// error message (on hardware level) - don't forget, that there is another one on driver level
        /// all this done for saving error message text during exception and display it to user (MaximDL tested)
        /// </summary>
        public string ASCOM_ERROR_MESSAGE = "";

        public bool opened_shutter_flag;
        public bool closed_shutter_flag;

        //Caching connection check
        public static DateTime EXPIRED_CACHE = new DateTime(2010, 05, 12, 13, 15, 00); //CONSTANT FOR MARKING AN OLD TIME
        private DateTime lastConnectedCheck = EXPIRED_CACHE; //when was the last hardware checking provided for connect state

        public const uint CACHE_CHECKCONNECTED_INTERVAL_default = 10;
        public static uint CACHE_CHECKCONNECTED_INTERVAL = CACHE_CHECKCONNECTED_INTERVAL_default; //how often to held hardware checking (in seconds)
        internal static string CACHE_CHECKCONNECTED_INTERVAL_profilename = "CACHE_CHECKCONNECTED_INTERVAL";

        //Previos shutter states
        private ShutterState prev_shutter_state;
        bool last_OpenedState; // last measured value for opened sensor
        bool last_ClosedState; // last measured value closed sensor

        //Caching last shutter status
        public DateTime lastShutterStatusCheck = EXPIRED_CACHE; //when was the last hardware checking provided for shutter state 

        //how often to check true shutter status (in seconds) for regular cases
        public const uint CACHE_SHUTTERSTATUS_INTERVAL_NORMAL_default = 10;
        public static uint CACHE_SHUTTERSTATUS_INTERVAL_NORMAL = CACHE_SHUTTERSTATUS_INTERVAL_NORMAL_default;
        //how often to check true shutter status (in seconds) when shutter is moving
        public const uint CACHE_SHUTTERSTATUS_INTERVAL_REDUCED_default = 2;
        public static uint CACHE_SHUTTERSTATUS_INTERVAL_REDUCED = CACHE_SHUTTERSTATUS_INTERVAL_REDUCED_default;
        internal static string CACHE_SHUTTERSTATUS_INTERVAL_NORMAL_profilename = "CACHE_SHUTTERSTATUS_INTERVAL_NORMAL", CACHE_SHUTTERSTATUS_INTERVAL_REDUCED_profilename = "CACHE_SHUTTERSTATUS_INTERVAL_REDUCED";

        /// <summary>
        /// Constructor of IP9212_switch_class
        /// </summary>
        public IP9212_switch_class(Dome DomeDriver_ext)
        {
            DomeDriverLnk = DomeDriver_ext;

            tl = DomeDriverLnk.tl; //the same logger with Dome Driver

            //tl.Enabled = true; //default value before reading settings

            //RegisterSettings();
            //readSettings();
            tl.LogMessage("Switch_constructor", "Starting initialisation");

            hardware_connected_flag = false;

            IP9212Semaphore = new Semaphore(1, 2, "ip9212");

            tl.LogMessage("Switch_constructor", "Exit");
        }

        // if we need to ESTABLISH CONNECTION
        public void Connect()
        {
            tl.LogMessage("Switch_Connect", "Enter");

            // Get the ip9212 settings from the profile and cache them in appropriate fields
            //readSettings();

            //reset cache
            lastConnectedCheck = EXPIRED_CACHE;
            lastShutterStatusCheck = EXPIRED_CACHE;

            //if current state of connection coincidies with new state then do nothing
            if (hardware_connected_flag)
            {
                tl.LogMessage("Switch_Connect", "Exit because of no state change");
                return;
            }

            // check (forced) if there is connection with hardware
            if (IsConnected(true))
            {
                tl.LogMessage("Switch_Connect", "Connected");
                return;
            }
            else
            {
                tl.LogMessage("Switch_Connect", "Couldn't connect to IP9212 control device on [" + ip_addr + "]");
                ASCOM_ERROR_MESSAGE = "Couldn't connect to IP9212 control device on [" + ip_addr + "]";
                //throw new ASCOM.DriverException(ASCOM_ERROR_MESSAGE);

            }
            tl.LogMessage("Switch_Connect", "Exit");
        }

        // if we need to ESTABLISH CONNECTION
        public void Disconnect()
        {
            tl.LogMessage("Switch_Disconnect", "Enter");

            // Get the ip9212 settings from the profile and cache them in appropriate fields
            //readSettings();

            //reset cache
            lastConnectedCheck = EXPIRED_CACHE;
            lastShutterStatusCheck = EXPIRED_CACHE;

            //if current state of connection coincidies with new state then do nothing
            if (!hardware_connected_flag)
            {
                tl.LogMessage("Switch_Disconnect", "Exit because of no state change");
                return;
            }
            // if we need to DICONNECT - do nothing except changing _Hardware_Connected 
            hardware_connected_flag = false;
            tl.LogMessage("Switch_Disconnect", "Exit");
        }

        /// <summary>
        /// Check if device is available
        /// </summary>
        /// <param name="forcedflag">[bool] if function need to force noncached checking of device availability</param>
        /// <returns>true is available, false otherwise</returns>
        public bool IsConnected(bool forcedflag = false)
        {
            tl.LogMessage("Switch_IsConnected", "Enter, forced flag=" + forcedflag.ToString());

            //Check - if forced mode? (=no cache, no async)
            if (forcedflag)
            {
                hardware_connected_flag = false;
                checkLink_forced();
                return hardware_connected_flag;
            }

            //Usual mode
            //Measure how much time have passed since last HARDWARE measure
            TimeSpan passed = DateTime.Now - lastConnectedCheck;
            if (passed.TotalSeconds > CACHE_CHECKCONNECTED_INTERVAL)
            {
                // check that the driver hardware connection exists and is connected to the hardware
                tl.LogMessage("Switch_IsConnected", "Starting read hardware values thread [in cache: " + passed.TotalSeconds + "s]...");
                // reset cache
                lastConnectedCheck = DateTime.Now;

                //read
                checkLink_async();
            }
            else
            {
                // use previos value
                tl.LogMessage("Switch_IsConnected", "Using cached value [in cache:" + passed.TotalSeconds + "s]");
            }

            tl.LogMessage("Switch_IsConnected", "Exit. Return value: " + hardware_connected_flag);
            return hardware_connected_flag;
        }



        /// <summary>
        /// Check the availability of IP server by starting async read from input sensors. Result handeled to checkLink_DownloadCompleted()
        /// </summary>
        /// <returns>Nothing</returns> 
        public void checkLink_async()
        {
            tl.LogMessage("Switch_CheckLink_async", "enter");

            //Check - address was specified?
            if (string.IsNullOrEmpty(ip_addr))
            {
                hardware_connected_flag = false;
                tl.LogMessage("Switch_CheckLink_async", "ERROR (ip_addr wasn't set)!");
                // report a problem with the port name
                //throw new ASCOM.DriverException("checkLink_async error");
                return;
            }

            string siteipURL;
            siteipURL = "http://" + ip_login + ":" + ip_pass + "@" + ip_addr + ":" + ip_port + "/set.cmd?cmd=getio";
            // new style
            siteipURL = "http://" + ip_addr + ":" + ip_port + "/Set.cmd?user=" + ip_login + "+pass=" + ip_pass + "CMD=getio";
            //FOR DEBUGGING
            if (debugFlag)
            {
                siteipURL = "http://localhost/ip9212/getio.php";
            }
            Uri uri_siteipURL = new Uri(siteipURL);
            tl.LogMessage("Switch_CheckLink_async", "download url:" + siteipURL);

            // Send http query
            MyWebClient client = new MyWebClient();
            try
            {
                tl.LogMessage(">Semaphore", "WaitOne");
                IP9212Semaphore.WaitOne(Semaphore_timeout); // lock working with IP9212

                client.DownloadDataCompleted += new DownloadDataCompletedEventHandler(checkLink_DownloadCompleted);

                client.DownloadDataAsync(uri_siteipURL);

                tl.LogMessage("Switch_CheckLink_async", "http request was sent");
            }
            catch (WebException e)
            {
                tl.LogMessage("<Semaphore", "Release");
                IP9212Semaphore.Release();//unlock ip9212 device for others
                hardware_connected_flag = false;
                tl.LogMessage("Switch_CheckLink_async", "error:" + e.Message);
                //throw new ASCOM.NotConnectedException("Couldn't reach network server");
                tl.LogMessage("Switch_CheckLink_async", "exit on web error");
            }
        }

        private void checkLink_DownloadCompleted(Object sender, DownloadDataCompletedEventArgs e)
        {
            try
            {
                tl.LogMessage("<Semaphore", "Release");
            }
            catch { 
            // Object was disposed before download complete, so we should release all and exit
                return;
            }
            IP9212Semaphore.Release();//unlock ip9212 device for others
            tl.LogMessage("Switch_checkLink_DownloadCompleted", "http request was processed");
            if (e.Error != null)
            {
                hardware_connected_flag = false;
                tl.LogMessage("Switch_checkLink_DownloadCompleted", "error: " + e.Error.Message);
                return;
            }

            if (e.Result != null && e.Result.Length > 0)
            {
                string downloadedData = Encoding.Default.GetString(e.Result);


                if (downloadedData.IndexOf("P5") >= 0)
                {
                    hardware_connected_flag = true;
                    tl.LogMessage("Switch_checkLink_DownloadCompleted", "Downloaded data is ok");
                    lastConnectedCheck = DateTime.Now;
                    lastShutterStatusCheck = DateTime.Now;

                    //Parse input data just in case - it will be usefull
                    parseInputData(downloadedData);
                }
                else
                {
                    hardware_connected_flag = false;
                    tl.LogMessage("Switch_checkLink_DownloadCompleted", "Downloaded data error - string not found");
                }

            }
            else
            {
                tl.LogMessage("Switch_checkLink_DownloadCompleted", "bad result");
                hardware_connected_flag = false;
            }
            return;
        }

        /// <summary>
        /// Check the availability of IP server by straight read (NON ASYNC manner)
        /// </summary>  
        /// <returns>Aviability of IP server </returns> 
        public bool checkLink_forced()
        {
            tl.LogMessage("Switch_checkLink_forced", "Enter");

            //Just call getInputStatus() method. It would check and also parse input data as a side bonus :)
            getInputStatus();

            tl.LogMessage("Switch_checkLink_forced", "Exit. Returning status: " + hardware_connected_flag.ToString());
            return hardware_connected_flag;
        }


        /// <summary>
        /// Get input sensor status
        /// Notes: 
        /// 1. It is also base procedure to check if device is connected, so sync checklink call it (simply is a wrapper). But for async check it calls its own check
        /// 2. Because this procedure returns also data for shutter status check, it caches it's result, so OpenedState() and ClosedState() would reacquire this data
        /// </summary>
        /// <returns>Returns int array [0..8] with status flags of each input sensor. arr[0] is for read status (-1 for error, 1 for good read, 0 for smth else)</returns> 
        public int[] getInputStatus()
        {
            tl.LogMessage("Switch_getInputStatus", "Enter");

            input_state_arr[0] = -1;

            if (string.IsNullOrEmpty(ip_addr))
            {
                input_state_arr[0] = -1;
                hardware_connected_flag = false;

                tl.LogMessage("Switch_getInputStatus", "ERROR (ip_addr wasn't set)!");
                // report a problem with the port name
                return input_state_arr;
            }

            string siteipURL;
            siteipURL = "http://" + ip_login + ":" + ip_pass + "@" + ip_addr + ":" + ip_port + "/set.cmd?cmd=getio";
            // new style
            siteipURL = "http://" + ip_addr + ":" + ip_port + "/Set.cmd?user=" + ip_login + "+pass=" + ip_pass + "CMD=getio";

            //FOR DEBUGGING
            if (debugFlag)
            {
                siteipURL = "http://localhost/ip9212/getio.php";
            }
            tl.LogMessage("Switch_getInputStatus", "Download url:" + siteipURL);

            // Send http query
            tl.LogMessage(">Semaphore", "waitone");
            IP9212Semaphore.WaitOne(Semaphore_timeout); // lock working with IP9212
            string s = "";
            MyWebClient client = new MyWebClient();
            try
            {
                Stream data = client.OpenRead(siteipURL);
                StreamReader reader = new StreamReader(data);
                s = reader.ReadToEnd();
                data.Close();
                reader.Close();

                tl.LogMessage("Switch_getInputStatus", "Download str:" + s);

                tl.LogMessage("<Semaphore", "Release");
                IP9212Semaphore.Release();//unlock ip9212 device for others
                //wait
                //Thread.Sleep(1000);

                if (s.IndexOf("P5") >= 0)
                {
                    hardware_connected_flag = true;
                    tl.LogMessage("Switch_getInputStatus", "Downloaded data is ok");
                    lastConnectedCheck = DateTime.Now;
                    lastShutterStatusCheck= DateTime.Now;

                    //Parse input data just in case it will be usefull
                    parseInputData(s);
                }
                else
                {
                    hardware_connected_flag = false;
                    tl.LogMessage("Switch_getInputStatus", "Downloaded data error - string not found");
                }


            }
            catch (WebException e)
            {
                tl.LogMessage("<Semaphore", "Release");
                IP9212Semaphore.Release();//unlock ip9212 device for others
                input_state_arr[0] = -1;
                hardware_connected_flag = false;

                tl.LogMessage("Switch_getInputStatus", "Error:" + e.Message);
                tl.LogMessage("Switch_getInputStatus", "Exit by web error");
            }

            tl.LogMessage("Switch_getInputStatus", "Exit");
            return input_state_arr;
        }

        /// <summary>
        /// Parse input sensors string as retured by GETIO command
        /// </summary>
        /// <param name="s">string </param>
        public int[] parseInputData(string s)
        {
            tl.LogMessage("Switch_parseInputData", "Enter");
            // Parse data
            try
            {
                // Parse result string
                string[] stringSeparators = new string[] { "P5" };
                string[] iprawdata_arr = s.Split(stringSeparators, StringSplitOptions.None);

                Array.Resize(ref input_state_arr, iprawdata_arr.Length);

                //Parse an array
                for (var i = 1; i < iprawdata_arr.Length; i++)
                {
                    //Убираем запятую
                    if (iprawdata_arr[i].Length > 3)
                    {
                        iprawdata_arr[i] = iprawdata_arr[i].Substring(0, 3);
                    }
                    //Trace(iprawdata_arr[i]);

                    //Разбиваем на пары "номер порта"="значение"
                    char[] delimiterChars = { '=' };
                    string[] data_arr = iprawdata_arr[i].Split(delimiterChars);
                    //st = st + " |" + i + ' ' + data_arr[1];
                    if (data_arr.Length > 1)
                    {
                        input_state_arr[i] = Convert.ToInt16(data_arr[1]);
                        //Trace(input_state_arr[i]);
                    }
                    else
                    {
                        input_state_arr[i] = -1;
                    }
                }
                input_state_arr[0] = 1;
                tl.LogMessage("Switch_parseInputData", "Exit, data was parsed");
            }
            catch
            {
                tl.LogMessage("Switch_parseInputData", "ERROR (Exception)!");
                input_state_arr[0] = -1;
                tl.LogMessage("Switch_parseInputData", "Exit, parse error");
            }
            return input_state_arr;
        }

        /// <summary>
        /// Get output relay status
        /// </summary>
        /// <returns>Returns int array [0..8] with status flags of each realya status. arr[0] is for read status (-1 for error, 1 for good read, 0 for smth else)</returns> 
        public int[] getOutputStatus()
        {
            tl.LogMessage("Switch_getOutputStatus", "Enter");

            // get the ip9212 settings from the profile
            //readSettings();

            //return data
            int[] ipdata = new int[1] { 0 };

            if (string.IsNullOrEmpty(ip_addr))
            {
                ipdata[0] = -1;
                tl.LogMessage("Switch_getOutputStatus", "ERROR (ip_addr wasn't set)!");
                // report a problem with the port name
                ASCOM_ERROR_MESSAGE = "getOutputStatus(): no IP address was specified";
                throw new ASCOM.ValueNotSetException(ASCOM_ERROR_MESSAGE);
                //return input_state_arr;

            }
            string siteipURL;
            siteipURL = "http://" + ip_login + ":" + ip_pass + "@" + ip_addr + ":" + ip_port + "/set.cmd?cmd=getpower";
            // new style
            siteipURL = "http://" + ip_addr + ":" + ip_port + "/Set.cmd?user=" + ip_login + "+pass=" + ip_pass + "CMD=getpower";

            //FOR DEBUGGING
            if (debugFlag)
            {
                siteipURL = "http://localhost/ip9212/getpower.php";
            }
            tl.LogMessage("Switch_getOutputStatus", "Download url:" + siteipURL);


            // Send http query
            tl.LogMessage(">Semaphore", "waitone");
            IP9212Semaphore.WaitOne(Semaphore_timeout); // lock working with IP9212

            string s = "";
            MyWebClient client = new MyWebClient();
            try
            {
                Stream data = client.OpenRead(siteipURL);
                StreamReader reader = new StreamReader(data);
                s = reader.ReadToEnd();
                data.Close();
                reader.Close();

                tl.LogMessage("Switch_getOutputStatus", "Download str:" + s);

                tl.LogMessage("<Semaphore", "Release");
                IP9212Semaphore.Release();//unlock ip9212 device for others
                //wait
                //Thread.Sleep(1000);

            }
            catch (WebException e)
            {
                tl.LogMessage("<Semaphore", "Release");
                IP9212Semaphore.Release();//unlock ip9212 device for others
                ipdata[0] = -1;
                tl.LogMessage("Switch_getOutputStatus", "Error:" + e.Message);
                ASCOM_ERROR_MESSAGE = "getInputStatus(): Couldn't reach network server";
                //throw new ASCOM.NotConnectedException(ASCOM_ERROR_MESSAGE);
                //Trace("> IP9212_harware.getOutputStatus(): exit by web error");
                tl.LogMessage("Switch_getOutputStatus", "Exit by web error");
                return ipdata;
            }

            // Parse data
            try
            {
                string[] stringSeparators = new string[] { "P6" };
                string[] iprawdata_arr = s.Split(stringSeparators, StringSplitOptions.None);

                Array.Resize(ref ipdata, iprawdata_arr.Length);

                //Parse an array
                for (var i = 1; i < iprawdata_arr.Length; i++)
                {
                    //Убираем запятую
                    if (iprawdata_arr[i].Length > 3)
                    {
                        iprawdata_arr[i] = iprawdata_arr[i].Substring(0, 3);
                    }
                    //Console.WriteLine(iprawdata_arr[i]);

                    //Разбиваем на пары "номер порта"="значение"
                    char[] delimiterChars = { '=' };
                    string[] data_arr = iprawdata_arr[i].Split(delimiterChars);
                    //st = st + " |" + i + ' ' + data_arr[1];
                    if (data_arr.Length > 1)
                    {
                        ipdata[i] = Convert.ToInt16(data_arr[1]);
                        //Trace(ipdata[i]);
                    }
                    else
                    {
                        ipdata[i] = -1;
                    }
                }
                ipdata[0] = 1;
                tl.LogMessage("Switch_getOutputStatus", "Data was read");
            }
            catch
            {
                ipdata[0] = -1;
                tl.LogMessage("Switch_getOutputStatus", "ERROR (Exception)!");
                tl.LogMessage("Switch_getOutputStatus", "exit by parse error");
                return ipdata;
            }
            return ipdata;
        }


        /// <summary>
        /// Chage output relay state
        /// </summary>
        /// <param name="PortNumber">Relay port number, int [1..9]</param>
        /// <param name="PortValue">Port value [0,1]</param>
        /// <returns>Returns true in case of success</returns> 
        public bool setOutputStatus(int PortNumber, int PortValue)
        {
            tl.LogMessage("Switch_setOutputStatus", "Enter (" + PortNumber + "," + PortValue + ")");

            // get the ip9212 settings from the profile
            //readSettings();

            //return data
            bool ret = false;

            if (string.IsNullOrEmpty(ip_addr))
            {
                tl.LogMessage("Switch_setOutputStatus", "ERROR (ip_addr wasn't set)!");
                // report a problem with the port name
                ASCOM_ERROR_MESSAGE = "Switch_setOutputStatus(): no IP address was specified";
                throw new ASCOM.ValueNotSetException(ASCOM_ERROR_MESSAGE);
                //return ret;
            }
            string siteipURL = "http://" + ip_login + ":" + ip_pass + "@" + ip_addr + ":" + ip_port + "/set.cmd?cmd=setpower+P6" + PortNumber + "=" + PortValue;
            // new style
            siteipURL = "http://" + ip_addr + ":" + ip_port + "/Set.cmd?user=" + ip_login + "+pass=" + ip_pass + "CMD=setpower+P6" + PortNumber + "=" + PortValue;
            //FOR DEBUGGING
            if (debugFlag)
            {
                siteipURL = "http://localhost/ip9212/set.php?cmd=setpower+P6" + PortNumber + "=" + PortValue;
            }
            tl.LogMessage("Switch_setOutputStatus", "Download url:" + siteipURL);


            // Send http query
            tl.LogMessage(">Semaphore", "waitone");
            IP9212Semaphore.WaitOne(Semaphore_timeout); // lock working with IP9212
            string s = "";
            MyWebClient client = new MyWebClient();
            try
            {
                Stream data = client.OpenRead(siteipURL);
                StreamReader reader = new StreamReader(data);
                s = reader.ReadToEnd();
                data.Close();
                reader.Close();

                tl.LogMessage("Switch_setOutputStatus", "Download str:" + s);

                //wait
                //Thread.Sleep(1000);
                tl.LogMessage("<Semaphore", "Release");
                IP9212Semaphore.Release();//unlock ip9212 device for others

                ret = true;
            }
            catch (WebException e)
            {
                tl.LogMessage("<Semaphore", "Release");
                IP9212Semaphore.Release();//unlock ip9212 device for others
                ret = false;

                tl.LogMessage("Switch_setOutputStatus", "Error:" + e.Message);
                ASCOM_ERROR_MESSAGE = "setOutputStatus(" + PortNumber + "," + PortValue + "): Couldn't reach network server";
                //throw new ASCOM.NotConnectedException(ASCOM_ERROR_MESSAGE);
                tl.LogMessage("Switch_setOutputStatus", "Exit by web error");
                return ret;
                // report a problem with the port name (never get there)
            }
            // Parse data
            // not implemented yet

            return ret;
        }

        /// <summary>
        /// Press switch button to open/close roof
        /// </summary>
        /// <returns>Returns true in case of success</returns> 
        //press switch
        public bool pressRoofSwitch()
        {
            tl.LogMessage("Switch_pressRoofSwitch", "Enter");

            //Get config data
            int int_switch_port_state_type = (switch_port_state_type ? 0 : 1);
            int int_inverted_switch_port_state_type = (switch_port_state_type ? 1 : 0);

            //read output states
            int[] outStates = getOutputStatus();
            int curPortState = outStates[switch_roof_port];
            tl.LogMessage("Switch_pressRoofSwitch", "Using port " + switch_roof_port.ToString() + ", type: " + switch_port_state_type.ToString());

            //check - what is the state of switch port?
            if (outStates[switch_roof_port] != int_switch_port_state_type)
            {
                //return to normal value
                tl.LogMessage("Switch_pressRoofSwitch", "first need to return switch to normal state");
                setOutputStatus(switch_roof_port, int_switch_port_state_type);
            }

            //press switch
            tl.LogMessage("Switch_pressRoofSwitch", "Pressing");
            setOutputStatus(switch_roof_port, int_inverted_switch_port_state_type);

            //wait
            Thread.Sleep(1000);

            //release switch
            tl.LogMessage("Switch_pressRoofSwitch", "Releasing");
            setOutputStatus(switch_roof_port, int_switch_port_state_type);

            tl.LogMessage("Switch_pressRoofSwitch", "Exit");
            return true;
        }

        /// <summary>
        /// return true if OPENNED STATE SENSOR signaling (using cache)
        /// </summary>
        /// <returns>Returns true in case of opened state signaling, false otherwise</returns> 
        public bool OpenedSensorState()
        {
            tl.LogMessage("Switch_OpenedSensorState", "Enter");

            //read OPENED_PORT STATE TYPE value
            int int_opened_port_state_type;
            int_opened_port_state_type = (opened_port_state_type ? 1 : 0);

            // READ CURRENT INPUT STATE IF IT WASN'T READ YET
            if (input_state_arr[0] <= 0)
            {
                tl.LogMessage("Switch_OpenedSensorState", "Unidentified input status array, re-reading states");
                getInputStatus();
            }

            // READ CURRENT INPUT STATE IF IT WASN'T READ YET
            //if shutter in moving state - reduce check interval
            uint checkInterval = 0;
            if ((!opened_shutter_flag && !closed_shutter_flag))
            {
                checkInterval = CACHE_SHUTTERSTATUS_INTERVAL_REDUCED;
                tl.LogMessage("Switch_OpenedSensorState", "Shutter in medium position, using reduced caching interval (" + CACHE_SHUTTERSTATUS_INTERVAL_REDUCED + ")");
            }
            else
            {
                checkInterval = CACHE_SHUTTERSTATUS_INTERVAL_NORMAL;
                tl.LogMessage("Switch_OpenedSensorState", "Using normal caching interval (" + CACHE_SHUTTERSTATUS_INTERVAL_NORMAL + ")");
            }

            //Measure how much time have passed since last HARDWARE measure
            TimeSpan passed = DateTime.Now - lastShutterStatusCheck;

            if (passed.TotalSeconds > checkInterval)
            {
                tl.LogMessage("Switch_OpenedSensorState", "Cache expired [" + passed.TotalSeconds + " sec passed], re-reading states");
                // Read input status
                getInputStatus();

                lastShutterStatusCheck = DateTime.Now;
            }
            else
            {
                tl.LogMessage("Switch_OpenedSensorState", "Using cached inputsensor values [" + passed.TotalSeconds + " sec passed]");
            }


            //calculate state
            bool boolState;
            if (input_state_arr[opened_sensor_port] == int_opened_port_state_type)
            {
                boolState = true;
            }
            else
            {
                boolState = false;
            }

            tl.LogMessage("Switch_OpenedSensorState", "Exix. Status: " + boolState);
            return boolState;
        }

        /// <summary>
        /// return true if CLOSED STATE sensor signaling, cacheable
        /// </summary>
        /// <returns>Returns true in case of closed state signaling, false otherwise</returns> 
        public bool ClosedSensorState()
        {
            tl.LogMessage("Switch_ClosedSensorState", "Enter");

            //read closED_PORT STATE TYPE value
            int int_closed_port_state_type = (closed_port_state_type ? 1 : 0);

            // READ CURRENT INPUT STATE
            if (input_state_arr[0] <= 0)
            {
                tl.LogMessage("Switch_ClosedSensorState", "Unidentified input status array, re-reading states");
                getInputStatus();
            }

            // READ CURRENT INPUT STATE IF IT WASN'T READ YET
            //if shutter in moving state - reduce check interval
            uint checkInterval = 0;
            if ((!opened_shutter_flag && !closed_shutter_flag))
            {
                checkInterval = CACHE_SHUTTERSTATUS_INTERVAL_REDUCED;
                tl.LogMessage("Switch_ClosedSensorState", "Shutter in medium position, using reduced caching interval (" + CACHE_SHUTTERSTATUS_INTERVAL_REDUCED + ")");
            }
            else
            {
                checkInterval = CACHE_SHUTTERSTATUS_INTERVAL_NORMAL;
                tl.LogMessage("Switch_ClosedSensorState", "Using normal caching interval (" + CACHE_SHUTTERSTATUS_INTERVAL_NORMAL + ")");
            }

            //Measure how much time have passed since last HARDWARE measure
            TimeSpan passed = DateTime.Now - lastShutterStatusCheck;

            if (passed.TotalSeconds > checkInterval)
            {
                tl.LogMessage("Switch_ClosedSensorState", "Cache expired [" + passed.TotalSeconds + " sec passed], re-reading states");
                // Read input status
                getInputStatus();

                lastShutterStatusCheck = DateTime.Now;
            }
            else
            {
                tl.LogMessage("Switch_ClosedSensorState", "Using cached inputsensor values [" + passed.TotalSeconds + " sec passed]");
            }


            //calculate state
            bool boolState;
            if (input_state_arr[closed_sensor_port] == int_closed_port_state_type)
            {
                boolState = true;
            }
            else
            {
                boolState = false;
            }

            tl.LogMessage("Switch_ClosedSensorState", "Exix. Status: " + boolState);
            return boolState;
        }

        /// <summary>
        /// return true if OPENNED STATE SENSOR signaling, forcing reading current input statused 
        /// </summary>
        /// <returns>Returns true in case of opened state signaling, false otherwise</returns> 
        public bool OpenedSensorState_forced()
        {
            tl.LogMessage("Switch_OpenedSensorState_forced", "Enter");
            getInputStatus();
            bool retStatus = OpenedSensorState();

            tl.LogMessage("Switch_OpenedSensorState_forced", "Exit, status: " + retStatus);

            return retStatus;
        }

        /// <summary>
        /// return true if CLOSED STATE sensor signaling, forcing reading current input statused
        /// </summary>
        /// <returns>Returns true in case of closed state signaling, false otherwise</returns> 
        public bool ClosedSensorState_forced()
        {
            tl.LogMessage("Switch_ClosedSensorState_forced", "Enter");
            getInputStatus();
            bool retStatus = ClosedSensorState();

            tl.LogMessage("Switch_ClosedSensorState_forced", "Exit, status: " + retStatus);

            return retStatus;

        }

        /// <summary>
        /// Read settings from ASCOM profile storage
        /// </summary>
        public void readSettings___()
        {
            tl.LogMessage("Switch_readSettings", "Enter");
            using (ASCOM.Utilities.Profile p = new Profile())
            {
                //System.Collections.ArrayList T = p.RegisteredDeviceTypes;
                p.DeviceType = "Switch";

                //General settings
                try
                {
                    ip_addr = p.GetValue(IP9212_switch_id, ip_addr_profilename, string.Empty, ip_addr_default);
                }
                catch (Exception e)
                {
                    //p.WriteValue(driverID, ip_addr_profilename, ip_addr_default);
                    ip_addr = ip_addr_default;
                    tl.LogMessage("readSettings", "Wrong input string for [ip_addr]: [" + e.Message + "]");
                }
                try
                {
                    ip_port = p.GetValue(IP9212_switch_id, ip_port_profilename, string.Empty, ip_port_default);
                }
                catch (Exception e)
                {
                    //p.WriteValue(driverID, ip_port_profilename, ip_port_default);
                    ip_port = ip_port_default;
                    tl.LogMessage("readSettings", "Wrong input string for [ip_port]: [" + e.Message + "]");
                }
                try
                {
                    ip_login = p.GetValue(IP9212_switch_id, ip_login_profilename, string.Empty, ip_login_default);
                }
                catch (Exception e)
                {
                    //p.WriteValue(driverID, ip_login_profilename, ip_login_default);
                    ip_login = ip_login_default;
                    tl.LogMessage("readSettings", "Wrong input string for [ip_login]: [" + e.Message + "]");
                }

                try
                {
                    ip_pass = p.GetValue(IP9212_switch_id, ip_pass_profilename, string.Empty, ip_pass_default);
                }
                catch (Exception e)
                {
                    //p.WriteValue(driverID, ip_pass_profilename, ip_pass_default);
                    ip_pass = ip_pass_default;
                    tl.LogMessage("readSettings", "Wrong input string for [ip_pass]: [" + e.Message + "]");
                }



                try
                {
                    switch_roof_port = Convert.ToInt16(p.GetValue(IP9212_switch_id, switch_port_profilename, string.Empty, switch_port_default));
                }
                catch (Exception e)
                {
                    switch_roof_port = Convert.ToInt16(switch_port_default);
                    tl.LogMessage("Switch_readSettings", "Input string [switch_roof_port] is not a sequence of digits [" + e.Message + "]");
                    //ASCOM_ERROR_MESSAGE = "Switch_readSettings(): [switch_roof_port] is not a numeric value";
                    //throw new ASCOM.InvalidValueException(ASCOM_ERROR_MESSAGE);
                }

                try
                {
                    opened_sensor_port = Convert.ToInt16(p.GetValue(IP9212_switch_id, opened_port_profilename, string.Empty, opened_port_default));
                }
                catch (Exception e)
                {
                    opened_sensor_port = Convert.ToInt16(opened_port_default);
                    tl.LogMessage("Switch_readSettings", "Input string [opened_sensor_port] is not a sequence of digits [" + e.Message + "]");
                    //ASCOM_ERROR_MESSAGE = "Switch_readSettings(): [opened_sensor_port] is not a numeric value";
                    //throw new ASCOM.InvalidValueException(ASCOM_ERROR_MESSAGE);
                }
                try
                {
                    closed_sensor_port = Convert.ToInt16(p.GetValue(IP9212_switch_id, closed_port_profilename, string.Empty, closed_port_default));
                }
                catch (Exception e)
                {
                    closed_sensor_port = Convert.ToInt16(closed_port_default); 
                    tl.LogMessage("Switch_readSettings", "Input string [closed_sensor_port] is not a sequence of digits [" + e.Message + "]");
                    //ASCOM_ERROR_MESSAGE = "Switch_readSettings(): [closed_sensor_port] is not a numeric value";
                    //throw new ASCOM.InvalidValueException(ASCOM_ERROR_MESSAGE);
                }

                try
                {
                    switch_port_state_type = Convert.ToBoolean(p.GetValue(IP9212_switch_id, switch_port_state_type_profilename, string.Empty, switch_port_state_type_default));
                }
                catch (Exception e)
                {
                    switch_port_state_type = Convert.ToBoolean(switch_port_state_type_default); 
                    tl.LogMessage("Switch_readSettings", "Input string [switch_port_state_type] is not a boolean value [" + e.Message + "]");
                    //ASCOM_ERROR_MESSAGE = "Switch_readSettings(): [switch_port_state_type] is not a boolean value";
                    //throw new ASCOM.InvalidValueException(ASCOM_ERROR_MESSAGE);
                }
                try
                {
                    opened_port_state_type = Convert.ToBoolean(p.GetValue(IP9212_switch_id, opened_port_state_type_profilename, string.Empty, opened_port_state_type_default));
                }
                catch (Exception e)
                {
                    opened_port_state_type = Convert.ToBoolean(opened_port_state_type_default);
                    tl.LogMessage("Switch_readSettings", "Input string [opened_port_state_type] is not a boolean value [" + e.Message + "]");
                    //ASCOM_ERROR_MESSAGE = "Switch_readSettings(): [opened_port_state_type] is not a boolean value";
                    //throw new ASCOM.InvalidValueException(ASCOM_ERROR_MESSAGE);
                }
                try
                {
                    closed_port_state_type = Convert.ToBoolean(p.GetValue(IP9212_switch_id, closed_port_state_type_profilename, string.Empty, closed_port_state_type_default));
                }
                catch (Exception e)
                {
                    closed_port_state_type = Convert.ToBoolean(closed_port_state_type_default);
                    tl.LogMessage("Switch_readSettings", "Input string [closed_port_state_type] is not a boolean value [" + e.Message + "]");
                    //ASCOM_ERROR_MESSAGE = "Switch_readSettings(): [closed_port_state_type] is not a boolean value";
                    //throw new ASCOM.InvalidValueException(ASCOM_ERROR_MESSAGE);
                }

                try
                {
                    telescope_power_port = Convert.ToInt16(p.GetValue(IP9212_switch_id, telescope_power_port_profilename, string.Empty, telescope_power_port_default));
                }
                catch (Exception e)
                {
                    telescope_power_port = Convert.ToInt16(telescope_power_port_default); 
                    tl.LogMessage("Switch_readSettings", "Input string [telescope_power_port] is not a sequence of digits [" + e.Message + "]");
                    //ASCOM_ERROR_MESSAGE = "Switch_readSettings(): [telescope_power_port] is not a numeric value";
                    //throw new ASCOM.InvalidValueException(ASCOM_ERROR_MESSAGE);
                }
                try
                {
                    focuser_power_port = Convert.ToInt16(p.GetValue(IP9212_switch_id, focuser_power_port_profilename, string.Empty, focuser_power_port_default));
                }
                catch (Exception e)
                {
                    focuser_power_port = Convert.ToInt16(focuser_power_port_default); 
                    tl.LogMessage("Switch_readSettings", "Input string [focuser_power_port] is not a sequence of digits [" + e.Message + "]");
                    //ASCOM_ERROR_MESSAGE = "Switch_readSettings(): [focuser_power_port] is not a numeric value";
                    //throw new ASCOM.InvalidValueException(ASCOM_ERROR_MESSAGE);
                }
                try
                {
                    heating_port = Convert.ToInt16(p.GetValue(IP9212_switch_id, heating_port_profilename, string.Empty, heating_port_default));
                }
                catch (Exception e)
                {
                    heating_port = Convert.ToInt16(heating_port_default); 
                    tl.LogMessage("Switch_readSettings", "Input string [heating_port] is not a sequence of digits [" + e.Message + "]");
                    //ASCOM_ERROR_MESSAGE = "Switch_readSettings(): [heating_port] is not a numeric value";
                    //if (debugFlag) { new ASCOM.InvalidValueException(ASCOM_ERROR_MESSAGE); }
                }
                try
                {
                    roofpower_port = Convert.ToInt16(p.GetValue(IP9212_switch_id, roof_power_port_profilename, string.Empty, roof_power_port_default));
                }
                catch (Exception e)
                {
                    roofpower_port = Convert.ToInt16(roof_power_port_default); 
                    tl.LogMessage("Switch_readSettings", "Input string [roofpower_port] is not a sequence of digits [" + e.Message + "]");
                    //ASCOM_ERROR_MESSAGE = "Switch_readSettings(): [roofpower_port] is not a numeric value";
                    //throw new ASCOM.InvalidValueException(ASCOM_ERROR_MESSAGE);
                }

                try
                {
                    telescope_power_port_state_type = Convert.ToBoolean(p.GetValue(IP9212_switch_id, telescope_power_port_state_type_profilename, string.Empty, telescope_power_port_state_type_default));
                }
                catch (Exception e)
                {
                    telescope_power_port_state_type = Convert.ToBoolean(telescope_power_port_state_type_default);
                    tl.LogMessage("Switch_readSettings", "Input string [telescope_power_port_state_type] is not a boolean value [" + e.Message + "]");
                    //ASCOM_ERROR_MESSAGE = "Switch_readSettings(): [telescope_power_port_state_type] is not a boolean value";
                    //throw new ASCOM.InvalidValueException(ASCOM_ERROR_MESSAGE);
                }
                try
                {
                    focuser_power_port_state_type = Convert.ToBoolean(p.GetValue(IP9212_switch_id, focuser_power_port_state_type_profilename, string.Empty, focuser_power_port_state_type_default));
                }
                catch (Exception e)
                {
                    focuser_power_port_state_type = Convert.ToBoolean(focuser_power_port_state_type_default);
                    tl.LogMessage("Switch_readSettings", "Input string [focuser_power_port_state_type] is not a boolean value [" + e.Message + "]");
                    //ASCOM_ERROR_MESSAGE = "Switch_readSettings(): [focuser_power_port_state_type] is not a boolean value";
                    //throw new ASCOM.InvalidValueException(ASCOM_ERROR_MESSAGE);
                }
                try
                {
                    heating_port_state_type = Convert.ToBoolean(p.GetValue(IP9212_switch_id, heating_port_state_type_profilename, string.Empty, heating_port_state_type_default));
                }
                catch (Exception e)
                {
                    heating_port_state_type = Convert.ToBoolean(heating_port_state_type_default);
                    tl.LogMessage("Switch_readSettings", "Input string [heating_port_state_type] is not a boolean value [" + e.Message + "]");
                    //ASCOM_ERROR_MESSAGE = "Switch_readSettings(): [heating_port_state_type] is not a boolean value";
                    //throw new ASCOM.InvalidValueException(ASCOM_ERROR_MESSAGE);
                }
                try
                {
                    roofpower_port_state_type = Convert.ToBoolean(p.GetValue(IP9212_switch_id, roof_power_port_state_type_profilename, string.Empty, roof_power_port_state_type_default));
                }
                catch (Exception e)
                {
                    roofpower_port_state_type = Convert.ToBoolean(roof_power_port_state_type_default);
                    tl.LogMessage("Switch_readSettings", "Input string [roofpower_port_state_type] is not a boolean value [" + e.Message + "]");
                    //ASCOM_ERROR_MESSAGE = "Switch_readSettings(): [roofpower_port_state_type] is not a boolean value";
                    //throw new ASCOM.InvalidValueException(ASCOM_ERROR_MESSAGE);
                }

                try
                {
                    traceState = Convert.ToBoolean(p.GetValue(IP9212_switch_id, traceStateProfileName, string.Empty, traceStateDefault));
                }
                catch (Exception e)
                {
                    traceState = Convert.ToBoolean(traceStateDefault);
                    tl.LogMessage("Switch_readSettings", "Input string [traceState] is not a boolean value [" + e.Message + "]");
                    //ASCOM_ERROR_MESSAGE = "Switch_readSettings(): [traceState] is not a boolean value";
                    //throw new ASCOM.InvalidValueException(ASCOM_ERROR_MESSAGE);
                }

            }
            tl.LogMessage("Switch_readSettings", "Exit");
        }

        /// <summary>
        /// Write settings to ASCOM profile storage
        /// </summary>
        public void writeSettings___()
        {
            tl.LogMessage("Switch_writeSettings", "Enter");
            using (Profile p = new Profile())
            {
                p.DeviceType = "Switch";

                p.WriteValue(IP9212_switch_id, ip_addr_profilename, ip_addr);
                p.WriteValue(IP9212_switch_id, ip_port_profilename, ip_port);
                p.WriteValue(IP9212_switch_id, ip_login_profilename, ip_login);
                p.WriteValue(IP9212_switch_id, ip_pass_profilename, ip_pass);

                p.WriteValue(IP9212_switch_id, switch_port_profilename, switch_roof_port.ToString());
                p.WriteValue(IP9212_switch_id, opened_port_profilename, opened_sensor_port.ToString());
                p.WriteValue(IP9212_switch_id, closed_port_profilename, closed_sensor_port.ToString());

                p.WriteValue(IP9212_switch_id, telescope_power_port_profilename, telescope_power_port.ToString());
                p.WriteValue(IP9212_switch_id, focuser_power_port_profilename, focuser_power_port.ToString());
                p.WriteValue(IP9212_switch_id, heating_port_profilename, heating_port.ToString());
                p.WriteValue(IP9212_switch_id, roof_power_port_profilename, roofpower_port.ToString());

                p.WriteValue(IP9212_switch_id, telescope_power_port_state_type_profilename, telescope_power_port_state_type.ToString());
                p.WriteValue(IP9212_switch_id, focuser_power_port_state_type_profilename, focuser_power_port_state_type.ToString());
                p.WriteValue(IP9212_switch_id, heating_port_state_type_profilename, heating_port_state_type.ToString());
                p.WriteValue(IP9212_switch_id, roof_power_port_state_type_profilename, roofpower_port_state_type.ToString());

                p.WriteValue(IP9212_switch_id, traceStateProfileName, traceState.ToString());
            }
            tl.LogMessage("Switch_writeSettings", "Exit");
        }

        /// <summary>
        /// Registering switch as ASCOM device
        /// </summary>
        public void RegisterSettings___()
        {
            using (var P = new ASCOM.Utilities.Profile())
            {
                P.DeviceType = "Switch";
                P.Register(IP9212_switch_id, IP9212_switch_description);
            }
        }

        /// <summary>
        /// Tracing (logging) - 3 overloaded method
        /// </summary>
        public void Trace__(string st)
        {
            Console.WriteLine(st);
            try
            {
                using (StreamWriter outfile = File.AppendText("d:/ascom_ip9212_logfile.log"))
                {
                    outfile.WriteLine("{0} {1}: {2}", DateTime.Now.ToLongDateString(), DateTime.Now.ToLongTimeString(), st);
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("Write trace file error! [" + e.Message + "]");
            }
        }

        public void Trace__(int st)
        {
            Console.WriteLine(st);
        }

        public void Trace__(string st, int[] arr_int)
        {
            string st_out = st;
            foreach (int el in arr_int)
            {
                st_out = st_out + el + " ";
            }

            Console.WriteLine(st_out);

            try
            {
                using (StreamWriter outfile = File.AppendText("d:/ascom_ip9212_logfile.log"))
                {
                    outfile.WriteLine("{0} {1}: {2}", DateTime.Now.ToLongDateString(), DateTime.Now.ToLongTimeString(), st_out);
                }

            }
            catch (IOException e)
            {
                Console.WriteLine("Write trace file error! [" + e.Message + "]");
            }
        }


        /// <summary>
        /// Standart dispose method
        /// </summary>
        public void Dispose()
        {
            tl.Dispose();
            tl = null;

            IP9212Semaphore.Dispose();
            IP9212Semaphore = null;
        }

        #region DriverInformation
        /// <summary>
        /// Some properties for displaying driver version
        /// </summary>
        public string Description
        {
            get
            {
                tl.LogMessage("Switch_Description Get", IP9212_switch_description);
                return IP9212_switch_description;
            }
        }

        public static string DriverInfo
        {
            get
            {
                Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                string driverInfo = IP9212_switch_description + ". Version: " + DriverVersion;
                tl.LogMessage("Switch_DriverInfo Get", driverInfo);
                return driverInfo;
            }
        }

        public static string DriverVersion
        {
            get
            {
                Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                string driverVersion = String.Format(CultureInfo.InvariantCulture, "{0}.{1}", version.Major, version.Minor);
                tl.LogMessage("Switch_DriverVersion Get", driverVersion);
                return driverVersion;
            }
        }

        public string Name
        {
            get
            {
                tl.LogMessage("Switch_Name Get", IP9212_switch_description_short);
                return IP9212_switch_description_short;
            }
        }
        
        #endregion DriverInformation


    }
}
