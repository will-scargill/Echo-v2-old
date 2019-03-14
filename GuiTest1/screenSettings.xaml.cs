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

            List<string> config = CFM.ReadSettings();

            cboSettingsClrSch.SelectedValue = config[4];

            //bSettingsNewServ.Background = new SolidColorBrush(Color.FromArgb(0xFF, (byte)60, (byte)73, (byte)63));

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
                List<string> newServer = new List<string> { newName, newIP, newPort };

                DBM.SQLWriteToTable(newServer, "servers");

                populateServers();
            }
            else
            {
                MessageBox.Show("Error - Cannot use duplicate server name");
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

                    }
                }
                else if (cboSettingsClrSch.SelectedValue.ToString() == "Dark Theme")
                {
                    if (config[4] == "Dark Theme") { }
                    else
                    {
                        MainWindow.main.Background = new SolidColorBrush(Color.FromArgb(0xFF, (byte)20, (byte)25, (byte)21));

                        Panel mainContainer = (Panel)this.Content;

                        /// GetAll UIElement
                        UIElementCollection element = mainContainer.Children;

                        /// casting the UIElementCollection into List
                        List<FrameworkElement> lstElement = element.Cast<FrameworkElement>().ToList();

                        /// Geting all Control from list
                        var lstControl = lstElement.OfType<Control>();

                        foreach (Control control in lstControl)
                        {
                            ///Hide all Controls
                            switch (control.GetType().ToString())
                            {
                                case "System.Windows.Controls.Label":
                                    {
                                        control.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, (byte)240, (byte)247, (byte)244));
                                        break;
                                    }
                                case "System.Windows.Controls.TextBox":
                                    {
                                        control.Background = new SolidColorBrush(Color.FromArgb(0xFF, (byte)23, (byte)28, (byte)24));
                                        control.BorderBrush = new SolidColorBrush(Color.FromArgb(0xFF, (byte)56, (byte)56, (byte)56));
                                        control.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, (byte)240, (byte)247, (byte)244));
                                        break;
                                    }
                                case "System.Windows.Controls.Button":
                                    {
                                        control.Background = new SolidColorBrush(Color.FromArgb(0xFF, (byte)23, (byte)28, (byte)24));
                                        control.BorderBrush = new SolidColorBrush(Color.FromArgb(0xFF, (byte)56, (byte)56, (byte)56));
                                        control.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, (byte)240, (byte)247, (byte)244));
                                        break;
                                    }
                                case "System.Windows.Controls.ListBox":
                                    {
                                        control.Background = new SolidColorBrush(Color.FromArgb(0xFF, (byte)23, (byte)28, (byte)24));
                                        control.BorderBrush = new SolidColorBrush(Color.FromArgb(0xFF, (byte)56, (byte)56, (byte)56));
                                        control.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, (byte)240, (byte)247, (byte)244));
                                        break;
                                    }
                                case "System.Windows.Controls.ComboBox":
                                    {
                                        control.Background = new SolidColorBrush(Color.FromArgb(0xFF, (byte)23, (byte)28, (byte)24));
                                        control.BorderBrush = new SolidColorBrush(Color.FromArgb(0xFF, (byte)56, (byte)56, (byte)56));
                                        control.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, (byte)240, (byte)247, (byte)244));
                                        break;
                                    }
                            }


                        }
                    }
                }
            }

            
        }
    }
}
