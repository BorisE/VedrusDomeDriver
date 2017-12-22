//tabs=4
// --------------------------------------------------------------------------------
//
// ASCOM Dome driver for roll-off roof on Vedrus observatory (http://astrohostel.ru)
//
// Description:	ASCOM Driver for roll-off roof controling by Orange Pi on Astrohostel observatory
//				It receives commands by http protocol commands
//
// Implements:	ASCOM Dome interface version: 2
// Author:		(XXX) Boris Emchenko <www.astromania.info>
//
// Edit Log:
//
// Date			Who	Vers	Description
// -----------	---	-----	-------------------------------------------------------
// 20-08-2016	XXX	3.0.1	caching optimization +minor changes
// 22-12-2017	XXX	0.0.1	initial alpha version based on IP9212 driver version

//
#define Dome

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;

using ASCOM;
using ASCOM.Utilities;
using ASCOM.DeviceInterface;
using System.Globalization;
using System.Collections;


namespace ASCOM.Vedrus_rolloffroof
{
    //
    // Your driver's DeviceID is ASCOM.IP9212_rolloffroof2.Dome
    //
    // The Guid attribute sets the CLSID for ASCOM.IP9212_rolloffroof2.Dome
    // The ClassInterface/None addribute prevents an empty interface called
    // _IP9212_rolloffroof2 from being created and used as the [default] interface
    //
    // TODO Replace the not implemented exceptions with code to implement the function or
    // throw the appropriate ASCOM exception.
    //

    /// <summary>
    /// ASCOM Dome Driver for Vedrus_rolloffroof.
    /// </summary>
    [Guid("C9ACEBB9-0391-4EB6-9D15-5483FDE4F205")]
    [ClassInterface(ClassInterfaceType.None)]
    public class Dome : IDomeV2
    {
        /// <summary>
        /// ASCOM DeviceID (COM ProgID) for this driver.
        /// The DeviceID is used by ASCOM applications to load the driver at runtime.
        /// </summary>
        internal static string driverID = "ASCOM.Vedrus_rolloffroof.Dome";
        /// <summary>
        /// Driver description that displays in the ASCOM Chooser.
        /// </summary>
        private static string driverDescription = "ASCOM Dome driver for roll-off roof controlled by Aviosys IP9212. Written by Boris Emchenko http://astromania.info";
        private static string driverDescriptionShort = "Roll-off roof on IP9212v3";
        private static string driverVersion = "3.0.1";

        internal static string traceStateProfileName = "Trace Level";
        internal static string traceStateDefault = "true";

        /// <summary>
        /// Settings fields
        /// </summary>
        //public string ip_addr, ip_port, ip_login, ip_pass;
        //public string switch_port, opened_port, closed_port;
        //public string switch_port_state_type, opened_port_state_type, closed_port_state_type;
        internal static bool traceState;

        /// <summary>
        /// Private variable to hold the connected state
        /// </summary>
        private bool connectedState;

        /// <summary>
        ///class that is linked with IP9212 hardware
        /// </summary>
        private Hardware_class Hardware;

        public static DateTime EXPIRED_CACHE = new DateTime(2010, 05, 12, 13, 15, 00); //CONSTANT FOR MARKING AN OLD TIME

        //Previos shutter states
        private ShutterState prev_shutter_state;
        bool last_OpenedState; // last measured value for opened sensor
        bool last_ClosedState; // last measured value closed sensor

        //Caching last shutter status
        private DateTime lastShutterStatusCheck = EXPIRED_CACHE; //when was the last hardware checking provided for shutter state 
        //int SHUTTERSTATUS_CHECK_INTERVAL_NORMAL = 10; //how often to chech true shutter status (in seconds) for regular cases
        //int SHUTTERSTATUS_CHECK_INTERVAL_REDUCED = 2;//how often to chech true shutter status (in seconds) when shutter is moving

        //error message
        private string ERROR_MESSAGE = "";
        
        /// <summary>
        /// Private variable to hold an ASCOM Utilities object
        /// </summary>
        //private Util utilities;

        /// <summary>
        /// Private variable to hold an ASCOM AstroUtilities object to provide the Range method
        /// </summary>
        //private AstroUtils astroUtilities;

        /// <summary>
        /// Private variable to hold the trace logger object (creates a diagnostic log file with information that you specify)
        /// </summary>
        internal TraceLogger tl;

