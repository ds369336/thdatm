using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Web.UI;

namespace DataLibrary
{
    public class XmlConfigService
    {
        private string xmlConfigPath = "";
        private string wsPath = "";
        private string savPath = "";
        private string ssoPath = "";
        private string physicalPath = "C:\\GCOOP_ALL\\AtmCoreCoopBAY\\CoreCoop";

        private DataTable dtXmlConfig = null;

        private DataTable XmlServiceData
        {
            get
            {
                if (this.dtXmlConfig != null) return dtXmlConfig;
                String path = xmlConfigPath + "\\xmlconf.constmap.xml";
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

        public XmlConfigService()
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

        public int ConnectionID { get { return GetDataInt("connection.id"); } }

        //public String ReportPDFPath { get { return GetDataString("reportservice.pdfpath"); } }

        //public String ReportServicePdfUrl { get { return GetDataString("reportservice.pdfurl"); } }

        //public String ReportServicePdfUrlInternet { get { return GetDataString("reportservice.pdfurl_internet"); } }

        //public bool ClondUsing { get { return GetDataInt("server.cloud_using") == 1; } }

        //public String ClondIP { get { return GetDataString("server.clond_ip"); } }

        //public int ClondPort { get { return GetDataInt("server.clond_port"); } }

        //public String WinPrintIP { get { return GetDataString("winprint.winprint_ip"); } }

        //public int WinPrintPort { get { return GetDataInt("winprint.winprint_port"); } }

        //public String WinReportIP { get { return GetDataString("winreport.winreport_ip"); } }

        //public int WinReportPort { get { return GetDataInt("winreport.winreport_port"); } }

        //public String WinReportExePBPath { get { return GetDataString("winreport.winreport_exepb_path"); } }

        //public String WinLogIP { get { return GetDataString("winlog.winlog_ip"); } }

        //public int WinLogPort { get { return GetDataInt("winlog.winlog_port"); } }

        //public bool WinLogUsing { get { return GetDataInt("winlog.winlog_using") == 1; } }

        //public String WinLogConnectionString { get { return GetDataString("winlog.winlog_connectionstring"); } }

        //public int ShrlonPrintMode { get { return GetDataInt("shrlon.printmode"); } }

        //public int DepositPrintMode { get { return GetDataInt("deposit.printmode"); } }

        //public int LnReceivePrintMode { get { return GetDataInt("lnreceive.printmode"); } }

        //public int FinancePrintMode { get { return GetDataInt("finance.printmode"); } }

        //public String ClientIpPattern { get { return GetDataString("client.ip_pattern"); } }

        //// SingleSingOn Constant ************************************************************************************

        //public String SSOProtocal { get { return GetDataString("sso.protocal"); } }

        //public bool SSOSingleSession { get { return GetDataInt("sso.single_session") == 1; } }

        //public int SSOPort { get { return GetDataInt("sso.port"); } }

        //public String SSOWsPass { get { return GetDataString("sso.ws_pass"); } }

        //public String SSOCookieDomain { get { return GetDataString("sso.cookie_domain"); } }

        //public String SSODomain
        //{
        //    get
        //    {
        //        //return GetDataString("sso.domain");
        //        String domain = GetDataString("sso.domain");
        //        if (domain.IndexOf(".*") > 0)
        //        {
        //            domain = domain.Replace(".*", ".") + this.SSOCookieDomain;
        //        }
        //        return domain;
        //    }
        //}

        //public String SSOImplementKey { get { return GetDataString("sso.implement_key"); } }

        //public String SSOPathPattern { get { return GetDataString("sso.path_pattern"); } }

        //public String SSOSuggestUrl
        //{
        //    get
        //    {
        //        //return GetDataString("sso.suggest_url");
        //        String domain = GetDataString("sso.suggest_url");
        //        if (domain == "sav.*")
        //        {
        //            domain = SavProtocal + "://" + SavDomain + ":" + SavPort + "/" + SavPathPattern;
        //        }
        //        return domain;
        //    }
        //}

        //public int SSOTimeOutLogon { get { return GetDataInt("sso.timeout_logon"); } }

        //public bool SSOAutoChangeDate { get { return GetDataInt("sso.auto_changedate") == 1; } }

        //public bool SSOShowDB { get { return GetDataInt("sso.showdb") == 1; } }
        //// Saving Constant ************************************************************************************

        //public String SavAppletDomain
        //{
        //    get
        //    {
        //        //return GetDataString("sav.domain");
        //        String domain = GetDataString("sav.applet_domain");
        //        if (domain.IndexOf(".*") > 0)
        //        {
        //            domain = domain.Replace(".*", ".") + this.SSOCookieDomain;
        //        }
        //        return domain;
        //    }
        //}

        //public String SavAppletIp { get { return GetDataString("sav.applet_ip"); } }

        //public int SavAppletPort { get { return GetDataInt("sav.applet_port"); } }

        //public String SavAppletProtocal { get { return GetDataString("sav.applet_protocal"); } }

        //public String SavDomain
        //{
        //    get
        //    {
        //        //return GetDataString("sav.domain");
        //        String domain = GetDataString("sav.domain");
        //        if (domain.IndexOf(".*") > 0)
        //        {
        //            domain = domain.Replace(".*", ".") + this.SSOCookieDomain;
        //        }
        //        return domain;
        //    }
        //}

        //public String SavPathPattern { get { return GetDataString("sav.path_pattern"); } }

        //public int SavPort { get { return GetDataInt("sav.port"); } }

        //public String SavProtocal { get { return GetDataString("sav.protocal"); } }

        //public bool SavShowDbProfile { get { return GetDataInt("sav.show_db_profile") == 1; } }

        //public int SavWcfMethod { get { return GetDataInt("sav.wcf_method"); } }

        //// WCF Service Constant ************************************************************************************

        //public String WcfWsPass { get { return GetDataString("wcf.ws_pass"); } }

        //public String WcfProtocal { get { return GetDataString("wcf.protocal"); } }

        //public String WcfDomain
        //{
        //    get
        //    {
        //        String domain = GetDataString("wcf.domain");
        //        if (domain.IndexOf(".*") > 0)
        //        {
        //            domain = domain.Replace(".*", ".") + this.SSOCookieDomain;
        //        }
        //        return domain;
        //    }
        //}

        //public int WcfPort { get { return GetDataInt("wcf.port"); } }

        //public String WcfPathPattern { get { return GetDataString("wcf.path_pattern"); } }

        //// Wsr Constant ************************************************************************************

        //public String WsrProtocal { get { return GetDataString("wsr.protocal"); } }

        //public String WsrDomain
        //{
        //    get
        //    {
        //        String domain = GetDataString("wsr.domain");
        //        if (domain.IndexOf(".*") > 0)
        //        {
        //            domain = domain.Replace(".*", ".") + this.SSOCookieDomain;
        //        }
        //        return domain;
        //    }
        //}

        //public int WsrPort { get { return GetDataInt("wsr.port"); } }

        //public String WsrPathPattern { get { return GetDataString("wsr.path_pattern"); } }

        //// CentLog

        //public bool CentLogUsing { get { return GetDataInt("centlog.using") == 1; } }

        //public String CentLogConnectionString { get { return GetDataString("centlog.connectionstring"); } }

        //// Applet PBSlip Constant ************************************************************************************

        //public String AppletPBSlipClientPath { get { return GetDataString("applet.pbslip_client_path"); } }

        //public String AppletPBSlipServerPath { get { return GetDataString("applet.pbslip_server_path"); } }

        //public String AppletPBSlipAutoUpdate { get { return GetDataString("applet.pbslip_auto_update"); } }

        //public String AppletPBSlipVersion { get { return GetDataString("applet.pbslip_version"); } }

        //public String AppletPBSlipFiles { get { return GetDataString("applet.pbslip_files"); } }

        //// iREPORT Constant ************************************************************************************

        //public String iReportDomain
        //{
        //    get
        //    {
        //        String domain = GetDataString("ireport.domain");
        //        if (domain.IndexOf(".*") > 0)
        //        {
        //            domain = domain.Replace(".*", ".") + this.SSOCookieDomain;
        //        }
        //        return domain;
        //    }
        //}

        //public int iReportPort { get { return GetDataInt("ireport.port"); } }

        //public string iReportSavePath { get { return GetDataString("ireport.save_path"); } }

        //public String iReportOutputProtocal { get { return GetDataString("ireport.output_protocal"); } }

        //public String iReportOutputPathPattern { get { return GetDataString("ireport.output_path_pattern"); } }

        //public String iReportOutputDomain
        //{
        //    get
        //    {
        //        String domain = GetDataString("ireport.output_domain");
        //        if (domain.IndexOf(".*") > 0)
        //        {
        //            domain = domain.Replace(".*", ".") + this.SSOCookieDomain;
        //        }
        //        return domain;
        //    }
        //}

        //public int iReportOutputPort { get { return GetDataInt("ireport.output_port"); } }
    }
}