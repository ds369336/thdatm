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
            String SqlRequest = String.Empty;
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
                //#############################################################################
                DataResponse.ResponseCode = ResponseCode.SystemError; //ป้องกันการ Error Exception
                SqlResponse = DataResponse.GetInsertATMACT();
                Result = DataResponse.DataMassage;
                DataResponse.ResponseCode = ResponseCode.Approve;
                //#############################################################################
                SqlRequest = DataRequest.GetInsertATMACT();//บันทึกลงตาราง ATMACT เก็บ LOG การ Request
                LogMessage.WriteLog("LOGREQ SQL", SqlRequest);
                taCommit.Exe(SqlRequest);
                //#############################################################################

                try
                {
                    //ตรวจสอบ Request
                    switch (DataRequest.TransactionMessageCode)
                    {
                        case "0100": // Account Name Inquiry [ถามชื่อบัญชี]
                            AccountNameInquiry(DataRequest, ref DataResponse, ta, LogMessage);
                            break;
                        case "0700": //Balance Inquiry
                            BalanceInquiry(DataRequest, ref DataResponse, ta, LogMessage);
                            break;
                        case "0200": // Fund Transfer //Money Withdraw
                            Transaction(DataRequest, ref DataResponse, ta, LogMessage);
                            break;
                        case "0400": //Fund Transfer/ Cash Withdraw Reversal Request[ยกเลิกรายการทันที]
                            CancelTransaction(DataRequest, ref DataResponse, ta, LogMessage);
                            break;
                        default: break;
                    }
                }
                catch (Exception ex)
                {
                    if (DataResponse.ResponseCode == 0) DataResponse.ResponseCode = ResponseCode.SystemError;
                    LogMessage.WriteLog("ResponseCode", "[" + DataResponse.ResponseCode + "]");
                    Result = DataResponse.DataMassage;
                    SqlResponse = DataResponse.GetInsertATMACT();
                    throw ex;
                }
                Result = DataResponse.DataMassage;
                SqlResponse = DataResponse.GetInsertATMACT();//บันทึกลงตาราง ATMACT เก็บ LOG การ Response
                LogMessage.WriteLog("LOGRES SQL", SqlResponse);
                taCommit.Exe(SqlResponse);
                taCommit.Commit();
                ta.Commit();


                taCommit.Close();
                ta.Close();

            }
            catch (Exception ex)
            {
                LogMessage.WriteLog("ExceptionErr", ex.Message.Trim());
                try
                {
                    ta.RollBack();
                    ta.Close();
                }
                catch { }
                try
                {
                    try { LogMessage.WriteLog("LOGRES SQL", SqlResponse); taCommit.Exe(SqlResponse); }
                    catch { }
                    taCommit.Commit();
                    taCommit.Close();
                }
                catch { }
            }
            LogMessage.WriteLog("Response", Result);
            LogMessage.WriteLog("", "-------------------------------------------------------------------------------\r\n");

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
                    String Member_Name = "";
                    Member_Name = ServiceOTH.GetAccountName(DataRequest.COOPCustomerID.ToString("00000000"), LogMessage, ta);
                    if (Member_Name == "")
                    {
                        LogMessage.WriteLog("", "NOT HAVE ACCOUNT IN COOP (ไม่พบเลขทะเบียนสมาชิกนี้ในระบบ)");
                        DataResponse.ResponseCode = ResponseCode.AccountNotAuthorize;
                        LogMessage.WriteLog("ResponseCode", "[" + ResponseCode.AccountNotAuthorize + "] Account not authorize, Contact bank");
                    }
                    else
                    {
                        DataResponse.IssuerReference = Member_Name;
                    }
                }
            }
            catch (Exception ex)
            {
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
                        LogMessage.WriteLog("TransactionType", "Inquiry Deposit [สอบถามยอดเงินฝาก] ####################");
                        Inq.DeptInquiry(DataRequest.COOPFIID, DataRequest.COOPCustomerID.ToString("00000000"), LogMessage, ta);
                        if (Inq.DeptHold == 1 || Inq.AccountHold == 1) //ปิดระบบเงินฝาก
                        {
                            LogMessage.WriteLog("DEPT HOLD", "HOLD_FLAG = " + Inq.DeptHold.ToString() + " [1 = ปิดระบบเงินฝาก], ACCOUNT_HOLD = " + Inq.AccountHold + " [1 = อายัดบัญชีเงินฝาก]");
                            LogMessage.WriteLog("ResponseCode", "[" + ResponseCode.SystemNotAvailable + "] System Not Available");
                            DataResponse.ResponseCode = ResponseCode.SystemNotAvailable;
                        }
                        else
                        {
                            DataResponse.Amount2 = Inq.LedgerBalance;//คงเหลือ
                            DataResponse.Amount3 = Inq.AvailableBalance;//ถอนได้
                        }
                    }
                    else if (DataRequest.FromAccountCode == 34) //COOP Loan Account [ถามยอดเงินกู้]
                    {
                        LogMessage.WriteLog("TransactionType", "Inquiry Loan [สอบถามยอดเงินกู้] ####################");
                        Inq.LoanInquiry(DataRequest.COOPFIID, DataRequest.COOPCustomerID.ToString("00000000"), LogMessage, ta);
                        if (Inq.LoanHold == 1 || Inq.AccountHold == 1) //ปิดระบบเงินกู้
                        {
                            LogMessage.WriteLog("LOAN HOLD", "HOLD_FLAG = " + Inq.LoanHold.ToString() + " [1 = ปิดระบบเงินกู้], ACCOUNT_HOLD = " + Inq.AccountHold + " [1 = อายัดสัญญาเงินกู้]");
                            LogMessage.WriteLog("ResponseCode", "[" + ResponseCode.SystemNotAvailable + "] System Not Available");
                            DataResponse.ResponseCode = ResponseCode.SystemNotAvailable;
                        }
                        else
                        {
                            DataResponse.Amount2 = Inq.LedgerBalance;//คงเหลือ
                            DataResponse.Amount3 = Inq.AvailableBalance;//ถอนได้
                        }
                    }
                    else
                    {
                        DataResponse.ResponseCode = ResponseCode.MessageEditError; //ข้อความไม่ถูกต้อง
                    }

                }
                else
                {
                    DataResponse.ResponseCode = ResponseCode.MessageEditError; //ข้อความไม่ถูกต้อง
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Transaction(DataEncode DataRequest, ref DataEncode DataResponse, Sta ta, LogMessage LogMessage)
        {
            try
            {
                String Member_No = DataRequest.COOPCustomerID.ToString("00000000");
                String Coop_Id = DataRequest.COOPFIID;
                Decimal LedgerBalance = 0;
                Decimal AvailableBalance = 0;
                Decimal Item_Amt = 0;
                DataResponse.TransactionMessageCode = "0210";
                //#######################################################################################################################
                #region ถอนสด
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
                            String Deptaccount_No = String.Empty;
                            LogMessage.WriteLog("TransactionType", "Cash Withdraw Deposit [ถอนเงินฝากแบบเงินสด] ####################");
                            Inq.DeptInquiry(Coop_Id, Member_No, LogMessage, ta);

                            Item_Amt = DataRequest.Amount1;
                            LogMessage.WriteLog("Withdraw Amount", Item_Amt.ToString("#,##0.00"));

                            if (Inq.DeptHold == 1 || Inq.AccountHold == 1) //ปิดระบบเงินฝาก
                            {
                                LogMessage.WriteLog("DEPOSIT HOLD", "HOLD_FLAG = " + Inq.DeptHold.ToString() + " [1 = ปิดระบบเงินฝาก], ACCOUNT_HOLD = " + Inq.AccountHold + " [1 = อายัดบัญชีเงินฝาก]");
                                LogMessage.WriteLog("ResponseCode", "[" + ResponseCode.SystemNotAvailable + "] System Not Available");
                                DataResponse.ResponseCode = ResponseCode.SystemNotAvailable;
                                return;
                            }
                            else
                            {
                                LogMessage.WriteLog("", "HOLD_FLAG = " + Inq.DeptHold.ToString());
                                LedgerBalance = Inq.LedgerBalance;//คงเหลือ
                                AvailableBalance = Inq.AvailableBalance;//ถอนได้
                                Deptaccount_No = Inq.DeptaccountNo;
                            }

                            if (AvailableBalance < Item_Amt)
                            {
                                LogMessage.WriteLog("ResponseCode", "[" + ResponseCode.AmountExceededLimit + "] Amount Exceeded Limit เงินคงเหลือไม่เพียงพอ");
                                DataResponse.ResponseCode = ResponseCode.AmountExceededLimit; //เงินคงเหลือไม่เพียงพอ
                                Result = DataResponse.DataMassage;
                                return;
                            }
                            else
                            {
                                DataResponse.Amount3 = LedgerBalance - Item_Amt; //คงเหลือ
                                //###### ตัดยอด ######
                                Receive Recv = new Receive();
                                String System_Code = "02"; //เงินฝาก
                                String Operate_Code = "002"; //ประเภทถอน
                                String Cash_type = "CSH";
                                Recv.ProcessWithdraw(Member_No, Coop_Id, Deptaccount_No, Item_Amt, DataRequest.TransactionDateTime, System_Code, Operate_Code, Cash_type, DataRequest.AcquirerTerminalNumber, DataRequest.TerminalSequenceNo, DataRequest.COOPCustomerAC.ToString("0000000000"), LogMessage, ta);
                            }
                            break;
                        case "34": //กู้เงินกู้
                            String Loancontract_No = String.Empty;
                            LogMessage.WriteLog("TransactionType", "Cash Withdraw Loan [ถอน(กู้เพิ่ม)เงินกู้แบบเงินสด] ####################");
                            Inq.LoanInquiry(Coop_Id, Member_No, LogMessage, ta);

                            Item_Amt = DataRequest.Amount1;
                            LogMessage.WriteLog("Withdraw Amount", Item_Amt.ToString("#,##0.00"));

                            if (Inq.LoanHold == 1 || Inq.AccountHold == 1) //ปิดระบบเงินกู้
                            {
                                LogMessage.WriteLog("", "HOLD_FLAG = " + Inq.LoanHold.ToString() + " [1 = ปิดระบบเงินกู้], ACCOUNT_HOLD = " + Inq.AccountHold + " [1 = อายัดสัญญาเงินกู้]");
                                LogMessage.WriteLog("ResponseCode", "[" + ResponseCode.SystemNotAvailable + "] System Not Available");
                                DataResponse.ResponseCode = ResponseCode.SystemNotAvailable;
                                return;
                            }
                            else
                            {
                                LogMessage.WriteLog("", "HOLD_FLAG = " + Inq.LoanHold.ToString());
                                LedgerBalance = Inq.LedgerBalance;//คงเหลือ
                                AvailableBalance = Inq.AvailableBalance;//ถอนได้
                                Loancontract_No = Inq.LoancontractNo;
                            }

                            if (AvailableBalance < Item_Amt)
                            {
                                LogMessage.WriteLog("ResponseCode", "[" + ResponseCode.AmountExceededLimit + "] Amount Exceeded Limit เงินคงเหลือไม่เพียงพอ");
                                DataResponse.ResponseCode = ResponseCode.AmountExceededLimit; //เงินคงเหลือไม่เพียงพอ
                                Result = DataResponse.DataMassage;
                                return;
                            }
                            else if (Item_Amt % 100 != 0)
                            {
                                LogMessage.WriteLog("ResponseCode", "[" + ResponseCode.InvalidAmount + "] Invalid Amount ถอนยอดมีเศษไม่ได้ = " + (Item_Amt % 100));
                                DataResponse.ResponseCode = ResponseCode.InvalidAmount; //เงินคงเหลือไม่เพียงพอ
                                Result = DataResponse.DataMassage;
                                return;
                            }
                            else
                            {
                                DataResponse.Amount3 = LedgerBalance - Item_Amt; //คงเหลือ
                                //###### ตัดยอด ######
                                Receive Recv = new Receive();
                                String System_Code = "01"; //เงินกู้
                                String Operate_Code = "002"; //ประเภทถอน
                                String Cash_type = "CSH";
                                Recv.ProcessWithdraw(Member_No, Coop_Id, Loancontract_No, Item_Amt, DataRequest.TransactionDateTime, System_Code, Operate_Code, Cash_type, DataRequest.AcquirerTerminalNumber, DataRequest.TerminalSequenceNo, DataRequest.COOPCustomerAC.ToString("0000000000"), LogMessage, ta);
                            }
                            break;
                        default:
                            DataResponse.ResponseCode = ResponseCode.TransactionNotAuthorized;
                            break;
                    }
                }
                #endregion
                //#######################################################################################################################
                #region ถอนโอน
                else if (DataRequest.TransactionCode == 42) //ถอนเงินแบบโอน
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
                            String Deptaccount_No = String.Empty;
                            LogMessage.WriteLog("TransactionType", "Cash Withdraw Deposit [ถอนเงินฝากแบบโอนเข้าบัญชี] ####################");
                            Inq.DeptInquiry(Coop_Id, Member_No, LogMessage, ta);

                            Item_Amt = DataRequest.Amount1;
                            LogMessage.WriteLog("Withdraw Amount", Item_Amt.ToString("#,##0.00"));

                            if (Inq.DeptHold == 1 || Inq.AccountHold == 1) //ปิดระบบเงินฝาก
                            {
                                LogMessage.WriteLog("", "HOLD_FLAG = " + Inq.DeptHold.ToString() + " [1 = ปิดระบบเงินฝาก], ACCOUNT_HOLD = " + Inq.AccountHold + " [1 = อายัดบัญชีเงินฝาก]");
                                LogMessage.WriteLog("ResponseCode", "[" + ResponseCode.SystemNotAvailable + "] System Not Available");
                                DataResponse.ResponseCode = ResponseCode.SystemNotAvailable;
                                return;
                            }
                            else
                            {
                                LogMessage.WriteLog("", "HOLD_FLAG = " + Inq.DeptHold.ToString());
                                LedgerBalance = Inq.LedgerBalance;//คงเหลือ
                                AvailableBalance = Inq.AvailableBalance;//ถอนได้
                                Deptaccount_No = Inq.DeptaccountNo;
                            }

                            if (AvailableBalance < Item_Amt)
                            {
                                LogMessage.WriteLog("ResponseCode", "[" + ResponseCode.AmountExceededLimit + "] Amount Exceeded Limit เงินคงเหลือไม่เพียงพอ");
                                DataResponse.ResponseCode = ResponseCode.AmountExceededLimit; //เงินคงเหลือไม่เพียงพอ
                                Result = DataResponse.DataMassage;
                                return;
                            }
                            else
                            {
                                DataResponse.Amount3 = LedgerBalance - Item_Amt; //คงเหลือ
                                //###### ตัดยอด ######
                                Receive Recv = new Receive();
                                String System_Code = "02"; //เงินฝาก
                                String Operate_Code = "002"; //ประเภทถอน
                                String Cash_type = "TRN";
                                Recv.ProcessWithdraw(Member_No, Coop_Id, Deptaccount_No, Item_Amt, DataRequest.TransactionDateTime, System_Code, Operate_Code, Cash_type, DataRequest.AcquirerTerminalNumber, DataRequest.TerminalSequenceNo, DataRequest.COOPCustomerAC.ToString("0000000000"), LogMessage, ta);
                            }
                            break;
                        case "34": //กู้เงินกู้
                            String Loancontract_No = String.Empty;
                            LogMessage.WriteLog("TransactionType", "Tranfer Withdraw Loan [ถอน(กู้เพิ่ม)เงินกู้แบบโอน] ####################");
                            Inq.LoanInquiry(Coop_Id, Member_No, LogMessage, ta);

                            Item_Amt = DataRequest.Amount1;
                            LogMessage.WriteLog("Withdraw Amount", Item_Amt.ToString("#,##0.00"));

                            if (Inq.LoanHold == 1 || Inq.AccountHold == 1) //ปิดระบบเงินกู้
                            {
                                LogMessage.WriteLog("", "HOLD_FLAG = " + Inq.LoanHold.ToString() + " [1 = ปิดระบบเงินกู้], ACCOUNT_HOLD = " + Inq.AccountHold + " [1 = อายัดสัญญาเงินกู้]");
                                LogMessage.WriteLog("ResponseCode", "[" + ResponseCode.SystemNotAvailable + "] System Not Available");
                                DataResponse.ResponseCode = ResponseCode.SystemNotAvailable;
                                return;
                            }
                            else
                            {
                                LogMessage.WriteLog("", "HOLD_FLAG = " + Inq.LoanHold.ToString());
                                LedgerBalance = Inq.LedgerBalance;//คงเหลือ
                                AvailableBalance = Inq.AvailableBalance;//ถอนได้
                                Loancontract_No = Inq.LoancontractNo;
                            }

                            if (AvailableBalance < Item_Amt)
                            {
                                LogMessage.WriteLog("ResponseCode", "[" + ResponseCode.AmountExceededLimit + "] Amount Exceeded Limit เงินคงเหลือไม่เพียงพอ");
                                DataResponse.ResponseCode = ResponseCode.AmountExceededLimit; //เงินคงเหลือไม่เพียงพอ
                                Result = DataResponse.DataMassage;
                                return;
                            }
                            else if (Item_Amt % 100 != 0)
                            {
                                LogMessage.WriteLog("ResponseCode", "[" + ResponseCode.InvalidAmount + "] Invalid Amount ถอนยอดมีเศษไม่ได้ = " + (Item_Amt % 100));
                                DataResponse.ResponseCode = ResponseCode.InvalidAmount; //เงินคงเหลือไม่เพียงพอ
                                Result = DataResponse.DataMassage;
                                return;
                            }
                            else
                            {
                                DataResponse.Amount3 = LedgerBalance - Item_Amt; //คงเหลือ
                                //###### ตัดยอด ######
                                Receive Recv = new Receive();
                                String System_Code = "01"; //เงินกู้
                                String Operate_Code = "002"; //ประเภทถอน
                                String Cash_type = "TRN";
                                Recv.ProcessWithdraw(Member_No, Coop_Id, Loancontract_No, Item_Amt, DataRequest.TransactionDateTime, System_Code, Operate_Code, Cash_type, DataRequest.AcquirerTerminalNumber, DataRequest.TerminalSequenceNo, DataRequest.COOPCustomerAC.ToString("0000000000"), LogMessage, ta);
                            }
                            break;
                        default:
                            DataResponse.ResponseCode = ResponseCode.TransactionNotAuthorized;
                            break;
                    }

                }
                #endregion
                //#######################################################################################################################
                #region ฝากเงิน
                else if (DataRequest.TransactionCode == 43) //ใช้ชำระหนี้ หรือฝากเงิน
                {
                    Payment Pay = new Payment();
                    switch (DataRequest.ToAccountCode.ToString("00"))
                    {
                        case "01":
                            DataResponse.ResponseCode = ResponseCode.TransactionNotAuthorized;
                            break;
                        case "11":
                            DataResponse.ResponseCode = ResponseCode.TransactionNotAuthorized;
                            break;
                        case "14": //ฝากเงินฝาก
                            String Deptaccount_No = String.Empty;
                            LogMessage.WriteLog("TransactionType", "Tranfer Deposit [ฝากเงินฝาก] ####################");
                            Inq.DeptInquiry(Coop_Id, Member_No, LogMessage, ta);

                            Item_Amt = DataRequest.Amount1;
                            LogMessage.WriteLog("Deposit Amount", Item_Amt.ToString("#,##0.00"));

                            if (Inq.DeptHold == 1 || Inq.AccountHold == 1) //ปิดระบบเงินฝาก
                            {
                                LogMessage.WriteLog("", "HOLD_FLAG = " + Inq.DeptHold.ToString() + " [1 = ปิดระบบเงินฝาก], ACCOUNT_HOLD = " + Inq.AccountHold + " [1 = อายัดบัญชีเงินฝาก]");
                                LogMessage.WriteLog("ResponseCode", "[" + ResponseCode.SystemNotAvailable + "] System Not Available");
                                DataResponse.ResponseCode = ResponseCode.SystemNotAvailable;
                                return;
                            }
                            else
                            {
                                LogMessage.WriteLog("", "HOLD_FLAG = " + Inq.DeptHold.ToString());
                                LedgerBalance = Inq.LedgerBalance;//คงเหลือ
                                AvailableBalance = Inq.AvailableBalance;//ถอนได้
                                Deptaccount_No = Inq.DeptaccountNo;
                            }

                            String Sharp = "00000000000000000000";
                            String DeptTranfer = DataRequest.COOPCustomerAC.ToString(Sharp.Substring(0, Deptaccount_No.Length));
                            LogMessage.WriteLog("", "MEMBER_NO = " + Member_No + ", DEPOSIT_TRANFER = " + DeptTranfer + " (ข้อมูลธนาคารฝากเข้าบัญชี)");
                            LogMessage.WriteLog("", "DEPTACCOUNT_NO = " + Deptaccount_No + " (เลขบัญชีสหกรณ์ที่ผู้ไว้กับสมาชิก)");
                            if (DeptTranfer != Deptaccount_No)
                            {
                                LogMessage.WriteLog("ResponseCode", "[" + ResponseCode.InvalidAccountType + "] Invalid Account Type or Contact number");
                                DataResponse.ResponseCode = ResponseCode.InvalidAccountType;
                                return;
                            }
                            else
                            {
                                DataResponse.Amount3 = LedgerBalance + Item_Amt; //คงเหลือ
                                //###### ตัดยอด ######
                                String System_Code = "02"; //เงินฝาก
                                String Operate_Code = "003"; //ประเภทฝาก
                                String Cash_type = "TRN";
                                Pay.ProcessPayment(Member_No, Coop_Id, Deptaccount_No, Item_Amt, DataRequest.TransactionDateTime, System_Code, Operate_Code, Cash_type, DataRequest.AcquirerTerminalNumber, DataRequest.TerminalSequenceNo, DataRequest.COOPCustomerAC.ToString("0000000000"), LogMessage, ta);
                            }
                            break;
                        case "34": //ชำระเงินกู้
                            String Loancontract_No = String.Empty;
                            LogMessage.WriteLog("TransactionType", "Tranfer Loan [ชำระเงินกู้] ####################");
                            Inq.LoanInquiry(Coop_Id, Member_No, LogMessage, ta);

                            Item_Amt = DataRequest.Amount1;
                            LogMessage.WriteLog("Payment Amount", Item_Amt.ToString("#,##0.00"));

                            if (Inq.LoanHold == 1 || Inq.AccountHold == 1) //ปิดระบบเงินฝาก
                            {
                                LogMessage.WriteLog("", "HOLD_FLAG = " + Inq.LoanHold.ToString() + " [1 = ปิดระบบเงินกู้], ACCOUNT_HOLD = " + Inq.AccountHold + " [1 = อายัดสัญญาเงินกู้]");
                                LogMessage.WriteLog("ResponseCode", "[" + ResponseCode.SystemNotAvailable + "] System Not Available");
                                DataResponse.ResponseCode = ResponseCode.SystemNotAvailable;
                                return;
                            }
                            else
                            {
                                LedgerBalance = Inq.LedgerBalance;//คงเหลือ
                                AvailableBalance = Inq.AvailableBalance;//ถอนได้
                                Loancontract_No = Inq.LoancontractNo;
                            }

                            String LoanTranfer = DataRequest.COOPCustomerAC.ToString("00000000"); //ใช้อ้างอิงเลขที่ ขั้นต่ำ 10 หลัก โดยหลักการจะให้ใส่เลขที่สัญญาที่จะทำการชำระ
                            LogMessage.WriteLog("", "MEMBER_NO = " + Member_No + " (เลขที่สมาชิกที่ใช้ชำระหนี้)");
                            LogMessage.WriteLog("", "LOANCONTRACT_NO = " + Loancontract_No + " (เลขบัญชีสหกรณ์ที่ผู้ไว้กับสมาชิก)");
                            LogMessage.WriteLog("", "TRANFER_NO = " + LoanTranfer + " (เลขที่อ้างอิง)");

                            if (LoanTranfer != Inq.Member_No && false)
                            {
                                LogMessage.WriteLog("ResponseCode", "[" + ResponseCode.InvalidAccountType + "] Invalid Account Type or Contact number");
                                DataResponse.ResponseCode = ResponseCode.InvalidAccountType;
                                return;
                            }
                            else
                            {
                                DataResponse.Amount3 = LedgerBalance + Item_Amt; //คงเหลือ
                                //###### ตัดยอด ######
                                String System_Code = "01"; //เงินกู้
                                String Operate_Code = "003"; //ประเภทชำระหนี้
                                String Cash_type = "TRN";
                                Pay.ProcessPayment(Member_No, Coop_Id, Loancontract_No, Item_Amt, DataRequest.TransactionDateTime, System_Code, Operate_Code, Cash_type, DataRequest.AcquirerTerminalNumber, DataRequest.TerminalSequenceNo, DataRequest.COOPCustomerAC.ToString("0000000000"), LogMessage, ta);
                            }
                            break;
                        default:
                            DataResponse.ResponseCode = ResponseCode.TransactionNotAuthorized;
                            break;
                    }
                }
                #endregion
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
                Sdt dt;
                String SqlGetAccount = String.Empty;
                int RowCount = 0;
                String Member_No = DataRequest.COOPCustomerID.ToString("00000000");
                String Coop_Id = DataRequest.COOPFIID;
                Decimal LedgerBalance = 0;
                Decimal AvailableBalance = 0;
                Decimal Item_Amt = 0;
                DataResponse.TransactionMessageCode = "0410";
                //#######################################################################################################################
                #region ถอนสด
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
                            String Deptaccount_No = String.Empty;
                            LogMessage.WriteLog("TransactionType", "Cancel Cash Withdraw Deposit [ยกเลิก ถอนเงินฝากแบบเงินสด] ####################");

                            Item_Amt = DataRequest.Amount1;
                            LogMessage.WriteLog("Withdraw Amount", Item_Amt.ToString("#,##0.00"));

                            SqlGetAccount = "SELECT ACCOUNT_NO FROM ATMTRANSACTION WHERE MEMBER_NO = {0} AND CCS_OPERATE_DATE = {1}";
                            SqlGetAccount = WebUtil.SQLFormat(SqlGetAccount, Member_No, DataRequest.TransactionDateTime);
                            LogMessage.WriteLog("TRANSACTION SQL", SqlGetAccount);
                            dt = ta.Query(SqlGetAccount);
                            RowCount = dt.GetRowCount();
                            if (RowCount != 1)
                            {
                                LogMessage.WriteLog("", "พบข้อมูลทั้งหมด = " + RowCount);
                                return;
                            }
                            else if (dt.Next())
                            {
                                Deptaccount_No = dt.GetString("ACCOUNT_NO");
                                LogMessage.WriteLog("", "DEPTACCOUNT_NO = " + Deptaccount_No);

                                //###### ย้อนรายการ ######
                                Receive Recv = new Receive();
                                String System_Code = "02"; //เงินฝาก
                                String Operate_Code = "002"; //ประเภทถอน
                                String Cash_type = "CSH";
                                Recv.ProcessCancelWithdraw(Member_No, Coop_Id, Deptaccount_No, Item_Amt, DataRequest.TransactionDateTime, System_Code, Operate_Code, Cash_type, DataRequest.AcquirerTerminalNumber, DataRequest.TerminalSequenceNo, DataRequest.COOPCustomerAC.ToString("0000000000"), LogMessage, ta);
                            }
                            break;
                        case "34": //กู้เงินกู้
                            String Loancontract_No = String.Empty;
                            LogMessage.WriteLog("TransactionType", "Cancel Cash Withdraw Loan [ยกเลิก ถอน(กู้เพิ่ม)เงินกู้แบบเงินสด] ####################");

                            Item_Amt = DataRequest.Amount1;
                            LogMessage.WriteLog("Withdraw Amount", Item_Amt.ToString("#,##0.00"));

                            SqlGetAccount = "SELECT ACCOUNT_NO FROM ATMTRANSACTION WHERE MEMBER_NO = {0} AND CCS_OPERATE_DATE = {1}";
                            SqlGetAccount = WebUtil.SQLFormat(SqlGetAccount, Member_No, DataRequest.TransactionDateTime);
                            LogMessage.WriteLog("TRANSACTION SQL", SqlGetAccount);
                            dt = ta.Query(SqlGetAccount);
                            RowCount = dt.GetRowCount();
                            if (RowCount != 1)
                            {
                                LogMessage.WriteLog("", "พบข้อมูลทั้งหมด = " + RowCount);
                                return;
                            }
                            else if (dt.Next())
                            {
                                Loancontract_No = dt.GetString("ACCOUNT_NO");
                                LogMessage.WriteLog("", "LOANACCOUNT_NO = " + Loancontract_No);

                                //###### ย้อนรายการ ######
                                Receive Recv = new Receive();
                                String System_Code = "01"; //เงินกู้
                                String Operate_Code = "002"; //ประเภทถอน
                                String Cash_type = "CSH";
                                Recv.ProcessCancelWithdraw(Member_No, Coop_Id, Loancontract_No, Item_Amt, DataRequest.TransactionDateTime, System_Code, Operate_Code, Cash_type, DataRequest.AcquirerTerminalNumber, DataRequest.TerminalSequenceNo, DataRequest.COOPCustomerAC.ToString("0000000000"), LogMessage, ta);
                            }
                            break;
                        default:
                            DataResponse.ResponseCode = ResponseCode.TransactionNotAuthorized;
                            break;
                    }
                }
                #endregion
                //#######################################################################################################################
                #region ถอนโอน
                else if (DataRequest.TransactionCode == 42) //ถอนเงินแบบโอน
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
                            String Deptaccount_No = String.Empty;
                            LogMessage.WriteLog("TransactionType", "Cancel Cash Withdraw Deposit [ยกเลิก ถอนเงินฝากแบบโอนเข้าบัญชี] ####################");

                            Item_Amt = DataRequest.Amount1;
                            LogMessage.WriteLog("Withdraw Amount", Item_Amt.ToString("#,##0.00"));

                            SqlGetAccount = "SELECT ACCOUNT_NO FROM ATMTRANSACTION WHERE MEMBER_NO = {0} AND CCS_OPERATE_DATE = {1}";
                            SqlGetAccount = WebUtil.SQLFormat(SqlGetAccount, Member_No, DataRequest.TransactionDateTime);
                            LogMessage.WriteLog("TRANSACTION SQL", SqlGetAccount);
                            dt = ta.Query(SqlGetAccount);
                            RowCount = dt.GetRowCount();
                            if (RowCount != 1)
                            {
                                LogMessage.WriteLog("", "พบข้อมูลทั้งหมด = " + RowCount);
                                return;
                            }
                            else if (dt.Next())
                            {
                                Deptaccount_No = dt.GetString("ACCOUNT_NO");
                                LogMessage.WriteLog("", "DEPTACCOUNT_NO = " + Deptaccount_No);

                                //###### ย้อนรายการ ######
                                Receive Recv = new Receive();
                                String System_Code = "02"; //เงินฝาก
                                String Operate_Code = "002"; //ประเภทถอน
                                String Cash_type = "TRN";
                                Recv.ProcessCancelWithdraw(Member_No, Coop_Id, Deptaccount_No, Item_Amt, DataRequest.TransactionDateTime, System_Code, Operate_Code, Cash_type, DataRequest.AcquirerTerminalNumber, DataRequest.TerminalSequenceNo, DataRequest.COOPCustomerAC.ToString("0000000000"), LogMessage, ta);
                            }
                            break;
                        case "34": //กู้เงินกู้
                            String Loancontract_No = String.Empty;
                            LogMessage.WriteLog("TransactionType", "Cancel Tranfer Withdraw Loan [ยกเลิก ถอน(กู้เพิ่ม)เงินกู้แบบโอน] ####################");

                            Item_Amt = DataRequest.Amount1;
                            LogMessage.WriteLog("Withdraw Amount", Item_Amt.ToString("#,##0.00"));

                            SqlGetAccount = "SELECT ACCOUNT_NO FROM ATMTRANSACTION WHERE MEMBER_NO = {0} AND CCS_OPERATE_DATE = {1}";
                            SqlGetAccount = WebUtil.SQLFormat(SqlGetAccount, Member_No, DataRequest.TransactionDateTime);
                            LogMessage.WriteLog("TRANSACTION SQL", SqlGetAccount);
                            dt = ta.Query(SqlGetAccount);
                            RowCount = dt.GetRowCount();
                            if (RowCount != 1)
                            {
                                LogMessage.WriteLog("", "พบข้อมูลทั้งหมด = " + RowCount);
                                return;
                            }
                            else if (dt.Next())
                            {
                                Loancontract_No = dt.GetString("ACCOUNT_NO");
                                LogMessage.WriteLog("", "LOANACCOUNT_NO = " + Loancontract_No);

                                //###### ย้อนรายการ ######
                                Receive Recv = new Receive();
                                String System_Code = "01"; //เงินกู้
                                String Operate_Code = "002"; //ประเภทถอน
                                String Cash_type = "TRN";
                                Recv.ProcessCancelWithdraw(Member_No, Coop_Id, Loancontract_No, Item_Amt, DataRequest.TransactionDateTime, System_Code, Operate_Code, Cash_type, DataRequest.AcquirerTerminalNumber, DataRequest.TerminalSequenceNo, DataRequest.COOPCustomerAC.ToString("0000000000"), LogMessage, ta);
                            }
                            break;
                        default:
                            DataResponse.ResponseCode = ResponseCode.TransactionNotAuthorized;
                            break;
                    }

                }
                #endregion
                //#######################################################################################################################
                #region ฝากเงิน
                else if (DataRequest.TransactionCode == 43) //ใช้ชำระหนี้ หรือฝากเงิน
                {
                    Payment Pay = new Payment();
                    switch (DataRequest.ToAccountCode.ToString("00"))
                    {
                        case "01":
                            DataResponse.ResponseCode = ResponseCode.TransactionNotAuthorized;
                            break;
                        case "11":
                            DataResponse.ResponseCode = ResponseCode.TransactionNotAuthorized;
                            break;
                        case "14": //ฝากเงินฝาก
                            String Deptaccount_No = String.Empty;
                            LogMessage.WriteLog("TransactionType", "Cancel Tranfer Deposit [ยกเลิก ฝากเงินฝาก] ####################");

                            Item_Amt = DataRequest.Amount1;
                            LogMessage.WriteLog("Deposit Amount", Item_Amt.ToString("#,##0.00"));

                            SqlGetAccount = "SELECT ACCOUNT_NO FROM ATMTRANSACTION WHERE MEMBER_NO = {0} AND CCS_OPERATE_DATE = {1}";
                            SqlGetAccount = WebUtil.SQLFormat(SqlGetAccount, Member_No, DataRequest.TransactionDateTime);
                            LogMessage.WriteLog("TRANSACTION SQL", SqlGetAccount);
                            dt = ta.Query(SqlGetAccount);
                            RowCount = dt.GetRowCount();
                            if (RowCount != 1)
                            {
                                LogMessage.WriteLog("", "พบข้อมูลทั้งหมด = " + RowCount);
                                return;
                            }
                            else if (dt.Next())
                            {
                                Deptaccount_No = dt.GetString("ACCOUNT_NO");
                                LogMessage.WriteLog("", "DEPTACCOUNT_NO = " + Deptaccount_No);

                                //###### ย้อนรายการ ######
                                String System_Code = "02"; //เงินฝาก
                                String Operate_Code = "003"; //ประเภทฝาก
                                String Cash_type = "TRN";
                                Pay.ProcessCancelPayment(Member_No, Coop_Id, Deptaccount_No, Item_Amt, DataRequest.TransactionDateTime, System_Code, Operate_Code, Cash_type, DataRequest.AcquirerTerminalNumber, DataRequest.TerminalSequenceNo, DataRequest.COOPCustomerAC.ToString("0000000000"), LogMessage, ta);
                            }
                            break;
                        case "34": //ชำระเงินกู้
                            String Loancontract_No = String.Empty;
                            LogMessage.WriteLog("TransactionType", "Tranfer Loan [ชำระเงินกู้] ####################");
                            Inq.LoanInquiry(Coop_Id, Member_No, LogMessage, ta);

                            Item_Amt = DataRequest.Amount1;
                            LogMessage.WriteLog("Payment Amount", Item_Amt.ToString("#,##0.00"));

                            SqlGetAccount = "SELECT ACCOUNT_NO FROM ATMTRANSACTION WHERE MEMBER_NO = {0} AND CCS_OPERATE_DATE = {1}";
                            SqlGetAccount = WebUtil.SQLFormat(SqlGetAccount, Member_No, DataRequest.TransactionDateTime);
                            LogMessage.WriteLog("TRANSACTION SQL", SqlGetAccount);
                            dt = ta.Query(SqlGetAccount);
                            RowCount = dt.GetRowCount();
                            if (RowCount != 1)
                            {
                                LogMessage.WriteLog("", "พบข้อมูลทั้งหมด = " + RowCount);
                                return;
                            }
                            else if (dt.Next())
                            {
                                Loancontract_No = dt.GetString("ACCOUNT_NO");
                                LogMessage.WriteLog("", "LOANACCOUNT_NO = " + Loancontract_No);

                                //###### ย้อนรายการ ######
                                String System_Code = "01"; //เงินกู้
                                String Operate_Code = "003"; //ประเภทชำระหนี้
                                String Cash_type = "TRN";
                                Pay.ProcessCancelPayment(Member_No, Coop_Id, Loancontract_No, Item_Amt, DataRequest.TransactionDateTime, System_Code, Operate_Code, Cash_type, DataRequest.AcquirerTerminalNumber, DataRequest.TerminalSequenceNo, DataRequest.COOPCustomerAC.ToString("0000000000"), LogMessage, ta);
                            }
                            break;
                        default:
                            DataResponse.ResponseCode = ResponseCode.TransactionNotAuthorized;
                            break;
                    }
                }
                #endregion
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
    }
}