        /// <summary>
        /// Initializes a new instance of the <see cref="Vedrus_rolloffroof"/> class.
        /// Must be public for COM registration.
        /// </summary>
        public Dome()
        {
            tl = new TraceLogger("", "IP9212_rolloffroof2");
            #if DEBUG
            tl.Enabled = true; //at least for debugging - log will be always created no matter the value of traceState
            tl.LogMessage("Dome", "Init traceloger (debug mode)");
            #endif

            Hardware = new Hardware_class(this);

            ReadProfile(); // Read device configuration from the ASCOM Profile store

            //set the correct logger state
            tl.Enabled = traceState;
            tl.LogMessage("Dome", "Starting initialisation");

            connectedState = false; // Initialise connected to false
         
            //utilities = new Util(); //Initialise util object
            //astroUtilities = new AstroUtils(); // Initialise astro utilities object

            tl.LogMessage("Dome", "Completed initialisation");
        }


        //
        // PUBLIC COM INTERFACE IDomeV2 IMPLEMENTATION
        //

        #region Common properties and methods.

        /// <summary>
        /// Displays the Setup Dialog form.
        /// If the user clicks the OK button to dismiss the form, then
        /// the new settings are s  aved, otherwise the old values are reloaded.
        /// THIS IS THE ONLY PLACE WHERE SHOWING USER INTERFACE IS ALLOWED!
        /// </summary>
        public void SetupDialog()
        {
            // consider only showing the setup dialog if not connected
            // or call a different dialog if connected
            if (IsConnected())
            {
                System.Windows.Forms.MessageBox.Show("Already connected, disconnect to modify settings");
            }
            else
            {
                using (SetupDialogForm F = new SetupDialogForm(this,Hardware))
                {
                    var result = F.ShowDialog();
                    if (result == System.Windows.Forms.DialogResult.OK)
                    {
                        WriteProfile(); // Persist device configuration values to the ASCOM Profile store
                    }
                }
            }
            //using (SetupDialogForm F = new SetupDialogForm())
            //{
            //    var result = F.ShowDialog();
            //    if (result == System.Windows.Forms.DialogResult.OK)
            //    {
            //        WriteProfile(); // Persist device configuration values to the ASCOM Profile store
            //    }
            //}
        }

        public ArrayList SupportedActions
        {
            get
            {
                tl.LogMessage("SupportedActions Get", "Returning empty arraylist");
                return new ArrayList() { "IPAddress", "GetCacheParameter", "GetTimeout" };
            }
        }

        public string Action(string actionName, string actionParameters)
        {
            // Get device IP address
            if (actionName == "IPAddress")
            {
                return Hardware_class.ip_addr;
            }
            // Get cache settings
            else if (actionName == "GetCacheParameter")
            {
                if (actionParameters == "CacheCheckConnection")
                {
                    return Hardware_class.CACHE_CHECKCONNECTED_INTERVAL.ToString();
                }
                else if (actionParameters == "CacheSensorState")
                {
                    return Hardware_class.CACHE_SHUTTERSTATUS_INTERVAL_NORMAL.ToString();
                }
                else if (actionParameters == "CacheSensorState_reduced")
                {
                    return Hardware_class.CACHE_SHUTTERSTATUS_INTERVAL_REDUCED.ToString();
                }
                else
                {
                    return "";
                }
            }
            // Get web timeout settings
            else if (actionName == "GetTimeout")
            {
                return MyWebClient.NETWORK_TIMEOUT.ToString();
            }
            else
            {
                throw new ASCOM.ActionNotImplementedException("Action " + actionName + " is not implemented by this driver");
            }

        }

        public void CommandBlind(string command, bool raw)
        {
            CheckConnected("CommandBlind");
            // Call CommandString and return as soon as it finishes
            this.CommandString(command, raw);
            // or
            throw new ASCOM.MethodNotImplementedException("CommandBlind");
        }

        public bool CommandBool(string command, bool raw)
        {
            CheckConnected("CommandBool");
            string ret = CommandString(command, raw);
            // TODO decode the return string and return true or false
            // or
            throw new ASCOM.MethodNotImplementedException("CommandBool");
        }

        public string CommandString(string command, bool raw)
        {
            CheckConnected("CommandString");
            // it's a good idea to put all the low level communication with the device here,
            // then all communication calls this function
            // you need something to ensure that only one command is in progress at a time

            throw new ASCOM.MethodNotImplementedException("CommandString for [" + command + ", " + raw + "]");
        }

