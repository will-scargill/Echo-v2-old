using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;

namespace EMessenger
{
    /// <summary>
    /// Interaction logic for screenMain.xaml
    /// </summary>
    public partial class screenMain : Page
    {
        public static screenMain pageMain;
        public static string username = "";
        public static string channel = "";
        public static int timesUpdated = 0;
        public static bool stopUpdating = false;


        public screenMain()
        {
            InitializeComponent();

            pageMain = this;
            List<string> config = CFM.ReadSettings();

            Application.Current.MainWindow.Height = Convert.ToDouble(config[2]);
            Application.Current.MainWindow.Width = Convert.ToDouble(config[3]);
            //MainWindow.main.frame.Height = 570;
            //MainWindow.main.frame.Width = 992;

            MainWindow.main.ResizeMode = ResizeMode.CanResize;

            List<string> channels = JsonConvert.DeserializeObject<List<string>>((NM.serverInfo["channels"]).ToString());

            foreach (string ch in channels)
            {
                this.lbMainChannels.Items.Add(ch);
            }

            if (config[4] == "Light Theme")
            {

            }
            else if (config[4] == "Dark Theme")
            {
                VM.DarkTheme("screenMain");
            }
        }

        private void btnMainSendMsg_Click(object sender, RoutedEventArgs e)
        {
            if (channel != "" && this.tbMainMessageEntry.Text != "")
            {
                Dictionary<string, object> message = new Dictionary<string, object>();
                message.Add("username", username);
                message.Add("channel", channel);
                message.Add("content", this.tbMainMessageEntry.Text);
                message.Add("messagetype", "inboundMessage");

                List<object> encryptedMessage = EMAES.Encrypt(JsonConvert.SerializeObject(message));

                string jsonMessage = JsonConvert.SerializeObject(encryptedMessage);



                NM.SendMessage(jsonMessage);                
            }
            this.tbMainMessageEntry.Text = "";
        }


