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

        public void DeptInquiry(String ATMcard_ID, ref Decimal LedgerBalance, ref Decimal AvailableBalance)
        {
            Sta ta = new Sta();
            try
            {
                String SqlString = "SELECT NVL(PRNCBAL,0) AS LEDGER_AMT, (NVL(PRNCBAL,0) - NVL(SEQUEST_AMOUNT,0) - NVL(CHECKPEND_AMT,0)) AS AVAILABLE_AMT FROM DPDEPTMASTER WHERE DEPTCLOSE_STATUS = 0 AND ATMCARD_ID = {0}";
                SqlString = WebUtil.SQLFormat(SqlString, ATMcard_ID);
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

        public void LoanInquiry(String ATMcard_ID, ref Decimal LedgerBalance, ref Decimal AvailableBalance)
        {
            Sta ta = new Sta();
            try
            {
                String SqlString = "SELECT (NVL(LOANAPPROVE_AMT, 0) - NVL(PRINCIPAL_BALANCE, 0)) AS LEDGER_AMT, (NVL(LOANAPPROVE_AMT, 0) - NVL(PRINCIPAL_BALANCE, 0)) AS AVAILABLE_AMT FROM LNCONTMASTER WHERE CONTRACT_STATUS = 1 AND EXPENSE_ACCID = {0}";
                SqlString = WebUtil.SQLFormat(SqlString, ATMcard_ID);
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
