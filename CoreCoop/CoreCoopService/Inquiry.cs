using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLibrary;

namespace CoreCoopService
{
    public class Inquiry
    {
        WebUtility WebUtil = new WebUtility();

        public void DeptInquiry(String Member_ID, ref Decimal LedgerBalance, ref Decimal AvailableBalance)
        {
            LedgerBalance = 1000;
            AvailableBalance = 1000;
            return;
            Sta ta = new Sta();
            try
            {
                String SqlString = "SELECT NVL(PRNCBAL,0) AS LEDGER_AMT, (NVL(PRNCBAL,0) - NVL(SEQUEST_AMOUNT,0) - NVL(CHECKPEND_AMT,0)) AS AVAILABLE_AMT FROM DPDEPTMASTER WHERE DEPTCLOSE_STATUS = 0 AND ATMFLAG = 1 AND MEMBER_NO = {0}";
                SqlString = WebUtil.SQLFormat(SqlString, Member_ID);
                Sdt dt = ta.Query(SqlString);
                int RowCount = dt.GetRowCount();
                if (RowCount > 1)
                {
                    throw new Exception("พบจำนวนบัญชีเงินฝากของเลขที่สมาชิก " + Member_ID + " ที่ผูกกับ ATM จำนวน " + RowCount + " บัญชี");
                }
                if (dt.Next())
                {
                    LedgerBalance = dt.GetDecimal("LEDGER_AMT");
                    AvailableBalance = dt.GetDecimal("AVAILABLE_AMT");
                }
                ta.Close();
            }
            catch (Exception ex)
            {
                ta.Close();
                throw ex;
            }
        }

        public void LoanInquiry(String Member_ID, ref Decimal LedgerBalance, ref Decimal AvailableBalance)
        {
            LedgerBalance = 2000;
            AvailableBalance = 2000;
            return;
            Sta ta = new Sta();
            try
            {
                String SqlString = "SELECT (NVL(LOANAPPROVE_AMT, 0) - NVL(PRINCIPAL_BALANCE, 0)) AS LEDGER_AMT, (NVL(LOANAPPROVE_AMT, 0) - NVL(PRINCIPAL_BALANCE, 0)) AS AVAILABLE_AMT FROM LNCONTMASTER WHERE CONTRACT_STATUS = 1 AND ATMFLAG = 1 AND MEMBER_NO = {0}";
                SqlString = WebUtil.SQLFormat(SqlString, Member_ID);
                Sdt dt = ta.Query(SqlString);
                if (dt.Next())
                {
                    LedgerBalance = dt.GetDecimal("LEDGER_AMT");
                    AvailableBalance = dt.GetDecimal("AVAILABLE_AMT");
                }
                ta.Close();
            }
            catch (Exception ex)
            {
                ta.Close();
                throw ex;
            }
        }
    }
}