        public void Dispose()
        {
            // Clean up the tracelogger and util objects
            tl.Enabled = false;
            tl.Dispose();
            tl = null;
            //utilities.Dispose();
            //utilities = null;
            //astroUtilities.Dispose();
            //astroUtilities = null;
            Hardware.Dispose();
            Hardware = null;

        }

        public bool Connected
        {
            get
            {
                tl.LogMessage("Connected(Get)","Enter");
                bool isConnected_res = IsConnected();
                tl.LogMessage("Connected(Get)", "Exit. Returning status: " + isConnected_res.ToString());
                return isConnected_res;
            }
            set
            {
                tl.LogMessage("Connected(Set)", "Enter. Should set connected = "+value.ToString());

                //Special case for MaximDL - finish with exception to display message
                if (!value && (ERROR_MESSAGE != "" || Hardware.ASCOM_ERROR_MESSAGE != ""))
                {
                    if (ERROR_MESSAGE == "" && Hardware.ASCOM_ERROR_MESSAGE != "") ERROR_MESSAGE = Hardware.ASCOM_ERROR_MESSAGE;
                    tl.LogMessage("Connected(Set)", "Throwing exception for MaximDL with message [" + ERROR_MESSAGE + "]");
                    throw new ASCOM.DriverException(ERROR_MESSAGE);
                }

                tl.LogMessage("Connected(Set)", "Check current connection status");
                bool curState = IsConnected(true);

                if (value && curState)
                {
                    tl.LogMessage("Connected(Set)", "Exit. Already connected");
                    return;
                }
                else if (!value && !curState)
                {
                    tl.LogMessage("Connected(Set)", "Exit. Already disconnected");
                    return;
                }
                else
                {
                    tl.LogMessage("Connected(Set)", "Connection status different, proceeding further");
                }

                if (value)
                {
                    tl.LogMessage("Connected(Set)", "Connecting to IP9212...");

                    Hardware.Connect();
                    connectedState = Hardware.hardware_connected_flag;

                    if (connectedState == false)
                    {
                        //if driver couldn't connect to ip9212 then raise an exception. 
                        // In case of using with MaximDL it willn't display it but instead try to set connection to false. 
                        // So I put there an workaround...
                        ERROR_MESSAGE = "Couldn't connect to IP9212 control device on [" + Hardware_class.ip_addr + "]";
                        throw new ASCOM.DriverException(ERROR_MESSAGE);
                    }
                }
                else
                {
                    tl.LogMessage("Connected(Set)", "Disconnecting from IP9212...");

                    Hardware.Disconnect();
                    connectedState = Hardware.hardware_connected_flag;
                }
            }
        }

        public string Description
        {
            get
            {
                tl.LogMessage("Description Get", driverDescription);
                return driverDescription;
            }
        }

        public string DriverInfo
        {
            get
            {
                string driverInfo = driverDescription + ". Version: " + DriverVersion;
                tl.LogMessage("DriverInfo Get", driverInfo);
                return driverInfo;
            }
        }

        public string DriverVersion
        {
            get
            {
                //Working olny if Assembly Version set to 6xxx. So specify manual settings...
                //Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                //string driverVersion = String.Format(CultureInfo.InvariantCulture, "{0}.{1}", version.Major, version.Minor);
                tl.LogMessage("DriverVersion Get", driverVersion);
                return driverVersion;
            }
        }

        public short InterfaceVersion
        {
            // set by the driver wizard
            get
            {
                tl.LogMessage("InterfaceVersion Get", "2");
                return Convert.ToInt16("2");
            }
        }

        public string Name
        {
            get
            {
                tl.LogMessage("Name Get", driverDescriptionShort);
                return driverDescriptionShort;
            }
        }

        #endregion

        #region IDome Implementation

        private bool domeShutterState = false; // Variable to hold the open/closed status of the shutter, true = Open

        public void AbortSlew()
        {
            // This is a mandatory parameter but we have no action to take in this simple driver
            tl.LogMessage("AbortSlew", "Completed");
        }

        public double Altitude
        {
            get
            {
                tl.LogMessage("Altitude Get", "Not implemented");
                throw new ASCOM.PropertyNotImplementedException("Altitude", false);
            }
        }

        public bool AtHome
        {
            get
            {
                tl.LogMessage("AtHome Get", "Not implemented");
                throw new ASCOM.PropertyNotImplementedException("AtHome", false);
            }
        }

