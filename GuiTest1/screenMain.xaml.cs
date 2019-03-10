using System;
using System.Collections.Generic;
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

namespace GuiTest1
{
    /// <summary>
    /// Interaction logic for screenMain.xaml
    /// </summary>
    public partial class screenMain : Page
    {
        public static screenMain pageMain;
        public static string username;
        public static string channel;

        public screenMain()
        {
            InitializeComponent();
            pageMain = this;
            Application.Current.MainWindow.Height = 600;
            Application.Current.MainWindow.Width = 1010;
            //MainWindow.main.frame.Height = 570;
            //MainWindow.main.frame.Width = 992;

            List<string> channels = JsonConvert.DeserializeObject<List<string>>((NM.serverInfo["channels"]).ToString());

            foreach (string ch in channels)
            {
                this.lbMainChannels.Items.Add(ch);
            }

        }

        private void btnMainSendMsg_Click(object sender, RoutedEventArgs e)
        {
            if (channel != null)
            {
                Dictionary<string, object> message = new Dictionary<string, object>();
                message.Add("username", username);
                message.Add("channel", channel);
                message.Add("content", this.tbMainMessageEntry.Text);
                message.Add("messagetype", "inboundMessage");

                string jsonMessage = JsonConvert.SerializeObject(message);



                NM.SendMessage(jsonMessage);                
            }
            this.tbMainMessageEntry.Text = "";
        }


        private void Grid_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                if (channel != null)
                {
                    Dictionary<string, object> message = new Dictionary<string, object>();
                    message.Add("username", username);
                    message.Add("channel", channel);
                    message.Add("content", this.tbMainMessageEntry.Text);
                    message.Add("messagetype", "inboundMessage");

                    string jsonMessage = JsonConvert.SerializeObject(message);

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
                string channelName = lbi.ToString();
                channel = channelName;
                Dictionary<string, object> message = new Dictionary<string, object>();
                message.Add("username", username);
                message.Add("channel", "");
                message.Add("content", channelName);
                message.Add("messagetype", "changedChannel");

                string jsonMessage = JsonConvert.SerializeObject(message);

                NM.SendMessage(jsonMessage);
            }
            
        }

        private void MenuDisconnect_Click(object sender, RoutedEventArgs e)
        {
            NM.DC();
        }
    }
}
