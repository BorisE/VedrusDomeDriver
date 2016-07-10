//tabs=4
// --------------------------------------------------------------------------------
//
// ASCOM Dome driver for roll-off roof, controlled by Aviosys IP9212
//
// Description:	ASCOM Driver for roll-off roof controling by AvioSys IP9212 
//				control server. It receives commands by http protocol commands
//
// Implements:	ASCOM Dome interface version: 2
// Author:		(XXX) Boris Emchenko <www.astromania.info>
//
// Edit Log:
//
// Date			Who	Vers	Description
// -----------	---	-----	-------------------------------------------------------
// 12-06-2013	XXX	0.1.0	Initial edit, created from ASCOM driver template
// 15-09-2013	XXX	0.1.1	starting impementing caching for reading connected and shutter states
// 30-09-2013	XXX	0.1.2	impemented caching for reading connected and shutter states
// 30-10-2013	XXX	2.0.0	rewriting driver based on new ASCOM template. 
//                          Using standalone driver for controlling Aviosys IP 9212 (using some elemenets of ASCOM switch wich is not disclosed yet)
//                          Using ASCOM trace log instead of custom
// 16-11-2013	XXX	2.0.1	final release 
// 17-11-2013	XXX	2.0.2	caching improvements 
// 18-07-2015	XXX	2.0.3	trying to make source code work again :)


// This is used to define code in the template that is specific to one class implementation
// unused code canbe deleted and this definition removed.
#define Dome

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;

using ASCOM;
using ASCOM.Astrometry;
using ASCOM.Astrometry.AstroUtils;
using ASCOM.Utilities;
using ASCOM.DeviceInterface;
using System.Globalization;
using System.Collections;

using IP9212_switch;