        private void Grid_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                if (channel != "" && this.tbMainMessageEntry.Text != "")
                {
                    Dictionary<string, object> message = new Dictionary<string, object>();
                    message.Add("username", username);
                    message.Add("channel", channel);
                    message.Add("content", this.tbMainMessageEntry.Text);
                    message.Add("messagetype", "inboundMessage");

                    List<object> encryptedMessage = EMAES.Encrypt(JsonConvert.SerializeObject(message));

                    string jsonMessage = JsonConvert.SerializeObject(encryptedMessage);

                    NM.SendMessage(jsonMessage);
                }
                this.tbMainMessageEntry.Text = "";
            }
        }

        private void lbMainChannels_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            object lbi = this.lbMainChannels.SelectedItem;
            if (lbi == null) { }
            else
            {
                stopUpdating = false;
                string channelName = lbi.ToString();
                channel = channelName;
                Dictionary<string, object> message = new Dictionary<string, object>();
                message.Add("username", username);
                message.Add("channel", "");
                message.Add("content", channelName);
                message.Add("messagetype", "changedChannel");

                List<object> encryptedMessage = EMAES.Encrypt(JsonConvert.SerializeObject(message));

                string jsonMessage = JsonConvert.SerializeObject(encryptedMessage);

                NM.SendMessage(jsonMessage);
            }
            
        }

        private void MenuDisconnect_Click(object sender, RoutedEventArgs e)
        {
            NM.DC();
        }

        private void MenuItem_Disconn_Click(object sender, RoutedEventArgs e)
        {
            NM.DC();
            CFM.UpdateSetting("mainHeight", Convert.ToString(MainWindow.main.ActualHeight));
            CFM.UpdateSetting("mainWidth", Convert.ToString(MainWindow.main.ActualWidth));
            MainWindow.main.frame.Source = new Uri("screenStartup.xaml", UriKind.Relative);
            NM.serverInfo.Clear();
            channel = "";
            Application.Current.MainWindow.Height = 350;
            Application.Current.MainWindow.Width = 525;            
        }

        private void lbMainMessages_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Message messageContent = (Message)this.lbMainMessages.SelectedItem;
            if (messageContent == null) { }
            else if (messageContent.Content == "[Load more messages]")
            {
                screenMain.timesUpdated += 1;
                Dictionary<string, object> message = new Dictionary<string, object>();

                message.Add("username", "");
                message.Add("channel", channel);
                message.Add("content", timesUpdated);
                message.Add("messagetype", "messageReq");

                List<object> encryptedMessage = EMAES.Encrypt(JsonConvert.SerializeObject(message));

                string jsonMessage = JsonConvert.SerializeObject(encryptedMessage);

                NM.SendMessage(jsonMessage);
                
            }
        }

        private void MenuItem_ViewAll_Click(object sender, RoutedEventArgs e)
        {


        }

        private void ChannelMembers_Context_Whois(object sender, RoutedEventArgs e)
        {
            if (lbMainChannelMembers.SelectedItem == null) { }
            else
            {
                string target = lbMainChannelMembers.SelectedItem.ToString();
                if (target.Substring((target.Length - 2), 2) == " \u2606")
                {
                    Dictionary<string, object> message = new Dictionary<string, object>();
                    message.Add("username", username);
                    message.Add("channel", channel);
                    message.Add("content", "/whois " + target.Substring(0, target.Length - 2));
                    message.Add("messagetype", "inboundMessage");

                    List<object> encryptedMessage = EMAES.Encrypt(JsonConvert.SerializeObject(message));

                    string jsonMessage = JsonConvert.SerializeObject(encryptedMessage);



                    NM.SendMessage(jsonMessage);
                }
                else
                {
                    Dictionary<string, object> message = new Dictionary<string, object>();
                    message.Add("username", username);
                    message.Add("channel", channel);
                    message.Add("content", "/whois " + target);
                    message.Add("messagetype", "inboundMessage");

                    List<object> encryptedMessage = EMAES.Encrypt(JsonConvert.SerializeObject(message));

                    string jsonMessage = JsonConvert.SerializeObject(encryptedMessage);



                    NM.SendMessage(jsonMessage);
                }
            }
        }

        private void ChannelMembers_Context_Kick(object sender, RoutedEventArgs e)
        {
            if (lbMainChannelMembers.SelectedItem == null) { }
            else
            {
                string target = lbMainChannelMembers.SelectedItem.ToString();
                if (target.Substring((target.Length - 2), 2) == " \u2606")
                {
                    Dictionary<string, object> message = new Dictionary<string, object>();
                    message.Add("username", username);
                    message.Add("channel", channel);
                    message.Add("content", "/kick " + target.Substring(0, target.Length - 2));
                    message.Add("messagetype", "inboundMessage");

                    List<object> encryptedMessage = EMAES.Encrypt(JsonConvert.SerializeObject(message));

                    string jsonMessage = JsonConvert.SerializeObject(encryptedMessage);



                    NM.SendMessage(jsonMessage);
                }
                else
                {
                    Dictionary<string, object> message = new Dictionary<string, object>();
                    message.Add("username", username);
                    message.Add("channel", channel);
                    message.Add("content", "/kick " + target);
                    message.Add("messagetype", "inboundMessage");

                    List<object> encryptedMessage = EMAES.Encrypt(JsonConvert.SerializeObject(message));

                    string jsonMessage = JsonConvert.SerializeObject(encryptedMessage);



                    NM.SendMessage(jsonMessage);
                }
            }
        }

        private void ChannelMembers_Context_Ban(object sender, RoutedEventArgs e)
        {
            if (lbMainChannelMembers.SelectedItem == null) { }
            else
            {
                string target = lbMainChannelMembers.SelectedItem.ToString();
                if (target.Substring((target.Length - 2), 2) == " \u2606")
                {
                    Dictionary<string, object> message = new Dictionary<string, object>();
                    message.Add("username", username);
                    message.Add("channel", channel);
                    message.Add("content", "/ban " + target.Substring(0, target.Length - 2));
                    message.Add("messagetype", "inboundMessage");

                    List<object> encryptedMessage = EMAES.Encrypt(JsonConvert.SerializeObject(message));

                    string jsonMessage = JsonConvert.SerializeObject(encryptedMessage);



                    NM.SendMessage(jsonMessage);
                }
                else
                {
                    Dictionary<string, object> message = new Dictionary<string, object>();
                    message.Add("username", username);
                    message.Add("channel", channel);
                    message.Add("content", "/ban " + target);
                    message.Add("messagetype", "inboundMessage");

                    List<object> encryptedMessage = EMAES.Encrypt(JsonConvert.SerializeObject(message));

                    string jsonMessage = JsonConvert.SerializeObject(encryptedMessage);



                    NM.SendMessage(jsonMessage);
                }
            }
        }

        private void Messages_Context_Copy(object sender, RoutedEventArgs e)
        {
            if (lbMainMessages.SelectedItem == null) { }
            else
            {
                Message item = (Message)lbMainMessages.SelectedItem;
                Clipboard.SetText(item.Content);
            }
        }

        private void lbMainMessages_ScrollBar_MouseUp(object sender, RoutedEventArgs e)
        {
            if (channel != "" && stopUpdating == false && lbMainMessages.Items.Count >= 50)
            {   

                Border border = (Border)VisualTreeHelper.GetChild(lbMainMessages, 0);
                ScrollViewer scrollViewer = (ScrollViewer)VisualTreeHelper.GetChild(border, 0);

                scrollViewer.ReleaseMouseCapture();

                if (scrollViewer.VerticalOffset == 0)
                {
                    screenMain.timesUpdated += 1;
                    Dictionary<string, object> message = new Dictionary<string, object>();

                    message.Add("username", "");
                    message.Add("channel", channel);
                    message.Add("content", timesUpdated);
                    message.Add("messagetype", "messageReq");

                    List<object> encryptedMessage = EMAES.Encrypt(JsonConvert.SerializeObject(message));

                    string jsonMessage = JsonConvert.SerializeObject(encryptedMessage);

                    NM.SendMessage(jsonMessage);
                }
            }

        }

        private void viewAllUsers_Click(object sender, RoutedEventArgs e)
        {
            if (true)
            {
                Dictionary<string, object> message = new Dictionary<string, object>
                {
                    { "username", "" },
                    { "channel", "" },
                    { "content", "" },
                    { "messagetype", "userReq" }
                };

                List<object> encryptedMessage = EMAES.Encrypt(JsonConvert.SerializeObject(message));

                string jsonMessage = JsonConvert.SerializeObject(encryptedMessage);

                NM.SendMessage(jsonMessage);
            }
            else
            {
                //MessageBox.Show("something else");
            }
        }

        private void viewAllUsers_Whois(object sender, RoutedEventArgs e)
        {
            if (lbMainChannelMembers.SelectedItem == null) { }
            else
            {
                string target = lbMainChannelMembers.SelectedItem.ToString();

                Dictionary<string, object> message = new Dictionary<string, object>();
                message.Add("username", username);
                message.Add("channel", channel);
                message.Add("content", "/whois " + target);
                message.Add("messagetype", "inboundMessage");

                List<object> encryptedMessage = EMAES.Encrypt(JsonConvert.SerializeObject(message));

                string jsonMessage = JsonConvert.SerializeObject(encryptedMessage);

            }
        }

        private void viewAllUsers_Kick(object sender, RoutedEventArgs e)
        {
            if (lbMainChannelMembers.SelectedItem == null) { }
            else
            {
                string target = lbMainChannelMembers.SelectedItem.ToString();

                Dictionary<string, object> message = new Dictionary<string, object>();
                message.Add("username", username);
                message.Add("channel", channel);
                message.Add("content", "/kick " + target);
                message.Add("messagetype", "inboundMessage");

                List<object> encryptedMessage = EMAES.Encrypt(JsonConvert.SerializeObject(message));

                string jsonMessage = JsonConvert.SerializeObject(encryptedMessage);

            }
        }

        private void viewAllUsers_Ban(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(sender.ToString());
            if (sender == null) { }
            else
            {
                string target = lbMainChannelMembers.SelectedItem.ToString();

                Dictionary<string, object> message = new Dictionary<string, object>();
                message.Add("username", username);
                message.Add("channel", channel);
                message.Add("content", "/ban " + target);
                message.Add("messagetype", "inboundMessage");

                List<object> encryptedMessage = EMAES.Encrypt(JsonConvert.SerializeObject(message));

                string jsonMessage = JsonConvert.SerializeObject(encryptedMessage);

            }
        }
    }
}