        public bool AtPark
        {
            get
            {
                tl.LogMessage("AtPark Get", "Not implemented");
                throw new ASCOM.PropertyNotImplementedException("AtPark", false);
            }
        }

        public double Azimuth
        {
            get
            {
                tl.LogMessage("Azimuth Get", "Not implemented");
                throw new ASCOM.PropertyNotImplementedException("Azimuth", false);
            }
        }

        public bool CanFindHome
        {
            get
            {
                tl.LogMessage("CanFindHome Get", false.ToString());
                return false;
            }
        }

        public bool CanPark
        {
            get
            {
                tl.LogMessage("CanPark Get", false.ToString());
                return false;
            }
        }

        public bool CanSetAltitude
        {
            get
            {
                tl.LogMessage("CanSetAltitude Get", false.ToString());
                return false;
            }
        }

        public bool CanSetAzimuth
        {
            get
            {
                tl.LogMessage("CanSetAzimuth Get", false.ToString());
                return false;
            }
        }

        public bool CanSetPark
        {
            get
            {
                tl.LogMessage("CanSetPark Get", false.ToString());
                return false;
            }
        }

        public bool CanSetShutter
        {
            get
            {
                tl.LogMessage("CanSetShutter Get", true.ToString());
                return true;
            }
        }

        public bool CanSlave
        {
            get
            {
                tl.LogMessage("CanSlave Get", false.ToString());
                return false;
            }
        }

        public bool CanSyncAzimuth
        {
            get
            {
                tl.LogMessage("CanSyncAzimuth Get", false.ToString());
                return false;
            }
        }

        public void CloseShutter()
        {
            tl.LogMessage("CloseShutter", "Enter");
            domeShutterState = false;

            //Reread sensor data
            Hardware.getInputStatus();

            //if (!Hardware.closed_shutter_flag && Hardware.opened_shutter_flag)
            if (!Hardware.closed_shutter_flag)
            {
                //Press switch
                tl.LogMessage("CloseShutter", "Opened, pressing switch");
                Hardware.pressRoofSwitch();

                //clear shutter status cache
                lastShutterStatusCheck = EXPIRED_CACHE;
                
                //set moving state
                Hardware.closed_shutter_flag = false;
                Hardware.opened_shutter_flag = false;
                tl.LogMessage("CloseShutter", "Moving was initiated");
            }
            else if (Hardware.closed_shutter_flag)
            {
                tl.LogMessage("CloseShutter", "Skipping because already closed");
            }
            tl.LogMessage("CloseShutter", "Exit");
        }

        public void FindHome()
        {
            tl.LogMessage("FindHome", "Not implemented");
            throw new ASCOM.MethodNotImplementedException("FindHome");
        }

        public void OpenShutter()
        {

            tl.LogMessage("OpenShutter", "Enter");

            //Reread sensor data no matter of cache
            Hardware.getInputStatus();

            //if (Hardware.closed_shutter_flag && !Hardware.opened_shutter_flag)
            if (!Hardware.opened_shutter_flag)
            {
                //Press switch
                tl.LogMessage("OpenShutter", "Not opened, pressing switch");
                Hardware.pressRoofSwitch();

                //clear shutter status cache
                lastShutterStatusCheck = EXPIRED_CACHE;

                //set moving state
                Hardware.closed_shutter_flag = false;
                Hardware.opened_shutter_flag = false;
                tl.LogMessage("OpenShutter", "Moving was initiated");
            }
            else if (Hardware.closed_shutter_flag)
            {
                tl.LogMessage("OpenShutter", "Skipping because already opened");
            }
            tl.LogMessage("OpenShutter", "Exit");
            domeShutterState = true;
        }

        public void Park()
        {
            tl.LogMessage("Park", "Not implemented");
            throw new ASCOM.MethodNotImplementedException("Park");
        }

        public void SetPark()
        {
            tl.LogMessage("SetPark", "Not implemented");
            throw new ASCOM.MethodNotImplementedException("SetPark");
        }