namespace ASCOM.IP9212_rolloffroof3
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
    /// ASCOM Dome Driver for IP9212_rolloffroof2.
    /// </summary>
    [Guid("C9ACEBB9-0391-4EB6-9D15-5483FDE4F205")]
    [ClassInterface(ClassInterfaceType.None)]
    public class Dome : IDomeV2
    {
        /// <summary>
        /// ASCOM DeviceID (COM ProgID) for this driver.
        /// The DeviceID is used by ASCOM applications to load the driver at runtime.
        /// </summary>
        internal static string driverID = "ASCOM.IP9212_rolloffroof3.Dome";
        /// <summary>
        /// Driver description that displays in the ASCOM Chooser.
        /// </summary>
        private static string driverDescription = "ASCOM Dome driver for roll-off roof controlled by Aviosys IP9212. Written by Boris Emchenko http://astromania.info";
        private static string driverDescriptionShort = "Roll-off roof on IP9212v3";

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
        private IP9212_switch_class Hardware;

        public static DateTime EXPIRED_CACHE = new DateTime(2010, 05, 12, 13, 15, 00); //CONSTANT FOR MARKING AN OLD TIME

        //Previos shutter states
        private ShutterState prev_shutter_state;
        bool last_OpenedState; // last measured value for opened sensor
        bool last_ClosedState; // last measured value closed sensor

        //Caching last shutter status
        private DateTime lastShutterStatusCheck = EXPIRED_CACHE; //when was the last hardware checking provided for shutter state 
        int SHUTTERSTATUS_CHECK_INTERVAL_NORMAL = 10; //how often to chech true shutter status (in seconds) for regular cases
        int SHUTTERSTATUS_CHECK_INTERVAL_REDUCED = 2;//how often to chech true shutter status (in seconds) when shutter is moving

        //error message
        private string ERROR_MESSAGE = "";
        
        /// <summary>
        /// Private variable to hold an ASCOM Utilities object
        /// </summary>
        private Util utilities;

        /// <summary>
        /// Private variable to hold an ASCOM AstroUtilities object to provide the Range method
        /// </summary>
        private AstroUtils astroUtilities;

        /// <summary>
        /// Private variable to hold the trace logger object (creates a diagnostic log file with information that you specify)
        /// </summary>
        private TraceLogger tl;

        /// <summary>
        /// Initializes a new instance of the <see cref="IP9212_rolloffroof2"/> class.
        /// Must be public for COM registration.
        /// </summary>
        public Dome()
        {
            #if DEBUG
            
            #endif
            tl = new TraceLogger("", "IP9212_rolloffroof2");
            tl.Enabled = true; //at least for debugging - log will be always created no matter the value of traceState
            tl.LogMessage("Dome", "Starting initialisation");

            Hardware = new IP9212_switch_class();

            ReadProfile(); // Read device configuration from the ASCOM Profile store

            //set the correct logger state
            tl.Enabled = traceState;

            connectedState = false; // Initialise connected to false
         
            utilities = new Util(); //Initialise util object
            astroUtilities = new AstroUtils(); // Initialise astro utilities object

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
                System.Windows.Forms.MessageBox.Show("Already connected, just press OK");
            }
            else
            {
                using (IP9212_switch.SetupDialog F = new SetupDialog(externalCallType.domeCall,this))
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
                return new ArrayList();
            }
        }

        public string Action(string actionName, string actionParameters)
        {
            throw new ASCOM.ActionNotImplementedException("Action " + actionName + " is not implemented by this driver");
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
            utilities.Dispose();
            utilities = null;
            astroUtilities.Dispose();
            astroUtilities = null;
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
                tl.LogMessage("Connected(Set)", "Enter. Param: "+value.ToString());

                //Special case for MaximDL - finish with exception to display message
                if (!value && (ERROR_MESSAGE != "" || Hardware.ASCOM_ERROR_MESSAGE != ""))
                {
                    if (ERROR_MESSAGE == "" && Hardware.ASCOM_ERROR_MESSAGE != "") ERROR_MESSAGE = Hardware.ASCOM_ERROR_MESSAGE;
                    tl.LogMessage("Connected(Set)", "Throwing exception for MaximDL with message [" + ERROR_MESSAGE + "]");
                    throw new ASCOM.DriverException(ERROR_MESSAGE);
                }

                
                if (value == IsConnected(true))
                    return;

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
                        ERROR_MESSAGE = "Couldn't connect to IP9212 control device on [" + IP9212_switch_class.ip_addr + "]";
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
                string driverVersion = "2.0";
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
                
                //if shutter in moving state - reduce check interval
                int checkInterval=0;
                if ((!Hardware.opened_shutter_flag && !Hardware.closed_shutter_flag))
                {
                    checkInterval=SHUTTERSTATUS_CHECK_INTERVAL_REDUCED;
                    tl.LogMessage("ShutterStatus", "Shutter in medium position, using reduced caching interval (" + SHUTTERSTATUS_CHECK_INTERVAL_REDUCED+")");
                }
                else
                {
                    checkInterval=SHUTTERSTATUS_CHECK_INTERVAL_NORMAL;
                    tl.LogMessage("ShutterStatus", "Using normal caching interval (" + SHUTTERSTATUS_CHECK_INTERVAL_NORMAL + ")");
                }

                //Check if connected
                if (! IsConnected())
                {
                    tl.LogMessage("ShutterStatus", "ERROR. Can't return shutter status because not connected");
                    ERROR_MESSAGE = "Can't return shutter status because device is not connected";
                    throw new ASCOM.DriverException(ERROR_MESSAGE);

                }

                //// READ CURRENT INPUT STATE IF IT WASN'T READ YET
                //if (Hardware.input_state_arr[0] <= 0)
                //{
                //    tl.LogMessage("ShutterStatus", "Unidentified input status array, re-reading states");
                //    Hardware.getInputStatus();
                //}

                
                //Measure how much time have passed since last HARDWARE measure
                TimeSpan passed = DateTime.Now - Hardware.lastShutterStatusCheck;

                if (passed.TotalSeconds > checkInterval)
                {
                    tl.LogMessage("ShutterStatus", "Cache expired [" + passed.TotalSeconds + " sec passed], rereading sensor status");
                    // Read input status
                    Hardware.getInputStatus();

                    // Read input status
                    Hardware.opened_shutter_flag=Hardware.OpenedSensorState();
                    Hardware.closed_shutter_flag=Hardware.ClosedSensorState();

                    lastShutterStatusCheck = DateTime.Now;
                }
                else
                {
                    tl.LogMessage("ShutterStatus", "Using cached inputsensor values [" + passed.TotalSeconds + " sec passed]");
                }

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
            
            
            //get
            //{
            //    tl.LogMessage("ShutterStatus Get", "Enter");
            //    if (domeShutterState)
            //    {
            //        tl.LogMessage("ShutterStatus", ShutterState.shutterOpen.ToString());
            //        return ShutterState.shutterOpen;
            //    }
            //    else
            //    {
            //        tl.LogMessage("ShutterStatus", ShutterState.shutterClosed.ToString());
            //        return ShutterState.shutterClosed;
            //    }
            //}
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

            Hardware.readSettings(); 

            using (Profile driverProfile = new Profile())
            {
                driverProfile.DeviceType = "Dome";
                traceState = Convert.ToBoolean(driverProfile.GetValue(driverID, traceStateProfileName, string.Empty, traceStateDefault));
            }

            tl.LogMessage("ReadProfile", "Exit");
        }

        /// <summary>
        /// Write the device configuration to the  ASCOM  Profile store
        /// </summary>
        internal void WriteProfile()
        {
            tl.LogMessage("WriteProfile", "Enter");
            
            Hardware.writeSettings();
            
            using (Profile driverProfile = new Profile())
            {
                driverProfile.DeviceType = "Dome";
                driverProfile.WriteValue(driverID, traceStateProfileName, traceState.ToString());
            }

            tl.LogMessage("WriteProfile", "Exit");
        }

        #endregion

    }




}
