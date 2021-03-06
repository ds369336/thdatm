--=================== DataBase 01/09/2014 ==================
DROP TABLE "ATMACT";
CREATE TABLE "ATMACT" ("CCS_OPERATE_DATE" DATE NOT NULL, "SERVICE_TYPE" VARCHAR2(4), "TRANS_MESSAGE_CODE" VARCHAR2(4) NOT NULL, "ATMCARD_ID" VARCHAR2(19), "FROM_ACC_CODE" NUMBER(2), "TO_ACC_CODE" NUMBER(2), "ATM_NO" VARCHAR2(16) NULL, "ATM_SEQNO" VARCHAR2(8) NULL, "TRANS_CODE" NUMBER(2), "ATMCARD_LENGTH" NUMBER(2), "PIN_BLOCK" VARCHAR2(32), "ATM_LOCATION" VARCHAR2(30), "ATM_OWNER" VARCHAR2(3), "ATM_CITY" VARCHAR2(3), "TRACE_NUMBER" NUMBER(6), COOP_FIID VARCHAR2(10), MEMBER_ID NUMBER(10), MEMBER_ACC NUMBER(10), COOP_BANK_ACC NUMBER(10), MEMBER_BANK_ACC NUMBER(10), COOP_REF VARCHAR(50), ITEM_AMT NUMBER(15,2) DEFAULT 0, FEE NUMBER(15,2) DEFAULT 0, LEDGER_BAL NUMBER(15,2) DEFAULT 0, AVAILABLE_BAL NUMBER(15,2) DEFAULT 0, RESPONSE_CODE CHAR(2), REVERSAL_CODE CHAR(2), APPROVE_CODE VARCHAR2(6), RESPONSE_MSG VARCHAR2(18), "OPERATE_DATE" DATE DEFAULT SYSDATE );
ALTER TABLE "ATMACT" ADD ( CONSTRAINT "PK_ATMACT" PRIMARY KEY ( "CCS_OPERATE_DATE", "SERVICE_TYPE", "TRANS_MESSAGE_CODE", "ATMCARD_ID", "FROM_ACC_CODE")) ;

DROP TABLE "ATMUCFSERVICETYPE";
CREATE TABLE "ATMUCFSERVICETYPE" ("SERVICE_TYPE" VARCHAR2(4), "RESPONSE_FLAG" NUMBER(1,0) DEFAULT 0);
ALTER TABLE "ATMUCFSERVICETYPE" ADD ( CONSTRAINT "PK_ATMUCFSERVICETYPE" PRIMARY KEY ( "SERVICE_TYPE")) ;

DROP TABLE "ATMTRANSACTION";
CREATE TABLE "ATMTRANSACTION" ("COOP_ID" VARCHAR2(10) NOT NULL, "MEMBER_NO" CHAR(8) NOT NULL, "ACCOUNT_NO" CHAR(10) NOT NULL, "CCS_OPERATE_DATE" DATE NOT NULL, "OPERATE_DATE" DATE DEFAULT SYSDATE, "SYSTEM_CODE" CHAR(2) NOT NULL, "OPERATE_CODE" CHAR(3) NOT NULL, "CASH_TYPE" CHAR(3) NOT NULL, "ITEM_AMT" NUMBER(15,2) NOT NULL, "ATM_NO" VARCHAR2(16) NOT NULL, "ATM_SEQNO" VARCHAR2(8), "ITEM_STATUS" NUMBER(1,0) DEFAULT 0 NOT NULL, "POST_STATUS" NUMBER(1,0) DEFAULT 0 NOT NULL, "POST_DATE" DATE, "SAVING_ACC" CHAR(15) NOT NULL, "RECONCILE_DATE" DATE, "STATEMENT_SEQNO" NUMBER(9,0)) ;
ALTER TABLE "ATMTRANSACTION" ADD ( CONSTRAINT "PK_ATMTRANSACTION" PRIMARY KEY ( "COOP_ID", "MEMBER_NO", "CCS_OPERATE_DATE" )) ;

--SYSTEM_CODE [01:LOAN, 02:DEPT]
--OPERATE_CODE [002:WITHDRAW 003:DEPOSIT]
--STATEMENT_SEQNO ใช้สำหรับจับคู่ตาราง STATEMENT (ADD BY WA)

