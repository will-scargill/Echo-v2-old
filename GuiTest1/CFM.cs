using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace GuiTest1
{
    class CFM
    {
        public static object ConfigurationManager { get; private set; }

        public static void fetchUserSettings()
        {
            int value = Int32.Parse(ConfigurationManager.AppSettings["StartingMonthColumn"]);
        }

    }
}
