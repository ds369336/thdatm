using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLibrary;
using System.IO;
using System.Globalization;

namespace CoreCoopService
{
    public class LogMessage
    {
        XmlConfigService cfg = new XmlConfigService();
        
        String LogFileName = "InquiryLog" + DateTime.Now.ToString("yyyyMMdd") + ".txt";

        public LogMessage(String LogFileName)
        {
            try
            {
                this.LogFileName = LogFileName;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void WriteLog(String DataMassage)
        {
            try
            {
                int logflag = cfg.GetDataInt("write_webserver_log");
                if (logflag != 1) return;
                String LogFile_Path = CheckDirectory() + LogFileName;
                File.AppendAllText(LogFile_Path, DateTime.Now.ToString("yyyyMMdd_HHmmss ", new CultureInfo("en-US")) + DataMassage + "\r\n", Encoding.UTF8);
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex.Message);
            }
        }

        public void WriteLog(String Description, String Value)
        {
            try
            {
                String DESC = Description + "               ";
                DESC = DESC.Substring(0, 15);
                WriteLog(DESC + " " + Value);
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex.Message);
            }
        }

        private String CheckDirectory()
        {
            try
            {
                String LogFile_Path = cfg.GetDataString("log_filepath") + DateTime.Now.ToString("yyyyMM", new CultureInfo("en-US")) + "\\" + DateTime.Now.ToString("dd", new CultureInfo("en-US")) + "\\";
                if (!Directory.Exists(LogFile_Path))
                {
                    Directory.CreateDirectory(LogFile_Path);
                }
                return LogFile_Path;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void WriteErrorLog(String DataMassage)
        {
            String LogFile_Path = "C:\\AtmCoreCoop\\logsError\\ERROR" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
            if (!Directory.Exists(LogFile_Path))
            {
                Directory.CreateDirectory(LogFile_Path);
            }
            File.AppendAllText(LogFile_Path, DateTime.Now.ToString("yyyyMMdd_HHmmss ", new CultureInfo("en-US")) + DataMassage + "\r\n", Encoding.Default);
        }
    }
}
