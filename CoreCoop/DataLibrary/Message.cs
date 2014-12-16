using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Services.Protocols;

namespace DataLibrary
{
    public class Message
    {
        public static String SoapMessage(SoapException ex)
        {
            try
            {
                String prefix = "[SW]";
                String message = "";
                String exMessage = ex.Message;
                String pbException = "Sybase.PowerBuilder.PBThrowableE: ";
                String soapException = "System.Web.Services.Protocols.SoapException: ";
                String sysException = "System.Exception: ";
                String at = "\n   at ";
                int indexOfStart = 0;
                int indexOfPbService = exMessage.IndexOf(pbException);
                int indexOfSystemEx = exMessage.IndexOf(sysException);
                if (indexOfPbService > 0)
                {
                    prefix = "[PB]";
                    indexOfStart = indexOfPbService + pbException.Length;
                }
                else if (indexOfSystemEx > 0)
                {
                    prefix = "[SY]";
                    indexOfStart = indexOfSystemEx + sysException.Length;
                }
                else
                {
                    indexOfStart = exMessage.IndexOf(soapException) + soapException.Length;
                }
                message = exMessage.Substring(indexOfStart);
                message = message.Substring(0, message.IndexOf(at));
                message = message.Replace("Server was unable to process request. --->", "");
                return prefix + message;
            }
            catch
            {

                return ex.Message.Replace("Server was unable to process request. --->", "");
            }
        }

        public static String ErrorMessage(String message)
        {
            return "<div align=\"center\"><table><tr><td valign=\"top\"></td><td align=\"left\"><font color=\"red\">" + message + "</font></td></tr></table></div>";
        }

        public static String ErrorMessage(Exception ex)
        {
            if (ex.GetType().FullName == "System.Web.Services.Protocols.SoapException")
            {
                return Message.ErrorMessage(Message.SoapMessage((SoapException)ex));
            }
            else
            {
                return Message.ErrorMessage(ex.Message);
            }
        }

        public static String CompleteMessage(String message)
        {
            return "<div align=\"center\"><table><tr><td valign=\"top\"></td><td align=\"left\"><font color=\"green\">" + message + "</font></td></tr></table></div>";
        }

        public static String WarningMessage(String message)
        {
            return "<div align=\"center\"><table><tr><td valign=\"top\"></td><td align=\"left\"><font color=\"orange\">" + message + "</font></td></tr></table></div>";
        }

        public static String WarningMessage(Exception ex)
        {
            if (ex.GetType().FullName == "System.Web.Services.Protocols.SoapException")
            {
                return Message.WarningMessage(Message.SoapMessage((SoapException)ex));
            }
            else
            {
                return Message.WarningMessage(ex.Message);
            }
        }

    }
}
