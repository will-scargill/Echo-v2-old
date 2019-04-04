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

        private string hour;
        private string minute;
        private string day;
        private string month;

        public string Datetime
        {
            get { return this._datetime; }
            set
            {
                if (systemMessage == true)
                {
                    this._datetime = "";
                }
                else
                {
                    DateTime valDT = Convert.ToDateTime(value);
                    DateTime currentDT = DateTime.Now;

                    int timeCheck = DateTime.Compare(valDT, currentDT);

                    if (timeCheck < 0)
                    {
                        if (valDT.Date == currentDT.Date)
                        {
                            if (valDT.Minute < 10)
                            { minute = "0" + valDT.Minute.ToString(); }
                            else
                            { minute = valDT.Minute.ToString(); }
                            if (valDT.Hour < 10)
                            { hour = "0" + valDT.Hour.ToString(); }
                            else
                            { hour = valDT.Hour.ToString(); }

                            this._datetime = hour + ":" + minute;
                        }
                        //{ this._datetime = value.Substring(11, 5); }
                        else
                        {
                            if (valDT.Day < 10)
                            { day = "0" + valDT.Day.ToString(); }
                            else
                            { day = valDT.Day.ToString(); }
                            if (valDT.Month < 10)
                            { month = "0" + valDT.Month.ToString(); }
                            else
                            { month = valDT.Month.ToString(); }

                            this._datetime = day + "/" + month;
                        }
                        //{ this._datetime = value.Substring(0, 6) + value.Substring(8, 2); }
                    }
                    else if (timeCheck == 0)
                    {
                        if (valDT.Minute < 10)
                        { minute = "0" + valDT.Minute.ToString(); }
                        else
                        { minute = valDT.Minute.ToString(); }
                        if (valDT.Hour < 10)
                        { hour = "0" + valDT.Hour.ToString(); }
                        else
                        { hour = valDT.Hour.ToString(); }

                        this._datetime = hour + ":" + minute;
                    }
                }
                
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
                    //Debug.WriteLine(value);
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
