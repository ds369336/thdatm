using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataLibrary
{
    public class XmlConnection
    {
        private int id;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        private string profile;

        public string Profile
        {
            get { return profile; }
            set { profile = value; }
        }

        private string connectionString;

        public string ConnectionString
        {
            get { return connectionString; }
            set { connectionString = value; }
        }

        public string UserService
        {
            get
            {
                try
                {
                    string resu = "";
                    resu = this.GetConnectionElement("User ID") + "@" + this.GetConnectionElement("Data Source");
                    if (resu.Trim() == "@") throw new Exception();
                    return resu;
                }
                catch
                {
                    return "";
                }
            }
        }

        private String GetConnectionElement(String elementName)
        {
            String result = "";
            try
            {
                String[] conArray = connectionString.Split(';');
                for (int i = 0; i < conArray.Length; i++)
                {
                    if (conArray[i].IndexOf(elementName) == 0)
                    {
                        String[] ar2 = conArray[i].Split('=');
                        result = ar2[1].Trim();
                        break;
                    }
                }
            }
            catch { }
            return result;
        }
    }
}