using System;
using System.Data.OleDb;
using System.Data;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Data.OracleClient;
using System.Collections.Generic;

namespace DataLibrary
{
    public class Sta
    {
        private String url;
        // ORACLE
        private IDbConnection con;
        private IDbCommand cmd;
        private IDbTransaction tran;
        private List<OracleParameter> param;

        private System.Data.OracleClient.OracleConnection conOra;
        private System.Data.OracleClient.OracleCommand cmdOra;

        private DbType dbType;

        public DbType DbType
        {
            get { return this.dbType; }
        }

        private bool isTran;

        public Sta(int conIndex)
        {
            XmlConfigService xmlc = new XmlConfigService();
            XmlService x = new XmlService();
            string conStr = x.GetConnectionString(conIndex);
            //this.url = conStr;
            //if (this.url.ToLower().IndexOf("Provider=MSDAORA;".ToLower()) >= 0)
            //{
            //    this.dbType = DbType.OleDb;
            //}
            //else if (this.url.ToLower().IndexOf("charset=utf8;".ToLower()) >= 0)
            //{
            //    this.dbType = DbType.MySQL;
            //}
            //else
            //{
            //    this.dbType = DbType.Oracle;
            //}
            this.SecoundConstructor(conStr);
        }

        public Sta(String connectionString)
        {
            //this.url = connectionString;
            //if (this.url.ToLower().IndexOf("Provider=MSDAORA;".ToLower()) >= 0)
            //{
            //    this.dbType = DbType.OleDb;
            //}
            //else if (this.url.ToLower().IndexOf("charset=utf8;".ToLower()) >= 0)
            //{
            //    this.dbType = DbType.MySQL;
            //}
            //else
            //{
            //    this.dbType = DbType.Oracle;
            //}
            this.SecoundConstructor(connectionString);
        }
        public Sta()
        {
            XmlService x = new XmlService();
            String connectionString = x.GetConnectionString();
            this.SecoundConstructor(connectionString);
        }
        //---------------------------------------------------------------

        private void SecoundConstructor(String connectionString)
        {
            this.url = connectionString;
            if (this.url.ToLower().IndexOf("Provider=MSDAORA;".ToLower()) >= 0)
            {
                this.dbType = DbType.OleDb;
            }
            else if (this.url.ToLower().IndexOf("charset=utf8;".ToLower()) >= 0)
            {
                this.dbType = DbType.MySQL;
            }
            else
            {
                this.dbType = DbType.Oracle;
            }
            //this.SecoundConstructor();

            try
            {
                if (dbType == DbType.Oracle)
                {
                    con = new OracleConnection(url);
                }
                else if (dbType == DbType.OleDb)
                {
                    con = new OleDbConnection(url);
                }
                else if (dbType == DbType.MySQL)
                {
                    con = new MySqlConnection(url);
                }
                cmd = con.CreateCommand();
                con.Open();
                if (dbType == DbType.Oracle)
                {
                    this.Exe("ALTER SESSION SET NLS_SORT = THAI_DICTIONARY");
                }
            }
            catch
            {
                try
                {
                    con.Close();
                }
                catch { }
                throw new StaConnectException();
            }
        }

        private void createOracleConnection()
        {
            try
            {
                if (dbType == DbType.Oracle)
                {
                    if (conOra == null)
                        conOra = new System.Data.OracleClient.OracleConnection(url);
                    if (cmdOra == null)
                        cmdOra = conOra.CreateCommand();
                    if (conOra.State != ConnectionState.Open)
                        conOra.Open();
                }
            }
            catch
            {
                try
                {
                    conOra.Close();
                }
                catch { }
                throw new StaConnectException();
            }
        }

        //---------------------------------------------------------------

        public Sdt Query(String sql)
        {
            Sdt dt = new Sdt();
            cmd.CommandText = sql;
            if (dbType == DbType.MySQL)
            {
                MySqlDataAdapter da = new MySqlDataAdapter((MySqlCommand)cmd);
                da.Fill(dt);
                dt.SecoundConstructor();
            }
            else if (dbType == DbType.Oracle)
            {
                OracleDataAdapter da = new OracleDataAdapter((OracleCommand)cmd);
                da.Fill(dt);
                dt.SecoundConstructor();
            }
            else if (dbType == DbType.OleDb)
            {
                OleDbDataAdapter da = new OleDbDataAdapter((OleDbCommand)cmd);
                da.Fill(dt);
                dt.SecoundConstructor();
            }
            else
            {
                return null;
            }
            return dt;
        }

        public void clearParameters()
        {
            cmd.Parameters.Clear();
        }

        public void addParameterInput(String columnName, object value)
        {
            if (dbType == DbType.Oracle)
            {
                createOracleConnection();
                cmdOra.Parameters.AddWithValue(columnName, value);
            }
        }

        public void addParameterOutput(String columnName)
        {
            if (dbType == DbType.Oracle)
            {
                createOracleConnection();
                cmdOra.Parameters.Add(columnName, System.Data.OracleClient.OracleType.Cursor).Direction = ParameterDirection.Output;
            }
        }

