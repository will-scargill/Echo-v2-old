using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocketLibrary;
using System.Windows;

namespace GuiTest1
{
    class NM
    {
        private static ConnectedSocket socket;

        public static void Connect(string ip, int port)
        {
            socket = new ConnectedSocket(ip, port);
        }

        public static void SendMessage(string message)
        {
            using (socket)
            {
                socket.Send("Hello world!");
            }
        }

        public static void RecieveMessage()
        {
            if (socket.AnythingToReceive == true)
            {
                using (socket)
                {
                    var data = socket.Receive();
                    MessageBox.Show(data.ToString());
                }
            }
        }

        public static void DC()
        {
            socket.Dispose();
        }
    }
}
