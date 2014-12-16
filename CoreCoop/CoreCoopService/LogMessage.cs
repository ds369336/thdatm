using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLibrary;
using System.IO;

namespace CoreCoopService
{
    public class LogMessage
    {
        XmlConfigService cfg = new XmlConfigService();

        public void WriteLog(String DataMassage)
        {
            try
            {
                int logflag = cfg.GetDataInt("write_webserver_log");
                if(logflag != 1) return;
                String LogFile_Path = cfg.GetDataString("log_filepath");
                if (!Directory.Exists(LogFile_Path))
                {
                    Directory.CreateDirectory(LogFile_Path);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
    }
}
