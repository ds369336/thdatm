using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLibrary;

namespace CoreCoopService
{
    public class Payment
    {
        WebUtility WebUtil = new WebUtility();
        public void DeptPayment(String MEMBER_NO, String COOP_ID, String DEPTACCOUNT_NO, Decimal ITEM_AMT, DateTime CCS_OPERATE_DATE, String SYSTEM_CODE, String OPERATE_CODE, String ATM_NO, String ATM_SEQNO, String SAVING_ACC)
        {
            Sta ta = new Sta();
            try
            {
                String SqlUpdateATMDEPT = "UPDATE ATMDEPT SET PAY_AMT=PAY_AMT + {0} WHERE ATMDEPT.MEMBER_NO={1}  AND ATMDEPT.COOP_ID={2}  AND ATMDEPT.DEPTACCOUNT_NO={3}";
                SqlUpdateATMDEPT = String.Format(SqlUpdateATMDEPT, ITEM_AMT, MEMBER_NO, COOP_ID, DEPTACCOUNT_NO);
                ta.Exe(SqlUpdateATMDEPT);

                String SqlInsertATMTRANSACTION = @"INSERT INTO ATMTRANSACTION
                                                     ( MEMBER_NO, COOP_ID, CCS_OPERATE_DATE, SYSTEM_CODE, OPERATE_CODE, ITEM_AMT, 
                                                       ATM_NO, ATM_SEQNO, SAVING_ACC )
                                                  VALUES ( {0}, {1}, {2}, {3}, {4}, {5}, 
                                                     {8}, {9}, {10})";
                SqlInsertATMTRANSACTION = String.Format(SqlInsertATMTRANSACTION, MEMBER_NO, COOP_ID, CCS_OPERATE_DATE, SYSTEM_CODE, OPERATE_CODE, ITEM_AMT, ATM_NO, ATM_SEQNO, SAVING_ACC);
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
