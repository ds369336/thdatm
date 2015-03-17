using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLibrary;
using CoreCoopService;
using System.Security.Cryptography.X509Certificates;

namespace ATMBAY
{
    public partial class Default : System.Web.UI.Page
    {
        protected String Result = String.Empty;
        WebUtility WebUtil = new WebUtility();
        LogMessage LogMessage;
        ResponseCode ResponseCode = new ResponseCode();
        Inquiry Inq = new Inquiry();

        protected void Page_Load(object sender, EventArgs e)
        {
            String SqlInsert = String.Empty;
            String SqlResponse = String.Empty;
            Sta ta = new Sta();
            Sta taCommit = new Sta();
            String LogFileName = DateTime.Now.ToString("yyyyMMdd") + ".txt";
            try
            {
                String DataMessage = Request["DataMassage"];
                try
                {
                    LogFileName = DateTime.Now.ToString("yyyyMMdd") + DataMessage.Substring(157, 10) + ".txt";
                }
                catch { }
                LogMessage = new LogMessage(LogFileName);
                LogMessage.WriteLog("Request", DataMessage);
                //เชค Online ใน AppTest
                if (DataMessage == "TestWCF")
                {
                    Result = "1";
                    return;
                }
                //ตรวจสอบข้อมูล
                CheckLength(ref DataMessage);

                //จัดการข้อมูล
                DataEncode DataRequest = new DataEncode(DataMessage);
                DataEncode DataResponse = new DataEncode(DataMessage);
                try
                {
                    taCommit.Transection();
                    ta.Transection();
                }
                catch (Exception ex)
                {
                    DataResponse.ResponseCode = ResponseCode.DatabaseProblem;
                    Result = DataResponse.DataMassage;
                    LogMessage.WriteLog("Database Problem", "");
                    throw ex;
                }
                SqlInsert = DataRequest.GetInsertATMACT();//บันทึกลงตาราง ATMACT เก็บ LOG การ Request
                LogMessage.WriteLog("LOGREQ SQL", SqlInsert);
                taCommit.Exe(SqlInsert);
                //#############################################################################
                DataResponse.ResponseCode = ResponseCode.SystemError; //ป้องกันการ Error Exception
                SqlResponse = DataResponse.DataMassage;
                Result = DataResponse.DataMassage;
                //#############################################################################

                DataResponse.IssuerReference = "Somchai Suksombhut";
                //ตรวจสอบ Request
                switch (DataRequest.TransactionMessageCode)
                {
                    case "0700": //Balance Inquiry
                        BalanceInquiry(DataRequest, ref DataResponse, ta, LogMessage);
                        break;
                    case "0100": // Account Name Inquiry [ถามชื่อบัญชี]
                        AccountNameInquiry(DataRequest, ref DataResponse, ta, LogMessage);
                        break;
                    case "0200": // Fund Transfer //Money Withdraw
                        Transaction(DataRequest, ref DataResponse, ta, LogMessage);
                        break;
                    case "0400": //Fund Transfer/ Cash Withdraw Reversal Request[ยกเลิกรายการทันที]
                        CancelTransaction(DataRequest, ref DataResponse, ta, LogMessage);
                        break;
                    default: break;
                }
                SqlInsert = DataResponse.GetInsertATMACT();//บันทึกลงตาราง ATMACT เก็บ LOG การ Response
                LogMessage.WriteLog("LOGRES SQL", SqlInsert);
                taCommit.Exe(SqlInsert);
                taCommit.Commit();
                ta.Commit();

                Result = DataResponse.DataMassage;

                taCommit.Close();
                ta.Close();

            }
            catch (Exception ex)
            {
                try
                {
                    ta.RollBack();
                    ta.Close();
                }
                catch { }
                try
                {
                    try { taCommit.Exe(SqlResponse); }
                    catch { }
                    taCommit.Commit();
                    taCommit.Close();
                }
                catch { }
                LogMessage.WriteLog("ExceptionErr", ex.Message);
            }
            LogMessage.WriteLog("Response", Result);
            LogMessage.WriteLog("", "-------------------------------------------------------------------------------");
        }

        private void CheckLength(ref String DataMessage)
        {
            if (DataMessage.Length != 320)
            {
                int looptmp = 0;
                for (int i = DataMessage.Length; DataMessage.Length < 320; i++)
                {
                    looptmp++;
                    DataMessage += " ";
                    if (looptmp > 320) break;
                }
            }
        }

