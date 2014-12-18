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
        private String url = "http://127.0.0.1/AtmCoreCoopBAY/ATMBAY/ATMcore.aspx";
        public ApplicationBAY()
        {
            InitializeComponent();
            StepFirst();
        }

        private String SendData(String DataMassage)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + "?DataMassage=" + DataMassage);
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; .NET CLR 1.0.3705)";
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
                switch (Function)
                {
                    case 1://สอบถามยอด
                        ClearBT();
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
                    case 2: break;
                    case 3: //ถอนเงิน
                        ClearBT();
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
                    case 4: break;
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
                if (page1 == 3)
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
                            TB_ITEM_AMT.Visible = true;
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
                            TB_ITEM_AMT.Visible = true;
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
                if (page1 == 3 && page2 == 1)
                {
                    switch (page3)
                    {
                        case 1:
                            DeptWithdraw(Convert.ToDecimal(TB_ITEM_AMT));
                            break;
                        case 2:
                            DeptWithdraw(100);
                            break;
                        case 3:
                            DeptWithdraw(100);
                            break;
                        case 5:
                            DeptWithdraw(100);
                            break;
                        case 6:
                            DeptWithdraw(100);
                            break;
                        case 7:
                            DeptWithdraw(100);
                            break;
                        case 8:
                            DeptWithdraw(1000);
                            break;
                        default: break;
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

                Data.ServiceType = "ATMs";

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
                WriteLog("Request :" + Input);
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

                Data.ServiceType = "ATMs";

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
                WriteLog("Request :" + Input);
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

        private void DeptWithdraw(Decimal Item_Amt)
        {
            try
            {
                String ATMCARD_ID = TB_ATMCARD_ID.Text.Trim();
                DataEncode Data = new DataEncode();
                Data.TransactionMessageCode = "0200";

                Data.ServiceType = "ATMs";

                Data.TransactionCode = 10;
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
                WriteLog("Request :" + Input);
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
                String DataSend = TB_Send.Text.Trim();
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + "?DataMassage=" + DataSend);
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; .NET CLR 1.0.3705)";
                request.Method = "POST";
                request.GetRequestStream();

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    TB_Recv.Text = reader.ReadToEnd();
                    WriteLog(reader.ReadToEnd());
                }

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
                    //Mode = 2;
                    page1 = 2;
                    //Step++;
                    //Step2(2);
                    break;
                case 1:
                    Step++;
                    page2 = 2;
                    Step3();
                    break;
                case 2:
                    Step++;
                    page3 = 2;
                    Step3();
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
            }
        }

        private void bt4_Click(object sender, EventArgs e)
        {
            switch (Step)
            {
                case 0:
                    Step2(4);
                    Step++;
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

        }

        private void bt6_Click(object sender, EventArgs e)
        {

        }

        private void bt7_Click(object sender, EventArgs e)
        {

        }

        private void bt8_Click(object sender, EventArgs e)
        {

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

    }
}
