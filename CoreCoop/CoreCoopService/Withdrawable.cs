using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLibrary;

namespace CoreCoopService
{
    class Withdrawable
    {
        WebUtility WebUtil = new WebUtility();
        public void DeptWithdraw(Decimal Item_Amt, String Coop_ID, String ATMcard_ID, DateTime CCS_OPERATE_DATE, String SYSTEM_CODE, String OPERATE_CODE, String ATM_NO, String ATM_SEQNO, String BANK_CODE, String BRANCH_CODE)
        {
            Sta ta = new Sta();
            try
            {
                String SqlUpdateATMDEPT = "UPDATE ATMDEPT SET ATMDEPT.RECEIVE_AMT=ATMDEPT.RECEIVE_AMT + {0} WHERE ATMDEPT.COOP_ID={1}  AND ATMDEPT.SAVING_ACC={2}  AND ATMDEPT.DEPTHOLD=0 AND 0=( SELECT COOPHOLD FROM COOP WHERE COOP_ID=ATMDEPT.COOP_ID)";
                SqlUpdateATMDEPT = String.Format(SqlUpdateATMDEPT, Item_Amt, Coop_ID, ATMcard_ID);
                ta.Exe(SqlUpdateATMDEPT);

                String SqlInsertATMTRANSACTION = "INSERT INTO ATMTRANSACTION ( MEMBER_NO,COOP_ID,SAVING_ACC,ITEM_AMT,OPERATE_DATE,CCS_OPERATE_DATE,SYSTEM_CODE,OPERATE_CODE,ATM_NO,ATM_SEQNO,BANK_CODE,BRANCH_CODE)VALUES((SELECT MEMBER_NO FROM ATMDEPT WHERE SAVING_ACC={2} AND COOP_ID ={1}),{1},{2},{0},{3},{4},{5},{6},{7},{8},{9},{10})";
                SqlInsertATMTRANSACTION = String.Format(SqlInsertATMTRANSACTION, Item_Amt, Coop_ID, ATMcard_ID, DateTime.Now, CCS_OPERATE_DATE, SYSTEM_CODE, OPERATE_CODE, ATM_NO, ATM_SEQNO, BANK_CODE, BRANCH_CODE);
                ta.Exe(SqlInsertATMTRANSACTION);
                
            }
            catch (Exception ex)
            {
                ta.RollBack();
                ta.Close();
                throw ex;
            }
            try
            {
                ta.Commit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            ta.Close();
        }
    }
}