        public ShutterState ShutterStatus
        {
            get {
                tl.LogMessage("ShutterStatus", "Enter");

                ShutterState retShutterSatus = new ShutterState();
                    // shutterOpen 0 Dome shutter status open  
                    // shutterClosed 1 Dome shutter status closed  
                    // shutterOpening 2 Dome shutter status opening  
                    // shutterClosing 3 Dome shutter status closing  
                    // shutterError 4 Dome shutter status error  
                

                //Check if connected
                tl.LogMessage("ShutterStatus", "Check if device connected");
                if (! IsConnected())
                {
                    tl.LogMessage("ShutterStatus", "ERROR. Can't return shutter status because not connected");
                    ERROR_MESSAGE = "Can't return shutter status because device is not connected";
                    throw new ASCOM.DriverException(ERROR_MESSAGE);
                }

               
                // Read input status
                Hardware.opened_shutter_flag=Hardware.OpenedSensorState();
                Hardware.closed_shutter_flag=Hardware.ClosedSensorState();

                //Calculate shutter status
                retShutterSatus = ShutterState.shutterError;

                if (!Hardware.opened_shutter_flag && !Hardware.closed_shutter_flag)
                {
                // shutter is moving
                    if (prev_shutter_state == ShutterState.shutterOpen)
                    {
                    // shutter is closing
                        retShutterSatus = ShutterState.shutterClosing;
                    }
                    else if (prev_shutter_state == ShutterState.shutterClosed)
                    {
                    // shutter is opening
                        retShutterSatus = ShutterState.shutterOpening;
                    }
                }
                else if (Hardware.opened_shutter_flag)
                {
                // shutter is opened
                    retShutterSatus = ShutterState.shutterOpen;
                    prev_shutter_state = ShutterState.shutterOpen;
                }
                else if (Hardware.closed_shutter_flag)
                {
                // shutter is closed
                    retShutterSatus = ShutterState.shutterClosed;
                    prev_shutter_state = ShutterState.shutterClosed;
                }


                tl.LogMessage("ShutterStatus", "Exit, shutter status: " + retShutterSatus.ToString());
                return retShutterSatus;
            }
            
        }

        public bool Slaved
        {
            get
            {
                tl.LogMessage("Slaved Get", false.ToString());
                return false;
            }
            set
            {
                tl.LogMessage("Slaved Set", "not implemented");
                throw new ASCOM.PropertyNotImplementedException("Slaved", true);
            }
        }

        public void SlewToAltitude(double Altitude)
        {
            tl.LogMessage("SlewToAltitude", "Not implemented");
            throw new ASCOM.MethodNotImplementedException("SlewToAltitude");
        }

        public void SlewToAzimuth(double Azimuth)
        {
            tl.LogMessage("SlewToAzimuth", "Not implemented");
            throw new ASCOM.MethodNotImplementedException("SlewToAzimuth");
        }

        public bool Slewing
        {
            get
            {
                tl.LogMessage("Slewing Get", false.ToString());
                return false;
            }
        }

        public void SyncToAzimuth(double Azimuth)
        {
            tl.LogMessage("SyncToAzimuth", "Not implemented");
            throw new ASCOM.MethodNotImplementedException("SyncToAzimuth");
        }

        #endregion

        #region Private properties and methods
        // here are some useful properties and methods that can be used as required
        // to help with driver development

        #region ASCOM Registration

        // Register or unregister driver for ASCOM. This is harmless if already
        // registered or unregistered. 
        //
        /// <summary>
        /// Register or unregister the driver with the ASCOM Platform.
        /// This is harmless if the driver is already registered/unregistered.
        /// </summary>
        /// <param name="bRegister">If <c>true</c>, registers the driver, otherwise unregisters it.</param>
        private static void RegUnregASCOM(bool bRegister)
        {
            using (var P = new ASCOM.Utilities.Profile())
            {
                P.DeviceType = "Dome";
                if (bRegister)
                {
                    P.Register(driverID, driverDescriptionShort);
                }
                else
                {
                    P.Unregister(driverID);
                }
            }
        }

        /// <summary>
        /// This function registers the driver with the ASCOM Chooser and
        /// is called automatically whenever this class is registered for COM Interop.
        /// </summary>
        /// <param name="t">Type of the class being registered, not used.</param>
        /// <remarks>
        /// This method typically runs in two distinct situations:
        /// <list type="numbered">
        /// <item>
        /// In Visual Studio, when the project is successfully built.
        /// For this to work correctly, the option <c>Register for COM Interop</c>
        /// must be enabled in the project settings.
        /// </item>
        /// <item>During setup, when the installer registers the assembly for COM Interop.</item>
        /// </list>
        /// This technique should mean that it is never necessary to manually register a driver with ASCOM.
        /// </remarks>
        [ComRegisterFunction]
        public static void RegisterASCOM(Type t)
        {
            RegUnregASCOM(true);
        }

