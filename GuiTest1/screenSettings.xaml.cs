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

namespace EMessenger
{
    /// <summary>
    /// Interaction logic for screenSettings.xaml
    /// </summary>
    public partial class screenSettings : Page
    {
        public static screenSettings pageSettings;
        public screenSettings()
        {
            InitializeComponent();
            pageSettings = this;
            DBM.SQLInitialise();
            populateServers();

            List<string> config = CFM.ReadSettings();

            cboSettingsClrSch.SelectedValue = config[4];

            if (config[4] == "Light Theme")
            {

            }
            else if (config[4] == "Dark Theme")
            {
                VM.DarkTheme("screenSettings");
            }
        }

        private void button_Copy2_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.main.frame.Source = new Uri("screenStartup.xaml", UriKind.Relative);
        }

        private void populateServers()
        {
            this.lbSettingsServers.Items.Clear();
            List<List<string>> data = DBM.SQLGetTableData("servers");
            foreach (List<string> row in data)
            {
                //foreach (string item in row)
                //{
                    //System.Diagnostics.Debug.WriteLine(item);
                //}
                ListBoxItem itm = new ListBoxItem();
                itm.Content = row[1];
                this.lbSettingsServers.Items.Add(itm);
            }
        }

        private void lbSettingsServers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBoxItem lbi = ((sender as ListBox).SelectedItem as ListBoxItem);
            if (lbi == null) {}            
            else
            {
                string serverName = lbi.Content.ToString();
                List<List<string>> data = DBM.SQLRaw("SELECT * FROM servers WHERE name = '" + serverName + "'", "servers");
                this.tbSettingsName.Text = data[0][1].ToString();
                this.tbSettingsIP.Text = data[0][2].ToString();
                this.tbSettingsPort.Text = data[0][3].ToString();
            }
        }

        private void bSettingsSaveServ_Click(object sender, RoutedEventArgs e)
        {
            ListBoxItem lbi = this.lbSettingsServers.SelectedItem as ListBoxItem;
            if (lbi == null)
            {
                MessageBox.Show("Error - Please select a server");
            }
            else
            {
                string serverName = lbi.Content.ToString();
                string newName = this.tbSettingsName.Text;
                string newIP = this.tbSettingsIP.Text;
                string newPort = this.tbSettingsPort.Text;

                List<List<string>> data = DBM.SQLRaw("UPDATE servers SET name='" + newName + "', ip='" + newIP + "', port=" + newPort + " WHERE name='" + serverName + "'", "servers");
                populateServers();
            }
        }

        private void bSettingsNewServ_Click(object sender, RoutedEventArgs e)
        {
            string newName = this.tbSettingsName.Text;
            string newIP = this.tbSettingsIP.Text;
            string newPort = this.tbSettingsPort.Text;
            bool check = true;
            if (newName == "" || newIP == "" || newPort == "")
            {
                MessageBox.Show("Error - Please enter details before creating new server");
            }
            else
            {
                List<List<string>> data = DBM.SQLGetTableData("servers");
                foreach (List<string> row in data)
                {
                    if (row[1] == newName)
                    {
                        check = false;
                    }
                }
                if (check == true)
                {
                    try
                    {
                        List<string> newServer = new List<string> { newName, newIP, newPort };

                        DBM.SQLWriteToTable(newServer, "servers");

                        populateServers();
                    }
                    catch
                    {
                        MessageBox.Show("Error - Inputted information was in an invalid format");
                    }
                }
                else
                {
                    MessageBox.Show("Error - Cannot use duplicate server name");
                }
            }

            
        }

        private void bSettingsDelServ_Click(object sender, RoutedEventArgs e)
        {
            ListBoxItem lbi = this.lbSettingsServers.SelectedItem as ListBoxItem;
            if (lbi == null)
            { }
            else
            {
                string servName = lbi.Content.ToString();
                List<List<string>> data = DBM.SQLRaw("DELETE FROM servers WHERE name='" + servName + "'", "servers");
                populateServers();
                tbSettingsIP.Text = "";
                tbSettingsName.Text = "";
                tbSettingsPort.Text = "";
            }
            
            
        }

        private void cboSettingsClrSch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<string> config = CFM.ReadSettings();
            if (cboSettingsClrSch.SelectedValue == null) { }
            else
            {
                if (cboSettingsClrSch.SelectedValue.ToString() == "Light Theme")
                {
                    if (config[4] == "Light Theme") { }
                    else
                    {
                        VM.LightTheme("screenSettings");
                        CFM.UpdateSetting("colourScheme", "Light Theme");
                    }
                }
                else if (cboSettingsClrSch.SelectedValue.ToString() == "Dark Theme")
                {
                    if (config[4] == "Dark Theme") { }
                    else
                    {
                        VM.DarkTheme("screenSettings");
                        CFM.UpdateSetting("colourScheme", "Dark Theme");
                    }
                }
            }

            
        }

        private void toggleButton_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
