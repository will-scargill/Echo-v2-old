using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Newtonsoft.Json;
using System.Windows.Controls;
using System.Diagnostics;
using System.Windows.Automation.Peers;
using System.Windows.Media;

namespace ECHO
{
    class NM
    {
        private static Socket sender;
        public static bool recieving;
        private static List<Message> historyArchive = new List<Message>();
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
            //var thread = new Thread(() => SendMessageThread(message));
            //thread.Start();

            byte[] msg = Encoding.UTF8.GetBytes(message);

            int bytesSent = sender.Send(msg);

            Debug.WriteLine("Sent Message");
        }

        private static void SendMessageThread(string message)
        {
            
        }

        private static void RecvLoop()
        {
            ASCIIEncoding ascii = new ASCIIEncoding();
            byte[] bytes = new byte[10240];
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
                                List<string> messageContent = ((Newtonsoft.Json.Linq.JArray)message["content"]).ToObject<List<string>>();
                                screenMain.pageMain.lbMainMessages.Items.Add(new Message(messageContent[0].ToString(), messageContent[1].ToString(), messageContent[2].ToString(), messageContent[3].ToString()));
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
                            historyArchive.Clear();
                            screenMain.pageMain.lbMainMessages.Dispatcher.Invoke(() =>
                            {
                                screenMain.pageMain.lbMainMessages.Items.Clear();
                                
                                List<List<string>> messageHistory = ((Newtonsoft.Json.Linq.JArray)message["content"]).ToObject<List<List<string>>>();

                                foreach (List<string> row in messageHistory)
                                {
                                    historyArchive.Add(new Message(row[2], row[0], row[3], row[4]));
                                }

                                //historyArchive = messageHistory;

                                if (messageHistory.Count == 50)
                                    screenMain.pageMain.lbMainMessages.Items.Add(new Message("","system","[Load more messages]", ""));

                                foreach (Message msg in historyArchive)
                                {
                                    screenMain.pageMain.lbMainMessages.Items.Add(msg);
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
                                foreach (List<string> row in ((Newtonsoft.Json.Linq.JArray)message["content"]).ToObject<List<List<string>>>())
                                {
                                    historyArchive.Insert(0,new Message(row[2], row[0], row[3], row[4]));
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
                                        screenMain.pageMain.lbMainMessages.Items.Add(new Message("", "system", "[Load more messages]", ""));
                                    }

                                    foreach (Message msg in historyArchive)
                                    {
                                        screenMain.pageMain.lbMainMessages.Items.Add(msg);
                                    }

                                });
                            }
                            break;
                        case "userKicked":
                            {
                                MainWindow.main.Dispatcher.Invoke(() => {
                                    CFM.UpdateSetting("mainHeight", Convert.ToString(MainWindow.main.ActualHeight));
                                    CFM.UpdateSetting("mainWidth", Convert.ToString(MainWindow.main.ActualWidth));
                                    MainWindow.main.frame.Source = new Uri("screenStartup.xaml", UriKind.Relative);
                                    Application.Current.MainWindow.Height = 350;
                                    Application.Current.MainWindow.Width = 525;
                                    if ((string)message["content"] == "")
                                        MessageBox.Show("Kicked - No reason given");
                                    else
                                        MessageBox.Show("Kicked - " + message["content"]);
                                    NM.DC();                              
                                    NM.serverInfo.Clear();
                                    
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
