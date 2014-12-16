using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Web.UI;

namespace DataLibrary
{
    public class XmlConfigSQL
    {
        private string xmlConfigPath = "";
        private string wsPath = "";
        private string savPath = "";
        private string ssoPath = "";
        private string physicalPath = "C:\\GCOOP_ALL\\ATM_BANK\\CoreCoop";

        private DataTable dtXmlConfig = null;

        private DataTable XmlServiceData
        {
            get
            {
                if (this.dtXmlConfig != null) return dtXmlConfig;
                String path = xmlConfigPath + "\\xmlconf.sqlmap.xml";
                try
                {
                    if (File.Exists(path))
                    {
                        DataSet ds = new DataSet();
                        ds.ReadXml(path);
                        DataTable dt = ds.Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            return dt;
                        }
                    }
                }
                catch { }
                return null;
            }
        }

        public DataTable ConnectionStringData
        {
            get
            {
                String path = "";
                try
                {
                    if (xmlConfigPath != "")
                    {
                        path = xmlConfigPath + "\\server.connection_string.xml";
                    }
                    else
                    {

                        string filePath = "C:\\TEMP\\gcoop_path.txt";
                        if (File.Exists(filePath))
                        {
                            StreamReader reader = new StreamReader(filePath);
                            path = reader.ReadLine() + @"XMLConfig\server.connection_string.xml";
                            reader.Close();
                        }
                    }
                }
                catch { }
                try
                {
                    if (File.Exists(path))
                    {
                        DataSet ds = new DataSet();
                        ds.ReadXml(path);
                        DataTable dt = ds.Tables[0];
                        dt.TableName = "xmlconnectionstring";
                        if (dt.Rows.Count > 0)
                        {
                            return dt;
                        }
                    }
                }
                catch { }
                return null;
            }
        }

        /// <summary>
        /// คืนค่า Physcal path root เช่น C:\GCOOP_ALL\CEN\GCOOP
        /// </summary>
        public string PhysicalPath
        {
            get { return physicalPath; }
        }

        /// <summary>
        /// คืนค่า physical path เช่น C:\GCOOP_ALL\CEN\GCOOP\XMLConfig
        /// </summary>
        public String XmlConfigPath
        {
            get { return xmlConfigPath; }
        }

        /// <summary>
        /// คืนค่า physical path เช่น C:\ICOOP\FSCT\WebService
        /// </summary>
        public String WebServicePath
        {
            get { return wsPath; }
        }

        /// <summary>
        /// คืนค่า physical path เช่น C:\GCOOP_ALL\CEN\GCOOP\Saving
        /// </summary>
        public String SavingPath
        {
            get { return savPath; }
        }

        /// <summary>
        /// คืนค่า physical path เช่น C:\GCOOP_ALL\CEN\GCOOP\SingleSignOn
        /// </summary>
        public String SingleSignOnPath
        {
            get { return ssoPath; }
        }

        public XmlConfigSQL()
        {
            //this.physicalPath = "C:\\ICOOP\\FSCT";
            this.xmlConfigPath = physicalPath + "\\XMLConfig";
            this.wsPath = physicalPath + "\\WebService";
            this.savPath = physicalPath + "\\Saving";
            this.ssoPath = physicalPath + "\\SingleSignOn";
        }

        private void InitDataTableXmlConfig()
        {
            if (dtXmlConfig == null)
            {
                dtXmlConfig = this.XmlServiceData;
            }
        }

        public String GetDataString(String code)
        {
            InitDataTableXmlConfig();
            try
            {
                for (int i = 0; i < dtXmlConfig.Rows.Count; i++)
                {
                    String forCode = dtXmlConfig.Rows[i]["config_code"].ToString();
                    if (forCode == code)
                    {
                        return dtXmlConfig.Rows[i]["config_value"].ToString();
                    }

                }
            }
            catch { }
            return "";
        }

        public int GetDataInt(String code)
        {
            try
            {
                String v = GetDataString(code);
                int i = int.Parse(v);
                return i;
            }
            catch { return 0; }
        }

        public String DepositInquiry { get { return GetDataString("deposit.inquiry"); } }


    }
}