        /// <summary>
        /// This function unregisters the driver from the ASCOM Chooser and
        /// is called automatically whenever this class is unregistered from COM Interop.
        /// </summary>
        /// <param name="t">Type of the class being registered, not used.</param>
        /// <remarks>
        /// This method typically runs in two distinct situations:
        /// <list type="numbered">
        /// <item>
        /// In Visual Studio, when the project is cleaned or prior to rebuilding.
        /// For this to work correctly, the option <c>Register for COM Interop</c>
        /// must be enabled in the project settings.
        /// </item>
        /// <item>During uninstall, when the installer unregisters the assembly from COM Interop.</item>
        /// </list>
        /// This technique should mean that it is never necessary to manually unregister a driver from ASCOM.
        /// </remarks>
        [ComUnregisterFunction]
        public static void UnregisterASCOM(Type t)
        {
            RegUnregASCOM(false);
        }

        #endregion

        /// <summary>
        /// Returns true if there is a valid connection to the driver hardware
        /// </summary>
        private bool IsConnected(bool forcedflag= false)
        {
            tl.LogMessage("IsConnected", "Enter");
            // Check that the driver hardware connection exists and is connected to the hardware
            connectedState = Hardware.IsConnected(forcedflag);

            tl.LogMessage("IsConnected", "Exit. Returning status: " + connectedState.ToString());

            return connectedState;
        }

        /// <summary>
        /// Use this function to throw an exception if we aren't connected to the hardware
        /// </summary>
        /// <param name="message"></param>
        private void CheckConnected(string message)
        {
            if (!IsConnected())
            {
                throw new ASCOM.NotConnectedException(message);
            }
        }

