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
using System.Windows.Controls;
using System.Diagnostics;
using System.Windows.Automation.Peers;
using System.Windows.Media;

namespace GuiTest1
{
    class NM
    {
        private static Socket sender;
        public static bool recieving;
        private static List<List<string>> historyArchive;
        private static bool moreMessages = true;

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
                MessageBox.Show("Error - Could not connect to server");
                return false;
            }
            
            
        }

        public static void SendMessage(string message)
        {
            byte[] msg = Encoding.UTF8.GetBytes(message);

            int bytesSent = sender.Send(msg);
        }

        //public delegate void UpdateListboxCallback(string message);

        private static void RecvLoop()
        {
            ASCIIEncoding ascii = new ASCIIEncoding();
            byte[] bytes = new byte[4096];
            try
            {
                while (recieving == true)
                {
                    int bytesRec = sender.Receive(bytes);
                    string jsonData = Encoding.UTF8.GetString(bytes, 0, bytesRec);

                    Dictionary<string, object> message = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonData);
                    //MessageBox.Show((string)message["messagetype"]);
                    switch (message["messagetype"])
                    {
                        case "outboundMessage":
                            screenMain.pageMain.lbMainMessages.Dispatcher.Invoke(() => {
                                screenMain.pageMain.lbMainMessages.Items.Add(message["content"]);
                                if (VisualTreeHelper.GetChildrenCount(screenMain.pageMain.lbMainMessages) > 0)
                                {
                                    Border border = (Border)VisualTreeHelper.GetChild(screenMain.pageMain.lbMainMessages, 0);
                                    ScrollViewer scrollViewer = (ScrollViewer)VisualTreeHelper.GetChild(border, 0);
                                    scrollViewer.ScrollToBottom();
                                }
                            });
                            break;
                        case "connReqAccepted":
                            serverInfo.Add("channels", message["content"]);
                            MainWindow.main.frame.Dispatcher.Invoke(() => { MainWindow.main.frame.Source = new Uri("screenMain.xaml", UriKind.Relative); });
                            break;
                        case "connReqDenied":
                            NM.DC();
                            break;
                        case "channelMembers":
                            screenMain.pageMain.lbMainChannelMembers.Dispatcher.Invoke(() =>
                            {
                                screenMain.pageMain.lbMainChannelMembers.Items.Clear();
                                List<string> channelMembers = ((Newtonsoft.Json.Linq.JArray)message["content"]).ToObject<List<string>>();

                                foreach (string username in channelMembers)
                                {
                                    screenMain.pageMain.lbMainChannelMembers.Items.Add(username);
                                }
                            });
                            break;
                        case "channelHistory":
                            screenMain.pageMain.lbMainMessages.Dispatcher.Invoke(() =>
                            {
                                screenMain.pageMain.lbMainMessages.Items.Clear();
                                
                                List<List<string>> messageHistory = ((Newtonsoft.Json.Linq.JArray)message["content"]).ToObject<List<List<string>>>();
                                historyArchive = messageHistory;

                                if (messageHistory.Count == 50)
                                    screenMain.pageMain.lbMainMessages.Items.Add("[Load more messages]");

                                foreach (List<string> row in messageHistory)
                                {
                                    screenMain.pageMain.lbMainMessages.Items.Add(row[0] + ": " + row[3]);
                                }
                                if (VisualTreeHelper.GetChildrenCount(screenMain.pageMain.lbMainMessages) > 0)
                                {
                                    Border border = (Border)VisualTreeHelper.GetChild(screenMain.pageMain.lbMainMessages, 0);
                                    ScrollViewer scrollViewer = (ScrollViewer)VisualTreeHelper.GetChild(border, 0);
                                    scrollViewer.ScrollToBottom();
                                }
                                screenMain.timesUpdated = 1;
                            });
                            break;
                        case "userUpdate":
                            if ((string)message["content"] == "username")
                            {
                                screenMain.username = (string)message["username"];
                            }
                            break;
                        case "additionalHistory":
                            {
                                moreMessages = false;
                                foreach (List<string> item in ((Newtonsoft.Json.Linq.JArray)message["content"]).ToObject<List<List<string>>>())
                                {
                                    historyArchive.Insert(0, item);
                                }

                                if (((Newtonsoft.Json.Linq.JArray)message["content"]).ToObject<List<List<string>>>().Count == 50)
                                {
                                    moreMessages = true;
                                }

                                screenMain.pageMain.lbMainMessages.Dispatcher.Invoke(() =>
                                {
                                    screenMain.pageMain.lbMainMessages.Items.Clear();
                                    if (moreMessages == true)
                                    {
                                        screenMain.pageMain.lbMainMessages.Items.Add("[Load more messages]");
                                    }

                                    foreach (List<string> row in historyArchive)
                                    {
                                        screenMain.pageMain.lbMainMessages.Items.Add(row[0] + ": " + row[3]);
                                    }

                                });
                            }
                            break;

                    }
                }
            }
            catch (System.Net.Sockets.SocketException)
            {
                if (recieving == true)
                {
                    MessageBox.Show("Error - Connection Lost");
                    MainWindow.main.Dispatcher.Invoke(() =>
                    {
                        MainWindow.main.frame.Source = new Uri("screenStartup.xaml", UriKind.Relative);
                    });
                    
                    NM.serverInfo.Clear();
                    MainWindow.main.Dispatcher.Invoke(() =>
                    {
                        Application.Current.MainWindow.Height = 350;
                        Application.Current.MainWindow.Width = 525;
                    });
                    
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
            Dictionary<string, object> message = new Dictionary<string, object>();

            message.Add("username", "");
            message.Add("channel", "");
            message.Add("content", "");
            message.Add("messagetype", "disconnect");

            string jsonMessage = JsonConvert.SerializeObject(message);

            NM.SendMessage(jsonMessage);

            recieving = false;
            sender.Close();
        }
    }
}
