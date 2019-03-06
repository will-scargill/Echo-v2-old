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

namespace GuiTest1
{
    /// <summary>
    /// Interaction logic for screenStartup.xaml
    /// </summary>
    public partial class screenStartup : Page
    {
        public static screenStartup pageStartup;
        public screenStartup()
        {
            InitializeComponent();
            pageStartup = this;
            DBM.SQLInitialise();
            populateServers();
            List<string> settings = CFM.ReadSettings();
            


            if (Convert.ToBoolean(settings[0]) == true)
            {
                this.cbRememberUser.IsChecked = true;
                this.tbStartupUsername.Text = settings[1];
            }
        }

        private void bStartupSettings_Click(object sender, RoutedEventArgs e)
        {
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
            NM.Connect("127.0.0.1", 15000);
            NM.RecieveMessage();
            MainWindow.main.frame.Source = new Uri("screenMain.xaml", UriKind.Relative);

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            NM.SendMessage("Hi there");
        }
    }
}


