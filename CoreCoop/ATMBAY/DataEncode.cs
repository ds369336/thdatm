using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLibrary;
using CoreCoopService;

namespace ATMBAY
{
    public class DataEncode
    {
        public String TransactionMessageCode = String.Empty;
        public String ServiceType = String.Empty;
        public UInt32 TransactionCode = 0;
        public UInt32 FromAccountCode = 0;
        public UInt32 ToAccountCode = 0;
        public DateTime TransactionDateTime = new DateTime();
        public UInt32 PANLength = 0;
        public String PANNumber = String.Empty;
        public String PINBlock = String.Empty;
        public String AcquirerTerminalNumber = String.Empty;
        public String AcquirerTerminalLocation = String.Empty;
        public String AcquirerTerminalOwner = String.Empty;
        public String AcquirerTerminalCity = String.Empty;
        public UInt32 TerminalSequenceNo = 0;
        public UInt32 AcquirerTraceNumber = 0;
        public String COOPFIID = String.Empty;
        public UInt32 COOPCustomerID = 0;
        public UInt32 COOPCustomerAC = 0;
        public UInt32 COOPBankAC = 0;
        public UInt32 COOPCustomerBankAC = 0;
        public String IssuerReference = String.Empty;
        public Decimal Amount1 = 0;
        public Decimal Amount2 = 0;
        public Decimal Amount3 = 0;
        public UInt32 ResponseCode = 0;
        public UInt32 ReversalCode = 0;
        public UInt32 ApproveCode = 0;
        public String ResponseMessage = String.Empty;

        public DataEncode()
        {

        }

