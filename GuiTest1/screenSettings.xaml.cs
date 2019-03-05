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

        private void fetchServerDetails()
        {

        }

        private void lbSettingsServers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBoxItem lbi = ((sender as ListBox).SelectedItem as ListBoxItem);
            if (lbi == null) {}            
            else
            {
                string serverName = lbi.Content.ToString();
                List<List<object>> data = DBM.SQLRaw("SELECT * FROM servers WHERE name = '" + serverName + "'", "servers");
                this.tbSettingsName.Text = data[0][1].ToString();
                this.tbSettingsIP.Text = data[0][2].ToString();
                this.tbSettingsPort.Text = data[0][3].ToString();
            }
        }

        private void bSettingsSaveServ_Click(object sender, RoutedEventArgs e)
        {
            ListBoxItem lbi = this.lbSettingsServers.SelectedItem as ListBoxItem;
            string serverName = lbi.Content.ToString();
            string newName = this.tbSettingsName.Text;
            string newIP = this.tbSettingsIP.Text;
            string newPort = this.tbSettingsPort.Text;

            List<List<object>> data = DBM.SQLRaw("UPDATE servers SET name='"+newName+"', ip='"+newIP+"', port="+newPort+" WHERE name='"+serverName+"'", "servers");
            
            populateServers();

        }

        private void bSettingsNewServ_Click(object sender, RoutedEventArgs e)
        {
            string newName = this.tbSettingsName.Text;
            string newIP = this.tbSettingsIP.Text;
            string newPort = this.tbSettingsPort.Text;

            List<string> data = new List<string> { newName, newIP, newPort };

            DBM.SQLWriteToTable(data, "servers");

            populateServers();
        }
    }
}