--=================== DataBase 12/06/2015 ==================
DROP TABLE "ATMTRANSACTIONCANCEL";
CREATE TABLE "ATMTRANSACTIONCANCEL" ("COOP_ID" VARCHAR2(10) NOT NULL, "MEMBER_NO" CHAR(8) NOT NULL, "ACCOUNT_NO" CHAR(10) NOT NULL, "CCS_OPERATE_DATE" DATE NOT NULL, "OPERATE_DATE" DATE DEFAULT SYSDATE, "SYSTEM_CODE" CHAR(2) NOT NULL, "OPERATE_CODE" CHAR(3) NOT NULL, "CASH_TYPE" CHAR(3) NOT NULL, "ITEM_AMT" NUMBER(15,2) NOT NULL, "ATM_NO" VARCHAR2(16) NOT NULL, "ATM_SEQNO" VARCHAR2(8), "SAVING_ACC" CHAR(15)) ;
ALTER TABLE "ATMTRANSACTIONCANCEL" ADD ( CONSTRAINT "PK_ATMTRANSACTIONCANCEL" PRIMARY KEY ( "COOP_ID", "MEMBER_NO", "CCS_OPERATE_DATE" )) ;

--=================== DataBase 22/02/2015 ==================
DROP TABLE "ATMCOOP";
CREATE TABLE "ATMCOOP" ("COOP_ID" VARCHAR2(10) NOT NULL, "DEPT_HOLD" NUMBER(1,0) DEFAULT 0, "LOAN_HOLD" NUMBER(1,0) DEFAULT 0, "DEPT_FEE" NUMBER(1,0) DEFAULT 0, "LOAN_FEE" NUMBER(1,0) DEFAULT 0, "DEPTSEQUEST_AMT" NUMBER(15,2) DEFAULT 0, "LOANSEQUEST_AMT" NUMBER(15,2) DEFAULT 0) ;
ALTER TABLE "ATMCOOP" ADD ( CONSTRAINT "PK_ATMCOOP" PRIMARY KEY ( "COOP_ID"));

INSERT INTO "ATMCOOP" ( "COOP_ID", "DEPT_HOLD", "LOAN_HOLD", "DEPT_FEE", "LOAN_FEE", "DEPTSEQUEST_AMT", "LOANSEQUEST_AMT" ) VALUES ( '007097', 0, 0, 0, 0, 100, 0 );

--=================== DataBase 09/03/2015 ==================
--ATMDEPT ยอดถอนที่ยังไม่ลงบันทึก ยอดฝากที่ยังไม่ลงบันทึก อายัดยอด อายัดบัญชี 
DROP TABLE "ATMDEPT";
CREATE TABLE "ATMDEPT" ("COOP_ID" VARCHAR2(10) NOT NULL, "MEMBER_NO" CHAR(8) NOT NULL, "DEPTACCOUNT_NO" CHAR(10) NOT NULL, "RECEIVE_AMT" NUMBER(15,2) DEFAULT 0, "PAY_AMT" NUMBER(15,2) DEFAULT 0, "SEQUEST_AMT" NUMBER(15,2) DEFAULT 0, "ACCOUNT_HOLD" NUMBER(1,0) DEFAULT 0, "ACCOUNT_STATUS" NUMBER(1,0) DEFAULT 0, "OPERATE_DATE" DATE DEFAULT SYSDATE);
ALTER TABLE "ATMDEPT" ADD ( CONSTRAINT "PK_ATMDEPT" PRIMARY KEY ( "COOP_ID", "MEMBER_NO", "DEPTACCOUNT_NO" )) ;
--ACCOUNT_STATUS[1:USE, -9:CANCEL]

--ATMLOAN ยอดกู้ที่ยังไม่ลงบันทึก ยอดชำระหนี้ที่ยังไม่ลงบันทึก อายัดยอด อายัดบัญชี
DROP TABLE "ATMLOAN";
CREATE TABLE "ATMLOAN" ("COOP_ID" VARCHAR2(10) NOT NULL, "MEMBER_NO" CHAR(8) NOT NULL, "LOANCONTRACT_NO" CHAR(10) NOT NULL, "RECEIVE_AMT" NUMBER(15,2) DEFAULT 0, "PAY_AMT" NUMBER(15,2) DEFAULT 0, "SEQUEST_AMT" NUMBER(15,2) DEFAULT 0, "ACCOUNT_HOLD" NUMBER(1,0) DEFAULT 0, "ACCOUNT_STATUS" NUMBER(1,0) DEFAULT 0, "OPERATE_DATE" DATE DEFAULT SYSDATE);
ALTER TABLE "ATMLOAN" ADD ( CONSTRAINT "PK_ATMLOAN" PRIMARY KEY ( "COOP_ID", "MEMBER_NO", "LOANCONTRACT_NO" )) ;
--ACCOUNT_STATUS[1:USE, -9:CANCEL]

