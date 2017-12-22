using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace IP9212_emul
{
    class MyWebServer
    {
        private TcpListener myListener;
        private int port = 5050;  // Select any free port you wish 

        internal Vedrus_dome_emul Hardware;

        public MyWebServer()
        {
            try
            {
                //start listing on the given port
                myListener = new TcpListener(port);
                myListener.Start();
                Console.WriteLine("Web Server Running [port "+ port + "]... Press ^C to Stop...");

                //start the thread which calls the method 'StartListen'
                Thread th = new Thread(new ThreadStart(StartListen));
                th.Start();

                Hardware = new Vedrus_dome_emul();

            }
            catch (Exception e)
            {
                Console.WriteLine("An Exception Occurred while Listening :"
                                   + e.ToString());
            }
        }




        //This method Accepts new connection and
        //First it receives the welcome massage from the client,
        //Then it sends the Current date time to the Client.
        public void StartListen()
        {

            int iStartPos = 0, iEndPos = 0; ;
            String sRequest;
            String sDirName;
            String sGetStr;
            String sRequestedFile;
            String sErrorMessage;
            String sLocalDir;
            String sMyWebServerRoot = "C:\\MyWebServerRoot\\";
            String sPhysicalFilePath = "";
            String sFormattedMessage = "";
            String sResponse = "";



            while (true)
            {
                //Accept a new connection
                Socket mySocket = myListener.AcceptSocket();

                Console.WriteLine("Socket Type " + mySocket.SocketType);
                if (mySocket.Connected)
                {
                    Console.WriteLine("\nClient Connected!!\n==================\nCLient IP {0}\n", mySocket.RemoteEndPoint);

                    //make a byte array and receive data from the client 
                    Byte[] bReceive = new Byte[1024];
                    int i = mySocket.Receive(bReceive, bReceive.Length, 0);

                    //Convert Byte to String
                    string sBuffer = Encoding.ASCII.GetString(bReceive);

                    //At present we will only deal with GET type
                    if (sBuffer.Substring(0, 3) != "GET")
                    {
                        Console.WriteLine("Only Get Method is supported..");
                        mySocket.Close();
                        return;
                    }

                    // Look for HTTP request
                    iStartPos = sBuffer.IndexOf("HTTP", 1);

                    // Get the HTTP text and version e.g. it will return "HTTP/1.1"
                    string sHttpVersion = sBuffer.Substring(iStartPos, 8);

                    // Extract the Requested Type and Requested file/directory
                    sRequest = sBuffer.Substring(0, iStartPos - 1);

                    //Replace backslash with Forward Slash, if Any
                    sRequest.Replace("\\", "/");

                    //If file name is not supplied add forward slash to indicate 
                    //that it is a directory and then we will look for the 
                    //default file name..
                    if ((sRequest.IndexOf(".") < 1) && (!sRequest.EndsWith("/")))
                    {
                        sRequest = sRequest + "/";
                    }


                    //Extract The directory Name
                    sDirName = sRequest.Substring(sRequest.IndexOf("/"), sRequest.LastIndexOf("/") - 3);

                    //Extract the requested file name
                    iStartPos = sRequest.LastIndexOf("/");
                    iEndPos = sRequest.LastIndexOf("?");
                    iEndPos = (iEndPos < 0 ? sRequest.Length : iEndPos)-1;

                    sRequestedFile = sRequest.Substring(iStartPos + 1, iEndPos-iStartPos);

                    //Extract GET string
                    iStartPos = sRequest.LastIndexOf("?") + 1;
                    if (iStartPos <= 0)
                    {
                        sGetStr = "";
                    }
                    else
                    { 
                        sGetStr = sRequest.Substring(iStartPos);
                    }

                    //Console.WriteLine("Buffer: " + sBuffer);
                    Console.WriteLine("Directory Requested : " + sDirName);
                    Console.WriteLine("File Requested : " + sRequestedFile);
                    Console.WriteLine("Parameter String : " + sGetStr);

                    String sMimeType = GetMimeType(sRequestedFile);

                    //set.cmd?cmd=getio
                    //set.cmd?cmd=getpower
                    //set.cmd?cmd=getpower
                    //set.cmd?cmd=setpower+P6
                    //http://192.168.1.147/Set.cmd?user=admin+pass=12345678CMD=setpower+P63=1


                    //If The file name is not supplied then look in the default file list
                    if (sRequestedFile.ToLower()!="set.cmd")
                    {
                        sErrorMessage = "<H2>404 Error! File [" + sDirName + "/" + sRequestedFile + "] Does Not Exists...</H2>";
                        SendHeader(sHttpVersion, "", sErrorMessage.Length, " 404 Not Found", ref mySocket);
                        SendToBrowser(sErrorMessage, ref mySocket);
                        Console.WriteLine(sErrorMessage);

                        mySocket.Close();
                        continue;
                    }
                    else
                    {
                        sResponse = "";

                        //Parser parameter string 
                        // (should be called 1st)
                        Hardware.ParseGetString(sGetStr);

                        if (Hardware.debugf)
                        {
                            sResponse += "User : " + Hardware.username + "<br>"
                                + "Pass : " + Hardware.pass + "<br>"
                                + "CMD : " + Hardware.cmd + "<br>";
                        }

                        //Parse commmands
                        //should be called 2nd
                        string output=Hardware.ParseCMDParameters();

                        //Now output results
                        if (Hardware.debugf)
                        {
                            sResponse += "Output : " + output + "<br>";
                        }
                        else
                        {
                            sResponse += output;
                        }

                        if (Hardware.debugf)
                        {
                            sResponse += "<br><br>" + Hardware.LittleHelp();
                        }

                        SendHeader(sHttpVersion, sMimeType, sResponse.Length, " 200 OK", ref mySocket);
                        SendToBrowser(sResponse, ref mySocket);

                        Console.WriteLine(DateTime.Now.ToString("HH:mm:ss.f")+" : response : " + sResponse);

                        //mySocket.Send(bytes, bytes.Length,0);

                    }
                    mySocket.Close();
                }
            }
        }


        /// <summary>
        /// This function send the Header Information to the client (Browser)
        /// </summary>
        /// <param name="sHttpVersion">HTTP Version</param>
        /// <param name="sMIMEHeader">Mime Type</param>
        /// <param name="iTotalBytes">Total Bytes to be sent in the body</param>
        /// <param name="mySocket">Socket reference</param>
        /// <returns></returns>
        public void SendHeader(string sHttpVersion, string sMIMEHeader, int iTotalBytes, string sStatusCode, ref Socket mySocket)
        {

            String sBuffer = "";

            // if Mime type is not provided set default to text/html
            if (sMIMEHeader.Length == 0)
            {
                sMIMEHeader = "text/html";  // Default Mime Type is text/html
            }

            sBuffer = sBuffer + sHttpVersion + sStatusCode + "\r\n";
            sBuffer = sBuffer + "Server: cx1193719-b\r\n";
            sBuffer = sBuffer + "Content-Type: " + sMIMEHeader + "\r\n";
            sBuffer = sBuffer + "Accept-Ranges: bytes\r\n";
            sBuffer = sBuffer + "Content-Length: " + iTotalBytes + "\r\n\r\n";

            Byte[] bSendData = Encoding.ASCII.GetBytes(sBuffer);

            SendToBrowser(bSendData, ref mySocket);

            Console.WriteLine("Total Bytes : " + iTotalBytes.ToString());

        }



        /// <summary>
        /// Overloaded Function, takes string, convert to bytes and calls 
        /// overloaded sendToBrowserFunction.
        /// </summary>
        /// <param name="sData">The data to be sent to the browser(client)</param>
        /// <param name="mySocket">Socket reference</param>
        public void SendToBrowser(String sData, ref Socket mySocket)
        {
            SendToBrowser(Encoding.ASCII.GetBytes(sData), ref mySocket);
        }



        /// <summary>
        /// Sends data to the browser (client)
        /// </summary>
        /// <param name="bSendData">Byte Array</param>
        /// <param name="mySocket">Socket reference</param>
        public void SendToBrowser(Byte[] bSendData, ref Socket mySocket)
        {
            int numBytes = 0;

            try
            {
                if (mySocket.Connected)
                {
                    if ((numBytes = mySocket.Send(bSendData, bSendData.Length, 0)) == -1)
                        Console.WriteLine("Socket Error cannot Send Packet");
                    else
                    {
                        Console.WriteLine("No. of bytes send {0}", numBytes);
                    }
                }
                else
                    Console.WriteLine("Connection Dropped....");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error Occurred : {0} ", e);

            }
        }


        /// <summary>
        /// This function takes FileName as Input and returns the mime type..
        /// </summary>
        /// <param name="sRequestedFile">To indentify the Mime Type</param>
        /// <returns>Mime Type</returns>
        public string GetMimeType(string sRequestedFile)
        {
            String sMimeType = "";

            sMimeType = "text/html";
            return sMimeType;
        }


    }
}
