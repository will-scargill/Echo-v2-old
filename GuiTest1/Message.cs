using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECHO
{
    class Message
    {
        private bool systemMessage = false;

        private string _username;
        public string Username
        {
            get { return this._username; }
            set
            {
                if (value == "system")
                {
                    systemMessage = true;
                    _username = "";
                }
                else
                {
                    _username = value;
                }
            }
        }
        public string Content { get; set; }

        private string _datetime;

        public string Datetime
        {
            get { return this._datetime; }
            set
            {
                if (systemMessage == true)
                {
                    this._datetime = "";
                }
                else if (Convert.ToDateTime(value).Day < DateTime.Now.Day)
                { this._datetime = value.Substring(0, 6) + value.Substring(8, 2); }
                else
                { this._datetime = value.Substring(11, 5); } 
            }
        }

        private string _colour;

        public string Colour
        {
            get { return this._colour; }
            set
            {
                if (value == "")
                    this._colour = "White";
                else
                    Debug.WriteLine(value);
                    this._colour = value;
            }
        }

        public Message (string datetime, string username, string content, string colour)
        {
            Content = content;
            Username = username;
            Datetime = datetime;
            Colour = colour;
        }


    }
}
