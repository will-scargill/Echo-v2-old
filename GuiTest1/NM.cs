using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocketLibrary;
using System.Windows;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Newtonsoft.Json;

namespace GuiTest1
{
    class NM
    {
        private static Socket sender;
        private static bool recieving;

        public static Dictionary<string, object> serverInfo = new Dictionary<string, object>();

        public static bool Connect(string ip, int port)
        {
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = IPAddress.Parse(ip);
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);

            sender = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                sender.Connect(remoteEP);
                recieving = true;
                return true;
            }
            catch (System.Net.Sockets.SocketException)
            {
                MessageBox.Show("Error - Connection Failed");
                return false;
            }
            
            
        }

        public static void SendMessage(string message)
        {
            byte[] msg = Encoding.UTF8.GetBytes(message);

            int bytesSent = sender.Send(msg);
        }

        public delegate void UpdateListboxCallback(string message);

        private static void RecvLoop()
        {
            byte[] bytes = new byte[1024];
            while (recieving == true)
            {
                int bytesRec = sender.Receive(bytes);
                string jsonData = Encoding.UTF8.GetString(bytes, 0, bytesRec);

                Dictionary<string, object> message = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonData);
                //MessageBox.Show((string)message["messagetype"]);
                switch (message["messagetype"])
                {
                    case "outboundMessage":
                        screenMain.pageMain.lbMainMessages.Dispatcher.Invoke(() => { screenMain.pageMain.lbMainMessages.Items.Add(message["content"]); });
                        break;
                    case "connReqAccepted":
                        serverInfo.Add("channels", message["content"]);
                        MainWindow.main.frame.Dispatcher.Invoke(() => { MainWindow.main.frame.Source = new Uri("screenMain.xaml", UriKind.Relative); });
                        break;
                    case "connReqDenied":
                        NM.DC();
                        break;

                }
 
            }
        }

        public static void RecieveMessage()
        {
            ThreadStart childref = new ThreadStart(RecvLoop);
            Thread childThread = new Thread(childref);
            childThread.Start();
        }

        public static void DC()
        {
            recieving = false;
        }
    }
}