        private void AccountNameInquiry(DataEncode DataRequest, ref DataEncode DataResponse, Sta ta, LogMessage LogMessage)
        {
            try
            {
                DataResponse.TransactionMessageCode = "0110";
                ServiceOther ServiceOTH = new ServiceOther();
                if (DataRequest.TransactionCode == 31) //To AC : name Query [***ห้ามเป็นภาษาไทย]
                {
                    //ServiceOTH.GetAccountName("", "");
                }
            }
            catch (Exception ex)
            {
                DataResponse.ResponseCode = ResponseCode.SystemError;
                Result = DataResponse.DataMassage;
                throw ex;
            }
        }

        private void BalanceInquiry(DataEncode DataRequest, ref DataEncode DataResponse, Sta ta, LogMessage LogMessage)
        {
            try
            {
                DataResponse.TransactionMessageCode = "0710";
                if (DataRequest.TransactionCode == 30)
                {
                    if (DataRequest.FromAccountCode == 14) //COOP Deposit Account [ถามยอดเงินฝาก]
                    {
                        Inq.DeptInquiry(DataRequest.COOPFIID, DataRequest.COOPCustomerID.ToString("00000000"), LogMessage, ta);
                    }
                    else if (DataRequest.FromAccountCode == 34) //COOP Loan Account [ถามยอดเงินกู้]
                    {
                        Inq.LoanInquiry(DataRequest.COOPFIID, DataRequest.COOPCustomerID.ToString("00000000"), LogMessage, ta);
                    }
                    LogMessage.WriteLog("DEPOSIT HOLD", Inq.DeptHold.ToString() + " [1 = ปิดระบบเงินฝาก]");
                    if (Inq.DeptHold == 1) //ปิดระบบเงินฝาก
                    {
                        DataResponse.ResponseCode = ResponseCode.SystemNotAvailable;
                    }
                    else
                    {
                        DataResponse.Amount2 = Inq.AvailableBalance;
                        DataResponse.Amount3 = Inq.LedgerBalance;
                    }
                }
            }
            catch (Exception ex)
            {
                DataResponse.ResponseCode = ResponseCode.SystemError;
                Result = DataResponse.DataMassage;
                throw ex;
            }
        }