        /// <summary>
        /// Read the device configuration from the ASCOM Profile store
        /// </summary>
        internal void ReadProfile()
        {
            tl.LogMessage("ReadProfile", "Enter");


            using (Profile p = new Profile())
            {
                p.DeviceType = "Dome";

                //General settings
                #region Device settings
               
                try
                {
                    Hardware_class.ip_addr = p.GetValue(driverID, Hardware_class.ip_addr_profilename, string.Empty, Hardware_class.ip_addr_default);
                }
                catch (Exception e)
                {
                    //p.WriteValue(driverID, ip_addr_profilename, ip_addr_default);
                    Hardware_class.ip_addr = Hardware_class.ip_addr_default;
                    tl.LogMessage("readSettings", "Wrong input string for [ip_addr]: [" + e.Message + "]");
                }
                try
                {
                    Hardware_class.ip_port = p.GetValue(driverID, Hardware_class.ip_port_profilename, string.Empty, Hardware_class.ip_port_default);
                }
                catch (Exception e)
                {
                    //p.WriteValue(driverID, ip_port_profilename, ip_port_default);
                    Hardware_class.ip_port = Hardware_class.ip_port_default;
                    tl.LogMessage("readSettings", "Wrong input string for [ip_port]: [" + e.Message + "]");
                }
                try
                {
                    Hardware_class.ip_login = p.GetValue(driverID, Hardware_class.ip_login_profilename, string.Empty, Hardware_class.ip_login_default);
                }
                catch (Exception e)
                {
                    //p.WriteValue(driverID, ip_login_profilename, ip_login_default);
                    Hardware_class.ip_login = Hardware_class.ip_login_default;
                    tl.LogMessage("readSettings", "Wrong input string for [ip_login]: [" + e.Message + "]");
                }

                try
                {
                    Hardware_class.ip_pass = p.GetValue(driverID, Hardware_class.ip_pass_profilename, string.Empty, Hardware_class.ip_pass_default);
                }
                catch (Exception e)
                {
                    //p.WriteValue(driverID, ip_pass_profilename, ip_pass_default);
                    Hardware_class.ip_pass = Hardware_class.ip_pass_default;
                    tl.LogMessage("readSettings", "Wrong input string for [ip_pass]: [" + e.Message + "]");
                }
                #endregion


                //Base settings
                #region Base settings
                try
                {
                    Hardware_class.switch_roof_port = Convert.ToInt16(p.GetValue(driverID, Hardware_class.switch_port_profilename, string.Empty, Hardware_class.switch_port_default));
                }
                catch (Exception e)
                {
                    Hardware_class.switch_roof_port = Convert.ToInt16(Hardware_class.switch_port_default);
                    tl.LogMessage("readProfile", "Input string [switch_roof_port] is not a sequence of digits [" + e.Message + "]");
                }

                try
                {
                    Hardware_class.opened_sensor_port = Convert.ToInt16(p.GetValue(driverID, Hardware_class.opened_port_profilename, string.Empty, Hardware_class.opened_port_default));
                }
                catch (Exception e)
                {
                    Hardware_class.opened_sensor_port = Convert.ToInt16(Hardware_class.opened_port_default);
                    tl.LogMessage("readProfile", "Input string [opened_sensor_port] is not a sequence of digits [" + e.Message + "]");
                }
                try
                {
                    Hardware_class.closed_sensor_port = Convert.ToInt16(p.GetValue(driverID, Hardware_class.closed_port_profilename, string.Empty, Hardware_class.closed_port_default));
                }
                catch (Exception e)
                {
                    Hardware_class.closed_sensor_port = Convert.ToInt16(Hardware_class.closed_port_default);
                    tl.LogMessage("readProfile", "Input string [closed_sensor_port] is not a sequence of digits [" + e.Message + "]");
                }

                try
                {
                    Hardware_class.switch_port_state_type = Convert.ToBoolean(p.GetValue(driverID, Hardware_class.switch_port_state_type_profilename, string.Empty, Hardware_class.switch_port_state_type_default));
                }
                catch (Exception e)
                {
                    Hardware_class.switch_port_state_type = Convert.ToBoolean(Hardware_class.switch_port_state_type_default);
                    tl.LogMessage("readProfile", "Input string [switch_port_state_type] is not a boolean value [" + e.Message + "]");
                }
                try
                {
                    Hardware_class.opened_port_state_type = Convert.ToBoolean(p.GetValue(driverID, Hardware_class.opened_port_state_type_profilename, string.Empty, Hardware_class.opened_port_state_type_default));
                }
                catch (Exception e)
                {
                    Hardware_class.opened_port_state_type = Convert.ToBoolean(Hardware_class.opened_port_state_type_default);
                    tl.LogMessage("readProfile", "Input string [opened_port_state_type] is not a boolean value [" + e.Message + "]");
                }
                try
                {
                    Hardware_class.closed_port_state_type = Convert.ToBoolean(p.GetValue(driverID, Hardware_class.closed_port_state_type_profilename, string.Empty, Hardware_class.closed_port_state_type_default));
                }
                catch (Exception e)
                {
                    Hardware_class.closed_port_state_type = Convert.ToBoolean(Hardware_class.closed_port_state_type_default);
                    tl.LogMessage("readProfile", "Input string [closed_port_state_type] is not a boolean value [" + e.Message + "]");
                }
                #endregion

                //Advanced settings
                #region Advanced settings

                try
                {
                    MyWebClient.NETWORK_TIMEOUT = Convert.ToInt16(p.GetValue(driverID, MyWebClient.NETWORK_TIMEOUT_profilename, string.Empty, MyWebClient.NETWORK_TIMEOUT_default.ToString()));
                }
                catch (Exception e)
                {
                    MyWebClient.NETWORK_TIMEOUT = MyWebClient.NETWORK_TIMEOUT_default;
                    tl.LogMessage("readProfile", "Input string [NETWORK_TIMEOUT] is not a sequence of digits [" + e.Message + "]");
                }
                Hardware_class.Semaphore_timeout = MyWebClient.NETWORK_TIMEOUT+ Hardware_class.Semaphore_timeout_extratime; //+1 sec
                try
                {
                    Hardware_class.CACHE_CHECKCONNECTED_INTERVAL = Convert.ToUInt32(p.GetValue(driverID, Hardware_class.CACHE_CHECKCONNECTED_INTERVAL_profilename, string.Empty, Hardware_class.CACHE_CHECKCONNECTED_INTERVAL_default.ToString()));
                }
                catch (Exception e)
                {
                    Hardware_class.CACHE_CHECKCONNECTED_INTERVAL = Hardware_class.CACHE_CHECKCONNECTED_INTERVAL_default;
                    tl.LogMessage("readProfile", "Input string [CACHE_CHECKCONNECTED_INTERVAL] is not a sequence of digits [" + e.Message + "]");
                }
                try
                {
                    Hardware_class.CACHE_SHUTTERSTATUS_INTERVAL_NORMAL = Convert.ToUInt32(p.GetValue(driverID, Hardware_class.CACHE_SHUTTERSTATUS_INTERVAL_NORMAL_profilename, string.Empty, Hardware_class.CACHE_SHUTTERSTATUS_INTERVAL_NORMAL_default.ToString()));
                }
                catch (Exception e)
                {
                    Hardware_class.CACHE_SHUTTERSTATUS_INTERVAL_NORMAL = Hardware_class.CACHE_SHUTTERSTATUS_INTERVAL_NORMAL_default;
                    tl.LogMessage("readProfile", "Input string [CACHE_SHUTTERSTATUS_INTERVAL_NORMAL] is not a sequence of digits [" + e.Message + "]");
                }
                try
                {
                    Hardware_class.CACHE_SHUTTERSTATUS_INTERVAL_REDUCED = Convert.ToUInt32(p.GetValue(driverID, Hardware_class.CACHE_SHUTTERSTATUS_INTERVAL_REDUCED_profilename, string.Empty, Hardware_class.CACHE_SHUTTERSTATUS_INTERVAL_REDUCED_default.ToString()));
                }
                catch (Exception e)
                {
                    Hardware_class.CACHE_SHUTTERSTATUS_INTERVAL_REDUCED = Hardware_class.CACHE_SHUTTERSTATUS_INTERVAL_REDUCED_default;
                    tl.LogMessage("readProfile", "Input string [CACHE_SHUTTERSTATUS_INTERVAL_REDUCED] is not a sequence of digits [" + e.Message + "]");
                }


                try
                {
                    traceState = Convert.ToBoolean(p.GetValue(driverID, traceStateProfileName, string.Empty, traceStateDefault));
                }
                catch (Exception e)
                {
                    traceState = Convert.ToBoolean(traceStateDefault);
                    tl.LogMessage("readProfile", "Input string [traceState] is not a boolean value [" + e.Message + "]");
                }
                #endregion

            }

            tl.LogMessage("ReadProfile", "Exit");


        }

