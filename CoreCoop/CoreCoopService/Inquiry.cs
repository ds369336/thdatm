using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLibrary;

namespace CoreCoopService
{
    public class Inquiry
    {
        private String MemberNo = String.Empty;
        private Decimal LedgerBal = 0; //คงเหลือ
        private Decimal AvailableBal = 0; //ถอนได้
        private Decimal Dept_Hold = 1;
        private Decimal Loan_Hold = 1;
        private Decimal ACCOUNT_HOLD = 1;
        private String DEPTACCOUNT_NO = String.Empty;
        private String LOANCONTRACT_NO = String.Empty;

        WebUtility WebUtil = new WebUtility();

        public String Member_No
        {
            get
            {
                try
                {
                    return MemberNo;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

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

        public Decimal LoanHold
        {
            get
            {
                try
                {
                    return Loan_Hold;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

        public Decimal AccountHold
        {
            get
            {
                try
                {
                    return ACCOUNT_HOLD;
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

        public String LoancontractNo
        {
            get
            {
                try
                {
                    return LOANCONTRACT_NO;
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
                MemberNo = Member_ID;
                Decimal RECEIVE_AMT = 0;
                Decimal PAY_AMT = 0;
                Decimal SEQUEST_AMT = 0;
                String SqlGetAccount = "SELECT TRIM(DEPTACCOUNT_NO) AS DEPTACCOUNT_NO, RECEIVE_AMT, PAY_AMT, SEQUEST_AMT, ACCOUNT_HOLD FROM ATMDEPT WHERE ACCOUNT_STATUS = 1 AND MEMBER_NO = {0} AND COOP_ID = {1}";
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
                    RECEIVE_AMT = dt.GetDecimal("RECEIVE_AMT");
                    PAY_AMT = dt.GetDecimal("PAY_AMT");
                    SEQUEST_AMT = dt.GetDecimal("SEQUEST_AMT");
                    ACCOUNT_HOLD = dt.GetDecimal("ACCOUNT_HOLD");
                    LogMessage.WriteLog("", "DEPTACCOUNT_NO = " + DEPTACCOUNT_NO + " , RECEIVE_AMT = " + RECEIVE_AMT.ToString("#,##0.00") + " , PAY_AMT = " + PAY_AMT.ToString("#,##0.00") + " , SEQUEST_AMT = " + SEQUEST_AMT.ToString("#,##0.00") + " , ACCOUNT_HOLD = " + Dept_Hold.ToString("#0"));
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
                    AvailableBal = dt.GetDecimal("AVAILABLE_AMT") - SEQUEST_AMT;
                    Dept_Hold = dt.GetDecimal("DEPT_HOLD");
                    LogMessage.WriteLog("", "LEDGER_AMT = " + LedgerBal.ToString("#,##0.00") + " , AVAILABLE_AMT = " + AvailableBal.ToString("#,##0.00") + " , DEPT_HOLD = " + Dept_Hold);

                    LedgerBal = LedgerBal - RECEIVE_AMT + PAY_AMT;
                    AvailableBal = AvailableBal - RECEIVE_AMT + PAY_AMT;
                }
                LogMessage.WriteLog("", "LedgerBalance = " + LedgerBal.ToString("#,##0.00"));
                LogMessage.WriteLog("", "AvailableBalance = " + AvailableBal.ToString("#,##0.00"));
                if (AvailableBal < 0)
                {
                    AvailableBal = 0;
                    LogMessage.WriteLog("", "[ChangBalance] AvailableBalance = " + AvailableBal.ToString("#,##0.00"));
                }
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
                MemberNo = Member_ID;
                Decimal RECEIVE_AMT = 0;
                Decimal PAY_AMT = 0;
                Decimal SEQUEST_AMT = 0;
                String SqlGetAccount = "SELECT TRIM(LOANCONTRACT_NO) AS LOANCONTRACT_NO, RECEIVE_AMT, PAY_AMT, SEQUEST_AMT, ACCOUNT_HOLD FROM ATMLOAN WHERE ACCOUNT_STATUS = 1 AND MEMBER_NO = {0} AND COOP_ID = {1}";
                SqlGetAccount = WebUtil.SQLFormat(SqlGetAccount, Member_ID, COOP_FIID);
                LogMessage.WriteLog("LoanContract SQL", SqlGetAccount);
                Sdt dt = ta.Query(SqlGetAccount);
                int RowCount = dt.GetRowCount();
                if (RowCount != 1)
                {
                    LedgerBal = 0;
                    AvailableBal = 0;
                    Loan_Hold = 1;
                    LogMessage.WriteLog("", "พบจำนวนสัญญาเงินกู้ ATM ของเลขที่สมาชิก " + Member_ID + " ที่ผูกกับ ATM [" + RowCount + " Row]");
                    return;
                }
                else if (dt.Next())
                {
                    LOANCONTRACT_NO = dt.GetString("LOANCONTRACT_NO");
                    RECEIVE_AMT = dt.GetDecimal("RECEIVE_AMT");
                    PAY_AMT = dt.GetDecimal("PAY_AMT");
                    SEQUEST_AMT = dt.GetDecimal("SEQUEST_AMT");
                    ACCOUNT_HOLD = dt.GetDecimal("ACCOUNT_HOLD");
                    LogMessage.WriteLog("", "LOANCONTRACT_NO = " + LOANCONTRACT_NO + " , RECEIVE_AMT = " + RECEIVE_AMT.ToString("#,##0.00") + " , PAY_AMT = " + PAY_AMT.ToString("#,##0.00") + " , ACCOUNT_HOLD = " + ACCOUNT_HOLD.ToString("#0"));
                }
                String SqlString = "SELECT (NVL(LN.LOANAPPROVE_AMT, 0) - NVL(LN.PRINCIPAL_BALANCE, 0)) AS LEDGER_AMT, (NVL(LN.LOANAPPROVE_AMT, 0) - NVL(LN.PRINCIPAL_BALANCE, 0) - NVL(AC.LOANSEQUEST_AMT,0)) AS AVAILABLE_AMT, AC.LOAN_HOLD AS LOAN_HOLD FROM LNCONTMASTER LN, ATMCOOP AC WHERE LN.CONTRACT_STATUS = 1 AND TRIM(AC.COOP_FIID) = {0} AND LN.MEMBER_NO = {1} AND LN.LOANCONTRACT_NO = {2}";
                SqlString = WebUtil.SQLFormat(SqlString, COOP_FIID, Member_ID, LOANCONTRACT_NO);
                LogMessage.WriteLog("LoanInquiry SQL", SqlString);
                dt = ta.Query(SqlString);
                RowCount = dt.GetRowCount();
                if (RowCount != 1)
                {
                    LedgerBal = 0;
                    AvailableBal = 0;
                    Dept_Hold = 1;
                    LogMessage.WriteLog("", "พบจำนวนสัญญาเงินกู้ ATM เลขที่ " + LOANCONTRACT_NO + " ของเลขที่สมาชิก " + Member_ID + " ที่ผูกกับ ATM [" + RowCount + " Row]");
                    return;
                }
                else if (dt.Next())
                {
                    LedgerBal = dt.GetDecimal("LEDGER_AMT");
                    AvailableBal = dt.GetDecimal("AVAILABLE_AMT");
                    Loan_Hold = dt.GetDecimal("LOAN_HOLD");
                    LogMessage.WriteLog("", "LEDGER_AMT = " + LedgerBal.ToString("#,##0.00") + " , AVAILABLE_AMT = " + AvailableBal.ToString("#,##0.00") + " , LOAN_HOLD = " + Loan_Hold);

                    LedgerBal = LedgerBal - RECEIVE_AMT + PAY_AMT;
                    AvailableBal = AvailableBal - RECEIVE_AMT + PAY_AMT;
                }
                LogMessage.WriteLog("", "LedgerBalance = " + LedgerBal.ToString("#,##0.00"));
                LogMessage.WriteLog("", "AvailableBalance = " + AvailableBal.ToString("#,##0.00"));
                if (AvailableBal < 0)
                {
                    AvailableBal = 0;
                    LogMessage.WriteLog("", "[ChangBalance] AvailableBalance = " + AvailableBal.ToString("#,##0.00"));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
