using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLibrary;

namespace CoreCoopService
{
    public class Inquiry
    {
        private Decimal LedgerBal = 0;
        private Decimal AvailableBal = 0;
        private Decimal Dept_Hold = 1;
        private Decimal Loan_Hold = 1;
        private String DEPTACCOUNT_NO = String.Empty;

        WebUtility WebUtil = new WebUtility();

        public Decimal LedgerBalance
        {
            get
            {
                try
                {
                    return LedgerBal;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

        public Decimal AvailableBalance
        {
            get
            {
                try
                {
                    return AvailableBal;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

        public Decimal DeptHold
        {
            get
            {
                try
                {
                    return Dept_Hold;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

        public String DeptaccountNo
        {
            get
            {
                try
                {
                    return DEPTACCOUNT_NO;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

        public void DeptInquiry(String COOP_FIID, String Member_ID, LogMessage LogMessage, Sta ta)
        {
            try
            {
                Decimal PAY_AMT = 0;
                Decimal RECEIVE_AMT = 0;
                String SqlGetAccount = "SELECT DEPTACCOUNT_NO, PAY_AMT, RECEIVE_AMT FROM ATMDEPT WHERE MEMBER_NO = {0} AND COOP_ID = {1}";
                SqlGetAccount = WebUtil.SQLFormat(SqlGetAccount, Member_ID, COOP_FIID);
                LogMessage.WriteLog("DeptAccount SQL", SqlGetAccount);
                Sdt dt = ta.Query(SqlGetAccount);
                int RowCount = dt.GetRowCount();
                if (RowCount != 1)
                {
                    LedgerBal = 0;
                    AvailableBal = 0;
                    Dept_Hold = 1;
                    LogMessage.WriteLog("", "พบจำนวนบัญชีเงินฝากของเลขที่สมาชิก " + Member_ID + " ที่ผูกกับ ATM [" + RowCount + " Row]");
                    return;
                }
                else if (dt.Next())
                {
                    DEPTACCOUNT_NO = dt.GetString("DEPTACCOUNT_NO");
                    PAY_AMT = dt.GetDecimal("PAY_AMT");
                    RECEIVE_AMT = dt.GetDecimal("RECEIVE_AMT");
                    LogMessage.WriteLog("", "DEPTACCOUNT_NO = " + DEPTACCOUNT_NO + " , PAY_AMT = " + PAY_AMT.ToString("#,##0.00") + " , RECEIVE_AMT = " + RECEIVE_AMT.ToString("#,##0.00"));
                }
                String SqlString = "SELECT NVL(DP.PRNCBAL,0) AS LEDGER_AMT, (NVL(DP.PRNCBAL,0) - NVL(DP.SEQUEST_AMOUNT,0) - NVL(DP.CHECKPEND_AMT,0) - NVL(AC.DEPTSEQUEST_AMT,0)) AS AVAILABLE_AMT, AC.DEPT_HOLD AS DEPT_HOLD FROM DPDEPTMASTER DP, ATMCOOP AC WHERE DP.DEPTCLOSE_STATUS = 0 AND TRIM(AC.COOP_FIID) = {0} AND MEMBER_NO = {1} AND DP.DEPTACCOUNT_NO = {2}";
                SqlString = WebUtil.SQLFormat(SqlString, COOP_FIID, Member_ID, DEPTACCOUNT_NO);
                LogMessage.WriteLog("DeptInquiry SQL", SqlString);
                dt = ta.Query(SqlString);
                RowCount = dt.GetRowCount();
                if (RowCount != 1)
                {
                    LedgerBal = 0;
                    AvailableBal = 0;
                    Dept_Hold = 1;
                    LogMessage.WriteLog("", "พบจำนวนบัญชีเงินฝากของเลขที่สมาชิก " + Member_ID + " ที่ผูกกับ ATM [" + RowCount + " Row]");
                    return;
                }
                else if (dt.Next())
                {
                    LedgerBal = dt.GetDecimal("LEDGER_AMT");
                    AvailableBal = dt.GetDecimal("AVAILABLE_AMT");
                    Dept_Hold = dt.GetDecimal("DEPT_HOLD");
                }
                LogMessage.WriteLog("LedgerBalance", LedgerBalance.ToString("#,##0.00"));
                LogMessage.WriteLog("AvailableBalance", LedgerBalance.ToString("#,##0.00"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LoanInquiry(String COOP_FIID, String Member_ID, LogMessage LogMessage, Sta ta)
        {
            try
            {
                String SqlString = "SELECT (NVL(LOANAPPROVE_AMT, 0) - NVL(PRINCIPAL_BALANCE, 0)) AS LEDGER_AMT, (NVL(LOANAPPROVE_AMT, 0) - NVL(PRINCIPAL_BALANCE, 0) - NVL(AC.LOANSEQUEST_AMT,0)) AS AVAILABLE_AMT, AC.LOAN_HOLD AS DEPT_HOLD FROM LNCONTMASTER LN, ATMCOOP AC WHERE CONTRACT_STATUS = 1 AND ATMFLAG = 1 AND MEMBER_NO = {0}";
                SqlString = WebUtil.SQLFormat(SqlString, COOP_FIID.Trim(), Member_ID);
                LogMessage.WriteLog("LoanInquiry SQL", SqlString);
                Sdt dt = ta.Query(SqlString);
                int RowCount = dt.GetRowCount();
                if (RowCount != 1)
                {
                    LedgerBal = 0;
                    AvailableBal = 0;
                    LogMessage.WriteLog("", "พบจำนวนสัญญาเงินกู้ของเลขที่สมาชิก " + Member_ID + " ที่ผูกกับ ATM จำนวน " + RowCount + " สัญญา");
                }
                else if (dt.Next())
                {
                    LedgerBal = dt.GetDecimal("LEDGER_AMT");
                    AvailableBal = dt.GetDecimal("AVAILABLE_AMT");
                    Loan_Hold = dt.GetDecimal("LOAN_HOLD");
                }
                LogMessage.WriteLog("LedgerBalance", LedgerBalance.ToString("#,##0.00"));
                LogMessage.WriteLog("AvailableBalance", LedgerBalance.ToString("#,##0.00"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