        /// <summary>
        /// Write the device configuration to the  ASCOM  Profile store
        /// </summary>
        internal void WriteProfile()
        {
            tl.LogMessage("WriteProfile", "Enter");
            
            using (Profile driverProfile = new Profile())
            {
                driverProfile.DeviceType = "Dome";
                driverProfile.WriteValue(driverID, traceStateProfileName, traceState.ToString());

                driverProfile.WriteValue(driverID, Hardware_class.ip_addr_profilename, Hardware_class.ip_addr);
                driverProfile.WriteValue(driverID, Hardware_class.ip_port_profilename, Hardware_class.ip_port);
                driverProfile.WriteValue(driverID, Hardware_class.ip_login_profilename, Hardware_class.ip_login);
                driverProfile.WriteValue(driverID, Hardware_class.ip_pass_profilename, Hardware_class.ip_pass);

                driverProfile.WriteValue(driverID, Hardware_class.switch_port_profilename, Hardware_class.switch_roof_port.ToString());
                driverProfile.WriteValue(driverID, Hardware_class.opened_port_profilename, Hardware_class.opened_sensor_port.ToString());
                driverProfile.WriteValue(driverID, Hardware_class.closed_port_profilename, Hardware_class.closed_sensor_port.ToString());


                driverProfile.WriteValue(driverID, MyWebClient.NETWORK_TIMEOUT_profilename, MyWebClient.NETWORK_TIMEOUT.ToString());
                driverProfile.WriteValue(driverID, Hardware_class.CACHE_CHECKCONNECTED_INTERVAL_profilename, Hardware_class.CACHE_CHECKCONNECTED_INTERVAL.ToString());
                driverProfile.WriteValue(driverID, Hardware_class.CACHE_SHUTTERSTATUS_INTERVAL_NORMAL_profilename, Hardware_class.CACHE_SHUTTERSTATUS_INTERVAL_NORMAL.ToString());
                driverProfile.WriteValue(driverID, Hardware_class.CACHE_SHUTTERSTATUS_INTERVAL_REDUCED_profilename, Hardware_class.CACHE_SHUTTERSTATUS_INTERVAL_REDUCED.ToString());
            }
            tl.LogMessage("Switch_writeSettings", "Exit");



            tl.LogMessage("WriteProfile", "Exit");
        }

        #endregion

    }




}