        public Sdt ExecuteProcedure(String procedureName)
        {
            Sdt dt = new Sdt();
            if (dbType == DbType.Oracle)
            {
                cmdOra.CommandText = procedureName;
                cmdOra.CommandType = CommandType.StoredProcedure;
                System.Data.OracleClient.OracleDataAdapter da = new System.Data.OracleClient.OracleDataAdapter(cmdOra);
                da.Fill(dt);
                dt.SecoundConstructor();
            }
            if (dbType == DbType.MySQL)
            {
                cmd.CommandText = procedureName;
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlDataAdapter da = new MySqlDataAdapter((MySqlCommand)cmd);
                da.Fill(dt);
                dt.SecoundConstructor();
            }
            return dt;
        }

        //---------------------------------------------------------------

        public void ExePlSql(String exec_command)
        {
            cmd.Parameters.Clear();
            if (param != null)
            {
                OracleCommand cmdo = (OracleCommand)cmd;
                for (int i = 0; i < param.Count; i++)
                {
                    cmdo.Parameters.Add(param[i]);
                }
            }
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = exec_command;
            cmd.ExecuteNonQuery();
        }

        public void ResetParameter()
        {
            param = new List<OracleParameter>();
        }

        public void AddInParameter(string name, string value)
        {
            if (param == null)
            {
                ResetParameter();
            }
            OracleParameter p = new OracleParameter(name, value);
            param.Add(p);
        }

        public void AddInParameter(string name, object value, OracleType oraType)
        {
            if (param == null)
            {
                ResetParameter();
            }
            if (oraType == OracleType.Clob)
            {
                string v = value.ToString();
                if (string.IsNullOrEmpty(v))
                {
                    value = DBNull.Value;
                }
            }
            OracleParameter p = new OracleParameter(name, oraType);
            p.Value = value;
            param.Add(p);
        }

        public void AddInOutParameter(string name, object value, OracleType oraType)
        {
            if (param == null)
            {
                ResetParameter();
            }
            OracleParameter p = new OracleParameter(name, oraType);
            if (oraType == OracleType.Clob)
            {
                string v = value.ToString();
                if (string.IsNullOrEmpty(v))
                {
                    value = DBNull.Value;
                }
            }
            p.Value = value;
            p.Direction = ParameterDirection.InputOutput;
            if (oraType == OracleType.VarChar)
            {
                p.Size = 999;
            }
            else if (oraType == OracleType.Char)
            {
                p.Size = 30;
            }
            param.Add(p);
        }

        public void AddOutParameter(string name, OracleType oraType)
        {
            if (param == null)
            {
                ResetParameter();
            }
            OracleParameter p = new OracleParameter(name, oraType);
            p.Direction = ParameterDirection.Output;
            if (oraType == OracleType.VarChar)
            {
                p.Size = 999;
            }
            else if (oraType == OracleType.Char)
            {
                p.Size = 30;
            }
            param.Add(p);
        }

        public void AddReturnParameter(string name, OracleType oraType)
        {
            if (param == null)
            {
                ResetParameter();
            }
            OracleParameter p = new OracleParameter(name, oraType);
            p.Direction = ParameterDirection.ReturnValue;
            if (oraType == OracleType.VarChar)
            {
                p.Size = 999;
            }
            else if (oraType == OracleType.Char)
            {
                p.Size = 30;
            }
            param.Add(p);
        }

        public object OutParameter(int index)
        {
            OracleParameter p = param[index];
            return p.Value;
        }

        public object OutParameter(string paramName)
        {
            for (int i = 0; i < param.Count; i++)
            {
                if (paramName.ToLower() == param[i].ParameterName.ToLower())
                {
                    return OutParameter(i);
                }
            }
            return null;
        }

        //---------------------------------------------------------------

        public DataTable QueryDataTable(String sql)
        {
            DataTable dt = new DataTable();
            cmd.CommandText = sql;
            if (dbType == DbType.MySQL)
            {
                MySqlDataAdapter da = new MySqlDataAdapter((MySqlCommand)cmd);
                da.Fill(dt);
            }
            else if (dbType == DbType.Oracle)
            {
                OracleDataAdapter da = new OracleDataAdapter((OracleCommand)cmd);
                da.Fill(dt);
            }
            else if (dbType == DbType.OleDb)
            {
                OleDbDataAdapter da = new OleDbDataAdapter((OleDbCommand)cmd);
                da.Fill(dt);
            }
            else
            {
                return null;
            }
            return dt;
        }

        //---------------------------------------------------------------

        public int Exe(String sql)
        {
            cmd.CommandText = sql;
            return cmd.ExecuteNonQuery();
        }

        //---------------------------------------------------------------

        public bool Transection()
        {
            try
            {
                tran = con.BeginTransaction(IsolationLevel.ReadCommitted);
                cmd.Transaction = tran;
                isTran = true;
            }
            catch
            {
                isTran = false;
            }
            return isTran;
        }

        //---------------------------------------------------------------

        public void RollBack()
        {
            tran.Rollback();
        }

        //---------------------------------------------------------------

        public void RollBack(bool close)
        {
            RollBack();
            if (close) Close();
        }

        //---------------------------------------------------------------

        public void Commit()
        {
            tran.Commit();
        }

        //---------------------------------------------------------------

        public void Commit(bool close)
        {
            Commit();
            if (close) Close();
        }

        //---------------------------------------------------------------

        public void Close()
        {
            try
            {
                con.Close();
            }
            catch { }

            try
            {
                if (conOra != null)
                    conOra.Close();
            }
            catch { }
        }
    }
}
