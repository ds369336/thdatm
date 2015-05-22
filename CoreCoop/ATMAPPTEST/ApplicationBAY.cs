using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Collections.Specialized;
using System.IO;
using System.Threading;
using System.Security.Cryptography.X509Certificates;
using System.Globalization;

namespace ATMAPPTEST
{
    public partial class ApplicationBAY : Form
    {
        private String DataMassage = String.Empty;
        private int Step = 0;
        private int Mode = 0;
        private int page1 = 0;
        private int page2 = 0;
        private int page3 = 0;
        private int page4 = 0;
        private String url = "https://127.0.0.1/AtmCoreCoopBAY/ATMBAY/ATMcore.aspx";
        public ApplicationBAY()
        {
            InitializeComponent();
            StepFirst();
        }

        private String SendData(String DataMassage)
        {
            try
            {
                //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + "?DataMassage=" + DataMassage);
                //request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; .NET CLR 1.0.3705)";
                //request.Method = "POST";
                //request.GetRequestStream();

                //HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                //using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                //{
                //    return reader.ReadToEnd();
                //}
                System.Net.ServicePointManager.CertificatePolicy = new MyPolicy();
                X509Certificate2 Cert = new X509Certificate2(TB_CERT.Text, "1234", X509KeyStorageFlags.MachineKeySet);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + "?DataMassage=" + DataMassage);
                request.ClientCertificates.Add(Cert);
                request.Method = "POST";
                request.GetRequestStream();

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ClearBT()
        {
            try
            {
                WriteLog("Step = " + Step + " " + page1 + "/" + page2 + "/" + page3 + "/" + page4 + " Mode = " + Mode);
                bt1.Text = "";
                bt2.Text = "";
                bt3.Text = "";
                bt4.Text = "";
                bt5.Text = "";
                bt6.Text = "";
                bt7.Text = "";
                bt8.Text = "";
                bt1.Enabled = true;
                bt2.Enabled = true;
                bt3.Enabled = true;
                bt4.Enabled = true;
                bt5.Enabled = true;
                bt6.Enabled = true;
                bt7.Enabled = true;
                bt8.Enabled = true;

                TBM_01.Text = "";
                TBM_02.Text = "";
            }
            catch (Exception ex)
            {
                TB_Exception.Text = ex.Message;
                WriteLog(ex.Message);
            }
        }

        private void StepFirst()
        {
            try
            {
                Step = 0;
                Mode = 0;
                page1 = 0;
                page2 = 0;
                page3 = 0;
                page4 = 0;
                ClearBT();
                bt1.Text = "สอบถามยอด";
                bt2.Text = "โอนเงิน/ชำระเงินกู้";
                bt3.Text = "ถอนเงิน";
                bt4.Text = "ค้นหาชื่อสมาชิก";
                bt5.Enabled = false;
                bt6.Enabled = false;
                bt7.Enabled = false;
                bt8.Enabled = false;
            }
            catch (Exception ex)
            {
                TB_Exception.Text = ex.Message;
                WriteLog(ex.Message);
            }
        }

        private void Step2(int Function)
        {
            try
            {
                ClearBT();
                switch (Function)
                {
                    case 1://สอบถามยอด
                        bt1.Text = "บัญชีออมทรัพย์สหกรณ์";
                        bt2.Text = "บัญชีเงินกู้สหกรณ์";
                        //bt3.Text = "บัญชีออมทรัพย์ธนาคารกรุงศรี";
                        //bt4.Text = "บัญชีกระแสรายวันธนาคารกรุงศรี";
                        bt3.Enabled = false;
                        bt4.Text = "ย้อนกลับ";
                        bt5.Enabled = false;
                        bt6.Enabled = false;
                        bt7.Enabled = false;
                        bt8.Enabled = false;
                        break;
                    case 2:
                    case 3: //ถอนเงิน
                        bt1.Text = "บัญชีออมทรัพย์สหกรณ์";
                        bt2.Text = "บัญชีเงินกู้สหกรณ์";
                        //bt3.Text = "บัญชีออมทรัพย์ธนาคารกรุงศรี";
                        //bt4.Text = "บัญชีกระแสรายวันธนาคารกรุงศรี";
                        bt3.Enabled = false;
                        bt4.Text = "ย้อนกลับ";
                        bt5.Enabled = false;
                        bt6.Enabled = false;
                        bt7.Enabled = false;
                        bt8.Enabled = false;
                        break;
                    case 4:
                        AccountInquiry();
                        StepFirst();
                        break;
                }
            }
            catch (Exception ex)
            {
                TB_Exception.Text = ex.Message;
                WriteLog(ex.Message);
            }
        }

        private void Step3()
        {
            try
            {
                if (page1 == 1)
                {
                    switch (page2)
                    {
                        case 1: //สอบถามยอด >> บัญชีออมทรัพย์สหกรณ์
                            ClearBT();
                            bt1.Enabled = false;
                            bt2.Enabled = false;
                            bt3.Enabled = false;
                            bt4.Text = "ย้อนกลับ";
                            bt5.Enabled = false;
                            bt6.Enabled = false;
                            bt7.Enabled = false;
                            bt8.Enabled = false;
                            DeptInquiry();
                            break;
                        case 2: //สอบถามยอด >> บัญชีเงินกู้สหกรณ์
                            ClearBT();
                            bt1.Enabled = false;
                            bt2.Enabled = false;
                            bt3.Enabled = false;
                            bt4.Text = "ย้อนกลับ";
                            bt5.Enabled = false;
                            bt6.Enabled = false;
                            bt7.Enabled = false;
                            bt8.Enabled = false;
                            LoanInquiry();
                            break;
                        case 3: break;
                        case 4: break;
                    }
                }
                else if (page1 == 2)
                {
                    switch (page2)
                    {
                        case 1: //ฝากเงิน >> บัญชีออมทรัพย์สหกรณ์
                            ClearBT();
                            bt1.Text = "ฝากเงินระบุจำนวน";
                            bt2.Text = "100";
                            bt3.Text = "200";
                            bt4.Text = "ย้อนกลับ";
                            bt5.Text = "500";
                            bt6.Text = "1000";
                            bt7.Text = "5000";
                            bt8.Text = "10000";
                            ITEM_AMT.Visible = true;
                            //DeptInquiry();
                            break;
                        case 2: //ชำระเงิน >> บัญชีเงินกู้สหกรณ์
                            ClearBT();
                            bt1.Text = "ชำระเงินระบุจำนวน";
                            bt2.Text = "100";
                            bt3.Text = "200";
                            bt4.Text = "ย้อนกลับ";
                            bt5.Text = "500";
                            bt6.Text = "1000";
                            bt7.Text = "5000";
                            bt8.Text = "10000";
                            ITEM_AMT.Visible = true;
                            //LoanInquiry();
                            break;
                        case 3: break;
                        case 4: break;
                    }
                }
                else if (page1 == 3)
                {
                    switch (page2)
                    {
                        case 1: //ถอนเงิน >> บัญชีออมทรัพย์สหกรณ์
                            ClearBT();
                            bt1.Text = "ถอนระบุจำนวน";
                            bt2.Text = "100";
                            bt3.Text = "200";
                            bt4.Text = "ย้อนกลับ";
                            bt5.Text = "500";
                            bt6.Text = "1000";
                            bt7.Text = "5000";
                            bt8.Text = "10000";
                            ITEM_AMT.Visible = true;
                            //DeptInquiry();
                            break;
                        case 2: //ถอนเงิน >> บัญชีเงินกู้สหกรณ์
                            ClearBT();
                            bt1.Text = "ถอนระบุจำนวน";
                            bt2.Text = "100";
                            bt3.Text = "200";
                            bt4.Text = "ย้อนกลับ";
                            bt5.Text = "500";
                            bt6.Text = "1000";
                            bt7.Text = "5000";
                            bt8.Text = "10000";
                            ITEM_AMT.Visible = true;
                            //LoanInquiry();
                            break;
                        case 3: break;
                        case 4: break;
                    }
                }
            }
            catch (Exception ex)
            {
                TB_Exception.Text = ex.Message;
                WriteLog(ex.Message);
            }
        }

        private void Step4()
        {
            try
            {
                if (page1 == 2)
                {
                    if (page2 == 1)
                    {
                        if (page3 == 1) LoanWithdraw(Convert.ToDecimal(ITEM_AMT.Value));
                        else if (page3 == 2) LoanWithdraw(100);
                        else if (page3 == 3) LoanWithdraw(200);
                        else if (page3 == 5) LoanWithdraw(500);
                        else if (page3 == 6) LoanWithdraw(1000);
                        else if (page3 == 7) LoanWithdraw(5000);
                        else if (page3 == 8) LoanWithdraw(10000);
                        StepFirst();
                    }
                    else if (page2 == 2)
                    {
                        if (page3 == 1) LoanWithdraw(Convert.ToDecimal(ITEM_AMT.Value));
                        else if (page3 == 2) LoanWithdraw(100);
                        else if (page3 == 3) LoanWithdraw(200);
                        else if (page3 == 5) LoanWithdraw(500);
                        else if (page3 == 6) LoanWithdraw(1000);
                        else if (page3 == 7) LoanWithdraw(5000);
                        else if (page3 == 8) LoanWithdraw(10000);
                        StepFirst();
                    }
                }
                else if (page1 == 3)
                {
                    if (page2 == 1)
                    {
                        if (page3 == 1) DeptWithdraw(Convert.ToDecimal(ITEM_AMT.Value));
                        else if (page3 == 2) DeptWithdraw(100);
                        else if (page3 == 3) DeptWithdraw(200);
                        else if (page3 == 5) DeptWithdraw(500);
                        else if (page3 == 6) DeptWithdraw(1000);
                        else if (page3 == 7) DeptWithdraw(5000);
                        else if (page3 == 8) DeptWithdraw(10000);
                        StepFirst();
                    }
                    else if (page2 == 2)
                    {
                        if (page3 == 1) LoanWithdraw(Convert.ToDecimal(ITEM_AMT.Value));
                        else if (page3 == 2) LoanWithdraw(100);
                        else if (page3 == 3) LoanWithdraw(200);
                        else if (page3 == 5) LoanWithdraw(500);
                        else if (page3 == 6) LoanWithdraw(1000);
                        else if (page3 == 7) LoanWithdraw(5000);
                        else if (page3 == 8) LoanWithdraw(10000);
                        StepFirst();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CheckOutput(ref String DataOutput)
        {
            if (DataOutput.Length != 320)
            {
                int looptmp = 0;
                for (int i = DataOutput.Length; DataOutput.Length < 320; i++)
                {
                    looptmp++;
                    DataOutput += " ";
                    if (looptmp > 320) break;
                }
            }
        }

        private void DeptInquiry()
        {
            try
            {
                String ATMCARD_ID = TB_ATMCARD_ID.Text.Trim();
                DataEncode Data = new DataEncode();
                Data.TransactionMessageCode = "0700";

                Data.ServiceType = "EATM";

                Data.TransactionCode = 30;
                Data.FromAccountCode = 14;

                Data.ToAccountCode = 0;
                Data.TransactionDateTime = DT_OPERATE_DATE.Value;

                Data.PANLength = 0;
                Data.PANNumber = ATMCARD_ID;

                Data.PINBlock = "";

                Data.AcquirerTerminalNumber = TB_ATM_NO.Text;
                Data.AcquirerTerminalLocation = "";
                //Data.AcquirerTerminalOwner = "xxx"; //Defalt ไว้แล้ว
                //Data.AcquirerTerminalCity = "xxx"; //Defalt ไว้แล้ว
                Data.TerminalSequenceNo = Convert.ToUInt32(TB_TRACE_NUMBER.Text);
                Data.AcquirerTraceNumber = 0;

                uint COOPCustomerID = 0;
                try
                {
                    COOPCustomerID = Convert.ToUInt32(TB_MEMBER_NO.Text);
                }
                catch
                {
                    throw new Exception("กรุณาระบุเลขที่สมาชิกเป็นตัวเลขเท่านั้น");
                }
                Data.COOPFIID = "";
                Data.COOPCustomerID = COOPCustomerID;
                Data.COOPCustomerAC = 0;
                Data.COOPBankAC = 0;
                Data.COOPCustomerBankAC = 0;
                Data.IssuerReference = "";

                Data.Amount1 = 0;
                Data.Amount2 = 0;
                Data.Amount3 = 0;

                Data.ResponseCode = 0;
                Data.ReversalCode = 0;
                Data.ApproveCode = 0;
                Data.ResponseMessage = "";

                String Input = Data.DataMassage;
                WriteLog("=================== DataMassage ===================");
                WriteLog("Request:" + Input);
                String Output = SendData(Input);
                CheckOutput(ref Output);
                WriteLog("Response:" + Output);
                WriteLog("===================================================");
                Data = new DataEncode(Output);
                TBM_01.Text = "ยอดเงินที่สามารถถอนได้ " + Data.Amount2.ToString("#,##0.00");
                TBM_02.Text = "ยอดเงินคงเหลือทั้งหมด " + Data.Amount2.ToString("#,##0.00");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoanInquiry()
        {
            try
            {
                String ATMCARD_ID = TB_ATMCARD_ID.Text.Trim();
                DataEncode Data = new DataEncode();
                Data.TransactionMessageCode = "0700";

                Data.ServiceType = "EATM";

                Data.TransactionCode = 30;
                Data.FromAccountCode = 34;

                Data.ToAccountCode = 0;
                Data.TransactionDateTime = DT_OPERATE_DATE.Value;

                Data.PANLength = 0;
                Data.PANNumber = ATMCARD_ID;

                Data.PINBlock = "";

                Data.AcquirerTerminalNumber = TB_ATM_NO.Text;
                Data.AcquirerTerminalLocation = "";
                //Data.AcquirerTerminalOwner = "xxx"; //Defalt ไว้แล้ว
                //Data.AcquirerTerminalCity = "xxx"; //Defalt ไว้แล้ว
                Data.TerminalSequenceNo = Convert.ToUInt32(TB_TRACE_NUMBER.Text);
                Data.AcquirerTraceNumber = 0;

                Data.COOPFIID = "";
                Data.COOPCustomerID = 0;
                Data.COOPCustomerAC = 0;
                Data.COOPBankAC = 0;
                Data.COOPCustomerBankAC = 0;
                Data.IssuerReference = "";

                Data.Amount1 = 0;
                Data.Amount2 = 0;
                Data.Amount3 = 0;

                Data.ResponseCode = 0;
                Data.ReversalCode = 0;
                Data.ApproveCode = 0;
                Data.ResponseMessage = "";

                String Input = Data.DataMassage;
                WriteLog("=================== DataMassage ===================");
                WriteLog("Request:" + Input);
                String Output = SendData(Input);
                CheckOutput(ref Output);
                WriteLog("Response:" + Output);
                WriteLog("===================================================");
                Data = new DataEncode(Output);
                TBM_01.Text = "ยอดเงินที่สามารถกู้ได้ " + Data.Amount2.ToString("#,##0.00");
                TBM_02.Text = "ยอดเงินคงเหลือทั้งหมด " + Data.Amount2.ToString("#,##0.00");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void DeptDeposit(Decimal Item_Amt)
        {
            try
            {
                String ATMCARD_ID = TB_ATMCARD_ID.Text.Trim();
                DataEncode Data = new DataEncode();
                Data.TransactionMessageCode = "0200";

                Data.ServiceType = "EATM";

                Data.TransactionCode = 10;
                Data.FromAccountCode = 42;

                Data.ToAccountCode = 0;
                Data.TransactionDateTime = DT_OPERATE_DATE.Value;

                Data.PANLength = 0;
                Data.PANNumber = ATMCARD_ID;

                Data.PINBlock = "";

                Data.AcquirerTerminalNumber = TB_ATM_NO.Text;
                Data.AcquirerTerminalLocation = "";
                //Data.AcquirerTerminalOwner = "xxx"; //Defalt ไว้แล้ว
                //Data.AcquirerTerminalCity = "xxx"; //Defalt ไว้แล้ว
                Data.TerminalSequenceNo = Convert.ToUInt32(TB_TRACE_NUMBER.Text);
                Data.AcquirerTraceNumber = 0;

                Data.COOPFIID = "";
                Data.COOPCustomerID = 0;
                Data.COOPCustomerAC = 0;
                Data.COOPBankAC = 0;
                Data.COOPCustomerBankAC = 0;
                Data.IssuerReference = "";

                Data.Amount1 = Item_Amt;
                Data.Amount2 = 0;
                Data.Amount3 = 0;

                Data.ResponseCode = 0;
                Data.ReversalCode = 0;
                Data.ApproveCode = 0;
                Data.ResponseMessage = "";

                String Input = Data.DataMassage;
                WriteLog("=================== DataMassage ===================");
                WriteLog("Request:" + Input);
                String Output = SendData(Input);
                CheckOutput(ref Output);
                WriteLog("Response:" + Output);
                WriteLog("===================================================");
                Data = new DataEncode(Output);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoanDeposit(Decimal Item_Amt)
        {
            try
            {
                String ATMCARD_ID = TB_ATMCARD_ID.Text.Trim();
                DataEncode Data = new DataEncode();
                Data.TransactionMessageCode = "0200";

                Data.ServiceType = "EATM";

                Data.TransactionCode = 10;
                Data.FromAccountCode = 43;

                Data.ToAccountCode = 0;
                Data.TransactionDateTime = DT_OPERATE_DATE.Value;

                Data.PANLength = 0;
                Data.PANNumber = ATMCARD_ID;

                Data.PINBlock = "";

                Data.AcquirerTerminalNumber = TB_ATM_NO.Text;
                Data.AcquirerTerminalLocation = "";
                //Data.AcquirerTerminalOwner = "xxx"; //Defalt ไว้แล้ว
                //Data.AcquirerTerminalCity = "xxx"; //Defalt ไว้แล้ว
                Data.TerminalSequenceNo = Convert.ToUInt32(TB_TRACE_NUMBER.Text);
                Data.AcquirerTraceNumber = 0;

                Data.COOPFIID = "";
                Data.COOPCustomerID = 0;
                Data.COOPCustomerAC = 0;
                Data.COOPBankAC = 0;
                Data.COOPCustomerBankAC = 0;
                Data.IssuerReference = "";

                Data.Amount1 = Item_Amt;
                Data.Amount2 = 0;
                Data.Amount3 = 0;

                Data.ResponseCode = 0;
                Data.ReversalCode = 0;
                Data.ApproveCode = 0;
                Data.ResponseMessage = "";

                String Input = Data.DataMassage;
                WriteLog("=================== DataMassage ===================");
                WriteLog("Request:" + Input);
                String Output = SendData(Input);
                CheckOutput(ref Output);
                WriteLog("Response:" + Output);
                WriteLog("===================================================");
                Data = new DataEncode(Output);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void DeptWithdraw(Decimal Item_Amt)
        {
            try
            {
                String ATMCARD_ID = TB_ATMCARD_ID.Text.Trim();
                DataEncode Data = new DataEncode();
                Data.TransactionMessageCode = "0200";

                Data.ServiceType = "EATM";

                Data.TransactionCode = 10;
                Data.FromAccountCode = 42;

                Data.ToAccountCode = 0;
                Data.TransactionDateTime = DT_OPERATE_DATE.Value;

                Data.PANLength = 0;
                Data.PANNumber = ATMCARD_ID;

                Data.PINBlock = "";

                Data.AcquirerTerminalNumber = TB_ATM_NO.Text;
                Data.AcquirerTerminalLocation = "";
                //Data.AcquirerTerminalOwner = "xxx"; //Defalt ไว้แล้ว
                //Data.AcquirerTerminalCity = "xxx"; //Defalt ไว้แล้ว
                Data.TerminalSequenceNo = Convert.ToUInt32(TB_TRACE_NUMBER.Text);
                Data.AcquirerTraceNumber = 0;

                Data.COOPFIID = "";
                Data.COOPCustomerID = 0;
                Data.COOPCustomerAC = 0;
                Data.COOPBankAC = 0;
                Data.COOPCustomerBankAC = 0;
                Data.IssuerReference = "";

                Data.Amount1 = Item_Amt;
                Data.Amount2 = 0;
                Data.Amount3 = 0;

                Data.ResponseCode = 0;
                Data.ReversalCode = 0;
                Data.ApproveCode = 0;
                Data.ResponseMessage = "";

                String Input = Data.DataMassage;
                WriteLog("=================== DataMassage ===================");
                WriteLog("Request:" + Input);
                String Output = SendData(Input);
                CheckOutput(ref Output);
                WriteLog("Response:" + Output);
                WriteLog("===================================================");
                Data = new DataEncode(Output);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoanWithdraw(Decimal Item_Amt)
        {
            try
            {
                String ATMCARD_ID = TB_ATMCARD_ID.Text.Trim();
                DataEncode Data = new DataEncode();
                Data.TransactionMessageCode = "0200";

                Data.ServiceType = "EATM";

                Data.TransactionCode = 10;
                Data.FromAccountCode = 43;

                Data.ToAccountCode = 0;
                Data.TransactionDateTime = DT_OPERATE_DATE.Value;

                Data.PANLength = 0;
                Data.PANNumber = ATMCARD_ID;

                Data.PINBlock = "";

                Data.AcquirerTerminalNumber = TB_ATM_NO.Text;
                Data.AcquirerTerminalLocation = "";
                //Data.AcquirerTerminalOwner = "xxx"; //Defalt ไว้แล้ว
                //Data.AcquirerTerminalCity = "xxx"; //Defalt ไว้แล้ว
                Data.TerminalSequenceNo = Convert.ToUInt32(TB_TRACE_NUMBER.Text);
                Data.AcquirerTraceNumber = 0;

                Data.COOPFIID = "";
                Data.COOPCustomerID = 0;
                Data.COOPCustomerAC = 0;
                Data.COOPBankAC = 0;
                Data.COOPCustomerBankAC = 0;
                Data.IssuerReference = "";

                Data.Amount1 = Item_Amt;
                Data.Amount2 = 0;
                Data.Amount3 = 0;

                Data.ResponseCode = 0;
                Data.ReversalCode = 0;
                Data.ApproveCode = 0;
                Data.ResponseMessage = "";

                String Input = Data.DataMassage;
                WriteLog("=================== DataMassage ===================");
                WriteLog("Request:" + Input);
                String Output = SendData(Input);
                CheckOutput(ref Output);
                WriteLog("Response:" + Output);
                WriteLog("===================================================");
                Data = new DataEncode(Output);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void AccountInquiry()
        {
            try
            {
                String ATMCARD_ID = TB_ATMCARD_ID.Text.Trim();
                DataEncode Data = new DataEncode();
                Data.TransactionMessageCode = "0100";

                Data.ServiceType = "EATM";

                Data.TransactionCode = 31;
                Data.FromAccountCode = 43;

                Data.ToAccountCode = 0;
                Data.TransactionDateTime = DT_OPERATE_DATE.Value;

                Data.PANLength = 0;
                Data.PANNumber = ATMCARD_ID;

                Data.PINBlock = "";

                Data.AcquirerTerminalNumber = TB_ATM_NO.Text;
                Data.AcquirerTerminalLocation = "";
                //Data.AcquirerTerminalOwner = "xxx"; //Defalt ไว้แล้ว
                //Data.AcquirerTerminalCity = "xxx"; //Defalt ไว้แล้ว
                Data.TerminalSequenceNo = Convert.ToUInt32(TB_TRACE_NUMBER.Text);
                Data.AcquirerTraceNumber = 0;

                Data.COOPFIID = "";
                Data.COOPCustomerID = 0;
                Data.COOPCustomerAC = 0;
                Data.COOPBankAC = 0;
                Data.COOPCustomerBankAC = 0;
                Data.IssuerReference = "";

                Data.Amount1 = 0;
                Data.Amount2 = 0;
                Data.Amount3 = 0;

                Data.ResponseCode = 0;
                Data.ReversalCode = 0;
                Data.ApproveCode = 0;
                Data.ResponseMessage = "";

                String Input = Data.DataMassage;
                WriteLog("=================== DataMassage ===================");
                WriteLog("Request:" + Input);
                String Output = SendData(Input);
                CheckOutput(ref Output);
                WriteLog("Response:" + Output);
                WriteLog("===================================================");
                Data = new DataEncode(Output);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void WriteLog(String Massage)
        {
            try
            {
                TB_LOG.AppendText(Massage + "\r\n");
                TB_LOG.ScrollToCaret();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void submit_Click(object sender, EventArgs e)
        {
            try
            {
                String Input = TB_Send.Text;
                WriteLog("=================== DataMassage ===================");
                WriteLog("Request:" + Input);
                String Output = SendData(Input);
                CheckOutput(ref Output);
                WriteLog("Response:" + Output);
                WriteLog("===================================================");
            }
            catch (Exception ex)
            {
                TB_Exception.Text = ex.Message;
                WriteLog(ex.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                TB_COREURL.Text = url;
                TB_CERT.Text = "C:\\GCOOP_ALL\\AtmCoreCoopBAY\\Certificate\\Certificate_THD_client.pfx";
                CheckUrl(url);

                TB_ATMCARD_ID.Text = "0123456789";
                DT_OPERATE_DATE.Value = DateTime.Now;
                DT_OPERATE_DATE.Format = DateTimePickerFormat.Short;
                TB_ATM_NO.Text = "BAY001";
                TB_TRACE_NUMBER.Text = "1";
                TB_MEMBER_NO.Text = "00001222";

                TBM_01.Text = "";
                TBM_02.Text = "";
            }
            catch (Exception ex)
            {
                TB_Exception.Text = ex.Message;
                WriteLog(ex.Message);
            }
        }

        private void bt1_Click(object sender, EventArgs e)
        {
            switch (Step)
            {
                case 0:
                    Mode = 1;
                    page1 = 1;
                    Step++;
                    Step2(1);
                    break;
                case 1:
                    page2 = 1;
                    Step++;
                    Step3();
                    break;
                case 2:
                    Step++;
                    page3 = 1;
                    Step4();
                    break;
                case 3:
                    Step++;
                    page3 = 1;
                    break;
            }
        }

        private void bt2_Click(object sender, EventArgs e)
        {
            switch (Step)
            {
                case 0:
                    Mode = 2;
                    page1 = 2;
                    Step++;
                    Step2(2);
                    break;
                case 1:
                    Step++;
                    page2 = 2;
                    Step3();
                    break;
                case 2:
                    Step++;
                    page3 = 2;
                    Step4();
                    break;
            }
        }

        private void bt3_Click(object sender, EventArgs e)
        {
            switch (Step)
            {
                case 0:
                    Mode = 3;
                    page1 = 3;
                    Step++;
                    Step2(3);
                    break;
                case 1: break;
                case 2:
                    Step++;
                    page3 = 3;
                    Step4();
                    break;
            }
        }

        private void bt4_Click(object sender, EventArgs e)
        {
            switch (Step)
            {
                case 0:
                    Mode = 4;
                    page1 = 4;
                    Step++;
                    Step2(4);
                    break;
                case 1:
                    StepFirst();
                    break;
                case 2:
                    StepFirst();
                    break;
                case 3:
                    StepFirst();
                    break;
                case 4:
                    StepFirst();
                    break;
                default: break;
            }
        }

        private void bt5_Click(object sender, EventArgs e)
        {
            switch (Step)
            {
                case 0:
                    break;
                case 1: break;
                case 2:
                    Step++;
                    page3 = 5;
                    Step4();
                    break;
            }
        }

        private void bt6_Click(object sender, EventArgs e)
        {
            switch (Step)
            {
                case 0:
                    break;
                case 1: break;
                case 2:
                    Step++;
                    page3 = 6;
                    Step4();
                    break;
            }
        }

        private void bt7_Click(object sender, EventArgs e)
        {
            switch (Step)
            {
                case 0:
                    break;
                case 1: break;
                case 2:
                    Step++;
                    page3 = 7;
                    Step4();
                    break;
            }
        }

        private void bt8_Click(object sender, EventArgs e)
        {
            switch (Step)
            {
                case 0:
                    break;
                case 1: break;
                case 2:
                    Step++;
                    page3 = 8;
                    Step4();
                    break;
            }
        }

        private void BT_CLEAR_LOG_Click(object sender, EventArgs e)
        {
            TB_LOG.Text = "";
        }

        private void BT_CHECK_WCF_Click(object sender, EventArgs e)
        {
            try
            {
                url = TB_COREURL.Text;
                CheckUrl(url);
            }
            catch (Exception ex)
            {
                TB_Exception.Text = ex.Message;
                WriteLog(ex.Message);
            }
        }

        private void CheckUrl(String StringURL)
        {
            try
            {
                WriteLog(StringURL);
                String DataSend = "TestWCF";
                String Result = SendData(DataSend);

                if (Result == "1")
                {
                    LB_Status.Text = "Online";
                    WriteLog("Result = " + Result + " : Online");
                }
                else
                {
                    LB_Status.Text = "Offline";
                    WriteLog("Result = " + Result + " : Offline");
                }
                WriteLog("__________________________________________________________________");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void BT_SYSTEM_CHECK_Click(object sender, EventArgs e)
        {
            try
            {
                WriteLog("================== System Check ===================");
                WriteLog("Response:" + SendData(""));
                WriteLog("===================================================");
            }
            catch (Exception ex)
            {
                TB_Exception.Text = ex.Message;
            }
        }

        private void PostGenFile_Click(object sender, EventArgs e)
        {
            WriteLog("================== Start GenFile ===================");
            try
            {
                StringWriter DataStringWriter = new StringWriter();
                CultureInfo en = new CultureInfo("en-US");
                //Header
                String Space = "                                                                                                    ";
                String REC_TYPE = "0";
                String COOP_ID = "097";
                String SETTLEMENT_DATE = DateTime.Now.ToString("dd/MM/yyyy", en);
                String FILE_DESC = "COOP";//35 หลัก
                FILE_DESC = (FILE_DESC + Space).Substring(0, 35);
                String COOP_TYPE = "007097";
                String RESERVE = (Space + Space + Space).Substring(0, 245);
                DataStringWriter.WriteLine(REC_TYPE + COOP_ID + SETTLEMENT_DATE + FILE_DESC + COOP_TYPE + RESERVE);

                //Detail
                REC_TYPE = "1";
                String COOP_CUST = "0000002990";
                RESERVE = "   ";
                String DATA_TYPE = "1";
                String FUNCTION = "A";
                String THAI_NAME = ("ประภาภรณ์ ประจักษ์" + Space).Substring(0, 50);
                String ENGS_NAME = ("PRAPAPRON PRAJUCK" + Space).Substring(0, 50);
                String SEX = "2";
                String BIRTH_DATE = "19990101";
                String CARD_TYPE = "01";
                String CARD_NUM = "1470500010444";
                String RESERVE2 = Space.Substring(0, 26);
                String CONTRACT_ADDR = Space.Substring(0, 80);
                String ACCOUNT_NO = "  00002990";
                String RESERVE3 = Space.Substring(0, 38);
                DataStringWriter.WriteLine(REC_TYPE + COOP_TYPE + COOP_CUST + RESERVE + DATA_TYPE + FUNCTION + THAI_NAME + ENGS_NAME + SEX + BIRTH_DATE + CARD_TYPE + CARD_NUM + RESERVE2 + CONTRACT_ADDR + ACCOUNT_NO + RESERVE3);

                DATA_TYPE = "2";
                String CUST_STATUS = "1";
                String CUST_EXPDATE = "20991231";
                String AUTH_INFORMATION = "00000010000" + "00004000000" + "99999" + "00100000000" + "99999" + "01000000000" + "99999" + "03000000000" + "99999" + "00000000000";
                String DEBT_INFORMATION = Space.Substring(0, 78);
                String BANK_INFO = Space.Substring(0, 10);
                RESERVE2 = Space.Substring(0, 95);
                DataStringWriter.WriteLine(REC_TYPE + COOP_TYPE + COOP_CUST + RESERVE + DATA_TYPE + FUNCTION + CUST_STATUS + CUST_EXPDATE + AUTH_INFORMATION + DEBT_INFORMATION + BANK_INFO + RESERVE2);

                //Tailer
                REC_TYPE = "9";
                String TOTAL_END = "END";
                String TOTAL_RECORDS = "000000002";
                String CUST_LIMIT_AMT = "0000000000000";
                String OD_PRINCIPAL = "0000000000000";
                RESERVE = (Space + Space + Space).Substring(0, 261);
                DataStringWriter.WriteLine(REC_TYPE + TOTAL_END + TOTAL_RECORDS + CUST_LIMIT_AMT + OD_PRINCIPAL + RESERVE);

                File.WriteAllText("C:\\test.txt", DataStringWriter.ToString(), Encoding.Default);

            }
            catch (Exception ex)
            {
                WriteLog("Error Exception:" + ex.Message);
            }
            WriteLog("===================================================");
        }

        private String COOP_TYPE = "007097";
        private String GetHeader()
        {
            try
            {
                CultureInfo en = new CultureInfo("en-US");
                String Space = "                                                                                                    ";
                String REC_TYPE = "0";
                String COOP_ID = "097";
                String SETTLEMENT_DATE = DateTime.Now.ToString("dd/MM/yyyy", en);
                String FILE_DESC = "COOP";//35 หลัก
                FILE_DESC = (FILE_DESC + Space).Substring(0, 35);
                String RESERVE = (Space + Space + Space).Substring(0, 245);
                return REC_TYPE + COOP_ID + SETTLEMENT_DATE + FILE_DESC + COOP_TYPE + RESERVE;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private String GetDetail()
        {
            try
            {
                String Space = "                                                                                                    ";
                String REC_TYPE = "1";
                String COOP_CUST = "0000002990";//******************************
                String RESERVE = "   ";
                String DATA_TYPE = "1";
                String FUNCTION = "A";//******************************
                String THAI_NAME = ("ประภาภรณ์ ประจักษ์" + Space).Substring(0, 50);
                String ENGS_NAME = ("PRAPAPRON PRAJUCK" + Space).Substring(0, 50);
                String SEX = "2";//******************************
                String BIRTH_DATE = "19990101";
                String CARD_TYPE = "01";
                String CARD_NUM = "1470500010444";//******************************
                String RESERVE2 = Space.Substring(0, 26);
                String CONTRACT_ADDR = Space.Substring(0, 80);
                String ACCOUNT_NO = "  00002990";//******************************
                String RESERVE3 = Space.Substring(0, 38);

                String SqlSelect = "SELECT COOP_ID, MEMBER_NO, INFO_FLAG FROM ATMBANKWAITPROCRESS WHERE PROCESS_FLAG <> 1";

                return REC_TYPE + COOP_TYPE + COOP_CUST + RESERVE + DATA_TYPE + FUNCTION + THAI_NAME + ENGS_NAME + SEX + BIRTH_DATE + CARD_TYPE + CARD_NUM + RESERVE2 + CONTRACT_ADDR + ACCOUNT_NO + RESERVE3;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private String GetTailer()
        {
            try
            {
                String Space = "                                                                                                    ";
                String REC_TYPE = "9";
                String TOTAL_END = "END";
                String TOTAL_RECORDS = "000000002";
                String CUST_LIMIT_AMT = "0000000000000";
                String OD_PRINCIPAL = "0000000000000";
                String RESERVE = (Space + Space + Space).Substring(0, 261);
                return REC_TYPE + TOTAL_END + TOTAL_RECORDS + CUST_LIMIT_AMT + OD_PRINCIPAL + RESERVE;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