--=================== DataBase 08/05/2015 ==================
--ATMBANKPROCESS เก็บสถานะล่าสุดที่ GEN ไฟล์ส่งเข้า BANK จากตาราง ATMBANKPROCESSDET
DROP TABLE "ATMBANKPROCESS";
CREATE TABLE "ATMBANKPROCESS" ("COOP_ID" VARCHAR2(10) NOT NULL, "MEMBER_NO" CHAR(8) NOT NULL, "INFO_FLAG" NUMBER(1,0) DEFAULT 0, "LNRECV_FLAG" NUMBER(1,0) DEFAULT 0, "LNPAY_FLAG" NUMBER(1,0) DEFAULT 0, "DPWITH_FLAG" NUMBER(1,0) DEFAULT 0, "DPDEPT_FLAG" NUMBER(1,0) DEFAULT 0, "CLEAR_FLAG" NUMBER(1,0) DEFAULT 0);
ALTER TABLE "ATMBANKPROCESS" ADD ( CONSTRAINT "PK_ATMBANKPROCESS" PRIMARY KEY ( "COOP_ID", "MEMBER_NO" )) ;
--FLAG [0:NO_PROCESS,1:ADD_OR_CHANGE, -9:DELETE]
--CLEAR_FLAG [0:NO_PROCESS,1:CLEAR_ALL]

DROP TABLE "ATMBANKPROCESSDET";
CREATE TABLE "ATMBANKPROCESSDET" ("COOP_ID" VARCHAR2(10) NOT NULL, "MEMBER_NO" CHAR(8) NOT NULL, "INSERT_DATE" DATE DEFAULT SYSDATE, "DATA_TYPE" NUMBER(1,0) DEFAULT 1, "FUNCTION_TYPE" CHAR(1) DEFAULT 'C', "PROCESS_DATE" DATE);
ALTER TABLE "ATMBANKPROCESSDET" ADD ( CONSTRAINT "PK_ATMBANKWAITPROCRESS" PRIMARY KEY ( "COOP_ID", "MEMBER_NO", "INSERT_DATE" ));
--DATA_TYPE [0:DELETE_ALL, 1:INFORMATION, 2:LOAN_RECEIVE, 3:DEPOSIT_WITHDRAW, 4:LOAN_PAYMENT, 5:DEPOSIT_DEPOSIT]
--FUNCTION_TYPE [A:ADD, C:CHANGE, D:DELETE]

--=================== DataBase 15/06/2015 ==================
DROP TABLE "ATMMEMBER";
CREATE TABLE "ATMMEMBER" ("COOP_ID" VARCHAR2(10) NOT NULL, "MEMBER_NO" CHAR(8) NOT NULL,"SAVING_ACC" VARCHAR(15), "INFO_FLAG" NUMBER(1,0) DEFAULT 0, "LNRECV_FLAG" NUMBER(1,0) DEFAULT 0, "LNPAY_FLAG" NUMBER(1,0) DEFAULT 0, "DPWITH_FLAG" NUMBER(1,0) DEFAULT 0, "DPDEPT_FLAG" NUMBER(1,0) DEFAULT 0);
ALTER TABLE "ATMMEMBER" ADD ( CONSTRAINT "PK_ATMMEMBER" PRIMARY KEY ( "COOP_ID", "MEMBER_NO" )) ;
--FLAG [0:NOT ACCESS,1:ACCESS]

--=================== DataBase 27/06/2015 ==================
DROP TABLE "ATMBANKTRANS";
CREATE TABLE "ATMBANKTRANS" ("COOP_ID" VARCHAR2(10) NOT NULL, "MEMBER_NO" VARCHAR2(13) NOT NULL, "TRANS_DATE" DATE NOT NULL, "RECORD_TYPE" NUMBER (1,0), "ATMCARD_OWNER" VARCHAR2(3), "ATMCARD_ID" VARCHAR2(19), "TERM_OWNER" VARCHAR2(3), "TERM_NO" VARCHAR2(7), "TERM_LOCATION" VARCHAR2(15), "TERM_CITY" VARCHAR2(2), "TERM_STATE" VARCHAR2(2), "TERM_STSEQ" VARCHAR2(6), "TRANS_CODE" VARCHAR2(6), "COOP_BANK_ACC" VARCHAR2(10), "MEMBER_BANK_ACC" VARCHAR2(10), "ITEM_AMT" NUMBER(15,2), "DISP_AMT" NUMBER(15,2), "FEE" NUMBER(15,2), "RESPONSE_BY" VARCHAR2(1), "RESPONSE_CODE01" VARCHAR2(1), "RESPONSE_CODE02" VARCHAR2(2), "REVERSAL_CODE" VARCHAR2(2), "APPROVE_CODE" VARCHAR2(6), "FILE_NAME" VARCHAR2(50));
ALTER TABLE "ATMBANKTRANS" ADD ( CONSTRAINT "PK_ATMBANKTRANS" PRIMARY KEY ( "COOP_ID", "MEMBER_NO", "TRANS_DATE" )) ;
