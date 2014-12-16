using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;

namespace DataLibrary
{
    public class XmlWcfDetail
    {
        private XmlConfigService xml;
        private DataTable wcfData;

        public XmlWcfDetail()
        {
            xml = new XmlConfigService();
            ConstructorEnding();
        }

        public XmlWcfDetail(XmlConfigService xml)
        {
            this.xml = xml;
            ConstructorEnding();
        }

        private void ConstructorEnding()
        {
            String path = xml.XmlConfigPath + "\\server.wcf_detail.xml";
            try
            {
                if (File.Exists(path))
                {
                    DataSet ds = new DataSet();
                    ds.ReadXml(path);
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        wcfData = dt;
                    }
                }
            }
            catch { }
        }

        private String GetStringData(int row, String column)
        {
            try
            {
                return wcfData.Rows[row][column].ToString().Trim();
            }
            catch
            {
                return "";
            }
        }

        public String GetDomainServiceDefault(String serviceName)
        {
            for (int i = 0; i < wcfData.Rows.Count; i++)
            {
                String sName = this.GetStringData(i, "service_name");
                String sOption = this.GetStringData(i, "service_option");
                if (sName.ToLower() == serviceName.ToLower() && sOption.ToLower() == "Default".ToLower())
                {
                    if (this.GetStringData(i, "service_method") == "ip")
                    {
                        return this.GetStringData(i, "service_ip");
                    }
                    else
                    {
                        return this.GetStringData(i, "service_domain");
                    }
                }
            }
            return "";
        }
    }
}
