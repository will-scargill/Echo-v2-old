using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ECHO
{
    class NM // Network manager
    {
        public static Socket sender;
        public static bool recieving;
        private static List<Message> historyArchive = new List<Message>();
        private static bool moreMessages;

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

            Debug.WriteLine("Sent Message");
        }

        private static void RecvLoop()
        {
            ASCIIEncoding ascii = new ASCIIEncoding();
            byte[] bytes = new byte[20480];
            try
            {
                Queue netQueue = new Queue();

                while (recieving == true)
                {
                    int bytesRec = sender.Receive(bytes);
                    string jsonData = Encoding.UTF8.GetString(bytes, 0, bytesRec);

                    netQueue.Clear();

                    if (jsonData[0].Equals('[') && jsonData[(jsonData.Length - 1)].Equals(']'))
                    {
                        netQueue.Enqueue(jsonData);
                    }
                    else
                    {
                        string incompleteMessage = jsonData;
                        bool messageComplete = false;
                        while (messageComplete == false)
                        {
                            bytesRec = sender.Receive(bytes);
                            jsonData = Encoding.UTF8.GetString(bytes, 0, bytesRec);
                            if (jsonData[(jsonData.Length - 1)].Equals(']'))
                            {
                                messageComplete = true;
                                incompleteMessage += jsonData;

                                netQueue.Enqueue(incompleteMessage);

                                /*
                                bytesRec = sender.Receive(bytes);
                                jsonData = Encoding.UTF8.GetString(bytes, 0, bytesRec);

                                if (jsonData[0].Equals('[') == false || jsonData == null)
                                {
                                    Debug.WriteLine("check 4");
                                }
                                else
                                {
                                    Debug.WriteLine("check 5");
                                    incompleteMessage = "";
                                    messageComplete = false;
                                    incompleteMessage = jsonData;
                                }
                                */
                            }
                            else
                            {
                                incompleteMessage += jsonData;
                            }
                        }
                    }

                    foreach (string netMessage in netQueue)
                    {
                        List<string> encryptedData = JsonConvert.DeserializeObject<List<string>>(netMessage);

                        jsonData = EMAES.Decrypt(encryptedData[0], KeyGenerator.SecretKey, encryptedData[1]);

                        Dictionary<string, object> message = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonData);

                        switch (message["messagetype"])
                        {
                            case "outboundMessage":
                                screenMain.pageMain.lbMainMessages.Dispatcher.Invoke(() =>
                                {
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
                                if (message["content"] as string == "banned")
                                {
                                    MessageBox.Show("You are banned from this server");
                                }
                                else if (message["content"] as string == "password")
                                {
                                    MessageBox.Show("Error - Incorrect password");
                                }
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

                                //if (messageHistory.Count == 50)
                                //screenMain.pageMain.lbMainMessages.Items.Add(new Message("","system","[Load more messages]", ""));

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
                                    if (screenMain.channel == "")
                                        moreMessages = false;
                                    foreach (List<string> row in ((Newtonsoft.Json.Linq.JArray)message["content"]).ToObject<List<List<string>>>())
                                    {
                                        historyArchive.Insert(0, new Message(row[2], row[0], row[3], row[4]));
                                    }

                                    if (((Newtonsoft.Json.Linq.JArray)message["content"]).ToObject<List<List<string>>>().Count == 50)
                                    {
                                        moreMessages = true;
                                    }
                                    else { screenMain.stopUpdating = true; }

                                    screenMain.pageMain.lbMainMessages.Dispatcher.Invoke(() =>
                                    {

                                        object posRetain = screenMain.pageMain.lbMainMessages.Items.GetItemAt(1);

                                        screenMain.pageMain.lbMainMessages.Items.Clear();
                                    //if (moreMessages == true)
                                    //{
                                    //screenMain.pageMain.lbMainMessages.Items.Add(new Message("", "system", "[Load more messages]", ""));
                                    //}

                                    foreach (Message msg in historyArchive)
                                        {
                                            screenMain.pageMain.lbMainMessages.Items.Add(msg);
                                        }
                                    //ScrollToVerticalOffset(row * _grid.RowHeight.Value);
                                    Border border = (Border)VisualTreeHelper.GetChild(screenMain.pageMain.lbMainMessages, 0);
                                        ScrollViewer scrollViewer = (ScrollViewer)VisualTreeHelper.GetChild(border, 0);
                                        screenMain.pageMain.lbMainMessages.ScrollIntoView(posRetain);
                                        int visibleItems = Convert.ToInt32(scrollViewer.ViewportHeight);
                                        screenMain.pageMain.lbMainMessages.SelectedItem = posRetain;
                                        int currentIndex = screenMain.pageMain.lbMainMessages.SelectedIndex;
                                        object scrollDownTo = screenMain.pageMain.lbMainMessages.Items.GetItemAt(currentIndex + visibleItems - 2);
                                        screenMain.pageMain.lbMainMessages.ScrollIntoView(scrollDownTo);

                                    });
                                }
                                break;
                            case "userKicked":
                                {
                                    MainWindow.main.Dispatcher.Invoke(() =>
                                    {
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
                            case "userList":
                                {
                                    screenMain.pageMain.Dispatcher.Invoke(() =>
                                    {
                                        MenuItem subItem = new MenuItem();
                                        MenuItem viewAllUsers = (MenuItem)screenMain.pageMain.menuMain.Items.GetItemAt(1);
                                        viewAllUsers.Items.Clear();
                                        foreach (string item in ((Newtonsoft.Json.Linq.JArray)message["content"]).ToObject<List<string>>())
                                        {
                                            subItem = new MenuItem();
                                            subItem.Header = item;
                                            subItem.Background = new SolidColorBrush(Color.FromArgb(0xFF, (byte)23, (byte)28, (byte)24));
                                            subItem.BorderBrush = new SolidColorBrush(Color.FromArgb(0xFF, (byte)56, (byte)56, (byte)56));
                                            subItem.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, (byte)240, (byte)247, (byte)244));
                                        //subItem.Click = 
                                        //Style style = Application.Current.FindResource("DarkTheme") as Style;
                                        //subItem.Style = style;
                                        viewAllUsers.Items.Add(subItem);
                                        }



                                    });
                                }
                                break;
                        }
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
                    screenMain.channel = "";
                    NM.DC();
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
            NM.serverInfo.Clear();

            Dictionary<string, object> message = new Dictionary<string, object>();

            message.Add("username", "");
            message.Add("channel", "");
            message.Add("content", "");
            message.Add("messagetype", "disconnect");


            string jsonMessage = JsonConvert.SerializeObject(message);

            //NM.SendMessage(jsonMessage);
            screenMain.channel = "";
            recieving = false;
            sender.Close();
        }
    }
}
