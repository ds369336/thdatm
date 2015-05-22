using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLibrary;

namespace CoreCoopService
{
    public class ServiceOther
    {
        WebUtility WebUtil = new WebUtility();
        public String GetAccountName(String Member_No, LogMessage LogMessage, Sta ta)
        {
            String ACCOUNT_NAME = String.Empty; //[***ห้ามเป็นภาษาไทย]
            try
            {
                String SqlGetName = "SELECT MEMB_ENAME||' '||MEMB_ESURNAME AS ACCOUNT_NAME FROM MBMEMBMASTER WHERE MEMBER_NO = {0}";
                SqlGetName = WebUtil.SQLFormat(SqlGetName, Member_No);
                LogMessage.WriteLog("DeptAccount SQL", SqlGetName);
                Sdt dt = ta.Query(SqlGetName);
                if (dt.Next())
                {
                    ACCOUNT_NAME = dt.GetString("ACCOUNT_NAME").ToUpper();
                    LogMessage.WriteLog("", "ACCOUNT_NAME = " + ACCOUNT_NAME);
                    if (ACCOUNT_NAME.Trim() == "") ACCOUNT_NAME = "N/A";
                }
                else
                {
                    ACCOUNT_NAME = "";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ACCOUNT_NAME;
        }
    }
}
