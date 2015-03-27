--=================== DataBase 01/09/2014 ==================
DROP TABLE ATMACT;
CREATE TABLE "ATMACT" ("CCS_OPERATE_DATE" DATE NOT NULL, "ATM_NO" VARCHAR(16) NOT NULL, "ATM_SEQNO" CHAR(8) NOT NULL, "TRANS_MESSAGE_CODE" VARCHAR2(4) NOT NULL, "SERVICE_TYPE" VARCHAR2(4), "TRANS_CODE" NUMBER(2), "FROM_ACC_CODE" NUMBER(2), "TO_ACC_CODE" NUMBER(2), "ATMCARD_LENGTH" NUMBER(2), "ATMCARD_ID" VARCHAR2(19), "PIN_BLOCK" VARCHAR2(32), "ATM_LOCATION" VARCHAR2(30), "ATM_OWNER" VARCHAR2(3), "ATM_CITY" VARCHAR2(3), "TRACE_NUMBER" NUMBER(6), COOP_FIID VARCHAR2(10), MEMBER_ID NUMBER(10), MEMBER_ACC NUMBER(10), COOP_BANK_ACC NUMBER(10), MEMBER_BANK_ACC NUMBER(10), COOP_REF VARCHAR(50), ITEM_AMT NUMBER(15,2) DEFAULT 0, FEE NUMBER(15,2) DEFAULT 0, LEDGER_BAL NUMBER(15,2) DEFAULT 0, AVAILABLE_BAL NUMBER(15,2) DEFAULT 0, RESPONSE_CODE CHAR(2), REVERSAL_CODE CHAR(2), APPROVE_CODE VARCHAR2(6), RESPONSE_MSG VARCHAR2(18), "OPERATE_DATE" DATE DEFAULT SYSDATE );
ALTER TABLE "ATMACT" ADD ( CONSTRAINT "PK_ATMACT" PRIMARY KEY ( "CCS_OPERATE_DATE", "ATM_NO", "ATM_SEQNO","TRANS_MESSAGE_CODE", "OPERATE_DATE", "TRANS_MESSAGE_CODE" )) ;

DROP TABLE "ATMTRANSACTION";
CREATE TABLE "ATMTRANSACTION" ("MEMBER_NO" CHAR(8) NOT NULL, "COOP_ID" VARCHAR2(10) NOT NULL, "CCS_OPERATE_DATE" DATE NOT NULL, "OPERATE_DATE" DATE DEFAULT SYSDATE, "SYSTEM_CODE" CHAR(2) NOT NULL, "OPERATE_CODE" CHAR(3) NOT NULL, "ITEM_AMT" NUMBER(15,2) NOT NULL, "ATM_NO" VARCHAR2(12) NOT NULL, "ATM_SEQNO" VARCHAR2(8) NOT NULL, "ITEM_STATUS" NUMBER(1,0) DEFAULT 0 NOT NULL, "POST_STATUS" NUMBER(1,0) DEFAULT 0 NOT NULL, "POST_DATE" DATE, "SAVING_ACC" CHAR(15) NOT NULL, "RECONCILE_DATE" DATE) ;
ALTER TABLE "ATMTRANSACTION" ADD ( CONSTRAINT "PK_ATMTRANSACTION" PRIMARY KEY ( "MEMBER_NO", "COOP_ID", "CCS_OPERATE_DATE" )) ;

--SYSTEM_CODE [01:LOAN, 02:DEPT]
--OPERATE_CODE [002:WITHDRAW 003:DEPOSIT]

--ไม่ต้องใช้ ATMCATD_ID เพราะมีผูกไว้ทั้งหมดอยู่แล้ว
--ALTER TABLE DPDEPTMASTER ADD ATMCARD_ID VARCHAR2(15);

--Flag เงินฝากที่ผู้กับ ATM
--ALTER TABLE DPDEPTMASTER ADD ATMFLAG NUMBER(1) DEFAULT 0;



--=================== DataBase 22/02/2015 ==================
DROP TABLE "ATMCOOP";
CREATE TABLE "ATMCOOP" ("COOP_ID" VARCHAR2(10) NOT NULL, "DEPT_HOLD" NUMBER(1,0) DEFAULT 0, "LOAN_HOLD" NUMBER(1,0) DEFAULT 0, "DEPT_FEE" NUMBER(1,0) DEFAULT 0, "LOAN_FEE" NUMBER(1,0) DEFAULT 0, "DEPTSEQUEST_AMT" NUMBER(15,2) DEFAULT 0, "LOANSEQUEST_AMT" NUMBER(15,2) DEFAULT 0) ;
ALTER TABLE "ATMCOOP" ADD ( CONSTRAINT "PK_ATMCOOP" PRIMARY KEY ( "COOP_ID"));

INSERT INTO "ATMCOOP" ( "COOP_ID", "DEPT_HOLD", "LOAN_HOLD", "DEPT_FEE", "LOAN_FEE", "DEPTSEQUEST_AMT", "LOANSEQUEST_AMT" ) VALUES ( '007097', 0, 0, 0, 0, 100, 0 );

--=================== DataBase 09/03/2015 ==================
--ATMDEPT ยอดถอนที่ยังไม่ลงบันทึก ยอดฝากที่ยังไม่ลงบันทึก อายัดยอด อายัดบัญชี 
DROP TABLE "ATMDEPT";
CREATE TABLE "ATMDEPT" ("MEMBER_NO" CHAR(8) NOT NULL, "COOP_ID" VARCHAR2(10) NOT NULL, "DEPTACCOUNT_NO" CHAR(10) NOT NULL, "RECEIVE_AMT" NUMBER(15,2) DEFAULT 0 NOT NULL, "PAY_AMT" NUMBER(15,2) DEFAULT 0 NOT NULL, "SEQUEST_AMT" NUMBER(15,2) DEFAULT 0, "ACCOUNT_HOLD" NUMBER(1,0) DEFAULT 0 NOT NULL);
ALTER TABLE "ATMDEPT" ADD ( CONSTRAINT PK_ATMDEPT PRIMARY KEY ( "MEMBER_NO", "COOP_ID" )) ;

--ATMLOAN ยอดกู้ที่ยังไม่ลงบันทึก ยอดชำระหนี้ที่ยังไม่ลงบันทึก อายัดยอด อายัดบัญชี
DROP TABLE "ATMLOAN";
CREATE TABLE "ATMLOAN" ("MEMBER_NO" CHAR(8) NOT NULL, "COOP_ID" VARCHAR2(10) NOT NULL, "LOANCONTRACT_NO" CHAR(10) NOT NULL, "RECEIVE_AMT" NUMBER(15,2) DEFAULT 0 NOT NULL, "PAY_AMT" NUMBER(15,2) DEFAULT 0 NOT NULL, "SEQUEST_AMT" NUMBER(15,2) DEFAULT 0, "ACCOUNT_HOLD" NUMBER(1,0) DEFAULT 0 NOT NULL);
ALTER TABLE "ATMLOAN" ADD ( CONSTRAINT PK_ATMLOAN PRIMARY KEY ( "MEMBER_NO", "COOP_ID" )) ;

