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
        public String GetAccountName(String Coop_ID, String Member_No)
        {
            Sta ta = new Sta();
            String ACCOUNT_NAME = String.Empty;

            try
            {//SELECT MEMBER_NO FROM ATMDEPT WHERE SAVING_ACC={2} AND COOP_ID ={1}
                String SqlString = "SELECT TRIM(NAME)||'  '||TRIM(SURNAME) AS ACCOUNT_NAME FROM MEMBER WHERE COOP_ID = {0} AND MEMBER_NO = {1}";
                SqlString = String.Format(SqlString, Coop_ID, Member_No);
                Sdt dt = ta.Query(SqlString);
                if (dt.Next())
                {
                    ACCOUNT_NAME = dt.GetString("ACCOUNT_NAME");
                }
            }
            catch (Exception ex)
            {
                ta.Close();
                throw ex;
            }
            ta.Close();
            return ACCOUNT_NAME;
        }

        public String GetMemberNoDept(String Coop_ID, String Atmcard_ID)
        {
            Sta ta = new Sta();
            String MEMBER_NO = String.Empty;

            try
            {
                String SqlString = "SELECT MEMBER_NO FROM ATMDEPT WHERE COOP_ID ={0} AND SAVING_ACC={1}";
                SqlString = String.Format(SqlString, Coop_ID, Atmcard_ID);
                Sdt dt = ta.Query(SqlString);
                if (dt.Next())
                {
                    MEMBER_NO = dt.GetString("MEMBER_NO");
                }
            }
            catch (Exception ex)
            {
                ta.Close();
                throw ex;
            }
            ta.Close();
            return MEMBER_NO;
        }

        public String GetMemberNoLoan(String Coop_ID, String Atmcard_ID)
        {
            Sta ta = new Sta();
            String MEMBER_NO = String.Empty;

            try
            {
                String SqlString = "SELECT MEMBER_NO FROM ATMLOAN WHERE COOP_ID ={0} AND SAVING_ACC={1}";
                SqlString = String.Format(SqlString, Coop_ID, Atmcard_ID);
                Sdt dt = ta.Query(SqlString);
                if (dt.Next())
                {
                    MEMBER_NO = dt.GetString("MEMBER_NO");
                }
            }
            catch (Exception ex)
            {
                ta.Close();
                throw ex;
            }
            ta.Close();
            return MEMBER_NO;
        }
    }
}