        public DataEncode(String DataMessage)
        {
            try
            {
                this.TransactionMessageCode = DataMessage.Substring(0, 4);//1

                this.ServiceType = DataMessage.Substring(4, 4);//2.1

                this.TransactionCode = Convert.ToUInt32(DataMessage.Substring(8, 2));//3.1
                this.FromAccountCode = Convert.ToUInt32(DataMessage.Substring(10, 2));//3.2
                this.ToAccountCode = Convert.ToUInt32(DataMessage.Substring(12, 2));//3.3

                Int32 Year = Convert.ToInt32(DataMessage.Substring(14, 4));
                Int32 Mount = Convert.ToInt32(DataMessage.Substring(18, 2));
                Int32 Day = Convert.ToInt32(DataMessage.Substring(20, 2));
                Int32 HH = Convert.ToInt32(DataMessage.Substring(22, 2));
                Int32 MM = Convert.ToInt32(DataMessage.Substring(24, 2));
                Int32 SS = Convert.ToInt32(DataMessage.Substring(26, 2));
                this.TransactionDateTime = new DateTime(Year, Mount, Day, HH, MM, SS);//4.1-4.2

                this.PANLength = Convert.ToUInt32(DataMessage.Substring(28, 2));//5.1
                this.PANNumber = DataMessage.Substring(30, 19);//5.2

                this.PINBlock = DataMessage.Substring(49, 32);//6.1

                this.AcquirerTerminalNumber = DataMessage.Substring(81, 16);//7.1
                this.AcquirerTerminalLocation = DataMessage.Substring(97, 30);//7.2
                this.AcquirerTerminalOwner = DataMessage.Substring(127, 3);//7.3
                this.AcquirerTerminalCity = DataMessage.Substring(130, 3);//7.4
                this.TerminalSequenceNo = Convert.ToUInt32(DataMessage.Substring(133, 8));//7.5
                this.AcquirerTraceNumber = Convert.ToUInt32(DataMessage.Substring(141, 6));//7.6

                this.COOPFIID = DataMessage.Substring(147, 10);//8.1
                this.COOPCustomerID = Convert.ToUInt32(DataMessage.Substring(157, 10));
                this.COOPCustomerAC = Convert.ToUInt32(DataMessage.Substring(167, 10));
                this.COOPBankAC = Convert.ToUInt32(DataMessage.Substring(177, 10));
                this.COOPCustomerBankAC = Convert.ToUInt32(DataMessage.Substring(187, 10));

                this.IssuerReference = DataMessage.Substring(197, 50);//8.2

                this.Amount1 = Convert.ToDecimal(DataMessage.Substring(247, 15)) / 100;//9.1
                this.Amount2 = Convert.ToDecimal(DataMessage.Substring(262, 15)) / 100;//9.2
                this.Amount3 = Convert.ToDecimal(DataMessage.Substring(277, 15)) / 100;//9.3

                this.ResponseCode = Convert.ToUInt32(DataMessage.Substring(292, 2));//10.1
                this.ReversalCode = Convert.ToUInt32(DataMessage.Substring(294, 2));//10.2
                this.ApproveCode = Convert.ToUInt32(DataMessage.Substring(296, 6));//10.3

                this.ResponseMessage = DataMessage.Substring(302, 18);//10.4
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public String DataMassage
        {
            get
            {
                try
                {
                    return GetDataMassage();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

        private String GetDataMassage()
        {
            String Result = String.Empty;
            try
            {
                String Space = "                                                  ";
                Result += (this.TransactionMessageCode + Space).Substring(0, 4);//1

                Result += (this.ServiceType + Space).Substring(0, 4);//2.1

                Result += this.TransactionCode.ToString("00").Substring(0, 2);//3.1
                Result += this.FromAccountCode.ToString("00").Substring(0, 2);//3.2
                Result += this.ToAccountCode.ToString("00").Substring(0, 2);//3.3

                Result += this.TransactionDateTime.ToString("yyyyMMddHHmmss");//4.1-4.2

                Result += this.PANLength.ToString("00").Substring(0, 2);//5.1
                Result += (this.PANNumber + Space).Substring(0, 19);//5.2

                Result += (this.PINBlock + Space).Substring(0, 32);//6.1

                Result += (this.AcquirerTerminalNumber + Space).Substring(0, 16);//7.1
                Result += (this.AcquirerTerminalLocation + Space).Substring(0, 30);//7.2
                Result += (this.AcquirerTerminalOwner + Space).Substring(0, 3);//7.3
                Result += (this.AcquirerTerminalCity + Space).Substring(0, 3);//7.4
                Result += this.TerminalSequenceNo.ToString("00000000").Substring(0, 8);//7.5
                Result += this.AcquirerTraceNumber.ToString("000000").Substring(0, 6);//7.6

                Result += (this.COOPFIID + Space).Substring(0, 10);//8.1
                Result += this.COOPCustomerID.ToString("0000000000").Substring(0, 10);
                Result += this.COOPCustomerAC.ToString("0000000000").Substring(0, 10);
                Result += this.COOPBankAC.ToString("0000000000").Substring(0, 10);
                Result += this.COOPCustomerBankAC.ToString("0000000000").Substring(0, 10);

                Result += (this.IssuerReference + Space).Substring(0, 50);//8.2

                Result += (this.Amount1 * 100).ToString("000000000000000").Substring(0, 15);//9.1
                Result += (this.Amount2 * 100).ToString("000000000000000").Substring(0, 15);//9.2
                Result += (this.Amount3 * 100).ToString("000000000000000").Substring(0, 15);//9.3

                Result += this.ResponseCode.ToString("0000000000").Substring(0, 2);//10.1
                Result += this.ReversalCode.ToString("0000000000").Substring(0, 2);//10.2
                Result += this.ApproveCode.ToString("0000000000").Substring(0, 6);//10.3

                Result += (this.ResponseMessage + Space).Substring(0, 18);//10.4
                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertATMACT()
        {
            Sta ta = new Sta();
            try
            {
                WebUtility WebUtil = new WebUtility();
                Decimal ITEM_AMT = 0;
                Decimal FEE = 0;
                Decimal LEDGER_BAL = 0;
                Decimal AVAILABLE_BAL = 0;
                if (this.TransactionMessageCode == "0700" || this.TransactionMessageCode == "0710")
                {
                    ITEM_AMT = Amount1;
                    LEDGER_BAL = Amount2;
                    AVAILABLE_BAL = Amount3;
                }
                else
                {
                    ITEM_AMT = Amount1;
                    FEE = Amount2;
                    AVAILABLE_BAL = Amount3;
                }
                //XmlConfigSQL cfgSQL = new XmlConfigSQL();
                String SqlString = "INSERT INTO ATMACT (CCS_OPERATE_DATE, ATM_NO, ATM_SEQNO, TRANS_MESSAGE_CODE, SERVICE_TYPE, TRANS_CODE, FROM_ACC_CODE, TO_ACC_CODE, ATMCARD_LENGTH, ATMCARD_ID, PIN_BLOCK, ATM_LOCATION, ATM_OWNER, ATM_CITY, TRACE_NUMBER, COOP_FIID, MEMBER_ID, MEMBER_ACC, COOP_BANK_ACC, MEMBER_BANK_ACC, COOP_REF, ITEM_AMT, FEE, LEDGER_BAL, AVAILABLE_BAL, RESPONSE_CODE, REVERSAL_CODE, APPROVE_CODE, RESPONSE_MSG) VALUES ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15}, {16}, {17}, {18}, {19}, {20}, {21}, {22}, {23}, {24}, {25}, {26}, {27}, {28})";
                String SqlInsert = WebUtil.SQLFormat(SqlString, this.TransactionDateTime, this.AcquirerTerminalNumber, this.TerminalSequenceNo, this.TransactionMessageCode, this.ServiceType, this.TransactionCode, this.FromAccountCode, this.ToAccountCode, this.PANLength, this.PANNumber, this.PINBlock, this.AcquirerTerminalLocation, this.AcquirerTerminalOwner, this.AcquirerTerminalCity, this.AcquirerTraceNumber, this.COOPFIID, this.COOPCustomerID, this.COOPCustomerAC, this.COOPBankAC, this.COOPCustomerBankAC, this.IssuerReference, ITEM_AMT, FEE, LEDGER_BAL, AVAILABLE_BAL, this.ResponseCode.ToString("00"), this.ReversalCode.ToString("00"), this.ApproveCode.ToString("000000"), this.ResponseMessage);

                ta.Transection();
                ta.Exe(SqlInsert);
                ta.Commit();
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
