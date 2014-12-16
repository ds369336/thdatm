using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace DataLibrary
{
    public class XmlService
    {
        //public XmlConstant GetXmlConstant()
        //{
        //    XmlConstant xml = new XmlConstant();

        //    XmlConfigService cfg = new XmlConfigService();

        //    XmlConsPrintService printService = new XmlConsPrintService();
        //    printService.PDFDeleteTime = cfg.GetDataInt("printservice.ws.pdfdeletetime");
        //    printService.PDFMethod = cfg.GetDataInt("printservice.ws.pdfmethod");
        //    printService.Command = cfg.GetDataString("printservice.ws.pdfwinprintcmd");
        //    printService.PDFPath = cfg.GetDataString("printservice.pdfpath");
        //    printService.PDFUrl = cfg.GetDataString("printservice.pdfurl");
        //    xml.PrintService = printService;

        //    XmlConsReportService report = new XmlConsReportService();
        //    report.Command = cfg.GetDataString("reportservice.ws.pdfwinprintcmd");
        //    report.PDFDeleteTime = cfg.GetDataInt("reportservice.ws.pdfdeletetime");
        //    report.PDFMethod = cfg.GetDataInt("reportservice.ws.pdfmethod");
        //    report.PDFPath = cfg.GetDataString("reportservice.pdfpath");
        //    report.PDFUrl = cfg.GetDataString("reportservice.pdfurl");
        //    xml.ReportService = report;

        //    XmlConsServer server = new XmlConsServer();
        //    server.SwitchingIP = cfg.GetDataString("server.switching_ip");
        //    server.SwitchingPort = cfg.GetDataInt("server.switching_port");
        //    server.SwitchingUsing = cfg.GetDataInt("server.switching_using") == 1;
        //    server.WsPass = cfg.GetDataString("server.wspass");
        //    xml.Server = server;

        //    XmlConsWinLog winLog = new XmlConsWinLog();
        //    winLog.ConnectionString = cfg.GetDataString("winlog.winlog_connectionstring");
        //    winLog.IP = cfg.GetDataString("winlog.winlog_ip");
        //    winLog.Port = cfg.GetDataInt("winlog.winlog_port");
        //    winLog.WinLogUsing = cfg.GetDataInt("winlog.winlog_using") == 1;
        //    xml.WinLog = winLog;

        //    XmlConsWinPrint winPrint = new XmlConsWinPrint();
        //    winPrint.IP = cfg.GetDataString("winprint.winprint_ip");
        //    winPrint.Port = cfg.GetDataInt("winprint.winprint_port");
        //    xml.WinPrint = winPrint;

        //    XmlConsWinReport winReport = new XmlConsWinReport();
        //    winReport.Debug = cfg.GetDataInt("winreport.winreport_debug") == 1;
        //    winReport.EXE_PB_Path = cfg.GetDataString("winreport.winreport_exepb_path");
        //    winReport.IP = cfg.GetDataString("winreport.winreport_ip");
        //    winReport.Port = cfg.GetDataInt("winreport.winreport_port");
        //    xml.WinReport = winReport;

        //    return xml;
        //}

        public List<XmlConnection> GetConnectionStrings()
        {
            List<XmlConnection> list = new List<XmlConnection>();
            DataTable dt = new XmlConfigService().ConnectionStringData;
            for(int i = 0; i < dt.Rows.Count; i++)
            {
                XmlConnection xc = new XmlConnection();
                xc.ID = Convert.ToInt32( dt.Rows[i]["id"] );
                xc.Profile = dt.Rows[i]["profile"].ToString();
                xc.ConnectionString = dt.Rows[i]["connection_string"].ToString();
                list.Add(xc);
            }
            return list;
        }

        public String GetConnectionString(int id)
        {
            List<XmlConnection> x = this.GetConnectionStrings();
            for (int i = 0; i < x.Count; i++)
            {
                if (x[i].ID == id)
                {
                    return x[i].ConnectionString;
                }
            }
            return "";
        }

        public String GetConnectionString()
        {
            XmlConfigService cfg = new XmlConfigService();
            int id = cfg.ConnectionID;
            List<XmlConnection> x = this.GetConnectionStrings();
            for (int i = 0; i < x.Count; i++)
            {
                if (x[i].ID == id)
                {
                    return x[i].ConnectionString;
                }
            }
            return "";
        }

        public XmlConnection GetXmlConnection(int id)
        {
            List<XmlConnection> list = new List<XmlConnection>();
            DataTable dt = new XmlConfigService().ConnectionStringData;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i == id)
                {
                    XmlConnection xc = new XmlConnection();
                    xc.ID = Convert.ToInt32(dt.Rows[i]["id"]);
                    xc.Profile = dt.Rows[i]["profile"].ToString();
                    xc.ConnectionString = dt.Rows[i]["connection_string"].ToString();
                    return xc;
                }
            }
            return null;
        }
    }
}