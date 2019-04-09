using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace ECHO
{
    /// <summary>
    /// Interaction logic for screenStartup.xaml
    /// </summary>
    public partial class screenStartup : Page
    {
        public static screenStartup pageStartup;
        private static List<string> config;

        public screenStartup()
        {
            InitializeComponent();

            pageStartup = this;
            DBM.SQLInitialise();
            populateServers();

            config = CFM.ReadSettings();

            if (config[4] == "Light Theme")
            {

            }
            else if (config[4] == "Dark Theme")
            {
                VM.DarkTheme("screenStartup");
            }

            Panel mainContainer = (Panel)this.Content;

            /// GetAll UIElement
            UIElementCollection element = mainContainer.Children;

            /// casting the UIElementCollection into List
            List<FrameworkElement> lstElement = element.Cast<FrameworkElement>().ToList();

            /// Geting all Control from list
            var lstControl = lstElement.OfType<Control>();

            foreach (Control control in lstControl)
            {
                Debug.WriteLine(control.GetType().ToString());
            }

            if (Convert.ToBoolean(config[0]) == true)
            {
                this.cbRememberUser.IsChecked = true;
                this.tbStartupUsername.Text = config[1];
            }
        }

        private void bStartupSettings_Click(object sender, RoutedEventArgs e)
        {
            if (config[0] == "true")
            {
                CFM.UpdateSetting("username", Regex.Replace(tbStartupUsername.Text, @" ", ""));
            }
            MainWindow.main.frame.Source = new Uri("screenSettings.xaml", UriKind.Relative);
        }

        private void bStartupExit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void populateServers()
        {
            
            List<List<string>> data = DBM.SQLGetTableData("servers");
            foreach (List<string> row in data)
            {                
                ListBoxItem itm = new ListBoxItem();
                itm.Content = row[1];
                this.lbStartupServers.Items.Add(itm);
            }
        }

        private void cbRememberUser_Click(object sender, RoutedEventArgs e)
        {
            if (this.cbRememberUser.IsChecked == false)
            {
                CFM.UpdateSetting("saveUsername", "false");
            }
            else if (this.cbRememberUser.IsChecked == true)
            {
                CFM.UpdateSetting("saveUsername", "true");
            }
        }

        private void lbStartupServers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void bStartupConn_Click(object sender, RoutedEventArgs e)
        {   
            if (config[0] == "true")
            {
                CFM.UpdateSetting("username", Regex.Replace(tbStartupUsername.Text, @" ", ""));
            }
            if (this.lbStartupServers.SelectedItem == null)
            {
                MessageBox.Show("Error - Please select a server");
            }
            else if (this.tbStartupUsername.Text == "")
            {
                MessageBox.Show("Please enter a username");
            }
            else
            {   
                ListBoxItem lbi = this.lbStartupServers.SelectedItem as ListBoxItem;
                string servName = lbi.Content.ToString();

                List<List<string>> data = DBM.SQLRaw("SELECT * FROM servers WHERE name='" + servName + "'", "servers");
                string servIP = data[0][2];
                int servPort = Convert.ToInt16(data[0][3]);
                bool connected = NM.Connect(servIP, servPort);
                
                if (connected == true)
                {
                    NM.RecieveMessage();

                    string username = tbStartupUsername.Text;
                    username = Regex.Replace(username, @" ", "");

                    List<string> connRequest = new List<string> { username, this.tbStartupPassword.Password };

                    Dictionary<string, object> message = new Dictionary<string, object>();

                    message.Add("username", "");
                    message.Add("channel", "");
                    message.Add("content", connRequest);
                    message.Add("messagetype", "connRequest");

                    string jsonMessage = JsonConvert.SerializeObject(message);

                    NM.SendMessage(jsonMessage);
                }
            }
        }
    }
}


