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
    /// Interaction logic for screenMain.xaml
    /// </summary>
    public partial class screenMain : Page
    {
        public static screenMain pageMain;
        public screenMain()
        {
            InitializeComponent();
            pageMain = this;
            Application.Current.MainWindow.Height = 600;
            Application.Current.MainWindow.Width = 1010;
            MainWindow.main.frame.Height = 570;
            MainWindow.main.frame.Width = 992;

            KeyBinding OpenCmdKeyBinding = new KeyBinding(
                btnMainSendMsg_KeyBind(),
                Key.Enter,
                );

            KeyBinding SendMsgOnEnter = new KeyBinding();


            this.InputBindings.Add(OpenCmdKeyBinding);
        }

        private void btnMainSendMsg_Click(object sender, RoutedEventArgs e)
        {
            NM.SendMessage(this.tbMainMessageEntry.Text);
            this.tbMainMessageEntry.Text = "";
        }
        private void btnMainSendMsg_KeyBind()
        {
            NM.SendMessage(this.tbMainMessageEntry.Text);
            this.tbMainMessageEntry.Text = "";
        }
    }
}
