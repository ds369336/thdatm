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
        public void ProcessPayment(String MEMBER_NO, String COOP_ID, String ACCOUNT_NO, Decimal ITEM_AMT, DateTime CCS_OPERATE_DATE, String SYSTEM_CODE, String OPERATE_CODE, String CASH_TYPE, String ATM_NO, String ATM_SEQNO, String SAVING_ACC, LogMessage LogMessage, Sta ta)
        {
            try
            {
                if (SYSTEM_CODE == "01") //เงินกู้
                {
                    String SqlUpdateATMLOAN = "UPDATE ATMLOAN SET PAY_AMT=PAY_AMT + {0} WHERE ATMLOAN.MEMBER_NO={1}  AND ATMLOAN.COOP_ID={2}  AND ATMLOAN.LOANCONTRACT_NO={3}";
                    SqlUpdateATMLOAN = WebUtil.SQLFormat(SqlUpdateATMLOAN, ITEM_AMT, MEMBER_NO, COOP_ID, ACCOUNT_NO);
                    LogMessage.WriteLog("UPDATE ATMLOAN", SqlUpdateATMLOAN);
                    ta.Exe(SqlUpdateATMLOAN);
                }
                else if (SYSTEM_CODE == "02") //เงินฝาก
                {
                    String SqlUpdateATMDEPT = "UPDATE ATMDEPT SET PAY_AMT=PAY_AMT + {0} WHERE ATMDEPT.MEMBER_NO={1}  AND ATMDEPT.COOP_ID={2}  AND ATMDEPT.DEPTACCOUNT_NO={3}";
                    SqlUpdateATMDEPT = WebUtil.SQLFormat(SqlUpdateATMDEPT, ITEM_AMT, MEMBER_NO, COOP_ID, ACCOUNT_NO);
                    LogMessage.WriteLog("UPDATE ATMDEPT", SqlUpdateATMDEPT);
                    ta.Exe(SqlUpdateATMDEPT);
                }
                else
                {
                    throw new Exception("SYSTEM_CODE NOT VALUE (ไม่ถูกต้อง)");
                }

                String SqlInsertATMTRANSACTION = @"INSERT INTO ATMTRANSACTION
                                                     ( MEMBER_NO, COOP_ID, ACCOUNT_NO, CCS_OPERATE_DATE, SYSTEM_CODE, OPERATE_CODE, CASH_TYPE,
                                                        ITEM_AMT, ATM_NO, ATM_SEQNO, SAVING_ACC )
                                                  VALUES ( {0}, {1}, {2}, {3}, {4}, {5}, {6}, 
                                                     {7}, {8}, {9}, {10})";
                SqlInsertATMTRANSACTION = WebUtil.SQLFormat(SqlInsertATMTRANSACTION, MEMBER_NO, COOP_ID, ACCOUNT_NO, CCS_OPERATE_DATE, SYSTEM_CODE, OPERATE_CODE, CASH_TYPE, ITEM_AMT, ATM_NO, ATM_SEQNO, SAVING_ACC);
                LogMessage.WriteLog("INSERT ATMTRAN", SqlInsertATMTRANSACTION);
                ta.Exe(SqlInsertATMTRANSACTION);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ProcessCancelPayment(String MEMBER_NO, String COOP_ID, String ACCOUNT_NO, Decimal ITEM_AMT, DateTime CCS_OPERATE_DATE, String SYSTEM_CODE, String OPERATE_CODE, String CASH_TYPE, String ATM_NO, String ATM_SEQNO, String SAVING_ACC, LogMessage LogMessage, Sta ta)
        {
            try
            {
                String SqlInsertATMTRANSACTIONCANCEL = @"INSERT INTO ATMTRANSACTIONCANCEL
                                                     ( MEMBER_NO, COOP_ID, ACCOUNT_NO, CCS_OPERATE_DATE, SYSTEM_CODE, OPERATE_CODE, CASH_TYPE,
                                                        ITEM_AMT, ATM_NO, ATM_SEQNO, SAVING_ACC )
                                                  VALUES ( {0}, {1}, {2}, {3}, {4}, {5}, {6}, 
                                                     {7}, {8}, {9}, {10})";
                SqlInsertATMTRANSACTIONCANCEL = WebUtil.SQLFormat(SqlInsertATMTRANSACTIONCANCEL, MEMBER_NO, COOP_ID, ACCOUNT_NO, CCS_OPERATE_DATE, SYSTEM_CODE, OPERATE_CODE, CASH_TYPE, ITEM_AMT, ATM_NO, ATM_SEQNO, SAVING_ACC);
                LogMessage.WriteLog("INSERT TRNCANCEL", SqlInsertATMTRANSACTIONCANCEL);
                ta.Exe(SqlInsertATMTRANSACTIONCANCEL);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