        private void Transaction(DataEncode DataRequest, ref DataEncode DataResponse, Sta ta, LogMessage LogMessage)
        {
            try
            {
                Decimal LedgerBalance = 0;
                Decimal AvailableBalance = 0;
                Decimal Item_Amt = 0;
                DataResponse.TransactionMessageCode = "0210";
                //#######################################################################################################################
                if (DataRequest.TransactionCode == 10) //[ถอนเงินสด] (เงินออกทางตู้ ATM ทันที)
                {
                    switch (DataRequest.FromAccountCode.ToString("00"))
                    {
                        case "01":
                            DataResponse.ResponseCode = ResponseCode.TransactionNotAuthorized;
                            break;
                        case "11":
                            DataResponse.ResponseCode = ResponseCode.TransactionNotAuthorized;
                            break;
                        case "14": //ถอนเงินฝาก
                            LogMessage.WriteLog("ประเภทรายการ", "ถอนเงินฝากแบบเงินสด");
                            BalanceInquiry(DataRequest, ref DataResponse, ta, LogMessage);
                            AvailableBalance = DataResponse.Amount2; //คงเหลือ
                            LedgerBalance = DataResponse.Amount3;//ถอนได้
                            DataResponse.Amount2 = DataRequest.Amount2;//คืนค่าเดิม
                            DataResponse.Amount3 = DataRequest.Amount3;//คืนค่าเดิม
                            Item_Amt = DataRequest.Amount1;
                            LogMessage.WriteLog("Withdraw Amount", Item_Amt.ToString("#,##0.00"));

                            if (LedgerBalance < Item_Amt)
                            {
                                DataResponse.ResponseCode = ResponseCode.AmountExceededLimit; //เงินคงเหลือไม่เพียงพอ
                                Result = DataResponse.DataMassage;
                                throw new Exception("เงินคงเหลือไม่เพียงพอ");
                            }
                            break;
                        case "34": //กู้เงินกู้
                            LogMessage.WriteLog("ประเภทรายการ", "ถอน(กู้เพิ่ม)เงินกู้แบบเงินสด");
                            break;
                        default:
                            DataResponse.ResponseCode = ResponseCode.TransactionNotAuthorized;
                            break;
                    }
                }
                //#######################################################################################################################
                else if (DataRequest.TransactionCode == 42) //ถอนเงินกู้แบบโอน
                {
                    switch (DataRequest.FromAccountCode.ToString("00"))
                    {
                        case "01":
                            DataResponse.ResponseCode = ResponseCode.TransactionNotAuthorized;
                            break;
                        case "11":
                            DataResponse.ResponseCode = ResponseCode.TransactionNotAuthorized;
                            break;
                        case "14": //ถอนเงินฝาก
                            LogMessage.WriteLog("ประเภทรายการ", "ถอนเงินฝากแบบโอน");
                            BalanceInquiry(DataRequest, ref DataResponse, ta, LogMessage);
                            AvailableBalance = DataResponse.Amount2; //คงเหลือ
                            LedgerBalance = DataResponse.Amount3;//ถอนได้
                            DataResponse.Amount2 = DataRequest.Amount2;//คืนค่าเดิม
                            DataResponse.Amount3 = DataRequest.Amount3;//คืนค่าเดิม
                            Item_Amt = DataRequest.Amount1;
                            LogMessage.WriteLog("Withdraw Amount", Item_Amt.ToString("#,##0.00"));

                            if (LedgerBalance < Item_Amt)
                            {
                                DataResponse.ResponseCode = ResponseCode.AmountExceededLimit; //เงินคงเหลือไม่เพียงพอ
                                Result = DataResponse.DataMassage;
                                throw new Exception("เงินคงเหลือไม่เพียงพอ");
                            }
                            break;
                        case "34": //กู้เงินกู้
                            LogMessage.WriteLog("ประเภทรายการ", "ถอน(กู้เพิ่ม)เงินกู้แบบโอน");
                            BalanceInquiry(DataRequest, ref DataResponse, ta, LogMessage);
                            AvailableBalance = DataResponse.Amount2; //คงเหลือ
                            LedgerBalance = DataResponse.Amount3;//ถอนได้
                            DataResponse.Amount2 = DataRequest.Amount2;//คืนค่าเดิม
                            DataResponse.Amount3 = DataRequest.Amount3;//คืนค่าเดิม
                            Item_Amt = DataRequest.Amount1;
                            LogMessage.WriteLog("Withdraw Amount", Item_Amt.ToString("#,##0.00"));

                            if (LedgerBalance < Item_Amt)
                            {
                                DataResponse.ResponseCode = ResponseCode.AmountExceededLimit; //เงินคงเหลือไม่เพียงพอ
                                Result = DataResponse.DataMassage;
                                throw new Exception("เงินคงเหลือไม่เพียงพอ");
                            }
                            break;
                        default:
                            DataResponse.ResponseCode = ResponseCode.TransactionNotAuthorized;
                            break;
                    }

                }
                //#######################################################################################################################
                else if (DataRequest.TransactionCode == 43) //ใช้ชำระหนี้ หรือฝากเงิน
                {

                }
                //#######################################################################################################################
                else
                {
                    DataResponse.ResponseCode = ResponseCode.TransactionNotAuthorized;
                }
            }
            catch (Exception ex)
            {
                DataResponse.ResponseCode = ResponseCode.SystemError;
                Result = DataResponse.DataMassage;
                throw ex;
            }
        }

        private void CancelTransaction(DataEncode DataRequest, ref DataEncode DataResponse, Sta ta, LogMessage LogMessage)
        {
            try
            {
                DataResponse.TransactionMessageCode = "0410";
                if (DataRequest.TransactionCode == 42) //Fund transfer from COOP A/C TO Bank A/C [โอนจากสหกรณ์>>>ไปธนาคาร]
                {
                    //Result = "MODE : Money Withdraw >> COOP Deposit Account";
                    if (DataRequest.FromAccountCode == 14) //COOP Deposit Account [ถามยอดเงินฝาก]
                    {
                        //Result = "MODE : Balance Inquiry >> COOP Deposit Account";
                    }
                    else if (DataRequest.FromAccountCode == 34) //COOP Loan Account [ถามยอดเงินกู้]
                    {
                        //Result = "MODE : Balance Inquiry >> COOP Loan Account";
                        Decimal Item_Amt = DataRequest.Amount1; //ยอดเงินที่กด
                        //DataResponse.Amount1 = 2000.36m;
                        //DataResponse.Amount3 = 1345.56m;
                    }
                }
                else if (DataRequest.TransactionCode == 43) //Fund transfer from Bank A/C TO COOP A/C [Coop Loan Payment] [โอนจากธนาคาร>>ไปสหกรณ์ ใช้ชำระหนี้]
                {
                    //Result = "MODE : Fund Transfer >> COOP Loan Payment";
                }
            }
            catch (Exception ex)
            {
                DataResponse.ResponseCode = ResponseCode.SystemError;
                Result = DataResponse.DataMassage;
                throw ex;
            }
        }
    }
}