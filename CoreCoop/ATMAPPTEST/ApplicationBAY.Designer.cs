﻿namespace ATMAPPTEST
{
    partial class ApplicationBAY
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.ITEM_AMT = new System.Windows.Forms.NumericUpDown();
            this.TBM_02 = new System.Windows.Forms.TextBox();
            this.TBM_01 = new System.Windows.Forms.TextBox();
            this.bt8 = new System.Windows.Forms.Button();
            this.bt7 = new System.Windows.Forms.Button();
            this.bt6 = new System.Windows.Forms.Button();
            this.bt5 = new System.Windows.Forms.Button();
            this.bt4 = new System.Windows.Forms.Button();
            this.bt3 = new System.Windows.Forms.Button();
            this.bt2 = new System.Windows.Forms.Button();
            this.bt1 = new System.Windows.Forms.Button();
            this.submit = new System.Windows.Forms.Button();
            this.TB_Exception = new System.Windows.Forms.RichTextBox();
            this.TB_LOG = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.LB_Status = new System.Windows.Forms.Label();
            this.TB_ATMCARD_ID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.TB_ATM_NO = new System.Windows.Forms.TextBox();
            this.BT_CLEAR_LOG = new System.Windows.Forms.Button();
            this.DT_OPERATE_DATE = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.TB_TRACE_NUMBER = new System.Windows.Forms.TextBox();
            this.TB_COREURL = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.BT_CHECK_WCF = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.TB_MEMBER_NO = new System.Windows.Forms.TextBox();
            this.TB_Send = new System.Windows.Forms.TextBox();
            this.BT_SYSTEM_CHECK = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.TB_CERT = new System.Windows.Forms.TextBox();
            this.PostGenFile = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ITEM_AMT)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.ITEM_AMT);
            this.panel1.Controls.Add(this.TBM_02);
            this.panel1.Controls.Add(this.TBM_01);
            this.panel1.Controls.Add(this.bt8);
            this.panel1.Controls.Add(this.bt7);
            this.panel1.Controls.Add(this.bt6);
            this.panel1.Controls.Add(this.bt5);
            this.panel1.Controls.Add(this.bt4);
            this.panel1.Controls.Add(this.bt3);
            this.panel1.Controls.Add(this.bt2);
            this.panel1.Controls.Add(this.bt1);
            this.panel1.Location = new System.Drawing.Point(538, 180);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(390, 281);
            this.panel1.TabIndex = 0;
            // 
            // ITEM_AMT
            // 
            this.ITEM_AMT.Location = new System.Drawing.Point(137, -1);
            this.ITEM_AMT.Name = "ITEM_AMT";
            this.ITEM_AMT.Size = new System.Drawing.Size(120, 20);
            this.ITEM_AMT.TabIndex = 24;
            this.ITEM_AMT.Visible = false;
            this.ITEM_AMT.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // TBM_02
            // 
            this.TBM_02.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TBM_02.Cursor = System.Windows.Forms.Cursors.Default;
            this.TBM_02.Location = new System.Drawing.Point(2, 129);
            this.TBM_02.Name = "TBM_02";
            this.TBM_02.ReadOnly = true;
            this.TBM_02.Size = new System.Drawing.Size(382, 13);
            this.TBM_02.TabIndex = 10;
            this.TBM_02.Text = "Text2";
            this.TBM_02.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TBM_01
            // 
            this.TBM_01.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TBM_01.Cursor = System.Windows.Forms.Cursors.Default;
            this.TBM_01.Location = new System.Drawing.Point(1, 59);
            this.TBM_01.Name = "TBM_01";
            this.TBM_01.ReadOnly = true;
            this.TBM_01.Size = new System.Drawing.Size(382, 13);
            this.TBM_01.TabIndex = 9;
            this.TBM_01.Text = "Text1";
            this.TBM_01.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // bt8
            // 
            this.bt8.Location = new System.Drawing.Point(238, 227);
            this.bt8.Name = "bt8";
            this.bt8.Size = new System.Drawing.Size(146, 36);
            this.bt8.TabIndex = 8;
            this.bt8.Text = "8";
            this.bt8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.bt8.UseVisualStyleBackColor = true;
            this.bt8.Click += new System.EventHandler(this.bt8_Click);
            // 
            // bt7
            // 
            this.bt7.Location = new System.Drawing.Point(238, 159);
            this.bt7.Name = "bt7";
            this.bt7.Size = new System.Drawing.Size(146, 36);
            this.bt7.TabIndex = 7;
            this.bt7.Text = "7";
            this.bt7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.bt7.UseVisualStyleBackColor = true;
            this.bt7.Click += new System.EventHandler(this.bt7_Click);
            // 
            // bt6
            // 
            this.bt6.Location = new System.Drawing.Point(238, 87);
            this.bt6.Name = "bt6";
            this.bt6.Size = new System.Drawing.Size(146, 36);
            this.bt6.TabIndex = 6;
            this.bt6.Text = "6";
            this.bt6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.bt6.UseVisualStyleBackColor = true;
            this.bt6.Click += new System.EventHandler(this.bt6_Click);
            // 
            // bt5
            // 
            this.bt5.Location = new System.Drawing.Point(238, 17);
            this.bt5.Name = "bt5";
            this.bt5.Size = new System.Drawing.Size(146, 36);
            this.bt5.TabIndex = 5;
            this.bt5.Text = "5";
            this.bt5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.bt5.UseVisualStyleBackColor = true;
            this.bt5.Click += new System.EventHandler(this.bt5_Click);
            // 
            // bt4
            // 
            this.bt4.Location = new System.Drawing.Point(3, 227);
            this.bt4.Name = "bt4";
            this.bt4.Size = new System.Drawing.Size(149, 36);
            this.bt4.TabIndex = 4;
            this.bt4.Text = "4";
            this.bt4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt4.UseVisualStyleBackColor = true;
            this.bt4.Click += new System.EventHandler(this.bt4_Click);
            // 
            // bt3
            // 
            this.bt3.Location = new System.Drawing.Point(3, 159);
            this.bt3.Name = "bt3";
            this.bt3.Size = new System.Drawing.Size(149, 36);
            this.bt3.TabIndex = 3;
            this.bt3.Text = "3";
            this.bt3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt3.UseVisualStyleBackColor = true;
            this.bt3.Click += new System.EventHandler(this.bt3_Click);
            // 
            // bt2
            // 
            this.bt2.Location = new System.Drawing.Point(3, 87);
            this.bt2.Name = "bt2";
            this.bt2.Size = new System.Drawing.Size(149, 36);
            this.bt2.TabIndex = 2;
            this.bt2.Text = "2";
            this.bt2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt2.UseVisualStyleBackColor = true;
            this.bt2.Click += new System.EventHandler(this.bt2_Click);
            // 
            // bt1
            // 
            this.bt1.Location = new System.Drawing.Point(3, 17);
            this.bt1.Name = "bt1";
            this.bt1.Size = new System.Drawing.Size(149, 36);
            this.bt1.TabIndex = 1;
            this.bt1.Text = "1";
            this.bt1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt1.UseVisualStyleBackColor = true;
            this.bt1.Click += new System.EventHandler(this.bt1_Click);
            // 
            // submit
            // 
            this.submit.Location = new System.Drawing.Point(447, 111);
            this.submit.Name = "submit";
            this.submit.Size = new System.Drawing.Size(69, 20);
            this.submit.TabIndex = 1;
            this.submit.Text = "Send";
            this.submit.UseVisualStyleBackColor = true;
            this.submit.Click += new System.EventHandler(this.submit_Click);
            // 
            // TB_Exception
            // 
            this.TB_Exception.Location = new System.Drawing.Point(12, 137);
            this.TB_Exception.Name = "TB_Exception";
            this.TB_Exception.ReadOnly = true;
            this.TB_Exception.Size = new System.Drawing.Size(504, 35);
            this.TB_Exception.TabIndex = 5;
            this.TB_Exception.Text = "";
            // 
            // TB_LOG
            // 
            this.TB_LOG.Location = new System.Drawing.Point(12, 180);
            this.TB_LOG.Name = "TB_LOG";
            this.TB_LOG.ReadOnly = true;
            this.TB_LOG.Size = new System.Drawing.Size(504, 281);
            this.TB_LOG.TabIndex = 6;
            this.TB_LOG.Text = "";
            this.TB_LOG.WordWrap = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(535, 159);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "CoreATM Status :";
            // 
            // LB_Status
            // 
            this.LB_Status.AutoSize = true;
            this.LB_Status.Location = new System.Drawing.Point(623, 159);
            this.LB_Status.Name = "LB_Status";
            this.LB_Status.Size = new System.Drawing.Size(37, 13);
            this.LB_Status.TabIndex = 8;
            this.LB_Status.Text = "Offline";
            // 
            // TB_ATMCARD_ID
            // 
            this.TB_ATMCARD_ID.Location = new System.Drawing.Point(610, 6);
            this.TB_ATMCARD_ID.Name = "TB_ATMCARD_ID";
            this.TB_ATMCARD_ID.Size = new System.Drawing.Size(105, 20);
            this.TB_ATMCARD_ID.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(535, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "เลขที่บัญชี :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(535, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "วันทำรายการ :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(535, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "เลขตู้ ATM :";
            // 
            // TB_ATM_NO
            // 
            this.TB_ATM_NO.Location = new System.Drawing.Point(610, 58);
            this.TB_ATM_NO.Name = "TB_ATM_NO";
            this.TB_ATM_NO.Size = new System.Drawing.Size(105, 20);
            this.TB_ATM_NO.TabIndex = 13;
            // 
            // BT_CLEAR_LOG
            // 
            this.BT_CLEAR_LOG.Location = new System.Drawing.Point(15, 469);
            this.BT_CLEAR_LOG.Name = "BT_CLEAR_LOG";
            this.BT_CLEAR_LOG.Size = new System.Drawing.Size(75, 23);
            this.BT_CLEAR_LOG.TabIndex = 15;
            this.BT_CLEAR_LOG.Text = "Clear Log";
            this.BT_CLEAR_LOG.UseVisualStyleBackColor = true;
            this.BT_CLEAR_LOG.Click += new System.EventHandler(this.BT_CLEAR_LOG_Click);
            // 
            // DT_OPERATE_DATE
            // 
            this.DT_OPERATE_DATE.Location = new System.Drawing.Point(610, 32);
            this.DT_OPERATE_DATE.Name = "DT_OPERATE_DATE";
            this.DT_OPERATE_DATE.Size = new System.Drawing.Size(105, 20);
            this.DT_OPERATE_DATE.TabIndex = 16;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(535, 87);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "Trace Num :";
            // 
            // TB_TRACE_NUMBER
            // 
            this.TB_TRACE_NUMBER.Location = new System.Drawing.Point(610, 84);
            this.TB_TRACE_NUMBER.Name = "TB_TRACE_NUMBER";
            this.TB_TRACE_NUMBER.Size = new System.Drawing.Size(105, 20);
            this.TB_TRACE_NUMBER.TabIndex = 17;
            // 
            // TB_COREURL
            // 
            this.TB_COREURL.Location = new System.Drawing.Point(103, 12);
            this.TB_COREURL.Name = "TB_COREURL";
            this.TB_COREURL.Size = new System.Drawing.Size(338, 20);
            this.TB_COREURL.TabIndex = 19;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 13);
            this.label6.TabIndex = 20;
            this.label6.Text = "CoreATM URL :";
            // 
            // BT_CHECK_WCF
            // 
            this.BT_CHECK_WCF.Location = new System.Drawing.Point(447, 12);
            this.BT_CHECK_WCF.Name = "BT_CHECK_WCF";
            this.BT_CHECK_WCF.Size = new System.Drawing.Size(69, 23);
            this.BT_CHECK_WCF.TabIndex = 21;
            this.BT_CHECK_WCF.Text = "Submit";
            this.BT_CHECK_WCF.UseVisualStyleBackColor = true;
            this.BT_CHECK_WCF.Click += new System.EventHandler(this.BT_CHECK_WCF_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(743, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(69, 13);
            this.label7.TabIndex = 23;
            this.label7.Text = "เลขที่สมาชิก :";
            // 
            // TB_MEMBER_NO
            // 
            this.TB_MEMBER_NO.Location = new System.Drawing.Point(818, 6);
            this.TB_MEMBER_NO.Name = "TB_MEMBER_NO";
            this.TB_MEMBER_NO.Size = new System.Drawing.Size(105, 20);
            this.TB_MEMBER_NO.TabIndex = 22;
            // 
            // TB_Send
            // 
            this.TB_Send.Location = new System.Drawing.Point(12, 111);
            this.TB_Send.Name = "TB_Send";
            this.TB_Send.Size = new System.Drawing.Size(429, 20);
            this.TB_Send.TabIndex = 24;
            // 
            // BT_SYSTEM_CHECK
            // 
            this.BT_SYSTEM_CHECK.Location = new System.Drawing.Point(12, 82);
            this.BT_SYSTEM_CHECK.Name = "BT_SYSTEM_CHECK";
            this.BT_SYSTEM_CHECK.Size = new System.Drawing.Size(125, 23);
            this.BT_SYSTEM_CHECK.TabIndex = 25;
            this.BT_SYSTEM_CHECK.Text = "Check System ATM";
            this.BT_SYSTEM_CHECK.UseVisualStyleBackColor = true;
            this.BT_SYSTEM_CHECK.Click += new System.EventHandler(this.BT_SYSTEM_CHECK_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 39);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(90, 13);
            this.label8.TabIndex = 26;
            this.label8.Text = "Certificates Path :";
            // 
            // TB_CERT
            // 
            this.TB_CERT.Location = new System.Drawing.Point(103, 39);
            this.TB_CERT.Name = "TB_CERT";
            this.TB_CERT.Size = new System.Drawing.Size(338, 20);
            this.TB_CERT.TabIndex = 27;
            // 
            // PostGenFile
            // 
            this.PostGenFile.Location = new System.Drawing.Point(143, 82);
            this.PostGenFile.Name = "PostGenFile";
            this.PostGenFile.Size = new System.Drawing.Size(125, 23);
            this.PostGenFile.TabIndex = 28;
            this.PostGenFile.Text = "GenFile";
            this.PostGenFile.UseVisualStyleBackColor = true;
            this.PostGenFile.Click += new System.EventHandler(this.PostGenFile_Click);
            // 
            // ApplicationBAY
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(935, 499);
            this.Controls.Add(this.PostGenFile);
            this.Controls.Add(this.TB_CERT);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.BT_SYSTEM_CHECK);
            this.Controls.Add(this.TB_Send);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.TB_MEMBER_NO);
            this.Controls.Add(this.BT_CHECK_WCF);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.TB_COREURL);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.TB_TRACE_NUMBER);
            this.Controls.Add(this.DT_OPERATE_DATE);
            this.Controls.Add(this.BT_CLEAR_LOG);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.TB_ATM_NO);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TB_ATMCARD_ID);
            this.Controls.Add(this.LB_Status);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TB_LOG);
            this.Controls.Add(this.TB_Exception);
            this.Controls.Add(this.submit);
            this.Controls.Add(this.panel1);
            this.Name = "ApplicationBAY";
            this.Text = "Application";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ITEM_AMT)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button submit;
        private System.Windows.Forms.RichTextBox TB_Exception;
        private System.Windows.Forms.Button bt8;
        private System.Windows.Forms.Button bt7;
        private System.Windows.Forms.Button bt6;
        private System.Windows.Forms.Button bt5;
        private System.Windows.Forms.Button bt4;
        private System.Windows.Forms.Button bt3;
        private System.Windows.Forms.Button bt2;
        private System.Windows.Forms.Button bt1;
        private System.Windows.Forms.RichTextBox TB_LOG;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label LB_Status;
        private System.Windows.Forms.TextBox TBM_01;
        private System.Windows.Forms.TextBox TB_ATMCARD_ID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TB_ATM_NO;
        private System.Windows.Forms.Button BT_CLEAR_LOG;
        private System.Windows.Forms.DateTimePicker DT_OPERATE_DATE;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox TB_TRACE_NUMBER;
        private System.Windows.Forms.TextBox TBM_02;
        private System.Windows.Forms.TextBox TB_COREURL;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button BT_CHECK_WCF;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox TB_MEMBER_NO;
        private System.Windows.Forms.NumericUpDown ITEM_AMT;
        private System.Windows.Forms.TextBox TB_Send;
        private System.Windows.Forms.Button BT_SYSTEM_CHECK;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox TB_CERT;
        private System.Windows.Forms.Button PostGenFile;
    }
}

