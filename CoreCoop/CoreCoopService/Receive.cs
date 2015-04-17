using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLibrary;

namespace CoreCoopService
{
    public class Receive
    {
        WebUtility WebUtil = new WebUtility();
        public void DeptWithdraw(String MEMBER_NO, String COOP_ID, String DEPTACCOUNT_NO, Decimal ITEM_AMT, DateTime CCS_OPERATE_DATE, String SYSTEM_CODE, String OPERATE_CODE, String CASH_TYPE, String ATM_NO, String ATM_SEQNO, String SAVING_ACC, LogMessage LogMessage, Sta ta)
        {
            try
            {
                String SqlUpdateATMDEPT = "UPDATE ATMDEPT SET RECEIVE_AMT=RECEIVE_AMT + {0} WHERE ATMDEPT.MEMBER_NO={1}  AND ATMDEPT.COOP_ID={2}  AND ATMDEPT.DEPTACCOUNT_NO={3}";
                SqlUpdateATMDEPT = WebUtil.SQLFormat(SqlUpdateATMDEPT, ITEM_AMT, MEMBER_NO, COOP_ID, DEPTACCOUNT_NO);
                LogMessage.WriteLog("UPDATE ATMDEPT", SqlUpdateATMDEPT);
                ta.Exe(SqlUpdateATMDEPT);

                String SqlInsertATMTRANSACTION = @"INSERT INTO ATMTRANSACTION
                                                     ( MEMBER_NO, COOP_ID, ACCOUNT_NO, CCS_OPERATE_DATE, SYSTEM_CODE, OPERATE_CODE, CASH_TYPE,
                                                        ITEM_AMT, ATM_NO, ATM_SEQNO, SAVING_ACC )
                                                  VALUES ( {0}, {1}, {2}, {3}, {4}, {5}, {6}, 
                                                     {7}, {8}, {9}, {10})";
                SqlInsertATMTRANSACTION = WebUtil.SQLFormat(SqlInsertATMTRANSACTION, MEMBER_NO, COOP_ID, DEPTACCOUNT_NO, CCS_OPERATE_DATE, SYSTEM_CODE, OPERATE_CODE, CASH_TYPE, ITEM_AMT, ATM_NO, ATM_SEQNO, SAVING_ACC);
                LogMessage.WriteLog("UPDATE ATMTRAN..", SqlInsertATMTRANSACTION);
                ta.Exe(SqlInsertATMTRANSACTION);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LoanWithdraw(String MEMBER_NO, String COOP_ID, String DEPTACCOUNT_NO, Decimal ITEM_AMT, DateTime CCS_OPERATE_DATE, String SYSTEM_CODE, String OPERATE_CODE, String CASH_TYPE, String ATM_NO, String ATM_SEQNO, String SAVING_ACC, LogMessage LogMessage, Sta ta)
        {
            try
            {
                String SqlUpdateATMDEPT = "UPDATE ATMDEPT SET RECEIVE_AMT=RECEIVE_AMT + {0} WHERE ATMDEPT.MEMBER_NO={1}  AND ATMDEPT.COOP_ID={2}  AND ATMDEPT.DEPTACCOUNT_NO={3}";
                SqlUpdateATMDEPT = WebUtil.SQLFormat(SqlUpdateATMDEPT, ITEM_AMT, MEMBER_NO, COOP_ID, DEPTACCOUNT_NO);
                LogMessage.WriteLog("UPDATE ATMDEPT", SqlUpdateATMDEPT);
                ta.Exe(SqlUpdateATMDEPT);

                String SqlInsertATMTRANSACTION = @"INSERT INTO ATMTRANSACTION
                                                     ( MEMBER_NO, COOP_ID, ACCOUNT_NO, CCS_OPERATE_DATE, SYSTEM_CODE, OPERATE_CODE, CASH_TYPE,
                                                        ITEM_AMT, ATM_NO, ATM_SEQNO, SAVING_ACC )
                                                  VALUES ( {0}, {1}, {2}, {3}, {4}, {5}, {6}, 
                                                     {7}, {8}, {9}, {10})";
                SqlInsertATMTRANSACTION = WebUtil.SQLFormat(SqlInsertATMTRANSACTION, MEMBER_NO, COOP_ID, DEPTACCOUNT_NO, CCS_OPERATE_DATE, SYSTEM_CODE, OPERATE_CODE, CASH_TYPE, ITEM_AMT, ATM_NO, ATM_SEQNO, SAVING_ACC);
                LogMessage.WriteLog("UPDATE ATMTRAN..", SqlInsertATMTRANSACTION);
                ta.Exe(SqlInsertATMTRANSACTION);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
