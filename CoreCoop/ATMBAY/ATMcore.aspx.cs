using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLibrary;
using CoreCoopService;

namespace ATMBAY
{
    public partial class Default : System.Web.UI.Page
    {
        protected String Result = String.Empty;
        WebUtility WebUtil = new WebUtility();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                String DataMassage = Request["DataMassage"];
                //เชค Online ใน AppTest
                if (DataMassage == "TestWCF")
                {
                    Result = "1";
                    return;
                }
                else if (DataMassage == "test")
                {
                    LogMessage Log = new LogMessage();
                    Log.WriteLog("");
                    return;
                }
                //ตรวจสอบข้อมูล
                if (DataMassage.Length != 320)
                {
                    int looptmp = 0;
                    for (int i = DataMassage.Length; DataMassage.Length < 320; i++)
                    {
                        looptmp++;
                        DataMassage += " ";
                        if (looptmp > 320) break;
                    }
                }
                //จัดการข้อมูล
                DataEncode DataRequest = new DataEncode(DataMassage);
                DataEncode DataResponse = DataRequest;

                //ตรวจสอบ Request
                switch (DataRequest.TransactionMessageCode)
                {
                    case "0700": //Balance Inquiry
                        DataResponse.TransactionMessageCode = "0710";
                        Inquiry Inq = new Inquiry();
                        if (DataRequest.FromAccountCode == 14) //COOP Deposit Account [ถามยอดเงินฝาก]
                        {
                            Result = "MODE : Balance Inquiry >> COOP Deposit Account";
                            //Inq.DeptInquiry(DataResponse.COOPCustomerID.ToString("00000000"), ref DataResponse.Amount2, ref DataResponse.Amount3);
                        }
                        else if (DataRequest.FromAccountCode == 34) //COOP Loan Account [ถามยอดเงินกู้]
                        {
                            Result = "MODE : Balance Inquiry >> COOP Loan Account";
                            //Inq.LoanInquiry(DataResponse.COOPCustomerID.ToString("00000000"), ref DataResponse.Amount2, ref DataResponse.Amount3);
                        }
                        break;
                    case "0100": // Account Name Inquiry [ถามชื่อบัญชี]
                        DataResponse.TransactionMessageCode = "110";
                        if (DataRequest.FromAccountCode == 31) //Fund transfer from COOP A/C TO Bank A/C
                        {

                        }
                        break;
                    case "0200": // Fund Transfer //Money Withdraw
                        DataResponse.TransactionMessageCode = "210";
                        if (DataRequest.FromAccountCode == 42) //Fund transfer from COOP A/C TO Bank A/C [โอนจากสหกรณ์ไปธนาคาร]
                        {
                            Result = "MODE : Money Withdraw >> COOP Deposit Account";
                        }
                        else if (DataRequest.FromAccountCode == 43) //Fund transfer from Bank A/C TO COOP A/C [Coop Loan Payment] [โอนจากธนาคารไปสหกรณ์ ใช้ชำระหนี้]
                        {
                            Result = "MODE : Fund Transfer >> COOP Loan Payment";
                        }
                        else if (DataRequest.FromAccountCode == 10) //Cash Withdraw [ถอนเงินสด]
                        {
                            Result = "MODE : Balance Inquiry >> COOP Cash Withdraw";
                        }
                        break;
                    default: break;
                }
                //DataRequest.InsertATMACT();//บันทึกลงตาราง ATMACT เก็บ LOG การ Request
                //DataResponse.InsertATMACT();//บันทึกลงตาราง ATMACT เก็บ LOG การ Response
                //Result = DataResponse.DataMassage;

                //XmlService x = new XmlService();
                //String connectionString = x.GetConnectionString();
                //Result = connectionString;
                //Sta ta = new Sta(connectionString);
                //ta.Transection();
                //Sdt dt;
                //ta.Exe("");
                //XmlConfigSQL cfgSQL = new XmlConfigSQL();
                //Result = cfgSQL.DepositInquiry;
                //return;
                //String SQLINQ = WebUtil.SQLFormat(cfgSQL.DepositInquiry, "");
                //dt = ta.Query(SQLINQ);
                //if (dt.Next())
                //{
                //    Result = dt.GetString("PRNCBAL");
                //}
                //return;
                //Inquiry inq = new Inquiry();
                //Decimal test = 0;
                //Decimal test1 = 9;
                //inq.DeptInquiry("0144", "0123456789", ref test, ref test1);
                //Result = "[Complete] >> " + DataMassage;
                //inq.DeptInquiry("0144", "0123456789", ref test, ref test1);
                //return;
                //ta.Commit(true);

                //DataEndcode_.Amount2;
                //String adw = Data.DataMassage;
                //DataEncode A = new DataEncode(DataMassage);
                //Result = DataMassage + " : Return";

            }
            catch (Exception ex)
            {
                Result = "[SERVER COOP] " + ex.Message;
            }

        }
    }
}