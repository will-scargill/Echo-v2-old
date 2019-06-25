using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace EMessenger
{
    class VM // Visual Manager
    {
        private static Panel mainContainer;

        public static void LightTheme(string pageName)
        {
            
            MainWindow.main.Background = new SolidColorBrush(Color.FromArgb(0xFF, (byte)255, (byte)255, (byte)255));


            switch (pageName)
            {
                case "screenSettings":
                    {
                        mainContainer = (Panel)screenSettings.pageSettings.Content;
                        //MessageBox.Show("screenSettings");
                        break;
                    }
            }
            
            /// GetAll UIElement
            UIElementCollection element = mainContainer.Children;

            /// casting the UIElementCollection into List
            List<FrameworkElement> lstElement = element.Cast<FrameworkElement>().ToList();


            /// Geting all Control from list
            var lstControl = lstElement.OfType<Control>();

            foreach (Control control in lstControl)
            {
                //Debug.WriteLine(control.GetType().ToString());
                ///Hide all Controls
                switch (control.GetType().ToString())
                {
                    case "System.Windows.Controls.Label":
                        {
                            control.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, (byte)0, (byte)0, (byte)0));
                            control.Background = new SolidColorBrush(Color.FromArgb(0xFF, (byte)255, (byte)255, (byte)255));
                            break;
                        }
                    case "System.Windows.Controls.TextBox":
                        {
                            control.Background = new SolidColorBrush(Color.FromArgb(0xFF, (byte)255, (byte)255, (byte)255));
                            control.BorderBrush = new SolidColorBrush(Color.FromArgb(0xFF, (byte)171, (byte)173, (byte)179));
                            control.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, (byte)0, (byte)0, (byte)0));
                            break;
                        }
                    case "System.Windows.Controls.Button":
                        {
                            control.Background = new SolidColorBrush(Color.FromArgb(0xFF, (byte)221, (byte)221, (byte)221));
                            control.BorderBrush = new SolidColorBrush(Color.FromArgb(0xFF, (byte)172, (byte)172, (byte)172));
                            control.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, (byte)0, (byte)0, (byte)0));
                            Style style = Application.Current.FindResource("LightTheme") as Style;
                            control.Style = style;
                            break;
                        }
                    case "System.Windows.Controls.ListBox":
                        {
                            control.Background = new SolidColorBrush(Color.FromArgb(0xFF, (byte)255, (byte)255, (byte)255));
                            control.BorderBrush = new SolidColorBrush(Color.FromArgb(0xFF, (byte)171, (byte)173, (byte)179));
                            control.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, (byte)0, (byte)0, (byte)0));
                            break;
                        }
                }
            }
        }

        public static void GenericUpdateDark(object obj)
        {
            ((Control)obj).Background = new SolidColorBrush(Color.FromArgb(0xFF, (byte)23, (byte)28, (byte)24));
            ((Control)obj).BorderBrush = new SolidColorBrush(Color.FromArgb(0xFF, (byte)56, (byte)56, (byte)56));
            ((Control)obj).Foreground = new SolidColorBrush(Color.FromArgb(0xFF, (byte)240, (byte)247, (byte)244));
        }

        public static void DarkTheme(string pageName)
        {
            MainWindow.main.Background = new SolidColorBrush(Color.FromArgb(0xFF, (byte)255, (byte)255, (byte)255));
            MainWindow.main.Background = new SolidColorBrush(Color.FromArgb(0xFF, (byte)20, (byte)25, (byte)21));


            switch (pageName)
            {
                case "screenSettings":
                    {
                        mainContainer = (Panel)screenSettings.pageSettings.Content;
                        //MessageBox.Show("screenSettings");
                        break;
                    }
                case "screenStartup":
                    {
                        mainContainer = (Panel)screenStartup.pageStartup.Content;
                        //MessageBox.Show("screenStartup");
                        break;
                    }
                case "screenMain":
                    {
                        mainContainer = (Panel)screenMain.pageMain.Content;
                        GenericUpdateDark(screenMain.pageMain.lbMainMessages);
                        GenericUpdateDark(screenMain.pageMain.lbMainChannels);
                        GenericUpdateDark(screenMain.pageMain.lbMainChannelMembers);
                        GenericUpdateDark(screenMain.pageMain.tbMainMessageEntry);
                        GenericUpdateDark(screenMain.pageMain.btnMainSendMsg);
                        GenericUpdateDark(screenMain.pageMain.lblMainChannelMembers);
                        GenericUpdateDark(screenMain.pageMain.lblMainChannels);
                        break;
                    }
            }

            /// GetAll UIElement
            UIElementCollection element = mainContainer.Children;

            /// casting the UIElementCollection into List
            List<FrameworkElement> lstElement = element.Cast<FrameworkElement>().ToList();


            /// Geting all Control from list
            var lstControl = lstElement.OfType<Control>();

            foreach (Control control in lstControl)
            {
                //Debug.WriteLine(control.GetType().ToString());
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
                            Style style = Application.Current.FindResource("DarkTheme") as Style;
                            control.Style = style;
                            break;
                        }
                    case "System.Windows.Controls.ListBox":
                        {
                            control.Background = new SolidColorBrush(Color.FromArgb(0xFF, (byte)23, (byte)28, (byte)24));
                            control.BorderBrush = new SolidColorBrush(Color.FromArgb(0xFF, (byte)56, (byte)56, (byte)56));
                            control.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, (byte)240, (byte)247, (byte)244));
                            break;
                        }
                    case "System.Windows.Controls.Menu":
                        {
                            control.Background = new SolidColorBrush(Color.FromArgb(0xFF, (byte)23, (byte)28, (byte)24));
                            control.BorderBrush = new SolidColorBrush(Color.FromArgb(0xFF, (byte)56, (byte)56, (byte)56));
                            control.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, (byte)240, (byte)247, (byte)244));
                            break;
                        }
                    case "System.Windows.Controls.PasswordBox":
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
