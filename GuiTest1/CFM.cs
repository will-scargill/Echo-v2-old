using System;
using System.Configuration;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Xml;

namespace GuiTest1
{
    class CFM
    {
        public static List<string> ReadSettings()
        {
            List<string> data = new List<string> { };
            string saveUsername = ConfigurationManager.AppSettings.Get("saveUsername");
            string username = ConfigurationManager.AppSettings.Get("username");

            data.Add(saveUsername);
            data.Add(username);

            return data;
            
        }

        public static void UpdateSetting(string setting, string newValue)
        {
            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

            foreach (XmlElement element in xmlDoc.DocumentElement)
            {
                if (element.Name.Equals("appSettings"))
                {
                    foreach (XmlNode node in element.ChildNodes)
                    {
                        if (node.Attributes[0].Value.Equals(setting))
                        {
                            node.Attributes[1].Value = newValue;
                        }
                    }
                }
            }

            xmlDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

            ConfigurationManager.RefreshSection("appSettings");
        }
    }
}
